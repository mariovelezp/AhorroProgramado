using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Meta
{
    public class ConsultaMetasResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
        public int tiempoIncial { get; set; }
        public int tiempoFinal { get; set; }
        public int montoInicial { get; set; }
        public int montoFinal { get; set; }
        public List<ListaMetas> listasMetasCreadas { get; set; }
        public List<ObtenerTodasEditar> ListaTodas { get; set; }
        public List<VentasCruzadas> ListaVentasCruzadas { get; set; }

        public ConsultaMetasResponse()
        {
            codigoRetorno = "0000";
            mensajeRetorno = "Exitoso";
        }
    }


    public class ListaMetas
    {
        public string cuentaAhorro { get; set; }
        public string cuenta { get; set; }
        public string numeroSolicitud { get; set; }
        public string cuentaDebito { get; set; }
        public string tipoCuentaDebito { get; set; }
        public string frecuenciaDebito { get; set; }
        public string diaDebito1 { get; set; }
        public string diaDebito2 { get; set; }
        public string montoMeta { get; set; }
        public string cuotaMeta { get; set; }
        public string fechaEmision { get; set; }
        public string valorDebitosAcumulados { get; set; }
        public string montoMetaform { get; set; }
        public string plazoMeta { get; set; }
        public string nombreMeta { get; set; }
        public string cuotaMetaform { get; set; }
        public string plazoMetaform { get; set; }
        public int idSegmento { get; set; }
        public byte[] emojid { get; set; }
        public int porcentaje { get; set; }
    }

    public class ObtenerTodasEditar
    {
        public string cta { get; set; }
        public string ctaEnm { get; set; }
        public string tipo { get; set; }
    }

    public class VentasCruzadas
    {
        public int codigoCruzar { get; set; }
        public string valorCruzar { get; set; }
    }
}
