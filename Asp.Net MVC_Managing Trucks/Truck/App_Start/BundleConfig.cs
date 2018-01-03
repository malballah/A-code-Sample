using System.Web;
using System.Web.Optimization;

namespace Truck
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/assets/js/jquery-{version}.js"));
           
            bundles.Add(new ScriptBundle("~/bundles/main").Include(
                "~/assets/js/jquery.knob.js",
                "~/assets/js/toastr.min.js",
                "~/assets/js/moment.min.js",
                "~/assets/js/jquery.datetimepicker.js",
                "~/assets/js/theme.js",
                "~/assets/js/app.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/assets/js/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/assets/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/assets/js/bootstrap.min.js",
                      "~/assets/js/respond.js"));

            bundles.Add(new StyleBundle("~/css/main").Include(
                      "~/assets/css/bootstrap.min.css",
                      "~/assets/css/bootstrap-overrides.css",
                      "~/assets/css/jquery.bootgrid.min.css",
                      "~/assets/css/jquery-ui-1.10.2.custom.css",
                      "~/assets/css/jquery.datetimepicker.css",
                      "~/assets/css/font-awesome.css",
                      "~/assets/css/layout.css",
                      "~/assets/css/elements.css",
                      "~/assets/css/icons.css",
                      "~/assets/css/toastr.min.css",
                      "~/assets/css/site.css"));
        }
    }
}
