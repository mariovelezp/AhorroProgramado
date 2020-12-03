using MiMetaBG_AccesoDatos.BaseDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
{
    public class SegmentoRequest
    {
        public int MarcaImagen { get; set; }
        public string Ip { get; set; }
        public string Aplicacion { get; set; }
        public string idPantalla { get; set; }


        public SegmentoRequest() {
            MarcaImagen = 0;
            Ip = "";
            Aplicacion = "";
            idPantalla = "";
        }



        public DataSet MiMetaBG_consulta_Segmento(int Id, ref string CodError, ref string Descripcion)
        {
            DataSet ds = new DataSet();
            Error e = new Error();
            Conexion db = new Conexion();
            string sql = "VIV_MiMetaBG.Obtener_Cat_Segmento";

            try
            {
                db.ConexionBD("MiMetaBG");
                db.CrearComandoPreparado(sql);
                db.AgregarParametroSP("@MarcaSegmento", Id, DbType.Int16, ParameterDirection.Input, 20);
                db.AgregarParametroSP("@idSegmento", 0, DbType.Int16, ParameterDirection.Input, 20);
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
