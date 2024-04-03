﻿using System.Web;
using System.Web.Optimization;

namespace SMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/commonjs").Include(
                        "~/Scripts/Common.js"));
            
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));


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

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include(
                  "~/Scripts/kendo/2017.2.621/kendo.all.min.js",
                  "~/Scripts/kendo/2017.2.621/jszip.min.js",
                  "~/Scripts/kendo/2017.2.621/kendo.aspnetmvc.min.js"));

            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                        "~/Content/kendo/2017.2.621/kendo.common.min.css",
                        "~/Content/kendo/2017.2.621/kendo.metro.min.css"));

            bundles.IgnoreList.Clear();
        }
    }
}
