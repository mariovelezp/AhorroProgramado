using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Cliente
{
    public class ValidacionClienteResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
        public string correoElectronico { get; set; }
        public string nombreCliente { get; set; }
        public string correoSinMascara { get; set; }
        public string fullName { get; set; }

        public ValidacionClienteResponse()
        {
            codigoRetorno = "";
            mensajeRetorno = "";
            correoElectronico = "";
            nombreCliente = "";
            correoSinMascara = "";
            fullName = "";
        }
    }
}
