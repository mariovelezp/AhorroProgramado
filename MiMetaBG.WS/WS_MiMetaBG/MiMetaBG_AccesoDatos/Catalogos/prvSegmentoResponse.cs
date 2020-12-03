using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
{
    public class prvSegmentoResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }

        public prvSegmentoResponse()
        {
            codigoRetorno = "";
            mensajeRetorno = "";
        }
    }
    
}
