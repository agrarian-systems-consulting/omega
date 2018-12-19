using OMEGA.Data_Classes;
using OMEGA.SQLQuery;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;

namespace OMEGA.Forms
{
    internal partial class ImportGPSDataForm : Form
    {
        private List<GPSPoint> ListInfoGPS = new List<GPSPoint>();
        private int langue = Properties.Settings.Default.Langue;

        internal ImportGPSDataForm()
        {
            InitializeComponent();
            Translate();
        }

        private void buttonGPSdata_Click(object sender, EventArgs e)
        {

        }

        private void Translate()
        {
            try
            {
                buttonImportGPX.Text = Translation.Translate("Import gpx file", langue);
                buttonImportPoint.Text = Translation.Translate("Import selected points", langue);
                checkBox1.Text = Translation.Translate("Select all", langue);
                this.Text = Translation.Translate("Import points", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonImportGPX_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog sfd = new OpenFileDialog();
                sfd.Title = "Ouvrir un fichier GPS eXchange";
                sfd.InitialDirectory = "|DataDirectory|";
                sfd.Filter = "GPS eXchange (*.gpx)|*.gpx";
                sfd.FilterIndex = 1;
                sfd.Multiselect = true;
                sfd.RestoreDirectory = true;
                DialogResult dialogResult = new DialogResult();
                dialogResult = sfd.ShowDialog();
                if (dialogResult == DialogResult.OK)
                    loadGPX(sfd.FileName);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void loadGPX(string file)
        {
            try
            {

                XmlNode node;
                XmlDocument xmldoc = new XmlDocument();
                XmlDocument doc;
                string n;
                DateTime date;


                checkedListBox1.Items.Clear();

                xmldoc.Load(file);
                node = xmldoc.FirstChild;
                if (node.Name != "xml")
                {
                    MessageBox.Show(Translation.Translate("This xml file is incompatible", langue));
                    return;
                }
                node = node.NextSibling;
                if (node.Name != "gpx")
                {
                    MessageBox.Show(Translation.Translate("This gpx file is incompatible", langue));
                    return;
                }
                if (!node.HasChildNodes)
                {
                    MessageBox.Show(Translation.Translate("This gpx file is empty", langue));
                    return;
                }

                if (node.OwnerDocument.GetElementsByTagName("trk").Count > 0) //trace
                {
                  

                    foreach (XmlElement elem1 in node.OwnerDocument.GetElementsByTagName("trk"))
                    {
                         doc = elem1.OwnerDocument;
                        // get <name>
                        if (elem1.GetElementsByTagName("name").Count > 0)
                            n = elem1.GetElementsByTagName("name").Item(0).InnerText;
                        else
                            n = "tracé sans nom";
                        // get #<trkpt>
                        checkedListBox1.Items.Add(n+ " : tracé de " + elem1.GetElementsByTagName("trkpt").Count + " points.");
                        GPSPoint InfoGPS = new GPSPoint();
                        foreach (XmlElement elem2 in node.OwnerDocument.GetElementsByTagName("trkpt"))
                        {
                            InfoGPS.Name = n;
                            double temp;
                            Double.TryParse(elem2.Attributes["lat"].InnerText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out temp);
                            InfoGPS.Lat = temp;
                            Double.TryParse(elem2.Attributes["lon"].InnerText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out temp);
                            InfoGPS.Lon = temp;
                            if (elem2.GetElementsByTagName("ele").Count > 0)
                            {
                                Double.TryParse(elem2.GetElementsByTagName("ele").Item(0).InnerText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out temp);
                                InfoGPS.Alt = temp;
                            }
                            if (elem2.GetElementsByTagName("time").Count > 0)
                            {
                                InfoGPS.Date = DateTime.Parse(elem2.GetElementsByTagName("time").Item(0).InnerText);
                            }
                            else InfoGPS.Date = DateTime.Now;
                            ListInfoGPS.Add(InfoGPS);
                        }
                    }
                }
                else if (node.OwnerDocument.GetElementsByTagName("rte").Count > 0)
                {
                    foreach (XmlElement elem2 in node.OwnerDocument.GetElementsByTagName("rte"))
                    {
                        doc = elem2.OwnerDocument;
                        // get <name>
                        if (elem2.GetElementsByTagName("name").Count > 0)
                            n = elem2.GetElementsByTagName("name").Item(0).InnerText;
                        else
                            n = "tracé sans nom";
                        // get #<trkpt>
                        checkedListBox1.Items.Add(n+ " : tracé de " + elem2.GetElementsByTagName("trkpt").Count + " points.");
                        GPSPoint InfoGPS = new GPSPoint();
                        foreach (XmlElement elem3 in node.OwnerDocument.GetElementsByTagName("trkpt"))
                        {
                            InfoGPS.Name = n;
                            double temp;
                            Double.TryParse(elem3.Attributes["lat"].InnerText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out temp);
                            InfoGPS.Lat = temp;
                            Double.TryParse(elem3.Attributes["lon"].InnerText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out temp);
                            InfoGPS.Lon = temp;
                            if (elem3.GetElementsByTagName("ele").Count > 0)
                            {
                                Double.TryParse(elem3.GetElementsByTagName("ele").Item(0).InnerText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out temp);
                                InfoGPS.Alt = temp;
                            }
                            if (elem3.GetElementsByTagName("time").Count > 0)
                            {
                                InfoGPS.Date = DateTime.Parse(elem3.GetElementsByTagName("time").Item(0).InnerText);
                            }
                            else InfoGPS.Date = DateTime.Now;
                            ListInfoGPS.Add(InfoGPS);
                        }
                    }
                }
                else
                {
                    if (node.OwnerDocument.GetElementsByTagName("time").Count > 0)
                        date = DateTime.Parse(node.OwnerDocument.GetElementsByTagName("time").Item(0).InnerText);
                    else
                        date = DateTime.Now;

                    foreach (XmlElement elem3 in node.OwnerDocument.GetElementsByTagName("wpt"))
                    {
                        GPSPoint InfoGPS = new GPSPoint();
                        if (elem3.GetElementsByTagName("name").Count == 0)
                        {
                            MessageBox.Show("Ce fichier GPX ne contient pas les noms des points (colonne 'NAME').");
                        }
                        InfoGPS.Name = elem3.GetElementsByTagName("name")[0].InnerText;
                        //InfoGPS.NameShown = s.name
                        double temp;
                        Double.TryParse(elem3.Attributes["lat"].InnerText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture,out temp);
                        InfoGPS.Lat = temp;
                        Double.TryParse(elem3.Attributes["lon"].InnerText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture,out temp);
                        InfoGPS.Lon = temp;

                        if (elem3.GetElementsByTagName("ele").Count > 0)
                        {
                            Double.TryParse(elem3.GetElementsByTagName("ele").Item(0).InnerText, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out temp);
                            InfoGPS.Alt = temp;
                        }
                        if (elem3.GetElementsByTagName("time").Count > 0)
                        {
                            InfoGPS.Date = DateTime.Parse(elem3.GetElementsByTagName("time").Item(0).InnerText);
                        }
                        else InfoGPS.Date = date;
                        //If bdd.ds.GPS.FindByCODE(s.name) IsNot Nothing Then s.nameShown &= " (existe déjà !)"
                        checkedListBox1.Items.Add(InfoGPS.Name + ", lat : " + InfoGPS.Lat +", lon : "+ InfoGPS.Lon);
                        ListInfoGPS.Add(InfoGPS);
                    }
                }
                if (checkedListBox1.Items.Count > 0) buttonImportPoint.Enabled = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= checkedListBox1.Items.Count - 1; i++)
                {
                    checkedListBox1.SetItemChecked(i, checkBox1.Checked);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonImportPoint_Click(object sender, EventArgs e)
        {
            try
            {
                string value = "";
                string query;
                if (Commun.InputBox("Import GPS Point", "Import Name", ref value) == DialogResult.OK)
                {
                    string nomImport = value;
                    query = SQLQueryBuilder.SelectQuery("GPS", "*", "Where Import_name = '" + nomImport + "' ");
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        MessageBox.Show(Translation.Translate("This import already exists. Please choose an other name. Importation canceled", langue));
                        return;
                    }
                    foreach (GPSPoint infoGPS in ListInfoGPS)
                    {

                        // y = lat , x = lon
                        query = "Insert into GPS (CODE,X,Y,ALT,DATE,Import_name,isImport)" +
                            " VALUES ('" + infoGPS.Name + "','" + infoGPS.Lon + "','" + infoGPS.Lat
                            + "','" + infoGPS.Alt + "','" + infoGPS.Date + "','" + nomImport + "','1');";
                        SQlQueryExecuter.RunQuery(query);
                    }
                    ListInfoGPS.Clear();
                }
                MessageBox.Show(Translation.Translate("Import successfully done.", langue));
                Properties.Settings.Default.DoitSauvegarger = true;
                this.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ImportGPSDataForm_Load(object sender, EventArgs e)
        {

        }
    }
}
