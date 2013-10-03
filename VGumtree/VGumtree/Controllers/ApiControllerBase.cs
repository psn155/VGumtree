using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using VGumtree.Model;
using WebMatrix.WebData;

namespace VGumtree.Controllers
{
    public class ApiControllerBase : ApiController
    {     
        //public ApiControllerBase()
        //{
        //}

        /// <summary>
        /// Check if current user has Admin role
        /// </summary>
        /// <returns></returns>
        protected bool IsAdmin()
        {
            return User.IsInRole(Config.AdminRole);            
        }

        /// <summary>
        /// Check if an ad belongs to the current user
        /// </summary>
        /// <param name="ad"></param>
        /// <returns></returns>
        protected bool AdBelongsToUser(Ad ad)
        {
            return ad.UserId == WebSecurity.GetUserId(User.Identity.Name);
        }

        protected bool CanEdit(Ad ad)
        {
            return IsAdmin() || AdBelongsToUser(ad);
        }

        protected bool CanDelete(Ad ad)
        {
            return IsAdmin() || AdBelongsToUser(ad);
        }
    }
}