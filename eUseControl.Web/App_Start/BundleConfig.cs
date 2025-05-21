using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace eUseControl.Web
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            AreaRegistration.RegisterAllAreas();
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            // Add jQuery bundle
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                                       "~/Scripts/jquery-{version}.js"));

            // Add jQuery validation bundle
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                                       "~/Scripts/jquery.validate*"));

            // Add Modernizr bundle
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                                       "~/Scripts/modernizr-*"));

            // Add Bootstrap bundle
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                                     "~/Scripts/bootstrap.js",
                                                          "~/Scripts/respond.js"));

            // Add CSS bundle
            bundles.Add(new StyleBundle("~/Content/css").Include(
                                     "~/Content/bootstrap.css",
                                                          "~/Content/site.css"));


        }
    }
}   