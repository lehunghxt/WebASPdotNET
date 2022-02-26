using System.Web;
using System.Web.Optimization;

namespace Web.Backend
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/bower_components/jquery/dist/jquery.min.js",
                        "~/Scripts/jquery.bpopup-0.11.0.min.js"));
            //"~/Scripts/jquery.datetimepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Content/bower_components/bootstrap/dist/js/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/datetimepicker").Include(
                      "~/scripts/moment.min.js",
                      "~/scripts/bootstrap-datetimepicker.js"));

                     bundles.Add(new ScriptBundle("~/bundles/js").Include(
                        "~/Content/bower_components/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/Content/bower_components/datatables.net/js/jquery.dataTables.min.js",
                        "~/Content/bower_components/fastclick/lib/fastclick.js",
                        "~/Content/bower_components/moment/min/moment.min.js",
                        "~/Content/dist/js/adminlte.min.js",
                        "~/Content/dist/js/demo.js",
                        "~/Content/bower_components/datatables.net-bs/js/dataTables.bootstrap.min.js",
                        "~/Content/bower_components/ckeditor/ckeditor.js",
                        "~/Content/bower_components/chart.js/Chart.2.3.0.min.js",
                        "~/Content/autocomplete/jquery.autocomplete.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/layout").Include(
                        "~/Scripts/Layout/layout.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bower_components/bootstrap/dist/css/bootstrap.min.css",
                      "~/Content/bower_components/font-awesome/css/font-awesome.min.css",
                      "~/Content/bower_components/Ionicons/css/ionicons.min.css",
                      "~/Content/bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css",
                      "~/Content/dist/css/AdminLTE.min.css",
                      "~/Content/dist/css/skins/_all-skins.min.css",
                      "~/Content/Style.css",
                      "~/Content/receipt.css",
                      "~/Content/bootstrap-datetimepicker.css"
                      ));
        }
    }
}
