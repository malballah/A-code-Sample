using System.Web;
using System.Web.Optimization;

namespace Store
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/assets/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/assets/js/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/commonjs").Include(
                        "~/assets/js/jquery.bootgrid.js",
                        "~/assets/js/toastr.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/assets/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/assets/js/bootstrap.js",
                      "~/assets/js/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/assets/css/bootstrap.css",
                      "~/assets/css/site.css"));
            bundles.Add(new StyleBundle("~/Content/commoncss").Include(
                      "~/assets/css/jquery.bootgrid.css",
                      "~/assets/css/toastr.css"));
        }
    }
}
