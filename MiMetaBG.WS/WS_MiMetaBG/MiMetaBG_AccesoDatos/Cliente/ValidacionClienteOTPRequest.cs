﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace MiMetaBG_AccesoDatos.Cliente
{
    public class ValidacionClienteOTPRequest
    {
        public string identificacion { get; set; }
        public string tipoIdentificacion { get; set; } //agrego
        public string codigoOTP { get; set; }
        public string ip { get; set; }
        public string aplicacion { get; set; }
        public string idPantalla { get; set; }

        public byte[] GetBytes(string value)
        {
            try
            {
                byte[] bytes = new byte[value.Length * sizeof(char)];
                System.Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
                return System.Security.Cryptography.SHA256.Create().ComputeHash(bytes);
            }
            catch (Exception e)
            {
                throw new Exception("GetBytes: " + e.ToString());
            }
        }

        public int GetSaltSize(byte[] passwordBytes)
        {
            var key = new System.Security.Cryptography.Rfc2898DeriveBytes(passwordBytes,
            passwordBytes, 1000);
            byte[] ba = key.GetBytes(2);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ba.Length; i++)
            {
                sb.Append(Convert.ToInt32(ba[i]).ToString());
            }
            int saltSize = 0;
            string s = sb.ToString();
            foreach (char c in s)
            {
                int intc = Convert.ToInt32(c.ToString());
                saltSize = saltSize + intc;
            }
            return saltSize;
        }

        public byte[] GetRandomBytes(int length)
        {
            byte[] ba = new byte[length];
            System.Security.Cryptography.RNGCryptoServiceProvider.Create().GetBytes(ba);
            return ba;
        }

        public byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] Key, byte[] Iv)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytesKey = Key;
            byte[] saltBytesIv = Iv;
            using (System.IO.MemoryStream ms = new MemoryStream())
            {
                using (System.Security.Cryptography.RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;
                    var key = new System.Security.Cryptography.Rfc2898DeriveBytes(Key,
                    saltBytesKey, 1000);
                    var iv = new Rfc2898DeriveBytes(Iv, saltBytesIv, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = iv.GetBytes(AES.BlockSize / 8);
                    AES.Mode = System.Security.Cryptography.CipherMode.CBC;
                    using (System.Security.Cryptography.CryptoStream cs = new CryptoStream(ms,
                    AES.CreateEncryptor(), System.Security.Cryptography.CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }
    }


    public class TokenG
    {
        public string token { get; set; }
        public string usuario { get; set; }
        public string codigo_autorizac { get; set; }
        public string tokenfirma { get; set; }
    }


}