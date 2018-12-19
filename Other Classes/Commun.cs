using Ionic.Zip;
using MapWinGIS;
using OMEGA.Forms.Territory_Forms;
using OMEGA.SQLQuery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;

namespace OMEGA
{
    /// <summary>
    /// Cette classe contient des fonctions utiles un peu partout dans le code
    /// </summary>
    static class Commun
    {

        internal static string FontName { get; set; } 
        internal static string Type { get; set; }
        internal static float Size { get; set; }
        private static int langue = Properties.Settings.Default.Langue;

        /// <summary>
        /// Zip les fichier et forme un .omg qui pourra être lu par l'application
        /// </summary>
        /// <param name="directory"> Répertoire où sont stockées les données à enregistrer</param>
        /// <param name="filename"> Nom complet (path + nom du fichier sans l'extension) du fichier sauvegardé</param>
        /// <returns></returns>
        internal static bool Compress(string directory, string filename)
        {
            DirectoryInfo directorySelected = new DirectoryInfo(directory);
            bool NoError = true;
            if (filename.Contains(".omg") == false)
            {
                filename = filename + ".omg";
            }
            ZipFile zip = new ZipFile(filename);
            try
            {
                if (!Directory.Exists(directorySelected.FullName))
                {
                    DirectoryInfo dir = Directory.CreateDirectory(directorySelected.FullName);
                    dir.Attributes = FileAttributes.Hidden;
                }
                //MessageBox.Show(directorySelected.FullName);
                foreach (FileInfo fichier in directorySelected.GetFiles())
                {
                    if (fichier.Extension == ".xml")
                    {
                        zip.AddFile(fichier.FullName, "Info");
                        //MessageBox.Show("save xml");
                    }
                    if (fichier.Extension == ".db")
                    {
                        zip.AddFile(fichier.FullName, "Info");
                       // MessageBox.Show("save db");
                    }
                    if (fichier.Extension == ".log")
                    {
                        zip.AddFile(fichier.FullName, "Info");

                        //MessageBox.Show("save log");
                    }
                    if (fichier.Extension == ".shp")
                    {
                        zip.AddFile(fichier.FullName, "Shp");

                        //MessageBox.Show("save log");
                    }
                    if (fichier.Extension == ".shx")
                    {
                        zip.AddFile(fichier.FullName, "Shp");

                        //MessageBox.Show("save log");
                    }
                    if (fichier.Extension == ".prj")
                    {
                        zip.AddFile(fichier.FullName, "Shp");

                        //MessageBox.Show("save log");
                    }
                    if (fichier.Extension == ".dbf")
                    {
                        zip.AddFile(fichier.FullName, "Shp");

                        //MessageBox.Show("save log");
                    }
                    zip.Save();
                }
                zip.Dispose();
                //MessageBox.Show("save end");

                if (File.Exists(""))
                {

                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                NoError = false;
            }
            return NoError;
        }


        /// <summary>
        /// permet d'extraire tous les fichiers dans le présent dans le Zip envoyé en paramètre
        /// </summary>
        /// <param name="filefullpath"> Représente le fichier à décompresser </param>
        /// <returns> vrai si pas d'erreur</returns>
        internal static bool Decompress(string filefullpath)
        {
            Boolean DecompressOK = true;
            string pathToExtract;
            ZipFile zip;
            try
            {
                if (File.Exists(filefullpath))
                {
                    if (filefullpath.Contains(".omg"))
                    {
                        zip = new ZipFile(filefullpath);
                        pathToExtract = RemoveExtension(filefullpath, 3);
                    }
                    else
                    {
                        zip = new ZipFile(filefullpath + "omg");
                        pathToExtract = filefullpath;
                    }
                    if (Directory.Exists(pathToExtract) == false)
                    {
                        DirectoryInfo dir = Directory.CreateDirectory(pathToExtract);
                        dir.Attributes = FileAttributes.Hidden  ;
                    }
                    foreach (ZipEntry zipedfile in zip)
                    {
                        zipedfile.Extract(pathToExtract, ExtractExistingFileAction.OverwriteSilently);
                    }
                    zip.Save();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, System.Reflection.MethodBase.GetCurrentMethod().Name);
                DecompressOK = false;
            }

            return DecompressOK;
        }

        /// <summary>
        /// Retire l'extension d'un chemin complet
        /// </summary>
        /// <param name="fullpath"> le chemin complet du fichier</param>
        /// <param name="ExtensionSize"> le nombre de caratère de l'extension</param>
        /// <returns></returns>
        internal static string RemoveExtension(string fullpath, int ExtensionSize)
        {
            if (fullpath == "") return "";
            int pathlenght = fullpath.Length;
            string pathToReturn = fullpath.Substring(0, pathlenght - ExtensionSize - 1);
            if (pathToReturn.Last() == '.') pathToReturn = pathToReturn.Remove(pathToReturn.Length-1,1);
            return pathToReturn;
        }

        internal static int[] CastStringArrayToInt(string[] reader)
        {
            int[] arrint = new int[reader.Length];
            try
            {
                for (int i = 0; i < reader.Length; i++)
                {
                    int.TryParse(reader[i], out int temp);
                    arrint[i] = temp;
                }
            }
            catch
            {

            }
            return arrint;
            
            
        }

        internal static double[] CastStringArrayToDouble(string[] reader)
        {
            double[] arrint = new double[reader.Length];
            try
            {
                for (int i = 0; i < reader.Length; i++)
                {
                    double.TryParse(reader[i], out double temp);
                    arrint[i] = temp;
                }
            }
            catch
            {

            }
            return arrint;


        }

        /// <summary>
        /// Donne le nom du fichier
        /// </summary>
        /// <param name="fullpath"> Chemin complet</param>
        /// <param name="withExtension"> true si on veut récupérer l'extension</param>
        /// <param name="ExtensionSize"> si true alors il faut envoyer le nombre de caractère de l'extension</param>
        /// <returns></returns>
        internal static string GetFileName(String fullpath, Boolean withExtension, int ExtensionSize = 0)
        {
            string filename = "";
            string[] stringSeparators = new string[] { "[\\]" };
            Array ArrString = fullpath.Split(stringSeparators, StringSplitOptions.None);
            for (int i = ArrString.Length - 1; i < ArrString.Length; i++)
            {
                filename = ArrString.GetValue(i).ToString();
            }
            if (withExtension == false && ExtensionSize > 0)
            {
                int Filelenght = filename.Count<char>();
                // -1 pour enlever le point
                filename = filename.Substring(0, Filelenght - ExtensionSize - 1);
            }
            return filename;
        }

        /// <summary>
        /// Créer un fichier .db dans le chemin spécifié
        /// </summary>
        /// <param name="targetPath">Répertoire où le fichier sera créé</param>
        /// <param name="filetocreate">nom + extension du fichier à créer</param>
        internal static Boolean CreateSQLDatabase(string targetPath, string filetocreate)
        {
            Boolean FileCreated = false;
            try
            {
                if (Directory.Exists(Properties.Settings.Default.DataPath + "\\Info") == false)
                {
                    DirectoryInfo dir = Directory.CreateDirectory(targetPath + "\\Info");
                    dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                }
                if (File.Exists(Properties.Settings.Default.DataPath + "\\Info\\" + filetocreate))
                {
                    File.Delete(Properties.Settings.Default.DataPath + "\\Info\\" + filetocreate);
                }
                //MessageBox.Show(filetocreate);
                SQLiteConnection.CreateFile(Properties.Settings.Default.DataPath + "\\Info\\" + filetocreate);
                
                FileCreated = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message,
                    "Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                FileCreated = false;
            }
            return FileCreated;

        }

        /// <summary>
        /// Return the sequence If of the table
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        internal static int GetSeqId(string table)
        {
            try
            {
                string query = SQLQueryBuilder.SeqIdQuery(table);
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("Seq", query);
                if (list.Count > 0) return list[0];
                else return 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return 0;
        }

        /// <summary>
        /// Check if the list send has not null value
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static bool ListHasValue(List<string> list)
        {
            try
            {
                if (list.Count > 0)
                {
                    if (list[0] != null)
                    {
                        if (list[0] != "")
                        {
                            return true;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return false;
            }
           
        }

        /// <summary>
        /// Check if the Array send has not null value
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static bool ArrayHasValue(string[] array)
        {
            try
            { 
                if (array.Length > 0)
                {
                    if (array[0] != null)
                    {
                        if (array[0] != "")
                        {
                            return true;
                        }
                        else return false;
                    }
                    else return false;
                }
                else return false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                return false;
            }

}

        /// <summary>
        /// Check if the Array send has not null value
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static bool ArrayHasValue(Double[] array)
        {
            try
            {
                if (array.Length > 0)
                {                   
                        if (array[0] != 0)
                        {
                            return true;
                        }
                        else return false;
                    
                }
                else return false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                return false;
            }

        }

        /// <summary>
        /// Check if the list send has not null value
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static bool ListHasValue(List<int> list)
        {
            if (list != null)
            {
                if (list.Count > 0)
                {
                    if (list[0] == 0) return false;
                    else return true;
                }
                else return false;
            }
            else return false;

        }

        /// <summary>
        /// Check if the list send has not null value
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        internal static bool ListHasValue(List<double> list)
        {
            if (list != null)
            {
                if (list.Count > 0)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }

        /// <summary>
        /// Set The background color of the datagridview send. The column index reporesents the column where the
        /// color will be based on.
        /// </summary>
        /// <param name="columnindex"></param>
        /// <param name="dataGridView"></param>
        //internal static void Setbackground(int columnindex, DataGridView dataGridView)
        //{
        //    try
        //    {
        //        dataGridView.Columns[columnindex].SortMode = DataGridViewColumnSortMode.NotSortable;
        //        try
        //        {
        //            dataGridView.Sort(dataGridView.Columns[columnindex], System.ComponentModel.ListSortDirection.Ascending);
        //        }
        //        catch
        //        {
        //            return;
        //        }
        //        string value1;
        //        string value2;
        //        int i = 0;
        //        while (i <= dataGridView.RowCount - 1)
        //        {
        //            dataGridView.Rows[i].DefaultCellStyle.BackColor = DefaultColor;
        //            // if its the last, exit the loop
        //            if (i == dataGridView.RowCount - 1)
        //            {
        //                break;
        //            }
        //            // to avoid issue with the first letter (cap or not)
        //            value1 = dataGridView.Rows[i].Cells[columnindex].Value.ToString().Substring(1);
        //            value2 = GetNextGroupeName(i, columnindex, dataGridView).Substring(1);
        //            if (value1 != value2)
        //                DefaultColor = SwitchColor(DefaultColor);
        //            i++;
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //        Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

        //    }
        //}

        /// <summary>
        /// Return the max Id of the table send
        /// </summary>
        /// <param name="field"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        internal static int GetMaxId(string field,string table)
        {
            try
            {
                string query = "Select Max(" + field + ") From " + table + " ;";
                string[] reader = SQlQueryExecuter.RunQueryReader(query);
                if (reader.Length > 0)
                {
                    if (reader[0] != null)
                    {
                        int id;
                        int.TryParse(reader[0], out id);
                        return id;
                    }
                    else
                        return 0;
                }
                else
                    return 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return 0;
            }

        }

        /// <summary>
        /// Set the current font for the windows form
        /// </summary>
        internal static void SetCurrentFont()
        {
            try
            {
                string query = "Select * From Font WHERE ID = '1';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom",query);
                List<string> listTaille = SQlQueryExecuter.RunQueryReaderStr("taille", query);
                List<string> listType = SQlQueryExecuter.RunQueryReaderStr("type", query);

                if (ListHasValue(list))
                {
                    FontName = list[0];
                    Size = GetFloatFromString(listTaille[0]);
                }

            }
             catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        /// <summary>
        ///  Set the current font for the windows form
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="size"></param>
        /// <param name="italic"></param>
        /// <param name="bold"></param>
        internal static void SetCurrentFont(string nom, float size, string type)
        {
            try
            {
                FontName = nom;
                Size = size;
                Type = type;
                SaveFont(nom, size, type);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Get the current font from database
        /// </summary>
        /// <returns></returns>
        internal static Font GetCurrentFont()
        {
            try
            {
                Font font;
                FontStyle style = new FontStyle();
                switch (Type)
                {
                    case "Bold":
                        style = FontStyle.Bold;
                        break;
                    case "Italic":
                        style = FontStyle.Italic;
                        break;
                    case "Regular":
                        style = FontStyle.Regular;
                        break;
                    default:
                        style = FontStyle.Regular;
                        break;
                }
                font = new Font(FontName, Size,style);
                return font;
            }
            
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return null;
            }
        }

        /// <summary>
        /// Save font to the datatable
        /// </summary>
        /// <param name="n"></param>
        /// <param name="s"></param>
        /// <param name="i"></param>
        /// <param name="b"></param>
        private static void SaveFont (string n,float s,string t)
        {
            try
            {
                string type = t;
                string query = "Update Font set Nom = '" + n + "',taille ='" + s + "',type ='" + t +
                    "' WHERE ID = '1';";
                SQlQueryExecuter.RunQuery(query);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }


        /// <summary>
        /// set the background color, alternating the color for each line
        /// </summary>
        /// <param name="dataGridView"></param>
        //internal static void Setbackground(DataGridView dataGridView)
        //{
        //    try
        //    {
        //        Color DefaultColor = NewMethod();
        //        int i = 0;
        //        while (i <= dataGridView.RowCount - 1)
        //        {
        //             DefaultColor = SwitchColor(DefaultColor); 
        //             dataGridView.Rows[i].DefaultCellStyle.BackColor = DefaultColor;
        //            i++;
        //            // if its the last, exit the loop
        //            if (i == dataGridView.RowCount)
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //        Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

        //    }
        //}

        /// <summary>
        /// LoadShapeFile to the map
        /// </summary>
        /// <param name="pathToTest"></param>
        internal static bool LoadShapeFile(string pathToTest)
        {
            try
            {
                bool test = false; ;
                if (File.Exists(pathToTest))
                {
                    Shapefile territoryShapeFile = new Shapefile();
                    test = territoryShapeFile.Open(pathToTest);
                    if (!test)
                    {
                        DialogResult result = MessageBox.Show((territoryShapeFile.get_ErrorMsg(territoryShapeFile.LastErrorCode)) + "Shapefile : " +
                             pathToTest + Translation.Translate("Do not see this message again ? (Data will be deleted on the database)", langue),"",MessageBoxButtons.YesNo);
                        if (result == DialogResult.Yes)
                        {
                            string query = "Delete From SHP_Info Where path = '" + pathToTest + "';";
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                    else
                    {
                        // function returns null pointer on failure
                        Shapefile sfCopy = territoryShapeFile.Clone();
                        if (sfCopy == null)
                        {
                            test = false;
                            MessageBox.Show((territoryShapeFile.get_ErrorMsg(territoryShapeFile.LastErrorCode)));
                        }
                    }
                }
                else
                {
                    NofileFoundForm form = new NofileFoundForm(pathToTest);
                    form.ShowDialog();
                }
                return test;
            }
            catch (Exception Ex)
            {
               
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return false;
            }

        }

        internal static int GetNbPhase(int IdAct)
        {
            try
            {
                string query = "Select * From Ate_CatPhase Where Nom = '" + SQLQueryBuilder.FindName("Activite", "Nom", "ID", IdAct) + "';";
                string[] reader = SQlQueryExecuter.RunQueryReader(query);
                int nbphase = 0;
                for (int i = 3; i < reader.Length - 2; i++)
                {
                    if (reader[i] != null && reader[i] != "")
                    {
                        nbphase++;
                    }
                }
                return nbphase;
            }
            catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return 0;
            }

        }

        /// <summary>
        /// Parse string to double
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static double GetDoubleFromString(string input)
        {
            try
            {
                string value;
                if (input.Contains("."))
                {
                    string[] inputarr = input.Split('.');
                    value = inputarr[0] + "," + inputarr[1];
                }
                else value = input;
                double temp;
                double.TryParse(value, out temp);
                return temp;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                return 0;
            }
        }

        /// <summary>
        /// Parse string to float
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static float GetFloatFromString(string input)
        {
            try
            {
                string value;
                if (input.Contains("."))
                {
                    string[] inputarr = input.Split('.');
                    value = inputarr[0] + "," + inputarr[1];
                }
                else value = input;
                float temp;
                float.TryParse(value, out temp);
                return temp;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

                return 0;
            }
        }

        /// <summary>
        /// set the background color, alternating the color for each line
        /// </summary>
        /// <param name="dataGridView"></param>
        internal static void Setbackground(DataGridView dataGridView,Color color1,Color color2)
        {
            try
            {
                int i = 0;
                while (i <= dataGridView.RowCount - 1)
                {
                    if (i%2==0) dataGridView.Rows[i].DefaultCellStyle.BackColor = color1;
                    else dataGridView.Rows[i].DefaultCellStyle.BackColor = color2;
                    i++;
                    // if its the last, exit the loop
                    if (i == dataGridView.RowCount)
                    {
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// set the background color of header
        /// </summary>
        /// <param name="dataGridView"></param>
        internal static void SetbackgroundHeader(DataGridView dataGridView, Color color)
        {
            try
            {

                dataGridView.RowHeadersDefaultCellStyle.BackColor = color;
                dataGridView.ColumnHeadersDefaultCellStyle.BackColor = color;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Return the nex groupe name
        /// </summary>
        /// <param name="Rowindex"></param>
        /// <param name="columnindex"></param>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        private static string GetNextGroupeName(int Rowindex, int columnindex, DataGridView dataGridView)
        {
            return dataGridView.Rows[Rowindex + 1].Cells[columnindex].Value.ToString();
        }

        /// <summary>
        /// Swich color of lines in gridview
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        private static Color SwitchColor(Color color)
        {
            try
            {
                Color Blue = Color.CadetBlue;
                Color Red = Color.OrangeRed;

                if (color == Blue)
                {
                    color = Red;
                }
                else color = Blue;
                return color;
            }
              catch (Exception Ex)
            {

                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return color;
            }
        }

        /// <summary>
        /// Return the Id of the current exploitation
        /// </summary>
        /// <returns></returns>
        internal static int GetIdExpl()
        {
            try
            {
                string query = "Select ID From Exploitation Where Encours = '1';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (Commun.ListHasValue(list))
                {
                    return list[0];
                }
                else
                    return 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return 0;
            }
        }

        /// <summary>
        /// Return the Id of the current exploitation
        /// </summary>
        /// <returns></returns>
        internal static void SetIdExpl(int id)
        {
            try
            {
                try
                {
                    string query = "Update Exploitation Set Encours = '0' Where ID <>'" + id + "';";
                    SQlQueryExecuter.RunQueryReader(query);
                    query = "Update Exploitation Set Encours = '1' Where ID ='" + id + "';";
                    SQlQueryExecuter.RunQueryReader(query);
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    Log.WriteLog(" . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /// <summary>
        /// Input box allow user to put new info
        /// </summary>
        /// <param name="title"></param>
        /// <param name="promptText"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal static DialogResult InputBox(string title, string promptText, ref string value)
        {
            DialogResult dialogResult = new DialogResult();
            try
            {
                Form form = new Form();
                System.Windows.Forms.Label label = new System.Windows.Forms.Label();
                TextBox textBox = new TextBox();
                Button buttonOk = new Button();
                Button buttonCancel = new Button();

                form.Text = title;
                label.Text = promptText;
                textBox.Text = value;

                buttonOk.Text = "OK";
                buttonCancel.Text = "Cancel";
                buttonOk.DialogResult = DialogResult.OK;
                buttonCancel.DialogResult = DialogResult.Cancel;

                label.SetBounds(9, 20, 372, 13);
                textBox.SetBounds(12, 36, 372, 20);
                buttonOk.SetBounds(228, 72, 75, 23);
                buttonCancel.SetBounds(309, 72, 75, 23);

                label.AutoSize = true;
                textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
                buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

                form.ClientSize = new Size(396, 107);
                form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
                form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
                form.FormBorderStyle = FormBorderStyle.FixedDialog;
                form.StartPosition = FormStartPosition.CenterScreen;
                form.MinimizeBox = false;
                form.MaximizeBox = false;
                form.AcceptButton = buttonOk;
                form.CancelButton = buttonCancel;
                dialogResult = form.ShowDialog();

                value = textBox.Text;
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return dialogResult;
        }

        /// <summary>
        /// Parse Uint to color
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        internal static Color UIntToColor(uint color)
        {
            try
            {
                byte r = (byte)(color >> 16);
                byte g = (byte)(color >> 8);
                byte b = (byte)(color >> 0);
                return Color.FromArgb(r, g, b);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return Color.White;
            }

        }
        /// <summary>
        /// Parse color to Uint
        /// </summary>
        /// <param name="color"></param>
        /// <returns></returns>
        internal static uint ColorToUInt(Color color)
        {
            try
            {
                return (uint)((color.R << 0) | ((color.G << 8)) | ((color.B << 16)));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return 0;
            }
        }

        /// <summary>
        /// Read XML file send on the path and search the node name
        /// </summary>
        /// <param name="path"></param>
        /// <param name="nodeName"></param>
        /// <returns></returns>
        internal static string ReadXMLFile(string path, string nodeName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);

                XmlNode node = doc.DocumentElement.SelectSingleNode(nodeName);

                string text = node.InnerText;

                return text;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Commun. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }

        /// <summary>
        /// Write the value on XMLfile send by the path, in the nodename 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <param name="nodeName"></param>
        internal static void WriteXMLFile(string path, string value, string nodeName)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(path);
                XmlNode root = FindNode(nodeName, doc.DocumentElement);
                root.LastChild.InnerText = value;
                doc.Save(path);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        internal static XmlNode FindNode(string nodeName, XmlNode root1)
        {
            try
            {
                XmlNode root2 = root1.FirstChild;
                if (root2.Name.Contains(nodeName))
                {
                    return root2;
                }
                else
                {
                    FindNode(nodeName, root2);
                }
                return root2;
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Check if the name already exist, to prevent user to put the same name in the same table
        /// </summary>
        /// <param name="newName"></param>
        /// <param name="table"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        internal static bool NameExists(string newName, string table, string fieldName)
        {
            try
            {
                string query = "Select * FROM " + table + " Where " + fieldName + " = '" + newName + "';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (ListHasValue(list))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Return the number of row od the table send
        /// </summary>
        /// <param name="table"></param>
        /// <returns></returns>
        internal static int GetNbRow(string table)
        {
            try
            {
                string query = "Select count(ID) from " + table + ";";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("Count(ID)", query);
                if (ListHasValue(list))
                {
                    return list[0];
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return 0;

            }
        }

        /// <summary>
        /// Clean the data to adjust the index of the table
        /// </summary>
        /// <param name="table"></param>
        internal static void CleanDataTableSeqId(string table)
        {
            try
            {
                int NbRow = GetNbRow(table);
                int MaxId = GetMaxId("ID", table);
                string query = "";

                // Get all the ID from the table
                query = "Select ID From " + table + ";";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);

                // put to all IDs the MaxID + iteration so we are sure they have a new and unique ID
                // thats is bigger than the max ID
                if (list.Count == NbRow)
                {
                    for (int i = 1; i <= NbRow; i++)
                    {
                        query = "Update " + table + " Set ID = '" + (MaxId + i) + "' Where ID = '" + list[i - 1] + "';";
                        SQlQueryExecuter.RunQuery(query);
                    }
                }

                // Get the new list os IDs of the table
                query = "Select ID From " + table + ";";
                list = SQlQueryExecuter.RunQueryReaderInt("ID", query);

                //Reupdate all the IDs to put this time the right ID (e.g if it's the fisrt record, the ID will be 1, 
                // the second record will have 2 as ID and so on.
                if (list.Count == NbRow)
                {
                    for (int i = 1; i <= NbRow; i++)
                    {
                        query = "Update " + table + " Set ID = '" + i + "' Where ID = '" + list[i - 1] + "';";
                        SQlQueryExecuter.RunQuery(query);
                    }
                }

                //update the SeqID for the next ID
                query = "Update SQlite_sequence SET Seq = '" + NbRow + "'  Where name ='" + table + "';";
                SQlQueryExecuter.RunQuery(query);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        internal static Color GetColor(string code)
        {
            try
            {
                Color color1 = new Color();
                string query = "Select * From Couleur;";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt(code, query);
                if (ListHasValue(list))
                {
                    color1 = Color.FromArgb(list[0]);
                }
                return color1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return Color.Transparent;
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        static internal int NbPhase(string phaseQuery)
        {
            try
            {

                string[] array = phaseQuery.Split(',');
                return array.Length;
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return 0;
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        static internal string GetPhase(string actname,bool withAs = true)
        {
            string query = "Select * From Ate_Catphase Where Nom = '" + actname + "'";
            List<int> list = new List<int>();
            List<int> listTotal = new List<int>();
            List<string> listnom = new List<string>();
            string nom = "";
            try
            {
                for (int i = 1; i <= 40; i++)
                {
                    try
                    {
                        list = SQlQueryExecuter.RunQueryReaderInt("Ph" + i, query);
                        if (Commun.ListHasValue(list))
                        {
                            listTotal.Add(list[0]);
                        }
                    }
                    catch (Exception Ex)
                    {

                    }
                }

                if (ListHasValue(listTotal))
                {
                    listnom = GetNomChamp(listTotal, listTotal[0]);
                }
                int index = 1;
                nom = "";
                if (withAs)
                {
                    foreach (string item in listnom)
                    {
                        nom = nom + "Ph" + index + " as '" + item + "',";
                        index++;
                    }
                }
                else
                {
                    foreach (string item in listnom)
                    {
                        nom = nom + item + ",";
                        index++;
                    }
                }
                if (nom.Length > 1) nom = nom.Remove(nom.Length - 1, 1);
            }
            catch (Exception Ex)
            {

            }
            return nom;
        }

        static internal string GetPhaseNoPh(string actname, bool withAs = true)
        {
            string query = "Select * From Ate_Catphase Where Nom = '" + actname + "'";
            List<int> list = new List<int>();
            List<int> listTotal = new List<int>();
            List<string> listnom = new List<string>();
            string nom = "";
            try
            {
                for (int i = 1; i <= 40; i++)
                {
                    try
                    {
                        list = SQlQueryExecuter.RunQueryReaderInt("Ph" + i, query);
                        if (Commun.ListHasValue(list))
                        {
                            listTotal.Add(list[0]);
                        }
                    }
                    catch (Exception Ex)
                    {

                    }
                }

                if (ListHasValue(listTotal))
                {
                    listnom = GetNomChamp(listTotal, listTotal[0]);
                }
                int index = 1;
                nom = "";
                if (withAs)
                {
                    foreach (string item in listnom)
                    {
                        nom = nom + index + " as '" + item + "',";
                        index++;
                    }
                }
                else
                {
                    foreach (string item in listnom)
                    {
                        nom = nom + item + ",";
                        index++;
                    }
                }
                if (nom.Length > 1) nom = nom.Remove(nom.Length - 1, 1);
            }
            catch (Exception Ex)
            {

            }
            return nom;
        }

        static internal int GetYear()
        {
            int An0 = 0;
            string query = "Select An_0 From Agri_DefSim Where IdExploitations = '" + GetIdExpl() + "'";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
            if (Commun.ListHasValue(list))
            {
                An0 = list[0];
            }
            else An0 = DateTime.Now.Year;
            return An0;
        }

        static internal List<string> GetNomChamp(List<int> list, int value)
        {
            List<string> listchamp = new List<string>();
            try
            {
                int index = 0;
                string champ = "";
                for (int j =1; j < list.Count;j++)
                {
                    if (j == list[index] && j + 1 == NextValue(list, index))
                    {
                        champ = "Ph" + j;
                        listchamp.Add("Ph" + j);
                        index++;
                    }
                    else
                    {
                        if (hasNextValue(list, index))
                        {
                            champ = "Ph" + list[index] + ".." + NextValue(list, index);
                            listchamp.Add(champ);
                            index++;
                        }
                        else
                        {
                            champ = "Ph" + list[index] + "..";
                            listchamp.Add(champ);
                            index++;
                        }
                    }

                }
                listchamp.Add("Ph" + list.Last().ToString() + "..");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return listchamp;
        }

        static private bool hasNextValue(List<int> list, int index)
        {

            if (list.Count > index + 1)
                return true;
            else
                return false;
        }

        static private int NextValue(List<int> list, int index)
        {
            return list[index + 1];

        }

        static private string PhNotNull()
        {
            string ph = "";
            for (int i = 1; i <= 40; i++)
            {
                ph = ph + "Ph" + i + "is not null And";
            }
            ph = ph.Remove(ph.Length - 3, 3);
            return ph;
        }

    }
}


