using MiMetaBG_AccesoDatos.BaseDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Meta
{
    public class ConsultaMetasRequest
    {       
        public string tokenValidacion { get; set; }
        public int idSegmento { get; set; }
        public string ip { get; set; }
        public string aplicacion { get; set; }
        public string idPantalla { get; set; }

        public DataSet MiMetaBG_Consulta_Solicitud(int Opcion, string Identificacion, int CuentaAhorro, string NombreMeta, int IdSegmento, int NumeroSolicitud, DateTime FechaCreacion, ref string CodError, ref string Descripcion)
        {
            DataSet ds = new DataSet();
            Error e = new Error();
            Conexion db = new Conexion();
            string sql = "VIV_MiMetaBG.Mantenimiento_Metas";

            
            try
            {

                db.ConexionBD("MiMetaBG");
                db.CrearComandoPreparado(sql);
                db.AgregarParametroSP("@Opcion", Opcion, DbType.Int32, ParameterDirection.Input, 20);
                db.AgregarParametroSP("@cedula_clte", Identificacion, DbType.String, ParameterDirection.Input, 13);
                db.AgregarParametroSP("@Numero_cta", CuentaAhorro, DbType.Int32, ParameterDirection.Input, 20);
                db.AgregarParametroSP("@Nombre_Meta", NombreMeta, DbType.String, ParameterDirection.Input, 150);
                db.AgregarParametroSP("@IdSegmento", IdSegmento, DbType.Int32, ParameterDirection.Input, 1);
                db.AgregarParametroSP("@NumeroSolicitud", NumeroSolicitud, DbType.Int32, ParameterDirection.Input, 20);
                db.AgregarParametroSP("@FechaCreacion", FechaCreacion, DbType.DateTime, ParameterDirection.Input, 20);

                ds = db.EjecutarConsultaDataSet();
                CodError = "0000";
                Descripcion = "Ok-Exitoso";

                return ds;
            }
            catch (Exception ex_Meta)
            {
                CodError = "9999";
                Descripcion = "Error Inesperado - " + ex_Meta.Message;
                return ds;
            }
            finally
            {
                db.Desconectar();
            }
        }


        public MetasCreadas ConsultaSolicitudI(DataSet dsListaImagenes, string NumSolicitud, ref string codigoError, ref string Descripcion)
        {
            DataTable dt = new DataTable();
            List<MetasCreadas> listaMetas = new List<MetasCreadas>();
            MetasCreadas response = new MetasCreadas();

            try
            {
                if (dsListaImagenes != null && dsListaImagenes.Tables[1] != null)
                {
                    dt = dsListaImagenes.Tables[0];
                    if (dt.Rows.Count != 0)
                    {
                        foreach (DataRow data in dt.Rows)
                        {

                            if (Convert.ToInt32(data[2]) == Convert.ToInt32(NumSolicitud))
                            {
                                response.identificacion = Convert.ToString(data[0]);
                                response.nombreMeta = Convert.ToString(data[1]);
                                response.numeroSolicitud = Convert.ToInt32(data[2]);
                                response.segmentoId = Convert.ToInt32(data[3]);
                                response.emojid = (byte[])(data[4]);
                            }
                        }
                    }
                }
                else
                {
                    codigoError = "9999";
                    Descripcion = "Lista Vacia";
                }
            }
            catch (Exception ex)
            {
                codigoError = "9999";
                Descripcion = "Error Inesperado -" + ex.Message;
            }
            return response;
        }


        public MetasCreadas ConsultasMetasOtros(DataSet dsListaImagenes, ref string codigoError, ref string Descripcion)
        {
            MetasCreadas response = new MetasCreadas();
            DataTable dt = new DataTable();
            try
            {
                if (dsListaImagenes != null)
                {

                    //response.identificacion = dsListaImagenes.Tables[0].Rows[0][0].ToString();
                    response.nombreMeta = dsListaImagenes.Tables[1].Rows[0][1].ToString();
                    //response.numeroSolicitud = Convert.ToInt32(dsListaImagenes.Tables[0].Rows[0][2]);
                    response.segmentoId = Convert.ToInt32(dsListaImagenes.Tables[1].Rows[0][3]);
                    response.emojid = (byte[])(dsListaImagenes.Tables[1].Rows[0][4]);
                }
                else
                {
                    codigoError = "9999";
                    Descripcion = "Lista Vacia";
                }
            }
            catch (Exception ex)
            {
                codigoError = "9999";
                Descripcion = "Error Inesperado -" + ex.Message;
            }
            return response;
        }




        public string FormatoFecha(string fechaHost)
        {
            string FechaFormato = string.Empty;
            string mes = string.Empty;
            string fechaVencimiento = Convert.ToDateTime(fechaHost).ToString("dd/MM/yyyy");

            string[] lista = new string[0];
            lista = fechaVencimiento.Split('/');

            switch (lista[1])
            {
                case "01":
                    mes = "Enero";
                    break;
                case "02":
                    mes = "Febrero";
                    break;
                case "03":
                    mes = "Marzo";
                    break;
                case "04":
                    mes = "Abril";
                    break;
                case "05":
                    mes = "Mayo";
                    break;
                case "06":
                    mes = "Junio";
                    break;
                case "07":
                    mes = "Julio";
                    break;
                case "08":
                    mes = "Agosto";
                    break;
                case "09":
                    mes = "Septiembre";
                    break;
                case "10":
                    mes = "Octubre";
                    break;
                case "11":
                    mes = "Noviembre";
                    break;
                default:
                    mes = "Diciembre";
                    break;
            }

            FechaFormato = lista[0] + "/" + mes + "/" + lista[2];

            return FechaFormato;
        }

        public DataSet MiMetaBG_Consultar_Ventas_Cruzadas(int Opcion, string Identificacion, int cnt_ventas_Cruzadas, ref string CodError, ref string Descripcion)
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
                CodError = "0000";
                Descripcion = "Ok-Exitoso";

                return ds;
            }
            catch (Exception ex_Meta)
            {
                CodError = "9999";
                Descripcion = "Error Inesperado - " + ex_Meta.Message;
                return ds;
            }
            finally
            {
                db.Desconectar();
            }
        }
    }


    public class MetasCreadas
    {
        public string identificacion { get; set; }
        public string nombreMeta { get; set; }
        public int numeroSolicitud { get; set; }
        public int segmentoId { get; set; }
        public byte[] emojid { get; set; }
    }
}