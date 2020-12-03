using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
{
    public class prvImagenResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }

        public prvImagenResponse()
        {
            codigoRetorno = "";
            mensajeRetorno = "";
        }
    }
}
