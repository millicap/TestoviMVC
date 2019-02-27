﻿using System.Web;
using System.Web.Optimization;

namespace TestoviMVC
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
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/summernote/summernote.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.min.css",
                      "~/Scripts/summernote/summernote.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/Scripts/jtable-scripts").Include(
                "~/Scripts/jquery-ui-1.12.1.js",
                "~/Scripts/jtable/jquery.jtable.js"));

            bundles.Add(new StyleBundle("~/Content/jtable-css").Include(
                              "~/Content/themes/base/jquery-ui.min.css",
                              "~/Scripts/jtable/themes/metro/blue/jtable.min.css"));

        }
    }
}
