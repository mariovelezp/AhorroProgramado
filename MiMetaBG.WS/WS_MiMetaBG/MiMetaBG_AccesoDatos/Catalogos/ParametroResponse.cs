using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
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
        public List<ImagSegmento> imagSegmento { get; set;}
    }

    public class ImagSegmento
    {
        public int segmentoId { get; set; }
        public byte[] emojid { get; set; }
    }
}
