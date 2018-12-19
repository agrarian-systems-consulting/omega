using MapWinGIS;
using OMEGA.Data_Classes;
using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = System.Windows.Forms.Label;

namespace OMEGA.Forms.Territory_Forms
{
    public partial class PrintMapForm : Form
    {
        private Bitmap memoryImage;
        private List<InfoShapeFile> mListShapeFile = new List<InfoShapeFile>();
        private int langue = Properties.Settings.Default.Langue;

        public PrintMapForm()
        {
            try
            {
                InitializeComponent();
                this.Text = Translation.Translate("Preview", langue);
                LoadMap();
                axMap1.Redraw2(tkRedrawType.RedrawAll);
                AddLegend();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadMap()
        {
            try
            {

                string query = SQLQueryBuilder.SelectQuery("SHP_Info", "*", "Where MainMap LIKE 'True'");
                LoadMap(query, true);
                query = SQLQueryBuilder.SelectQuery("SHP_Info", "*", "Where MainMap LIKE 'False' and IdExploitation is not null and IdExploitation <> '0'");
                LoadMap(query, false);
                query = SQLQueryBuilder.SelectQuery("SHP_Info", "*", "Where MainMap LIKE 'False' and IdActivite is not null and IdActivite <> '0'");
                LoadMap(query, false);
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadMap(string query, bool mainmap)
        {
            try
            {
                List<string> listpath = SQlQueryExecuter.RunQueryReaderStr("path", query);
                List<string> listname = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                List<string> listtype = SQlQueryExecuter.RunQueryReaderStr("type", query);
                List<int> listcolor = SQlQueryExecuter.RunQueryReaderInt("Color_ARGB", query);
                List<int> listid = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                List<int> ListidExploitation = SQlQueryExecuter.RunQueryReaderInt("IdExploitation", query);
                List<int> ListidActivite = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);
                List<int> ListPosition = SQlQueryExecuter.RunQueryReaderInt("Position", query);
                List<string> ListCode = SQlQueryExecuter.RunQueryReaderStr("Code_point", query);

                foreach (string item in listpath)
                {
                    Panel panel = new Panel();
                    //AddShapeFile(item, listname[listpath.IndexOf(item)], mainmap, 0, listtype[listpath.IndexOf(item)],
                    // listid[listpath.IndexOf(item)], listcolor[listpath.IndexOf(item)], ListPosition[listpath.IndexOf(item)]);

                    InfoShapeFile infoShapeFile = new InfoShapeFile();
                    infoShapeFile.FilePath = item;
                    infoShapeFile.FileName = listname[listpath.IndexOf(item)];
                    infoShapeFile.MainMap = mainmap;
                    infoShapeFile.Id = listid[listpath.IndexOf(item)];
                    infoShapeFile.visible = true;
                    infoShapeFile.type = listtype[listpath.IndexOf(item)];
                    infoShapeFile.ColorARGB = listcolor[listpath.IndexOf(item)];
                    infoShapeFile.Position = ListPosition[listpath.IndexOf(item)];

                    string[] array = ListCode[listpath.IndexOf(item)].Split('@');
                    List<string> list = new List<string>();
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (array[i].Length != 0) list.Add(array[i]);
                    }
                    infoShapeFile.ListPoints = list;

                    Shapefile shapefile = new Shapefile();
                    if (Commun.LoadShapeFile(item))
                    {
                        var open = shapefile.Open(item);
                        if (!open)
                        {
                            MessageBox.Show(shapefile.ErrorMsg[shapefile.LastErrorCode]);
                            continue;
                        }
                        infoShapeFile.NoLayer = axMap1.AddLayer(shapefile, true);
                        axMap1.ZoomToMaxVisibleExtents();
                        shapefile.GeoProjection.SetWgs84();
                        axMap1.set_LayerVisible(infoShapeFile.NoLayer, true);
                        axMap1.Redraw();
                        Color color1 = new Color();
                        if (infoShapeFile.ColorARGB == 0)
                        {
                            color1 = Commun.UIntToColor(shapefile.DefaultDrawingOptions.FillColor);
                            infoShapeFile.ColorARGB = color1.ToArgb();
                        }
                        else
                        {
                            color1 = Color.FromArgb(infoShapeFile.ColorARGB);
                            shapefile.DefaultDrawingOptions.FillColor = Commun.ColorToUInt(color1);
                            shapefile.DefaultDrawingOptions.LineColor = Commun.ColorToUInt(color1);
                        }
                        mListShapeFile.Add(infoShapeFile);
                    }
                    else
                    {
                        // MessageBox.Show("Error during Loading the shapefile : " + infoShapeFile.FileName + ".");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void AddLegend()
        {//Ajouter dessin devant le label pour void différence entre points/ligne et polygone
            try
            {
                int compteur = 1;
                foreach (InfoShapeFile shapefile in mListShapeFile)
                {
                    Label label = new Label();
                    label.Text = shapefile.FileName + ".shp";
                    label.Font = new Font("Corbel", 10);
                    label.ForeColor = Color.FromArgb(shapefile.ColorARGB);
                    label.Location = new System.Drawing.Point(200, compteur * 20 + 50);
                    label.AutoSize = true;
                    axMap1.Controls.Add(label);
                    compteur++;
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                buttonOK.Visible = false;
                buttonCancel.Visible = false;
                axMap1.ShowZoomBar = false;
                Graphics myGraphics = axMap1.CreateGraphics();
                Size s = axMap1.Size;
                string path = "";
                axMap1.Location = new System.Drawing.Point(0, 0);
                memoryImage = new Bitmap(axMap1.Size.Width, axMap1.Size.Height, myGraphics);
                Graphics memoryGraphics = Graphics.FromImage(memoryImage);
                memoryGraphics.CopyFromScreen(axMap1.Location.X, axMap1.Location.Y, 0, 0, s);
                string name = "";
                DialogResult result = Commun.InputBox(Translation.Translate("Export Map", langue), Translation.Translate("Please, choice a name for the map", langue), ref name);
                if (result == DialogResult.OK)
                {
                    path = Directory.GetDirectoryRoot(Properties.Settings.Default.DataPath);
                    string[] array = Properties.Settings.Default.DataPath.Split(new string[] { "\\" }, StringSplitOptions.None);
                    for (int i = 0; i < array.Length; i++)
                    {
                        if (i == 0) continue;
                        if (i == array.Length - 1) break;
                        path = path + array[i] + "\\";
                    }
                    if (Directory.Exists(path))
                    {
                        memoryImage.Save(path + name + ".png", ImageFormat.Png);
                        System.Diagnostics.Process.Start(path + name + ".png");
                    }
                    else // ajouter autre options
                    {
                        //??S
                    }
                }
                this.Close();
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }
    }
}
