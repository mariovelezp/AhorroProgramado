using MiMetaBG.WebApp.MiMetaWS;
using MiMetaBG.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MiMetaBG.WebApp.Controllers
{
    public class ValidarController : Controller
    {

        MiMetaWS.WS_MiMetaBG objWsBG = new MiMetaWS.WS_MiMetaBG();

        // GET: Validar
        public ActionResult GetIdentification()
        {
            string msgErrorService;
            string identificacion;
            string CodError;
            string link;
            string ahref;
            string correo;
            string urlCuentas;
            string cliente = string.Empty;
            string clascss = string.Empty;
            string Sesiones = string.Empty;

            if (Session["Consulta"] != null && Session["Consulta"].ToString() != "")
            {
                Sesiones = Session["Consulta"].ToString();
                Session["Consulta"] = Sesiones;
            }

            if (Session["CodError"] == null)
            {
                msgErrorService = "";
                identificacion = "";
                CodError = "0000";
                link = "";
                ahref = "#";  
                correo = "";
                cliente = "";
                clascss = "";
            }
            else
            {
                CodError = Session["CodError"].ToString();
                msgErrorService = Session["msgErrorService"].ToString();
                identificacion = Session["identificacion"].ToString();
                link = Session["link"].ToString();
                ahref= Session["ahref"].ToString();  
                correo = Session["correo"].ToString();
                cliente = Session["cliente"].ToString();
                clascss = TempData["clascss"].ToString();

                if (Session["CodError"] != null) { Session.Remove("CodError"); }
                if (Session["msgErrorService"] != null) { Session.Remove("msgErrorService"); }
                if (Session["identificacion"] != null) { Session.Remove("identificacion"); }
                if (Session["link"] != null) { Session.Remove("link"); }
                if (Session["ahref"] != null) { Session.Remove("ahref"); }   
                if (Session["correo"] != null) { Session.Remove("correo"); }
                if (Session["cliente"] != null) { Session.Remove("cliente"); }
            }
            urlCuentas = ConfigurationManager.AppSettings["urlCreaCuentas"].ToString();  

            ViewBag.urlCuentas = urlCuentas;
            ViewBag.CodError = CodError;
            ViewBag.msgErrorService = msgErrorService;

            ViewBag.identificacion = identificacion;
            ViewBag.link = link;
            ViewBag.ahref = ahref;
            ViewBag.correo = correo;
            ViewBag.cliente = cliente;
            ViewBag.clascss = clascss;

            return View();
        }
        

        public ActionResult GetOTP()
        {
            string identificacion;
            string tipoidentificacion; 
            string correo = "";
            string msgErrorService;
            string CodError;
            string ahref_updateCorreo;
            string cliente = string.Empty;
            string Sesiones = string.Empty;

            if (Session["Consulta"] != null && Session["Consulta"].ToString() != "")
            {
                Sesiones = Session["Consulta"].ToString();
                Session["Consulta"] = Sesiones;
            }

            if (Session["CodError"] == null)
            {
                msgErrorService = "";
                identificacion = "";
                CodError = "0000";
                ahref_updateCorreo = "#";
                
                correo = (Session["correo"] == null) ? "" : Session["correo"].ToString();
                identificacion = (Session["identificacion"] == null) ? "" : Session["identificacion"].ToString();
                tipoidentificacion =  (Session["tipoidentificacion"] == null) ? "" : Session["tipoidentificacion"].ToString();
                cliente = (Session["cliente"] == null) ? "" : Session["cliente"].ToString();     
            }
            else
            {
                CodError = Session["CodError"].ToString();
                msgErrorService = Session["msgErrorService"].ToString();
                identificacion = (Session["identificacion"] == null) ? "" : Session["identificacion"].ToString();
                correo = (Session["correo"] == null) ? "" : Session["correo"].ToString();
                cliente = (Session["cliente"] == null) ? "" : Session["cliente"].ToString();
               
                tipoidentificacion= (Session["tipoidentificacion"] == null) ? "" : Session["tipoidentificacion"].ToString();
                ahref_updateCorreo = (Session["ahref_updateCorreo"] == null) ? "" : Session["ahref_updateCorreo"].ToString();

                Session.Remove("CodError");
                Session.Remove("msgErrorService");
                if (Session["ahref_updateCorreo"] != null) { Session.Remove("ahref_updateCorreos"); }   
            }

            ViewBag.sourcePage = (Session["sourcePage"] == null) ? "" : Session["sourcePage"].ToString();

            if (correo != "") {
                correo = "Hemos enviado un código al correo electrónico " + correo;
            }

            ViewBag.CodError = CodError;
            ViewBag.msgErrorService = msgErrorService;

            ViewBag.identificacion = identificacion;
            ViewBag.tipoidentificacion = tipoidentificacion; 
            ViewBag.correo = correo;
            ViewBag.ahref_updateCorreo = ahref_updateCorreo;

            return View();
        }
        

        public ActionResult ValidaCliente(string cedula, string TipoIdentificion)
        {
            string controlador = "Validar";
            string accion = "GetIdentification";
            string ahref = string.Empty;
            Session["correoSinMascara"] = "";
            Session["fullName"] = "";

            try
            {
                ValidacionClienteRequest request = new ValidacionClienteRequest();
                ValidacionClienteResponse response = new ValidacionClienteResponse();
                General general = new General();
                general.GetLocalData();

                request.aplicacion = general.Aplicacion;
                request.idPantalla = general.idPantalla;
                request.ip = general.Ip;
                request.tipoIdentificacion = TipoIdentificion;
                request.identificacion = cedula.ToUpper();
                response = objWsBG.MiMetaBG_ValidacionCliente(request);
                
                ahref = ConfigurationManager.AppSettings["urlCreaCuentas"].ToString();  
                Session["CodError"] = response.codigoRetorno;
                Session["msgErrorService"] = response.mensajeRetorno;
                Session["identificacion"] = cedula.ToUpper();
                Session["tipoidentificacion"] = TipoIdentificion; 
                Session["link"] = "";
                Session["ahref"] = "#"; 
                Session["correo"] = response.correoElectronico;
                Session["cliente"] = response.nombreCliente;
                TempData["clascss"] = "modal-icon icon-danger"; 
                
                switch (response.codigoRetorno)
                {
                    case "0000": 
                        controlador = "Validar";
                        accion = "GetOTP";
                        Session.Remove("CodError");
                        Session.Remove("msgErrorService");
                        Session.Remove("link");
                        Session.Remove("ahref");
                        Session["correoSinMascara"] = response.correoSinMascara;
                        Session["fullName"] = response.fullName;
                        break;
                    case "0001": 
                        controlador = "Validar";
                        accion = "GetIdentification";
                        break;
                    case "0014": 
                        controlador = "Validar";
                        accion = "GetIdentification";
                        Session["link"] = "Crear cuenta aquí";
                        Session["ahref"] = ahref;
                        break;
                    case "0015":
                        controlador = "Validar";
                        accion = "GetIdentification";
                        Session["link"] = "Crear cuenta aquí";
                        Session["ahref"] = ahref;
                        break;
                    case "0003": 
                        controlador = "Validar";
                        accion = "GetIdentification";
                        Session["link"] = "Crear cuenta aquí";
                        Session["ahref"] = ahref;
                        TempData["clascss"] = "modal-icon icon-account-error";
                        break;
                    default:    
                        controlador = "Validar";
                        accion = "GetIdentification";
                        break;
                }
            }
            catch 
            {
                return RedirectToAction("PageError", "Error");
            }
            return RedirectToAction(accion, controlador);
        }
        

        public ActionResult ValidaOTP(string codigo, string pIdentificion, string pTipoIdentificion, string idSegmentoIN)
        {
            string controlador = "Validar";
            string accion = "GetOTP";
            string consulta = string.Empty;
            string ahref_updateCorreo = "";
            int segmento = 0;
            string clientes = string.Empty;

            try
            {
                ValidacionClienteOTPRequest request = new ValidacionClienteOTPRequest();
                ValidacionClienteOTPResponse response = new ValidacionClienteOTPResponse();
                General general = new General();
                general.GetLocalData();
                request.aplicacion = general.Aplicacion;
                request.idPantalla = general.idPantalla;
                request.ip = general.Ip;
                request.codigoOTP = codigo;
                request.identificacion = pIdentificion;
                request.tipoIdentificacion = pTipoIdentificion;  

                response = objWsBG.MiMetaBG_ValidacionOTP(request);

                ahref_updateCorreo = ConfigurationManager.AppSettings["ahref_updateCorreo"].ToString();
                
                Session["CodError"] = response.codigoRetorno;
                Session["token"] = response.tokenValidacion;
                Session["msgErrorService"] = response.mensajeRetorno;
                Session["ahref_updateCorreo"] = "#"; 
                Session["identificacion"] = pIdentificion;
                Session["tipoidentificacion"] = pTipoIdentificion;
                
                if (idSegmentoIN!= "" && idSegmentoIN!= null)
                    segmento = Convert.ToInt32(idSegmentoIN);

                Session["sourcePage"] = "ENVIO_OTP";

                switch (response.codigoRetorno)
                {
                    case "0000": 
                        Session["token"] = response.tokenValidacion;
                        
                        if (Session["Consulta"] != null && Session["Consulta"].ToString() != "")
                        {
                            controlador = "Metas";
                            if (Session["Consulta"].ToString() == "SI")
                                accion = "ConsultaSuenio";
                            else
                                accion = "CreacionSuenio";
                        }
                        else {
                            controlador = "Home";
                            accion = "Index";
                        }
                        Session.Remove("CodError");
                        Session.Remove("msgErrorService");
                        Session.Remove("ahref_updateCorreo");
                        Session["sourcePage"] = "";
                        break;
                    default:  

                        controlador = "Validar";
                        accion = "GetOTP";
                        Session["ahref_updateCorreo"] = ahref_updateCorreo;
                        break;
                }
            }
            catch
            {
                return RedirectToAction("PageError", "Error");
            }
            return RedirectToAction(accion, controlador, new { idSegmento = segmento});
        }
       

        public ActionResult ReenviarOTP(string pIdent, string pTipoIdent)
        {
            string controlador = "Validar";
            string accion = "GetOTP";

            try
            {
                ValidacionClienteRequest request = new ValidacionClienteRequest();
                General g = new General();
                g.GetLocalData();

                request.aplicacion = g.Aplicacion;
                request.idPantalla = g.idPantalla;
                request.ip = g.Ip;
                request.tipoIdentificacion = pTipoIdent;
                request.identificacion = pIdent;

                ValidacionClienteResponse response = new ValidacionClienteResponse();
                response = objWsBG.MiMetaBG_ValidacionCliente(request);

                Session["CodError"] = response.codigoRetorno;
                Session["msgErrorService"] = response.mensajeRetorno;
                Session["identificacion"] = pIdent;
                Session["tipoidentificacion"] = pTipoIdent;
                Session["link"] = "";
                Session["correo"] = response.correoElectronico;
                Session["sourcePage"] = "";

                switch (response.codigoRetorno)
                {
                    case "0000": 
                        Session.Remove("CodError");
                        Session.Remove("msgErrorService");
                        Session.Remove("link");
                        Session["sourcePage"] = "REENVIO_OTP";
                        break;
                    default:     
                        controlador = "Validar";
                        accion = "GetOTP";
                        break;
                }
            }
            catch
            {
                return RedirectToAction("PageError", "Error");
            }
            return RedirectToAction(accion, controlador);
        }
    }
}