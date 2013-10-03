using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VGumtree.Model;

namespace VGumtree.Controllers
{
    public class LocationAPIController : ApiController
    {
        private IRepository _repo;

        public LocationAPIController(IRepository repo)
        {
            _repo = repo;
        }

        // GET api/LocationAPI
        public IEnumerable<Country> GetCountries(bool country)
        {
            _repo.DisableProxyCreation();
            return _repo.GetQueryable<Country>().ToList();
        } 

        // GET api/LocationAPI
        public IEnumerable<AdminAreaLevel2> GetAdminAreaLevel2(bool adminAreaLevel2)
        {
            _repo.DisableProxyCreation();
            return _repo.GetQueryable<AdminAreaLevel2>().ToList();
        } 

        // GET api/LocationAPI
        public IEnumerable<AdminAreaLevel1> GetAdminAreaLevel1(bool adminAreaLevel1)
        {
            _repo.DisableProxyCreation();
            return _repo.GetQueryable<AdminAreaLevel1>().ToList();
        } 
    }
}
