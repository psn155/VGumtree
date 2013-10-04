using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Web.WebPages.OAuth;
using VGumtree.Models;

namespace VGumtree
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // To let users of this site log in using their accounts from other sites such as Microsoft, Facebook, and Twitter,
            // you must update this site. For more information visit http://go.microsoft.com/fwlink/?LinkID=252166

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            OAuthWebSecurity.RegisterTwitterClient(
                consumerKey: "VFhO6vgkBSgvkAlCtiD2gQ",
                consumerSecret: "ojxwOpkpehlVAYcUpWJ3SQCwC20BEOqpNhk41PSdRg");

            OAuthWebSecurity.RegisterFacebookClient(
                appId: "572848439419608",
                appSecret: "bb73d991a53edd25d9e3c8859622388f");

            OAuthWebSecurity.RegisterGoogleClient();
        }
    }
}
