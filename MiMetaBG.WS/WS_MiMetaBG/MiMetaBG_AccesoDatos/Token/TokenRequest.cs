using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MiMetaBG_AccesoDatos.Token
{
    public class TokenRequest
    {
        public string tokenValidacion { get; set; }
        public string ip { get; set; }
        public string aplicacion { get; set; }
        public string idPantalla { get; set; }

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


        public long ToUnixTime(DateTime dateTime)
        {
            return (int)(dateTime.ToUniversalTime().Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        public string verificaTipoIdentificacion(string cadena)
        {
            string b = "P";
            if (cadena.Length == 10)
            {
                if (ValidadorCedula(cadena))
                {
                    b = "C";
                }
            }
            if (cadena.Length == 13)
            {
                if (ValidadorRuc(cadena))
                {
                    b = "R";
                }
            }
            return b;
        }

        private bool ValidadorCedula(string ID)
        {
            long Fx = 0;
            int i = 0;
            long residuo = 0;
            string cedula = ID;
            long Numero = 0;
            long provincia = 0;
            long TDig = 0;
            long verificador = 0;
            string Peso = "212121212";
            long Suma = 0;
            long Producto = 0;
            long Val1 = 0;
            long Val2 = 0;

            try
            {
                if (long.TryParse(ID, out Fx) == false)
                {
                    return false;
                }
                else
                {
                    if (ID.Substring(0, 3) == "000")
                    {
                        return false;
                    }
                    else
                    {
                        cedula = ID;
                        Numero = Convert.ToInt64(cedula);
                        provincia = Convert.ToInt64(cedula.Substring(0, 2));
                        if ((provincia < 1 || provincia > 25) && provincia != 30)
                        {
                            return false;
                        }
                        else
                        {
                            TDig = Convert.ToInt64(cedula.Substring(2, 1));
                            if (TDig > 6)
                            {
                                return false;
                            }
                            else
                            {
                                verificador = Convert.ToInt64(cedula.Substring(9, 1));
                                for (i = 0; i < 9; i++)
                                {
                                    Val1 = Convert.ToInt64(cedula.Substring(i, 1));
                                    Val2 = Convert.ToInt64(Peso.Substring(i, 1));
                                    Producto = Val1 * Val2;
                                    if (Producto > 9)
                                    {
                                        Producto = Producto - 9;
                                    }
                                    Suma = Suma + Producto;
                                }
                                residuo = Suma % 10;
                                if ((residuo == 0 && verificador == 0) || (verificador == (10 - residuo)))
                                {
                                    return true;
                                }
                                else
                                {
                                    return false;
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        private bool ValidadorRuc(string ID)
        {
            bool valor;
            long Fx = 0;
            string cedula = ID;
            long Numero = 0;
            long provincia = 0;
            long TDig = 0;

            try
            {
                if (long.TryParse(ID, out Fx) == false)
                {
                    return false;
                }
                else
                {
                    if (ID.Substring(0, 3) == "000")
                    {
                        return false;
                    }
                    else
                    {
                        cedula = ID;
                        Numero = Convert.ToInt64(cedula);
                        provincia = Convert.ToInt64(cedula.Substring(0, 2));
                        if (provincia < 1 || provincia > 24)
                        {
                            return false;
                        }
                        else
                        {
                            TDig = Convert.ToInt64(cedula.Substring(2, 1));
                            if (TDig < 6)
                            {
                                valor = verificador(cedula, "212121212", 0);
                                if (valor)
                                    return true;
                                else
                                    return false;
                            }
                            else if (TDig == 6)
                            {
                                valor = verificador(cedula, "32765432", 1);
                                if (valor)
                                    return true;
                                else
                                    return false;
                            }
                            else if (TDig == 9)
                            {
                                valor = verificador(cedula, "432765432", 0);
                                if (valor)
                                    return true;
                                else
                                    return false;
                            }
                            else
                                return false;
                        }
                    }
                }

            }
            catch
            {
                return false;
            }
        }

        private bool verificador(string cedula, string Peso, int completador)
        {
            long residuo = 0;
            long verificador = 0;
            long Val1 = 0;
            long Val2 = 0;
            long Producto = 0;
            long Suma = 0;
            int i = 0;
            int natural = 0;
            verificador = Convert.ToInt64(cedula.Substring(9 - completador, 1));
            for (i = 0; i < 9 - completador; i++)
            {
                Val1 = Convert.ToInt64(cedula.Substring(i, 1));
                Val2 = Convert.ToInt64(Peso.Substring(i, 1));
                Producto = Val1 * Val2;
                if (Peso == "212121212")
                {
                    if (Producto > 9)
                    {
                        Producto = Producto - 9;
                        natural = 1;
                    }
                }
                Suma = Suma + Producto;
            }
            residuo = Suma % (11 - natural);

            if ((((11 - natural) - residuo) == verificador) || (residuo == 0 && verificador == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
 

    }
}
