using OMEGA.Data_Classes;
using MapWinGIS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Forms.Territory_Forms
{
    public partial class ExportForm : Form
    {
        private int langue = Properties.Settings.Default.Langue;
        private List<InfoShapeFile> listShapeFile = new List<InfoShapeFile>();
        private bool ExportOk = false;
        private AxMapWinGIS.AxMap MainMap;

        public ExportForm(List<InfoShapeFile> list,AxMapWinGIS.AxMap map)
        {
            try
            {
                InitializeComponent();
                listShapeFile = list;
                LoadComboBoxType();
                MainMap = map;
                Translate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            try
            {

                folderBrowserDialog1.ShowDialog();
                textBoxpath.Text = folderBrowserDialog1.SelectedPath;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void Translate()
        {
            button1.Text = Translation.Translate("Export", langue);
           // labelAnd.Text = Translation.Translate("And", langue);
            labelFolder.Text = Translation.Translate("Directory to export", langue);
        }

        private void LoadComboBoxType()
        {
            try
            {
                List<string> list = new List<string>();
                list.Add(Translation.Translate("Main map", langue));
                list.Add(Translation.Translate("Farm", langue));
                list.Add(Translation.Translate("Activity", langue));
                comboBoxType.DataSource = list;
                comboBoxType.Text = "";
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadComboxlist()
        {
            try
            {
                string query = "";
                if (comboBoxType.Text == Translation.Translate("Main map", langue) || comboBoxType.Text == "Main map")
                {
                    query = "Select Nom From Shp_Info Where type = 'main map';";
                    //labelAnd.Visible = false;
                    //comboBoxOptExpl.Visible = false;
                }
                if (comboBoxType.Text == Translation.Translate("Farm", langue) || comboBoxType.Text == "Farm")
                {
                    query = "Select Nom From Exploitation;";
                    //labelAnd.Visible = true;
                    //comboBoxOptExpl.Visible = true;
                    //LoadComboxOptionExploi();
                }
                if (comboBoxType.Text == Translation.Translate("Activity", langue) || comboBoxType.Text == "Activity")
                {
                    query = "Select Nom From Activite;";
                    //labelAnd.Visible = false;
                    //comboBoxOptExpl.Visible = false;
                }
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                comboBoxList.DataSource = list;
                comboBoxList.Text = "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadComboxOptionExploi()
        {
            try
            {
                List<string> list = new List<string>();
                list.Add(Translation.Translate("Its Activities", langue));
                list.Add(Translation.Translate("Its farms", langue));
                list.Add(Translation.Translate("Its farms and its activities", langue));
                list.Add("");
                //comboBoxOptExpl.DataSource = list;
                //comboBoxOptExpl.Text = "";
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void comboBoxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadComboxlist();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                string value = comboBoxList.Text;
                if (value == "")
                {
                    MessageBox.Show(Translation.Translate("Please choose an item to export",langue));
                }
                else
                {
                   
                        copyfiles();
                    
                    //if (comboBoxType.Text == Translation.Translate("Farm", langue))
                    //{
                    //    if (comboBoxOptExpl.Text == Translation.Translate("Its Activities", langue))
                    //    {
                    //        query = "Select Activite.Nom From Activite " +
                    //            " JOIN Caract_Exploitation on Caract_Exploitation.IdActivite = Activite.ID" +
                    //            " JOIN Exploitation On Caract_Exploitation.IdExploitation = Exploitation.ID " +
                    //            " Where EXploitation.Nom = '" + comboBoxList.Text + "';";
                    //        List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    //        bool open = false;
                    //        Shapefile ExploitationShapefile = new Shapefile();
                    //        foreach (InfoShapeFile MyShapefile in listShapeFile)
                    //        {
                    //            if (!open)
                    //            {
                    //                 ExploitationShapefile.Open(Properties.Settings.Default.DataPath + "//shp//" + comboBoxList.Text + ".shp");
                    //                // open = ExploitationShapefile.Open(@"C:\Users\zapha\Desktop\OMEGA\" + comboBoxList.Text + ".shp");
                    //            }
                    //            if (list.Contains(MyShapefile.FileName))
                    //            {
                    //                Shapefile newShapefile = new Shapefile();
                    //                //newShapefile.Open(Properties.Settings.Default.DataPath + "//shp//" + shapefile.FileName + ".shp");
                    //                bool open2 = newShapefile.Open(Properties.Settings.Default.DataPath + "//shp//" + MyShapefile.FileName + ".shp");
                    //                //open2 = newShapefile.Open(@"C:\Users\pha\Desktop\Cours_sig_unikis_2014\09_madagascar\MDG_adm3.shp");
                    //                object result = new object();
                    //                object result2 = new object();
                    //                bool select = ExploitationShapefile.SelectShapes(MainMap.Extents,0,SelectMode.INTERSECTION,ref result);
                    //                bool select2 = newShapefile.SelectShapes(MainMap.Extents, 0, SelectMode.INCLUSION, ref result2);
                    //                Shapefile mergeShapefile = new Shapefile();
                    //                mergeShapefile = newShapefile.Merge(true, ExploitationShapefile, true);
                    //            }
                    //        }
                    //    }
                    //}
                }
                ShowMessage();
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void copyfiles()
        {
            try
            {
                string sourcepath = Properties.Settings.Default.DataPath + "\\shp";
                //sourcepath = @"C:\Users\zapha\Desktop\OMEGA";
                string targetpath = textBoxpath.Text;
                string[] arrfiles = Directory.GetFiles(sourcepath);
                for (int i = 0; i < arrfiles.Length; i++)
                {
                    string[] arrSplit = arrfiles[i].Split('.');
                    int taille = arrSplit.Length;
                    string[] arrSplit2 = arrSplit[taille - 2].Split(new string[] { "\\" },StringSplitOptions.None);
                    if (arrSplit2.Last() == (comboBoxList.Text))
                    {
                        File.Copy(sourcepath + "\\" + comboBoxList.Text + ".dbf", targetpath + "\\" + comboBoxList.Text + ".dbf");
                        File.Copy(sourcepath + "\\" + comboBoxList.Text + ".shp", targetpath + "\\" + comboBoxList.Text + ".shp");
                        File.Copy(sourcepath + "\\" + comboBoxList.Text + ".prj", targetpath + "\\" + comboBoxList.Text + ".prj");
                        File.Copy(sourcepath + "\\" + comboBoxList.Text + ".shx", targetpath + "\\" + comboBoxList.Text + ".shx");
                        ExportOk = true;
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void ShowMessage()
        {
            if (ExportOk)
            {
                MessageBox.Show(Translation.Translate("Export successfully done.", langue));
                this.Close();
            }
            else
            {
                MessageBox.Show(Translation.Translate("Error during export. See log for more information", langue));
            }

        }

    }
}
