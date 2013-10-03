using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VGumtree.Model;
using System.Data.Entity;

namespace VGumtree.Controllers
{
    public class AttrAPIController : ApiController
    {
        private IRepository _repo;

        public AttrAPIController(IRepository repo)
        {
            _repo = repo;
        }

        // GET api/attrapi
        public IEnumerable<VGumtree.Model.AdAttribute> GetByAd(int adId)
        {
            _repo.DisableProxyCreation();
            return _repo.GetQueryable<VGumtree.Model.AdAttribute>().Where(x => x.AdId == adId).Include(x => x.Attribute).ToList();
        }

        // GET api/attrapi
        public IEnumerable<VGumtree.Model.Attribute> GetBySubCatId(int subCatId)
        {
            _repo.DisableProxyCreation();            
            return _repo.GetQueryable<VGumtree.Model.Attribute>().Where(x => x.SubCategoryId == subCatId).ToList();
        }
    }
}
