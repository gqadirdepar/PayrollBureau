using System.Web;
using System.Web.Optimization;

namespace PayrollBureau
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.ResetAll();

            bundles.Add(new ScriptBundle("~/Scripts/bower").Include(
               "~/bower_components/jquery/dist/jquery.min.js",
               "~/bower_components/jquery-validation/dist/jquery.validate.min.js",
               "~/bower_components/jquery-validation-unobtrusive/jquery.validate.unobtrusive.min.js",
               "~/bower_components/bootstrap/dist/js/bootstrap.min.js",
               "~/bower_components/moment/min/moment.min.js",
               "~/bower_components/angular/angular.min.js",
               "~/bower_components/angular-animate/angular-animate.min.js",
               "~/bower_components/angular-sanitize/angular-sanitize.min.js",
               "~/bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js",
               "~/bower_components/angular-responsive-tables/release/angular-responsive-tables.min.js",
               "~/bower_components/angular-ui-select/dist/select.min.js",
               "~/bower_components/angular-ui-uploader/dist/uploader.min.js",
               "~/bower_components/bootbox/bootbox.js",
               "~/bower_components/ngBootbox/ngBootbox.js"
               
               ));



            bundles.Add(new ScriptBundle("~/Scripts/Application").Include(
                "~/Scripts/Angular/Moment.js",
                "~/Scripts/Angular/app.js",
               "~/Scripts/Angular/Prototypes/*.js",
                 "~/Scripts/Angular/Controllers/*.js",
                "~/Scripts/Angular/Services/*.js",
                "~/Scripts/App/*.js"
                ));


            bundles.Add(new StyleBundle("~/Content/bower").Include(
                 "~/bower_components/bootstrap/dist/css/bootstrap.min.css",
                 "~/bower_components/angular-responsive-tables/release/angular-responsive-tables.min.css",
                 "~/bower_components/angular-ui-select/dist/select.min.css",
                 "~/bower_components/font-awesome/css/font-awesome.min.css",
                 "~/bower_components/less-space/dist/less-space.min.css",
                 "~/bower_components/bootstrap-side-navbar/source/assets/stylesheets/navbar-fixed-side.css",
                 "~/bower_components/bootstrap-daterangepicker/daterangepicker.css",
                  "~/bower_components/ngprogress/ngProgress.css"
                 ));


            bundles.Add(new StyleBundle("~/Content/Application").Include(
                "~/Content/css/*.min.css"
                ));
        }
    }
}
