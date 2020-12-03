using MiMetaBG_AccesoDatos.Catalogos;
using MiMetaBG_AccesoDatos.Cliente;
using MiMetaBG_AccesoDatos.Meta;
using MiMetaBG_AccesoDatos.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Log
{
    public class logmasterRequest
    {
        public ParametroRequest parametroRequest { get; set; }
        public CuentasRequest cuentasRequest { get; set; }
        public ValidacionClienteRequest validacionClienteRequest { get; set; }
        public ValidacionClienteOTPRequest validacionOTPRequest { get; set; }
        public MetaRequest metaRequest { get; set; }
        public ConsultaMetasRequest consultasMetas { get; set; }
        public TokenRequest tokenRequest { get; set; }
        public MetaAdicionalRequest AdicionalRequest { get; set; }
    }
}
