using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using VGumtree.Model;

namespace VGumtree.Controllers
{
    [Authorize]
    public class PhotoAPIController : ApiController
    {
        private IConfig _config;
        private IRepository _repo;

        public PhotoAPIController(IConfig aConfig, IRepository repo)
        {
            _config = aConfig;   
            _repo = repo;
        }

        // GET api/photoapi
        [AllowAnonymous]
        [HttpGet]
        public string GetImageFolderName(bool uploadFolder)
        {
            return _config.GetImageFolderName();
        }

        //// POST api/photoapi        
        //[HttpPost]
        //[Authorize(Roles = "admin, user")]
        //public async Task<HttpResponseMessage> PostPhoto()
        //{
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    var root = _config.GetTempUploadFolder();
        //    var imageFolder = _config.GetImageUploadFolder();
        //    var provider = new MultipartFormDataStreamProvider(root);
        //    var result = await Request.Content.ReadAsMultipartAsync(provider);
        //    if (result.FormData["adId"] == null)
        //    {
        //        throw new HttpResponseException(HttpStatusCode.BadRequest);
        //    }

        //    var adId = Convert.ToInt32(result.FormData["adId"]);
        //    var photoId = Convert.ToInt16(result.FormData["photoId"]);
        //    //TODO: Do something with the json model which is currently a string


        //    //get the files
        //    string fileName = "";
        //    string ext = "";
        //    foreach (var file in result.FileData)
        //    {
        //        var uploadFileName = file.Headers.ContentDisposition.FileName.Trim('"');

        //        ext = Path.GetExtension(uploadFileName).ToLower();
        //        fileName = string.Format("{0}-{1}{2}", adId, photoId, ext);

        //        // Add photo for ad
        //        Photo newPhoto = new Photo()
        //        {
        //            AdId = adId,
        //            Name = uploadFileName,
        //            Order = photoId,
        //            Url = fileName
        //        };
        //        var ad = _repo.GetById<Ad>(adId);
        //        ad.Photos.Add(newPhoto);

        //        _repo.SaveChanges();
                
        //        File.Move(file.LocalFileName, imageFolder + fileName);
        //    }

        //    return Request.CreateResponse(HttpStatusCode.OK, "success!");
        //}

        // POST api/photoapi        
        [HttpPost]
        [Authorize(Roles = "admin, user")]
        public async Task<HttpResponseMessage> PostPhotos()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            var root = _config.GetTempUploadFolder();
            var provider = new MultipartFormDataStreamProvider(root);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            if (result.FormData["id"] == null)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var adId = Convert.ToInt32(result.FormData["id"]);

            //get the files
            string fileName = "";
            string ext = "";
            int index = 1;
            var imageFolder = _config.GetImageUploadFolder();
            foreach (var file in result.FileData)
            {
                var uploadFileName = file.Headers.ContentDisposition.FileName.Trim('"');

                ext = Path.GetExtension(uploadFileName).ToLower();
                fileName = string.Format("{0}-{1}{2}", adId, index, ext);

                // Add photo for ad
                Photo newPhoto = new Photo()
                {
                    AdId = adId,
                    Name = uploadFileName,
                    Order = index++,
                    Url = fileName
                };
                var ad = _repo.GetById<Ad>(adId);
                ad.Photos.Add(newPhoto);

                _repo.SaveChanges();

                File.Move(file.LocalFileName, imageFolder + fileName);
            }

            return Request.CreateResponse(HttpStatusCode.OK, "success!");
        }
           
    }
}
