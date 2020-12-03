using Jose;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace MiMetaBG.WebApp.Models
{
    public class metodos
    {

        public Token ValidaToken(string token)
        {
            Token tokens = new Token();
            string tokenValidado = string.Empty;
            string tIdentificacion = string.Empty;

            try 
            {
                string valueSecret = ConfigurationManager.AppSettings.Get("JWT_SECRET_KEY").ToString();
                byte[] secretKey =  tokens.Base64UrlDecode(valueSecret); //pass key to secure and decode it  
                tokenValidado = JWT.Decode(token, secretKey);
                dynamic objRespuesta = JsonConvert.DeserializeObject(tokenValidado);
                tokens.cuentasDebito = objRespuesta.cuentaDebito;
                tokens.tipoDebito = objRespuesta.TipoCuenta;
                tokens.respuesta = "0000";
            }
            catch (Exception e)
            {
                tokens.respuesta = "9999";
            }
            return tokens;
        }

        public string GenerarToken(string cuentaDebito, string TipoCuenta) //function for JWT Token  
        {
            Token request = new Token();
            string valueSecret = ConfigurationManager.AppSettings.Get("JWT_SECRET_KEY").ToString();
            byte[] secretKey = request.Base64UrlDecode(valueSecret);//pass key to secure and decode it  
            DateTime issued = DateTime.Now;
            var User = new Dictionary<string, object>()
                    {
                        {"cuentaDebito", cuentaDebito},
                        {"TipoCuenta", TipoCuenta}
                    };
            string token = JWT.Encode(User, secretKey, JwsAlgorithm.HS256);
            return token;
        }


        // ----------------------------------------------------------------------------------//
        public string GenerarCuentaAhorro(string cuentaAhorro) //function for JWT Token  
        {
            tokenLista request = new tokenLista();
            string valueSecret = ConfigurationManager.AppSettings.Get("JWT_SECRET_KEY").ToString();
            byte[] secretKey = request.Base64UrlDecode(valueSecret);//pass key to secure and decode it  
            DateTime issued = DateTime.Now;
            var User = new Dictionary<string, object>()
                    {
                        {"cuentaAhorro", cuentaAhorro}
                    };
            string token = JWT.Encode(User, secretKey, JwsAlgorithm.HS256);
            return token;
        }


        public string ValidaCuentaAhorro(string token)
        {
            string tokenValidado = string.Empty;
            tokenLista tokens = new tokenLista();
            string cuentaAhorro = string.Empty;
            try
            {
                string valueSecret = ConfigurationManager.AppSettings.Get("JWT_SECRET_KEY").ToString();

                byte[] secretKey = tokens.Base64UrlDecode(valueSecret); //pass key to secure and decode it  
                tokenValidado = JWT.Decode(token, secretKey);
                dynamic objRespuesta = JsonConvert.DeserializeObject(tokenValidado);
                cuentaAhorro = objRespuesta.cuentaAhorro;
            }
            catch (Exception e)
            {
                cuentaAhorro = "9999";
            }
            return cuentaAhorro;
        }

        // ------------------------------------------------------------------------------ //
        public string GenerarCuentaDebito(string cuentaDebito)
        {
            tokenLista request = new tokenLista();
            string valueSecret = ConfigurationManager.AppSettings.Get("JWT_SECRET_KEY").ToString();
            byte[] secretKey = request.Base64UrlDecode(valueSecret);
            DateTime issued = DateTime.Now;
            var User = new Dictionary<string, object>()
                    {
                        {"cuentaDebito", cuentaDebito}
                    };
            string token = JWT.Encode(User, secretKey, JwsAlgorithm.HS256);
            return token;
        }


        public string ValidaCuentaDebito(string token)
        {
            tokenLista tokens = new tokenLista();
            string tokenValidado = string.Empty;
            string cuentaDebito = string.Empty;
            try
            {
                string valueSecret = ConfigurationManager.AppSettings.Get("JWT_SECRET_KEY").ToString();

                byte[] secretKey = tokens.Base64UrlDecode(valueSecret); //pass key to secure and decode it  
                tokenValidado = JWT.Decode(token, secretKey);

                dynamic objRespuesta = JsonConvert.DeserializeObject(tokenValidado);
                cuentaDebito = objRespuesta.cuentaDebito;
            }
            catch (Exception e)
            {
                cuentaDebito = "9999";
            }
            return cuentaDebito;
        }
    }
}