using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MiMetaBG.WebApp.MiMetaWS;


namespace MiMetaBG.WebApp.Controllers
{
    public class DefinirController : Controller
    {
        MiMetaWS.WS_MiMetaBG objWsBG = new MiMetaWS.WS_MiMetaBG();

        
        // GET: Definir
        public ActionResult consultaSegmentos ()
        {
            string tokenValidacion = string.Empty;
            try
            {
                SegmentoRequest request = new SegmentoRequest();
                SegmentoResponse response = new SegmentoResponse();

                if (System.Web.HttpContext.Current.Session["token"] != null && System.Web.HttpContext.Current.Session["token"].ToString() != "")
                {
                    tokenValidacion = System.Web.HttpContext.Current.Session["token"].ToString();
                    System.Web.HttpContext.Current.Session["token"] = tokenValidacion;
                }

                Models.General g = new Models.General();
                g.GetLocalData();
                
                request.MarcaImagen = 1;
                request.Aplicacion = g.Aplicacion;
                request.idPantalla = g.idPantalla;
                request.Ip = g.Ip;

                response = objWsBG.MiMetaBG_ConsultaSegmentos(request);

                if (response.codigoRetorno == "0000")
                {
                    List<Models.Segmento> lista = new List<Models.Segmento>();

                    foreach (var item in response.listaSegmento)
                    {
                        Models.Segmento data = new Models.Segmento();

                        data.idSegmento = item.idSegmento;
                        data.nombre = item.nombre;
                        data.descripcion = item.descripcion;
                        data.imagen = item.imagen;
                        data.emojin = item.emojin;

                        lista.Add(data);
                    }
                    return View(lista); 
                }
                else {
                    return RedirectToAction("PageError", "Error");
                }
            }
            catch
            {
                return RedirectToAction("PageError", "Error");
            }
        }


        public ActionResult consultaDetSegmento(string idSeg)
        {    
            ParametroRequest request = new ParametroRequest();
            ParametroResponse response = new ParametroResponse();
            Models.General general = new Models.General();
            general.GetLocalData();
            string tokenValidacion = string.Empty;

            Models.ParametroResponse data = null;
            try {
                if (idSeg == null) {
                    request.MarcaImagen = 3; //Devuelve todas las imagenes de los segmentos
                    request.idSegmento = 8;
                }
                else
                {
                    request.MarcaImagen = 3; //Devuelve la imagenes del segmento en particular
                    request.idSegmento = Int32.Parse(idSeg);
                }

                ViewBag.idSegmento = idSeg;

                ViewBag.MontoSelected = 0;
                ViewBag.TiempoSelected = 0;
                request.Ip = general.Ip;
                request.Aplicacion = "";
                request.idPantalla = "";
                response = objWsBG.MiMetaBG_Monto_Tiempo(request);

                if (response.codigoRetorno == "0000")
                {
                    if (System.Web.HttpContext.Current.Session["token"] != null && System.Web.HttpContext.Current.Session["token"].ToString() != "")
                    {
                        tokenValidacion = System.Web.HttpContext.Current.Session["token"].ToString();
                        System.Web.HttpContext.Current.Session["token"] = tokenValidacion;
                    }
                    data = new Models.ParametroResponse();

                    data.codigoRetorno = response.codigoRetorno;
                    data.mensajeRetorno = response.mensajeRetorno;
                    data.montoInicial = response.montoInicial;
                    data.montoFinal = response.montoFinal;
                    data.tiempoIncial = response.tiempoIncial;
                    data.tiempoFinal = response.tiempoFinal;
                    data.mensaje = response.mensaje;

                    ViewBag.MontoSelected = data.montoInicial;
                    ViewBag.TiempoSelected = data.tiempoIncial;

                    List<Models.ImagSegmento> lista = new List<Models.ImagSegmento>();

                    foreach (var item in response.imagSegmento)
                    {
                        Models.ImagSegmento img = new Models.ImagSegmento();
                        img.segmentoId = item.segmentoId;
                        img.emojid = item.emojid;

                        lista.Add(img);
                    }
                    data.imagSegmento = lista;
                }
                else
                {
                    return RedirectToAction("PageError", "Error");
                }
            }
            catch(Exception e)
            {
                return RedirectToAction("PageError", "Error");
            }
            return View(data);
        }


        public ActionResult ValidarToken(int idSegmentoIN = 0)
        {
            string CodigoError = string.Empty;

            try
            {
                Session["Consulta"] = "NO";  //
                if (Session["token"] != null && Session["token"].ToString() != "")
                {
                    using (MiMetaWS.WS_MiMetaBG wsMetaBg = new MiMetaWS.WS_MiMetaBG())
                    {
                        TokenResponse response = new TokenResponse();
                        TokenRequest request = new TokenRequest();
                        
                        request.tokenValidacion = Session["token"].ToString();
                        request.ip = "";
                        request.idPantalla = "";
                        request.aplicacion = "";

                        response = wsMetaBg.MiMetaBG_ValidarToken(request);

                        if (response.codigoRetorno == "0000")
                        {
                            Session["token"] = request.tokenValidacion;
                            return RedirectToAction("CreacionSuenio", "Metas", new { idSegmento = idSegmentoIN });
                        }
                        else
                        {
                            return RedirectToAction("GetIdentification", "Validar");
                        }
                    }
                }
                else
                {
                    return RedirectToAction("GetIdentification", "Validar");
                }
            }
            catch (Exception ex)
            {

            }
            return View();
        }
    }
}