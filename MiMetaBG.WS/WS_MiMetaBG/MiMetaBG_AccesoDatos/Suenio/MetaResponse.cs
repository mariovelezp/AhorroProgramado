using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Suenio
{
    public class MetaResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
        public int numeroSolicitud { get; set; }

        public MetaResponse() {
            codigoRetorno = "";
            mensajeRetorno = "";
            numeroSolicitud = 0;
        }
    }
}
