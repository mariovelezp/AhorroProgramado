using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiMetaBG.WebApp.Controllers
{
    public class RedirectController : Controller
    {
        // GET: Redirect
        public ActionResult goToSegmentos()
        {
            return RedirectToAction("consultaSegmentos", "Definir");
        }

       // public ActionResult goToDetSegmento()
        //{
          //  return RedirectToAction("consultaDetSegmento", "Definir");
        //}

        public ActionResult goToGetIdentification()
        {
            return RedirectToAction("GetIdentification", "Validar");
        }

        public ActionResult goToValidaCliente()
        {
            return RedirectToAction("ValidaCliente", "Validar");
        }

        public ActionResult goToGetOTP()
        {
            return RedirectToAction("GetOTP", "Validar");
        }

        public ActionResult goToValidaOTP()
        {
            return RedirectToAction("ValidaOTP", "Validar");
        }

        public ActionResult goToCreaSuenio()
        {
            return RedirectToAction("CreaSuenio", "Validar");
        }

        public ActionResult goToConsultaMasiva()
        {
            return RedirectToAction("ConsultaMetas", "Metas");
        }

    }
}