using System.Web;
using System.Web.Optimization;

namespace HTTT_QLyBanDongHo
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));


            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            // bunder js admin template


            // bunder  Client template

            // css
            bundles.Add(new StyleBundle("~/ContentClient/css").Include(
                "~/Assets/assets/css/main.css",
                "~/Assets/assets/css/blue.css",
                "~/Assets/assets/css/owl.carousel.css",
                "~/Assets/assets/css/owl.transitions.css",
                "~/Assets/assets/css/animate.min.css",
                "~/Assets/assets/css/rateit.css",
                "~/Assets/assets/css/bootstrap-select.min.css",
                "~/Assets/assets/css/font-awesome.css",
                "~/Assets/assets/css/bootstrap.min.css",
                "~/Assets/assets/css/simple-line-icons.css",
                "~/Assets/assets/css/simple-line-icons.css"));
            // JS
            bundles.Add(new ScriptBundle("~/ContentClient/jquery").Include(
                "~/Assets/assets/js/jquery-1.11.1.min.js",
                "~/Assets/assets/js/bootstrap.min.js",
                "~/Assets/assets/js/bootstrap-hover-dropdown.min.js",
                "~/Assets/assets/js/owl.carousel.min.js",
                "~/Assets/assets/js/countdown.js",
                "~/Assets/assets/js/echo.min.js",
                "~/Assets/assets/js/jquery.easing-1.3.min.js",
                "~/Assets/assets/js/bootstrap-slider.min.js",
                "~/Assets/assets/js/jquery.rateit.min.js",
                "~/Assets/assets/js/lightbox.min.js",
                "~/Assets/assets/js/bootstrap-select.min.js",
                "~/Assets/assets/js/wow.min.js",
                "~/Assets/assets/js/scripts.js"));

            BundleTable.EnableOptimizations = true;
        }

    }
}
