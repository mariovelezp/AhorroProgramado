using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiMetaBG.WebApp.Models
{
    public class ParametroResponse 
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
        public int montoInicial { get; set; }
        public int montoFinal { get; set; }
        public int tiempoIncial { get; set; }
        public int tiempoFinal { get; set; }
        public string mensaje { get; set; }
        public List<ImagSegmento> imagSegmento { get; set; }


        public ParametroResponse()
        {
            codigoRetorno = "";
            mensajeRetorno = "";
            montoInicial = 0;
            montoFinal = 0;
            tiempoIncial = 0;
            tiempoFinal = 0;
            mensaje = "";
            imagSegmento = null;
        }


    }
    public class ImagSegmento
    {
        public int segmentoId { get; set; }
        public byte[] emojid { get; set; }
    }

}