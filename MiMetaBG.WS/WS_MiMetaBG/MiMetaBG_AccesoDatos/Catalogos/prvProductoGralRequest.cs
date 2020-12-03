using MiMetaBG_AccesoDatos.BaseDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
{
    public class prvProductoGralRequest
    {
        public int MarcaImagen { get; set; }
        public string Ip { get; set; }
        public string Aplicacion { get; set; }
        public string idPantalla { get; set; }


        public prvProductoGralRequest()
        {
            MarcaImagen = 0;
            Ip = "";
            Aplicacion = "";
            idPantalla = "";
        }

        public DataSet MiMetaBG_consGral_Producto(int Id, ref string CodError, ref string Descripcion)
        {
            DataSet ds = new DataSet();
            Error e = new Error();
            Conexion db = new Conexion();
            string sql = "VIV_MiMetaBG.crud_segmento";

            try
            {
                db.ConexionBD("MiMetaBG");
                db.CrearComandoPreparado(sql);
                db.AgregarParametroSP("@tabla", "CATPROCRUZ", DbType.String, ParameterDirection.Input, 10);
                db.AgregarParametroSP("@opcion", "C", DbType.String, ParameterDirection.Input, 1);

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
