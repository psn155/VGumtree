using System.Web;
using System.Web.Optimization;

namespace VGumtree
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        //"~/Scripts/angular.min.js",
                        "~/Scripts/angular.js",
                        "~/Scripts/angular-cookies.js",
                        //"~/Content/assets/bootstrap/js/bootstrap.min.js",
                        "~/Content/assets/bootstrap/js/bootstrap.js",
                        "~/Content/assets/js/jquery.flexslider.js",
                        "~/Content/assets/js/jquery.tweet.js",
                        "~/Content/assets/js/jflickrfeed.js",
                        "~/Content/assets/js/jquery.ui.map.min.js",
                        "~/Content/assets/js/jquery.quicksand.js",
                        "~/Content/assets/prettyPhoto/js/jquery.prettyPhoto.js",
                        "~/Content/assets/js/scripts.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(                       
                        //"~/Content/assets/bootstrap/css/bootstrap.min.css",
                        "~/Content/assets/bootstrap/css/bootstrap.css",
                        "~/Content/assets/prettyPhoto/css/prettyPhoto.css",
                        "~/Content/assets/css/flexslider.css",
                        "~/Content/assets/css/font-awesome.css",
                        "~/Content/assets/css/style.css"
                        ));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));
        }
    }
}