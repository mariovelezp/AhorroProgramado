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
    public class MetasController : Controller
    {
        [HttpGet]
        public ActionResult CreacionSuenio(int idSegmento = 0)
        {
            CuentasResponse response = null;

            string identificacion = string.Empty;
            string tipoidentificacion = string.Empty;
            string msgErrorService = string.Empty;
            string CodError = string.Empty;
            string tokenValidacion = string.Empty;
            string clientes = string.Empty;
            string ctaEnmascarada = string.Empty;
            string ctaAhoEnmascarada = string.Empty;
            Models.metodos metodo = new Models.metodos();

            try
            {
                if (Session["token"] != null && Session["token"].ToString() != "")
                {
                    tokenValidacion = Session["token"].ToString();
                }

                if (System.Web.HttpContext.Current.Session["cliente"] != null && System.Web.HttpContext.Current.Session["cliente"].ToString() != "")
                {
                    clientes = Session["cliente"].ToString();
                }

                using (MiMetaWS.WS_MiMetaBG webService = new MiMetaWS.WS_MiMetaBG())
                {
                    response = new CuentasResponse();
                    CuentasRequest request = new CuentasRequest();
                    Models.General general = new Models.General();
                    general.GetLocalData();

                    Models.Cliente r = new Models.Cliente();

                    if (tokenValidacion != "" && idSegmento != null)
                    {
                        request.idSegmento = idSegmento;
                        request.idPantalla = general.idPantalla;
                        request.aplicacion = general.Aplicacion;
                        request.ip = general.Ip;
                        request.tokenValidacion = tokenValidacion; 

                        response = webService.MiMetaBG_ListaCuentas(request);

                        @ViewData["NombreId"] = clientes;

                        if (response.codigoRetorno == "0000")
                        {
                            ViewBag.imagenes = response.imagenes;
                            r.codigoRetorno = response.codigoRetorno;

                            //Lista de Cuentas solo de Ahorro
                            List<Models.ObtenerUna> listaUna = new List<Models.ObtenerUna>();
                            foreach (var ahorro in response.ListaUna)
                            {
                                ctaAhoEnmascarada = metodo.GenerarCuentaAhorro(ahorro.cta);

                                Models.ObtenerUna objUna = new Models.ObtenerUna()
                                {
                                    ctaEnm = ahorro.ctaEnm,
                                    cta = ctaAhoEnmascarada 
                                };
                                listaUna.Add(objUna);
                            }
                            var ListaCtaAhorro = new SelectList(listaUna, "cta", "ctaEnm");
                            ViewData["cuentasA"] = ListaCtaAhorro;

                            //Lista de Cuentas de Ahorro y Corriente
                            List<Models.ObtenerTodas> listaTodos = new List<Models.ObtenerTodas>();
                            foreach (var a in response.ListaTodas)
                            {
                                ctaEnmascarada = metodo.GenerarToken(a.cta, a.tipo);
                                Models.ObtenerTodas objTodas = new Models.ObtenerTodas()
                                {
                                    ctaEnm = a.ctaEnm,
                                    cta = ctaEnmascarada 
                                };
                                listaTodos.Add(objTodas);
                            }
                            var listaCtaAhoCor = new SelectList(listaTodos, "cta", "ctaEnm");
                            ViewData["cuentasAC"] = listaCtaAhoCor;

                            List<Models.ObtenerMes> listaMes = new List<Models.ObtenerMes>();
                            Object[] arreglo = new Object[32];
                            for (int i = 1; i < arreglo.Length; i++)
                            {
                                Models.ObtenerMes mes = new Models.ObtenerMes()
                                {
                                    fecha = Convert.ToInt32(i).ToString(),
                                    NumFec = Convert.ToInt32(i).ToString()
                                };
                                listaMes.Add(mes);
                            }

                            var listasMes = new SelectList(listaMes, "fecha", "NumFec");
                            ViewData["listasM"] = listasMes;

                            Session["token"] = tokenValidacion;

                        }
                        else if(response.codigoRetorno == "0100")
                            return RedirectToAction("PageSesion", "Error");
                        else
                            return RedirectToAction("PageError", "Error");
                    }
                    else
                        return RedirectToAction("PageSesion", "Error");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("PageError", "Error");
            }
            return View();
        }

        //vmt-jpluas
        [HttpPost]
        public JsonResult CrearSuenio(string RequestJson)
        {
            string respuestaJson = string.Empty;
            string controlador = string.Empty;
            string tokenValidacion = string.Empty;
            string accion = string.Empty;
            string fechaMeta = string.Empty;
            string tipoCuenta = string.Empty;
            string clientes = string.Empty;
            Models.metodos metodo = new Models.metodos();
            string ctaAhorro = string.Empty;
            string[] jsonRequest = new string[0]; 


            string correoSinMascara = string.Empty;
            string fullName = string.Empty;

            if (Session["correoSinMascara"].ToString() != "" && Session["correoSinMascara"] != null) 
            {
                correoSinMascara = Session["correoSinMascara"].ToString();
            }
            if (Session["fullName"].ToString() != "" && Session["fullName"] != null) 
            {
                fullName = Session["fullName"].ToString();
            }
            try
            {
                if (Session["token"] != null && Session["token"].ToString() != "")
                {
                    RequestJson = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(RequestJson, false); 
                    jsonRequest = RequestJson.Split('|');

                    tokenValidacion = Session["token"].ToString();
                    Session["token"] = tokenValidacion;

                    if (Session["cliente"] != null && Session["cliente"].ToString() != "")
                    {
                        clientes = Session["cliente"].ToString();
                    }

                    using (MiMetaWS.WS_MiMetaBG webService = new MiMetaWS.WS_MiMetaBG())
                    {
                        Token token = new Token();
                        token = metodo.ValidaToken(jsonRequest[0]);
                        ctaAhorro = metodo.ValidaCuentaAhorro(jsonRequest[1]);

                        tokenValidacion = Session["token"].ToString();
                        fechaMeta = Convert.ToDateTime(jsonRequest[4]).ToString("yyyy/MM/dd");
                        fechaMeta = fechaMeta.Replace("/", "");

                        if (token.respuesta != "9999" && ctaAhorro != "9999")
                        {
                            MetaRequest request = new MetaRequest();
                            MetaResponse response = new MetaResponse();
                            Models.General general = new Models.General();
                            general.GetLocalData();

                            request.tokenValidacion = tokenValidacion;
                            request.direccionIP = general.Ip;
                            request.idPantalla = general.idPantalla;
                            request.nombreMeta = jsonRequest[5];
                            request.idSegmento = int.Parse(jsonRequest[6]);
                            request.fechaCreacion = DateTime.Now;
                            request.macAdress = "10SJSAJSA";
                            request.accion = "A";
                            request.cuentaMultiple = int.Parse(ctaAhorro); 
                            request.numeroSolicitud = 0;
                            request.tipoCtaDebito = token.tipoDebito; 
                            request.cuentaDebito = token.cuentasDebito;
                            request.montoDebito = jsonRequest[2];
                            request.frencuencia = "M";
                            request.diaUnoDebito = jsonRequest[3];
                            request.diaDosDebito = "0";
                            request.montoMeta = "0";
                            request.fechaVencimiento = fechaMeta;
                            //Mario Velez
                            request.correoSinMascara = correoSinMascara;
                            request.fullName = fullName;
                            request.montoFinalMeta = jsonRequest[7];

                            response = webService.MiMetaBG_Creacion(request);

                            respuestaJson = response.codigoRetorno;

                            if (respuestaJson != "0000" && respuestaJson != "9999" && respuestaJson != "0100")
                            {
                                List<string> jsonReturn = new List<string>();
                                jsonReturn.Add("0001");
                                jsonReturn.Add(response.mensajeRetorno);
                                return Json(jsonReturn);
                            }
                        }
                    }
                }
                else
                    respuestaJson ="0100";
            }
            catch (Exception ex)
            {
                respuestaJson = "9999";
            }
            return Json(respuestaJson);
        }

        //vmt-jpluas
        [HttpGet]
        public ActionResult ConsultaSuenio()
        {
            string tokeValidacion = string.Empty;
            string clientes = string.Empty;
            try
            {
                if (Session["token"] != null && Session["token"].ToString() != "")
                {
                    if (Session["cliente"] != null && Session["cliente"].ToString() != "")
                    {
                       clientes = Session["cliente"].ToString();
                       Session["cliente"] = clientes;
                       ViewData["NombreId"] = clientes;
                    }

                    using (MiMetaWS.WS_MiMetaBG wsMetaBg = new MiMetaWS.WS_MiMetaBG())
                    {
                        ConsultaMetasRequest request = new ConsultaMetasRequest();
                        ConsultaMetasResponse response = new ConsultaMetasResponse();
                        MetasCreadas metasCreadas = new MetasCreadas(); 
                        Models.General general = new Models.General();
                        general.GetLocalData();

                        tokeValidacion = Session["token"].ToString();
                        request.tokenValidacion = tokeValidacion;
                        request.idPantalla = general.idPantalla;
                        request.ip = general.Ip;
                        request.aplicacion = general.Aplicacion;

                        response = wsMetaBg.MiMetaBG_ConsultaMasiva(request);

                        Session["token"] = tokeValidacion;
                        Models.tokenLista tokenLista = new Models.tokenLista();
                        string tokenCuentas = string.Empty;
                        string cuentaAhorro = string.Empty;
                        string cuentaDebito = string.Empty;

                        Models.metodos metodo = new Models.metodos();
                        string ctaEnmascarada = string.Empty;
                        switch (response.codigoRetorno)
                        {
                            case "0000":
                                List<Models.MetasCreada> listaMetas = new List<Models.MetasCreada>();
                                
                                foreach (var item in response.listasMetasCreadas)
                                {
                                    //<------- Se agrego ------>
                                    cuentaAhorro = metodo.GenerarCuentaAhorro(item.cuentaAhorro);
                                    cuentaDebito = metodo.GenerarToken(item.cuentaDebito,item.tipoCuentaDebito);

                                    Models.MetasCreada metas = new Models.MetasCreada()
                                    {
                                        nombreMeta = item.nombreMeta,
                                        montoMeta = item.montoMeta,
                                        montoMetaForn = item.montoMetaform,
                                        numeroSolicitud = item.numeroSolicitud,
                                        cuotaMeta = item.cuotaMeta,
                                        cuotaMetaForm = item.cuotaMetaform,
                                        plazoMeta = item.plazoMeta,
                                        plazoMetaForm = item.plazoMetaform,
                                        cuentaAhorro = cuentaAhorro,   
                                        cuentasEnm = item.cuenta,
                                        idSegmento = item.idSegmento,
                                        emojid = item.emojid,
                                        fechaEmision = item.fechaEmision,
                                        porcentaje = item.porcentaje,
                                        cuentaDebito =  cuentaDebito,  
                                        tipoDebito = item.tipoCuentaDebito,
                                        frecuencia = item.frecuenciaDebito,
                                        diaDeb1 = item.diaDebito1,
                                        diaDeb2 = item.diaDebito2,
                                        valorDebAcumulado = item.valorDebitosAcumulados
                                    };
                                    listaMetas.Add(metas);
                                }

                                metasCreadas.metasCreadas = listaMetas;

                                //Lista de Cuentas de Ahorro y Corriente
                                List<Models.ObtenerTodas> listaTodos = new List<Models.ObtenerTodas>();
                                foreach (var a in response.ListaTodas)
                                {
                                    ctaEnmascarada = metodo.GenerarToken(a.cta, a.tipo);
                                    Models.ObtenerTodas objTodas = new Models.ObtenerTodas()
                                    {
                                        ctaEnm = a.ctaEnm,
                                        cta = ctaEnmascarada
                                    };
                                    listaTodos.Add(objTodas);
                                }
                                var listaCtaAhoCor = new SelectList(listaTodos, "cta", "ctaEnm");

                                //Notificacion de Ventas Cruzadas
                                if (response.ListaVentasCruzadas != null)
                                {
                                    int cont = 0;
                                    List<Models.VentaCruzadas> listaVentas = new List<Models.VentaCruzadas>();    
                                    foreach (var item in response.ListaVentasCruzadas)
                                    {
                                        if (item.codigoCruzar <= 2)
                                        {
                                            Models.VentaCruzadas ventas = new Models.VentaCruzadas()
                                            {
                                                codigoCruzar = item.codigoCruzar,
                                                Valor = item.valorCruzar
                                            };
                                            listaVentas.Add(ventas);
                                            cont++;
                                        }
                                    }

                                    metasCreadas.contador = cont;

                                    ViewData["Ventas"] = "SI";
                                    ViewData["TarjetaCredito"] = ConfigurationManager.AppSettings.Get("UrlTarjetaCredito").ToString();
                                    ViewData["Multicredito"] = ConfigurationManager.AppSettings.Get("UrlMulticredito").ToString();
                                    metasCreadas.ventasCruzada = listaVentas;
                                }
                                else
                                    ViewData["Ventas"] = "NO";

                               
                                ViewData["cuentasAC"] = listaCtaAhoCor;
                                ViewData["listasM"] = listaCtaAhoCor;
                                ViewData["montoInicial"] = response.montoInicial;
                                ViewData["montoFinal"] = response.montoFinal;
                                ViewData["meses"] = getMeses();
                                ViewData["anio"] = getAnio(response.tiempoFinal);

                                ViewData["urlFacebook"] = ConfigurationManager.AppSettings.Get("urlBancoProduccion").ToString();
                                ViewData["DescripcionBancoProduccion"] = ConfigurationManager.AppSettings.Get("DescripcionBancoProduccion").ToString();
                                ViewData["hashtagsBancoProduccion"] = ConfigurationManager.AppSettings.Get("hashtagsBancoProduccion").ToString();

                                return View(metasCreadas);

                            case "0100":
                                return RedirectToAction("PageSesion", "Error");

                            case "8800":
                                return RedirectToAction("consultaSegmentos", "Definir");

                            case "9999":
                                return RedirectToAction("PageError", "Error");
                        }
                    } 
                }
                else
                {
                    Session["Consulta"] = "SI";

                    return RedirectToAction("GetIdentification", "Validar");
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("PageError", "Error");
            }
            return View();
        }

        //vmt-kguacho
        [HttpPost]
        public JsonResult EditarSuenio(string jsonRequest)
        {
            string[] requestPost = new string[0];
            string controlador = string.Empty;
            string tokenValidacion = string.Empty;
            string accion = string.Empty;
            string clientes = string.Empty;
            string respuesta = "";
            string cuentaDebitoSelect = string.Empty;
            Models.Token token = new Models.Token();
            Models.metodos metodo = new Models.metodos();
            string cuentaAhorro = string.Empty;
            int numeroSolicitud = 0;
            try
            {
                jsonRequest = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(jsonRequest, false); 
                requestPost = jsonRequest.Split('|');

                numeroSolicitud = int.Parse(requestPost[1]);
                if (requestPost[7] != "")
                {
                    requestPost[7] = Convert.ToDateTime(requestPost[7]).ToString("yyyy/MM/dd");
                    requestPost[7] = requestPost[7].Replace("/", "");
                }

                if (Session["token"] != null && Session["token"].ToString() != "")
                {
                    tokenValidacion = Session["token"].ToString();
                    Session["token"] = tokenValidacion;

                    using (MiMetaWS.WS_MiMetaBG webService = new MiMetaWS.WS_MiMetaBG())
                    {
                        cuentaAhorro = metodo.ValidaCuentaAhorro(requestPost[0]);
                        token = metodo.ValidaToken(requestPost[2]);

                        if (token.respuesta != "9999" && cuentaAhorro!= "9999")
                        {
                            MetaRequest request = new MetaRequest();
                            MetaResponse response = new MetaResponse();
                            Models.General general = new Models.General();
                            general.GetLocalData();

                            request.tokenValidacion = tokenValidacion;
                            request.direccionIP = general.Ip;
                            request.idPantalla = general.idPantalla;
                            request.nombreMeta = "";
                            request.idSegmento = 0;
                            request.fechaCreacion = DateTime.Now;
                            request.macAdress = "10SJSAJSA";
                            request.accion = "M";
                            request.cuentaMultiple = int.Parse(cuentaAhorro);  
                            request.numeroSolicitud = numeroSolicitud;
                            request.tipoCtaDebito = token.tipoDebito; 
                            request.cuentaDebito = token.cuentasDebito;
                            request.montoDebito = requestPost[3].Replace(" ", "");
                            if (requestPost[7] == "")
                                request.montoMeta = requestPost[6].Replace("$", "").Replace(".00", "").Replace(",", "");
                            else
                                request.montoMeta = "0";

                            request.frencuencia = requestPost[8];
                            request.diaUnoDebito = requestPost[4];
                            if (requestPost[8] != "M")
                                request.diaDosDebito = requestPost[5];
                            else
                                request.diaDosDebito = "0";

                            request.fechaVencimiento = requestPost[7];
                            response = webService.MiMetaBG_Creacion(request);
                            respuesta = response.codigoRetorno;

                            if (respuesta != "0000" && respuesta != "9999" && respuesta != "0100")
                            {
                                List<string> jsonReturn = new List<string>();
                                jsonReturn.Add("0001");
                                jsonReturn.Add(response.mensajeRetorno);
                                return Json(jsonReturn);
                            }
                        }
                        else {
                            List<string> jsonReturn = new List<string>();
                            jsonReturn.Add("0001");
                            jsonReturn.Add("Cuenta invalida");
                            return Json(jsonReturn);
                        }
                    }
                }
                else
                    return Json("0100");
            }
            catch (Exception ex)
            {
                return Json("9999");
            }
            return Json(respuesta);
        }

        //vmt-jpluas
        [HttpPost]
        public JsonResult CancelarSuenio(string jsonRequest)
        {
            string controlador = string.Empty;
            string tokenValidacion = string.Empty;
            string accion = string.Empty;
            string respuesta = string.Empty;

            Models.metodos metodo = new Models.metodos();
            string ctaAhorro = string.Empty;
            string ctaDebito = string.Empty;
            string[] requestPost = new string[0];

            try
            {
                jsonRequest = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(jsonRequest, false); 
                requestPost = jsonRequest.Split('|');

                int numeroSolicitud = int.Parse(requestPost[1]);
                string fechaVencimiento = requestPost[7];

                if (Session["token"] != null && Session["token"].ToString() != "")
                {
                    tokenValidacion = Session["token"].ToString();
                    Session["token"] = tokenValidacion;

                    using (MiMetaWS.WS_MiMetaBG webService = new MiMetaWS.WS_MiMetaBG())
                    {
                        // ------ Se agrego ------  //
                        Token respuestaToken = metodo.ValidaToken(requestPost[2]);
                        ctaAhorro = metodo.ValidaCuentaAhorro(requestPost[0]);

                        if (ctaAhorro != "9999" && respuestaToken.respuesta != "9999")
                        {
                            if (fechaVencimiento != "")
                            {
                                fechaVencimiento = Convert.ToDateTime(fechaVencimiento).ToString("yyyy/MM/dd");
                                fechaVencimiento = fechaVencimiento.Replace("/", "");
                            }
                            else
                                fechaVencimiento = "0";

                            MetaRequest request = new MetaRequest();
                            MetaResponse response = new MetaResponse();
                            Models.General general = new Models.General();
                            general.GetLocalData();

                            request.tokenValidacion = tokenValidacion;
                            request.direccionIP = general.Ip;
                            request.idPantalla = general.idPantalla;
                            request.nombreMeta = "";
                            request.idSegmento = 0;
                            request.macAdress = "";
                            request.fechaCreacion = DateTime.Now;
                            request.accion = "P";
                            request.cuentaMultiple = int.Parse(ctaAhorro);  
                            request.numeroSolicitud = numeroSolicitud;
                            request.tipoCtaDebito = respuestaToken.tipoDebito;  
                            request.cuentaDebito = respuestaToken.cuentasDebito;
                            request.montoDebito = requestPost[3];
                            request.frencuencia = requestPost[8];
                            request.diaUnoDebito = requestPost[4];
                            request.diaDosDebito = requestPost[5];
                            request.montoMeta = requestPost[6];
                            request.fechaVencimiento = fechaVencimiento;
                            response = webService.MiMetaBG_Creacion(request);

                            respuesta = response.codigoRetorno;
                            Session["token"] = tokenValidacion;

                            if (respuesta != "0000" && respuesta != "9999" && respuesta != "0100")
                            {
                                List<string> jsonReturn = new List<string>();
                                jsonReturn.Add("0001");
                                jsonReturn.Add(response.mensajeRetorno);
                                return Json(jsonReturn);
                            }      
                        }
                        else
                        {
                            List<string> jsonReturn = new List<string>();
                            jsonReturn.Add("0001");
                            jsonReturn.Add("Cuenta invalida");
                            return Json(jsonReturn);
                        }
                    }
                }
                else
                    respuesta="0100";
            }
            catch (Exception ex)
            {
                respuesta = "9999";
            }
            return Json(respuesta);
        }

        //vmt-jpluas
        [HttpPost]
        public JsonResult ExtraSuenio(string jsonRequest)
        {
            string respuestaJson = string.Empty;
            respuestaJson = "";

            string tokenValidacion = string.Empty;
            string accion = string.Empty;
            string respuesta = string.Empty;

            Models.metodos metodo = new Models.metodos();
            string ctaAhorro = string.Empty;
            string ctaDebito = string.Empty;
            string tipoCuentaExtra =  string.Empty;
            string[] requestPost = new string[0];

            try
            {
                jsonRequest = System.Web.Security.AntiXss.AntiXssEncoder.HtmlEncode(jsonRequest, false);
                requestPost = jsonRequest.Split('|');

                if (Session["token"] != null && Session["token"].ToString() != "")    
                {
                    tokenValidacion = Session["token"].ToString();
                    Session["token"] = tokenValidacion;

                    using (MiMetaWS.WS_MiMetaBG webService = new MiMetaWS.WS_MiMetaBG())
                    {
                        Token respuestaToken = metodo.ValidaToken(requestPost[2]);
                        ctaAhorro = metodo.ValidaCuentaAhorro(requestPost[0]);

                        if (ctaAhorro != "9999" && respuestaToken.respuesta != "9999")
                        {
                            if (respuestaToken.tipoDebito == "A")
                                tipoCuentaExtra = "CA";
                            else
                                tipoCuentaExtra = "CC";

                            MetaAdicionalRequest request = new MetaAdicionalRequest();
                            MetaAdicionalResponse response = new MetaAdicionalResponse();
                            Models.General general = new Models.General();
                            general.GetLocalData();

                            request.aplicativo = "";
                            request.idPantalla = "";
                            request.tokenValidacion = tokenValidacion;
                            request.ip = general.Ip;
                            request.macAddress = "hsshaksa";
                            request.ctaC = ctaAhorro;
                            request.ctaD = respuestaToken.cuentasDebito;
                            request.ctaEmpresa = respuestaToken.cuentasDebito;
                            request.numeroSolicitud = requestPost[1]; 
                            request.referencia = "TRANSFERENCIA AHORRO META";
                            request.tipoTarC = "CA";
                            request.tipoTarD = tipoCuentaExtra;
                            request.valorD = requestPost[3];
                            request.IdTipoBeneficiario = "N";

                            response = webService.MiMetaBG_ValorAdicional(request);
                            respuesta = response.codigoRetorno;

                            if(respuesta != "0000" && respuesta != "9999" && respuesta != "0100")
                            {
                                List<string> jsonReturn = new List<string>();
                                jsonReturn.Add("0001");
                                jsonReturn.Add(response.mensajeRetorno);
                                return Json(jsonReturn);
                            }
                        }
                        else
                        {
                            List<string> jsonReturn = new List<string>();
                            jsonReturn.Add("0001");
                            jsonReturn.Add("Cuenta invalida");
                            return Json(jsonReturn);
                        }
                    }
                }
                else
                    respuesta = "0100";
            }
            catch (Exception ex)
            {
                respuesta = "9999";
            }
            return Json(respuesta);
        }

        [HttpPost]
        public JsonResult ActualizarVtaCruzada(int VecesMostradas)
        {
            string respuestaJson = string.Empty;
            string tokenValidacion = string.Empty;
            string tarjetaCredito = string.Empty;
            string multicredito = string.Empty;

            try
            {
                if (Session["token"] != null && Session["token"].ToString() != "")
                {
                    tokenValidacion = Session["token"].ToString();
                    Session["token"] = tokenValidacion;
                    tarjetaCredito = ConfigurationManager.AppSettings.Get("UrlTarjetaCredito").ToString();
                    multicredito = ConfigurationManager.AppSettings.Get("UrlMulticredito").ToString();

                    using (MiMetaWS.WS_MiMetaBG webService = new MiMetaWS.WS_MiMetaBG())
                    {
                        VentasCruzadasRequest request = new VentasCruzadasRequest();
                        VentaCruzadaResponse response = new VentaCruzadaResponse();
                        Models.General general = new Models.General();
                        general.GetLocalData();

                        request.tokenValidacion = tokenValidacion;
                        request.ip = general.Ip;
                        request.idPantalla = "ActualizarVtasCruzadas";
                        request.cantidadVecesMostradas = 0;
                        request.aplicacion = "Metas";

                        response = webService.MiMetaBG_VentasCruzadas(request);

                        respuestaJson = response.codigoRetorno;
                    }
                }
                else
                    respuestaJson = "0100";
            }
            catch (Exception ex)
            {
                respuestaJson = "9999";
            }
            return Json(respuestaJson);
        }


        public List<string> getMeses()
        {
            List<string> meses = new List<string>();

            meses.Add("Enero");
            meses.Add("Febrero");
            meses.Add("Marzo");
            meses.Add("Abril");
            meses.Add("Mayo");
            meses.Add("Junio");
            meses.Add("Julio");
            meses.Add("Agosto");
            meses.Add("Septiembre");
            meses.Add("Octubre");
            meses.Add("Noviembre");
            meses.Add("Diciembre");

            return meses;
        }


        public string conevertMesToNumber(string anioLetras)
        {
            string anio = "";

            if (anioLetras == "Enero")
                anio = "01";
            else if (anioLetras == "Febrero")
                anio = "02";
            else if (anioLetras == "Marzo")
                anio = "03";
            else if (anioLetras == "Abril")
                anio = "04";
            else if (anioLetras == "Mayo")
                anio = "05";
            else if (anioLetras == "Junio")
                anio = "06";
            else if (anioLetras == "Julio")
                anio = "07";
            else if (anioLetras == "Agosto")
                anio = "08";
            else if (anioLetras == "Septiembre")
                anio = "09";
            else if (anioLetras == "Octubre")
                anio = "10";
            else if (anioLetras == "Noviembre")
                anio = "11";
            else if (anioLetras == "Diciembre")
                anio = "12";

            return anio;
        }


        public List<int> getAnio(int limite)
        {
            List<int> listAnio = new List<int>();
            int anio = DateTime.Now.Year;

            for (int i = 0; i <= limite; i++)
            {
                listAnio.Add(anio++);
            }

            return listAnio;
        }
    }
}