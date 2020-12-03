using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.BaseDatos
{
    public class Conexion
    {

        private DbConnection conexion = null;
        private DbCommand comando = null;
        private DbDataAdapter adapter = null;
        private DbTransaction transaccion = null;
        string cadenaConexion = "";
        private static DbProviderFactory factory = null;

        public void ConexionBD(string Base)
        {
            try
            {
                if (Base == "MiMetaBG")
                {
                    cadenaConexion = ConfigurationManager.ConnectionStrings["ConexionMiMetaBG"].ConnectionString;
                    factory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["ConexionMiMetaBG"].ProviderName);
                }
                


                if (this.conexion != null && !this.conexion.State.Equals(ConnectionState.Closed))
                {
                    throw new System.Exception("La conexión ya se encuentra abierta.");
                }
                try
                {
                    if (this.conexion == null)
                    {
                        this.conexion = factory.CreateConnection();
                        this.conexion.ConnectionString = cadenaConexion;

                    }
                    this.conexion.Open();
                }
                catch (DataException ex)
                {
                    throw new System.Exception("Error al conectarse a la base de datos.", ex);
                }

            }
            catch (ConfigurationException ex)
            {
                throw new System.Exception("Error al cargar la configuración del acceso a datos.", ex);
            }
        }

        public void CrearComandoPreparado(string sentenciaSQL)
        {
            this.comando = factory.CreateCommand();
            this.comando.Connection = this.conexion;
            this.comando.CommandType = CommandType.StoredProcedure;
            this.comando.CommandText = sentenciaSQL;
            if (this.transaccion != null)
            {
                this.comando.Transaction = this.transaccion;
            }
        }

        public void CrearSentenciaSql(string sentenciaSQL)
        {
            this.comando = factory.CreateCommand();
            this.comando.Connection = this.conexion;
            this.comando.CommandType = CommandType.Text;
            this.comando.CommandText = sentenciaSQL;
            if (this.transaccion != null)
            {
                this.comando.Transaction = this.transaccion;
            }
        }


        public void AgregarParametroSP(string nombre, object valor, DbType tipo, ParameterDirection direccion)
        {
            DbParameter parametro = comando.CreateParameter();
            parametro.Direction = direccion;
            parametro.ParameterName = nombre;
            if (direccion != ParameterDirection.Output)
            {
                parametro.Value = valor;
            }
            parametro.DbType = tipo;
            this.comando.Parameters.Add(parametro);
        }

        public void AgregarParametroSP(string nombre, object valor, DbType tipo, ParameterDirection direccion, int tamanio)
        {
            DbParameter parametro = comando.CreateParameter();
            parametro.Direction = direccion;
            parametro.ParameterName = nombre;
            if (direccion != ParameterDirection.Output)
            {
                parametro.Value = valor;
            }
            parametro.DbType = tipo;
            parametro.Size = tamanio;
            this.comando.Parameters.Add(parametro);
        }

        public void EjecutarComando()
        {
            this.comando.ExecuteNonQuery();
        }

        public object RetornaParametroSP(string nombre)
        {
            return this.comando.Parameters[nombre].Value;
        }

        public void Desconectar()
        {
            if (this.conexion.State.Equals(ConnectionState.Open))
            {
                this.conexion.Close();
            }
        }

        public DataSet EjecutarConsultaDataSet()
        {
            adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = this.comando;
            DataSet ds = new DataSet();
            adapter.Fill(ds);
            return ds;
        }

        public DataTable EjecutarConsultaDataTable()
        {
            adapter = factory.CreateDataAdapter();
            adapter.SelectCommand = this.comando;
            DataTable dt = new DataTable();
            //NpgsqlDataAdapter adap = new NpgsqlDataAdapter(com);

            return dt;
        }


        /*jti: ConexionesAbiertas revision 12/10/2017*/
        public string conexionActiva()
        {
            return this.conexion.State.ToString();
        }

    }
}
