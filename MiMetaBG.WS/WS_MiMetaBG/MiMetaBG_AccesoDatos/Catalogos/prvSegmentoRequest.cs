using MiMetaBG_AccesoDatos.BaseDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
{
    public class prvSegmentoRequest
    {
        public int    idSegmento { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string codimagen { get; set; }
        public string umbral { get; set; }
        public string estado { get; set; }
        public string prod_cruz { get; set; }
        public string prioridad { get; set; }
        public string mensaje_seg { get; set; }
        public string cod_host { get; set; }

        public prvSegmentoRequest()
        {
            nombre = "";
            descripcion = "";

            codimagen = "0";
            umbral = "0";
            estado = "A";
            prod_cruz = "0";
            prioridad = "1";
            mensaje_seg = "";
       }
       public DataSet MiMetaBG_GrabarSegmento(int IdSegmento, string nombre_segmento, string descripcion_segmento, string codigo_imagen, string porcentaje_umbral, string estado, string codigo_prod_cruzar, string prioridad_presentacion, string msj_segmento, string cod_host, ref string CodError, ref string Descripcion)
       {
            DataSet ds = new DataSet();
            Error e = new Error();
            Conexion db = new Conexion();
            string sql = "VIV_MiMetaBG.crud_segmento";

            try
            {
                db.ConexionBD("MiMetaBG");
                db.CrearComandoPreparado(sql);
                db.AgregarParametroSP("@tabla", "CATSEGMENT", DbType.String, ParameterDirection.Input, 10);
                db.AgregarParametroSP("@nombre_segmento", nombre_segmento, DbType.String, ParameterDirection.Input, 50);
                db.AgregarParametroSP("@descripcion_segmento", descripcion_segmento, DbType.String, ParameterDirection.Input, 250);
                db.AgregarParametroSP("@codigo_imagen", Int32.Parse(codigo_imagen), DbType.Int32, ParameterDirection.Input, 20);
                db.AgregarParametroSP("@porcentaje_umbral", Convert.ToDecimal(porcentaje_umbral), DbType.Decimal, ParameterDirection.Input, 20);
                db.AgregarParametroSP("@estado", estado, DbType.String, ParameterDirection.Input, 1);
                db.AgregarParametroSP("@codigo_prod_cruzar", Int32.Parse(codigo_prod_cruzar), DbType.Int32, ParameterDirection.Input, 20);
                db.AgregarParametroSP("@prioridad_presentacion", Int32.Parse(prioridad_presentacion), DbType.Int32, ParameterDirection.Input, 20);
                db.AgregarParametroSP("@msj_segmento", msj_segmento, DbType.String, ParameterDirection.Input, 250);
                db.AgregarParametroSP("@cod_host", cod_host, DbType.String, ParameterDirection.Input, 2);

                if (IdSegmento == 9999) {
                   db.AgregarParametroSP("@opcion", "I", DbType.String, ParameterDirection.Input, 1);
                }
                else { 
                   db.AgregarParametroSP("@opcion", "M", DbType.String, ParameterDirection.Input, 1);
                   db.AgregarParametroSP("@idSegmento", IdSegmento, DbType.Int32, ParameterDirection.Input, 20);
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
