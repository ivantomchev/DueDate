using System.Web;
using System.Web.Optimization;

namespace Project.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            RegisterScriptBundles(bundles);
            RegisterStyleBundles(bundles);

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //TODO Turn true in Production
            BundleTable.EnableOptimizations = false;
        }
          //<script type="text/javascript" src="/scripts/moment.min.js"></script>
          //<script type="text/javascript" src="/scripts/bootstrap.min.js"></script>
          //<script type="text/javascript" src="/scripts/bootstrap-datetimepicker.*js"></script>

          //<!-- include your less or built css files  -->
          //<!-- 
          //bootstrap-datetimepicker-build.less will pull in "../bootstrap/variables.less" and "bootstrap-datetimepicker.less";
          //or
          //<link rel="stylesheet" href="/Content/bootstrap-datetimepicker.css" />

        private static void RegisterStyleBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Scripts/Chosen/chosen.css",
                      "~/Content/bootstrap.css",
                      "~/Content/datepicker3.css",
                      "~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/custom").Include(
                "~/Content/Site.css"));
        }

        private static void RegisterScriptBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/activate-datetimepicker").Include(
                        "~/Scripts/custom/activate-datetimepicker.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/moment").Include(
                        "~/Scripts/moment.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-datetimepicker").Include(
                        "~/Scripts/bootstrap-datetimepicker.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/jquery.unobtrusive-ajax").Include(
            "~/Scripts/jquery.unobtrusive-ajax.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/jquery.validate.unobtrusive").Include(
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.unobtrusive.js"
            ));

            bundles.Add(new ScriptBundle("~/bundles/scroll-to-top").Include(
            "~/Scripts/custom/scroll-to-top.js"));

            bundles.Add(new ScriptBundle("~/bundles/page-go-top").Include(
            "~/Scripts/custom/page-go-top.js"));

            bundles.Add(new ScriptBundle("~/bundles/activate-chosen").Include(
            "~/Scripts/custom/activate-chosen.js"));

            bundles.Add(new ScriptBundle("~/bundles/item-delete").Include(
            "~/Scripts/custom/item-delete.js"));

            bundles.Add(new ScriptBundle("~/bundles/modal").Include(
                        "~/Scripts/Modal/modalform.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/Chosen").Include(
                        "~/Scripts/chosen/chosen.jquery.js"));
        }
    }
}
