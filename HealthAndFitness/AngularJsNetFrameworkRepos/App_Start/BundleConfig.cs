using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Optimization;

namespace AngularJsNetFrameworkRepos
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            String timeStamp = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            //            bundles.Add(new ScriptBundle("~/bundles/vendor")
            //#if DEBUG
            //                .IncludeDirectory("~/Scripts/Vendor_dev/Ring0", "*.js", true)
            //                .IncludeDirectory("~/Scripts/Vendor_dev/Ring1", "*.js", true)
            //                .IncludeDirectory("~/Scripts/Vendor_dev/Ring2", "*.js", true)
            //                .IncludeDirectory("~/Scripts/Vendor_dev/Ring3", "*.js", true)
            //#else
            //                .IncludeDirectory("~/Scripts/Vendor/Ring0", "*.js", true)
            //                .IncludeDirectory("~/Scripts/Vendor/Ring1", "*.js", true)
            //                .IncludeDirectory("~/Scripts/Vendor/Ring2", "*.js", true)
            //                .IncludeDirectory("~/Scripts/Vendor/Ring3", "*.js", true)
            //#endif

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.unobtrusive*",
                "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/knockout").Include(
                "~/Scripts/knockout-{version}.js",
                "~/Scripts/knockout.validation.js"));

            bundles.Add(new ScriptBundle("~/bundles/app").Include(
                "~/Scripts/sammy-{version}.js",
                "~/Scripts/app/common.js",
                "~/Scripts/app/app.datamodel.js",
                "~/Scripts/app/app.viewmodel.js",
                "~/Scripts/app/home.viewmodel.js",
                "~/Scripts/app/_run.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                 "~/Content/bootstrap.css",
                 "~/Content/Site.css"));
                 //"~/Content/Site.css?v=" + timeStamp));

            // AngularJS
            bundles.Add(new Bundle("~/bundles/angularjs").Include(
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-sanitize.min.js",
                "~/Scripts/angular-animate.min.js"
                ));
            // VGuan AngularJs Codes ......
            bundles.Add(new ScriptBundle("~/bundles/demoApp").IncludeDirectory("~/ngApp", "*.js", true)
    );
        }
    }
}
