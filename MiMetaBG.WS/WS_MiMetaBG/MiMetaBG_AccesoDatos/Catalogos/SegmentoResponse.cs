using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
{
    public class SegmentoResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
        public List<SegmentoBG> listaSegmento { get; set; }

    }

    public class SegmentoBG
    {
        public int idSegmento { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public byte[] imagen { get; set; }
        public byte[] emojin { get; set; }
    }
}
