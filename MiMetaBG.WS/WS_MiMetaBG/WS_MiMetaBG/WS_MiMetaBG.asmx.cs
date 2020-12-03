using MiMetaBG_AccesoDatos;
using MiMetaBG_AccesoDatos.Catalogos;
using MiMetaBG_AccesoDatos.Cliente;
using MiMetaBG_AccesoDatos.Meta;
using MiMetaBG_AccesoDatos.Token;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Net;
using System.Threading;
using System.Web.Script.Serialization;
using System.Web.Services;
using WS_MiMetaBG.WsCashBV_BG;

namespace WS_MiMetaBG
{

    /// Descripción breve de WS_MiMetaBG
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]


    public class WS_MiMetaBG : System.Web.Services.WebService
    {

        MetodosOrquestador metodos = new MetodosOrquestador();

        //SegmentoRequest request
        [WebMethod(Description = "MiMetaBG: Consultas de Segmentos")]
        public SegmentoResponse MiMetaBG_ConsultaSegmentos(SegmentoRequest reques)
        {
            SegmentoResponse response = new SegmentoResponse();
            Error error = new Error();

            try
            {
                response.listaSegmento = metodos.ListaSegmento(reques.MarcaImagen, ref error);
                response.codigoRetorno = error.CodigoError;
                response.mensajeRetorno = error.DescripcionError;
            }
            catch (Exception e)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = e.Message;
            }
            return response;
        }


        [WebMethod(Description = "MiMetaBG: Consulta Parametrización de Monto y Tiempo")]
        public ParametroResponse MiMetaBG_Monto_Tiempo(ParametroRequest request)
        {
            #region Declaracion de Variables
            CultureInfo ci;
            string lang = Convert.ToString(ConfigurationManager.AppSettings.Get("ConfiguracionRegional"));
            ci = CultureInfo.CreateSpecificCulture(lang);
            Thread.CurrentThread.CurrentCulture = ci;

            Error error = new Error();
            int id_Error = 0;
            MiMetaBG_AccesoDatos.Log.LogSave Logs = new MiMetaBG_AccesoDatos.Log.LogSave();
            MiMetaBG_AccesoDatos.Log.logmasterRequest LogsRequest = new MiMetaBG_AccesoDatos.Log.logmasterRequest();
            MiMetaBG_AccesoDatos.Log.logmasterResponse LogsResponse = new MiMetaBG_AccesoDatos.Log.logmasterResponse();
            ///LogMasterReq.parametroRequest.Aplicacion = "";


            ParametroResponse response = new ParametroResponse();
            Dictionary<string, string> Parametros = new Dictionary<string, string>();
            string apiUrl = string.Empty;
            string Tipo = string.Empty;
            string Codigo = string.Empty;
            string Canal = string.Empty;
            string MensajeSegmento = string.Empty;
            #endregion
            try
            {
                //Consulta al microservicio
                apiUrl = ConfigurationManager.AppSettings.Get("WsServicioParametros").ToString();

                using (var client = new WebClient())
                {

                    Tipo = ConfigurationManager.AppSettings.Get("Tipo").ToString();
                    Codigo = ConfigurationManager.AppSettings.Get("Meta").ToString();
                    Canal = ConfigurationManager.AppSettings.Get("Canal").ToString();
                    Parametros.Add("tipo", Tipo);
                    Parametros.Add("codigo", Codigo + Canal);

                    LogsRequest.parametroRequest = request;
                    id_Error = Logs.LOGSAVEREQ(LogsRequest, "01", "01", "WsServicioParametros", "0", "Consulta Parametros", Tipo + "|" + Codigo + Canal);

                    var json_serializer = new JavaScriptSerializer();
                    var parametros = metodos.ConsultarMicroServicio(apiUrl, Parametros, ref error);
                    var json_respuesta = (IDictionary<string, object>)json_serializer.DeserializeObject(parametros);

                    response.codigoRetorno = error.CodigoError;
                    response.mensajeRetorno = error.DescripcionError;

                    if (response.codigoRetorno == "0000")
                    {
                        response.tiempoIncial = Convert.ToInt32(parametros.Substring(133, 4)) / 30;
                        response.tiempoFinal = Convert.ToInt32(parametros.Substring(138, 4)) / 30;
                        response.montoInicial = Convert.ToInt32(parametros.Substring(150, 8));
                        response.montoFinal = Convert.ToInt32(parametros.Substring(161, 8));
                    }
                    LogsResponse.parametroResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "01", "01", response.codigoRetorno, response.mensajeRetorno, id_Error);
                }

                //Consulta el Emojid en BD
                if (response.codigoRetorno == "0000")
                {
                    response.imagSegmento = metodos.ListaImagenes(request.MarcaImagen, request.idSegmento, ref MensajeSegmento, ref error);
                    response.mensaje = MensajeSegmento;
                    response.codigoRetorno = error.CodigoError;
                    response.mensajeRetorno = error.DescripcionError;
                }
            }
            catch (Exception e)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = "Error Inesperado - " + error.DescripcionError + " - " + e.Message;

                LogsResponse.parametroResponse = response;
                Logs.LOGSAVERES(LogsResponse, "01", "01", response.codigoRetorno, response.mensajeRetorno, id_Error);
            }
            return response;
        }


        [WebMethod(Description = "MiMetaBG: Validacion de Cliente OTP")]
        public ValidacionClienteResponse MiMetaBG_ValidacionCliente(ValidacionClienteRequest request)
        {
            ValidacionClienteResponse response = new ValidacionClienteResponse();

            MiMetaBG_AccesoDatos.Log.LogSave Logs = new MiMetaBG_AccesoDatos.Log.LogSave();
            MiMetaBG_AccesoDatos.Log.logmasterRequest LogsRequest = new MiMetaBG_AccesoDatos.Log.logmasterRequest();
            MiMetaBG_AccesoDatos.Log.logmasterResponse LogsResponse = new MiMetaBG_AccesoDatos.Log.logmasterResponse();
            int idError = 0;
            string accion = string.Empty;
            Error e = new Error();
            string codigoError = string.Empty;
            string descripcion = string.Empty;
            string respuestaOTP = string.Empty;
            string codigoOTP = string.Empty;
            string respuestaSMS = string.Empty;
            Dictionary<string, string> Parametros = new Dictionary<string, string>();
            Dictionary<string, string> ParametrosCuentas = new Dictionary<string, string>();
            string apiUrlCliente = string.Empty;


            string nombreCliente = string.Empty;
            string correoCliente = string.Empty;
            string celularCliente = string.Empty;

            try
            {
                //Consulta clientes
                apiUrlCliente = ConfigurationManager.AppSettings.Get("WsServicioCliente").ToString();
                Parametros.Add("identificacion", request.identificacion);
                Parametros.Add("tipoIdentificacion", request.tipoIdentificacion);

                accion = "03";
                LogsRequest.validacionClienteRequest = request;
                idError = Logs.LOGSAVEREQ(LogsRequest, "03", "", "WsServicioCliente", request.identificacion, "ConsultarClientes", "");

                var parametros = metodos.ConsultarMicroServicio(apiUrlCliente, Parametros, ref e);
                response.codigoRetorno = e.CodigoError;
                response.mensajeRetorno = e.DescripcionError;

                if (response.codigoRetorno == "0000")
                {
                    dynamic objRespuesta = JsonConvert.DeserializeObject(parametros);
                    nombreCliente = objRespuesta.data.nombres;
                    nombreCliente = nombreCliente.Trim();
                    correoCliente = objRespuesta.data.correoElectronico;
                    correoCliente = correoCliente.Trim();
                    celularCliente = objRespuesta.data.telefonoCelular;


                    if (correoCliente == "")
                    {
                        e = Error.ConsultaError("0001", "", "vive_bg");
                        response.codigoRetorno = e.CodigoError;
                        response.mensajeRetorno = e.DescripcionError;
                    }
                    else
                    {
                        response.correoElectronico = metodos.EnmascararCorreo(correoCliente);
                        response.correoSinMascara = correoCliente;
                        response.fullName = nombreCliente.ToUpper();
                        response.nombreCliente = metodos.ClienteObtener(nombreCliente);
                    }
                }
                else
                {
                    e = Error.ConsultaError(e.CodigoError, e.DescripcionError, "wsParametro");
                    response.codigoRetorno = e.CodigoError;
                    response.mensajeRetorno = e.DescripcionError;
                }

                LogsResponse.validacionClienteResponse = response;
                Logs.LOGSAVERES(LogsResponse, "03", "", response.codigoRetorno, response.mensajeRetorno, idError, "");

                //Consultar si posee cuentas
                if (response.codigoRetorno == "0000")
                {
                    accion = "04";
                    LogsResponse.validacionClienteResponse = response;
                    idError = Logs.LOGSAVEREQ(LogsRequest, "04", "", "WsServicioCuentas", request.identificacion, "ConsultarCuentasActivas", "");

                    ParametrosCuentas.Add("identificacion", request.identificacion);
                    ParametrosCuentas.Add("tipoIdentificacion", request.tipoIdentificacion);

                    apiUrlCliente = ConfigurationManager.AppSettings.Get("WsServicioCuentas").ToString();
                    var respuestaCuentas = metodos.ConsultarMicroServicio(apiUrlCliente, ParametrosCuentas, ref e);

                    bool cuenta = false;
                    if (response.codigoRetorno == "0000")
                    {
                        dynamic objRespuesta = JsonConvert.DeserializeObject(respuestaCuentas);

                        foreach (var recorrer in objRespuesta.data)
                        {
                            if (recorrer.estado == "0" && recorrer.tipo == "A") //VALIDACION SE DE AHORROP
                                cuenta = true;
                        }
                        if (cuenta)
                        {
                            response.codigoRetorno = "0000";
                            response.mensajeRetorno = "Ok-Exitoso";
                        }
                        else
                        {
                            e = Error.ConsultaError("0015", e.DescripcionError, "wsParametro");
                            response.codigoRetorno = e.CodigoError;
                            response.mensajeRetorno = e.DescripcionError;
                        }
                    }
                    LogsResponse.validacionClienteResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "04", "", response.codigoRetorno, response.mensajeRetorno, idError, "");
                }

                if (response.codigoRetorno == "0000")
                {
                    string aplicacion = ConfigurationManager.AppSettings.Get("Aplicacion").ToString();
                    string servicio = ConfigurationManager.AppSettings.Get("Servicio").ToString();

                    accion = "05";
                    LogsResponse.validacionClienteResponse = response;

                    idError = Logs.LOGSAVEREQ(LogsRequest, "05", "", "WsEntrust_BG", request.identificacion, "OTPgeneration", aplicacion + "--" + servicio);
                    using (WsEntrust_BG.FuncionesExt wsGeneracionOTP = new WsEntrust_BG.FuncionesExt())
                    {
                        wsGeneracionOTP.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Timeout"));
                        respuestaOTP = wsGeneracionOTP.OTPgeneration(aplicacion, servicio, request.identificacion, ref codigoError, ref descripcion, "BVI", "WEB", "01H1");

                        string _llave = ConfigurationManager.AppSettings.Get("key").ToString();
                        string _iv = ConfigurationManager.AppSettings.Get("iv").ToString();

                        response.codigoRetorno = codigoError;
                        response.mensajeRetorno = descripcion;

                        if (response.codigoRetorno == "0000")
                        {
                            codigoOTP = metodos.Decrypt_AES(respuestaOTP, _llave, _iv);
                        }
                        else
                        {
                            e = Error.ConsultaError(codigoError, descripcion, "wsEntrust_Ge");
                            response.codigoRetorno = e.CodigoError;
                            response.mensajeRetorno = e.DescripcionError;
                        }
                    }
                    LogsResponse.validacionClienteResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "05", "", response.codigoRetorno, response.mensajeRetorno, idError, codigoOTP);
                }

                if (response.codigoRetorno == "0000")
                {
                    string codigoAviso = ConfigurationManager.AppSettings.Get("codigoAviso").ToString();
                    string categoriaAviso = ConfigurationManager.AppSettings.Get("categoriaAviso").ToString();
                    string canalAviso = ConfigurationManager.AppSettings.Get("canalAviso").ToString();
                    string construirMail = ConfigurationManager.AppSettings.Get("construirMail").ToString();
                    string construirSSM = ConfigurationManager.AppSettings.Get("construirSSM").ToString();
                    string fechaEmision = DateTime.Today.ToString("dd-MM-yyyy");
                    string HoraActual = DateTime.Now.ToString("hh:mm:ss");

                    accion = "06";
                    LogsResponse.validacionClienteResponse = response;
                    idError = Logs.LOGSAVEREQ(LogsRequest, "06", "", "WsAvisoSeguro_BG", request.identificacion, "EnviaMensajeOTP", codigoAviso + "-" + fechaEmision + "-" + HoraActual);

                    using (WsAvisoSeguro_BG.NotificacionesService wsAvisoSeguro = new WsAvisoSeguro_BG.NotificacionesService())
                    {
                        wsAvisoSeguro.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Timeout"));

                        respuestaSMS = wsAvisoSeguro.EnviaMensaje(categoriaAviso, canalAviso, construirMail, "", construirSSM, "", "", "", codigoAviso, "",
                                                                  correoCliente, celularCliente, nombreCliente, "", "", "", "", "", "", "", "", request.ip,
                                                                  codigoOTP, "", fechaEmision, HoraActual, "1", "C");
                        string[] respuesta = new string[0];
                        respuesta = respuestaSMS.Split('|');

                        response.codigoRetorno = respuesta[0].ToString();
                        response.mensajeRetorno = respuesta[1].ToString();
                    }

                    LogsResponse.validacionClienteResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "06", "", response.codigoRetorno, response.mensajeRetorno, idError, respuestaSMS);
                }
            }
            catch (Exception ex)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = "Eror inesperado -- " + descripcion + "--" + ex.Message;

                LogsResponse.validacionClienteResponse = response;
                Logs.LOGSAVERES(LogsResponse, accion, "", response.codigoRetorno + "--" + codigoError, response.mensajeRetorno, idError, "");
            }
            return response;
        }


        [WebMethod(Description = "MiMetaBG: Validacion de OTP")]
        public ValidacionClienteOTPResponse MiMetaBG_ValidacionOTP(ValidacionClienteOTPRequest request)
        {
            ValidacionClienteOTPResponse response = new ValidacionClienteOTPResponse();

            MiMetaBG_AccesoDatos.Log.LogSave Logs = new MiMetaBG_AccesoDatos.Log.LogSave();
            MiMetaBG_AccesoDatos.Log.logmasterRequest LogsRequest = new MiMetaBG_AccesoDatos.Log.logmasterRequest();
            MiMetaBG_AccesoDatos.Log.logmasterResponse LogsResponse = new MiMetaBG_AccesoDatos.Log.logmasterResponse();

            string accion = string.Empty;
            int idError = 0;

            string codigoretorno = string.Empty;
            string mensajeError = string.Empty;
            Error error = new Error();

            try
            {
                LogsRequest.validacionOTPRequest = request;
                accion = "07";
                idError = Logs.LOGSAVEREQ(LogsRequest, "07", "", "WsEntrust_BG", request.identificacion, "OTPvalidate", request.codigoOTP);

                using (WsEntrust_BG.FuncionesExt wsValidacionOTP = new WsEntrust_BG.FuncionesExt())
                {
                    wsValidacionOTP.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Timeout"));

                    string _llave = ConfigurationManager.AppSettings.Get("key").ToString();
                    string _iv = ConfigurationManager.AppSettings.Get("iv").ToString();
                    string claveEncriptada = System.Convert.ToBase64String(metodos.Encrypt_AES(request.codigoOTP, _llave, _iv));
                    string opid = ConfigurationManager.AppSettings.Get("Opid").ToString();
                    string aplicacion = ConfigurationManager.AppSettings.Get("Aplicacion").ToString();
                    string servicio = ConfigurationManager.AppSettings.Get("Servicio").ToString();
                    wsValidacionOTP.OTPvalidate(aplicacion, servicio, request.identificacion, claveEncriptada, ref codigoretorno, ref mensajeError, "BVI", opid, "01H1");
                    response.codigoRetorno = codigoretorno;
                    response.mensajeRetorno = mensajeError;

                    LogsResponse.validacionOTPResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "07", "", response.codigoRetorno, response.mensajeRetorno, idError, "");
                }

                if (response.codigoRetorno == "0000")
                {
                    accion = "08";
                    idError = Logs.LOGSAVEREQ(LogsRequest, "08", "", "generarToken", request.identificacion, "tokenValidacion", request.codigoOTP);

                    response.tokenValidacion = metodos.GenerarToken(request.identificacion, request.codigoOTP, request.tipoIdentificacion, ref error);
                    response.codigoRetorno = error.CodigoError;
                    response.mensajeRetorno = error.DescripcionError;

                    LogsResponse.validacionOTPResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "08", "", response.codigoRetorno, response.mensajeRetorno, idError, "");
                }
                else
                {
                    error = Error.ConsultaError(codigoretorno, mensajeError, "wsEntrust_Va");
                    response.codigoRetorno = error.CodigoError;
                    response.mensajeRetorno = error.DescripcionError;
                }
            }
            catch (Exception e)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = "Error Inesperado - " + e.Message;

                LogsResponse.validacionOTPResponse = response;
                Logs.LOGSAVERES(LogsResponse, accion, "", response.codigoRetorno, response.mensajeRetorno, idError, "");


                idError = Logs.LOGSAVEREQ(LogsRequest, accion, "", "Exception", request.identificacion, "Exception", request.codigoOTP, accion);
                Logs.LOGSAVERES(LogsResponse, accion, "", response.codigoRetorno, response.mensajeRetorno, idError, "");

            }
            return response;
        }


        [WebMethod(Description = "MiMetaBG: Consulta datos cuentas")]
        public CuentasResponse MiMetaBG_ListaCuentas(CuentasRequest request)
        {
            MiMetaBG_AccesoDatos.Log.LogSave Logs = new MiMetaBG_AccesoDatos.Log.LogSave();
            MiMetaBG_AccesoDatos.Log.logmasterRequest LogsRequest = new MiMetaBG_AccesoDatos.Log.logmasterRequest();
            MiMetaBG_AccesoDatos.Log.logmasterResponse LogsResponse = new MiMetaBG_AccesoDatos.Log.logmasterResponse();
            string accion = string.Empty;

            Token token = new Token();

            int idError = 0;

            CuentasResponse response = new CuentasResponse();
            Error e = new Error();
            Dictionary<string, string> ParametrosCuentas = new Dictionary<string, string>();
            string apiUrlCliente = string.Empty;
            byte[] Imagenes = null;
            int idSegmento = 0;

            try
            {
                //Validar Token
                if (request.tokenValidacion != "")
                {
                    //Agregar LOGS
                    token = metodos.ValidaToken(request.tokenValidacion);
                    response.codigoRetorno = token.codigoRetorno;
                    response.mensajeRetorno = token.mensajeRetorno;
                }
                else
                {
                    e = Error.ConsultaError("0100", "", "vive_bg");
                    response.codigoRetorno = e.CodigoError;
                    response.mensajeRetorno = e.DescripcionError;
                }

                if (response.codigoRetorno == "0000")
                {
                    // Consultas cuentas
                    LogsRequest.cuentasRequest = request;

                    accion = "09";
                    idError = Logs.LOGSAVEREQ(LogsRequest, "09", "", "WsServicioCuentas", token.identificacion, "OTPvalidate", "", token.identificacion, token.tipoIdentificacion);

                    ParametrosCuentas.Add("identificacion", token.identificacion);
                    ParametrosCuentas.Add("tipoIdentificacion", token.tipoIdentificacion);

                    apiUrlCliente = ConfigurationManager.AppSettings.Get("WsServicioCuentas").ToString();
                    var respuestaCuentas = metodos.ConsultarMicroServicio(apiUrlCliente, ParametrosCuentas, ref e);

                    response.codigoRetorno = e.CodigoError;
                    response.mensajeRetorno = e.DescripcionError;

                    if (response.codigoRetorno == "0000")
                    {
                        response = metodos.ConsultarCuentas(respuestaCuentas);
                    }

                    LogsResponse.cuentasResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "09", "", response.codigoRetorno, response.mensajeRetorno, idError, "");
                }


                if (response.codigoRetorno == "0000")
                {
                    LogsRequest.cuentasRequest = request;
                    accion = "10";
                    idError = Logs.LOGSAVEREQ(LogsRequest, "10", "", "BDWebSites", token.identificacion, "ListaEmojin", "", token.identificacion, token.tipoIdentificacion);

                    metodos.ListaEmojin(request.idSegmento, ref idSegmento, ref Imagenes, ref e);
                    response.idSegmento = idSegmento;
                    response.imagenes = Imagenes;
                    response.codigoRetorno = e.CodigoError;
                    response.mensajeRetorno = e.DescripcionError;

                    LogsResponse.cuentasResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "10", "", response.codigoRetorno, response.mensajeRetorno, idError, "");
                }

            }
            catch (Exception ex)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = "Error Inesperado - " + ex.Message + " -- " + e.DescripcionError;

                LogsResponse.cuentasResponse = response;
                Logs.LOGSAVERES(LogsResponse, accion, "", response.codigoRetorno, response.mensajeRetorno, idError, "");

                idError = Logs.LOGSAVEREQ(LogsRequest, accion, "", "Exception", token.identificacion, "Exception",  accion);
                Logs.LOGSAVERES(LogsResponse, accion, "", response.codigoRetorno, response.mensajeRetorno, idError, "");
            }
            return response;
        }


        #region Mantenimiento de MiMetaBG
        [WebMethod(Description = "MiMetaBG: Crear, editar, cancelar Metas")]
        public MetaResponse MiMetaBG_Creacion(MetaRequest request)
        {
            MiMetaBG_AccesoDatos.Log.LogSave Logs = new MiMetaBG_AccesoDatos.Log.LogSave();
            MiMetaBG_AccesoDatos.Log.logmasterRequest LogsRequest = new MiMetaBG_AccesoDatos.Log.logmasterRequest();
            MiMetaBG_AccesoDatos.Log.logmasterResponse LogsResponse = new MiMetaBG_AccesoDatos.Log.logmasterResponse();
            string accion = string.Empty;
            int idError = 0;

            string identificacion = string.Empty;
            string tipoIdentificacion = string.Empty;
            string DescripcionError = string.Empty;
            string CodigoError = string.Empty;

            Token token = new Token();
            MetaResponse response = new MetaResponse();
            dsAhorroMeta dsAhorroMetaOUT = null;
            Error error = new Error();
            string codTransaccion = ConfigurationManager.AppSettings.Get("CodTransaccion").ToString();
            string codAplicacion = ConfigurationManager.AppSettings.Get("CodAplicacion").ToString();
            string codigoCanal = ConfigurationManager.AppSettings.Get("CodigoCanal").ToString();

            /********PARA ENVIO DE EMAIL CON CONTRATO********/
            string respuestaEmail = string.Empty;
            string MailDe = string.Empty;
            string MailPara = string.Empty;
            string MailCopia = string.Empty;
            string MailAsunto = string.Empty;
            string MailMensaje = string.Empty;
            string MailNombreAdjunto = string.Empty;
            string MailPathFile = string.Empty;
            byte[] FileByte;
            string SecuencialCash = string.Empty;
            MetaRequest meta = new MetaRequest(); 

            try
            {
                if (request.tokenValidacion != "")
                {
                    token = metodos.ValidaToken(request.tokenValidacion);
                    response.codigoRetorno = token.codigoRetorno;
                    response.mensajeRetorno = token.mensajeRetorno;
                }
                else
                {
                    error = Error.ConsultaError("0100", "", "vive_bg");
                    response.codigoRetorno = error.CodigoError;
                    response.mensajeRetorno = error.DescripcionError;
                }

                if (response.codigoRetorno == "0000")
                {
                    LogsRequest.metaRequest = request;
                    accion = "11";
                    idError = Logs.LOGSAVEREQ(LogsRequest, "11", "", "WsCashBV_BG", token.identificacion, "Crear, Update, Delete Sueño", codTransaccion + "-" + codAplicacion + "--" + codigoCanal, token.identificacion, token.tipoIdentificacion);

                    using (WsCashBV_BG.WebServiceBG ws = new WsCashBV_BG.WebServiceBG())
                    {
                        ws.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Timeout"));

                        SecuencialCash = meta.ObtenerSecuencialCASH(1, ref error);  //metodo

                        dsAhorroMetaOUT = new dsAhorroMeta(); //
                        dsAhorroMeta dsAhorroMetaIN = new dsAhorroMeta();
                        dsAhorroMeta.TB_AhorroMetaINRow rowIn =
                            (dsAhorroMeta.TB_AhorroMetaINRow)dsAhorroMetaIN.TB_AhorroMetaIN.NewTB_AhorroMetaINRow();

                        rowIn.CodTransaccion = codTransaccion;
                        rowIn.CodAplicacion = codAplicacion;
                        rowIn.NumeroRegistro = SecuencialCash.ToString();
                        rowIn.DireccionIP = request.direccionIP;
                        rowIn.MacAdress = request.macAdress;
                        rowIn.CodigoCanal = codigoCanal;
                        rowIn.Accion = request.accion;
                        rowIn.CuentaMultiple = Convert.ToString(request.cuentaMultiple);
                        rowIn.NumeroSolicitud = Convert.ToString(request.numeroSolicitud);
                        rowIn.TipoCtaDebito = request.tipoCtaDebito;
                        rowIn.CuentaDebito = request.cuentaDebito;
                        rowIn.MontoDebito = request.montoDebito;
                        rowIn.Frencuencia = request.frencuencia;
                        rowIn.DiaUnoDebito = request.diaUnoDebito;
                        rowIn.DiaDosDebito = request.diaDosDebito;
                        rowIn.MontoMeta = request.montoMeta;
                        rowIn.FechaVencimiento = request.fechaVencimiento;

                        dsAhorroMetaIN.TB_AhorroMetaIN.AddTB_AhorroMetaINRow(rowIn);
                        dsAhorroMetaOUT = ws.TrxSolicitudAhorroMeta(dsAhorroMetaIN);

                        dsAhorroMeta.TB_AhorroMetaOUTRow rowOut =
                            dsAhorroMetaOUT.TB_AhorroMetaOUT[0];

                        response.codigoRetorno = rowOut.CodRetorno;
                        response.mensajeRetorno = rowOut.MsjRetorno;

                        if (request.accion == "A" && response.codigoRetorno == "0000")
                        {
                            request.numeroSolicitud = Convert.ToInt32(rowOut.NumeroSolicitud.ToString());
                            response.numeroSolicitud = Convert.ToInt32(rowOut.NumeroSolicitud.ToString());
                        }

                        LogsResponse.metaResponse = response;
                        Logs.LOGSAVERES(LogsResponse, "11", "", response.codigoRetorno, response.mensajeRetorno, idError, rowOut.NumeroSolicitud.ToString());
                    }
                }

                if (response.codigoRetorno == "0000" && request.accion == "A")
                {
                    LogsRequest.metaRequest = request;
                    accion = "12";
                    idError = Logs.LOGSAVEREQ(LogsRequest, "12", "", "BDWebSites", token.identificacion, "RegistrarMetas", request.accion, token.identificacion, token.tipoIdentificacion);

                    response.numeroSolicitud = request.numeroSolicitud;
                    metodos.RegistrarMetas(token.identificacion, request.cuentaMultiple, request.nombreMeta, request.idSegmento, response.numeroSolicitud, request.fechaCreacion, ref error);
                    response.codigoRetorno = error.CodigoError;
                    response.mensajeRetorno = error.DescripcionError;

                    LogsResponse.metaResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "12", "", response.codigoRetorno, response.mensajeRetorno, idError, "");
                }
                /**********************Envio de contrato via email************************/
                if (response.codigoRetorno == "0000" && request.accion == "A")
                {
                    LogsRequest.metaRequest = request;
                    accion = "17";
                    idError = Logs.LOGSAVEREQ(LogsRequest, "17", "", "BG.CashBVBG.WS", token.identificacion, "Envio Mail Creacion Metas", request.accion, token.identificacion, token.tipoIdentificacion);

                    MailDe = ConfigurationManager.AppSettings.Get("contratoMailFrom");                           
                    MailPara = request.correoSinMascara;
                    MailAsunto = ConfigurationManager.AppSettings.Get("contratoMailSubject");                    
                    MailMensaje = metodos.EmailBody(request.fullName, request.cuentaDebito, request.nombreMeta, request.montoFinalMeta, request.montoDebito, request.fechaVencimiento, request.fechaCreacion);
                    MailNombreAdjunto = ConfigurationManager.AppSettings.Get("contratoMailFileName");           
                    MailPathFile = Server.MapPath(ConfigurationManager.AppSettings.Get("contratoMailPathFile")); 

                    FileByte = System.IO.File.ReadAllBytes(MailPathFile + MailNombreAdjunto);

                    using (WsCashBVBGMail_BG.WebServiceMSG ws = new WsCashBVBGMail_BG.WebServiceMSG())
                    {
                        ws.Url = ConfigurationManager.AppSettings.Get("contratoMailUrlWS");
                        respuestaEmail = ws.EnviarMail(MailDe, MailPara, MailCopia, MailAsunto, MailMensaje, MailNombreAdjunto, FileByte);

                        response.codigoRetorno = respuestaEmail;
                        response.mensajeRetorno = "Envío OK, del correo de contrato/convenio.";
                        if (respuestaEmail != "0000")
                        {
                            response.mensajeRetorno = "Error en el envío del correo de contrato/convenio.";
                        }
                    }
                    LogsResponse.metaResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "17", "", response.codigoRetorno, response.mensajeRetorno, idError, "");
                }
            }
            catch (Exception e)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = "Error Inesperado - " + e.Message;

                LogsResponse.metaResponse = response;
                Logs.LOGSAVERES(LogsResponse, accion, "", response.codigoRetorno, response.mensajeRetorno, idError, "");

                idError = Logs.LOGSAVEREQ(LogsRequest, accion, "", "Exception", token.identificacion, "Exception", request.accion, token.identificacion, accion);
                Logs.LOGSAVERES(LogsResponse, accion, "", response.codigoRetorno, response.mensajeRetorno, idError, "");
            }
            return response;
        }


        [WebMethod(Description = "MiMetaBG: Consulta Masiva Sueño")]
        public ConsultaMetasResponse MiMetaBG_ConsultaMasiva(ConsultaMetasRequest request)
        {
            ConsultaMetasResponse response = new ConsultaMetasResponse();

            MiMetaBG_AccesoDatos.Log.LogSave Logs = new MiMetaBG_AccesoDatos.Log.LogSave();
            MiMetaBG_AccesoDatos.Log.logmasterRequest LogsRequest = new MiMetaBG_AccesoDatos.Log.logmasterRequest();
            MiMetaBG_AccesoDatos.Log.logmasterResponse LogsResponse = new MiMetaBG_AccesoDatos.Log.logmasterResponse();
            string accion = string.Empty;
            string Tipo = string.Empty;
            string Codigo = string.Empty;
            string Canal = string.Empty;
            int idError = 0;

            Token token = new Token();
            string apiUrl = string.Empty;

            Dictionary<string, string> Parametros = new Dictionary<string, string>();
            Error e = new Error();
            Dictionary<string, string> ParametrosCuentas = new Dictionary<string, string>();
            string apiUrlCliente = string.Empty;

            try
            {
                //Validar Token
                if (request.tokenValidacion != "")
                {
                    token = metodos.ValidaToken(request.tokenValidacion);
                    response.codigoRetorno = token.codigoRetorno;
                    response.mensajeRetorno = token.mensajeRetorno;
                }
                else
                {
                    e = Error.ConsultaError("0100", "", "vive_bg");
                    response.codigoRetorno = e.CodigoError;
                    response.mensajeRetorno = e.DescripcionError;
                }

                
                if (response.codigoRetorno == "0000")
                {
                    LogsRequest.consultasMetas = request;
                    idError = Logs.LOGSAVEREQ(LogsRequest, "13", "", "WsServicioMetasCreadas", token.identificacion, "ConsultarMetas", "ConsultaMasiva", token.identificacion, token.tipoIdentificacion);

                    ParametrosCuentas.Add("identificacion", token.identificacion);
                    ParametrosCuentas.Add("tipoIdentificacion", token.tipoIdentificacion);

                    apiUrlCliente = ConfigurationManager.AppSettings.Get("WsServicioMetasCreadas").ToString();
                    var respuestaConsultaMasiva = metodos.ConsultarMicroServicio(apiUrlCliente, ParametrosCuentas, ref e);

                    response.codigoRetorno = e.CodigoError;
                    response.mensajeRetorno = e.DescripcionError;

                    if (response.codigoRetorno == "0000")
                    {
                        response.listasMetasCreadas = metodos.ListaMasivaMetas(respuestaConsultaMasiva, token.identificacion, ref e);
                        response.codigoRetorno = e.CodigoError;
                        response.mensajeRetorno = e.DescripcionError;
                    }

                    LogsResponse.consultaMetasResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "13", "", response.codigoRetorno, response.mensajeRetorno, idError, "");
                }

                
                if (response.codigoRetorno == "0000")
                {
                    accion = "14";
                    LogsRequest.consultasMetas = request;
                    idError = Logs.LOGSAVEREQ(LogsRequest, "14", "", "WsServicioCuentas", token.identificacion, "ConsultasTodasCuentas", "ConsultasTodasCuentas", token.identificacion, token.tipoIdentificacion);

                    apiUrlCliente = ConfigurationManager.AppSettings.Get("WsServicioCuentas").ToString();
                    var respuestaCuentas = metodos.ConsultarMicroServicio(apiUrlCliente, ParametrosCuentas, ref e);

                    response.codigoRetorno = e.CodigoError;
                    response.mensajeRetorno = e.DescripcionError;

                    if (response.codigoRetorno == "0000")
                    {
                        response.ListaTodas = metodos.ConsultarCuentasMetas(respuestaCuentas, ref e);
                        response.codigoRetorno = e.CodigoError;
                        response.mensajeRetorno = e.DescripcionError;
                    }

                    LogsResponse.consultaMetasResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "14", "", response.codigoRetorno, response.mensajeRetorno, idError, "");
                }


                if (response.codigoRetorno == "0000")
                {
                    accion = "15";
                    apiUrl = ConfigurationManager.AppSettings.Get("WsServicioParametros").ToString();
                    Tipo = ConfigurationManager.AppSettings.Get("Tipo").ToString();
                    Codigo = ConfigurationManager.AppSettings.Get("Meta").ToString();
                    Canal = ConfigurationManager.AppSettings.Get("Canal").ToString();
                    Parametros.Add("tipo", Tipo);
                    Parametros.Add("codigo", Codigo + Canal);

                    LogsRequest.consultasMetas = request;
                    idError = Logs.LOGSAVEREQ(LogsRequest, "15", "01", "WsServicioParametros", token.identificacion, "Consulta Parametros", Tipo + "|" + Codigo + Canal);

                    var json_serializer = new JavaScriptSerializer();
                    var parametros = metodos.ConsultarMicroServicio(apiUrl, Parametros, ref e);
                    var json_respuesta = (IDictionary<string, object>)json_serializer.DeserializeObject(parametros);

                    response.codigoRetorno = e.CodigoError;
                    response.mensajeRetorno = e.DescripcionError;

                    if (response.codigoRetorno == "0000")
                    {
                        response.tiempoIncial = Convert.ToInt32(parametros.Substring(133, 4)) / 30;
                        response.tiempoFinal = Convert.ToInt32(parametros.Substring(138, 4)) / 360;
                        response.montoInicial = Convert.ToInt32(parametros.Substring(150, 8));
                        response.montoFinal = Convert.ToInt32(parametros.Substring(161, 8));
                    }

                    LogsResponse.consultaMetasResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "15", "01", response.codigoRetorno, response.mensajeRetorno, idError);
                }

                if (response.codigoRetorno == "0000")
                {
                    accion = "";
                    int CantidadMostradas = int.Parse(ConfigurationManager.AppSettings.Get("cantidadMostrada").ToString());

                    response.ListaVentasCruzadas = metodos.ConsultarVentasCruzadas(token.identificacion, CantidadMostradas, ref e);
                    response.codigoRetorno = e.CodigoError;
                    response.mensajeRetorno = e.DescripcionError;
                }
            }
            catch (Exception ex)
            {

                response.codigoRetorno = "9999";
                response.mensajeRetorno = "Error Inesperado - " + e.DescripcionError + " -- " + ex.Message;

                LogsResponse.consultaMetasResponse = response;
                Logs.LOGSAVERES(LogsResponse, accion, "", response.codigoRetorno, response.mensajeRetorno, idError, "");


                idError = Logs.LOGSAVEREQ(LogsRequest, accion, "", "Exception", token.identificacion, "Exception",  accion);
                Logs.LOGSAVERES(LogsResponse, accion, "", response.codigoRetorno, response.mensajeRetorno, idError, "");

            }
            return response;
        }


        [WebMethod(Description = "MiMetaBG: Valor Extraordinario")]
        public MetaAdicionalResponse MiMetaBG_ValorAdicional(MetaAdicionalRequest request)
        {
            MetaAdicionalResponse response = new MetaAdicionalResponse();
            dsDebitoCredito dsDebitoCreditoOUT = null;
            Token token = new Token();
            Error error = new Error();

            MiMetaBG_AccesoDatos.Log.LogSave Logs = new MiMetaBG_AccesoDatos.Log.LogSave();
            MiMetaBG_AccesoDatos.Log.logmasterRequest LogsRequest = new MiMetaBG_AccesoDatos.Log.logmasterRequest();
            MiMetaBG_AccesoDatos.Log.logmasterResponse LogsResponse = new MiMetaBG_AccesoDatos.Log.logmasterResponse();

            string codTransaccion = string.Empty;
            string canal = string.Empty;
            string procesos = string.Empty;
            string codAplicacion = string.Empty;
            string cod_Banco_Beneficiario = string.Empty;
            string cod_Empresa = string.Empty;
            string tipo_transaccion = string.Empty;
            string tipoEmpresa = string.Empty;
            string tipoCobro = string.Empty;
            string opid = string.Empty;
            string MotivoComision = string.Empty;
            string terminal = string.Empty;
            string procesoCash = string.Empty;
            string idProceso = string.Empty;
            string codServicio = string.Empty;
            string SecuencialCash = string.Empty;
            string accion = string.Empty;
            int idError = 0;

            MetaRequest meta = new MetaRequest();
            try
            {
               
                if (request.tokenValidacion != "")
                {
                    //Agregar LOGS
                    token = metodos.ValidaToken(request.tokenValidacion);
                    response.codigoRetorno = token.codigoRetorno;
                    response.mensajeRetorno = token.mensajeRetorno;
                }
                else
                {
                    error = Error.ConsultaError("0100", "", "vive_bg");
                    response.codigoRetorno = error.CodigoError;
                    response.mensajeRetorno = error.DescripcionError;
                }

                if (response.codigoRetorno == "0000")
                {
                    codTransaccion = ConfigurationManager.AppSettings.Get("Transaccion").ToString();
                    canal = ConfigurationManager.AppSettings.Get("CanalCash").ToString();
                    procesos = ConfigurationManager.AppSettings.Get("Proceso").ToString();
                    codAplicacion = ConfigurationManager.AppSettings.Get("CodAplicacionCash").ToString();
                    cod_Banco_Beneficiario = ConfigurationManager.AppSettings.Get("Cod_Banco_Beneficiario").ToString();
                    cod_Empresa = ConfigurationManager.AppSettings.Get("Cod_Empresa").ToString();
                    tipo_transaccion = ConfigurationManager.AppSettings.Get("Tipo_transaccion").ToString();
                    tipoEmpresa = ConfigurationManager.AppSettings.Get("TipoEmpresa").ToString();
                    tipoCobro = ConfigurationManager.AppSettings.Get("TipoCobro").ToString();
                    opid = ConfigurationManager.AppSettings.Get("Opid").ToString();
                    terminal = ConfigurationManager.AppSettings.Get("Motivo_comision").ToString();
                    procesoCash = ConfigurationManager.AppSettings.Get("ProcesoCash").ToString();
                    idProceso = ConfigurationManager.AppSettings.Get("IdProceso").ToString();
                    codServicio = ConfigurationManager.AppSettings.Get("CodServicio");

                    
                    LogsRequest.AdicionalRequest = request;
                    idError = Logs.LOGSAVEREQ(LogsRequest, "18", "01", "WsCashBV_BG", token.identificacion, "Valor Extra", 
                        codTransaccion + "|" + canal + "|" + procesos + "|" +  codAplicacion + "|" + cod_Banco_Beneficiario + "|" +
                        cod_Empresa + "|" + tipo_transaccion + "|" + tipoEmpresa + "|" + tipoCobro + "|" + opid + "|" + terminal + "|" +
                        procesoCash + "|" + idProceso + "|" + codServicio);

                    using (WsCashBV_BG.WebServiceBG ws = new WsCashBV_BG.WebServiceBG())
                    {

                        SecuencialCash = meta.ObtenerSecuencialCASH(2, ref error);  //metodo

                        ws.Timeout = Convert.ToInt32(ConfigurationManager.AppSettings.Get("Timeout"));
                        
                        dsDebitoCreditoOUT = new dsDebitoCredito(); //
                        dsDebitoCredito dsDebitoCreditoIN = new dsDebitoCredito();
                        dsDebitoCredito.TB_DebitoCreditoINRow rowIn =
                            (dsDebitoCredito.TB_DebitoCreditoINRow)dsDebitoCreditoIN.TB_DebitoCreditoIN.NewTB_DebitoCreditoINRow();

                        rowIn.Transaccion = codTransaccion;
                        rowIn.CuentaDebito = metodos.FormatearCuentas(request.ctaD);
                        rowIn.CuentaCredito = metodos.FormatearCuentas(request.ctaC);
                        rowIn.ValorDebito = request.valorD + "00";
                        rowIn.DireccionIP = request.ip;
                        rowIn.Canal = canal;
                        rowIn.MacAddress = request.macAddress;
                        rowIn.Proceso = procesos;
                        rowIn.CodAplicacion = codAplicacion;
                        rowIn.TipoTarjetaCredito = request.tipoTarC;
                        rowIn.TipoTarjetaDebito = request.tipoTarD;
                        rowIn.Identificacion = token.identificacion;
                        rowIn.Cod_Banco_Beneficiario = cod_Banco_Beneficiario;
                        rowIn.Cod_Empresa = cod_Empresa;
                        rowIn.SecuencialHost = "0";
                        rowIn.SecuencialCash = SecuencialCash.ToString();
                        rowIn.Tipo_transaccion = tipo_transaccion;
                        rowIn.TipoEmpresa = tipoEmpresa;
                        rowIn.CtaEmpresa = metodos.FormatearCuentas(request.ctaD);
                        rowIn.TipoCobro = tipoCobro;
                        rowIn.NumeroPlazo_Solicitud = request.numeroSolicitud;
                        rowIn.Opid = opid;
                        rowIn.Valor_comision = "000";
                        rowIn.Terminal = terminal;
                        rowIn.Valor_descuento = "0";
                        rowIn.Num_cajas = "0";
                        rowIn.Num_inscripcion = "0";
                        rowIn.SemanaPago2 = "0";
                        rowIn.SemanaEmbarque1 = "0";
                        rowIn.SemanaEmbarque2 = "0";
                        rowIn.ProcesoCash = procesoCash;
                        rowIn.CodServicio = codServicio;
                        rowIn.IdProceso = idProceso;
                        rowIn.Ref3 = request.referencia + "-Num.Solicitud:" + request.numeroSolicitud;
                        rowIn.IdTipoBeneficiario = request.IdTipoBeneficiario;

                        dsDebitoCreditoIN.TB_DebitoCreditoIN.AddTB_DebitoCreditoINRow(rowIn);
                        dsDebitoCreditoOUT = ws.EjecutarDebitoCredito(dsDebitoCreditoIN);

                        dsDebitoCredito.TB_DebitoCreditoOUTRow rowOut =
                            dsDebitoCreditoOUT.TB_DebitoCreditoOUT[0];

                        response.codigoRetorno = rowOut.Cod_Retorno;

                        error = Error.ConsultaError(response.codigoRetorno, "", "WsDebitoCredito");
                        response.codigoRetorno = error.CodigoError;
                        response.mensajeRetorno = error.DescripcionError;


                    }
                    LogsResponse.AdicionalResponse = response;
                    Logs.LOGSAVERES(LogsResponse, "18", "", response.codigoRetorno, response.mensajeRetorno, idError, "");

                }

            }
            catch (Exception e)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = "Error Inesperado - " + e.Message;

                LogsResponse.AdicionalResponse = response;
                Logs.LOGSAVERES(LogsResponse, "", "18", response.codigoRetorno, response.mensajeRetorno, idError, "");

                idError = Logs.LOGSAVEREQ(LogsRequest, accion, "", "Exception", token.identificacion, "Exception", accion);
                Logs.LOGSAVERES(LogsResponse, accion, "", response.codigoRetorno, response.mensajeRetorno, idError, "");
            }
            return response;
        }
        #endregion Mantenimiento de MiMetaBG

        
        [WebMethod(Description = "MiMetaBG: Validación de Token")]
        public TokenResponse MiMetaBG_ValidarToken(TokenRequest request)
        {
            TokenResponse response = new TokenResponse();
            Token token = new Token();
            Error e = new Error();
            //Validar Token
            if (request.tokenValidacion != "")
            {
                //Agregar LOGS
                token = metodos.ValidaToken(request.tokenValidacion);
                response.codigoRetorno = token.codigoRetorno;
                response.mensajeRetorno = token.mensajeRetorno;
            }
            else
            {
                e = Error.ConsultaError("0100", "", "vive_bg");
                response.codigoRetorno = e.CodigoError;
                response.mensajeRetorno = e.DescripcionError;
            }
            return response;
        }


        [WebMethod(Description = "MiMetaBG: Actualizar Ventas Cruzada")]
        public VentaCruzadaResponse MiMetaBG_VentasCruzadas(VentasCruzadasRequest request) 
        {
            Token token = new Token();
            string Descripcion = string.Empty;
            Error e = new Error();
            VentaCruzadaResponse response = new VentaCruzadaResponse();

            if (request.tokenValidacion != "")
            {
                token = metodos.ValidaToken(request.tokenValidacion);
                response.codigoRetorno = token.codigoRetorno;
                response.mensajeRetorno = token.mensajeRetorno;
            }
            else
            {
                e = Error.ConsultaError("0100", "", "vive_bg");
                response.codigoRetorno = e.CodigoError;
                response.mensajeRetorno = e.DescripcionError;
            }

            if (response.codigoRetorno == "0000")
            {
                response.codigoRetorno = metodos.ContadorVentasCruzadas(token.identificacion, request.cantidadVecesMostradas, ref Descripcion);
                response.mensajeRetorno =  Descripcion;
            }
            return response;
        }



        #region Mantenimiento de segmentos
        /****************** vmt-mvelez *************************/
        [WebMethod(Description = "MiMetaBG: Graba Segmento")]
        public prvSegmentoResponse MiMetaBG_GrabaSegmento(int IdSegmento, prvSegmentoRequest request)
        {
            prvSegmentoResponse response = new prvSegmentoResponse();
            Error error = new Error();

            try
            {
                metodos.RegistrarSegmento(IdSegmento, request.nombre, request.descripcion, request.codimagen, request.umbral, request.estado, request.prod_cruz, request.prioridad, request.mensaje_seg, request.cod_host, ref error);
                response.codigoRetorno = error.CodigoError;
                response.mensajeRetorno = error.DescripcionError;
            }
            catch (Exception e)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = "Error Inesperado - " + e.Message;
            }
            return response;
        }

        [WebMethod(Description = "MiMetaBG: Consultas de Segmentos todos sus campos")]
        public prvSegmentoGralResponse MiMetaBG_ConsGeneralSegmento(prvSegmentoGralRequest reques)
        {
            prvSegmentoGralResponse response = new prvSegmentoGralResponse();
            Error error = new Error();

            try
            {
                response.listaSegmento = metodos.ListaSegmentoGral(reques.MarcaImagen, ref error);
                response.codigoRetorno = error.CodigoError;
                response.mensajeRetorno = error.DescripcionError;
            }
            catch (Exception e)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = e.Message;
            }
            return response;
        }

        [WebMethod(Description = "MiMetaBG: Consultas de Imagenes")]
        public prvImagenGralResponse MiMetaBG_ConsGeneralImagen(prvImagenGralRequest reques)
        {
            prvImagenGralResponse response = new prvImagenGralResponse();
            Error error = new Error();

            try
            {
                response.listaImagen = metodos.ListaImagenGral(reques.MarcaImagen, ref error);
                response.codigoRetorno = error.CodigoError;
                response.mensajeRetorno = error.DescripcionError;
            }
            catch (Exception e)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = e.Message;
            }
            return response;
        }

        [WebMethod(Description = "MiMetaBG: Consultas de Producto Cruzar")]
        public prvProductoGralResponse MiMetaBG_ConsGeneralProducto(prvProductoGralRequest reques)
        {
            prvProductoGralResponse response = new prvProductoGralResponse();
            Error error = new Error();

            try
            {
                response.listaProducto = metodos.ListaProductoGral(reques.MarcaImagen, ref error);
                response.codigoRetorno = error.CodigoError;
                response.mensajeRetorno = error.DescripcionError;
            }
            catch (Exception e)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = e.Message;
            }
            return response;
        }

        [WebMethod(Description = "MiMetaBG: Consultas Login Usuario")]
        public prvLoginResponse MiMetaBG_ConsultaUsuario(prvLoginRequest reques)
        {
            prvLoginResponse response = new prvLoginResponse();
            Error error = new Error();

            try
            {
                response.listaUsuario = metodos.ListaUsuario(reques.Id_codigo, ref error);
                response.codigoRetorno = error.CodigoError;
                response.mensajeRetorno = error.DescripcionError;
            }
            catch (Exception e)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = e.Message;
            }
            return response;
        }

        [WebMethod(Description = "MiMetaBG: Graba Imagen")]
        public prvImagenResponse MiMetaBG_GrabaImagen(int Codimagen, prvImagenRequest request)
        {
            prvImagenResponse response = new prvImagenResponse();
            Error error = new Error();

            try
            {
                metodos.RegistrarImagen(Codimagen, request.imagen, request.emojin, ref error);
                response.codigoRetorno = error.CodigoError;
                response.mensajeRetorno = error.DescripcionError;
            }
            catch (Exception e)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = "Error Inesperado - " + e.Message;
            }
            return response;
        }
        /**********************************************************/
        #endregion Mantenimiento de segmentos
    }
}
