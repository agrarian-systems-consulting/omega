using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

namespace OMEGA.Other_Classes
{
    /// <summary>
    /// Ecrit dans le XML les préférences utilisateurs
    /// </summary>
    static class WriteXML
    {

        internal static void Write(string elementString,string value)
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;
                string path = Properties.Settings.Default.DataPath + "\\Info\\OMEGA.xml";
                if (File.Exists(path))
                {
                    
                    XDocument xDocument = XDocument.Load(path);
                    XElement root = xDocument.Element("OMEGA");
                    XElement element2 = root.Element(elementString);
                    element2.Value = value;
                    xDocument.Save(path);
                }
                else
                {
                    MessageBox.Show(Translation.Translate("Impossible to find OMEGA.xml file on the directory",langue) +" : '"+ path + "'");
                }
            }
            catch (Exception Ex)
            {

            }
        }

    }
}
