using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA
{
    /// <summary>
    /// Log enregistre tous les messages d'erreur lors de l'utilisation
    /// </summary>
    static class Log
    {
        internal static void WriteLog(string message, string errorsource = "Unknown")
        {
            try
            {
                CheckSize();
                StreamWriter LogFile;
                if (File.Exists(Properties.Settings.Default.DataPath + "\\Info\\Log.log"))
                {
                     LogFile = new StreamWriter(File.Open(Properties.Settings.Default.DataPath + "\\Info\\Log.log", FileMode.Append), Encoding.UTF8);
                }
                else
                {
                    LogFile = new StreamWriter(File.Open(Properties.Settings.Default.DataPath + "\\Info\\Log.log", FileMode.CreateNew), Encoding.UTF8);
                }
                LogFile.Write(DateTime.Now.ToString()+ ". Error Info : " + message + " . Error location : " + errorsource);
                LogFile.WriteLine();
                LogFile.Close();
                LogFile.Dispose();
            }
            catch (Exception Ex)
            {
               //MessageBox.Show(Ex.Message);
            }
            
        }

        private static void CheckSize()
        {
            try
            {
                if (File.Exists(Properties.Settings.Default.DataPath + "\\Info\\Log.log"))
                {
                    FileInfo fileInfo = new FileInfo(Properties.Settings.Default.DataPath + "\\Info\\Log.log");
                    if (fileInfo.Length > 50000000) //supérieur à 50Mo on supprime
                    {
                        File.Delete(Properties.Settings.Default.DataPath + "\\Info\\Log.log");
                    }
                }
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
                WriteLog(Ex.Message,"Class Log. Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
         
            
        }
    }
}
