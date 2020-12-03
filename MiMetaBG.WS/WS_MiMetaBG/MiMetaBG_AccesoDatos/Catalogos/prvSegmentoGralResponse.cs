﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Catalogos
{
    public class prvSegmentoGralResponse
    {
        public string codigoRetorno { get; set; }
        public string mensajeRetorno { get; set; }
        public List<SegmentoGralBG> listaSegmento { get; set; }
    }
    public class SegmentoGralBG
    {
        public int idSegmento { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public byte[] imagen { get; set; }
        public byte[] emojin { get; set; }
        public string codimagen { get; set; }
        public string umbral { get; set; }
        public string estado { get; set; }
        public string prod_cruz { get; set; }
        public string prioridad { get; set; }
        public string mensaje_seg { get; set; }
        public string cod_host { get; set; }
    }
}