using OMEGA.Data_Classes;
using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using MapWinGIS;

namespace OMEGA.Forms
{
    /// <summary>
    /// Class that represents the form that hold the activities data.
    /// </summary>
    internal partial class ActivityMainForm : Form
    {
        private int IdAct;
        private Form mlisteform;
        private int langue = Properties.Settings.Default.Langue;
        private List<int> listRecord = new List<int>();
        /// <summary>
        /// Represents the products on this activity
        /// </summary>
        internal Produit Produit { get; set; }

        /// <summary>
        /// Represents the Inputs for this activity
        /// </summary>
        internal Charge Charge { get; set; }


        /// <summary>
        /// Represents the Inputs for this activity
        /// </summary>
        internal ChargeFQuantite ChargeF { get; set; }


        /// <summary>
        /// Represents the Externalities for this activity
        /// </summary>
        internal Externalite Externalite { get; set; }

        /// <summary>
        /// Represents the calendar of work for this activity
        /// </summary>

        internal Periode Periode { get; set; }
        /// <summary>
        /// Represents the features of this activity
        /// </summary>

        internal CritereCulture CritereCulture { get; set; }
        internal event EventHandler<SaveDataEvent> SaveDataProduit;
        internal event EventHandler<SaveDataEvent> SaveDataCharge;
        internal event EventHandler<SaveDataEvent> SaveDataExternalite;
        internal event EventHandler<ShowListEvent> ShowList;

