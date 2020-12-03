using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;

namespace MiMetaBG.WebApp.Models
{
    public class General
    {
        public string Ip { get; set; }
        public string Aplicacion { get; set; }
        public string idPantalla { get; set; }

        public General() {
            Ip = "";
            Aplicacion = "";
            idPantalla = "";
        }

        public void GetLocalData() {
            Ip = GetIPAddress();
        }

        public string GetIpLocal()
        {
            string hostName = Dns.GetHostName(); // Retrive the Name of HOST  
                                                 // Get the IP  
            string myIP = Dns.GetHostEntry(hostName).AddressList[1].ToString();
            return myIP;
        }


        protected string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }

            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

    }

    
}