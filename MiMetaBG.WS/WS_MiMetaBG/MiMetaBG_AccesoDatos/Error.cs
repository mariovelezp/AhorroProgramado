using MiMetaBG_AccesoDatos.BaseDatos;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos
{
    public class Error
    {
        public string CodigoError { get; set; }
        public string DescripcionError { get; set; }

        public Error() {
            CodigoError = "";
            DescripcionError = "";
        }


        public static Error ConsultaError(string CodigoRetorno, string MensajeRetorno, string ServicioWeb)
        {
            Conexion con = new Conexion();
            string sql = "VIV_MiMetaBG.Obtener_Cod_Error";
            
            Error Errores = new Error();
            DataSet dts = new DataSet();
            
            try
            {
                if (CodigoRetorno != "")
                {
                    con.ConexionBD("MiMetaBG");
                    con.CrearComandoPreparado(sql);
                    con.AgregarParametroSP("@CodigoRetorno", CodigoRetorno, DbType.String, ParameterDirection.Input);
                    con.AgregarParametroSP("@MensajeRetorno", MensajeRetorno, DbType.String, ParameterDirection.Input);
                    con.AgregarParametroSP("@ServicioWeb", ServicioWeb, DbType.String, ParameterDirection.Input);
                    dts = con.EjecutarConsultaDataSet();
                    for (int i = 0; i <= dts.Tables[0].Rows.Count - 1; i++)
                    {
                        Errores.CodigoError = Convert.ToString(dts.Tables[0].Rows[0]["CodigoError"]);
                        Errores.DescripcionError = Convert.ToString(dts.Tables[0].Rows[0]["DescripcionError"]);
                    }
                    return Errores;
                }
                else
                {
                    throw new System.InvalidOperationException("Parámetro inválido");
                }
            }
            catch (Exception e)
            {
                Errores.CodigoError = Convert.ToString(ConfigurationManager.AppSettings.Get("ProcesoErrorDesconocido"));
                Errores.DescripcionError = e.Message + "(MiMetaBG)";
                return Errores;
            }
            finally
            {
                if (con != null)
                    con.Desconectar();
            }

        }
    }
}
