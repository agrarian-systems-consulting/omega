using OMEGA.Forms;
using System;
using System.Collections;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace OMEGA
{
    static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        /// 
        
        [STAThread]
        public static void Main(string[] args)
        {
            try
            {
                Properties.Settings.Default.Langue = 1;
                int langue = Properties.Settings.Default.Langue;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                DirectoryInfo dir;
                bool HasData = false;
                // si l'utilisateur double click sur un fichier .omg, c'est ce qui passe en paramètre ici
                // If the user click on .omg file, it's passeb by this parameter : args[]

                if (args.Length > 0)
                {
                    //This propertie is the path where all the data and files are stored
                    Properties.Settings.Default.DataPath = Commun.RemoveExtension(args[0].ToString(), 3);
                    HasData = true;
                   
                    //this propertie is the name if the file opened by the user
                    Properties.Settings.Default.CurrentFile = Commun.GetFileName(args[0].ToString(), true);
                  
                    // on crée un dossier caché contenant le fichier XML et la BBD
                    // Creation of the hidden folder to stream and update new data and user information
                    if (!(Directory.Exists(Properties.Settings.Default.DataPath)))
                    {
                        dir = Directory.CreateDirectory(Properties.Settings.Default.DataPath);
                        dir.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
                    }
                    // on dézipe dans le fichier caché 
                    // unzipe data and files into the hidden folder
                    if (Commun.Decompress(args[0].ToString()))
                    {
                        // on écrit la connectionString
                        // Set the connectionString
                        Properties.Settings.Default.ConnectionString = "Data Source = " + Properties.Settings.Default.DataPath + "\\Info\\Olympe.db";
                        
                        // on écrit le fichier de traduction
                        // Set the translation file
                        Properties.Settings.Default.FichierTraduction = Application.StartupPath + "\\Translation.csv";
                    }
                    else
                    {
                        MessageBox.Show(Translation.Translate("Error during the opening of file : ",langue) + args[0].ToString() + ". You can contact ?? for more information");
                        return;
                    }
                }
                else
                {
                    HasData = false;
                    Properties.Settings.Default.FichierTraduction = Application.StartupPath + "\\Translation.csv";
                }

                SetInfoUserFromFile();

                // on lance l'appli
                Application.Run(new MainForm(HasData));

                // dernière vérification de sauvegarde si l'utilisateur ferme sans sauvegarder
                if (Properties.Settings.Default.DoitSauvegarger)
                {
                    ExitForm exitform = new ExitForm(false);
                    exitform.ShowDialog();
                    switch (exitform.OutPutValue)
                    { // 1 : save and quit, 2 quit no save, 3 cancel
                        case 1:
                            Save(false);
                            Properties.Settings.Default.DoitSauvegarger = false;
                            break;
                        case 2:
                            Properties.Settings.Default.DoitSauvegarger = false;
                            break;
                        default:
                            break;
                    }
                }

                // Delete the hidden folder when closing the app
                if (File.Exists(Application.StartupPath + "\\RunDeleteFolder.bat") && Properties.Settings.Default.DataPath != "")
                {
                    Process.Start(Application.StartupPath + "\\RunDeleteFolder.bat", Properties.Settings.Default.DataPath);
                }
                
               
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /// <summary>
        /// la sauvegarde va recompresser un dans le fichier .omg
        /// </summary>
        /// <param name="SaveAs"></param>
        /// <param name="silent"></param>
        internal static void Save(bool SaveAs, bool silent = false)
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;
                if (!SaveAs)
                {
                    if (Properties.Settings.Default.DataPath == "") return;
                    if (Properties.Settings.Default.CurrentFile == "") return;
                    // MessageBox.Show(Properties.Settings.Default.CurrentFile);
                    if (File.Exists(Properties.Settings.Default.CurrentFile))
                    {
                        try
                        {
                            File.Delete(Properties.Settings.Default.CurrentFile);
                        }
                        catch (Exception Ex)
                        {

                        }
                    }

                    // MessageBox.Show(Commun.RemoveExtension(Properties.Settings.Default.CurrentFile, 3));
                    //if (Directory.Exists(Commun.RemoveExtension(Properties.Settings.Default.CurrentFile, 3)))
                    //{
                    //    Directory.Delete(Commun.RemoveExtension(Properties.Settings.Default.CurrentFile, 3),true);
                    //    MessageBox.Show("delete ok");
                    //}
                    if ( Commun.Compress(Properties.Settings.Default.DataPath+"\\Info", Properties.Settings.Default.CurrentFile))
                    {
                        Commun.Compress(Properties.Settings.Default.DataPath+"\\Shp", Properties.Settings.Default.CurrentFile);
                        Properties.Settings.Default.DoitSauvegarger = false;
                        MessageBox.Show(Translation.Translate("Successfully saved",Properties.Settings.Default.Langue));

                    }
                    else if (!silent) MessageBox.Show(Translation.Translate("Error while saving data",langue));
                    return;
                }
                else
                {
                    // sinon on fait un save As : 
                    SaveFileDialog DialogueSaveFile = new SaveFileDialog();
                    DialogueSaveFile.Filter = "Fichiers OMEGA (*.omg)|*.omg";
                    DialogueSaveFile.RestoreDirectory = true;
                    DialogueSaveFile.ValidateNames = true;
                    DialogueSaveFile.SupportMultiDottedExtensions = false;
                    DialogueSaveFile.CheckFileExists = false;
                    DialogueSaveFile.CheckPathExists = true;
                    DialogueSaveFile.FilterIndex = 1;
                    //OMEGA.Properties.Settings.Default.DataPath = @"C:\ProgramData\OMEGA DATA";

                    // save as, on donne un nom
                    if (DialogueSaveFile.ShowDialog() == DialogResult.OK)
                    {
                        if (Properties.Settings.Default.CurrentFile == string.Empty)
                        {
                            Properties.Settings.Default.CurrentFile = Commun.GetFileName(DialogueSaveFile.FileName, true);
                        }
                        if (Commun.Compress(Properties.Settings.Default.DataPath, Properties.Settings.Default.CurrentFile))
                        {
                            Properties.Settings.Default.DoitSauvegarger = false;
                        }
                        else if (!silent) MessageBox.Show(Translation.Translate("Error while saving data",langue));
                    }
                }
            }
            catch (Exception Ex)
            {
                 if (!silent) MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Program. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
 
        /// <summary>
        /// On ouvre un fichier .omg
        /// </summary>
        internal static bool Open()
        {
            bool fileOpen = false;
            try
            {
                int langue = Properties.Settings.Default.Langue;
                OpenFileDialog DialogueOpenFile = new OpenFileDialog();
                DialogueOpenFile.Filter = "Fichiers OMEGA (*.omg)|*.omg";
                DialogueOpenFile.RestoreDirectory = true;
                DialogueOpenFile.ValidateNames = true;
                DialogueOpenFile.SupportMultiDottedExtensions = false;
                DialogueOpenFile.CheckFileExists = false;
                DialogueOpenFile.CheckPathExists = true;
                DialogueOpenFile.FilterIndex = 1;

                if (DialogueOpenFile.ShowDialog() == DialogResult.OK)
                {
                    if (Commun.Decompress(DialogueOpenFile.FileName))
                    {
                        Properties.Settings.Default.DataPath = Commun.RemoveExtension(DialogueOpenFile.FileName, 3);
                        SetInfoUserFromFile();
                        if (!Directory.Exists(Properties.Settings.Default.DataPath + "\\Shp"))
                        {
                            DirectoryInfo dir = Directory.CreateDirectory(Properties.Settings.Default.DataPath + "\\Shp");
                            dir.Attributes = FileAttributes.Hidden;
                        }
                        Properties.Settings.Default.ConnectionString = "Data Source = " + Properties.Settings.Default.DataPath + "\\Info\\Olympe.db";
                        Properties.Settings.Default.CurrentFile = Properties.Settings.Default.DataPath + ".omg";
                        fileOpen = true;
                    }
                    else
                    {
                        MessageBox.Show(Translation.Translate("Error while opening file.",langue));
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, "Class Program. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return fileOpen;
        }
        
        /// <summary>
        /// On crée un fichier avec une nouvelle BDD vide
        /// </summary>
        internal static bool  New()
        {
            bool newfile = false;
            try
            {
                SaveFileDialog DialogueSaveFile = new SaveFileDialog();
                DialogueSaveFile.Filter = "Fichiers OMEGA (*.omg)|*.omg";
                DialogueSaveFile.RestoreDirectory = true;
                DialogueSaveFile.ValidateNames = true;
                DialogueSaveFile.SupportMultiDottedExtensions = false;
                DialogueSaveFile.CheckFileExists = false;
                DialogueSaveFile.CheckPathExists = true;
                DialogueSaveFile.FilterIndex = 1;
                int langue = Properties.Settings.Default.Langue;
                string script = "";

                if (DialogueSaveFile.ShowDialog() == DialogResult.OK)
                {
                    Properties.Settings.Default.CurrentFile = Commun.GetFileName(DialogueSaveFile.FileName, true);
                    Properties.Settings.Default.DataPath = Commun.RemoveExtension(DialogueSaveFile.FileName, 3);

                    if (Commun.CreateSQLDatabase(Properties.Settings.Default.DataPath, "Olympe.db"))
                    {
                        if (!Directory.Exists(Properties.Settings.Default.DataPath))
                        {
                            DirectoryInfo dir = Directory.CreateDirectory(Properties.Settings.Default.DataPath);
                            dir.Attributes = FileAttributes.Hidden;
                        }
                        else
                        {
                            DirectoryInfo dir = new DirectoryInfo(Properties.Settings.Default.DataPath);
                            dir.Attributes = FileAttributes.Hidden;
                        }
                        //MessageBox.Show(Directory.GetCurrentDirectory());
                        if (!Directory.Exists(Properties.Settings.Default.DataPath + "\\Shp"))
                        {
                            DirectoryInfo dir = Directory.CreateDirectory(Properties.Settings.Default.DataPath + "\\Shp");
                            dir.Attributes = FileAttributes.Hidden;
                        }
                        if (!Directory.Exists(Properties.Settings.Default.DataPath + "\\Info"))
                        {
                            DirectoryInfo dir = Directory.CreateDirectory(Properties.Settings.Default.DataPath + "\\Info");
                            dir.Attributes = FileAttributes.Hidden;
                        }
                        if (File.Exists(Application.StartupPath + "\\OMEGA.xml"))
                        {
                            File.Copy(Application.StartupPath + "\\OMEGA.xml", Properties.Settings.Default.DataPath + "\\Info\\OMEGA.xml");
                        }
                        if (File.Exists(Application.StartupPath + "\\Olympe.sql"))
                        {
                            script = File.ReadAllText(Application.StartupPath + "\\Olympe.sql");

                        }
                        else MessageBox.Show(Translation.Translate("Error during the creation of new file, unable to create an empty database. Please contact ?? to solve this probleme.", langue));

                        Properties.Settings.Default.ConnectionString = "Data source = " + Properties.Settings.Default.DataPath + "\\Info\\Olympe.db";
                        SQlQueryExecuter.RunQuery(script);
                        newfile = true;
                    }
                    else MessageBox.Show(Translation.Translate("Error while creating file : ",langue) + Properties.Settings.Default.DataPath + "\\Info\\Olympe.db");
                    Commun.Compress(Properties.Settings.Default.DataPath, Properties.Settings.Default.DataPath + ".omg");
                    Properties.Settings.Default.DoitSauvegarger = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Program. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return newfile;
        }

        /// <summary>
        /// Lecture des informations provenant du fichier de configuration 
        /// </summary>
        private static void SetInfoUserFromFile()
        {
            try
            {
                if (Properties.Settings.Default.DataPath.Length > 1)
                {
                    XPathNav Xmlreader = new XPathNav(Properties.Settings.Default.DataPath + "\\Info\\OMEGA.xml");
                    string item = Xmlreader.SearchXPathNavigator("/OMEGA/Langue");
                    int.TryParse(item, out int langue);
                    Properties.Settings.Default.Langue = langue;
                    item = Xmlreader.SearchXPathNavigator("/OMEGA/UserView");
                    if (item == "") item = "standard";
                    Properties.Settings.Default.UserView = item;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Program. Function : " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

    }
}
