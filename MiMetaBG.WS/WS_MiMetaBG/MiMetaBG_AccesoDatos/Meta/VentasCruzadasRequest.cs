using MiMetaBG_AccesoDatos.BaseDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Meta
{
    public class VentasCruzadasRequest
    {
        public string tokenValidacion { get; set; }
        public int cantidadVecesMostradas { get; set; }
        public string ip { get; set; }
        public string aplicacion { get; set; }
        public string idPantalla { get; set; }



        public void MiMetaBG_Actualizar_Ventas_Cruzadas(int Opcion, string Identificacion, int cnt_ventas_Cruzadas, ref string CodError, ref string Descripcion)
        {
            DataSet ds = new DataSet();
            Error e = new Error();
            Conexion db = new Conexion();
            string respuesta = string.Empty;
            string sql = "VIV_MiMetaBG.Ventas_Cruzadas";

            try
            {
                db.ConexionBD("MiMetaBG");
                db.CrearComandoPreparado(sql);
                db.AgregarParametroSP("@Opcion", Opcion, DbType.Int32, ParameterDirection.Input, 20);
                db.AgregarParametroSP("@cedula_clte", Identificacion, DbType.String, ParameterDirection.Input, 13);
                db.AgregarParametroSP("@Cnt_veces_Mostradas", cnt_ventas_Cruzadas, DbType.Int32, ParameterDirection.Input, 1);
                ds = db.EjecutarConsultaDataSet();

                if (ds != null)
                {
                    CodError = ds.Tables[0].Rows[0][0].ToString();
                    Descripcion = ds.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                    CodError = "";
                    Descripcion = "";
                }
            }
            catch (Exception ex)
            {
                CodError = "9999";
                Descripcion = "Error Inesperado - " + ex.Message;
            }
            finally
            {
                db.Desconectar();
            }
        }
    }
}
