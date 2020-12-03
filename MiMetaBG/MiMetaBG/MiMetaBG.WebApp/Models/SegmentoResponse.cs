using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG.WebApp.Models
{
    public class SegmentoResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
        public List<Segmento> listaSegmento { get; set; }

    }
}
