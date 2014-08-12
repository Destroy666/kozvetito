using System.Web;
using System.Web.Optimization;

namespace kozvetito
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
                        "~/Content/themes/base/*.css"));

            bundles.Add(new ScriptBundle("~/bundles/msgbox").Include(
                        "~/Scripts/jquery.msgBox.js"));

            bundles.Add(new StyleBundle("~/bundles/msgboxcss").Include(
                        "~/Content/msgBoxLight.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryform").Include(
                        "~/Scripts/jquery.form.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/allas").Include(
                        "~/Content/allas.css"));

            bundles.Add(new StyleBundle("~/bundles/weblap").Include(
                        "~/Content/weblap.css"));

            bundles.Add(new StyleBundle("~/bundles/mainpage").Include(
                        "~/Content/mainpage.css"));
        }
    }
}
