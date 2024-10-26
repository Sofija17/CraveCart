using System.Web;
using System.Web.Optimization;

namespace CraveCart
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // jQuery
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            // jQuery Validation
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Modernizr
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            // Bootstrap and DataTables
            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                                   "~/Scripts/bootstrap.js",
                                   "~/Scripts/bootbox.js",
                                   "~/Scripts/DataTables/jquery.dataTables.js",
                                   "~/Scripts/DataTables/dataTables.bootstrap.js"));

            // jQuery UI
            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            // CSS Bundles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                               "~/Content/bootstrap.css",
                               "~/Content/site.css",
                               "~/Content/DataTables/css/dataTables.bootstrap.css"));

            // jQuery UI CSS
            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
                        "~/Content/themes/base/jquery-ui.css")); // Adjust the path if your theme is different
        }
    }
}
