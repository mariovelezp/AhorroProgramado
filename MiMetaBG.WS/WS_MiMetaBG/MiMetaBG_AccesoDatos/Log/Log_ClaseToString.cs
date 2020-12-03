using MiMetaBG_AccesoDatos.BaseDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Log
{
    public class Log_ClaseToString
    {
        public static string ViveBGRequest { get; set; }
        public static string ViveBGResponse { get; set; }
        public static int? Opcion { get; set; }
        public static int id_Error { get; set; }
        public static int idError { get; set; }
        public static DateTime? fecha_envio { get; set; }
        public static DateTime? fecha_recepcion { get; set; }
        public static string ip_Acceso { get; set; }
        public static string cod_Retorno { get; set; }
        public static string msj_Retorno { get; set; }
        public static string Servicio { get; set; }
        public static string Identificacion { get; set; }
        public static string accion_Cliente { get; set; }

        public Log_ClaseToString()
        {
            fecha_envio = DateTime.Today;
            fecha_recepcion = DateTime.Today;
        }

        public static int GrabarLogReq(Log.logmasterRequest LogMaster, string OP, string accion, string servicio_clte, string Identificacion_clte, string accion_clte, string opcional = "", string IdentificacionLogs = "", string tipoIdentificacionLogs = "")
        {
            try
            {
                switch (OP)
                {
                    case "01":  //Consultar Paramaetros
                            ViveBGRequest = "IdSegmento<" + vString(LogMaster.parametroRequest.idSegmento) + ">;"
                                                + "MarcaImagen<" + vString(LogMaster.parametroRequest.MarcaImagen) + ">;"
                                                + "idPantalla<" + vString(LogMaster.parametroRequest.idPantalla) + ">;"
                                                + "Parametros<" + vString(opcional) + ">;";
                            ip_Acceso = LogMaster.parametroRequest.Ip;
                            Servicio = servicio_clte;
                            fecha_recepcion = DateTime.Now;
                            Servicio = servicio_clte;
                            Identificacion = Identificacion_clte;
                            accion_Cliente = accion_clte;
                            Opcion = 1;
                            id_Error = InsertarLogs(0, 1);
                        break;
                    case "03":  //Preguntar si es cliente
                            ViveBGRequest = "identificacion<" + vString(LogMaster.validacionClienteRequest.identificacion) + ">;"
                                                + "TipoIdentificacion<" + vString(LogMaster.validacionClienteRequest.tipoIdentificacion) + ">;"
                                                + "idPantalla<" + vString(LogMaster.validacionClienteRequest.idPantalla) + ">;"
                                                + "Parametros<" + vString(opcional) + ">;";
                            ip_Acceso = LogMaster.validacionClienteRequest.ip;
                            Servicio = servicio_clte;
                            fecha_recepcion = DateTime.Now;
                            Servicio = servicio_clte;
                            Identificacion = Identificacion_clte;
                            accion_Cliente = accion_clte;
                            Opcion = 1;
                            id_Error = InsertarLogs(0, 1);
                        break;
                    case "04": //Preguntar si posee Cuentas
                            ViveBGRequest = "identificacion<" + vString(LogMaster.validacionClienteRequest.identificacion) + ">;"
                                               + "TipoIdentificacion<" + vString(LogMaster.validacionClienteRequest.tipoIdentificacion) + ">;"
                                               + "idPantalla<" + vString(LogMaster.validacionClienteRequest.idPantalla) + ">;"
                                               + "Parametros<" + vString(opcional) + ">;";
                            ip_Acceso = LogMaster.validacionClienteRequest.ip;
                            Servicio = servicio_clte;
                            fecha_recepcion = DateTime.Now;
                            Servicio = servicio_clte;
                            Identificacion = Identificacion_clte;
                            accion_Cliente = accion_clte;
                            Opcion = 1;
                            id_Error = InsertarLogs(0, 1);
                        break;
                    case "05": // Generar OTP
                            ViveBGRequest = "identificacion<" + vString(LogMaster.validacionClienteRequest.identificacion) + ">;"
                                               + "TipoIdentificacion<" + vString(LogMaster.validacionClienteRequest.tipoIdentificacion) + ">;"
                                               + "idPantalla<" + vString(LogMaster.validacionClienteRequest.idPantalla) + ">;"
                                               + "ParametrosEntrust<" + vString(opcional) + ">;";
                            ip_Acceso = LogMaster.validacionClienteRequest.ip;
                            Servicio = servicio_clte;
                            fecha_recepcion = DateTime.Now;
                            Servicio = servicio_clte;
                            Identificacion = Identificacion_clte;
                            accion_Cliente = accion_clte;
                            Opcion = 1;
                            id_Error = InsertarLogs(0, 1);
                        break;
                    case "06": // Enviar al correo
                            ViveBGRequest = "identificacion<" + vString(LogMaster.validacionClienteRequest.identificacion) + ">;"
                                               + "TipoIdentificacion<" + vString(LogMaster.validacionClienteRequest.tipoIdentificacion) + ">;"
                                               + "idPantalla<" + vString(LogMaster.validacionClienteRequest.idPantalla) + ">;"
                                               + "ParametrosAvisoBG<" + vString(opcional) + ">;";
                            ip_Acceso = LogMaster.validacionClienteRequest.ip;
                            Servicio = servicio_clte;
                            fecha_recepcion = DateTime.Now;
                            Servicio = servicio_clte;
                            Identificacion = Identificacion_clte;
                            accion_Cliente = accion_clte;
                            Opcion = 1;
                            id_Error = InsertarLogs(0, 1);
                        break;
                    case "07": // Validar OTP
                            ViveBGRequest = "identificacion<" + vString(LogMaster.validacionOTPRequest.identificacion) + ">;"
                                               + "codigoOTP<" + vString(LogMaster.validacionOTPRequest.codigoOTP) + ">;"
                                               + "idPantalla<" + vString(LogMaster.validacionOTPRequest.idPantalla) + ">;"
                                               + "Parametros<" + vString(opcional) + ">;";
                            ip_Acceso = LogMaster.validacionOTPRequest.ip;
                            Servicio = servicio_clte;
                            fecha_recepcion = DateTime.Now;
                            Servicio = servicio_clte;
                            Identificacion = Identificacion_clte;
                            accion_Cliente = accion_clte;
                            Opcion = 1;
                            id_Error = InsertarLogs(0, 1);
                        break;
                    case "08": // Validar Token
                            ViveBGRequest = "identificacion<" + vString(LogMaster.validacionOTPRequest.identificacion) + ">;"
                                               + "codigoOTP<" + vString(LogMaster.validacionOTPRequest.codigoOTP) + ">;"
                                               + "idPantalla<" + vString(LogMaster.validacionOTPRequest.idPantalla) + ">;"
                                               + "Parametros<" + vString(opcional) + ">;";
                            ip_Acceso = LogMaster.validacionOTPRequest.ip;
                            Servicio = servicio_clte;
                            fecha_recepcion = DateTime.Now;
                            Servicio = servicio_clte;
                            Identificacion = Identificacion_clte;
                            accion_Cliente = accion_clte;
                            Opcion = 1;
                            id_Error = InsertarLogs(0, 1);
                        break;
                    case "09": // Consultar Cuentas
                            ViveBGRequest = "identificacion<" + vString(IdentificacionLogs) + ">;"
                                               + "TipoIdentificacion<" + vString(tipoIdentificacionLogs) + ">;"
                                               + "idPantalla<" + vString(LogMaster.cuentasRequest.idPantalla) + ">;"
                                               + "Parametros<" + vString(opcional) + ">;";
                            ip_Acceso = LogMaster.cuentasRequest.ip;
                            Servicio = servicio_clte;
                            fecha_recepcion = DateTime.Now;
                            Servicio = servicio_clte;
                            Identificacion = Identificacion_clte;
                            accion_Cliente = accion_clte;
                            Opcion = 1;
                            id_Error = InsertarLogs(0, 1);
                        break;
                    case "10": // Consultar segmento
                        ViveBGRequest = "idSegmento<" + vString(LogMaster.cuentasRequest.idSegmento) + ">;"
                                        + "idPantalla<" + vString(LogMaster.cuentasRequest.idPantalla) + ">;"
                                        + "Parametros<" + vString(opcional) + ">;";
                        ip_Acceso = LogMaster.cuentasRequest.ip;
                        Servicio = servicio_clte;
                        fecha_recepcion = DateTime.Now;
                        Servicio = servicio_clte;
                        Identificacion = Identificacion_clte;
                        accion_Cliente = accion_clte;
                        Opcion = 1;
                        id_Error = InsertarLogs(0, 1);
                        break;

                    case "11": // Enviar datos a Host
                            ViveBGRequest = "Identificacion<" + vString(IdentificacionLogs) + ">;"
                                               + "DireccionIP<" + vString(LogMaster.metaRequest.direccionIP) + ">;"
                                               + "Accion<" + vString(LogMaster.metaRequest.accion) + ">;"
                                               + "CuentaDebito<" + vString(LogMaster.metaRequest.cuentaDebito) + ">;"
                                               + "CuentaMultiple<" + vString(LogMaster.metaRequest.cuentaMultiple) + ">;"
                                               + "DiaDosDebito<" + vString(LogMaster.metaRequest.diaDosDebito) + ">;"
                                               + "DiaUnoDebito<" + vString(LogMaster.metaRequest.diaUnoDebito) + ">;"
                                               + "MontoDebito<" + vString(LogMaster.metaRequest.montoDebito) + ">;"
                                               + "FechaVencimiento<" + vString(LogMaster.metaRequest.fechaVencimiento) + ">;"
                                               + "Frencuencia<" + vString(LogMaster.metaRequest.frencuencia) + ">;"
                                               + "MacAdress<" + vString(LogMaster.metaRequest.macAdress) + ">;"
                                               + "MontoMeta<" + vString(LogMaster.metaRequest.montoMeta) + ">;"
                                               + "NumeroSolicitud<" + vString(LogMaster.metaRequest.numeroSolicitud) + ">;"
                                               + "TipoCtaDebito<" + vString(LogMaster.metaRequest.tipoCtaDebito) + ">;"
                                               + "idPantalla<" + vString(LogMaster.metaRequest.idPantalla) + ">;"
                                               + "Parametros<" + vString(opcional) + ">;";
                            ip_Acceso = LogMaster.metaRequest.direccionIP;
                            Servicio = servicio_clte;
                            fecha_recepcion = DateTime.Now;
                            Servicio = servicio_clte;
                            Identificacion = Identificacion_clte;
                            accion_Cliente = accion_clte;
                            Opcion = 1;
                            id_Error = InsertarLogs(0, 1);
                        break;
                    case "12": // Enviar datos a Base de datos
                            ViveBGRequest = "identificacion<" + vString(IdentificacionLogs) + ">;"
                                               + "FechaCreacion<" + vString(LogMaster.metaRequest.fechaCreacion) + ">;"
                                               + "CuentaAhorro<" + vString(LogMaster.metaRequest.cuentaMultiple) + ">;"
                                               + "IdSegmento<" + vString(LogMaster.metaRequest.idSegmento) + ">;"
                                               + "NombreMeta<" + vString(LogMaster.metaRequest.nombreMeta) + ">;"
                                               + "NumeroSolicitud<" + vString(LogMaster.metaRequest.numeroSolicitud) + ">;"
                                               + "idPantalla<" + vString(LogMaster.metaRequest.idPantalla) + ">;"
                                               + "Parametros<" + vString(opcional) + ">;";
                            ip_Acceso = LogMaster.metaRequest.direccionIP;
                            Servicio = servicio_clte;
                            fecha_recepcion = DateTime.Now;
                            Servicio = servicio_clte;
                            Identificacion = Identificacion_clte;
                            accion_Cliente = accion_clte;
                            Opcion = 1;
                            id_Error = InsertarLogs(0, 1);
                        break;
                    case "13": // Enviar datos a Base de datos
                        ViveBGRequest = "identificacion<" + vString(IdentificacionLogs) + ">;"
                                           + "aplicacion<" + vString(LogMaster.consultasMetas.aplicacion) + ">;"
                                           + "tipoIdentificacion<" + vString(tipoIdentificacionLogs) + ">;"
                                           + "idPantalla<" + vString(LogMaster.consultasMetas.idPantalla) + ">;"
                                           + "IdSegmento<" + vString(LogMaster.consultasMetas.idSegmento) + ">;"
                                           + "Ip<" + vString(LogMaster.consultasMetas.ip) + ">;"
                                           + "Parametros<" + vString(opcional) + ">;";
                        ip_Acceso = LogMaster.consultasMetas.ip;
                        Servicio = servicio_clte;
                        fecha_recepcion = DateTime.Now;
                        Servicio = servicio_clte;
                        Identificacion = Identificacion_clte;
                        accion_Cliente = accion_clte;
                        Opcion = 1;
                        id_Error = InsertarLogs(0, 1);
                        break;

                    case "14": // Enviar datos a Base de datos
                        ViveBGRequest = "identificacion<" + vString(IdentificacionLogs) + ">;"
                                           + "aplicacion<" + vString(LogMaster.consultasMetas.aplicacion) + ">;"
                                           + "tipoIdentificacion<" + vString(tipoIdentificacionLogs) + ">;"
                                           + "idPantalla<" + vString(LogMaster.consultasMetas.idPantalla) + ">;"
                                           + "IdSegmento<" + vString(LogMaster.consultasMetas.idSegmento) + ">;"
                                           + "Ip<" + vString(LogMaster.consultasMetas.ip) + ">;"
                                           + "Parametros<" + vString(opcional) + ">;";
                        ip_Acceso = LogMaster.consultasMetas.ip;
                        Servicio = servicio_clte;
                        fecha_recepcion = DateTime.Now;
                        Servicio = servicio_clte;
                        Identificacion = Identificacion_clte;
                        accion_Cliente = accion_clte;
                        Opcion = 1;
                        id_Error = InsertarLogs(0, 1);
                        break;

                    case "15": // Enviar datos a Base de datos
                        ViveBGRequest = "identificacion<" + vString(IdentificacionLogs) + ">;"
                                           + "aplicacion<" + vString(LogMaster.consultasMetas.aplicacion) + ">;"
                                           + "tipoIdentificacion<" + vString(tipoIdentificacionLogs) + ">;"
                                           + "idPantalla<" + vString(LogMaster.consultasMetas.idPantalla) + ">;"
                                           + "IdSegmento<" + vString(LogMaster.consultasMetas.idSegmento) + ">;"
                                           + "Ip<" + vString(LogMaster.consultasMetas.ip) + ">;"
                                           + "Parametros<" + vString(opcional) + ">;";
                        ip_Acceso = LogMaster.consultasMetas.ip;
                        Servicio = servicio_clte;
                        fecha_recepcion = DateTime.Now;
                        Servicio = servicio_clte;
                        Identificacion = Identificacion_clte;
                        accion_Cliente = accion_clte;
                        Opcion = 1;
                        id_Error = InsertarLogs(0, 1);
                        break;

                    case "16": // Enviar datos a Base de datos
                        ViveBGRequest = "identificacion<" + vString(IdentificacionLogs) + ">;"
                                           + "aplicacion<" + vString(LogMaster.consultasMetas.aplicacion) + ">;"
                                           + "tipoIdentificacion<" + vString(tipoIdentificacionLogs) + ">;"
                                           + "idPantalla<" + vString(LogMaster.consultasMetas.idPantalla) + ">;"
                                           + "IdSegmento<" + vString(LogMaster.consultasMetas.idSegmento) + ">;"
                                           + "Ip<" + vString(LogMaster.consultasMetas.ip) + ">;"
                                           + "Parametros<" + vString(opcional) + ">;";
                        ip_Acceso = LogMaster.consultasMetas.ip;
                        Servicio = servicio_clte;
                        fecha_recepcion = DateTime.Now;
                        Servicio = servicio_clte;
                        Identificacion = Identificacion_clte;
                        accion_Cliente = accion_clte;
                        Opcion = 1;
                        id_Error = InsertarLogs(0, 1);
                        break;
                    case "17": // Enviar datos a Base de datos
                        ViveBGRequest = "identificacion<" + vString(IdentificacionLogs) + ">;"
                                           + "FechaCreacion<" + vString(LogMaster.metaRequest.fechaCreacion) + ">;"
                                           + "CuentaAhorro<" + vString(LogMaster.metaRequest.cuentaMultiple) + ">;"
                                           + "IdSegmento<" + vString(LogMaster.metaRequest.idSegmento) + ">;"
                                           + "NombreMeta<" + vString(LogMaster.metaRequest.nombreMeta) + ">;"
                                           + "NumeroSolicitud<" + vString(LogMaster.metaRequest.numeroSolicitud) + ">;"
                                           + "idPantalla<" + vString(LogMaster.metaRequest.idPantalla) + ">;"
                                           + "Parametros<" + vString(opcional) + ">;";
                        ip_Acceso = LogMaster.metaRequest.direccionIP;
                        Servicio = servicio_clte;
                        fecha_recepcion = DateTime.Now;
                        Servicio = servicio_clte;
                        Identificacion = Identificacion_clte;
                        accion_Cliente = accion_clte;
                        Opcion = 1;
                        id_Error = InsertarLogs(0, 1);
                        break;
                    case "18": // Enviar datos a Base de datos
                        ViveBGRequest = "identificacion<" + vString(IdentificacionLogs) + ">;"
                                           + "aplicacion<" + vString(LogMaster.AdicionalRequest.aplicativo) + ">;"
                                           + "tipoIdentificacion<" + vString(tipoIdentificacionLogs) + ">;"
                                           + "idPantalla<" + vString(LogMaster.AdicionalRequest.idPantalla) + ">;"
                                           + "ctaC<" + vString(LogMaster.AdicionalRequest.ctaC) + ">;"
                                           + "ctaD<" + vString(LogMaster.AdicionalRequest.ctaD) + ">;"
                                           + "ctaEmpresa<" + vString(LogMaster.AdicionalRequest.ctaEmpresa) + ">;"
                                           + "IdTipoBeneficiario<" + vString(LogMaster.AdicionalRequest.IdTipoBeneficiario) + ">;"
                                           + "numeroSolicitud<" + vString(LogMaster.AdicionalRequest.numeroSolicitud) + ">;"
                                           + "referencia<" + vString(LogMaster.AdicionalRequest.referencia) + ">;"
                                           + "tipoTarC<" + vString(LogMaster.AdicionalRequest.tipoTarC) + ">;"
                                           + "tipoTarD<" + vString(LogMaster.AdicionalRequest.tipoTarD) + ">;"
                                           + "valorD<" + vString(LogMaster.AdicionalRequest.valorD) + ">;"
                                           + "Ip<" + vString(LogMaster.AdicionalRequest.ip) + ">;"
                                           + "Parametros<" + vString(opcional) + ">;";
                        ip_Acceso = LogMaster.AdicionalRequest.ip;
                        Servicio = servicio_clte;
                        fecha_recepcion = DateTime.Now;
                        Servicio = servicio_clte;
                        Identificacion = Identificacion_clte;
                        accion_Cliente = accion_clte;
                        Opcion = 1;
                        id_Error = InsertarLogs(0, 1);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            return id_Error;
        }


        public static void GrabarLogRes(Log.logmasterResponse Log, string OP, string accion, string CodigoRetorno, string MensajeRetorno, int idError_cabecera, string Opcional = "")
        {
            try
            {
                switch (OP)
                {
                    case "01": //Obtencion de Parametros
                            ViveBGResponse = "CodigoRetorno<" + vString(Log.parametroResponse.codigoRetorno) + ">;" 
                                            + "MensajeRetorno<" + vString(Log.parametroResponse.mensajeRetorno) + ">;" 
                                            + "MontoInicial<" + vString(Log.parametroResponse.montoInicial) + ">;" 
                                            + "MontoFinal<" + vString(Log.parametroResponse.montoFinal) + ">;" 
                                            + "TiempoIncial<" + vString(Log.parametroResponse.tiempoIncial) + ">;" 
                                            + "TiempoFinal<" + vString(Log.parametroResponse.tiempoFinal) + ">;"
                                            + "Mensaje<" + vString(Log.parametroResponse.mensaje) + ">;"
                                            + "Parametros<" + vString(Opcional) + ">;";
                            fecha_envio = DateTime.Now;
                            cod_Retorno = CodigoRetorno;
                            Opcion = 2;
                            msj_Retorno = MensajeRetorno;
                            idError = idError_cabecera;
                            InsertarLogs(idError_cabecera, 2);
                        break;

                    case "03": //Respuesta si es Cliente
                            ViveBGResponse = "CodigoRetorno<" + vString(Log.validacionClienteResponse.codigoRetorno) + ">;"
                                            + "MensajeRetorno<" + vString(Log.validacionClienteResponse.mensajeRetorno) + ">;"
                                            + "CorreoElectronico<" + vString(Log.validacionClienteResponse.correoElectronico) + ">;"
                                            + "NombreCliente<" + vString(Log.validacionClienteResponse.nombreCliente) + ">;"
                                            + "Parametros<" + vString(Opcional) + ">;";
                            fecha_envio = DateTime.Now;
                            cod_Retorno = CodigoRetorno;
                            Opcion = 2;
                            msj_Retorno = MensajeRetorno;
                            idError = idError_cabecera;
                            InsertarLogs(idError_cabecera, 2);
                        break;

                    case "04": //Respuesta si posee cuentas 
                            ViveBGResponse = "CodigoRetorno<" + vString(Log.validacionClienteResponse.codigoRetorno) + ">;"
                                            + "MensajeRetorno<" + vString(Log.validacionClienteResponse.mensajeRetorno) + ">;"
                                            + "Parametros<" + vString(Opcional) + ">;";
                            fecha_envio = DateTime.Now;
                            cod_Retorno = CodigoRetorno;
                            Opcion = 2;
                            msj_Retorno = MensajeRetorno;
                            idError = idError_cabecera;
                            InsertarLogs(idError_cabecera, 2);
                        break;
                    case "05": //Respuesta si se genera token 
                            ViveBGResponse = "CodigoRetorno<" + vString(Log.validacionClienteResponse.codigoRetorno) + ">;"
                                            + "MensajeRetorno<" + vString(Log.validacionClienteResponse.mensajeRetorno) + ">;"
                                            + "Parametros<" + vString(Opcional) + ">;";
                            fecha_envio = DateTime.Now;
                            cod_Retorno = CodigoRetorno;
                            Opcion = 2;
                            msj_Retorno = MensajeRetorno;
                            idError = idError_cabecera;
                            InsertarLogs(idError_cabecera, 2);
                        break;
                    case "06": //Respuesta si se genera correo 
                            ViveBGResponse = "CodigoRetorno<" + vString(Log.validacionClienteResponse.codigoRetorno) + ">;"
                                            + "MensajeRetorno<" + vString(Log.validacionClienteResponse.mensajeRetorno) + ">;"
                                            + "Parametros<" + vString(Opcional) + ">;";
                            fecha_envio = DateTime.Now;
                            cod_Retorno = CodigoRetorno;
                            Opcion = 2;
                            msj_Retorno = MensajeRetorno;
                            idError = idError_cabecera;
                            InsertarLogs(idError_cabecera, 2);
                        break;
                    case "07": //Respuesta validacion OTP 
                            ViveBGResponse = "CodigoRetorno<" + vString(Log.validacionOTPResponse.codigoRetorno) + ">;"
                                            + "MensajeRetorno<" + vString(Log.validacionOTPResponse.mensajeRetorno) + ">;"
                                            + "Parametros<" + vString(Opcional) + ">;";
                            fecha_envio = DateTime.Now;
                            cod_Retorno = CodigoRetorno;
                            Opcion = 2;
                            msj_Retorno = MensajeRetorno;
                            idError = idError_cabecera;
                            InsertarLogs(idError_cabecera, 2);
                        break;
                    case "08": //Respuesta validacion de Token
                            ViveBGResponse = "CodigoRetorno<" + vString(Log.validacionOTPResponse.codigoRetorno) + ">;"
                                            + "MensajeRetorno<" + vString(Log.validacionOTPResponse.mensajeRetorno) + ">;"
                                            + "TokenValidacion<" + vString(Log.validacionOTPResponse.tokenValidacion) + ">;"
                                            + "Parametros<" + vString(Opcional) + ">;";
                            fecha_envio = DateTime.Now;
                            cod_Retorno = CodigoRetorno;
                            Opcion = 2;
                            msj_Retorno = MensajeRetorno;
                            idError = idError_cabecera;
                            InsertarLogs(idError_cabecera, 2);
                        break;
                    case "09": //Respuesta consulta cuentas
                            ViveBGResponse = "CodigoRetorno<" + vString(Log.cuentasResponse.codigoRetorno) + ">;"
                                            + "MensajeRetorno<" + vString(Log.cuentasResponse.mensajeRetorno) + ">;"
                                            + "ListaTodas<" + vString(Log.cuentasResponse.ListaTodas) + ">;"
                                            + "ListaUna<" + vString(Log.cuentasResponse.ListaUna) + ">;"
                                            + "Parametros<" + vString(Opcional) + ">;";
                            fecha_envio = DateTime.Now;
                            cod_Retorno = CodigoRetorno;
                            Opcion = 2;
                            msj_Retorno = MensajeRetorno;
                            idError = idError_cabecera;
                            InsertarLogs(idError_cabecera, 2);
                        break;
                    case "10": //Respuesta de segentos
                            ViveBGResponse = "CodigoRetorno<" + vString(Log.cuentasResponse.codigoRetorno) + ">;"
                                            + "MensajeRetorno<" + vString(Log.cuentasResponse.mensajeRetorno) + ">;"
                                            + "idSegmento<" + vString(Log.cuentasResponse.idSegmento) + ">;"
                                            + "Parametros<" + vString(Opcional) + ">;";
                            fecha_envio = DateTime.Now;
                            cod_Retorno = CodigoRetorno;
                            Opcion = 2;
                            msj_Retorno = MensajeRetorno;
                            idError = idError_cabecera;
                            InsertarLogs(idError_cabecera, 2);
                        break;
                    case "11": //Respuesta Creacion de Metas
                            ViveBGResponse = "CodigoRetorno<" + vString(Log.metaResponse.codigoRetorno) + ">;"
                                            + "MensajeRetorno<" + vString(Log.metaResponse.mensajeRetorno) + ">;"
                                            
                                            + "Numero de Solicitud<" + vString(Opcional) + ">;";
                            fecha_envio = DateTime.Now;
                            cod_Retorno = CodigoRetorno;
                            Opcion = 2;
                            msj_Retorno = MensajeRetorno;
                            idError = idError_cabecera;
                            InsertarLogs(idError_cabecera, 2);
                        break;
                    case "12": //Respuesta Creacion de Metas BD
                            ViveBGResponse = "CodigoRetorno<" + vString(Log.metaResponse.codigoRetorno) + ">;"
                                            + "MensajeRetorno<" + vString(Log.metaResponse.mensajeRetorno) + ">;"
                                            + "Parametros<" + vString(Opcional) + ">;";
                            fecha_envio = DateTime.Now;
                            cod_Retorno = CodigoRetorno;
                            Opcion = 2;
                            msj_Retorno = MensajeRetorno;
                            idError = idError_cabecera;
                            InsertarLogs(idError_cabecera, 2);
                        break;
                    case "13": //Respuesta Creacion de Metas BD
                        ViveBGResponse = "CodigoRetorno<" + vString(Log.consultaMetasResponse.codigoRetorno) + ">;"
                                        + "MensajeRetorno<" + vString(Log.consultaMetasResponse.mensajeRetorno) + ">;"
                                        + "Parametros<" + vString(Opcional) + ">;";
                        fecha_envio = DateTime.Now;
                        cod_Retorno = CodigoRetorno;
                        Opcion = 2;
                        msj_Retorno = MensajeRetorno;
                        idError = idError_cabecera;
                        InsertarLogs(idError_cabecera, 2);
                        break;
                    case "14": //Respuesta Creacion de Metas BD
                        ViveBGResponse = "CodigoRetorno<" + vString(Log.consultaMetasResponse.codigoRetorno) + ">;"
                                        + "MensajeRetorno<" + vString(Log.consultaMetasResponse.mensajeRetorno) + ">;"
                                        + "Parametros<" + vString(Opcional) + ">;";
                        fecha_envio = DateTime.Now;
                        cod_Retorno = CodigoRetorno;
                        Opcion = 2;
                        msj_Retorno = MensajeRetorno;
                        idError = idError_cabecera;
                        InsertarLogs(idError_cabecera, 2);
                        break;
                    case "15": //Respuesta Creacion de Metas BD
                        ViveBGResponse = "CodigoRetorno<" + vString(Log.consultaMetasResponse.codigoRetorno) + ">;"
                                        + "MensajeRetorno<" + vString(Log.consultaMetasResponse.mensajeRetorno) + ">;"
                                        + "Parametros<" + vString(Opcional) + ">;";
                        fecha_envio = DateTime.Now;
                        cod_Retorno = CodigoRetorno;
                        Opcion = 2;
                        msj_Retorno = MensajeRetorno;
                        idError = idError_cabecera;
                        InsertarLogs(idError_cabecera, 2);
                        break;
                    case "17": //Respuesta Creacion de Metas BD
                        ViveBGResponse = "CodigoRetorno<" + vString(Log.metaResponse.codigoRetorno) + ">;"
                                        + "MensajeRetorno<" + vString(Log.metaResponse.mensajeRetorno) + ">;"
                                        + "Parametros<" + vString(Opcional) + ">;";
                        fecha_envio = DateTime.Now;
                        cod_Retorno = CodigoRetorno;
                        Opcion = 2;
                        msj_Retorno = MensajeRetorno;
                        idError = idError_cabecera;
                        InsertarLogs(idError_cabecera, 2);
                        break;
                    case "18": //Respuesta Adicionar
                        ViveBGResponse = "CodigoRetorno<" + vString(Log.AdicionalResponse.codigoRetorno) + ">;"
                                        + "MensajeRetorno<" + vString(Log.AdicionalResponse.mensajeRetorno) + ">;"
                                        + "Parametros<" + vString(Opcional) + ">;";
                        fecha_envio = DateTime.Now;
                        cod_Retorno = CodigoRetorno;
                        Opcion = 2;
                        msj_Retorno = MensajeRetorno;
                        idError = idError_cabecera;
                        InsertarLogs(idError_cabecera, 2);
                        break;

                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public static string vString(object valor)
        {
            if (valor == null)
            {
                return "";
            }
            else if (valor.ToString() == "\0")
            {
                return "";
            }
            else { return Convert.ToString(valor); }
        }


        public static int InsertarLogs(int idError, int Opcion)
        {
            Conexion db = new Conexion();
            string sql = "VIV_MiMetaBG.BG_InsercionViveBG_Logs";
            Error Errores = new Error();
            DataSet ds = new DataSet();
            object ViCliente = new object();
            object ViCodRetorno = new object();
            object ViMjsRetorno = new object();
            object ViTramaRequest = new object();
            object ViFecRecepcion = new object();
            object ViServicio = new object();
            object ViIpAcceso = new object();
            object ViaccionCliente = new object();
            object ViFecEnvio = new object();
            object ViTramaResponse = new object();

            int id_Error = 0;

            if (Identificacion == null)
                ViCliente = DBNull.Value;
            else
                ViCliente = Identificacion;

            if (Servicio == null)
                ViServicio = DBNull.Value;
            else
                ViServicio = Servicio;

            if (ip_Acceso == null)
                ViIpAcceso = DBNull.Value;
            else
                ViIpAcceso = ip_Acceso;

            if (accion_Cliente == null)
                ViaccionCliente = DBNull.Value;
            else
                ViaccionCliente = accion_Cliente;

            if (fecha_envio == null)
                ViFecEnvio = DBNull.Value;
            else
                ViFecEnvio = fecha_envio;

            if (ViveBGResponse == null)
                ViTramaResponse = DBNull.Value;
            else
                ViTramaResponse = ViveBGResponse;

            if (ViveBGRequest == null)
                ViTramaRequest = DBNull.Value;
            else
                ViTramaRequest = ViveBGRequest;

            if (fecha_recepcion == null)
                ViFecRecepcion = DBNull.Value;
            else
                ViFecRecepcion = fecha_recepcion;

            if (cod_Retorno == null)
                ViCodRetorno = DBNull.Value;
            else
                ViCodRetorno = cod_Retorno;

            if (msj_Retorno == null)
                ViMjsRetorno = DBNull.Value;
            else
                ViMjsRetorno = msj_Retorno;
            
            try
            {
                db.ConexionBD("MiMetaBG");
                db.CrearComandoPreparado(sql);

                
                db.AgregarParametroSP("@Opcion", Opcion, DbType.Int32, ParameterDirection.Input);
                db.AgregarParametroSP("@Ip_acceso", ViIpAcceso, DbType.String, ParameterDirection.Input);
                db.AgregarParametroSP("@Fecha_envio", ViFecEnvio, DbType.DateTime, ParameterDirection.Input);
                db.AgregarParametroSP("@Trama_Envio", ViTramaResponse, DbType.String, ParameterDirection.Input);
                db.AgregarParametroSP("@Fecha_recepcion", ViFecRecepcion, DbType.DateTime, ParameterDirection.Input);
                db.AgregarParametroSP("@Trama_Recepcion", ViTramaRequest, DbType.String, ParameterDirection.Input);
                db.AgregarParametroSP("@Cod_retorno", ViCodRetorno, DbType.String, ParameterDirection.Input);
                db.AgregarParametroSP("@Msj_Retorno", ViMjsRetorno, DbType.String, ParameterDirection.Input);
                db.AgregarParametroSP("@Servicio", ViServicio, DbType.String, ParameterDirection.Input);
                db.AgregarParametroSP("@Identificacion_clte", ViCliente, DbType.String, ParameterDirection.Input);
                db.AgregarParametroSP("@Accion_Cliente", ViaccionCliente, DbType.String, ParameterDirection.Input);
                db.AgregarParametroSP("@idError", idError, DbType.Int32, ParameterDirection.Input);
                db.AgregarParametroSP("@Id_error", 0, DbType.Int32, ParameterDirection.Output);
                ds = db.EjecutarConsultaDataSet();
                if(Opcion==1)
                    id_Error = Convert.ToInt32(db.RetornaParametroSP("@Id_error"));
                
            }
            catch (Exception e)
            {
                Errores.CodigoError = Convert.ToString(ConfigurationManager.AppSettings.Get("ProcesoErrorDesconocido"));
                Errores.DescripcionError = "Cabecera log:" + e.Message;
            }
            finally
            {
                if (db != null)
                    db.Desconectar();

            }
            return id_Error;
        }


       
    }
}
