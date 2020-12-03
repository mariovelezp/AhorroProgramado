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
    public class logmasterResponse
    {
        public ParametroResponse parametroResponse { get; set; }
        public CuentasResponse cuentasResponse { get; set; }
        public ValidacionClienteOTPResponse validacionOTPResponse  { get; set; }
        public ValidacionClienteResponse validacionClienteResponse { get; set; }
        public MetaResponse metaResponse { get; set; }
        public ConsultaMetasResponse consultaMetasResponse { get; set; }
        public TokenResponse tokenResponse { get; set; }
        public MetaAdicionalResponse AdicionalResponse { get; set; }
    }
}
