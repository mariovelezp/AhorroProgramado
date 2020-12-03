using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiMetaBG.WebApp.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult PageError()
        {
            return View();
        }
        public ActionResult PrivatePageError()
        {
            return View();
        }
        public ActionResult PageSesion()
        {
            return View();
        }

        public ActionResult ConsultaSuenio()
        {
            return View();
        }

        public ActionResult LimpiarSesion() 
        {
            if (Session["token"] != null && Session["token"].ToString() != "")
            {
                Session.Remove("token");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}