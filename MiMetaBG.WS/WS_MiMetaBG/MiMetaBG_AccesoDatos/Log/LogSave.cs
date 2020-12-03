using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;

namespace MiMetaBG_AccesoDatos.Log
{
    public class LogSave
    {
        public static int id_Error { get; set; }
        public static string fecha { get; set; }
        public static string hora { get; set; }
        public static string ip_Acceso { get; set; }
        public static string cod_Retorno { get; set; }
        public static string msj_Retorno { get; set; }
        public static string Servicio { get; set; }
        public static string Identificacion_clte { get; set; }
        public static string accion_Cliente { get; set; }



        public int LOGSAVEREQ(logmasterRequest log, string op, string action, string servicioInvocado, string identificacionPersona, string accion, string opcional = "",  string Identificacion = "", string tipoIdentificacion = "")
        {
            try
            {
                string option = op.ToString();
                if (bool.Parse(ConfigurationManager.AppSettings["log"].ToString()))
                {
                        id_Error = Log_ClaseToString.GrabarLogReq(log, option, action, servicioInvocado, identificacionPersona, accion, opcional, Identificacion, tipoIdentificacion);
                }
            }
            catch (Exception ex)
            {
                //grabaLogs("Error al grabar logs [LOGSAVERES]", ex.ToString() + "[" + "metodoOrigen:" + metodoOrigen + "|" + "mensajeError:" + mensajeError + "]");
            }
            return id_Error;
        }



        public void LOGSAVERES(logmasterResponse log, string op,  string action,  string CodRetorno, string MjsRetorno,  int idCabecera, string Opcional = "")
        {
            try
            {
                string option = op.ToString();
                if (bool.Parse(ConfigurationManager.AppSettings["log"].ToString()))
                {
                    Log_ClaseToString.GrabarLogRes(log, option, action, CodRetorno, MjsRetorno, idCabecera, Opcional);
                }
            }
            catch (Exception ex)
            {
                //grabaLogs("Error al grabar logs [LOGSAVERES]", ex.ToString() + "[" + "metodoOrigen:" + metodoOrigen + "|" + "mensajeError:" + mensajeError + "]");
            }
        }


        public static string toXmlString(object MetodoClass)
        {
            string xml;
            try
            {
                var stringwriter = new System.IO.StringWriter();
                var serializer = new System.Xml.Serialization.XmlSerializer(MetodoClass.GetType());
                serializer.Serialize(stringwriter, MetodoClass);
                xml = stringwriter.ToString().Replace("\n", "").Replace("\r", "");
            }
            catch
            {
                xml = "";
            }
            return xml;
        }


        public void grabaLogs(string Metodo, string mensaje)
        {
            try
            {
                if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs"))
                    System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs");
                System.IO.StreamWriter sb = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\AhorroMeta_LogsCustomError_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                sb.Write("[CustomError]: " + DateTime.Now.ToString() + "\r\n");
                sb.Write("[Metodo]: " + Metodo + "\r\n");
                sb.Write("[Message]: " + mensaje + "\r\n");
                sb.Write("[CustomError]: FIN \r\n");

                sb.Close();
            }
            catch (Exception ex)
            { }
        }
    }
}
