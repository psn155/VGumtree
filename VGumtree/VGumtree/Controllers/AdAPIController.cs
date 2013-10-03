using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VGumtree.Model;
using System.Data.Entity;
using VGumtree.Models;
using System.Data.Spatial;
using WebMatrix.WebData;
using System.Web;

namespace VGumtree.Controllers
{
    [Authorize]
    public class AdApiController : ApiControllerBase
    {
        private IRepository _repo;

        public AdApiController(IRepository repo)
        {
            _repo = repo;
        }

        // GET api/adapi
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Ad> Get()
        {
            _repo.DisableProxyCreation();
            //// Include two levels: AdAttributes and Attribute
            //return _repo.GetQueryable<Ad>().Include(x => x.AdAttributes.Select(c => c.Attribute)).ToList();
            IList<Ad> data = _repo.GetQueryable<Ad>().Where(x => x.IsActive).Include(x => x.Photos).ToList();
            HidePhoneNumbers(ref data);           
            return data;
        }

        // GET api/adapi/5
        [AllowAnonymous]
        [HttpGet]
        public Ad Get(int id)
        {
            _repo.DisableProxyCreation();
            //return _repo.GetQueryable<Ad>().Where(x => x.Id == id).Include(x => x.AdAttributes.Select(c => c.Attribute)).FirstOrDefault();
            IList<Ad> data = _repo.GetQueryable<Ad>().Where(x => x.Id == id).Include(x => x.Photos).ToList();
            HidePhoneNumbers(ref data);
            return data.FirstOrDefault();
        }

        // GET api/adapi?myAds=true
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Ad> GetMyAds(bool myAds)
        {
            if (myAds)
            {
                _repo.DisableProxyCreation();
                int userId = WebSecurity.GetUserId(User.Identity.Name);
                if (userId > 0)
                {
                    return _repo.GetQueryable<Ad>().Where(x => x.UserId == userId).Include(x => x.Photos).ToList();
                }
            }
            return null;
        }

        // GET api/adapi?subCategoryId=5
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Ad> GetBySubCategory(int subCategoryId)
        {
            _repo.DisableProxyCreation();
            IList<Ad> data = _repo.GetQueryable<Ad>().Where(x => x.IsActive && x.SubCategoryId == subCategoryId).Include(x => x.Photos).ToList();
            HidePhoneNumbers(ref data);
            return data;
        }

        // GET api/adapi?locationId=5
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Ad> GetByLocation(int locationId)
        {
            //TO-DO
            throw new Exception();
        }

        // GET api/adapi?subCategoryId=5
        [AllowAnonymous]
        [HttpGet]
        public IEnumerable<Ad> GetBySearchCriteria(int? catId, int? subCatId, int? adminAreaLevel1Id, int? adminAreaLevel2Id, string keyword)
        {
            _repo.DisableProxyCreation();

            var data = _repo.GetQueryable<Ad>().Where(x => x.IsActive);

            // Criteria for searching cat/subcat/location
            if (catId != null)
                data = data.Where(x => x.SubCategory.CategoryId == catId);
            if (subCatId != null)
                data = data.Where(x => x.SubCategoryId == subCatId);
            if (adminAreaLevel1Id != null)
                data = data.Where(x => x.Locations.Any(l => l.AdminAreaLevel2.AdminAreaLevel1Id == adminAreaLevel1Id));
            if (adminAreaLevel2Id != null)
                data = data.Where(x => x.Locations.Any(l => l.AdminAreaLevel2Id == adminAreaLevel2Id));
                
            //System.Threading.Thread.Sleep(3000);

            // TO DO: need to implement search fields properly
            // Temporarily search by keyword only
            if (!string.IsNullOrWhiteSpace(keyword) && !keyword.ToUpper().Equals("NULL"))
            {
                data = data.Where(x => x.Name.Contains(keyword) || x.Description.Contains(keyword) || x.SubCategory.Name.Contains(keyword));
            }

            IList<Ad> result = data.Include(x => x.Photos).ToList();
            HidePhoneNumbers(ref result);
            return result;
        }

        //TODO: NEED TO IMPLEMENT [ValidateAntiForgeryToken] for this
        [AllowAnonymous]
        [HttpGet]
        public string GetFullPhoneNumber(int id, bool fullPhoneNumber)
        {
            var ad = _repo.GetById<Ad>(id);
            if (ad != null)
                return _repo.GetById<Ad>(id).ContactPhone;
            else
                return null;
        }

