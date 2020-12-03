using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Cliente
{
    public class CuentasRequest
    {
        public string tokenValidacion { get; set; }
        public int idSegmento { get; set; }
        public string ip { get; set; }
        public string aplicacion { get; set; }
        public string idPantalla { get; set; }
    }
}
