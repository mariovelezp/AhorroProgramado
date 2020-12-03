using MiMetaBG_AccesoDatos.BaseDatos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Suenio
{
    public class MetaRequest
    {
        public string identificacion { get; set; }
        public int idSegmento { get; set; }
        public string nombreMeta { get; set; }
        public DateTime fechaCreacion { get; set; }
        public string direccionIP { get; set; }
        public string macAdress { get; set; }
        public string accion { get; set; }
        public int cuentaMultiple { get; set; }
        public int numeroSolicitud { get; set; }
        public string tipoCtaDebito { get; set; }
        public string cuentaDebito { get; set; }
        public string montoDebito { get; set; }
        public string frencuencia { get; set; }
        public string diaUnoDebito { get; set; }
        public string diaDosDebito { get; set; }
        public string montoMeta { get; set; }
        public string fechaVencimiento { get; set; }


        public MetaRequest() {
            fechaCreacion = DateTime.Today;
        }



        public DataSet MiMetaBG_GestionMeta(int Opcion, string Identificacion, int CuentaAhorro, string NombreMeta, int IdSegmento, int NumeroSolicitud, DateTime FechaCreacion, ref string CodError, ref string Descripcion)
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
    }
}
