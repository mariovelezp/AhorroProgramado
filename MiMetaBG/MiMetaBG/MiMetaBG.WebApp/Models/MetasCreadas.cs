using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiMetaBG.WebApp.Models
{
    public class MetasCreadas
    {
        public List<MetasCreada> metasCreadas { get; set; }
        public List<VentaCruzadas> ventasCruzada { get; set; }
        public int contador { get; set; }
    }


    public class MetasCreada
    {
        public string nombreMeta { get; set; }
        public string montoMeta { get; set; }
        public string montoMetaForn { get; set; }
        public string numeroSolicitud { get; set; }
        public string cuentaAhorro { get; set; }
        public string cuotaMeta { get; set; }
        public string cuotaMetaForm { get; set; }
        public string plazoMeta { get; set; }
        public string plazoMetaForm { get; set; }
        public int idSegmento { get; set; }
        public byte[] emojid { get; set; }
        public int porcentaje { get; set; }
        public string fechaEmision { get; set; }
        public string cuentaDebito { get; set; }
        public string cuentasEnm { get; set; }
        public string tipoDebito { get; set; }
        public string frecuencia { get; set; }
        public string diaDeb1 { get; set; }
        public string diaDeb2 { get; set; }
        public string valorDebAcumulado { get; set; }
    }
}