        /// <summary>
        /// Contructor for the Activity main form, if it's a new activity
        /// </summary>
        /// <param name="nom"></param>
        internal ActivityMainForm(string nom)
        {
            try
            {
                InitializeComponent();
                LoadComboBox();
                IdAct = SaveNewActivite(nom, "Activite");
                LoadActivityName("Activite", nom);
                if (File.Exists(Properties.Settings.Default.FichierTraduction))
                {
                    SetCaption();
                }
                LoadActivityMap();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            
        }

        /// <summary>
        /// Contructor for thr activoty main form if the activity already exists
        /// </summary>
        /// <param name="idAct"></param>
        internal  ActivityMainForm(int idAct)
        {
            try
            {
                InitializeComponent();
                IdAct = idAct;
                ShowOrHideTab(idAct);
                LoadComboBox();
                LoadActivityName("Activite");
                if (File.Exists(Properties.Settings.Default.FichierTraduction))
                {
                    SetCaption();
                }
                LoadActivityMap();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Load the map that represents the activity
        /// </summary>
        private void LoadActivityMap()
        {
            try
            {
                string query = "Select * From SHP_Info Where IdActivite = '" + IdAct + "';";
                List<string> listpath = SQlQueryExecuter.RunQueryReaderStr("path", query);
                List<int> listcolor = SQlQueryExecuter.RunQueryReaderInt("Color_ARGB", query);

                foreach (string path in listpath)
                {
                    if (Commun.LoadShapeFile(path))
                    {
                        Shapefile ActiviteShapeFile = new Shapefile();
                        ActiviteShapeFile.Open(path);

                        int handler1 = axMap1.AddLayer(ActiviteShapeFile, true);
                        Color color1 = Color.FromArgb(listcolor[listpath.IndexOf(path)]);
                        ActiviteShapeFile.DefaultDrawingOptions.FillColor = Commun.ColorToUInt(color1);
                        axMap1.set_LayerVisible(handler1, true);
                        axMap1.ZoomToMaxVisibleExtents();
                    }
                    string[] array = path.Split('\\');
                    for (int i = 0; i <= array.Length; i++)
                    {
                        if (array[i].Contains(".shp"))
                        {
                            textBoxshapefile.Text = array[i];
                            break;
                        }
                    }
                    textBoxDirection.Text = path.Remove(path.Length - textBoxshapefile.Text.Length, textBoxshapefile.Text.Length);
                    textBoxDirection.ReadOnly = true;
                    textBoxshapefile.ReadOnly = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Hide the tab if not needed for this activity (depend on the activity type)
        /// </summary>
        /// <param name="idAct"></param>
        private void ShowOrHideTab(int idAct)
        {
            try
            {
                string nomtype = "";
                string query = ActivityQuery.SelectTypeActivityQuery(idAct);
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                if (Commun.ListHasValue(list))
                {
                    nomtype = list[0];
                }
                if (nomtype.Contains("Animaux"))
                {
                    tabGeneral.TabPages.Remove(tabPageChPied);
                    tabGeneral.TabPages.Remove(tabPagePiedHa);
                    tabGeneral.TabPages.Remove(tabPageProdImmo);
                    tabGeneral.TabPages.Remove(tabPageAvance);
                }
                else
                {
                    tabGeneral.TabPages.Remove(tabPageValInv);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// translation of the form features
        /// </summary>
        private void SetCaption()
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;
                buttonOkGeneral.Text = Translation.Translate("OK", langue);
                buttonCancelGeneral.Text = Translation.Translate("Cancel", langue);
                buttonSAveGeneral.Text = Translation.Translate("Save", langue);
                tabPageGeneral.Text = Translation.Translate("General information", langue);
                tabPageCharges.Text = Translation.Translate("Charges", langue);
                TapPageExternalites.Text = Translation.Translate("Externalites", langue);
                tabPageProduits.Text = Translation.Translate("Products", langue);
                tabPeriode.Text = Translation.Translate("Calendars", langue);
                buttonResultMarge.Text = Translation.Translate("Margin", langue);
                radioButtonNo.Text = Translation.Translate("No", langue);
                radioButtonYes.Text = Translation.Translate("Yes", langue);
                groupBoxGPS.Text = Translation.Translate("Info GPS", langue);
                labelShapefile.Text = Translation.Translate("Shapefile", langue) + ":";
                labelDirShape.Text = Translation.Translate("Directory", langue) +":";
                buttonAddProduit.Text = Translation.Translate("Add", langue);
                buttonAddCharges.Text = Translation.Translate("Add", langue);
                this.Text = Translation.Translate("Activity", langue);
                buttonGroupeC.Text = Translation.Translate("Enable group", langue);
                buttonAddExtern.Text = Translation.Translate("Add", langue);
                buttonAddQuantitef.Text = Translation.Translate("Add", langue);
                buttonRemoveChargef.Text = Translation.Translate("Remove", langue);
                buttonSaveChargef.Text = Translation.Translate("Save", langue);
                buttonCancelChargef.Text = Translation.Translate("Cancel", langue);
                label1CultPrin.Text = Translation.Translate("Main activity of the crop", langue);
                buttonListeP.Text = Translation.Translate("Products lists", langue);
                buttonListeCharge.Text = Translation.Translate("Costs lists", langue);
                buttonSaveChPied.Text = Translation.Translate("Save", langue);
                buttonSaveProdPied.Text = Translation.Translate("Save", langue);
                buttonOkPiedHa.Text = Translation.Translate("Save", langue);
                buttonremovepiedha.Text = Translation.Translate("Remove", langue);
                buttonRemoveProdPied.Text = Translation.Translate("Remove", langue);
                buttonRemoveChPied.Text = Translation.Translate("Remove", langue);
                buttonCancelChPied.Text = Translation.Translate("Cancel", langue);
                buttonCancelProdPied.Text = Translation.Translate("Cancel", langue);
                buttonCancelPiedHa.Text = Translation.Translate("Cancel", langue);
                buttonduplicatepiedha.Text = Translation.Translate("Duplicate", langue);
                buttonDuplicateProdPied.Text = Translation.Translate("Duplicate", langue);
                buttonDuplicateChPied.Text = Translation.Translate("Duplicate", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Set the name of the activity
        /// </summary>
        /// <param name="table"></param>
        /// <param name="nom"></param>
        private void LoadActivityName(string table, string nom = "")
        {
            try
            {
                string query;
                if (nom == "" && IdAct > 0)
                {
                     query = "SELECT " + table + ".Nom From " + table + " WHERE " + table + ".ID = '" + IdAct + "'  ;";
                    List<string> ListField = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    if (ListField.Count > 0)
                    {
                        label1NomActivity.Text = ListField[0];
                        textBoxNom.Text = ListField[0];
                    }
                }
                else
                {
                    label1NomActivity.Text = nom;
                    textBoxNom.Text = nom;
                }
                query = "Select Culture_princ From " + table + " WHERE " + table + ".ID = '" + IdAct + "'  ;";
                List<int> ListmainAct = SQlQueryExecuter.RunQueryReaderInt("Culture_princ", query);
                if (ListmainAct.Count >0)
                {
                    if (ListmainAct[0] == 0)
                    {
                        radioButtonYes.Checked = false;
                        radioButtonNo.Checked = true;
                    }
                    else
                    {
                        radioButtonYes.Checked = true;
                        radioButtonNo.Checked = false;
                    }
                }
                else
                {
                    radioButtonYes.Checked = true;
                    radioButtonNo.Checked = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// This region is where you manage data for each tab
        /// </summary>
        #region Load Tab and Data

        /// <summary>
        /// Management of the Product tab
        /// </summary>
        private void LoadProduitGrid()
        {
            try
            {
                string query = ActivityQuery.LoadProductToActivityQuery(IdAct);
                List<int> ListIdProduit = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                string type = "";
                if (comboBoxTypeAct.Text == Translation.Translate("Crop",langue))
                {
                    type = "annuelle";
                }
                if (comboBoxTypeAct.Text == Translation.Translate("Perenial", langue))
                {
                    type = "perenne";
                }
                if (comboBoxTypeAct.Text == Translation.Translate("pluriannual", langue))
                {
                    type = "pluriannuelle";
                }

                Produit = new Produit(dataGridViewProduit, null ,buttonSaveProduit,
                   buttonCancelProduit, buttonRemoveProduit, buttonDuplicateProduit, buttonProposiP, buttonGroupeP,
                   ListIdProduit, IdAct, pictureBoxProduit,this, type);

                // when the form is loaded, we subscribe to the events 
                buttonRemoveProduit.Click += new EventHandler(Produit.ButtonRemoveRow);
                buttonDuplicateProduit.Click += new EventHandler(Produit.ButtonDuplicate);
                buttonCancelProduit.Click += new EventHandler(Produit.buttonCancel);
                pictureBoxTravail.Click += new EventHandler(Produit.ExportTable);
                pictureBoxTravail.MouseLeave += new EventHandler(Produit.pictureBox_MouseLeave);
                pictureBoxTravail.MouseEnter += new EventHandler(Produit.pictureBox_MouseEnter);
                buttonGroupeP.Click += new EventHandler(Produit.EnableGroupe);
                dataGridViewProduit.CellValueChanged += new DataGridViewCellEventHandler(Produit.dataGridView1_CellValueChanged);
                dataGridViewProduit.RowPostPaint += new DataGridViewRowPostPaintEventHandler(Produit.DataGridViewDetailRowPostPaint);
                dataGridViewProduit.CellClick += new DataGridViewCellEventHandler(Produit.DataGridView1CellClick);
                dataGridViewProduit.CellEndEdit += new DataGridViewCellEventHandler(Produit.DataGridView1CellEndEdit);
                dataGridViewProduit.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(Produit.DataGridView1EditingControlShowing);
                dataGridViewProduit.MouseClick += new MouseEventHandler(Produit.DataGridViewMouseClick);
                dataGridViewProduit.Sorted += new EventHandler(Produit.DataGridView1_Sorted);
                ShowList += new EventHandler<ShowListEvent>(Liste_Click);
                buttonProposiP.Click += new EventHandler(Produit.ButtonPropositionClick);
                buttonNotesProduit.Click += new EventHandler(Produit.NoteForm);
                SaveDataProduit += new EventHandler<SaveDataEvent>(Produit.ButtonSaveData);
                buttonAddProduit.Click += new EventHandler(Produit.ButtonAdd);

                Produit.SetColor();
                toolTip1.SetToolTip(buttonListeP, Translation.Translate("Products lists", langue));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Management of the Cost tab
        /// </summary>
        private void LoadChargeGrid()
        {
            try
            {
                string query = ActivityQuery.LoadChargeToActivtyQuery(IdAct);
                List<int> ListIdCharge = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                string type = "";
                if (comboBoxTypeAct.Text == Translation.Translate("Crop", langue))
                {
                    type = "annuelle";
                }
                if (comboBoxTypeAct.Text == Translation.Translate("Perenial", langue))
                {
                    type = "perenne";
                }
                if (comboBoxTypeAct.Text == Translation.Translate("Pluriannual", langue))
                {
                    type = "pluriannuelle";
                }

                Charge = new Charge(dataGridViewCharge, null,
                    buttonSaveCharge, buttonCancelCharge, buttonRemoveCharge,
                    buttonDuplicateCharge, buttonListeCharge, 0, buttonGroupeC,
                    ListIdCharge, IdAct, pictureBoxCharge,this, type);

                // subscribing to the event
                buttonRemoveCharge.Click += new System.EventHandler(Charge.ButtonRemoveRow);
                buttonDuplicateCharge.Click += new EventHandler(Charge.ButtonDuplicate);
                buttonCancelCharge.Click += new EventHandler(Charge.buttonCancel);
                pictureBoxCharge.Click += new EventHandler(Charge.ExportTable);
                pictureBoxCharge.MouseLeave += new EventHandler(Charge.pictureBox_MouseLeave);
                pictureBoxCharge.MouseEnter += new EventHandler(Charge.pictureBox_MouseEnter);
                buttonGroupeC.Click += new EventHandler(Charge.EnableGroupe);
                dataGridViewCharge.CellClick += new DataGridViewCellEventHandler(Charge.dataGridView1CellClick);
                buttonListeCharge.Click += buttonListeCharge_Click;
                dataGridViewCharge.Sorted += new EventHandler(Charge.dataGridView1_Sorted);
                ShowList += new EventHandler<ShowListEvent>(Liste_Click);
                dataGridViewCharge.RowPostPaint += new DataGridViewRowPostPaintEventHandler(Charge.dataGridViewDetailRowPostPaint);
                SaveDataCharge += new EventHandler<SaveDataEvent>(Charge.ButtonSaveData);
                buttonAddCharges.Click += new EventHandler(Charge.ButtonAdd);
                dataGridViewCharge.CellEndEdit += new DataGridViewCellEventHandler(Charge.dataGridView1CellEndEdit);
                dataGridViewCharge.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(Charge.dataGridView1EditingControlShowing);

                Charge.SetColor();
                toolTip1.SetToolTip(buttonListeCharge, Translation.Translate("Costs lists", langue));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Management of the Cost tab
        /// </summary>
        private void LoadChargefGrid()
        {
            try
            {
                string query = ActivityQuery.LoadChargeToActivtyQuery(IdAct);
                List<int> ListIdCharge = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                string type = "";
                if (comboBoxTypeAct.Text == Translation.Translate("Crop", langue))
                {
                    type = "annuelle";
                }
                if (comboBoxTypeAct.Text == Translation.Translate("Perenial", langue))
                {
                    type = "perenne";
                }
                if (comboBoxTypeAct.Text == Translation.Translate("Pluriannual", langue))
                {
                    type = "pluriannuelle";
                }

                ChargeF = new ChargeFQuantite(dataGridViewChargef, null,
                    buttonSaveChargef, buttonCancelChargef, buttonRemoveChargef,
                    buttonDuplicateChargef, 2, buttonGroupeC,
                    ListIdCharge, IdAct, pictureBoxCharge, this, type);

                // subscribing to the event
                buttonRemoveChargef.Click += new System.EventHandler(ChargeF.ButtonRemoveRow);
                buttonSaveChargef.Click += new System.EventHandler(ChargeF.ButtonSaveData);
                buttonCancelChargef.Click += new EventHandler(ChargeF.buttonCancel);
                pictureBoxChargef.Click += new EventHandler(ChargeF.ExportTable);
                pictureBoxChargef.MouseLeave += new EventHandler(ChargeF.pictureBox_MouseLeave);
                pictureBoxChargef.MouseEnter += new EventHandler(ChargeF.pictureBox_MouseEnter);
                buttonGroupeC.Click += new EventHandler(ChargeF.EnableGroupe);
                dataGridViewChargef.CellClick += new DataGridViewCellEventHandler(ChargeF.dataGridView1CellClick);
                dataGridViewChargef.Sorted += new EventHandler(ChargeF.dataGridView1_Sorted);
                ShowList += new EventHandler<ShowListEvent>(Liste_Click);
                dataGridViewCharge.RowPostPaint += new DataGridViewRowPostPaintEventHandler(ChargeF.dataGridViewDetailRowPostPaint);
                SaveDataCharge += new EventHandler<SaveDataEvent>(ChargeF.ButtonSaveData);
                buttonAddQuantitef.Click += new EventHandler(ChargeF.ButtonAdd);
                dataGridViewChargef.CellEndEdit += new DataGridViewCellEventHandler(ChargeF.dataGridView1CellEndEdit);
                dataGridViewChargef.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(ChargeF.dataGridView1EditingControlShowing);

                ChargeF.SetColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Management of the Externalitie tab
        /// </summary>
        private void LoadExternaliteGrid()
        {
            try
            {
                string query = ActivityQuery.LoadExternalityToActivityQuery(IdAct);
                List<int> ListIdExtern = SQlQueryExecuter.RunQueryReaderInt("ID", query);

                Externalite = new Externalite(dataGridViewExtern, null,
                buttonSaveExtern, buttonCancelExtern, buttonRemoveExtern,
                buttonDuplicateExtern, buttonpropositionEx, buttongroupeExt, ListIdExtern,
                0, null, this);

                // when the form is loaded, we subscribe to the events 
                buttonRemoveExtern.Click += new EventHandler(Externalite.ButtonRemoveRow);
                buttonDuplicateExtern.Click += new EventHandler(Externalite.ButtonDuplicate);
                buttonCancelExtern.Click += new EventHandler(Externalite.buttonCancel);
                pictureBoxExterne.Click += new EventHandler(Externalite.ExportTable);
                dataGridViewExtern.RowPostPaint += new DataGridViewRowPostPaintEventHandler(Externalite.dataGridViewDetailRowPostPaint);
                dataGridViewExtern.CellClick += new DataGridViewCellEventHandler(Externalite.dataGridView1CellClick);
                buttonAddExtern.Click += new EventHandler(Externalite.ButtonAdd);
                dataGridViewExtern.Sorted += new EventHandler(Externalite.dataGridView1_Sorted);
                pictureBoxExterne.MouseLeave += new EventHandler(Externalite.pictureBox_MouseLeave);
                pictureBoxExterne.MouseEnter += new EventHandler(Externalite.pictureBox_MouseEnter);
                ShowList += new EventHandler<ShowListEvent>(Liste_Click);
                SaveDataExternalite += new EventHandler<SaveDataEvent>(Externalite.ButtonSaveData);
                Externalite.SetColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
           
        }
        private void LoadComboBox()
        {
            try
            {
                List<string> ListField2 = new List<string>();
                string query = SQLQueryBuilder.SelectQuery("Type", "Nom");
                List<string> ListField = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string item in ListField)
                {
                    ListField2.Add(Translation.Translate(item, langue));
                }
                ListField2.Add(Translation.Translate("Add", langue)+"...");
                comboBoxTypeAct.DataSource = ListField2;
                if (IdAct != -1)
                {
                    query = ActivityQuery.LoadComboBoxTypeQuery(IdAct);
                    ListField = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    if (ListField.Count > 0) comboBoxTypeAct.Text = Translation.Translate(ListField[0], langue);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
               
        /// <summary>
        /// Management of the Calendars tab
        /// </summary>
        private void LoadPeriode()
        {
            try
            {
                string query = PeriodeQuery.LoadPeriodeQuery();
                List<int> ListIdPeriode = SQlQueryExecuter.RunQueryReaderInt("IdPeriode", query);

                Periode = new Periode(buttonOKPeriode, buttonCancelPeriode, buttonAddPeriode,
                dataGridViewPeriode, dataGridViewDetailPeriode, panelPeriode,
                ListIdPeriode, IdAct, "Act", true);

                dataGridViewPeriode.CellClick += new DataGridViewCellEventHandler(Periode.dataGridViewPeriodeCellClickForActivity);
                dataGridViewDetailPeriode.CellValueChanged += new DataGridViewCellEventHandler(Periode.dataGridViewDetailPeriodeCellValueChanged);
                dataGridViewDetailPeriode.KeyPress += new KeyPressEventHandler(Periode.DatagridDetailPeriodKeyPress);
                buttonOKPeriode.Click += new EventHandler(Periode.ButtonOkClickForActivity);
                buttonAddPeriode.Click += new EventHandler(Periode.buttonAddPeriode);
                buttonCopy.Click += new EventHandler(Periode.ButtonCopyClick);
                pictureBoxTravail.Click += new EventHandler(Periode.buttonExport);
                pictureBoxTravail.MouseLeave += new EventHandler(Periode.pictureBox_MouseLeave);
                pictureBoxTravail.MouseEnter += new EventHandler(Periode.pictureBox_MouseEnter);
                buttonCopyfrom.Click += new EventHandler(Periode.ButtonCopyFromClick);
                dataGridViewDetailPeriode.RowPostPaint += new DataGridViewRowPostPaintEventHandler(Periode.dataGridViewDetailRowPostPaint);
                Periode.SetColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }
      
        /// <summary>
        /// Management of the tab cost per foot
        /// </summary>
        private void LoadPageChPied()
        {
            try
            {
                CritereCulture = new CritereCulture("ChPied",IdAct);
                CritereCulture.LoadDataGridView(dataGridViewChPied);
                ShowList += new EventHandler<ShowListEvent>(Liste_Click);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Management of the tab product per foot
        /// </summary>
        private void LoadPageProdPied()
        {
            try
            {
                CritereCulture = new CritereCulture("ProdPied", IdAct);
                CritereCulture.LoadDataGridView(dataGridViewProdPied);
                ShowList += new EventHandler<ShowListEvent>(Liste_Click);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Management of the tab foot per Ha
        /// </summary>
        private void LoadPagePiedHa()
        {
            try
            {
                CritereCulture = new CritereCulture("PiedHa",IdAct);
                CritereCulture.LoadDataGridView(dataGridViewPiedHa);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Management of the Fixed asset tab
        /// </summary>
        private void LoadProdImmo()
        {
            try
            {
                CritereCulture = new CritereCulture("Prod Immo",IdAct );
                CritereCulture.LoadDataGridView(dataGridViewProdImmo1, dataGridViewProdImmo2);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Management of the avance tab
        /// </summary>
        private void LoadAvance()
        {
            try
            {
                CritereCulture = new CritereCulture("Avance" , IdAct, dataGridViewAvance1);
                CritereCulture.LoadDataGridView(dataGridViewAvance1, dataGridViewAvance2);
                buttoncopyava.Click += new EventHandler(CritereCulture.ButtonCopyClick);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }
        #endregion

        /// <summary>
        /// This Region contains all the event that allow the user to interact with the forms
        /// </summary>
        #region GUI

        /// <summary>
        /// Occurs when the main form is loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ActivityMainForm_Load(object sender, EventArgs e)
        {
            try
            {
                // sur le load du controlTab, on crée un nouvelle event qui aura lieu à chaque changement de selection d'un onglet
                tabGeneral.Selecting += new TabControlCancelEventHandler(tabControlGeneral_Selecting);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        /// <summary>
        /// Occurs when the user clicks on a tab to open it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabControlGeneral_Selecting(object sender, TabControlCancelEventArgs e)
        {
            try
            {
                TabPage currentTab = (sender as TabControl).SelectedTab;
                currentTab.BringToFront();
                switch (currentTab.Name)
                {
                    case "tabPageProduits":
                        LoadProduitGrid();
                        break;
                    case "tabPageCharges":
                        LoadChargeGrid();
                        break;
                    case "Information General":
                        break;
                    case "TapPageExternalites":
                        LoadExternaliteGrid();
                        break;
                    case "tabPeriode":
                        LoadPeriode();
                        break;
                    case "tabPageAvance":
                        LoadAvance();
                        break;
                    case "tabPageProdImmo":
                        LoadProdImmo();
                        break;
                    case "tabPagePiedHa":
                        LoadPagePiedHa();
                        break;
                    case "tabPageProdPied":
                        LoadPageProdPied();
                        break;
                    case "tabPageChPied":
                        LoadPageChPied();
                        break;
                    case "tabPageValInv":
                        break;
                    case "tabChargef":
                        LoadChargefGrid();
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }

        }

        /// <summary>
        /// Occurs when the user clicks on OK button of a tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonokCharge_Click(object sender, EventArgs e)
        {
            try
            { 
                SaveDataCharge?.Invoke(this, new SaveDataEvent(false, "Activite"));
                SaveDataActivity("Charges", Charge.ListeRecordToDelete, Charge.DataGridView, null);
                Charge.RefreshView("Activite");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void buttonOkProduit_Click(object sender, EventArgs e)
        {
            try
            { 
                SaveDataProduit?.Invoke(this, new SaveDataEvent(false, "Activite"));
                SaveDataActivity("Produits", Produit.ListeRecordToDelete, null, Produit.mDataGridView);
                Produit.RefreshView("Activite");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

            //this.Close();
        }
        private void buttonokExtern_Click(object sender, EventArgs e)
        {
            try
            { 
                SaveDataExternalite?.Invoke(this, new SaveDataEvent(false, "Activite"));
                SaveDataActivity("Externalites", Externalite.ListeRecordToDelete, null, null, Externalite.DicoInfoUserExternalite);
                Externalite.RefreshView("Activite");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void buttonOkAvance_Click(object sender, EventArgs e)
        {
            CritereCulture = new CritereCulture("Avance", IdAct, dataGridViewAvance1);
            CritereCulture.SaveData(dataGridViewAvance1, listRecord);
        }
        private void buttonOkProdImmo_Click(object sender, EventArgs e)
        {
            try
            {
                CritereCulture = new CritereCulture("Prod Immo", IdAct);
                CritereCulture.SaveData(dataGridViewProdImmo1, listRecord);
                listRecord.Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }
        private void buttonOkPiedHa_Click(object sender, EventArgs e)
        {
            try
            {
                CritereCulture = new CritereCulture("PiedHa", IdAct);
                CritereCulture.SaveData(dataGridViewPiedHa, listRecord);
                listRecord.Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }
        private void buttonOkProdPied_Click(object sender, EventArgs e)
        {
            try
            {
                CritereCulture = new CritereCulture("ProdPied", IdAct);
                CritereCulture.SaveData(dataGridViewProdPied, listRecord);
                listRecord.Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }
        private void buttonOkChPied_Click(object sender, EventArgs e)
        {
            try
            {
                CritereCulture = new CritereCulture("ChPied", IdAct);
                CritereCulture.SaveData(dataGridViewChPied, listRecord);
                listRecord.Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void buttonAddPeriode_Click(object sender, EventArgs e)
        {
            try
            {
                PeriodeForm periodeForm = new PeriodeForm();
                periodeForm.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        /// <summary>
        /// Occurs when the user clicks on SAVE button of a tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveExtern_Click(object sender, EventArgs e)
        {
            try
            { 
                SaveDataExternalite?.Invoke(this, new SaveDataEvent(false, "Activite"));
                SaveDataActivity("Externalites", Externalite.ListeRecordToDelete, null, null, Externalite.DicoInfoUserExternalite);
                Externalite.RefreshView("Activite");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void buttonSaveCharge_Click(object sender, EventArgs e)
        {
            try
            { 
                SaveDataCharge?.Invoke(this, new SaveDataEvent(false, "Activite"));
                SaveDataActivity("Charges", Charge.ListeRecordToDelete, Charge.DataGridView, null, null);
                Charge.RefreshView("Activite");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void buttonSaveProduit_Click(object sender, EventArgs e)
        {
            try
            {

                SaveDataProduit?.Invoke(this, new SaveDataEvent(false, "Activite"));
                SaveDataActivity("Produits", Produit.ListeRecordToDelete, null, Produit.mDataGridView, null);
                Produit.RefreshView( "Activite");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
          
        /// <summary>
        /// Occurs when the user clicks on the list button to select a specific item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Liste_Click(object sender, ShowListEvent e)
        {
            try
            {
                if (mlisteform == null)
                {
                    mlisteform = new ListeForm(e.query,e.table,e.currentTab,e.activity,e.exploitation,e.currentGridView,e.currentDataTable,null,null,e.ID);
                    mlisteform.Show();
                }
                else if (mlisteform.Visible == false)
                {
                    mlisteform = new ListeForm(e.query, e.table, e.currentTab, e.activity, e.exploitation, e.currentGridView, e.currentDataTable,null,null,e.ID);
                    mlisteform.Show();
                }
                else mlisteform.BringToFront();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void dataGridViewDetailPeriode_KeyPress(object sender, KeyPressEventArgs e)
        {
            Periode.DatagridDetailPeriodKeyPress(sender, e);
        }

        /// <summary>
        /// Occurs when the user clickc on a the keyboard.
        /// This Override function is used only on the calendars tab to allow user to only input numeric value
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessDialogKey(Keys keyData)
        {
            try
            {
                if (Periode != null)
                {
                    //if (Periode.DataGridViewPeriodeDetail.ReadOnly == true && Periode.DataGridViewPeriodeDetail.Visible == true)
                    {
                        switch (keyData)
                        {
                            case Keys.D0:
                                return false;
                            case Keys.NumPad0:
                                return false;
                            case Keys.D1:
                                return false;
                            case Keys.NumPad1:
                                return false;
                            case Keys.D2:
                                return false;
                            case Keys.NumPad2:
                                return false;
                            case Keys.D3:
                                return false;
                            case Keys.NumPad3:
                                return false;
                            case Keys.D4:
                                return false;
                            case Keys.NumPad4:
                                return false;
                            case Keys.D5:
                                return false;
                            case Keys.NumPad5:
                                return false;
                            case Keys.D6:
                                return false;
                            case Keys.NumPad6:
                                return false;
                            case Keys.D7:
                                return false;
                            case Keys.NumPad7:
                                return false;
                            case Keys.D8:
                                return false;
                            case Keys.NumPad8:
                                return false;
                            case Keys.D9:
                                return false;
                            case Keys.NumPad9:
                                return false;
                            default:
                                return true;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            // return the key to the base class if not used.
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// Occurs when the user clicks on the list button to select a specific item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ButtonListeChargepied_Click(object sender, EventArgs e)
        {
            string query = "";
            query = " Select * From Caract_activite Where IdActivite = '" + IdAct + "' AND IdCharges is not null;";

            if (Commun.ListHasValue(SQlQueryExecuter.RunQueryReaderInt("ID", query)))
            {
                DialogResult result =  MessageBox.Show(Translation.Translate("This activity has already cost. Do you want to delete all the cost affected to this activity ?", langue),"Warning",MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    query = "Delete From Caract_Activite Where IdActivite = '" + IdAct + "' AND IdCharges is not null;";
                    SQlQueryExecuter.RunQuery(query);
                    query = SQLQueryBuilder.SelectQuery("Charges", "ID,Nom");
                    ShowList?.Invoke(this, new ShowListEvent("ChargesPied", query, "TabChargesPied", this, null, dataGridViewChPied, CritereCulture.table, null, null, IdAct));
                }
            }
            else
            {
                query = SQLQueryBuilder.SelectQuery("Charges", "ID,Nom");
                ShowList?.Invoke(this, new ShowListEvent("ChargesPied", query, "TabChargesPied", this, null, dataGridViewChPied,CritereCulture.table,null,null,IdAct));
            }
        }
               
        private void buttonListeCharge_Click(object sender, EventArgs e)
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Charges", "ID,Nom");
                ShowList?.Invoke(this, new ShowListEvent("Charges", query, "Charges", this, null, dataGridViewCharge));
                Charge.mDataSaved = false;
            }
           
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void buttonListeP_Click(object sender, EventArgs e)
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Produits", "ID,Nom");
                ShowList?.Invoke(this, new ShowListEvent("Produits", query, "TabProduits", this, null, dataGridViewProduit,null));
                Produit.mDataSaved = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void buttonListeProduitpied_Click(object sender, EventArgs e)
        {
            string query = "";
            query = " Select * From Caract_activite Where IdActivite = '" + IdAct + "' AND IdProduits is not null;";
           
            if (Commun.ListHasValue(SQlQueryExecuter.RunQueryReaderInt("ID", query)))
            {
                DialogResult result = MessageBox.Show(Translation.Translate("This activity has already product. Do you want to delete all the cost affected to this activity ?", langue), "Warning", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    query = "Delete From Caract_Activite Where IdActivite = '" + IdAct + "' AND IdProduits is not null;";
                    SQlQueryExecuter.RunQuery(query);
                    query = SQLQueryBuilder.SelectQuery("Produits", "ID,Nom");
                    ShowList?.Invoke(this, new ShowListEvent("ProdPied", query, "TabProduitsPied", this, null, dataGridViewChPied, null,null,null,IdAct));
                }
            }
            else
            {
                query = SQLQueryBuilder.SelectQuery("Produits", "ID,Nom");
                ShowList?.Invoke(this, new ShowListEvent("ProdPied", query, "TabProduitsPied", this, null, dataGridViewProdPied, null, null, null, IdAct));
            }
        }

        private void buttonListExt_Click(object sender, EventArgs e)
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Externalites", "ID,Nom");
                ShowList?.Invoke(this, new ShowListEvent("Externalites", query, "TabExternalites", this, null, dataGridViewExtern));
                Externalite.mDataSaved = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void buttonListeExter_Click(object sender, EventArgs e)
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Externalites", "ID,Nom");
                ShowList?.Invoke(this, new ShowListEvent("Externalites", query, "TabExternalites", this, null, dataGridViewExtern));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void buttoncharge_Click(object sender, EventArgs e)
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Charges", "ID,Nom");
                ShowList?.Invoke(this, new ShowListEvent("ChargesCharges", query, "Charges", this, null, dataGridViewCharge));
                ChargeF.mDataSaved = false;
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void buttonproduct_Click(object sender, EventArgs e)
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Produits", "ID,Nom");
                ShowList?.Invoke(this, new ShowListEvent("ProduitsCharges", query, "TabProduits", this, null, dataGridViewProduit, CritereCulture.table));
                ChargeF.mDataSaved = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user clicks on Marge/result button. Its diplay a new form to show the graphs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonResultMarge_Click(object sender, EventArgs e)
        {
            StandardForm margeForm = new StandardForm(IdAct, "marge");
            margeForm.ShowDialog();
        }

        /// <summary>
        /// Occurs when the user wants to add notes 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonNotes_Click(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("Activite");
            form.ShowDialog();
        }

        /// <summary>
        /// Occurs when the user remove an item of a tab (only for this specific tab)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonRemoveProdPied_Click(object sender, EventArgs e)
        {
            try
            {
                CritereCulture = new CritereCulture("ProdPied", IdAct, dataGridViewProdPied);
                listRecord = CritereCulture.RemoveData(dataGridViewProdPied.SelectedCells, "ProdPied");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }
        private void buttonRemoveChPied_Click(object sender, EventArgs e)
        {
            try
            {
                CritereCulture = new CritereCulture("ChPied", IdAct, dataGridViewChPied);
                listRecord = CritereCulture.RemoveData(dataGridViewChPied.SelectedCells, "ChPied");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }
        private void buttonremovepiedha_Click(object sender, EventArgs e)
        {
            try
            {
                CritereCulture = new CritereCulture("PiedHa", IdAct, dataGridViewPiedHa);
                listRecord = CritereCulture.RemoveData(dataGridViewPiedHa.SelectedCells, "PiedHa");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        /// <summary>
        /// Occurs when the user wants to save data and click on the save or Ok button on the main tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonOkGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                buttonSAveGeneral_Click(sender, e);
                this.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }
        private void buttonSAveGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                string oldname = "";
                string nom = textBoxNom.Text;
                string type = Translation.QuickTranslate(comboBoxTypeAct.Text, 0);
                string query = "";
                int idtype = SQLQueryBuilder.FindId("Type", "ID", "Nom", type);
                int MainActivity = 1;
                if (radioButtonNo.Checked) MainActivity = 0;
                else if (radioButtonYes.Checked) MainActivity = 1;
                if (IdAct == 0)
                {
                    oldname = SQLQueryBuilder.FindName("Activite", "Nom", "ID", IdAct);
                    query = ActivityQuery.InsertNewActivityQuery(nom, idtype, MainActivity);
                    SQlQueryExecuter.RunQuery(query);
                    IdAct = Commun.GetMaxId("ID", "Activite");
                    query = SQLQueryBuilder.SetEncoursTo0("Activite", IdAct);
                    SQlQueryExecuter.RunQuery(query);
                }
                else
                {
                    query = ActivityQuery.UpdateActivityQuery(nom, IdAct, idtype, MainActivity);
                    SQlQueryExecuter.RunQuery(query);
                }
                SetNewNameInResultCalcul(nom, oldname);
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }


        private void SetNewNameInResultCalcul(string nom, string previousname)
        {
            try
            {
                string query = "Update Result_Calcul Set Nom = '" + nom + "' Where Nom ='" + previousname + "';";
                SQlQueryExecuter.RunQuery(query);
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        #endregion

        /// <summary>
        /// Save the data of an activity.
        /// the list are optionales because it can save only one type of data at a time (eg. product data or cost data).
        /// </summary>
        /// <param name="Table"></param>
        /// <param name="ListToDelete"></param>
        /// <param name="MylistC"></param>
        /// <param name="MylistP"></param>
        /// <param name="MylistEx"></param>
        private void SaveDataActivity(string Table, List<int> ListToDelete,
            DataGridView dataGridViewC, DataGridView dataGridViewP = null,
            Dictionary<int, InfoUserExternalite> MylistEx = null)
        {
            try
            {
                string idTable = "";
                switch (Table)
                {
                    case "Charges":
                        idTable = "IdCharges";
                        foreach (int index in ListToDelete)
                        {
                            string query = SQLQueryBuilder.DeleteQuery(index, "Caract_Activite", "IdCharges");
                            SQlQueryExecuter.RunQuery(query);
                        }
                        foreach (DataGridViewRow row in dataGridViewC.Rows)
                        {
                            int.TryParse(row.Cells[Charge.GetNumColonneId()].Value.ToString(), out int ID);
                            string query = "SELECT IdActivite FROM Caract_Activite WHERE " + idTable + " = '" + ID +
                                "' AND IdActivite  = '" + IdAct + "';";
                            List<string> ListField = SQlQueryExecuter.RunQueryReaderStr("IdActivite", query);
                            if (ListField.Count == 0)
                            {
                                query = "INSERT INTO Caract_Activite (IdActivite,IdCharges) " +
                                    "VALUES ( '" + IdAct + "','" + ID + "');";
                                SQlQueryExecuter.RunQuery(query);
                            }
                        }
                        break;
                    case "Produits":
                        idTable = "IdProduits";
                        foreach (int index in ListToDelete)
                        {
                            string query = SQLQueryBuilder.DeleteQuery(index, "Caract_Activite", "IdProduits");
                            SQlQueryExecuter.RunQuery(query);
                        }
                        foreach (DataGridViewRow row in dataGridViewP.Rows)
                        {
                            int.TryParse(row.Cells[Produit.GetNumColonneId()].Value.ToString(), out int ID);
                            string query = "SELECT IdActivite FROM Caract_Activite WHERE " + idTable + " = '" + ID +
                                "' AND IdActivite  = '" + IdAct + "';";
                            List<string> ListField = SQlQueryExecuter.RunQueryReaderStr("IdActivite", query);
                            if (ListField.Count == 0)
                            {
                                query = "INSERT INTO Caract_Activite (IdActivite,IdProduits) " +
                                    "VALUES ( '" + IdAct + "','" + ID + "');";
                                SQlQueryExecuter.RunQuery(query);
                            }
                        }
                        break;
                    case "Externalites":
                        idTable = "IdExternalites";
                        foreach (int index in ListToDelete)
                        {
                            string query = SQLQueryBuilder.DeleteQuery(index, "Caract_Activite", "IdExternalites");
                            SQlQueryExecuter.RunQuery(query);
                        }
                        foreach (KeyValuePair<int, InfoUserExternalite> item in MylistEx)
                        {

                            string query = "SELECT IdActivite FROM Caract_Activite WHERE " + idTable + " = '" + item.Value.ID +
                                   "' AND IdActivite  = '" + IdAct + "';";
                            List<string> ListField = SQlQueryExecuter.RunQueryReaderStr("IdActivite", query);
                            if (ListField.Count == 0)
                            {
                                query = "INSERT INTO Caract_Activite (IdActivite,IdExternalites) " +
                                    "VALUES ( '" + IdAct + "','" + item.Value.ID + "');";
                                SQlQueryExecuter.RunQuery(query);
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

        /// <summary>
        /// Save new activity
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        private int SaveNewActivite(string nom, string table)
        {
            int id = 0;
            try
            {
                string query = "INSERT INTO " + table + " (nom) VALUES ('" + nom + "') ;";
                SQlQueryExecuter.RunQueryReader(query);

                query = "SELECT ID FROM " + table + " WHERE nom = '" + nom + "' ;";
                List<string> ListField = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                if (ListField.Count > 0)
                {
                    int.TryParse(ListField[0], out id);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }

        /// <summary>
        /// retourne l'IDtype de l'activité courante
        /// </summary>
        /// <returns></returns>
        private int GetIdType()
        {
            int idtype = 0;
            try
            {
                if (comboBoxTypeAct.Text == "")
                {
                    string query1 = ActivityQuery.LoadComboBoxTypeQuery(IdAct);
                    List<string> ListField = SQlQueryExecuter.RunQueryReaderStr("Nom", query1);
                    if (ListField.Count > 0) comboBoxTypeAct.Text = ListField[0];
                }
                string type = comboBoxTypeAct.Text;
                string query = "Select ID From Type Where Nom = '" + type + "';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (!Commun.ListHasValue(list))
                {
                    idtype = list[0];
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return idtype;
        }

        /// <summary>
        /// Set the altertavie color to display in gridviews
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        private KeyValuePair<Color, Color> ManageColor(DataGridView dgv)
        {
            Color color1 = new Color();
            Color color2 = new Color();
            try
            {
                string query = "Select * From Couleur;";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ARVB1", query);
                if (Commun.ListHasValue(list))
                {
                    color1 = Color.FromArgb(list[0]);
                }
                List<int> list2 = SQlQueryExecuter.RunQueryReaderInt("ARVB2", query);
                if (Commun.ListHasValue(list2))
                {
                    color2 = Color.FromArgb(list2[0]);
                }
                if (color1 != null && color2 != null)
                {
                    Commun.Setbackground(dgv, color1, color2);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return new KeyValuePair<Color, Color>(color1, color2);
        }

        private void comboBoxTypeAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxTypeAct.Text == Translation.Translate("Add",langue)+"...")
            {
               string msg = "";
               if (Commun.InputBox(Translation.Translate("Add new Item", langue), Translation.Translate("Add new type", langue), ref msg) == DialogResult.OK)
               {
                  if(!Commun.NameExists(msg, "Type", "Nom"))
                  {
                        string query = "Insert into Type (Nom) VALUES ('" + msg + "');";
                        SQlQueryExecuter.RunQuery(query);
                  } 
                  else
                  {
                        MessageBox.Show(Translation.Translate("A Type with this name already exists." +
                        " New value was not added.", langue), "Warning");
                  }
               }
               LoadComboBox();
            }
        }

        private void Home_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Home_MouseEnter(object sender, EventArgs e)
        {
            Home.BorderStyle = BorderStyle.Fixed3D;
        }

        private void Home_MouseLeave(object sender, EventArgs e)
        {
            Home.BorderStyle = BorderStyle.None;
        }

        private void dataGridViewProduit_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                if (Produit != null)
                {
                    Produit.mCurrentid = Produit.GetId(e.RowIndex);
                }
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private void ActivityMainForm_Resize(object sender, EventArgs e)
        {
           

        }

        private void tabPageCharges_Click(object sender, EventArgs e)
        {

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadPeriode();
        }

       
    }
}
