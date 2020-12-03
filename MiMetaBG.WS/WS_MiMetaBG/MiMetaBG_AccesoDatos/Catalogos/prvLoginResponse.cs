using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
{
    public class prvLoginResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
        public List<UsuarioBG> listaUsuario { get; set; }
    }
    public class UsuarioBG
    {
        public int Id_codigo { get; set; }
        public string Aplicacion { get; set; }
    }
}
