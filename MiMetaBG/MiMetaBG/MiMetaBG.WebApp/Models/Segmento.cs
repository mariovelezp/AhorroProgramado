using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiMetaBG.WebApp.Models
{
    public class Segmento
    {
        public int idSegmento { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public byte[] imagen { get; set; }
        public byte[] emojin { get; set; }
    }
}