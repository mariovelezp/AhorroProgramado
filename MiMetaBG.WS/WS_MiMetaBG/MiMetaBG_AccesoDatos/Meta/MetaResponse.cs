using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Meta
{
    public class MetaResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
        public int numeroSolicitud { get; set; }
        //public string correoSinMascara { get; set; }

        public MetaResponse()
        {
            codigoRetorno = "";
            mensajeRetorno = "";
            numeroSolicitud = 0;
            //correoSinMascara = "";
        }
    }
}
