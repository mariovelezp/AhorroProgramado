using MiMetaBG_AccesoDatos.BaseDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
{
    public class prvImagenRequest
    {
        public int Codimagen { get; set; }
        public byte[] imagen { get; set; }
        public byte[] emojin { get; set; }

        public prvImagenRequest()
        {
            imagen = null;
            emojin = null;
        }
        public DataSet MiMetaBG_GrabarImagen(int Codimagen, byte[] imagen, byte[] emoji, ref string CodError, ref string Descripcion)
        {
            DataSet ds = new DataSet();
            Error e = new Error();
            Conexion db = new Conexion();
            string sql = "VIV_MiMetaBG.crud_segmento";
            
            try
            {
                db.ConexionBD("MiMetaBG");
                db.CrearComandoPreparado(sql);
                db.AgregarParametroSP("@tabla", "CATIMAGEN", DbType.String, ParameterDirection.Input, 10);
                db.AgregarParametroSP("@Imagen", imagen, DbType.Binary, ParameterDirection.Input, 70000);
                db.AgregarParametroSP("@Imagen_Emoji", emoji, DbType.Binary, ParameterDirection.Input, 70000);

                if (Codimagen == 9999)
                {
                    db.AgregarParametroSP("@opcion", "I", DbType.String, ParameterDirection.Input, 1);
                }
                else
                {
                    db.AgregarParametroSP("@opcion", "M", DbType.String, ParameterDirection.Input, 1);
                    db.AgregarParametroSP("@Codimagen", Codimagen, DbType.Int32, ParameterDirection.Input, 20);
                }

                ds = db.EjecutarConsultaDataSet();
                CodError = "0000";
                Descripcion = "Ok-Exitoso";

                return ds;
            }
            catch (Exception ex_segmento)
            {
                CodError = "9999";
                Descripcion = "Error Inesperado - " + ex_segmento.Message;
                return ds;
            }
            finally
            {
                db.Desconectar();
            }
        }
    }
}
