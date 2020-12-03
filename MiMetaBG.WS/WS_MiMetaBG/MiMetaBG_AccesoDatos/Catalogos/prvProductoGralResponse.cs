using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
{
    public class prvProductoGralResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
        public List<ProductoGralBG> listaProducto { get; set; }
    }
    public class ProductoGralBG
    {
        public string idproducto { get; set; }
        public string descripcionproducto { get; set; }
    }
}
