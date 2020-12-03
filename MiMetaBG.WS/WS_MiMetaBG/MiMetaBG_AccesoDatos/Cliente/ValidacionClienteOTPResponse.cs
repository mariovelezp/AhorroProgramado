using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Cliente
{

    public class ValidacionClienteOTPResponse
    {
        public string tokenValidacion { get; set; }
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
    }

}
