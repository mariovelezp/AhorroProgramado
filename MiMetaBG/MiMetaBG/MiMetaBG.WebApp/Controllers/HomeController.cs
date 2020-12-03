using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiMetaBG.WebApp.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {            
            return View();
        }

        public ActionResult mostrarSegmento()
        {
            return RedirectToAction("consultaSegmentos", "Definir");
        }

        public ActionResult CerrarSesion()
        {
            if (Session["token"] != null && Session["token"].ToString() != "")
            {
                Session.Remove("token");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}