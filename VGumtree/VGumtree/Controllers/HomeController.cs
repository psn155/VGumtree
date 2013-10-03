using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VGumtree.Model;
using System.Data.Entity;

namespace VGumtree.Controllers
{
    public class HomeController : Controller
    {
        private IRepository _repo;

        public HomeController(IRepository repo)
        {
            _repo = repo;
        }

        public ActionResult Index()
        {
            ViewBag.Module = "adAppModule";
            //ViewBag.UserId = User.Identity.Name;
            var curUser = _repo.GetQueryable<User>().Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
            ViewBag.UserId = curUser != null ? curUser.UserId : 0;     
            //TO DO: need to get correct user role here                
            ViewBag.UserRole = "user";
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
