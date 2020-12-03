using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Cliente
{
    public class CuentasResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
        public int idSegmento { get; set; }
        public byte[] imagenes { get; set; }
        public List<ObtenerUna> ListaUna { get; set; }
        public List<ObtenerTodas> ListaTodas { get; set; }

    }

    public class ObtenerUna
    {
        public string cta { get; set; }
        public string ctaEnm { get; set; }
        public string tipo { get; set; }
    }

    public class ObtenerTodas
    {
        public string cta { get; set; }
        public string ctaEnm { get; set; }
        public string tipo { get; set; }
    }
}
