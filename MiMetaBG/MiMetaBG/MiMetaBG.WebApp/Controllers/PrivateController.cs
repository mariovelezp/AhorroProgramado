using MiMetaBG.WebApp.MiMetaWS;
using MiMetaBG.WebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MiMetaBG.WebApp.Controllers
{
    public class PrivateController : Controller
    {
        MiMetaWS.WS_MiMetaBG objWsBG = new MiMetaWS.WS_MiMetaBG();
         
        // GET: Private
        public ActionResult Index() {
            ViewBag.msgRetorno = "";
            ViewBag.display = "none";
            if (TempData["msgRetorno"] != null) { 
               ViewBag.msgRetorno = TempData["msgRetorno"].ToString();
                ViewBag.display = "block";
            }
            return View();
        }
        /**************************************************************************/
        public ActionResult ValidaUser(string usuario) {
            string controlador = "Private";
            string accion = "SegPrincipal";
            string GetUser = "";
            string user_code = "";

            TempData["msgRetorno"] = "";
            Session["initial_page"] = "";

            try
            {
                prvLoginRequest r = new prvLoginRequest();

                user_code = System.Configuration.ConfigurationManager.AppSettings["private_user_code"].ToString();
                r.Id_codigo = int.Parse(user_code);
                
                prvLoginResponse s = new prvLoginResponse();
                s = objWsBG.MiMetaBG_ConsultaUsuario(r);

                if (s.codigoRetorno == "0000") {
                    GetUser = s.listaUsuario[0].Aplicacion.ToString();
                    if (usuario != GetUser) {
                        accion = "Index";
                        TempData["msgRetorno"] = "Usuario ingresado incorrecto.";
                        return RedirectToAction(accion, controlador);
                    }
                }
                else {
                   return RedirectToAction("PrivatePageError", "Error");
                }
            }
            catch
            {
                return RedirectToAction("PrivatePageError", "Error");
            }
            Session["initial_page"] = "index";
            return RedirectToAction(accion, controlador);
        }
        /**************************************************************************/
        public ActionResult SegPrincipal() {
            if (Session["initial_page"] != null) { 
                if (Session["initial_page"].ToString() == "index") {


                    try
                    {
                        MiMetaWS.prvSegmentoGralRequest r = new MiMetaWS.prvSegmentoGralRequest();

                        Models.General g = new Models.General();
                        g.GetLocalData();

                        r.MarcaImagen = 1;
                        r.Aplicacion = g.Aplicacion;
                        r.idPantalla = g.idPantalla;
                        r.Ip = g.Ip;

                        MiMetaWS.prvSegmentoGralResponse s = new MiMetaWS.prvSegmentoGralResponse();
                        s = objWsBG.MiMetaBG_ConsGeneralSegmento(r);

                        if (s.codigoRetorno == "0000")
                        {
                            List<Models.SegmentoGral> lista = new List<Models.SegmentoGral>();

                            foreach (var item in s.listaSegmento)
                            {
                                Models.SegmentoGral data = new Models.SegmentoGral();

                                data.idSegmento = item.idSegmento;
                                data.nombre = item.nombre;
                                data.descripcion = item.descripcion;
                                data.imagen = item.imagen;
                                data.emojin = item.emojin;

                                data.codimagen = item.codimagen;
                                data.umbral = item.umbral;
                                data.estado = item.estado;
                                data.prod_cruz = item.prod_cruz;
                                data.prioridad = item.prioridad;
                                data.mensaje_seg = item.mensaje_seg;
                                data.cod_host = item.cod_host;

                                lista.Add(data);
                            }
                            return View(lista);
                        }
                        else
                        {
                            return RedirectToAction("PrivatePageError", "Error");
                        }
                    }
                    catch
                    {
                        return RedirectToAction("PrivatePageError", "Error");
                    }

                }
            }

            return RedirectToAction("Index", "Private");
        }
        /**************************************************************************/
        public ActionResult ImgPrincipal() {
            if (Session["initial_page"] != null)
            {
                if (Session["initial_page"].ToString() == "index")
                {
                    try
                    {
                        MiMetaWS.prvImagenGralRequest r = new MiMetaWS.prvImagenGralRequest();

                        Models.General g = new Models.General();
                        g.GetLocalData();

                        r.MarcaImagen = 1;
                        r.Aplicacion = g.Aplicacion;
                        r.idPantalla = g.idPantalla;
                        r.Ip = g.Ip;

                        MiMetaWS.prvImagenGralResponse s = new MiMetaWS.prvImagenGralResponse();
                        s = objWsBG.MiMetaBG_ConsGeneralImagen(r);

                        if (s.codigoRetorno == "0000")
                        {
                            List<Models.ImagenGral> lista = new List<Models.ImagenGral>();

                            foreach (var item in s.listaImagen)
                            {
                                Models.ImagenGral data = new Models.ImagenGral();

                                data.codimagen = item.codimagen;
                                data.imagen = item.imagen;
                                data.emojin = item.emojin;

                                lista.Add(data);
                            }
                            return View(lista);
                        }
                        else
                        {
                            return RedirectToAction("PrivatePageError", "Error");
                        }
                    }
                    catch
                    {
                        return RedirectToAction("PrivatePageError", "Error");
                    }

                }
            }

            return RedirectToAction("Index", "Private");
        }
        /**************************************************************************/
        public ActionResult SegConsImagenes()
        {
            if (Session["initial_page"] != null)
            {
                if (Session["initial_page"].ToString() == "index")
                {
                    try
                    {
                        MiMetaWS.prvImagenGralRequest r = new MiMetaWS.prvImagenGralRequest();

                        Models.General g = new Models.General();
                        g.GetLocalData();

                        r.MarcaImagen = 1;
                        r.Aplicacion = g.Aplicacion;
                        r.idPantalla = g.idPantalla;
                        r.Ip = g.Ip;

                        MiMetaWS.prvImagenGralResponse s = new MiMetaWS.prvImagenGralResponse();
                        s = objWsBG.MiMetaBG_ConsGeneralImagen(r);

                        if (s.codigoRetorno == "0000")
                        {
                            List<Models.ImagenGral> lista = new List<Models.ImagenGral>();

                            foreach (var item in s.listaImagen)
                            {
                                Models.ImagenGral data = new Models.ImagenGral();

                                data.codimagen = item.codimagen;
                                data.imagen = item.imagen;
                                data.emojin = item.emojin;

                                lista.Add(data);
                            }
                            return View(lista);
                        }
                        else
                        {
                            return RedirectToAction("PrivatePageError", "Error");
                        }
                    }
                    catch
                    {
                        return RedirectToAction("PrivatePageError", "Error");
                    }

                }
            }

            return RedirectToAction("Index", "Private");
        }
        /**************************************************************************/
        public ActionResult SegConsProductos()
        {
            if (Session["initial_page"] != null)
            {
                if (Session["initial_page"].ToString() == "index")
                {
                    try
                    {
                        MiMetaWS.prvProductoGralRequest r = new MiMetaWS.prvProductoGralRequest();

                        Models.General g = new Models.General();
                        g.GetLocalData();

                        r.MarcaImagen = 1;
                        r.Aplicacion = g.Aplicacion;
                        r.idPantalla = g.idPantalla;
                        r.Ip = g.Ip;

                        MiMetaWS.prvProductoGralResponse s = new MiMetaWS.prvProductoGralResponse();
                        s = objWsBG.MiMetaBG_ConsGeneralProducto(r);

                        if (s.codigoRetorno == "0000")
                        {
                            List<Models.ProductoGral> lista = new List<Models.ProductoGral>();

                            foreach (var item in s.listaProducto)
                            {
                                Models.ProductoGral data = new Models.ProductoGral();

                                data.idproducto = item.idproducto;
                                data.descripcionproducto = item.descripcionproducto;

                                lista.Add(data);
                            }
                            return View(lista);
                        }
                        else
                        {
                            return RedirectToAction("PrivatePageError", "Error");
                        }
                    }
                    catch
                    {
                        return RedirectToAction("PrivatePageError", "Error");
                    }

                }
            }

            return RedirectToAction("Index", "Private");
        }
        /*******************************VIENE DESDE EL LINK CREAR SEGMENTO*******************************************/
        [HttpGet]
        public ActionResult SaveSegment() {
            if ((Session["initial_page"] == null) || (Session["initial_page"].ToString() != "index")) {
                return RedirectToAction("Index", "Private");
            }

            ViewBag.IdSegmento = "    ";
            ViewBag.nombre = "";
            ViewBag.descripcion = "";
            ViewBag.codimagen = "";
            ViewBag.umbral = "";
            ViewBag.estado = "";
            ViewBag.producto = "";
            ViewBag.prioridad = "";
            ViewBag.mensaje_seg = "";
            ViewBag.cod_host = "";

            ViewBag.MensajeOk = "";

            if (TempData["origen"] != null)
            {
                if (TempData["origen"].ToString() == "save" || TempData["origen"].ToString() == "edit")
                {
                    ViewBag.IdSegmento = TempData["IdSegmento2"].ToString();
                    ViewBag.nombre = TempData["nombre2"].ToString();
                    ViewBag.descripcion = TempData["descripcion2"].ToString();
                    ViewBag.codimagen = TempData["codimagen2"].ToString();
                    ViewBag.umbral = TempData["umbral2"].ToString();
                    ViewBag.estado = TempData["estado2"].ToString();
                    ViewBag.producto = TempData["producto2"].ToString();
                    ViewBag.prioridad = TempData["prioridad2"].ToString();
                    ViewBag.mensaje_seg = TempData["mensaje_seg2"].ToString();
                    ViewBag.cod_host = TempData["cod_host2"].ToString();
                }
                if (TempData["origen"].ToString() == "save")
                {
                    ViewBag.MensajeOk = "!Segmento guardado con exito!";
                }
                TempData["origen"] = "";
            }
            return View();
        }
        /*******************************VIENE CUANDO GUARDO Y DEVOLVIO 0000*******************************************/
        [HttpGet]
        public ActionResult SaveSegmentResp(string origen)
        {
            TempData["IdSegmento2"] = TempData["IdSegmento"].ToString();
            TempData["nombre2"] = TempData["nombre"].ToString();
            TempData["descripcion2"] = TempData["descripcion"].ToString();
            TempData["codimagen2"] = TempData["codimagen"].ToString();
            TempData["umbral2"] = TempData["umbral"].ToString();
            TempData["estado2"] = TempData["estado"].ToString();
            TempData["producto2"] = TempData["producto"].ToString();
            TempData["prioridad2"] = TempData["prioridad"].ToString();
            TempData["mensaje_seg2"] = TempData["mensaje_seg"].ToString();
            TempData["cod_host2"] = TempData["cod_host"].ToString();

            TempData["origen"] = origen;
            return RedirectToAction("SaveSegment", "Private");
        }
        /*******************************VIENE DESDE LINK EDITAR DEL DETALLE DE SEGMENTOS**********************************/
        [HttpPost]
        public ActionResult SaveSegmentEdit(string opcion, string IdSegmento, string nombre, string descripcion, string codimagen, string umbral, string estado, string producto, string prioridad, string mensaje_seg, string cod_host)
        {

            TempData["IdSegmento2"] = IdSegmento;
            TempData["nombre2"] = nombre;
            TempData["descripcion2"] = descripcion;
            TempData["codimagen2"] = codimagen;
            TempData["umbral2"] = umbral;
            TempData["estado2"] = estado;
            TempData["producto2"] = producto;
            TempData["prioridad2"] = prioridad;
            TempData["mensaje_seg2"] = mensaje_seg;
            TempData["cod_host2"] = cod_host;
            

            TempData["origen"] = "edit";
            return RedirectToAction("SaveSegment", "Private");
        }
        /*******************************VIENE DEL BOTON GUARDAR*******************************************/
        [HttpPost]
        public ActionResult SaveSegment(string IdSegmento, string nombre, string descripcion, string codimagen, string umbral, string estado, string producto, string prioridad, string mensaje_seg, string cod_host)
        {
            string controlador = string.Empty;
            string accion = string.Empty;
            string IdSegmentoAux;

            IdSegmentoAux = IdSegmento;
            try 
            {
                using (MiMetaWS.WS_MiMetaBG objWsBG = new MiMetaWS.WS_MiMetaBG())
                {
                    prvSegmentoRequest  request = new prvSegmentoRequest();
                    prvSegmentoResponse response = new prvSegmentoResponse();

                    request.nombre = nombre;
                    request.descripcion = descripcion;
                    request.codimagen = codimagen;
                    request.umbral = umbral;
                    request.estado = estado;
                    request.prod_cruz = producto;
                    request.prioridad = prioridad;
                    request.mensaje_seg = mensaje_seg;
                    request.cod_host = cod_host;

                    if (IdSegmento == "    ") {
                        IdSegmentoAux = "9999";
                    }
                    response = objWsBG.MiMetaBG_GrabaSegmento(Int32.Parse(IdSegmentoAux), request);

                    TempData["IdSegmento"] = IdSegmento;
                    TempData["nombre"] = nombre;
                    TempData["descripcion"] = descripcion;
                    TempData["codimagen"] = codimagen;
                    TempData["umbral"] = umbral;
                    TempData["estado"] = estado;
                    TempData["producto"] = producto;
                    TempData["prioridad"] = prioridad;
                    TempData["mensaje_seg"] = mensaje_seg;
                    TempData["cod_host"] = cod_host;
                    
                    switch (response.codigoRetorno)
                    {
                        case "0000":
                            controlador = "Private";
                            accion = "SaveSegmentResp";
                            return RedirectToAction(accion, controlador, new { origen = "save" });
                            break;
                        default:
                            controlador = "Error";
                            accion = "PrivatePageError";
                            return RedirectToAction(accion, controlador);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("PrivatePageError", "Error");
            }
        }
        /*******************************VIENE CUANDO GUARDO Y DEVOLVIO 0000*******************************************/
        [HttpGet]
        public ActionResult SaveImagenResp(string origen)
        {
            TempData["Codimagen2"] = TempData["Codimagen"].ToString();
            TempData["iorigen"] = origen;
            return RedirectToAction("SaveImagen", "Private");
        }
        /*******************************VIENE DESDE LINK EDITAR DEL DETALLE DE IMAGENES**********************************/
        [HttpPost]
        public ActionResult SaveImagenEdit(string opcion, string Codimagen) {

            TempData["Codimagen2"] = Codimagen;

            TempData["iorigen"] = "edit";
            return RedirectToAction("SaveImagen", "Private");
        }
        /*******************************VIENE DESDE EL LINK CREAR IMAGEN*******************************************/
        [HttpGet]
        public ActionResult SaveImagen() {
            if ((Session["initial_page"] == null) || (Session["initial_page"].ToString() != "index"))
            {
                return RedirectToAction("Index", "Private");
            }

            ViewBag.Codimagen = "    ";
           
            ViewBag.imagen = "";
            ViewBag.emojin = "";
           
            ViewBag.iMensajeOk = "";

            if (TempData["iorigen"] != null)
            {
                if (TempData["iorigen"].ToString() == "save" || TempData["iorigen"].ToString() == "edit")
                {
                    ViewBag.Codimagen = TempData["Codimagen2"].ToString();
                   
                }
                if (TempData["iorigen"].ToString() == "save")
                {
                    ViewBag.iMensajeOk = "!Imagen de segmento guardado con exito!";
                }
                TempData["iorigen"] = "";
            }
            return View();
        }
        /*******************************VIENE DEL BOTON GUARDAR*******************************************/
        [HttpPost]
        public ActionResult SaveImagen(string Codimagen, HttpPostedFileBase fileimagen, HttpPostedFileBase fileemoji)
        {
            string controlador = string.Empty;
            string accion = string.Empty;
            string CodimagenAux;

            CodimagenAux = Codimagen;
            try
            {
                using (MiMetaWS.WS_MiMetaBG objWsBG = new MiMetaWS.WS_MiMetaBG())
                {
                    prvImagenRequest request = new prvImagenRequest();
                    prvImagenResponse response = new prvImagenResponse();

                    if (fileimagen != null && fileimagen.ContentLength > 0)
                    {
                        byte[] ImagenData = null;
                        using (var imagen = new BinaryReader(fileimagen.InputStream))
                        {
                            ImagenData = imagen.ReadBytes(fileimagen.ContentLength);
                        }
                        request.imagen = ImagenData;
                    }

                    if (fileemoji != null && fileemoji.ContentLength > 0)
                    {
                        byte[] Imagen2Data = null;
                        using (var imagen = new BinaryReader(fileemoji.InputStream))
                        {
                            Imagen2Data = imagen.ReadBytes(fileemoji.ContentLength);
                        }
                        request.emojin = Imagen2Data;
                    }

                    if (Codimagen == "    ")
                    {
                        CodimagenAux = "9999";
                    }
                    response = objWsBG.MiMetaBG_GrabaImagen(Int32.Parse(CodimagenAux), request);

                    TempData["Codimagen"] = Codimagen;

                    switch (response.codigoRetorno)
                    {
                        case "0000":
                            controlador = "Private";
                            accion = "SaveImagenResp";
                            return RedirectToAction(accion, controlador, new { origen = "save" });
                            break;
                        default:
                            controlador = "Error";
                            accion = "PrivatePageError";
                            return RedirectToAction(accion, controlador);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("PrivatePageError", "Error");
            }
            
        }
    }

}

