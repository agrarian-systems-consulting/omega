using MapWinGIS;
using OMEGA.Data_Classes;
using OMEGA.Forms.Territory_Forms;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class TerritoryForm : Form
    {

        internal Territory territory;
        internal MainView mainview;
        private Bitmap memoryImage;
        private PrintDocument doctoprint;
        private PrintAction printAction;
        private int langue = Properties.Settings.Default.Langue;

        internal TerritoryForm()
        {
            try
            {
                InitializeComponent();
                territory = new Territory(this);
                mainview = new MainView(null, null, null);
                subsribeToEvent();
                this.KeyPreview = true;
                Translate();
                Properties.Settings.Default.DoitSauvegarger = true;
                axMap1.ScalebarVisible = true;
               

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void subsribeToEvent()
        {
            try
            {
                this.FormClosing += Form_Close;
                this.KeyDown += Form_KeyDown;
                toolStripZoomIn.Click += toolStripZoomInClick;
                toolStripZoomOut.Click += toolStripZoomOutClick;
                toolStripadd.Click += new EventHandler(territory.toolStripaddClick);
                toolStripSelection.Click += toolStripSelectionClick;
                toolStripSelectionZoomIn.Click += toolStripSelectionZoomInClick;
                toolStripSelectionZoomOut.Click += toolStripSelectionZoomOutClick;
                toolStripFit.Click += toolStripFitClick;
                toolStripremove.Click += new EventHandler(territory.ToolStipRemoveLayerClick);
                checkedListBoxTerritoire.MouseUp += new MouseEventHandler(territory.CheckBoxListeItemClick);
                checkedListBoxTerritoire.SelectedIndexChanged += new EventHandler(territory.CheckedlisteboxCheckedChanged);
                checkedListBoxActivite.MouseUp += new MouseEventHandler(territory.CheckBoxListeItemClick);
                checkedListBoxActivite.SelectedIndexChanged += new EventHandler(territory.CheckedlisteboxCheckedChanged);
                checkedListBoxExploitation.MouseUp += new MouseEventHandler(territory.CheckBoxListeItemClick);
                checkedListBoxExploitation.SelectedIndexChanged += new EventHandler(territory.CheckedlisteboxCheckedChanged);
                saveToolStripMenuItem.Click += new EventHandler(territory.SaveData);
                loadShapeFileToolStripMenuItem.Click += new EventHandler(territory.toolStripaddClick);
                this.Load += new EventHandler(territory.TerritoryForm_Load);
                checkBoxActitivitelayer.CheckedChanged += new EventHandler(territory.CheckBoxCheckedChanged);
                checkBoxExploitation.CheckedChanged += new EventHandler(territory.CheckBoxCheckedChanged);
                checkBoxTerritorylayer.CheckedChanged += new EventHandler(territory.CheckBoxCheckedChanged);
                toolStripremove.Click += new EventHandler(territory.toolStripRemoveClick);
                removeShapeFileToolStripMenuItem.Click += new EventHandler(territory.toolStripRemoveClick);
                toolStripButtonLayertoTop.Click += new EventHandler(territory.ShapeFileToTop_Click);
                toolStripButtonLayerToBottom.Click += new EventHandler(territory.ShapeFileToBottom_Click);
                toolStripButtonMoveLayerup.Click += new EventHandler(territory.MoveShapeFileUp_Click);
                toolStripButtonMoveLayerdown.Click += new EventHandler(territory.MoveShapeFileDown_Click);
                createPointShapefileToolStripMenuItem.Click += new EventHandler(territory.createPointShapefileToolStripMenuItem_Click);
                createPolygoneShapefileToolStripMenuItem.Click += new EventHandler(territory.createPolygoneShapefileToolStripMenuItem_Click);
                createLineShapefileToolStripMenuItem.Click += new EventHandler(territory.createLinehapefileToolStripMenuItem_Click);
                toolStripButtonPoint.Click += new EventHandler(territory.createPointShapefileToolStripMenuItem_Click);
                toolStripButtonLine.Click += new EventHandler(territory.createLinehapefileToolStripMenuItem_Click);
                toolStripButtonpolygone.Click += new EventHandler(territory.createPolygoneShapefileToolStripMenuItem_Click);
                importGPSPointToolStripMenuItem.Click += new EventHandler(mainview.importGPSPointToolStripMenuItem_Click);
                manageGPSPointToolStripMenuItem.Click += new EventHandler(mainview.manageGPSPointToolStripMenuItem_Click);
                checkedListBoxTerritoire.SelectedIndexChanged += new EventHandler(territory.CheckedlisteboxSelectecIndexChanged);
                checkedListBoxExploitation.SelectedIndexChanged += new EventHandler(territory.CheckedlisteboxSelectecIndexChanged);
                checkedListBoxActivite.SelectedIndexChanged += new EventHandler(territory.CheckedlisteboxSelectecIndexChanged);
                dataGridView1.CellClick += new DataGridViewCellEventHandler(territory.DataGridViewClick);
                dataGridView1.CellDoubleClick += new DataGridViewCellEventHandler(territory.DataGridViewDoubleClick);
                importGPSPointtoolStripButton1.Click += new EventHandler(mainview.importGPSPointToolStripMenuItem_Click);
                manageGPStoolStripButton1.Click += new EventHandler(mainview.manageGPSPointToolStripMenuItem_Click);
                dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(territory.dataGridView1_CellValueChanged);
                buttonSave.Click += new EventHandler(territory.ButtonSaveClick);
                buttonCopy.Click += new EventHandler(territory.ButtonCopyValues);
                dataGridView1.MouseClick += new MouseEventHandler(territory.dataGridView1_MouseClick);
                shapefileToolStripMenuItem.Click += new EventHandler(territory.shapefileToolStripMenuItem_Click);
            }
              catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        #region Basic GUI
        private void Form_Close(object sender, EventArgs e)
        {
            
        }


        private void pictureBoxHomeTerritoire_Click(object sender, EventArgs e)
        {
            try
            {
                if (territory.dataModified)
                {
                    ExitForm exitForm = new ExitForm();
                    exitForm.ShowDialog();
                    switch (exitForm.OutPutValue)
                    {
                        case 1:
                            territory.SaveData(this, null);
                            this.Close();
                            break;
                        case 2:
                            this.Close();
                            break;
                        case 3:
                            break;
                    }
                }
                else
                {
                    this.Close();
                }
            }
              catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void checkedListBoxTerritoire_Click(object sender, EventArgs e)
        {
            try
            {
                checkedListBoxActivite.ClearSelected();
                checkedListBoxExploitation.ClearSelected();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void checkedListBoxActivite_Click(object sender, EventArgs e)
        {
            try
            {
                checkedListBoxTerritoire.ClearSelected();
                checkedListBoxExploitation.ClearSelected();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void checkedListBoxExploitation_Click(object sender, EventArgs e)
        {
            try
            {
                checkedListBoxActivite.ClearSelected();
                checkedListBoxTerritoire.ClearSelected();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void toolStripHandClick(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmPan;
        }

        private void toolStripSelectionClick(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmSelection;
        }

        private void toolStripSelectionZoomInClick(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmZoomIn;
        }

        private void toolStripSelectionZoomOutClick(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmZoomOut;
        }

        private void toolStripFitClick(object sender, EventArgs e)
        {
            axMap1.ZoomToMaxVisibleExtents();
        }

        private void toolStripZoomInClick(object sender, EventArgs e)
        {
            axMap1.ZoomIn(5);
        }

        private void toolStripZoomOutClick(object sender, EventArgs e)
        {
            axMap1.ZoomOut(5);
        }



        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.S)
                {
                    territory.SaveData(this, null);
                    e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
                }
                if (e.Alt && e.KeyCode == Keys.F4)
                {
                    this.Close();
                    e.SuppressKeyPress = true;  // Stops other controls on the form receiving event.
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void axMap1_SelectionChanged(object sender, AxMapWinGIS._DMapEvents_SelectionChangedEvent e)
        {
            //MessageBox.Show("hah");
        }

        private void toolStripHand_Click(object sender, EventArgs e)
        {
            axMap1.CursorMode = tkCursorMode.cmPan;
        }

        private void axMap1_DblClick(object sender, EventArgs e)
        {

        }

        private void axMap1_MouseUpEvent(object sender, AxMapWinGIS._DMapEvents_MouseUpEvent e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (territory.dataModified)
                {
                    ExitForm exitForm = new ExitForm();
                    exitForm.ShowDialog();
                    switch (exitForm.OutPutValue)
                    {
                        case 1:
                            territory.SaveData(this, null);
                            this.Close();
                            break;
                        case 2:
                            this.Close();
                            break;
                        case 3:
                            break;
                    }
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void Translate()
        {
            try
            {
                exitToolStripMenuItem.Text = Translation.Translate("Exit", langue);
                loadShapeFileToolStripMenuItem.Text = Translation.Translate("Load shapefile", langue);
                saveToolStripMenuItem.Text = Translation.Translate("Save", langue);
                removeShapeFileToolStripMenuItem.Text = Translation.Translate("Remove shapefile", langue);
                gPSDataManagementToolStripMenuItem.Text = Translation.Translate("GPS Data management", langue);
                createShapefileToolStripMenuItem.Text = Translation.Translate("Create shapefile", langue);
                createPointShapefileToolStripMenuItem.Text = Translation.Translate("Create point shapefile", langue);
                createPolygoneShapefileToolStripMenuItem.Text = Translation.Translate("Create polygone shapefile", langue);
                createLineShapefileToolStripMenuItem.Text = Translation.Translate("Create line shapefile", langue);
                manageGPSPointToolStripMenuItem.Text = Translation.Translate("Manage GPS points", langue);
                importGPSPointToolStripMenuItem.Text = Translation.Translate("Import GPS points", langue);
                labelTerritory.Text = Translation.Translate("Territory layers", langue);
                labelActivitylayer.Text = Translation.Translate("Activity layers", langue);
                labelWorkingfarm.Text = Translation.Translate("Farm layers", langue);
                labelcolor.Text = Translation.Translate("Color management", langue);
                toolStripHand.ToolTipText = Translation.Translate("Move map", langue);
                toolStripSelection.ToolTipText = Translation.Translate("Select layers", langue);
                toolStripZoomIn.ToolTipText = Translation.Translate("Zoom in", langue);
                toolStripZoomOut.ToolTipText = Translation.Translate("Zoom out", langue);
                toolStripFit.ToolTipText = Translation.Translate("Fit to screen", langue);
                toolStripSelectionZoomIn.ToolTipText = Translation.Translate("Cursor into 'zoom in selected area' mode", langue);
                toolStripSelectionZoomOut.ToolTipText = Translation.Translate("Cursor in zoom out mode", langue);
                toolStripButtonLayertoTop.ToolTipText = Translation.Translate("Move selected layer to the top", langue);
                toolStripButtonLayerToBottom.ToolTipText = Translation.Translate("Move selected layer to the bottom", langue);
                toolStripButtonMoveLayerup.ToolTipText = Translation.Translate("Move selected layer up", langue);
                toolStripButtonMoveLayerdown.ToolTipText = Translation.Translate("Move selected layer down", langue);
                toolStripButtonPoint.Text = Translation.Translate("Create point shapefile", langue);
                toolStripButtonpolygone.Text = Translation.Translate("Create polygone shapefile", langue);
                toolStripButtonLine.Text = Translation.Translate("Create line shapefile", langue);
                editToolStripMenuItem1.Text = Translation.Translate("Edit", langue);
                FileToolStripMenuItem.Text = Translation.Translate("File", langue);
                toolStripadd.ToolTipText = Translation.Translate("Add shapefile", langue);
                toolStripremove.ToolTipText = Translation.Translate("Remove shapefile", langue);
                toolStripButtonPoint.ToolTipText = Translation.Translate("Create point shapefile", langue);
                toolStripButtonLine.ToolTipText = Translation.Translate("Create line shapefile", langue);
                toolTip1.SetToolTip(labelTerritory, Translation.Translate("Right click to see display options",langue));
                toolTip1.SetToolTip(labelWorkingfarm, Translation.Translate("Right click to see display options", langue));
                toolTip1.SetToolTip(labelActivitylayer, Translation.Translate("Right click to see display options", langue));
                importGPSPointtoolStripButton1.ToolTipText = Translation.Translate("Import GPS points", langue);
                manageGPSPointToolStripMenuItem.ToolTipText = Translation.Translate("Manage GPS points", langue);
                manageGPStoolStripButton1.Text = Translation.Translate("Manage GPS points", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void Home_Click(object sender, EventArgs e)
        {
            try
            {
                if (territory.dataModified)
                {
                    ExitForm exitForm = new ExitForm();
                    exitForm.ShowDialog();
                    switch (exitForm.OutPutValue)
                    {
                        case 1:
                            territory.SaveData(this, null);
                            this.Close();
                            break;
                        case 2:
                            this.Close();
                            break;
                        case 3:
                            break;
                    }
                }
                else
                {
                    this.Close();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void imageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintMapForm printForm = new PrintMapForm();
            printForm.ShowDialog();
        }


        #endregion

        private void buttonSave_Click(object sender, EventArgs e)
        {

        }

        private void Doctoprint_PrintPage(object sender, PrintPageEventArgs e)
        {
            
        }

        private void seeHideBackgroundToolStripMenuItem_Click(object sender, EventArgs e)
        {
            axMap1.CtlbackColor = Color.White;
            
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //MessageBox.Show("Arf...");
            
        }
    }
}
