using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;


namespace MiMetaBG.WebApp.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {


            bundles.Add(new StyleBundle("~/Content/css/main.css").Include(
                                        "~/Content/css/main.css"));

            bundles.Add(new ScriptBundle("~/Scripts/ComboImagen.js").Include(
                        "~/Scripts/ComboImagen.js"));

            bundles.Add(new StyleBundle("~/Content/ComboImagen.css").Include(
                                        "~/Content/ComboImagen.css"));

            bundles.Add(new ScriptBundle("~/Scripts/js/vendor/bootstrap.min.js").Include(
                        "~/Scripts/js/vendor/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js/vendor/jquery-3.3.1.min.js").Include(
                        "~/Scripts/js/vendor/jquery-3.3.1.slim.min.js",

                        "~/Scripts/js/vendor/popper.min.js"));

            bundles.Add(new ScriptBundle("~/Scripts/js/main.js").Include(
                        "~/Scripts/js/main.js"));

            bundles.Add(new ScriptBundle("~/Scripts/ComboImagen.js").Include(
                        "~/Scripts/ComboImagen.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/controlRangos.js").Include(
                "~/Scripts/controlRangos.js"
            ));

            bundles.Add(new ScriptBundle("~/Scripts/js/main.js").Include(
              "~/Scripts/js/main.js"));
        }    
    }
}