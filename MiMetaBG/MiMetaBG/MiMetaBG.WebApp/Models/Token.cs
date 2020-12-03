using Jose;
using MiMetaBG.WebApp.MiMetaWS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MiMetaBG.WebApp.Models
{
    public class Token
    {
        public string cuentasDebito { get; set; }
        public string tipoDebito { get; set; }
        public string respuesta { get; set; }

        

        public byte[] Base64UrlDecode(string arg) // This function is for decoding string to   
        {
            string s = arg;
            s = s.Replace('-', '+'); // 62nd char of encoding  
            s = s.Replace('_', '/'); // 63rd char of encoding  
            switch (s.Length % 4) // Pad with trailing '='s  
            {
                case 0: break; // No pad chars in this case  
                case 2: s += "=="; break; // Two pad chars  
                case 3: s += "="; break; // One pad char  
                default:
                    throw new System.Exception();
            }
            return Convert.FromBase64String(s); // Standard base64 decoder  
        }
    }


    public class tokenLista {
        public string cuentaAhorro { get; set; }
        public string cuentasDebito { get; set; }
        public string tipoDebito { get; set; }
        public string respuesta { get; set; }

        public byte[] Base64UrlDecode(string arg) // This function is for decoding string to   
        {
            string s = arg;
            s = s.Replace('-', '+'); // 62nd char of encoding  
            s = s.Replace('_', '/'); // 63rd char of encoding  
            switch (s.Length % 4) // Pad with trailing '='s  
            {
                case 0: break; // No pad chars in this case  
                case 2: s += "=="; break; // Two pad chars  
                case 3: s += "="; break; // One pad char  
                default:
                    throw new System.Exception();
            }
            return Convert.FromBase64String(s); // Standard base64 decoder  
        }
    }
}