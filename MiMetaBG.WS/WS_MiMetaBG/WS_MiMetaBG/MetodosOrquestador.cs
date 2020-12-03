using Jose;
using MiMetaBG_AccesoDatos;
using MiMetaBG_AccesoDatos.Catalogos;
using MiMetaBG_AccesoDatos.Cliente;
using MiMetaBG_AccesoDatos.Meta;
using MiMetaBG_AccesoDatos.Token;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace WS_MiMetaBG
{
    public class MetodosOrquestador
    {
        //Lista de  Segmento
        public List<SegmentoBG> ListaSegmento(int MarcaImagen, ref Error error)
        {
            //Variables Globales
            List<SegmentoBG> listaSegmento = new List<SegmentoBG>();
            DataSet dsSegmento = new DataSet();
            SegmentoRequest request = new SegmentoRequest();
            DataTable dt = new DataTable();
            string Error = string.Empty;
            string Descripcion = string.Empty;

            try
            {
                dsSegmento = request.MiMetaBG_consulta_Segmento(MarcaImagen, ref Error, ref Descripcion);

                if ( Error == "0000" || dsSegmento != null)
                {
                    dt = dsSegmento.Tables[0];

                    if (dt.Rows.Count != 0)
                    {
                        foreach (DataRow data in dt.Rows)
                        {
                            SegmentoBG segmento = new SegmentoBG()
                            {
                                idSegmento = Convert.ToInt32(data[0]),
                                nombre = Convert.ToString(data[1]),
                                descripcion = Convert.ToString(data[2]),
                                imagen = (byte[])(data[3]),
                                emojin = (byte[])(data[4])
                            };
                            listaSegmento.Add(segmento);
                        }
                        error.CodigoError = Error;
                        error.DescripcionError = Descripcion;
                    }
                    else
                    {
                        error.CodigoError = "9999";
                        error.DescripcionError = "Lista Vacia";
                    }
                }
                else
                {
                    error.CodigoError = Error;
                    error.DescripcionError = "Lista null: " + Descripcion;
                }
            }
            catch (Exception ex)
            {
                error.CodigoError = "9999";
                error.DescripcionError = "Error Inesperado - " + ex.Message;
            }
            return listaSegmento;
        }

        public string ConsultarMicroServicio(string url, Dictionary<string, string> parameters, ref Error error)
        {
            string respuesta = string.Empty;
            try
            {
                string parametrosConcatenados = ConcatParams(parameters);
                string urlConParametros = url + "?" + parametrosConcatenados;

                System.Net.WebRequest wr = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(urlConParametros);
                wr.Method = "GET";
                wr.ContentType = "application/x-www-form-urlencoded";
                System.IO.Stream newStream;
                System.Net.WebResponse response = wr.GetResponse();
                newStream = response.GetResponseStream();
                System.IO.StreamReader reader = new System.IO.StreamReader(newStream);
                respuesta = reader.ReadToEnd();

                reader.Close();
                newStream.Close();
                response.Close();
                error.CodigoError = "0000";
                error.DescripcionError = "Ok-Exitoso";
                return respuesta;
            }
            catch(WebException e)
            {
                error.CodigoError = "9999";
                error.DescripcionError = e.Message;

                var respuestaException = new StreamReader(e.Response.GetResponseStream()).ReadToEnd();
                dynamic objException = JsonConvert.DeserializeObject(respuestaException);

                if (objException.error.status == "400")
                {
                    error.CodigoError = objException.error.errorCode;
                    error.DescripcionError = objException.error.userMessage;
                }
                return respuesta;
            }
        }

        public string ConcatParams(Dictionary<string, string> parameters)
        {
            bool FirstParam = true;
            StringBuilder Parametros = null;

            if (parameters != null)
            {
                Parametros = new StringBuilder();
                foreach (KeyValuePair<string, string> param in parameters)
                {
                    Parametros.Append(FirstParam ? "" : "&");
                    Parametros.Append(param.Key + "=" + System.Web.HttpUtility.UrlEncode(param.Value));
                    FirstParam = false;
                }
            }
            return Parametros == null ? string.Empty : Parametros.ToString();
        }

        public string EnmascararCorreo(string correoElectronico)
        {
            string CorreoEnmascarado = string.Empty;
            CorreoEnmascarado = correoElectronico.ToLower();
            string[] divisionCorreo = new string[0];
            divisionCorreo = CorreoEnmascarado.Split('@');
            int inicioCorreo = 2;
            int finalCorreo = 3;
            int longitud;

            if (divisionCorreo[0].Length > inicioCorreo + finalCorreo)
                longitud = divisionCorreo[0].Length - finalCorreo - inicioCorreo;
            else
                longitud = 1;

            divisionCorreo[0] = divisionCorreo[0].Remove(inicioCorreo, longitud).Insert(inicioCorreo, new string('*', longitud));
            CorreoEnmascarado = String.Join("@", divisionCorreo);

            return CorreoEnmascarado;
        }

        public string ClienteObtener(string cliente)
        {
            string NombreCliente = string.Empty;
            string[] divisionNombre = new string[0];
            divisionNombre = cliente.Split(' ');
            string nombre = string.Empty;

            if (divisionNombre[2] != "")
            {
                nombre = divisionNombre[2];
                string nombre_tmp = nombre.ToUpper();
                nombre = nombre.ToLower();
                nombre = nombre.Replace(nombre[0], nombre_tmp[0]); 
            }
            else
                nombre = "";
                    
            return nombre;
        }

        public List<ImagSegmento> ListaImagenes(int MarcaImagen, int idSegmento, ref string Mensaje, ref Error error)
        {
            List<ImagSegmento> lista = new List<ImagSegmento>();

            DataSet dsImagenes = new DataSet();
            DataTable dt = new DataTable();
            ParametroRequest request = new ParametroRequest();
            string Error = string.Empty;
            string Descripcion = string.Empty;

            try
            {
                dsImagenes = request.MiMetaBG_Emoji_Segmento(MarcaImagen, idSegmento, ref Error, ref Descripcion);

                if (Error == "0000" && dsImagenes != null)
                {
                    dt = dsImagenes.Tables[0];
                    if (dt.Rows.Count != 0)
                    {
                        foreach (DataRow data in dt.Rows)
                        {
                            ImagSegmento segmento = new ImagSegmento()
                            {
                                segmentoId = Convert.ToInt32(data[0]),
                                emojid = (byte[])(data[1])
                            };
                            lista.Add(segmento);
                        }
                        Mensaje = dsImagenes.Tables[1].Rows[0][0].ToString();
                        error.CodigoError = Error;
                        error.DescripcionError = Descripcion;
                    }
                    else
                    {
                        error.CodigoError = "9999";
                        error.DescripcionError = "Lista Vacia";
                    }
                }
                else
                {
                    error.CodigoError = Error;
                    error.DescripcionError = Descripcion;
                }
            }
            catch (Exception ex)
            {
                error.CodigoError = "9999";
                error.DescripcionError = "Error Inesperado - " + ex.Message;
            }
            return lista;
        }

        public string Decrypt_AES(string decryptedText, string stringKey, string stringIv)
        {
            ValidacionClienteRequest metodos = new ValidacionClienteRequest(); 
            try
            {
                byte[] bytesToBeDecrypted = Convert.FromBase64String(decryptedText);
                byte[] Key = System.Security.Cryptography.SHA256.Create().ComputeHash(metodos.GetBytes(stringKey));
                byte[] Iv = SHA256.Create().ComputeHash(metodos.GetBytes(stringIv));
                byte[] decryptedBytes = metodos.AES_Decrypt(bytesToBeDecrypted, Key, Iv);
                int saltSizeKey = metodos.GetSaltSize(Key);
                int saltSizeIv = metodos.GetSaltSize(Iv);
                byte[] originalBytes = new byte[decryptedBytes.Length - saltSizeKey];
                for (int i = saltSizeKey; i < decryptedBytes.Length; i++)
                {
                    originalBytes[i - saltSizeKey] = decryptedBytes[i];
                }
                return Encoding.UTF8.GetString(originalBytes);
            }
            catch (Exception e)
            {
                throw new Exception("Decrypt_AES: " + e.ToString());
            }
        }

        public byte[] Encrypt_AES(string text, string stringKey, string stringIv)
        {
            ValidacionClienteOTPRequest metodosRequest = new ValidacionClienteOTPRequest();

            byte[] originalBytes = Encoding.UTF8.GetBytes(text);
            byte[] encryptedBytes = null;
            byte[] Key =
            System.Security.Cryptography.SHA256.Create().ComputeHash(metodosRequest.GetBytes(stringKey));
            byte[] Iv = SHA256.Create().ComputeHash(metodosRequest.GetBytes(stringIv));
            int saltSizeKey = metodosRequest.GetSaltSize(Key);
            int saltSizeIv = metodosRequest.GetSaltSize(Iv);
            byte[] saltBytesKey = metodosRequest.GetRandomBytes(saltSizeKey);
            byte[] saltBytesIv = metodosRequest.GetRandomBytes(saltSizeIv);
            byte[] bytesToBeEncrypted = new byte[saltBytesKey.Length +
            originalBytes.Length];
            for (int i = 0; i < saltBytesKey.Length; i++)
            {
                bytesToBeEncrypted[i] = saltBytesKey[i];
            }
            for (int i = 0; i < originalBytes.Length; i++)
            {
                bytesToBeEncrypted[i + saltBytesKey.Length] = originalBytes[i];
            }
            encryptedBytes = metodosRequest.AES_Encrypt(bytesToBeEncrypted, Key, Iv);
            return encryptedBytes;
        }

        public void RegistrarMetas(string Identificacion, int CuentaAhorro, string NombreMeta, int IdSegmento, int NumeroSolicitud, DateTime FechaCreacion, ref Error e) 
        {
            DataSet ds = new DataSet();
            MetaRequest request = new MetaRequest();
            string Error = string.Empty;
            string Descripcion = string.Empty; 
            int Opcion = 0;
            try
            {
                Opcion = 1;
                ds = request.MiMetaBG_GestionMeta(Opcion, Identificacion, CuentaAhorro, NombreMeta, IdSegmento, NumeroSolicitud, FechaCreacion, ref Error, ref Descripcion);
                e.CodigoError = Error;
                e.DescripcionError = Descripcion;

                if (e.CodigoError == "0000") 
                {
                    if (ds != null) 
                    {
                        e.CodigoError = ds.Tables[0].Rows[0][0].ToString();
                        e.DescripcionError = ds.Tables[0].Rows[0][1].ToString();
                    }
                }
            }
            catch (Exception ex) 
            {
                e.CodigoError = "9999";
                e.DescripcionError = "Error Inesperado - " + ex.Message;
            }
        }

        public CuentasResponse ConsultarCuentas(string jsonParametros) 
        {
            CuentasResponse response = new CuentasResponse();
            dynamic objRespuesta = JsonConvert.DeserializeObject(jsonParametros);
            List<ObtenerUna> ListaUna = new List<ObtenerUna>();
            List<ObtenerTodas> listaTodos = new List<ObtenerTodas>();
            string cuenta = string.Empty;
            string todos = string.Empty;
            string descripcion = string.Empty;

            try
            {
                if (objRespuesta.data != null)
                {
                    foreach (var recorrer in objRespuesta.data)
                    {
                        if (recorrer.estado == "0" && recorrer.tipo == "A")
                        {
                            descripcion = "Cta. Ahorros - ";
                            cuenta = recorrer.numero;
                            cuenta = EnmascararCuentas(cuenta);
                            ObtenerUna una = new ObtenerUna()
                            {
                                cta = recorrer.numero,
                                ctaEnm = descripcion + cuenta,
                                tipo = recorrer.tipo
                            };
                            ListaUna.Add(una);
                        }
                    }
                    response.ListaUna = ListaUna;

                    foreach (var recorrer in objRespuesta.data)
                    {
                        if (recorrer.estado == "0")
                        {
                            if (recorrer.tipo == "A")
                                descripcion = "Cta. Ahorros - ";
                            else
                                descripcion = "Cta. Corriente - ";

                            cuenta = recorrer.numero;
                            cuenta = EnmascararCuentas(cuenta);
                            ObtenerTodas todo = new ObtenerTodas()
                            {
                                cta = recorrer.numero,
                                ctaEnm = descripcion + cuenta,
                                tipo = recorrer.tipo
                            };
                            listaTodos.Add(todo);
                        }
                    }
                    response.ListaTodas = listaTodos;

                    response.codigoRetorno = "0000";
                    response.mensajeRetorno = "Ok-Exitoso";
                }
                else {
                    response.codigoRetorno = "4444";
                    response.mensajeRetorno = "Consulta fue reclazada";
                }
            }
            catch(Exception ex)
            {
                response.codigoRetorno = "9999";
                response.mensajeRetorno = ex.Message;
            }
            return response;
        }

        public string EnmascararCuentas(string ObtenerCuentas)
        {
            int inicio = 2;
            int final = 2;
            int longitud;
            string CtasAhorroCorriente = string.Empty;

            if (ObtenerCuentas.Length > inicio + final)
                longitud = ObtenerCuentas.Length - final - inicio;
            else
                longitud = 1;

            CtasAhorroCorriente = ObtenerCuentas.Remove(inicio, longitud).Insert(inicio, new string('X', longitud));
            return CtasAhorroCorriente;
        }

        public string FormatearCuentas(string ObtenerCuentas)
        {
            string CtasAhorroCorriente = string.Empty;

            int contador = ObtenerCuentas.Length;
            int restante = 10 - contador;

            switch (restante) 
            { 
                case 1:
                    CtasAhorroCorriente = "0" + ObtenerCuentas;
                    break;
                case 2:
                    CtasAhorroCorriente = "00" + ObtenerCuentas;
                    break;
                case 3:
                    CtasAhorroCorriente = "000" + ObtenerCuentas;
                    break;
                case 4:
                    CtasAhorroCorriente = "0000" + ObtenerCuentas;
                    break;
                case 5:
                    CtasAhorroCorriente = "00000" + ObtenerCuentas;
                    break;
                case 6:
                    CtasAhorroCorriente = "000000" + ObtenerCuentas;
                    break;
                case 7:
                    CtasAhorroCorriente = "0000000" + ObtenerCuentas;
                    break;
                case 8:
                    CtasAhorroCorriente = "00000000" + ObtenerCuentas;
                    break;
                default:
                    CtasAhorroCorriente = "000000000" + ObtenerCuentas;
                    break;
            }

            return CtasAhorroCorriente;
        }
        
        public void ListaEmojin(int idSegmento, ref int idSegmentoOUT, ref byte[] ImagenOUT, ref Error error)
        {
            DataSet dsImagenes = new DataSet();
            DataTable dt = new DataTable();
            ParametroRequest request = new ParametroRequest();
            string Error = string.Empty;
            string Descripcion = string.Empty;

            try
            {
                dsImagenes = request.MiMetaBG_Emoji_Segmento(4, idSegmento, ref Error, ref Descripcion);

                if (Error == "0000" && dsImagenes != null)
                {
                    idSegmentoOUT = Convert.ToInt32(dsImagenes.Tables[0].Rows[0][0].ToString());
                    ImagenOUT = (byte[])(dsImagenes.Tables[0].Rows[0][1]);
                    error.CodigoError = "0000";
                    error.DescripcionError = "Ok-Exitoso";
                }
                else
                {
                    error.CodigoError = Error;
                    error.DescripcionError = Descripcion;
                }
            }
            catch (Exception ex)
            {
                error.CodigoError = "9999";
                error.DescripcionError = "Error Inesperado - " + ex.Message;
            }
        }

        public List<ListaMetas> ListaMasivaMetas(string jsonConsultaMasiva, string Identificacion, ref Error e)
        {
            Error error = new Error();
            string codigoError = string.Empty;
            string Descripcion = string.Empty;
            string NumeroSolicitud = string.Empty;
            string montoMetaDebito = string.Empty;
            string fechaMetaODS = string.Empty;
            string fechaInicioODS = string.Empty;
            string fechaVencimiento = string.Empty;
            string cuotaMensual = string.Empty;
            string montoMetaForm = string.Empty;
            string montoDebitoAcumulado = string.Empty;
            string cuentaEnmascarada = string.Empty;
            int porcentaje = 0;
            int montoPorcentaje = 0;
            double montoMetas = 0;
            string frecuenciaODS = string.Empty;
            DataSet dsListaImagenes = new DataSet();
            DataSet dsListaOtros = new DataSet();
            
            ConsultaMetasRequest metodosRequest = new ConsultaMetasRequest();
            List<ListaMetas> listaMasiva = new List<ListaMetas>();
            dynamic objRespuesta = JsonConvert.DeserializeObject(jsonConsultaMasiva);

            DateTime fechaActual = DateTime.Now;
            dsListaImagenes = metodosRequest.MiMetaBG_Consulta_Solicitud(2, Identificacion, 0, "", 0, 0, fechaActual, ref codigoError, ref Descripcion);
            

            MetasCreadas request = new MetasCreadas();
            List<MetasCreadas> listaMetas = new List<MetasCreadas>();

            try
            {
                if (objRespuesta.count != 0)
                {
                    foreach (var recorrer in objRespuesta.data)
                    {
                        if (recorrer.estadoSolicitud == "A" && recorrer.marcaPrecancelado != "P")
                        {
                            NumeroSolicitud = recorrer.solicitud;
                            request = metodosRequest.ConsultaSolicitudI(dsListaImagenes, NumeroSolicitud, ref codigoError, ref Descripcion);

                            if (request.identificacion == null) 
                            {
                                request = metodosRequest.ConsultasMetasOtros(dsListaImagenes, ref codigoError, ref Descripcion);
                            }
                            frecuenciaODS = recorrer.frecuenciaDebito;
                            montoDebitoAcumulado = recorrer.valorDebitosAcumulados;
                            montoMetaDebito = recorrer.montoDebito;
                            fechaMetaODS = recorrer.fechaMeta;
                            fechaInicioODS = Convert.ToString(recorrer.fechaEmision);

                            if (recorrer.frecuenciaDebito == "M")
                                cuotaMensual = "$ " + recorrer.montoDebito  + " mensual";
                            else
                                cuotaMensual = "$ " + recorrer.montoDebito + " quincenal";

                            int DebitoAcumulado = Convert.ToInt32(Math.Round(Convert.ToDouble(montoDebitoAcumulado)));

                            if (recorrer.montoMeta == "0")
                            {
                                montoMetas = CalcularMontoMeta(montoMetaDebito, montoDebitoAcumulado, fechaMetaODS, frecuenciaODS);
                                montoPorcentaje = CalcularPorcentaje(montoMetaDebito, fechaInicioODS, fechaMetaODS, frecuenciaODS); 
                                fechaVencimiento = metodosRequest.FormatoFecha(fechaMetaODS);

                                if (montoPorcentaje > 0)
                                    porcentaje = (DebitoAcumulado * 100) / montoPorcentaje;
                                else
                                    porcentaje = 0;
                            }
                            else
                            {
                                montoMetas = recorrer.montoMeta;
                                fechaMetaODS = "";
                                fechaVencimiento = "";

                                int montoMetaPorcentaje = Convert.ToInt32(Math.Round(Convert.ToDouble(montoMetas)));
                                porcentaje = (DebitoAcumulado * 100) / montoMetaPorcentaje;
                            }

                            montoMetaForm = string.Format("{0:###,###,###,##0.00##}", montoMetas);

                            cuentaEnmascarada = recorrer.cuentaDebito;
                            cuentaEnmascarada = EnmascararCuentas(cuentaEnmascarada);

                            ListaMetas metas = new ListaMetas()
                            {
                                nombreMeta = request.nombreMeta,
                                cuentaDebito = recorrer.cuentaDebito,
                                cuenta = cuentaEnmascarada,
                                tipoCuentaDebito = recorrer.tipoCuentaDebito,
                                frecuenciaDebito = recorrer.frecuenciaDebito,
                                diaDebito1 = recorrer.diaDebito1,
                                diaDebito2 = recorrer.diaDebito2,
                                valorDebitosAcumulados = recorrer.valorDebitosAcumulados,
                                montoMeta = recorrer.montoMeta,
                                fechaEmision = fechaInicioODS,
                                montoMetaform = montoMetaForm,
                                numeroSolicitud = recorrer.solicitud,
                                cuotaMeta = recorrer.montoDebito,
                                cuotaMetaform = cuotaMensual,
                                plazoMeta = fechaMetaODS,
                                plazoMetaform = fechaVencimiento,
                                cuentaAhorro = recorrer.cuentaSolicitud,
                                idSegmento = request.segmentoId,
                                emojid = request.emojid,
                                porcentaje = porcentaje
                            };
                            listaMasiva.Add(metas);
                        }
                    }
                    e.CodigoError = codigoError;
                    e.DescripcionError = Descripcion;
                }
                else 
                {
                    e.CodigoError = "8800";
                    e.DescripcionError = "Lista Vacia, no posee sueños ";
                }
            }
            catch (Exception ex)
            {
                e.CodigoError = "9999";
                e.DescripcionError = "Error Inesperado - " + ex.Message;
            }
           return listaMasiva;
        }

        public double CalcularMontoMeta(string MontoDebitoODS, string montoAcumulado, string fechaFinal, string Frecuencia) 
        {
            int diasMeta = 0;
            double calculo = 0;
            string montoFinal = string.Empty;
            DateTime FechaInicioMeta = DateTime.Now;
            DateTime FechaFinalMeta = Convert.ToDateTime(fechaFinal);

            TimeSpan ts = FechaFinalMeta - FechaInicioMeta;

            // Difference in days.
            diasMeta = 1 + ts.Days;

            if (Frecuencia == "M")
            {
                calculo = (diasMeta / 30) * double.Parse(MontoDebitoODS);
            }
            else {
                calculo = (diasMeta / 15) * double.Parse(MontoDebitoODS);
            }

            calculo = calculo + double.Parse(montoAcumulado);
            return calculo;
        }

        public int CalcularPorcentaje(string MontoDebitoODS, string FechaInicioODS, string fechaFinal, string Frecuencia)
        {
            int diasMeta = 0;
            int calculo = 0;
            string montoFinal = string.Empty;
            DateTime FechaInicioMeta = Convert.ToDateTime(FechaInicioODS);
            DateTime FechaFinalMeta = Convert.ToDateTime(fechaFinal);
            TimeSpan ts = FechaFinalMeta - FechaInicioMeta;

            // Difference in da .
            diasMeta = 1 + ts.Days;

            int montoDebito = Convert.ToInt32(Math.Round(Convert.ToDouble(MontoDebitoODS)));
            if (Frecuencia == "M")
            {
                calculo = (diasMeta / 30) * Convert.ToInt32(montoDebito);
            }
            else
            {
                calculo = (diasMeta / 15) * Convert.ToInt32(montoDebito);
            }

            //montoFinal = string.Format("{0:###,###,###,##0.00##}", Convert.ToDouble(calculo));
            return calculo;
        }

        public int CalcularMesesDeDiferencia(DateTime fechaDesde, DateTime fechaHasta)
        {
            return (fechaDesde.Month - fechaHasta.Month) + 360 * (fechaDesde.Year - fechaHasta.Year);
        }

        public List<ObtenerTodasEditar> ConsultarCuentasMetas(string jsonParametros, ref Error e)
        {
            List<ConsultaMetasResponse> response = new List<ConsultaMetasResponse>();
            dynamic objRespuesta = JsonConvert.DeserializeObject(jsonParametros);
            List<ObtenerTodasEditar> listaTodos = new List<ObtenerTodasEditar>();
            string cuenta = string.Empty;
            string todos = string.Empty;
            string descripcion = string.Empty;

            try
            {
                if (objRespuesta.data != null)
                {
                    foreach (var recorrer in objRespuesta.data)
                    {
                        if (recorrer.estado == "0")
                        {
                            if (recorrer.tipo == "A")
                                descripcion = "Cta. Ahorros - ";
                            else
                                descripcion = "Cta. Corriente - ";

                            cuenta = recorrer.numero;
                            cuenta = EnmascararCuentas(cuenta);
                            ObtenerTodasEditar todo = new ObtenerTodasEditar()
                            {
                                cta = recorrer.numero,
                                ctaEnm = descripcion + cuenta,
                                tipo = recorrer.tipo
                            };
                            listaTodos.Add(todo);
                        }
                    }
                    

                    e.CodigoError = "0000";
                    e.DescripcionError = "Ok-Exitoso";
                }
                else
                {
                    e.CodigoError = "4444";
                    e.DescripcionError = "Consulta fue reclazada";
                }
            }
            catch (Exception ex)
            {
                e.CodigoError = "9999";
                e.DescripcionError = ex.Message;
            }
            return listaTodos;
        }

        public List<VentasCruzadas> ConsultarVentasCruzadas(string identificacion, int CantidadMostrada, ref Error e)
        {
            //Error e = new Error();
            string Error = string.Empty;
            string Descripcion = string.Empty;
            DataTable dt = new DataTable();
            List<VentasCruzadas> response = new List<VentasCruzadas>();
            ConsultaMetasRequest metodosRequest = new ConsultaMetasRequest();
            DataSet dataset = metodosRequest.MiMetaBG_Consultar_Ventas_Cruzadas(1, identificacion, CantidadMostrada, ref Error, ref Descripcion);
            string valorFormato = string.Empty;

            if (Error == "0000" && dataset != null)
            {
                dt = dataset.Tables[0];

                if (dt.Rows.Count != 0)
                {
                    foreach (DataRow data in dt.Rows)
                    {
                        valorFormato = string.Format("{0:###,###,###,##0.00##}", data[1]);

                        VentasCruzadas segmento = new VentasCruzadas()
                        {
                            codigoCruzar = Convert.ToInt32(data[0]),
                            valorCruzar = valorFormato 
                        };
                        response.Add(segmento);
                    }
                    e.CodigoError = Error;
                    e.DescripcionError = Descripcion;
                }
                else
                {
                    response = null;
                    e.CodigoError = Error;
                    e.DescripcionError = Descripcion;
                }
            }
            else
            {
                e.CodigoError = Error;
                e.DescripcionError = Descripcion;
            }
            return response;
        }

        public string ContadorVentasCruzadas(string Identificacion, int CantidadMostradas, ref string Descripcion) 
        {
            string Error = string.Empty;
            VentasCruzadasRequest metodosRequest = new VentasCruzadasRequest();
            metodosRequest.MiMetaBG_Actualizar_Ventas_Cruzadas(2, Identificacion, CantidadMostradas, ref Error, ref Descripcion);

            return Error;
        }


        public Token ValidaToken(string token) 
        {
            Token tokensRequest = new Token();
            TokenRequest request =  new TokenRequest();
            string tokenValidado = string.Empty;
            string tIdentificacion = string.Empty;
            int valueTiempo = 0;
            int FechaFinalMeta = 0;
            int restaTiempo = 0;
            int horas = 0;
            int minutosCreacion = 0;
            int minutosActual = 0;
            int FechaActual = 0;
            int horaActual = 0;
            try
            {
                string valueSecret = ConfigurationManager.AppSettings.Get("JWT_SECRET_KEY").ToString();
                valueTiempo = Convert.ToInt32(ConfigurationManager.AppSettings.Get("JWT_EXPIRE").ToString());

                byte[] secretKey = request.Base64UrlDecode(valueSecret); //pass key to secure and decode it  
                tokenValidado = JWT.Decode(token, secretKey);

                dynamic objRespuesta = JsonConvert.DeserializeObject(tokenValidado);
                tokensRequest.identificacion = objRespuesta.identificacion;
                tokensRequest.tipoIdentificacion = objRespuesta.tipoIdentificacion;
                tokensRequest.tiempoDuracion = objRespuesta.iat;

                tIdentificacion = request.verificaTipoIdentificacion(tokensRequest.identificacion);

                if (tIdentificacion == tokensRequest.tipoIdentificacion)
                {
                    tokensRequest.codigoRetorno = "0000";
                    tokensRequest.mensajeRetorno = "ok-Exitoso";
                }
                else {
                    tokensRequest.codigoRetorno = "0100";
                    tokensRequest.mensajeRetorno = "Token no Valido";
                }

                string tiempo = DateTime.Now.ToString("HH:mm:ss");
                string[] respuesta = new string[0];

                
                DateTime FechaInicioMeta = DateTime.Now;
                //fecha genero token
                FechaFinalMeta = Convert.ToInt32(tokensRequest.tiempoDuracion);
                horas = (FechaFinalMeta / 3600);
                minutosCreacion = ((FechaFinalMeta - horas * 3600) / 60);
                
                FechaActual = (int)(FechaInicioMeta.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
                horaActual = (FechaActual / 3600);
                minutosActual = ((FechaActual - horaActual * 3600) / 60);

                restaTiempo = minutosActual - minutosCreacion;
                
                if(restaTiempo > valueTiempo)
                {
                    tokensRequest.codigoRetorno = "0100";   
                }
            }
            catch (Exception e) {
                tokensRequest.codigoRetorno = "9999";
                tokensRequest.mensajeRetorno = "Error Inesperado - " + e.Message;
            }
            return tokensRequest;
        }

        public string GenerarToken(string Identificacion, string CodigoOTP, string TipoIdentificacion, ref Error e) //function for JWT Token  
        {
            string token = string.Empty;
            try
            {
                TokenRequest request = new TokenRequest();
                string valueSecret = ConfigurationManager.AppSettings.Get("JWT_SECRET_KEY").ToString();
                byte[] secretKey = request.Base64UrlDecode(valueSecret);//pass key to secure and decode it  
                DateTime issued = DateTime.Now;
                var User = new Dictionary<string, object>()
                    {
                        {"identificacion", Identificacion},
                        {"codigoOtp", CodigoOTP},
                        {"tipoIdentificacion", TipoIdentificacion},
                        {"iat", request.ToUnixTime(issued).ToString()}  //hora segundos 
                    };
                token = JWT.Encode(User, secretKey, JwsAlgorithm.HS256);

                e.CodigoError = "0000";
                e.DescripcionError = "Ok-Exitoso";
            }
            catch(Exception ex)
            {
                e.CodigoError = "9999";
                e.DescripcionError = "Error_Token - " + ex.Message;
            }
            return token;
        }

        #region Mantenimiento de segmentos
        public void RegistrarSegmento(int IdSegmento, string nombre, string descripcion, string codimagen, string umbral, string estado, string prod_cruz, string prioridad, string mensaje_seg, string cod_host, ref Error e)
        {
            DataSet ds = new DataSet();
            prvSegmentoRequest request = new prvSegmentoRequest();
            string Error = string.Empty;
            string Descripcion = string.Empty;
            string Opcion = string.Empty;

            try
            {
                ds = request.MiMetaBG_GrabarSegmento(IdSegmento, nombre, descripcion, codimagen, umbral, estado, prod_cruz, prioridad, mensaje_seg, cod_host, ref Error, ref Descripcion);

                e.CodigoError = Error;
                e.DescripcionError = Descripcion;

                if (e.CodigoError == "0000")
                {
                    if (ds != null)
                    {
                        e.CodigoError = ds.Tables[0].Rows[0][0].ToString();
                        e.DescripcionError = ds.Tables[0].Rows[0][1].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                e.CodigoError = "9999";
                e.DescripcionError = "Error Inesperado - " + ex.Message;
            }
        }
        
        public void RegistrarImagen(int Codimagen, byte[] imagen, byte[] emoji, ref Error e)
        {
            DataSet ds = new DataSet();
            prvImagenRequest request = new prvImagenRequest();
            string Error = string.Empty;
            string Descripcion = string.Empty;
            string Opcion = string.Empty;

            try
            {
                ds = request.MiMetaBG_GrabarImagen(Codimagen, imagen, emoji, ref Error, ref Descripcion);

                e.CodigoError = Error;
                e.DescripcionError = Descripcion;

                if (e.CodigoError == "0000")
                {
                    if (ds != null)
                    {
                        e.CodigoError = ds.Tables[0].Rows[0][0].ToString();
                        e.DescripcionError = ds.Tables[0].Rows[0][1].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                e.CodigoError = "9999";
                e.DescripcionError = "Error Inesperado - " + ex.Message;
            }
        }

        public List<SegmentoGralBG> ListaSegmentoGral(int MarcaImagen, ref Error error)
        {
            //Variables Globales
            List<SegmentoGralBG> listaSegmento = new List<SegmentoGralBG>();
            DataSet dsSegmento = new DataSet();
            prvSegmentoGralRequest request = new prvSegmentoGralRequest();
            DataTable dt = new DataTable();
            string Error = string.Empty;
            string Descripcion = string.Empty;

            try
            {
                dsSegmento = request.MiMetaBG_consGral_Segmento(MarcaImagen, ref Error, ref Descripcion);

                if (Error == "0000" && dsSegmento != null)
                {
                    dt = dsSegmento.Tables[0];

                    if (dt.Rows.Count != 0)
                    {
                        foreach (DataRow data in dt.Rows)
                        {
                            SegmentoGralBG segmento = new SegmentoGralBG()
                            {
                                idSegmento = Convert.ToInt32(data[0]),
                                nombre = Convert.ToString(data[1]),
                                descripcion = Convert.ToString(data[2]),

                                imagen = (data[3] == DBNull.Value) ? null : (byte[])(data[3]),
                                emojin = (data[4] == DBNull.Value) ? null : (byte[])(data[4]),

                                codimagen = Convert.ToString(data[5]),
                                umbral = Convert.ToString(data[6]),
                                estado = Convert.ToString(data[7]),
                                prod_cruz = Convert.ToString(data[8]),
                                prioridad = Convert.ToString(data[9]),
                                mensaje_seg = Convert.ToString(data[10]),
                                cod_host = Convert.ToString(data[11])
                            };
                            listaSegmento.Add(segmento);
                        }
                        error.CodigoError = Error;
                        error.DescripcionError = Descripcion;
                    }
                    else
                    {
                        error.CodigoError = "9999";
                        error.DescripcionError = "Lista Vacia";
                    }
                }
                else
                {
                    error.CodigoError = Error;
                    error.DescripcionError = "Lista null: " + Descripcion;
                }
            }
            catch (Exception ex)
            {
                error.CodigoError = "9999";
                error.DescripcionError = "Error Inesperado - " + ex.Message;
            }
            return listaSegmento;
        }

        public List<ImagenGralBG> ListaImagenGral(int MarcaImagen, ref Error error)
        {
            //Variables Globales
            List<ImagenGralBG> listaImagen = new List<ImagenGralBG>();
            DataSet dsImagen = new DataSet();
            prvImagenGralRequest request = new prvImagenGralRequest();
            DataTable dt = new DataTable();
            string Error = string.Empty;
            string Descripcion = string.Empty;

            try
            {
                dsImagen = request.MiMetaBG_consGral_Imagen(MarcaImagen, ref Error, ref Descripcion);

                if (Error == "0000" && dsImagen != null)
                {
                    dt = dsImagen.Tables[0];

                    if (dt.Rows.Count != 0)
                    {
                        foreach (DataRow data in dt.Rows)
                        {
                            ImagenGralBG imagen = new ImagenGralBG()
                            {
                                codimagen = Convert.ToString(data[0]),
                                imagen = (data[1] == DBNull.Value) ? null : (byte[])(data[1]),
                                emojin = (data[2] == DBNull.Value) ? null : (byte[])(data[2])
                            };
                            listaImagen.Add(imagen);
                        }
                        error.CodigoError = Error;
                        error.DescripcionError = Descripcion;
                    }
                    else
                    {
                        error.CodigoError = "9999";
                        error.DescripcionError = "Lista Vacia";
                    }
                }
                else
                {
                    error.CodigoError = Error;
                    error.DescripcionError = "Lista null: " + Descripcion;
                }
            }
            catch (Exception ex)
            {
                error.CodigoError = "9999";
                error.DescripcionError = "Error Inesperado - " + ex.Message;
            }
            return listaImagen;
        }

        public List<ProductoGralBG> ListaProductoGral(int MarcaImagen, ref Error error)
        {
            //Variables Globales
            List<ProductoGralBG> listaProducto = new List<ProductoGralBG>();
            DataSet dsProducto = new DataSet();
            prvProductoGralRequest request = new prvProductoGralRequest();
            DataTable dt = new DataTable();
            string Error = string.Empty;
            string Descripcion = string.Empty;

            try
            {
                dsProducto = request.MiMetaBG_consGral_Producto(MarcaImagen, ref Error, ref Descripcion);

                if (Error == "0000" && dsProducto != null)
                {
                    dt = dsProducto.Tables[0];

                    if (dt.Rows.Count != 0)
                    {
                        foreach (DataRow data in dt.Rows)
                        {
                            ProductoGralBG producto = new ProductoGralBG()
                            {
                                idproducto = Convert.ToString(data[0]),
                                descripcionproducto = Convert.ToString(data[1])
                            };
                            listaProducto.Add(producto);
                        }
                        error.CodigoError = Error;
                        error.DescripcionError = Descripcion;
                    }
                    else
                    {
                        error.CodigoError = "9999";
                        error.DescripcionError = "Lista Vacia";
                    }
                }
                else
                {
                    error.CodigoError = Error;
                    error.DescripcionError = "Lista null: " + Descripcion;
                }
            }
            catch (Exception ex)
            {
                error.CodigoError = "9999";
                error.DescripcionError = "Error Inesperado - " + ex.Message;
            }
            return listaProducto;
        }

        public List<UsuarioBG> ListaUsuario(int Id_codigo, ref Error error)
        {
            //Variables Globales
            List<UsuarioBG> listaUsuario = new List<UsuarioBG>();
            DataSet dsUsuario = new DataSet();
            prvLoginRequest request = new prvLoginRequest();
            DataTable dt = new DataTable();
            string Error = string.Empty;
            string Descripcion = string.Empty;

            try
            {
                dsUsuario = request.MiMetaBG_consulta_usuario(Id_codigo, ref Error, ref Descripcion);


                if (Error == "0000" && dsUsuario != null)
                {
                    dt = dsUsuario.Tables[0];

                    if (dt.Rows.Count != 0)
                    {
                        foreach (DataRow data in dt.Rows)
                        {
                            UsuarioBG usuario = new UsuarioBG()
                            {
                                Aplicacion = Convert.ToString(data[0])
                            };
                            listaUsuario.Add(usuario);
                        }
                        error.CodigoError = Error;
                        error.DescripcionError = Descripcion;
                    }
                    else
                    {
                        error.CodigoError = "9999";
                        error.DescripcionError = "Lista Vacia";
                    }
                }
                else
                {
                    error.CodigoError = Error;
                    error.DescripcionError = "Lista null: " + Descripcion;
                }
            }
            catch (Exception ex)
            {
                error.CodigoError = "9999";
                error.DescripcionError = "Error Inesperado - " + ex.Message;
            }
            return listaUsuario;
        }
        #endregion Mantenimiento de segmentos

        public string EmailBody(string fullName, string cuentaDebito, string nombreMeta, string montoFinalMeta, string montoDebito, string fechaVencimiento, DateTime fechaCreacion)
        {
            string parrafo1 = string.Empty;
            string fecha = string.Empty;
            string fechaTexto = string.Empty;
            string anio = string.Empty;
            string anio2 = string.Empty;
            string mes = string.Empty;
            string mes2 = string.Empty;
            string dia = string.Empty;
            string strMes = string.Empty;
            int plazo;
            string strPlazo = string.Empty;
            string cuentaDebitoMask = string.Empty;
            string montoFinalMetaD = string.Empty;
            string montoDebitoD = string.Empty;
            string Mf = string.Empty;
            string Md = string.Empty;
            Int32 MF;
            Int32 MD;
            int PL;

            fecha = fechaCreacion.ToString("yyyy/MM/dd");
            fecha = fecha.Replace("/", "");
            anio = fecha.Substring(0, 4);
            mes = fecha.Substring(4, 2);
            dia = fecha.Substring(6, 2);

            if (mes == "01") strMes = "Enero";
            if (mes == "02") strMes = "Febrero";
            if (mes == "03") strMes = "Marzo";
            if (mes == "04") strMes = "Abril";
            if (mes == "05") strMes = "Mayo";
            if (mes == "06") strMes = "Junio";
            if (mes == "07") strMes = "Julio";
            if (mes == "08") strMes = "Agosto";
            if (mes == "09") strMes = "Septiembre";
            if (mes == "10") strMes = "Octubre";
            if (mes == "11") strMes = "Noviembre";
            if (mes == "12") strMes = "Diciembre";

            fechaTexto = dia + " días del mes de " + strMes + " del " + anio;

            mes2 = fechaVencimiento.Substring(4, 2);
            anio2 = fechaVencimiento.Substring(0, 4);

            plazo = (Int32.Parse(mes2) - Int32.Parse(mes)) + 12 * (Int32.Parse(anio2) - Int32.Parse(anio));
            plazo = plazo + 1;
            strPlazo = plazo.ToString();

            cuentaDebitoMask = EnmascararCuentas(cuentaDebito);

            montoFinalMetaD = montoFinalMeta;
            montoDebitoD = montoDebito;

            Mf = montoFinalMetaD.Replace(",", "");
            Md = montoDebitoD.Replace(",", "");
            Mf = montoFinalMetaD.Replace(".", "");
            Md = montoDebitoD.Replace(".", "");

            MF = Convert.ToInt32(Mf);
            MD = Convert.ToInt32(Md);
            PL = MF / MD;

            montoFinalMetaD = string.Format(CultureInfo.GetCultureInfo("de-DE"), "{0:n}", Convert.ToDouble(Mf));
            montoDebitoD = string.Format(CultureInfo.GetCultureInfo("de-DE"), "{0:n}", Convert.ToDouble(Md));


            parrafo1 = "<html><head></head><body><p><FONT FACE='Arial' SIZE ='2'>Estimado(a): " + fullName + "<br><br>";
            parrafo1 = parrafo1 + "Banco Guayaquil te informa que tu cuenta " + cuentaDebitoMask + " ha sido vinculada a tu meta con las siguientes características:<br><br>";
            parrafo1 = parrafo1 + "Nombre de tu meta: " + nombreMeta + "<br>";
            parrafo1 = parrafo1 + "Pago mensual: $ " + montoDebitoD + "<br>";
            parrafo1 = parrafo1 + "Plazo: " + PL + " meses<br>";
            parrafo1 = parrafo1 + "Monto final de ahorro: $ " + montoFinalMetaD + "<br><br>";

            parrafo1 = parrafo1 + "Se adjunta contrato, el cual <b>fue aceptado de manera electrónica a los " + fechaTexto + ",</b> podrá ser descargado y tenerlo como soporte.<br><br>";
            parrafo1 = parrafo1 + "Saludos Cordiales,<br>Banco Guayaquil<br><b></FONT><a href='" + "http://www.bancoguayaquil.com/" + "'>www.bancoguayaquil.com</a></b><br>";
            parrafo1 = parrafo1 + "</p><body></html>";
            return parrafo1;
        }
    }
}