        // POST api/adapi
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public HttpResponseMessage Post(Ad ad)
        {
            if (ModelState.IsValid)
            {
                //WebMatrix.WebData.WebSecurity.                   
                try
                {
                    // Initializing data
                    ad.CreatedDate = DateTime.Now;
                    ad.IsActive = true;
                    ad.UserId = WebSecurity.CurrentUserId;
                    
                    if (ad.Locations != null)
                    {
                        foreach (var loc in ad.Locations)
                        {
                            // Create DbGeopgraphy data for GeoLocation field
                            if (loc.Latitude != 0 && loc.Longtitude != 0)
                            {
                                loc.GeoLocation = DbGeography.FromText(string.Format("POINT({0} {1})", loc.Longtitude.ToString(), loc.Latitude.ToString()));
                            }

                            // Get AdminAreaLevel2
                            if (loc.AdminAreaLevel2 != null)
                            {
                                var adminAreaLevel2 = _repo.GetQueryable<AdminAreaLevel2>().Where(x => x.Name.Equals(loc.AdminAreaLevel2.Name.Trim())).FirstOrDefault();
                                // If adminAreaLevel2 already exists, just reference it. Otherwise, create a new one
                                if (adminAreaLevel2 != null) 
                                {
                                    loc.AdminAreaLevel2Id = adminAreaLevel2.Id;
                                    loc.AdminAreaLevel2 = null;
                                }

                                // Get AdminAreaLevel1
                                if (loc.AdminAreaLevel2.AdminAreaLevel1 != null)
                                {
                                    var adminAreaLevel1 = _repo.GetQueryable<AdminAreaLevel1>().Where(x => x.Name.Equals(loc.AdminAreaLevel2.AdminAreaLevel1.Name.Trim())).FirstOrDefault();
                                    // If adminAreaLevel1 already exists, just reference it. Otherwise, create a new one
                                    if (adminAreaLevel1 != null)
                                    {
                                        loc.AdminAreaLevel2.AdminAreaLevel1Id = adminAreaLevel1.Id;
                                        loc.AdminAreaLevel2.AdminAreaLevel1 = null;
                                    }
                                }
                            }                           
                        }
                    }

                    var newlyCreatedAd = _repo.Add<Ad>(ad);
                 
                    _repo.SaveChanges();
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, newlyCreatedAd);
                    response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = newlyCreatedAd.Id }));
                    return response;
                }
                catch (DbUpdateException)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }    

        // PUT api/adapi/5
        [HttpPut]
        [Authorize(Roles = "admin, user")]        
        public HttpResponseMessage Put(int id, Ad ad)
        {
            if (ModelState.IsValid && id == ad.Id)
            {
                ad.ModifiedDate = DateTime.Now;
                try
                {
                    _repo.Attach<Ad>(ad);
                    _repo.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, ad);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // TO DO: Implement AntiForgeryToken here [System.Web.Mvc.ValidateAntiForgeryToken]
        //        Check for errors when logging back to the system using saved cookies (not direct log in)
        // DELETE api/adapi/5
        [HttpDelete]
        [Authorize(Roles = "admin, user")]        
        public HttpResponseMessage Delete(int id)
        {
            var ad = _repo.GetById<Ad>(id);

            if (ad != null && CanDelete(ad))
            {

                #region Physical delete
                /*
                // Delete all references tables first
                // First: Delete attributes
                var adAttrs = new List<AdAttribute>(ad.AdAttributes);

                foreach (var adAttr in adAttrs)
                {
                    _repo.Delete<AdAttribute>(adAttr);
                }

                // Second: Delete Locations
                var locs = new List<Location>(ad.Locations);

                if (locs != null)
                {
                    foreach (var loc in locs)
                    {
                        _repo.Delete<Location>(loc);
                    }
                }

                // Then delete the ad
                ad = _repo.DeleteById<Ad>(id);
                */
                #endregion
            
                // Just flag the ad as inactive
                ad.IsActive = false;
                ad.InactiveDate = DateTime.Now;
                try
                {
                    _repo.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, ad);
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.Conflict);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        /// <summary>
        /// Method to mask phone numbers for ads. Only when users click on the masked phone number, full number can appear by communicating back to the server
        /// </summary>
        /// <param name="ads"></param>
        private void HidePhoneNumbers(ref IList<Ad> ads)
        {
            foreach (var ad in ads)
            {
                if (ad.ContactPhone.Length > 5)
                {
                    ad.ContactPhone = ad.ContactPhone.Substring(0, ad.ContactPhone.Length - 5) + "*****";
                }
                else
                {
                    ad.ContactPhone = "*****";
                }
            }
        }
    }
}
