using MiMetaBG_AccesoDatos.BaseDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
{
    public class prvLoginRequest
    {
        public int Id_codigo { get; set; }
        public string usuario { get; set; }

        public prvLoginRequest()
        {
            Id_codigo = 0;
            usuario = "";
        }

        public DataSet MiMetaBG_consulta_usuario(int Id, ref string CodError, ref string Descripcion)
        {
            DataSet ds = new DataSet();
            Error e = new Error();
            Conexion db = new Conexion();
            string sql = "VIV_MiMetaBG.crud_segmento";

            try
            {
                db.ConexionBD("MiMetaBG");
                db.CrearComandoPreparado(sql);
                db.AgregarParametroSP("@Id_codigo", Id, DbType.Int16, ParameterDirection.Input, 20);
                db.AgregarParametroSP("@tabla", "APLICACION", DbType.String, ParameterDirection.Input, 10);
               
                ds = db.EjecutarConsultaDataSet();
                CodError = "0000";
                Descripcion = "Ok-Exitoso";

                return ds;
            }
            catch (Exception ex_Segmento)
            {
                CodError = "403";
                Descripcion = "Base Datos: " + ex_Segmento.Message;
                return ds;
            }
            finally
            {
                db.Desconectar();
            }
        }
    }
}
