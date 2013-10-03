using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using VGumtree.Model;

namespace VGumtree.Controllers
{
    public class CategoryAPIController : ApiController
    {
        private IRepository _repo;

        public CategoryAPIController(IRepository repo)
        {
            _repo = repo;
        }

        // GET api/categoryapi
        public IEnumerable<Category> GetCategory()
        {
            _repo.DisableProxyCreation();
            return _repo.GetQueryable<Category>().ToList();
        }

        // GET api/categoryapi
        public IEnumerable<SubCategory> GetSubCategory(bool subCatOnly)
        {
            if (subCatOnly)
            {
                _repo.DisableProxyCreation();
                return _repo.GetQueryable<SubCategory>().ToList();
            }
            else return null;
        }
    }
}
