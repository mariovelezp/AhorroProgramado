using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiMetaBG.WebApp.Models
{
    public class Cliente
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
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

    public class ObtenerMes 
    {
        public string fecha { get; set; }
        public string NumFec { get; set; }
    }

    public class VentaCruzadas 
    {
        public int codigoCruzar { get; set; }
        public string Valor { get; set; }
    }
}