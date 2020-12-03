using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiMetaBG.WebApp.Models
{
    public class Logs
    {
        public void grabaLogs(string Metodo, string mensaje)
        {
            try
            {
                if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\Logs"))
                    System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "\\Logs");
                System.IO.StreamWriter sb = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\AhorroMeta_LogsCustomError_" + System.DateTime.Now.ToString("yyyyMMdd") + ".txt", true);
                sb.Write("[CustomError]: " + DateTime.Now.ToString() + "\r\n");
                sb.Write("[Metodo]: " + Metodo + "\r\n");
                sb.Write("[Message]: " + mensaje + "\r\n");
                sb.Write("[CustomError]: FIN \r\n");

                sb.Close();
            }
            catch (Exception ex)
            { }
        }

    }
}