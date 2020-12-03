﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MiMetaBG.WebApp.Models
{
    public class ParametroRequest
    {
        public int MarcaImagen { get; set; }
        public int idSegmento { get; set; }
        public string Ip { get; set; }
        public string Aplicacion { get; set; }
        public string idPantalla { get; set; }


        public ParametroRequest()
        {
            idSegmento = 0;
            MarcaImagen = 1;
            Ip =  GetIpLocal();
            Aplicacion = "";
            idPantalla = "";
        }
        private string GetIpLocal()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
            // Get the IP  
            string myIP = Dns.GetHostEntry(hostName).AddressList[0].ToString();
            return myIP;
        }

    }
}