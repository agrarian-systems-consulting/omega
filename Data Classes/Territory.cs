using MapWinGIS;
using OMEGA.Forms;
using OMEGA.Forms.Territory_Forms;
using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OMEGA.Data_Classes
{
    internal class Territory
    {
        private int langue = Properties.Settings.Default.Langue;
        private TerritoryForm mTerritoryForm;
        private List<InfoShapeFile> mListShapeFile = new List<InfoShapeFile>();
        private List<int> mListeShapefileToDelete = new List<int>();
        private CheckedListBox currentListBox;
        private System.Windows.Forms.Label currentLabel;
        private MyCheckedListBox mylistbox = new MyCheckedListBox();
        private int compteur = 0;
        private CheckData checkData = new CheckData();
        internal bool dataModified = false;

        internal Territory(TerritoryForm form)
        {
            mTerritoryForm = form;
            LoadMap();
        }

        private void LoadMap()
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("SHP_Info", "*", "Where MainMap LIKE 'True'");
                LoadMap(query, true);
                ScrollBar vScrollBar1 = new VScrollBar();
                vScrollBar1.Dock = DockStyle.Right;
                vScrollBar1.Scroll += ScrollPanelsTerritory;
                mTerritoryForm.panelColorsAll.Controls.Add(vScrollBar1);
                query = SQLQueryBuilder.SelectQuery("SHP_Info", "*", "Where MainMap LIKE 'False' and IdExploitation is not null and IdExploitation <> '0'");
                LoadMap(query, false);
                query = SQLQueryBuilder.SelectQuery("SHP_Info", "*", "Where MainMap LIKE 'False' and IdActivite is not null and IdActivite <> '0'");
                LoadMap(query, false);
                InitLayerToPosition();
                SetLayerToPosition();
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
                            MessageBox.Show(shapefile.ErrorMsg[shapefile.LastErrorCode] );
                            continue;
                        }
                        infoShapeFile.NoLayer = mTerritoryForm.axMap1.AddLayer(shapefile, true);
                        mTerritoryForm.axMap1.ZoomToMaxVisibleExtents();
                        //shapefile.GeoProjection.SetWgs84();
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
                        SetnewPanel(infoShapeFile.FileName, color1);
                        compteur++;
                        if (infoShapeFile.MainMap)
                        {
                            mTerritoryForm.checkedListBoxTerritoire.Items.Add(infoShapeFile.FileName, true);
                        }
                        else
                        {
                            if (infoShapeFile.type.Contains("Farm"))
                            {
                                mTerritoryForm.checkedListBoxExploitation.Items.Add(infoShapeFile.FileName, true);
                                infoShapeFile.IdActExploi = ListidExploitation[listpath.IndexOf(item)];
                            }
                            else
                            {
                                mTerritoryForm.checkedListBoxActivite.Items.Add(infoShapeFile.FileName, true);
                                infoShapeFile.IdActExploi = ListidActivite[listpath.IndexOf(item)];
                            }
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

        #region Complexe GUI

        internal void toolStripaddClick(object sender, EventArgs e)
        {
            try
            {
                AddShapeFileForm addShapeFileForm = new AddShapeFileForm();
                addShapeFileForm.ShowDialog();

                if (addShapeFileForm.DialogResult == DialogResult.OK)
                {
                    if (!addShapeFileForm.checkBoxMainMap.Checked)
                    {
                        if (addShapeFileForm.comboBoxtype.SelectedIndex == 7)
                        {
                            if (!checkData.DataExist(addShapeFileForm.comboBoxname.Text, "Exploitation"))
                            {
                                checkData.AddData(addShapeFileForm.comboBoxname.Text, addShapeFileForm.comboBoxtype.SelectedIndex, "Exploitation");
                            }
                        }
                        else
                        {
                            if (!checkData.DataExist(addShapeFileForm.comboBoxname.Text, "Activite"))
                            {
                                checkData.AddData(addShapeFileForm.comboBoxname.Text, addShapeFileForm.comboBoxtype.SelectedIndex, "Activite");
                            }
                        }
                    }

                    AddShapeFile(addShapeFileForm.textBoxLoad.Text, addShapeFileForm.textBoxName.Text, addShapeFileForm.checkBoxMainMap.Checked,
                        addShapeFileForm.comboBoxtype.SelectedIndex, "");
                    compteur++;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Program. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void AddShapeFile(string shapefilepath, string shapefilename, bool mainmap, int idtype, string type,
            int id = 0, int color = 0, int position = 0, List<string> list = null)
        {
            try
            {
                Shapefile territoryShapeFile = new Shapefile();
                bool open = territoryShapeFile.Open(shapefilepath);
                if (!open)
                {
                    MessageBox.Show(territoryShapeFile.ErrorMsg[territoryShapeFile.LastErrorCode] + ". Creation canceled");
                    return;
                }
                int handler1 = mTerritoryForm.axMap1.AddLayer(territoryShapeFile, true);
                territoryShapeFile = AddAttributField(territoryShapeFile);
                mTerritoryForm.axMap1.set_LayerVisible(handler1, true);
                mTerritoryForm.axMap1.Redraw2(tkRedrawType.RedrawAll);
                mTerritoryForm.axMap1.Redraw();
                InfoShapeFile infoShapeFile = new InfoShapeFile();
                infoShapeFile.NoLayer = handler1;
                infoShapeFile.FilePath = shapefilepath;
                infoShapeFile.FileName = shapefilename;
                infoShapeFile.MainMap = mainmap;
                infoShapeFile.Id = id;
                infoShapeFile.visible = true;
                infoShapeFile.ColorARGB = color;
                infoShapeFile.Position = position;
                infoShapeFile.ListPoints = list;


                if (infoShapeFile.MainMap)
                {
                    mTerritoryForm.checkedListBoxTerritoire.Items.Add(shapefilename, true);
                    infoShapeFile.type = "main map";
                }
                else
                {
                    string table;
                    if (idtype == 6)
                    {
                        mTerritoryForm.checkedListBoxExploitation.Items.Add(shapefilename, true);
                        table = "Exploitation";
                    }
                    else
                    {
                        mTerritoryForm.checkedListBoxActivite.Items.Add(shapefilename, true);
                        table = "Activite";
                    }
                    infoShapeFile.IdActExploi = SQLQueryBuilder.FindId(table, "ID", "Nom", shapefilename);
                    infoShapeFile.type = type;
                }
                Color color1 = Commun.UIntToColor(territoryShapeFile.DefaultDrawingOptions.FillColor);
                mListShapeFile.Add(infoShapeFile);
                mTerritoryForm.axMap1.ZoomToMaxVisibleExtents();
                SetnewPanel(infoShapeFile.FileName, color1);
                dataModified = true;
                if (!mTerritoryForm.Text.Contains("*"))
                {
                    mTerritoryForm.Text = mTerritoryForm.Text + "*";
                }
                mTerritoryForm.panelColorsAll.Refresh();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private Shapefile AddAttributField(Shapefile MyshapeFile)
        {
            try
            {
                if (!MyshapeFile.Table.StartEditingTable(null))
                {
                    MessageBox.Show("Failed to start edit mode1: " + MyshapeFile.Table.ErrorMsg[MyshapeFile.LastErrorCode]);
                }
                if (!MyshapeFile.StartEditingTable(null))
                {
                    MessageBox.Show("Failed to start edit mode2: " + MyshapeFile.Table.ErrorMsg[MyshapeFile.LastErrorCode]);
                }
                if (!MyshapeFile.StartEditingShapes(true))
                {
                    MessageBox.Show("Failed to start edit mode3: " + MyshapeFile.Table.ErrorMsg[MyshapeFile.LastErrorCode]);
                }
                MyshapeFile.EditAddField("Occup1", FieldType.STRING_FIELD, 1, 1);
                MyshapeFile.EditAddField("Occup2", FieldType.STRING_FIELD, 1, 1);
                MyshapeFile.EditAddField("field1", FieldType.STRING_FIELD, 1, 1);
                MyshapeFile.EditAddField("field2", FieldType.STRING_FIELD, 1, 1);
                MyshapeFile.EditAddField("field3", FieldType.STRING_FIELD, 1, 1);
                MyshapeFile.EditAddField("field4", FieldType.STRING_FIELD, 1, 1);
                MyshapeFile.EditAddField("field5", FieldType.STRING_FIELD, 1, 1);
                var save = MyshapeFile.Save();
                if (!save)
                {
                    MessageBox.Show(MyshapeFile.ErrorMsg[MyshapeFile.LastErrorCode] + "." + Translation.Translate("Error during the creation of the attribut field. The shapefile will be created but whitout attribut fields.",langue));
                }
                MyshapeFile.StopEditingShapes();
                MyshapeFile.StopEditingTable();
            }
              catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return MyshapeFile;
        }

        internal void toolStripRemoveClick(object sender, EventArgs e)
        {
            RemoveShapeFIleForm removeShapeform = new RemoveShapeFIleForm(mListShapeFile);
            removeShapeform.ShowDialog();
            try
            {
                if (removeShapeform.DialogResult == DialogResult.OK)
                {
                    foreach (object box in removeShapeform.checkedListBox1.CheckedItems)
                    {
                        char c = '(';
                        string[] array = box.ToString().Split(c);
                        if (Commun.ArrayHasValue(array))
                        {
                            if (!mListeShapefileToDelete.Contains(GetIdShapeFile(array[0].Trim())))
                            {
                                mListeShapefileToDelete.Add(GetIdShapeFile(array[0].Trim()));
                            }
                            foreach (InfoShapeFile shapefile in mListShapeFile)
                            {
                                if (shapefile.FileName.Contains(array[0]))
                                {
                                    mTerritoryForm.axMap1.set_LayerVisible(shapefile.NoLayer, false);
                                }
                            }
                            RemoveItemIncheckedBox(removeShapeform.checkedListBox1.CheckedItems, mTerritoryForm.checkedListBoxTerritoire);
                            RemoveItemIncheckedBox(removeShapeform.checkedListBox1.CheckedItems, mTerritoryForm.checkedListBoxActivite);
                            RemoveItemIncheckedBox(removeShapeform.checkedListBox1.CheckedItems, mTerritoryForm.checkedListBoxExploitation);
                            DeletePanel(array[0].Trim());
                        }
                    }
                }
                if (!mTerritoryForm.Text.Contains("*"))
                {
                    mTerritoryForm.Text = mTerritoryForm.Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void RemoveItemIncheckedBox(CheckedListBox.CheckedItemCollection checkedItems, CheckedListBox myCheckedlistbox)
        {
            try
            {
                for (int i = myCheckedlistbox.Items.Count - 1; i >= 0; i--)
                {
                    for (int j = 0; j < checkedItems.Count; j++)
                    {
                        if (checkedItems[j].ToString().Contains(myCheckedlistbox.Items[i].ToString()))
                        {
                            myCheckedlistbox.Items.RemoveAt(i);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void CheckedlisteboxCheckedChanged(object sender, EventArgs e)
        {
            try
            {

                if (SetCurrentCheckedListBox())
                {

                    if (currentListBox.GetItemChecked(currentListBox.SelectedIndex) == true)
                    {
                        foreach (InfoShapeFile shapefile in mListShapeFile)
                        {
                            if (shapefile.FileName == currentListBox.SelectedItem.ToString())
                            {
                                mTerritoryForm.axMap1.set_LayerVisible(shapefile.NoLayer, true);
                            }
                        }
                    }
                    if (currentListBox.GetItemChecked(currentListBox.SelectedIndex) == false)
                    {
                        foreach (InfoShapeFile shapefile in mListShapeFile)
                        {
                            if (shapefile.FileName == currentListBox.SelectedItem.ToString())
                            {
                                mTerritoryForm.axMap1.set_LayerVisible(shapefile.NoLayer, false);
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Program. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void CheckedlisteboxSelectecIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Color color = new Color();
                if (SetCurrentCheckedListBox())
                {
                    foreach (InfoShapeFile shapefile in mListShapeFile)
                    {
                        if (shapefile.FileName == currentListBox.SelectedItem.ToString())
                        {
                            LoadDataGridView(shapefile.ListPoints);
                        }
                        color = Color.FromArgb(shapefile.ColorARGB);
                        mTerritoryForm.axMap1.set_ShapeLayerFillColor(shapefile.NoLayer, Commun.ColorToUInt(color));
                        mTerritoryForm.axMap1.set_ShapeLayerLineColor(shapefile.NoLayer, Commun.ColorToUInt(color));
                        mTerritoryForm.axMap1.Redraw2(tkRedrawType.RedrawAll);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ToolStipRemoveLayerClick(object sender, EventArgs e)
        {



        }

        internal void CheckBoxListeItemClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right && SetCurrentCheckedListBox())
                {

                    ContextMenu contextmenu = new ContextMenu();
                    MenuItem itemRemove = new MenuItem("Remove ShapeFile");
                    MenuItem itemMoveLayerTop = new MenuItem("Put ShapeFile to the top");
                    MenuItem itemMoveLayerBottom = new MenuItem("Put ShapeFile to the bottom");
                    MenuItem itemMoveLayerUp = new MenuItem("Move Shapefile up");
                    MenuItem itemMoveLayerDown = new MenuItem("Move ShapeFile down");
                    itemRemove.Text = Translation.Translate("Remove ShapeFile", langue);
                    itemMoveLayerTop.Text = Translation.Translate("Move selected layer to the top", langue);
                    itemMoveLayerBottom.Text = Translation.Translate("Move selected layer to the bottom", langue);
                    itemMoveLayerUp.Text = Translation.Translate("Move selected layer up", langue);
                    itemMoveLayerDown.Text = Translation.Translate("Move selected layer down", langue);
                    contextmenu.MenuItems.Add(itemRemove);
                    contextmenu.MenuItems.Add(itemMoveLayerTop);
                    contextmenu.MenuItems.Add(itemMoveLayerBottom);
                    contextmenu.MenuItems.Add(itemMoveLayerUp);
                    contextmenu.MenuItems.Add(itemMoveLayerDown);
                    System.Drawing.Point pos = new System.Drawing.Point(e.X, e.Y);
                    currentListBox.ContextMenu = contextmenu;
                    contextmenu.Show(currentListBox, pos);
                    itemRemove.Click += ItemRemove_Click;
                    itemMoveLayerTop.Click += ShapeFileToTop_Click;
                    itemMoveLayerBottom.Click += ShapeFileToBottom_Click;
                    itemMoveLayerUp.Click += MoveShapeFileUp_Click;
                    itemMoveLayerDown.Click += MoveShapeFileDown_Click;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void MoveShapeFileDown_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetCurrentCheckedListBox())
                {
                    for (int i = 0; i < currentListBox.Items.Count; i++)
                    {
                        //CheckedListBox box = (CheckedListBox)mTerritoryForm.checkedListBoxTerritoire.Items[i];
                        if (currentListBox.Items[i].Equals(currentListBox.SelectedItem))
                        {
                            foreach (InfoShapeFile shapefile in mListShapeFile)
                            {
                                if (currentListBox.Items[i].ToString().Contains(shapefile.FileName))
                                {
                                    mTerritoryForm.axMap1.MoveLayerDown(mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer));
                                    shapefile.Position = mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        internal void MoveShapeFileUp_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetCurrentCheckedListBox())
                {
                    for (int i = 0; i < currentListBox.Items.Count; i++)
                    {
                        //CheckedListBox box = (CheckedListBox)mTerritoryForm.checkedListBoxTerritoire.Items[i];
                        if (currentListBox.Items[i].Equals(currentListBox.SelectedItem))
                        {
                            foreach (InfoShapeFile shapefile in mListShapeFile)
                            {
                                if (currentListBox.Items[i].ToString().Contains(shapefile.FileName))
                                {
                                    mTerritoryForm.axMap1.MoveLayerUp(mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer));
                                    shapefile.Position = mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer);
                                    break;
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ShapeFileToTop_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetCurrentCheckedListBox())
                {
                    for (int i = 0; i < currentListBox.Items.Count; i++)
                    {
                        //CheckedListBox box = (CheckedListBox)mTerritoryForm.checkedListBoxTerritoire.Items[i];
                        if (currentListBox.Items[i].Equals(currentListBox.SelectedItem))
                        {
                            foreach (InfoShapeFile shapefile in mListShapeFile)
                            {
                                if (currentListBox.Items[i].ToString().Contains(shapefile.FileName))
                                {
                                    mTerritoryForm.axMap1.MoveLayerTop(mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer));
                                    shapefile.Position = mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ShapeFileToBottom_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetCurrentCheckedListBox())
                {
                    for (int i = 0; i < currentListBox.Items.Count; i++)
                    {
                        if (currentListBox.Items[i].Equals(currentListBox.SelectedItem))
                        {
                            foreach (InfoShapeFile shapefile in mListShapeFile)
                            {
                                if (currentListBox.Items[i].ToString().Contains(shapefile.FileName))
                                {
                                    mTerritoryForm.axMap1.MoveLayerBottom(mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer));
                                    shapefile.Position = mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer);
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ItemRemove_Click(object sender, EventArgs e)
        {
            try
            {
                if (SetCurrentCheckedListBox())
                {
                    string shapefileToDelete = "";
                    string listBoxToDelete = "";
                    for (int i = 0; i < currentListBox.Items.Count; i++)
                    {
                        //CheckedListBox box = (CheckedListBox)mTerritoryForm.checkedListBoxTerritoire.Items[i];
                        if (currentListBox.Items[i].Equals(currentListBox.SelectedItem))
                        {
                            foreach (InfoShapeFile shapefile in mListShapeFile)
                            {
                                if (currentListBox.Items[i].ToString().Contains(shapefile.FileName))
                                {
                                    mTerritoryForm.axMap1.RemoveLayer(shapefile.NoLayer);
                                    shapefileToDelete = shapefile.FileName;
                                    listBoxToDelete = currentListBox.SelectedItem.ToString();
                                    mListeShapefileToDelete.Add(GetIdShapeFile(shapefile.FileName));
                                    break;
                                }
                            }
                        }
                    }

                    for (int i = currentListBox.Items.Count - 1; i >= 0; i--)
                    {
                        if (currentListBox.Items[i].ToString().Contains(listBoxToDelete))
                        {
                            currentListBox.Items.Remove(currentListBox.Items[i]);
                            break;
                        }
                    }
                    for (int i = mListShapeFile.Count - 1; i >= 0; i--)
                    {
                        if (mListShapeFile[i].FileName == shapefileToDelete)
                        {
                            DeletePanel(mListShapeFile[i].FileName);
                            mListShapeFile.RemoveAt(i);
                            break;
                        }
                    }
                    dataModified = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        //private EventHandler ItemRemove_Click2(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        Panel myPanel = new Panel();
        //        System.Windows.Forms.Label myLabel = new System.Windows.Forms.Label();
        //        myPanel = (Panel)sender; 
        //        string shapefileToDelete = "";
        //        string listBoxToDelete = "";
        //        for (int i = 0; i < currentListBox.Items.Count; i++)
        //        {

        //            foreach (InfoShapeFile shapefile in mListShapeFile)
        //            {
        //                if (myPanel.Tag.ToString() ==shapefile.FileName)
        //                {
        //                    mTerritoryForm.axMap1.RemoveLayer(shapefile.NoLayer);
        //                    shapefileToDelete = shapefile.FileName;
        //                    listBoxToDelete = currentListBox.SelectedItem.ToString();
        //                    mListeShapefileToDelete.Add(GetIdShapeFile(shapefile.FileName));
        //                    break;
        //                }
        //            }
        //        }
        //        for (int i = currentListBox.Items.Count - 1; i >= 0; i--)
        //        {
        //            if (currentListBox.Items[i].ToString().Contains(listBoxToDelete))
        //            {
        //                currentListBox.Items.Remove(currentListBox.Items[i]);
        //                break;
        //            }
        //        }
        //        for (int i = mListShapeFile.Count - 1; i >= 0; i--)
        //        {
        //            if (mListShapeFile[i].FileName == shapefileToDelete)
        //            {
        //                DeletePanel(mListShapeFile[i].FileName);
        //                mListShapeFile.RemoveAt(i);
        //                break;
        //            }
        //        }
        //    return null;

        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //        Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
        //        return null;
        //    }
        //}
        internal void SaveData(object sender, EventArgs e)
        {
            SaveShapeFileInfo();
        }

        internal void TerritoryForm_Load(object sender, EventArgs e)
        {
            try
            {
                Size panelCheckedBoxSize = mTerritoryForm.panelcheckbox.Size;
                double Heightdivided = panelCheckedBoxSize.Height / 3;
                double Widthdivided = panelCheckedBoxSize.Width;
                int MaxHeight = (int)Math.Ceiling(Heightdivided);
                int MaxWidth = (int)Math.Ceiling(Heightdivided);

                Size newSize = new Size(MaxWidth, MaxHeight);
                System.Drawing.Point point1 = new System.Drawing.Point(2, MaxHeight);
                System.Drawing.Point point2 = new System.Drawing.Point(2, 2 * MaxHeight);
                mTerritoryForm.panelExploitation.Location = point1;
                mTerritoryForm.panelActivity.Location = point2;

                mTerritoryForm.panelExploitation.Size = newSize;
                mTerritoryForm.panelActivity.Size = newSize;
                mTerritoryForm.panelTerritory.Size = newSize;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            ////newSize = new Size(115, MaxHeight);
            //mTerritoryForm.panelColorsExploitation.Size = newSize;
            //mTerritoryForm.panelColorsAct.Size = newSize;
            //mTerritoryForm.panelColorsTerritory.Size = newSize;

        }

        internal void label_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    if (sender.ToString().Contains(Translation.Translate("Farm", langue)))
                    {
                        currentLabel = mTerritoryForm.labelWorkingfarm;
                        currentListBox = mTerritoryForm.checkedListBoxExploitation;
                    }
                    else if (sender.ToString().Contains(Translation.Translate("Territory", langue)))
                    {
                        currentLabel = mTerritoryForm.labelTerritory;
                        currentListBox = mTerritoryForm.checkedListBoxTerritoire;

                    }
                    else if (sender.ToString().Contains(Translation.Translate("activity", langue)))
                    {
                        currentLabel = mTerritoryForm.labelActivitylayer;
                        currentListBox = mTerritoryForm.checkedListBoxActivite;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void CheckBoxCheckedChanged(object sender, EventArgs e)
        {
            try
            {
                SetUserChoice(mTerritoryForm.checkedListBoxActivite, mTerritoryForm.checkBoxActitivitelayer.Checked);
                SetUserChoice(mTerritoryForm.checkedListBoxExploitation, mTerritoryForm.checkBoxExploitation.Checked);
                SetUserChoice(mTerritoryForm.checkedListBoxTerritoire, mTerritoryForm.checkBoxTerritorylayer.Checked);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ItemDisplay_Click(object sender, EventArgs e)
        {
            try
            {
                SetUserChoice(currentListBox, true);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ItemHide_Click(object sender, EventArgs e)
        {
            try
            {
                SetUserChoice(currentListBox, false);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void PanelClick(object sender, MouseEventArgs e)
        {
            try
            {
                Panel myPanel = new Panel();
                System.Windows.Forms.Label myLabel = new System.Windows.Forms.Label();
                try { myPanel = (Panel)sender; }
                catch (Exception Ex) { }
                try
                {
                    myLabel = (System.Windows.Forms.Label)sender;
                    Control labelcontrol = myLabel.Parent;
                    if (labelcontrol is Panel)
                    {
                        myPanel = (Panel)labelcontrol;
                    }
                }
                catch (Exception Ex) { }

                if (e.Button == MouseButtons.Left)
                {
                    foreach (InfoShapeFile shapefile in mListShapeFile)
                    {
                        if (shapefile.FileName.Contains(myPanel.Tag.ToString()))
                        {
                            mTerritoryForm.colorDialog1.Color = mTerritoryForm.axMap1.get_ShapeLayerFillColor(shapefile.NoLayer);
                            mTerritoryForm.colorDialog1.ShowDialog();
                            mTerritoryForm.axMap1.set_ShapeLayerFillColor(shapefile.NoLayer, Commun.ColorToUInt(mTerritoryForm.colorDialog1.Color));
                            mTerritoryForm.axMap1.set_ShapeLayerLineColor(shapefile.NoLayer, Commun.ColorToUInt(mTerritoryForm.colorDialog1.Color));
                            myPanel.BackColor = mTerritoryForm.colorDialog1.Color;
                            shapefile.ColorARGB = mTerritoryForm.colorDialog1.Color.ToArgb();
                            if (myLabel != null)
                            {
                                if (myPanel.BackColor.GetBrightness() < 0.38)
                                {
                                    myLabel.ForeColor = Color.White;
                                }
                                else
                                {
                                    myLabel.ForeColor = Color.Black;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Program. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void MouseEnterPanel(object sender, EventArgs e)
        {
            try
            {
                Panel myPanel = new Panel();
                System.Windows.Forms.Label myLabel = new System.Windows.Forms.Label();
                try { myPanel = (Panel)sender; }
                catch (Exception Ex) { }
                try
                {
                    myLabel = (System.Windows.Forms.Label)sender;
                    Control labelcontrol = myLabel.Parent;
                    if (labelcontrol is Panel)
                    {
                        myPanel = (Panel)labelcontrol;
                    }
                }
                catch (Exception Ex) { }
                myPanel.BorderStyle = BorderStyle.Fixed3D;
                ToolTip tooltip = new ToolTip();
                tooltip.Active = true;
                tooltip.AutomaticDelay = 50;
                tooltip.AutoPopDelay = 1000;
                tooltip.SetToolTip(myPanel, myPanel.Tag.ToString() + ". " + Translation.Translate("Click to change color", langue));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Program. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void MouseLeavePanel(object sender, EventArgs e)
        {
            try
            {
                Panel myPanel = new Panel();
                System.Windows.Forms.Label myLabel = new System.Windows.Forms.Label();
                try { myPanel = (Panel)sender; }
                catch (Exception Ex) { }
                try
                {
                    myLabel = (System.Windows.Forms.Label)sender;
                    Control labelcontrol = myLabel.Parent;
                    if (labelcontrol is Panel)
                    {
                        myPanel = (Panel)labelcontrol;
                    }
                }
                catch (Exception Ex) { }
                myPanel.BorderStyle = BorderStyle.FixedSingle;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Program. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        public void createPointShapefileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateShapeFileForm shapefileform = new CreateShapeFileForm();
            shapefileform.ShowDialog();
            if (shapefileform.DialogResult == DialogResult.OK)
            {
                Color color1 = GetRandomColor();
                string path = GetPathToSave(shapefileform);
                List<string> list = shapefileform.ListeFiltre;
                CreateNewPointShapefileFromPoints(shapefileform.comboBox1Import.Text, path + "\\",
                    shapefileform.textBoxname.Text, color1, list);
                if (!shapefileform.checkBoxMainMap.Checked)
                {
                    if (shapefileform.comboBoxtype.SelectedIndex == 6)
                    {
                        if (!checkData.DataExist(shapefileform.comboBoxname.Text, "Exploitation"))
                        {
                            checkData.AddData(shapefileform.comboBoxname.Text, shapefileform.comboBoxtype.SelectedIndex, "Exploitation");
                        }
                    }
                    else
                    {
                        if (!checkData.DataExist(shapefileform.comboBoxname.Text, "Activite"))
                        {
                            checkData.AddData(shapefileform.comboBoxname.Text, shapefileform.comboBoxtype.SelectedIndex, "Activite");
                        }
                    }
                }
                AddShapeFile(path + "\\" + shapefileform.textBoxname.Text + ".shp", shapefileform.textBoxname.Text,
                    shapefileform.checkBoxMainMap.Checked, shapefileform.comboBoxtype.SelectedIndex,
                    shapefileform.comboBoxtype.Text, 0, color1.ToArgb(), 0, shapefileform.ListeFiltre);
                dataModified = true;
            }
        }

        private string GetPathToSave(CreateShapeFileForm shapefileform)
        {
            string path = "";
            try
            {
                if (shapefileform.radioButtonYes1.Checked)
                {
                    path = Properties.Settings.Default.DataPath + @"\shp";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                }
                else
                {
                    if (shapefileform.textBoxpath.Text.Length > 1)
                    {
                        path = shapefileform.textBoxpath.Text;
                    }
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return path;
        }

        public void createPolygoneShapefileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateShapeFileForm shapefileform = new CreateShapeFileForm();
            shapefileform.ShowDialog();
            if (shapefileform.DialogResult == DialogResult.OK)
            {
                Color color1 = GetRandomColor();
                string path = GetPathToSave(shapefileform);
                List<string> list = shapefileform.ListeFiltre;
                if (shapefileform.radioButtonYes.Checked) list.Clear();
                CreateNewPolyogoneShapefileFromPoints(shapefileform.comboBox1Import.Text, path + "\\",
                    shapefileform.textBoxname.Text, color1, list, "polygone");
                AddShapeFile(path + "\\" + shapefileform.textBoxname.Text + ".shp", shapefileform.textBoxname.Text,
                    shapefileform.checkBoxMainMap.Checked, shapefileform.comboBoxtype.SelectedIndex,
                    shapefileform.comboBoxtype.Text, 0, color1.ToArgb(), 0, shapefileform.ListeFiltre);
                dataModified = true;
            }
        }

        public void createLinehapefileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                CreateShapeFileForm shapefileform = new CreateShapeFileForm();
                shapefileform.ShowDialog();
                if (shapefileform.DialogResult == DialogResult.OK)
                {
                    Color color1 = GetRandomColor();
                    List<string> list = shapefileform.ListeFiltre;
                    string path = GetPathToSave(shapefileform);
                    if (shapefileform.radioButtonNo.Checked) list.Clear();
                    CreateNewPolyogoneShapefileFromPoints(shapefileform.comboBox1Import.Text, path + "\\",
                        shapefileform.textBoxname.Text, color1, list, "line");
                    AddShapeFile(path + "\\" + shapefileform.textBoxname.Text + ".shp", shapefileform.textBoxname.Text, shapefileform.checkBoxMainMap.Checked, shapefileform.comboBoxtype.SelectedIndex,
                        shapefileform.comboBoxtype.Text, 0, color1.ToArgb(), 0, shapefileform.ListeFiltre);
                    dataModified = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ScrollPanelsTerritory(object sender, ScrollEventArgs e)
        {

            mTerritoryForm.panelColorsAll.VerticalScroll.Value = e.NewValue;
            mTerritoryForm.checkedListBoxTerritoire.AutoScrollOffset = new System.Drawing.Point(0, e.NewValue);

        }

        internal void shapefileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportForm form = new ExportForm(mListShapeFile, mTerritoryForm.axMap1);
            form.ShowDialog();
        }

        #endregion

        #region Query
        internal void SetUserQuery(object sender, EventArgs e)
        {
            try
            {
                QueryTerritoryForm formquery = new QueryTerritoryForm();
                formquery.ShowDialog();

                if (formquery.DialogResult == DialogResult.OK)
                {
                    string maintype = formquery.comboBoxMainType.Text;
                    string type = "";
                    bool visible = true;

                    if (maintype.Contains(Translation.Translate("Territory", langue)))
                    {
                        currentListBox = mTerritoryForm.checkedListBoxTerritoire;
                        type = "territory";
                    }
                    else if (maintype.Contains(Translation.Translate("Farm", langue)))
                    {
                        currentListBox = mTerritoryForm.checkedListBoxExploitation;
                        type = "exploitation";
                    }
                    else if (maintype.Contains(Translation.Translate("Activity", langue)))
                    {
                        currentListBox = mTerritoryForm.checkedListBoxActivite;
                        type = "activite";
                    }

                    if (formquery.comboBoxdisplayhide.Text.Contains(Translation.Translate("Display all", langue))) visible = true;
                    else if (formquery.comboBoxdisplayhide.Text.Contains(Translation.Translate("Hide all", langue))) visible = false;
                    else
                    {
                        SetUserQuery(formquery, type);
                        return;
                    }

                    if (maintype == "")
                    {
                        SetUserChoice(mTerritoryForm.checkedListBoxActivite, visible);
                        SetUserChoice(mTerritoryForm.checkedListBoxExploitation, visible);
                        SetUserChoice(mTerritoryForm.checkedListBoxTerritoire, visible);
                    }
                    else
                    {
                        SetUserChoice(currentListBox, visible);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void SetUserQuery(QueryTerritoryForm formquery, string type)
        {
            try
            {
                bool visible;

                if (formquery.comboBoxdisplayhide.Text.Contains(Translation.Translate("Display only", langue))) visible = true;
                else visible = false;

                if (formquery.comboBoxSecondType.Text == "")
                {
                    SetUserChoice(currentListBox, visible);
                    ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxActivite, !visible);
                    ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxExploitation, !visible);
                    ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxTerritoire, !visible);
                }
                else if (!formquery.comboBoxSecondType.Text.Contains(Translation.Translate("words", langue)) && formquery.comboBoxFiltre.Text == "")
                {
                    SetUserChoice(currentListBox, visible, formquery.comboBoxSecondType.Text);
                    ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxActivite, !visible);
                    ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxExploitation, !visible);
                    ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxTerritoire, !visible);
                }
                else if (formquery.comboBoxFiltre.Text != "" && !formquery.comboBoxSecondType.Text.Contains(Translation.Translate("words", langue)))
                {
                    string query = SQLQueryBuilder.SelectQuery("Exploitation", "Nom");
                    List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    int IdExpl = 0;
                    if (list.Contains(GetExploitationName(formquery.comboBoxFiltre.Text)))
                    {
                        IdExpl = SQLQueryBuilder.FindId("Exploitation", "ID", "Nom", GetExploitationName(formquery.comboBoxFiltre.Text));
                        query = SQLQueryBuilder.SelectQuery("Caract_Exploitation", "*", "WHERE IdExploitation = '" + IdExpl + "'");
                        List<int> list2 = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);
                        foreach (int id in list2)
                        {
                            string ActiviteType = GetActiviteType(id);
                            if (ActiviteType == formquery.comboBoxSecondType.Text)
                            {
                                SetUserChoice(currentListBox, visible, ActiviteType);
                            }
                            else
                            {
                                ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxActivite, !visible, ActiviteType);
                                ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxExploitation, !visible, ActiviteType);
                                ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxTerritoire, !visible, ActiviteType);
                            }
                        }
                    }
                    else if (formquery.comboBoxFiltre.Text.Contains(Translation.Translate("all", langue)))
                    {
                        IdExpl = SQLQueryBuilder.FindId("Exploitation", "ID", "Nom", GetExploitationName(formquery.comboBoxFiltre.Text));
                        query = SQLQueryBuilder.SelectQuery("Caract_Exploitation", "*", "WHERE IdExploitation = '" + IdExpl + "'");
                        List<int> list2 = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);
                        foreach (int id in list2)
                        {
                            string ActiviteType = GetActiviteType(id);
                            if (ActiviteType == formquery.comboBoxSecondType.Text)
                            {
                                SetUserChoice(currentListBox, visible, ActiviteType);
                            }
                            else
                            {
                                ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxActivite, !visible, ActiviteType);
                                ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxExploitation, !visible, ActiviteType);
                                ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxTerritoire, !visible, ActiviteType);
                            }
                        }
                    }
                }
                else if (formquery.comboBoxSecondType.Text.Contains(Translation.Translate("words", langue)))
                {
                    SetUserChoice(currentListBox, visible, formquery.textBoxMot);
                    ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxActivite, !visible, formquery.textBoxMot);
                    ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxExploitation, !visible, formquery.textBoxMot);
                    ApplyUserChoiceToOtherChekedlistbox(mTerritoryForm.checkedListBoxTerritoire, !visible, formquery.textBoxMot);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetUserChoice(CheckedListBox MyCheckedListBox, bool MyVisible)
        {
            try
            {
                for (int i = 0; i <= MyCheckedListBox.Items.Count - 1; i++)
                {
                    foreach (InfoShapeFile shapefile in mListShapeFile)
                    {
                        if (shapefile.FileName == MyCheckedListBox.Items[i].ToString())
                        {
                            mTerritoryForm.axMap1.set_LayerVisible(shapefile.NoLayer, MyVisible);
                            MyCheckedListBox.SetItemChecked(i, MyVisible);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void SetUserChoice(CheckedListBox MyCheckedListBox, bool MyVisible, string type)
        {
            try
            {
                for (int i = 0; i <= MyCheckedListBox.Items.Count - 1; i++)
                {
                    foreach (InfoShapeFile shapefile in mListShapeFile)
                    {
                        if (shapefile.FileName == MyCheckedListBox.Items[i].ToString() && shapefile.type == type)
                        {
                            mTerritoryForm.axMap1.set_LayerVisible(shapefile.NoLayer, MyVisible);
                            MyCheckedListBox.SetItemChecked(i, MyVisible);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void SetUserChoice(CheckedListBox MyCheckedListBox, bool MyVisible, TextBox TxtBoxNom)
        {
            try
            {
                for (int i = 0; i <= MyCheckedListBox.Items.Count - 1; i++)
                {

                    foreach (InfoShapeFile shapefile in mListShapeFile)
                    {
                        if (shapefile.FileName == MyCheckedListBox.Items[i].ToString() && shapefile.FileName.Contains(TxtBoxNom.Text))
                        {
                            mTerritoryForm.axMap1.set_LayerVisible(shapefile.NoLayer, MyVisible);
                            MyCheckedListBox.SetItemChecked(i, MyVisible);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void ApplyUserChoiceToOtherChekedlistbox(CheckedListBox MyCheckedListBox, bool MyVisible)
        {
            try
            {
                for (int i = 0; i <= MyCheckedListBox.Items.Count - 1; i++)
                {
                    if (currentListBox == MyCheckedListBox) break;
                    foreach (InfoShapeFile shapefile in mListShapeFile)
                    {
                        if (shapefile.FileName == MyCheckedListBox.Items[i].ToString())
                        {
                            mTerritoryForm.axMap1.set_LayerVisible(shapefile.NoLayer, MyVisible);
                            MyCheckedListBox.SetItemChecked(i, MyVisible);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void ApplyUserChoiceToOtherChekedlistbox(CheckedListBox MyCheckedListBox, bool MyVisible, string type)
        {
            try
            {
                for (int i = 0; i <= MyCheckedListBox.Items.Count - 1; i++)
                {
                    if (currentListBox == MyCheckedListBox) break;
                    foreach (InfoShapeFile shapefile in mListShapeFile)
                    {
                        if (shapefile.FileName == MyCheckedListBox.Items[i].ToString() && shapefile.type != type)
                        {
                            mTerritoryForm.axMap1.set_LayerVisible(shapefile.NoLayer, MyVisible);
                            MyCheckedListBox.SetItemChecked(i, MyVisible);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void ApplyUserChoiceToOtherChekedlistbox(CheckedListBox MyCheckedListBox, bool MyVisible, TextBox TxtBoxNom)
        {
            try
            {
                for (int i = 0; i <= MyCheckedListBox.Items.Count - 1; i++)
                {
                    if (currentListBox == MyCheckedListBox) break;
                    MyCheckedListBox.SetItemChecked(i, MyVisible);
                    foreach (InfoShapeFile shapefile in mListShapeFile)
                    {
                        if (shapefile.FileName == MyCheckedListBox.Items[i].ToString() && shapefile.FileName.Contains(TxtBoxNom.Text) == false)
                        {
                            mTerritoryForm.axMap1.set_LayerVisible(shapefile.NoLayer, MyVisible);
                            MyCheckedListBox.SetItemChecked(i, MyVisible);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        #endregion

        #region PanelColor
        private void SetnewPanel(string filename, Color color1)
        {
            Panel panel = new Panel();
            try
            {
                int y = 0;
                int x = 1;
                Size size = new Size(mTerritoryForm.panelColorsAll.Size.Width - 22, 19);
                panel.Size = size;
                panel.BackColor = color1;
                panel.BorderStyle = BorderStyle.FixedSingle;
                panel.Tag = filename;
                panel.MouseClick += PanelClick;
                panel.MouseEnter += MouseEnterPanel;
                panel.MouseLeave += MouseLeavePanel;
                panel.AutoScroll = true;
                mTerritoryForm.panelColorsAll.Controls.Add(panel);
                panel.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
                y = compteur * 20 + 15;
                panel.Location = new System.Drawing.Point(x, y);
                SetNewLabel(filename, panel, color1);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void DeletePanel(string filename)
        {
            try
            {
                Panel myPanel = new Panel();
                foreach (Control panelcontrol in mTerritoryForm.panelColorsAll.Controls)
                {
                    if (panelcontrol is Panel)
                    {
                        myPanel = (Panel)panelcontrol;
                        if (myPanel.Tag.ToString() == filename)
                        {
                            break;
                        }
                    }
                }
                if (myPanel != null)
                {
                    mTerritoryForm.panelColorsAll.Controls.Remove(myPanel);
                    compteur--;
                }
                AdaptViewPanelsColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetNewLabel(string filename, Panel panel, Color mycolor)
        {
            System.Windows.Forms.Label label = new System.Windows.Forms.Label();
            try
            {

                label.Font = new Font("Corbel", 8);
                if (mycolor.GetBrightness() < 0.38)
                {
                    label.ForeColor = Color.White;
                }
                else
                {
                    label.ForeColor = Color.Black;
                }
                label.Text = filename;
                label.Size = new Size(85, 14);
                label.MouseClick += PanelClick;
                label.MouseEnter += MouseEnterPanel;
                label.MouseLeave += MouseLeavePanel;
                panel.Controls.Add(label);
                label.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
                label.Location = new System.Drawing.Point(0, 1);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void AdaptViewPanelsColor()
        {
            try
            {
                Panel myPanel = new Panel();
                List<Panel> listPanel = new List<Panel>();
                int nbPanel = 0;
                int x = 1;
                int y = 0;
                foreach (Control panelcontrol in mTerritoryForm.panelColorsAll.Controls)
                {
                    if (panelcontrol is Panel)
                    {
                        myPanel = (Panel)panelcontrol;
                        nbPanel++;
                        listPanel.Add(myPanel);
                    }
                }
                for (int i = 1; i <= nbPanel; i++)
                {
                    y = (i - 1) * 20 + 15;
                    listPanel[i - 1].Location = new System.Drawing.Point(x, y);

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        #endregion

        #region DataGridView & Fiche Attribut

        private void LoadDataGridView(List<string> listCodePoint)
        {
            try
            {
                if (listCodePoint == null) return ;
                if (mTerritoryForm.dataGridView1.Rows.Count != listCodePoint.Count)
                {
                    mTerritoryForm.dataGridView1.Rows.Clear();
                    mTerritoryForm.dataGridView1.Columns.Clear();
                    mTerritoryForm.dataGridView1.Columns.Add("Code Point", "Code Point");
                    AddComboBoxColumn("Select Nom From Type", "Nom", "Occupation");
                    foreach (string item in listCodePoint)
                    {
                        mTerritoryForm.dataGridView1.Rows.Add(new object[] { item });
                    }
                    GetField();
                    SetValues();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }




        private void GetField()
        {
            try
            {
                foreach (DataGridViewRow row in mTerritoryForm.dataGridView1.Rows)
                {
                    string query = "Select * From Info_Point Where Code_Point = '" + row.Cells[0].Value.ToString() + "';";
                    List<string> list = SQlQueryExecuter.RunQueryReaderStr("Occup1", query);
                    if (Commun.ListHasValue(list))
                    {
                      
                        if (list[0] == Translation.Translate("Tree",langue))
                        {
                            AddColumnField(1);
                        }
                        if (list[0] == Translation.Translate("Forest", langue) || list[0] == Translation.Translate("Wood", langue))
                        {
                            AddColumnField(2);
                        }
                        if (list[0] == Translation.Translate("Crop", langue))
                        {
                            AddColumnField(3);
                        }
                        if (list[0] == Translation.Translate("Farm", langue))
                        {
                            AddColumnField(4);
                        }
                    }
                    break;
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetValues()
        {
            foreach (DataGridViewRow row in mTerritoryForm.dataGridView1.Rows)
            {
                string query = "Select * From Info_Point Where Code_Point = '" + row.Cells[0].Value.ToString() + "';";
                string[] values = SQlQueryExecuter.RunQueryReader(query);
                for (int i = 2;i < values.Length;i++)
                {
                    if (row.Cells.Count > i-1)
                    {
                        if (values[i] != null && values[i] != "")
                            row.Cells[i - 1].Value = values[i];
                    }
                }
            }
        }

        internal void ButtonSaveClick(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in mTerritoryForm.dataGridView1.Rows)
                {
                    string query = "Select * From Info_Point Where Code_Point = '" + row.Cells[0].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        query = "Update Info_Point Set Occup1 = '" + row.Cells[1].Value.ToString() +
                            "', Occup2 = '" + row.Cells[2].Value.ToString() + "'," + GetFieldInfo(row)
                            + " Where ID = '" + list[0] + "';";
                    }
                    else
                    {
                        query = "Insert into Info_Point (Code_Point,Occup1,Occup2" + InsertFieldInfo1(row) +
                            ") VALUES ('" + row.Cells[0].Value.ToString() + "','" + row.Cells[1].Value.ToString() +
                            "','" + row.Cells[2].Value.ToString() + "'" + InsertFieldInfo2(row) + ");";
                    }
                    SQlQueryExecuter.RunQuery(query);

                    Shapefile myShapefile = new Shapefile();
                    foreach (InfoShapeFile infoShape in mListShapeFile)
                    {
                        var open = myShapefile.Open(infoShape.FilePath);
                        if (!open)
                        {
                            MessageBox.Show(myShapefile.ErrorMsg[myShapefile.LastErrorCode]);
                            return;
                        }
                        else
                        {
                            //couleur = royalblue = on a selectionné ce shapefile
                            if (mTerritoryForm.axMap1.get_ShapeLayerFillColor(infoShape.NoLayer) == Color.RoyalBlue)
                            {
                                if (!myShapefile.Table.StartEditingTable(null))
                                {
                                    MessageBox.Show("Failed to start edit mode: " + myShapefile.Table.ErrorMsg[myShapefile.LastErrorCode]);
                                }
                                if (!myShapefile.StartEditingTable(null))
                                {
                                    MessageBox.Show("Failed to start edit mode: " + myShapefile.Table.ErrorMsg[myShapefile.LastErrorCode]);
                                }
                                if (!myShapefile.StartEditingShapes(true))
                                {
                                    MessageBox.Show("Failed to start edit mode: " + myShapefile.Table.ErrorMsg[myShapefile.LastErrorCode]);
                                }
                                for( int i = 1; i < mTerritoryForm.dataGridView1.ColumnCount;i++)
                                {
                                    if (row.Cells[1].Value != null) myShapefile.EditCellValue(1, infoShape.NoLayer, row.Cells[i].Value.ToString());
                                }
                                
                                //if (row.Cells[2].Value != null) myShapefile.EditCellValue(2, infoShape.NoLayer, row.Cells[2].Value.ToString());
                                //if (row.Cells[3].Value != null) myShapefile.EditCellValue(3, infoShape.NoLayer, row.Cells[3].Value.ToString());
                                //if (row.Cells[4].Value != null) myShapefile.EditCellValue(4, infoShape.NoLayer, row.Cells[4].Value.ToString());
                                //if (row.Cells[5].Value != null) myShapefile.EditCellValue(5, infoShape.NoLayer, row.Cells[5].Value.ToString());
                                //if (row.Cells[6].Value != null) myShapefile.EditCellValue(6, infoShape.NoLayer, row.Cells[6].Value.ToString());
                                var save = myShapefile.Save();
                                if (! save)
                                {
                                    MessageBox.Show("Failed to save attributs fields : " + myShapefile.Table.ErrorMsg[myShapefile.LastErrorCode]);
                                }
                                myShapefile.StopEditingTable();
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ButtonCopyValues(object sender, EventArgs e)
        {
            try
            {
                string value = mTerritoryForm.dataGridView1.CurrentCell.Value.ToString();
                int indexCol = mTerritoryForm.dataGridView1.CurrentCell.ColumnIndex;
                foreach (DataGridViewRow row in mTerritoryForm.dataGridView1.Rows)
                {
                    row.Cells[indexCol].Value = value;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private string GetFieldInfo(DataGridViewRow row)
        {
            string fieldinfo = "";
            int index = 1;
            foreach (DataGridViewCell cell in row.Cells)
            {
                if (cell.OwningColumn.Name.Contains("field"))
                {
                    fieldinfo = fieldinfo + " field " + index + " = '" + row.Cells[index + 2].Value.ToString() + "',";
                }
            }
            fieldinfo = fieldinfo.Remove(fieldinfo.Length - 1, 1);
            return fieldinfo;
        }

        private string InsertFieldInfo1(DataGridViewRow row)
        {
            string fieldinfo = "";
            try
            {
                int index = -1;
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.OwningColumn.Name.Contains("field"))
                    {
                        fieldinfo = fieldinfo + "field" + index + ",";
                    }
                    index++;
                }
                if (fieldinfo.Length != 0)
                {
                    fieldinfo = fieldinfo.Remove(fieldinfo.Length - 1, 1);
                    fieldinfo = "," + fieldinfo;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return fieldinfo;
        }

        private string InsertFieldInfo2(DataGridViewRow row)
        {
            string fieldinfo = "";
            int index = 0;
            try
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.OwningColumn.Name.Contains("field") && !cell.GetType().ToString().Contains("ComboBox"))
                    {
                        if (row.Cells[index].Value != null) fieldinfo = fieldinfo + "'" + row.Cells[index].Value.ToString() + "',";
                        else fieldinfo = fieldinfo + "'',";
                    }
                    index++;
                }
                if (fieldinfo.Length != 0)
                {
                    fieldinfo = fieldinfo.Remove(fieldinfo.Length - 1, 1);
                    fieldinfo = "," + fieldinfo ;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return fieldinfo;
        }

        internal void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
                string value = mTerritoryForm.dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                if (value == Translation.Translate("Crop",langue) || value == "Crop")
                {
                    if (mTerritoryForm.dataGridView1.Columns.Contains("field"))
                    {
                        RemoveColumnField();
                    }
                    if (!mTerritoryForm.dataGridView1.Columns.Contains("Occupation2"))
                    {
                        string query = "Select Nom From Activite Where IdType = '1';";
                        AddComboBoxColumn(query, "Nom", "Occupation2");
                    }
                }
                else if (value == Translation.Translate("Animals", langue) || value == "Animals")
                {
                    if (mTerritoryForm.dataGridView1.Columns.Contains("field"))
                    {
                        RemoveColumnField();
                    }
                    if (!mTerritoryForm.dataGridView1.Columns.Contains("Occupation2"))
                    {
                        string query = "Select Nom From Activite Where IdType = '5';";
                        AddComboBoxColumn(query, "Nom", "Occupation2");
                    }
                }
                else if (value == Translation.Translate("Pluriannual", langue) || value == "Pluriannual")
                {
                    if (mTerritoryForm.dataGridView1.Columns.Contains("field"))
                    {
                        RemoveColumnField();
                    }
                    if (!mTerritoryForm.dataGridView1.Columns.Contains("Occupation2"))
                    {
                        string query = "Select Nom From Activite Where IdType = '6';";
                        AddComboBoxColumn(query, "Nom", "Occupation2");
                    }
                }
                else if (value == Translation.Translate("Farm", langue) || value == "Farm")
                {
                    if (mTerritoryForm.dataGridView1.Columns.Contains("field"))
                    {
                        RemoveColumnField();
                    }
                    if (!mTerritoryForm.dataGridView1.Columns.Contains("Occupation2"))
                    {
                        string query = "Select Nom From Exploitation;";
                        AddComboBoxColumn(query, "Nom", "Occupation2");
                    }
                }
                else  if (value == Translation.Translate("Tree", langue) || value == "Tree")
                {
                    if (!mTerritoryForm.dataGridView1.Columns.Contains("Occupation2"))
                    {
                        string query = "";
                        //AddComboBoxColumn(query, "", "Occupation2");
                    }
                    if (mTerritoryForm.dataGridView1.Columns.Count < 3)
                    {
                        AddColumnField(1);
                    }
                }
                else
                {
                    if (!mTerritoryForm.dataGridView1.Columns.Contains("Occupation2"))
                    {
                        string query = "";
                        AddComboBoxColumn(query, "", "Occupation2");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void AddColumnField(int type)
        {
            // type = 1 => arbre
            // type = 2 => foret
            // type = 3 => crop
            // type = 4 => ??
            // type = 5 => ??
            // type = 6 => ??
            try
            {
                switch (type)
                {
                    case 1:
                        mTerritoryForm.dataGridView1.Columns.Add("field1", Translation.Translate("Essence", langue));
                        mTerritoryForm.dataGridView1.Columns.Add("field2", Translation.Translate("High", langue));
                        mTerritoryForm.dataGridView1.Columns.Add("field3", Translation.Translate("Diameter", langue));
                        mTerritoryForm.dataGridView1.Columns.Add("field4", Translation.Translate("Note", langue));
                        break;
                    case 4: // farm, on met les classifications

                        break;
                }
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void AddComboBoxColumn(string query,string filtre,string ColName = "")
        {
            try
            { 
                DataGridViewComboBoxColumn dgvComboBoxColumn = new DataGridViewComboBoxColumn();
                dgvComboBoxColumn.Name = ColName;
                if (filtre != "")
                {
                    List<string> list = SQlQueryExecuter.RunQueryReaderStr(filtre, query);
                    foreach (string item in list)
                    {
                        dgvComboBoxColumn.Items.Add(Translation.Translate(item,langue));
                    }
                }
                mTerritoryForm.dataGridView1.Columns.Add(dgvComboBoxColumn);      
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void DataGridViewClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string code = mTerritoryForm.dataGridView1.Rows[mTerritoryForm.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
                Color color = new Color();
                foreach (InfoShapeFile shapefile in mListShapeFile)
                {
                    if (shapefile.ListPoints.Contains(code))
                    {
                        color = Color.RoyalBlue;
                    }
                    else
                    {
                        color = Color.FromArgb(shapefile.ColorARGB);
                    }
                    mTerritoryForm.axMap1.set_ShapeLayerFillColor(shapefile.NoLayer, Commun.ColorToUInt(color));
                    mTerritoryForm.axMap1.set_ShapeLayerLineColor(shapefile.NoLayer, Commun.ColorToUInt(color));
                    mTerritoryForm.axMap1.Redraw2(tkRedrawType.RedrawAll);
                }
                mTerritoryForm.axMap1.ShapeEditor.HighlightVertices = tkLayerSelection.lsActiveLayer;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void DataGridViewDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int ShapefileId = 0;
                string code = mTerritoryForm.dataGridView1.Rows[mTerritoryForm.dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
                foreach (InfoShapeFile shapefile in mListShapeFile)
                {
                    if (shapefile.ListPoints.Contains(code))
                    {
                        ShapefileId = shapefile.Id;
                    }
                }
                FicheAttributForm ficheForm = new FicheAttributForm(code, ShapefileId);
                ficheForm.ShowDialog(mTerritoryForm);

                if (ficheForm.DialogResult == DialogResult.OK)
                {
                    ficheForm.SaveInfoFiche(ShapefileId);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    ContextMenu contextmenu = new ContextMenu();
                    MenuItem menuitem = new MenuItem("Add new type");
                    contextmenu.MenuItems.Add(menuitem);
                    mTerritoryForm.dataGridView1.ContextMenu = contextmenu;
                    System.Drawing.Point point = new System.Drawing.Point(e.X, e.Y);
                    contextmenu.Show(mTerritoryForm.dataGridView1, point);
                    menuitem.Click += AddItemClick;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void AddItemClick(object sender, EventArgs e)
        {
            try
            {
                string uservalue = "";
                if (Commun.InputBox("Add new type", "", ref uservalue) == DialogResult.OK)
                {
                    if (Commun.NameExists(uservalue, "Type", "Nom"))
                    {
                        MessageBox.Show(Translation.Translate("This type already exists",langue));
                        return;
                    }
                    else
                    {
                        string query = "Insert into Type (Nom) VALUES ('" + uservalue + "');";
                        SQlQueryExecuter.RunQuery(query);
                        ReloadColumnOccupation();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void RemoveColumnField()
        {
            try
            {
                mTerritoryForm.dataGridView1.Columns.Remove("Field1");
                mTerritoryForm.dataGridView1.Columns.Remove("Field2");
                mTerritoryForm.dataGridView1.Columns.Remove("Field3");
                mTerritoryForm.dataGridView1.Columns.Remove("Field4");
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    
        private void ReloadColumnOccupation()
        {
            try
            {
                mTerritoryForm.dataGridView1.Columns.Remove("Occupation");
                AddComboBoxColumn("Select Nom From Type", "Nom", "Occupation");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        #endregion

        #region Manage ShapeFile

        private void InitLayerToPosition()
        {
            foreach (InfoShapeFile shapefile in mListShapeFile)
            {
                if (shapefile.Position < 0)
                {
                    shapefile.Position = mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer);
                }
            }
        }

        private void SetLayerToPosition()
        {
            int currentMapPos;
            int dataBasePos;
            foreach (InfoShapeFile shapefile in mListShapeFile)
            {
                dataBasePos = shapefile.Position;
                if (dataBasePos >= 0)
                {
                    currentMapPos = mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer);
                    while (currentMapPos < dataBasePos)
                    {
                        mTerritoryForm.axMap1.MoveLayerUp(mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer));
                        currentMapPos = mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer);
                    }
                    while (currentMapPos > dataBasePos)
                    {
                        mTerritoryForm.axMap1.MoveLayerDown(mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer));
                        currentMapPos = mTerritoryForm.axMap1.get_LayerPosition(shapefile.NoLayer);
                    }
                }
            }

        }

        private string GetActiviteType(int id)
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Activite", "*", "Where Id = '" + id + "'");
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("IdType", query);
                if (Commun.ListHasValue(list))
                {
                    return SQLQueryBuilder.GetNomFromId("Type", list[0], "Nom", "ID");
                }
                else return "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }

        private void SaveShapeFileInfo()
        {
            try
            {
                string query = "";

                foreach (InfoShapeFile shapefile in mListShapeFile)
                {
                    query = SQLQueryBuilder.SelectQuery("SHP_Info", "*", "Where nom = '" + shapefile.FileName + "'");
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    string code = "";
                    if (shapefile.ListPoints != null)
                    {
                        foreach (string item in shapefile.ListPoints)
                        {
                            code = code + item + "@";
                        }
                    }
                    if (Commun.ListHasValue(list))
                    {
                        query = TerritoryQuery.UpdateQuery(shapefile, list[0]);
                    }
                    else
                    {
                        query = TerritoryQuery.InsertQuery(shapefile, code);
                    }
                    SQlQueryExecuter.RunQuery(query);
                }

                foreach (int index in mListeShapefileToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(index, "SHP_Info");
                    SQlQueryExecuter.RunQuery(query);
                }
                dataModified = false;
                mTerritoryForm.Text = mTerritoryForm.Text.Substring(mTerritoryForm.Text.Length - 1, 1);

                Program.Save(false);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private string GetShapeFileTypeId(InfoShapeFile myShapefile)
        {
            string fieldValue = "'0','0'";
            try
            {
                if (myShapefile.MainMap)
                {
                    return fieldValue;
                }
                else
                {
                    if (myShapefile.type.Contains("farm"))
                    {
                        fieldValue = "'0','" + myShapefile.IdActExploi + "'";
                    }
                    else fieldValue = "'" + myShapefile.IdActExploi + "','0'";
                    return fieldValue;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return fieldValue;
            }
        }

        private int GetIdShapeFile(string name)
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("SHP_Info", "*", "Where Nom LIKE '" + name + "'");
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (Commun.ListHasValue(list))
                {
                    return list[0];
                }
                else return 0; ;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return 0;
            }
        }

        private bool CreateNewPointShapefileFromPoints(string importname, string pathToSAve,
            string shapefileName, Color color, List<string> Listcode)
        {
            try
            {
                Shapefile newShapefile = new Shapefile();
                int shapeindex = 0;
                shapefileName = shapefileName + ".shp";
                bool create = newShapefile.CreateNew(pathToSAve + shapefileName, ShpfileType.SHP_POINT);
                if (!create)
                {
                    MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + pathToSAve + shapefileName
                        + "\r " + newShapefile.ErrorMsg[newShapefile.LastErrorCode] + ". " + Translation.Translate("Creation canceled", langue));
                    return false;
                }
                bool geoproj = newShapefile.GeoProjection.ImportFromEPSG(4326);
               // bool geoproj2 = newShapefile.GeoProjection.SetWgs84();
                bool start = newShapefile.StartEditingShapes(true);
                if (!start)
                {
                    MessageBox.Show(Translation.Translate("Failed to start editing mode  : ",langue) + newShapefile.Table.ErrorMsg[newShapefile.LastErrorCode]);
                    return false;
                }
                MapWinGIS.Point point = new MapWinGIS.Point();
                string query = "";
                Shape shape1 = new Shape();
                //if (Listcode.Count == 0)
                //{
                    query = "Select * From GPS Where import_name = '" + importname + "';";
                    List<double> listX = SQlQueryExecuter.RunQueryReaderDouble("X", query);
                    List<double> listY = SQlQueryExecuter.RunQueryReaderDouble("Y", query);
                    
                    foreach (double x in listX)
                    {
                        shape1 = new Shape();
                        bool createshape = shape1.Create(ShpfileType.SHP_POINT);
                        point = new MapWinGIS.Point();
                        point.x = x;
                        point.y = listY[listX.IndexOf(x)];
                        var insert1 = shape1.InsertPoint(point, shapeindex);
                        //if (!insert1) //MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + shape1.IsValidReason);
                        newShapefile.EditInsertShape(shape1, shapeindex);
                        //if (!insert) MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + shape1.IsValidReason);
                    }
            
                newShapefile.DefaultDrawingOptions.FillColor = Commun.ColorToUInt(color);
                newShapefile.DefaultDrawingOptions.PointSize = 15;
                bool save = newShapefile.Save();
                if (!save)
                {
                    MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + pathToSAve + shapefileName
                         + "\r " + newShapefile.ErrorMsg[newShapefile.LastErrorCode] + ". " + Translation.Translate("Creation canceled", langue));
                    return false;
                }
                bool stopedititing = newShapefile.StopEditingShapes();
                if (!stopedititing) MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + ". ");
                newShapefile.Close();
                return true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return false;
            }
        }

        private bool CreateNewPolyogoneShapefileFromPoints(string importname, string pathToSAve,
            string shapefileName, Color color, List<string> Listcode, string type)
        {
            try
            {
                Shapefile newShapefile = new Shapefile();
                shapefileName = shapefileName + ".shp";
                bool create;
                if (type == "line") create = newShapefile.CreateNew(pathToSAve + shapefileName, ShpfileType.SHP_POLYLINE);
                else create = newShapefile.CreateNew(pathToSAve + shapefileName, ShpfileType.SHP_POLYGON);
                if (!create)
                {
                    MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + pathToSAve + shapefileName
                        + "\r " + newShapefile.ErrorMsg[newShapefile.LastErrorCode] + ". " + Translation.Translate("Creation canceled", langue));
                    return false;
                }
                bool geoproj = newShapefile.GeoProjection.ImportFromEPSG(4326);
                //bool geoproj2 = newShapefile.GeoProjection.SetWgs84();
                bool start = newShapefile.StartEditingShapes(true);
                int numPoints = 0;
                float[] arrx = new float[numPoints];
                float[] arry = new float[numPoints];
                Shape shape1 = new Shape();
                if (Listcode.Count == 0)
                {
                    MapWinGIS.Point point = new MapWinGIS.Point();
                    string query = "Select * From GPS Where import_name = '" + importname + "';";
                    List<double> listX = SQlQueryExecuter.RunQueryReaderDouble("X", query);
                    List<double> listY = SQlQueryExecuter.RunQueryReaderDouble("Y", query);
                    numPoints = listX.Count;
                    arrx = new float[numPoints];
                    arry = new float[numPoints];
                    shape1 = new Shape();
                    bool create1;
                    if (type == "line") create1 = shape1.Create(ShpfileType.SHP_POLYLINE);
                    else create1 = shape1.Create(ShpfileType.SHP_POLYGON);
                    if (create1)
                    {
                        int index = 0;
                        int shapeindex = 0;
                        foreach (double x in listX)
                        {
                            point = new MapWinGIS.Point();
                            point.x = x;
                            point.y = listY[index];
                            arrx[index] = (float)point.x;
                            arry[index] = (float)point.y;
                            //shape1.AddPoint(point.x, point.y);
                            shape1.InsertPoint(point, ref shapeindex);
                            var insert1 = shape1.InsertPoint(point, ref shapeindex);
                            if (!shape1.IsValid) //MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + shape1.IsValidReason);
                            index++;
                        }
                        // newShapefile.EditAddShape(shape1);

                        var insert3 = newShapefile.EditInsertShape(shape1, ref shapeindex);
                        if (!insert3) //MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + newShapefile.ErrorMsg[newShapefile.LastErrorCode]);

                        if (numPoints > 1)
                        {
                            newShapefile.DefaultDrawingOptions.DrawShape(IntPtr.Zero, arrx[0], arry[0], shape1, true, 10, 6);
                            newShapefile.DefaultDrawingOptions.LineColor = Commun.ColorToUInt(color);
                            newShapefile.DefaultDrawingOptions.FillColor = Commun.ColorToUInt(color);
                            newShapefile.DefaultDrawingOptions.PointSize = 15;
                            newShapefile.CacheExtents = true;
                            newShapefile.DefaultDrawingOptions.DrawingMode = tkVectorDrawingMode.vdmGDI;
                            //newShapefile.DefaultDrawingOptions.PointSize = 8;
                            bool save = newShapefile.Save();
                            if (!save)
                            {
                                MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + pathToSAve + shapefileName
                              + "\r " + newShapefile.ErrorMsg[newShapefile.LastErrorCode] + ". " + Translation.Translate("Creation canceled", langue));
                                return false;
                            }
                            bool stopedititing = newShapefile.StopEditingShapes();
                            newShapefile.Close();
                            return true;
                        }
                        else
                        {
                            if (type == "line") MessageBox.Show(Translation.Translate("Import has less thant 2 points, creation of line impossible",langue));
                            else MessageBox.Show(Translation.Translate("Import has less thant 3 points, creation of polygone impossible",langue));
                        }
                    }
                }
                else
                {
                    foreach (string item in Listcode)
                    {
                        MapWinGIS.Point point = new MapWinGIS.Point();
                        string query = "Select * From GPS Where import_name = '" + importname + "' and CODE = '" + item + "';";
                        List<double> listX = SQlQueryExecuter.RunQueryReaderDouble("X", query);
                        List<double> listY = SQlQueryExecuter.RunQueryReaderDouble("Y", query);
                        numPoints = listX.Count;
                        arrx = new float[numPoints];
                        arry = new float[numPoints];
                        shape1 = new Shape();
                        bool create1 = shape1.Create(ShpfileType.SHP_POLYGON);
                        if (create1)
                        {
                            int index = 0;
                            int shapeindex = 0;
                            foreach (double x in listX)
                            {
                                point = new MapWinGIS.Point();
                                point.x = x;
                                point.y = listY[index];
                                arrx[index] = (float)point.x;
                                arry[index] = (float)point.y;
                                 //shape1.AddPoint(point.x, point.y);
                                var insert1 =  shape1.InsertPoint(point, ref shapeindex); 
                                if (!shape1.IsValid) //MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + shape1.IsValidReason);

                                index++;
                            }
                            point = new MapWinGIS.Point();
                            point.x = listX[0];
                            point.y = listY[0];
                            var insert2 = shape1.InsertPoint(point, ref shapeindex);
                            if (!shape1.IsValid)
                            {
                                //MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + shape1.IsValidReason);
                                var clock = shape1.PartIsClockWise[0];
                            } 

                            //newShapefile.EditAddShape(shape1);
                            var insert3 = newShapefile.EditInsertShape(shape1, ref shapeindex);
                            //if (!insert3) MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + newShapefile.ErrorMsg[newShapefile.LastErrorCode]);
                            

                            if (numPoints > 1)
                            {
                                //newShapefile.DefaultDrawingOptions.DrawShape(IntPtr.Zero, arrx[0], arry[0], shape1, true, 10, 6);
                                //newShapefile.DefaultDrawingOptions.LineColor = Commun.ColorToUInt(color);
                                newShapefile.DefaultDrawingOptions.FillColor = Commun.ColorToUInt(color);
                                //newShapefile.CacheExtents = true;
                                //newShapefile.DefaultDrawingOptions.DrawingMode = tkVectorDrawingMode.vdmGDI;
                                //newShapefile.DefaultDrawingOptions.PointSize = 8;
                            }
                        }
                        else
                        {
                            if (type == "line") MessageBox.Show(Translation.Translate("Import has less thant 2 points, creation of line impossible", langue));
                            else MessageBox.Show(Translation.Translate("Import has less thant 3 points, creation of polygone impossible", langue));
                            return false;
                        }
                    }
                    bool save = newShapefile.Save();
                    if (!save)
                    {
                        MessageBox.Show(Translation.Translate("Error during the creation of the shapefile", langue) + " : " + pathToSAve + shapefileName
                         + "\r " + newShapefile.ErrorMsg[newShapefile.LastErrorCode] + ". " + Translation.Translate("Creation canceled", langue));
                        return false;
                    }
                    bool stopedititing = newShapefile.StopEditingShapes();
                    newShapefile.Close();
                    return true;
                }
                return false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return false;
            }
        }

        #endregion

        private Color GetRandomColor()
        {
            try
            {
                Random random = new Random();
                Color color = Commun.UIntToColor((UInt32)(random.Next()));
                return color;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return Color.LightGreen;
            }

        }

        private string GetExploitationName(string textboxText)
        {
            string[] array = textboxText.Split(new string[] { "From" }, StringSplitOptions.None);
            if (array.Length > 1) return array[1].Trim();
            else return "";
        }

        private int ColorHexToInt(Color color)
        {
            // Store integer 182
            int intValue = color.ToArgb();
            // Convert integer 182 as a hex in a string variable
            string hexValue = intValue.ToString("X");
            // Convert the hex string back to the number
            int intAgain = int.Parse(hexValue, System.Globalization.NumberStyles.HexNumber);
            return intAgain;
        }

        private bool SetCurrentCheckedListBox()
        {
            bool currentboxOk = true;
            if (mTerritoryForm.checkedListBoxTerritoire.SelectedItem != null) currentListBox = mTerritoryForm.checkedListBoxTerritoire;
            else if (mTerritoryForm.checkedListBoxActivite.SelectedItem != null) currentListBox = mTerritoryForm.checkedListBoxActivite;
            else if (mTerritoryForm.checkedListBoxExploitation.SelectedItem != null) currentListBox = mTerritoryForm.checkedListBoxExploitation;
            else currentboxOk = false;
            return currentboxOk;
        }
    }
}
