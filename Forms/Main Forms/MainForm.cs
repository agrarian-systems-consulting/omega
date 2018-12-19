using OMEGA.Data_Classes;
using OMEGA.Forms;
using OMEGA.Forms.Result_forms_and_user_controls;
using OMEGA.Other_Classes;
using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace OMEGA
{
    internal partial class MainForm : Form
    {
        // permet de gérer la fermeture de l'application si sauvegarde ou non.
        ExitForm exitform = new ExitForm();
        MainView mainview;
        string mTypeOfGrid;
        List<int> mListIndexToDelete = new List<int>();
        int mCurrentId;
        private int langue = Properties.Settings.Default.Langue;
        private DataTable BasedataTable = new DataTable();
        internal string SQLqueryBuilder { get; private set; }

        internal MainForm(bool data)
        {
            //  MessageBox.Show("main form");
            InitializeComponent();
            mainview = new MainView(this, dataGridView1, null);
            subscribeToEvent();
            LoadView(Properties.Settings.Default.UserView);
            if (File.Exists(Properties.Settings.Default.FichierTraduction))
            {
                SetCaption(Properties.Settings.Default.Langue);
            }
            if (data)
            {
                Inititalizeview();
            }
            else
            {
                Disable_EnableControl(false);
            }
            dataGridView1.Font = Commun.GetCurrentFont();
        }

        private void Inititalizeview()
        {
            Commun.SetCurrentFont();
           // MainPanel.Font = Commun.GetCurrentFont();
            this.Activate();
            panelActivity.Visible = false;
            LoadView(Properties.Settings.Default.UserView);
            mainview.SetColor();
            this.Text = Translation.Translate("Home Page", langue) + " - " + Properties.Settings.Default.DataPath + ".omg";
        }

        private void Disable_EnableControl(bool enable)
        {
            pictureBoxActivity.Enabled = enable;
            pictureBoxExploitation.Enabled = enable;
            pictureBoxRegion.Enabled = enable;
            currentFarmresultToolStripMenuItem.Enabled = enable;
            //importDataToolStripMenuItem.Enabled = enable;
            datamanagementtoolStripMenuItem.Enabled = enable;
            gPSDataManagementToolStripMenuItem.Enabled = enable;
            //helpToolStripMenuItem.Enabled = enable;
            viewToolStripMenuItem.Enabled = enable;
            EditToolStripMenuItem.Enabled = enable;
            buttonactivity.Enabled = enable;
            buttonregion.Enabled = enable;
            buttonexploitation_agri.Enabled = enable;
            aleasToolStripMenuItem.Enabled = enable;
        }

        #region Langue 
        internal void SetCaption(int langue)
        {
            try
            {
                currentFarmresultToolStripMenuItem.Text = Translation.Translate("Results", langue);
                buttonregion.Text = Translation.Translate("Territory", langue);
                buttonProduct.Text = Translation.Translate("Products", langue);
                FiletoolStripMenuItem1.Text = Translation.Translate("File", langue);
                EditToolStripMenuItem.Text = Translation.Translate("Edit", langue);
                helpToolStripMenuItem.Text = Translation.Translate("Help", langue);
                NewToolStripMenu.Text = Translation.Translate("New", langue);
                OpenToolStripMenu.Text = Translation.Translate("Open", langue);
                saveAsToolStripMenuItem.Text = Translation.Translate("Save as", langue);
                saveToolStripMenuItem1.Text = Translation.Translate("Save", langue);
                changeLanguageToolStripMenuItem.Text = Translation.Translate("Change Language", langue);
                helpToolStripMenuItem1.Text = Translation.Translate("Help", langue);
                aboutToolStripMenuItem.Text = Translation.Translate("About", langue);
                importDataToolStripMenuItem.Text = Translation.Translate("Import data", langue);
                exportDataToolStripMenuItem.Text = Translation.Translate("Export data", langue);
                exitToolStripMenuItem1.Text = Translation.Translate("Exit", langue);
                productsToolStripMenuItem.Text = Translation.Translate("Products", langue);
                unitsToolStripMenuItem.Text = Translation.Translate("Units", langue);
                chargesToolStripMenuItem.Text = Translation.Translate("Charges", langue);
                externalitésToolStripMenuItem.Text = Translation.Translate("Externalites", langue);
                datamanagementtoolStripMenuItem.Text = Translation.Translate("Data management", langue);
                dictionnaryDefinitionToolStripMenuItem.Text = Translation.Translate("Dictionnary defintion", langue);
                phasesToolStripMenuItem.Text = Translation.Translate("Phases", langue);
                calendarsToolStripMenuItem.Text = Translation.Translate("Calendars", langue);
                buttonactivity.Text = Translation.Translate("Activity", langue);
                buttonexploitation_agri.Text = Translation.Translate("Farm", langue);
                chargesOpérationnellesToolStripMenuItem.Text = Translation.Translate("Operational costs", langue);
                chargesStructurellesToolStripMenuItem.Text = Translation.Translate("Structural costs", langue);
                uploadDataToolStripMenuItem.Text = Translation.Translate("Upload data", langue);
                buttonOkActivity.Text = Translation.Translate("Save", langue);
                buttonRemoveActivity.Text = Translation.Translate("Remove", langue);
                buttonCancelActivity.Text = Translation.Translate("Cancel", langue);
                buttonAddActivity.Text = Translation.Translate("Add", langue);
                expenseIncomeToolStripMenuItem.Text = Translation.Translate("Expenses/Incomes", langue);
                familyToolStripMenuItem.Text = Translation.Translate("Family", langue);
                wToolStripMenuItem.Text = Translation.Translate("Farm", langue);
                miscExpensesToolStripMenuItem.Text = Translation.Translate("Misc Expenses", langue);
                otherRevenueToolStripMenuItem.Text = Translation.Translate("Others revenues", langue);
                revenuesToolStripMenuItem.Text = Translation.Translate("Revenues", langue);
                expensesToolStripMenuItem.Text = Translation.Translate("Expenses", langue);
                viewToolStripMenuItem.Text = Translation.Translate("View", langue);
                olympeViewToolStripMenuItem.Text = Translation.Translate("'Olympe' view", langue);
                standardViewToolStripMenuItem.Text = Translation.Translate("'Standard' view", langue);
                //currentFarmresultToolStripMenuItem.Text = Translation.Translate("Current Farm result", langue);
                HerdsToolStripMenuItem.Text = Translation.Translate("Herds", langue);
                manageGPSPointToolStripMenuItem.Text = Translation.Translate("Manage GPS points", langue);
                importGPSPointToolStripMenuItem.Text = Translation.Translate("Import GPS points", langue);
                gPSDataManagementToolStripMenuItem.Text = Translation.Translate("GPS Data management", langue);
                mananeLineColorToolStripMenuItem.Text = Translation.Translate("Manage lines color", langue);
                toolTip1.SetToolTip(buttonregion, Translation.Translate("A Territory regoups a couple of farms and data about vilage, forest and others map elements (mountains,rivers...)", langue));
                toolTip1.SetToolTip(pictureBoxRegion, Translation.Translate("A Territory regoups a couple of farms and data about vilage, forest and others map elements (mountains,rivers...)", langue));
                toolTip1.SetToolTip(buttonactivity, Translation.Translate("An Activity may be a crop, a breeding, or others elements connected with farmer's activities", langue));
                toolTip1.SetToolTip(pictureBoxActivity, Translation.Translate("An Activity may be a crop, a breeding, or others elements connected with farmer's activities", langue));
                toolTip1.SetToolTip(buttonexploitation_agri, Translation.Translate("A farm regoups several farmers and is based on group of activities", langue));
                toolTip1.SetToolTip(pictureBoxExploitation, Translation.Translate("A farm regoups several farmers and is based on group of activities", langue));
                this.Text = Translation.Translate("Home Page", langue) + " - " + Properties.Settings.Default.DataPath + ".omg";
                aleasToolStripMenuItem.Text = Translation.Translate("Hazard", langue);
                priceToolStripMenuItem.Text = Translation.Translate("Price", langue);
                quantityToolStripMenuItem.Text = Translation.Translate("Quantity", langue);
                fontToolStripMenuItem.Text = Translation.Translate("Font", langue);
                buttonDataExplotation.Text = Translation.Translate("Data", langue);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }

        private void EnglishtoolStripTextBox1_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Langue = 0;
                SetCaption(Properties.Settings.Default.Langue);
                mainview.translateText();
                WriteXML.Write("Langue", "0");
                langue = Properties.Settings.Default.Langue;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }
        private void françaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Langue = 1;
                SetCaption(Properties.Settings.Default.Langue);
                mainview.translateText();
                WriteXML.Write("Langue", "1");
                langue = Properties.Settings.Default.Langue;
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void espagnolToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Properties.Settings.Default.Langue = 2;
                SetCaption(Properties.Settings.Default.Langue);
                mainview.translateText();
                WriteXML.Write("Langue", "2");
                langue = Properties.Settings.Default.Langue;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        #endregion

        #region Gestion Event

        private void subscribeToEvent()
        {
            buttonTVA.Click += mainview.LoadTVAForm;
            tVAToolStripMenuItem.Click += mainview.LoadTVAForm;
            unitsToolStripMenuItem.Click += mainview.LoadSystemeUnitForm;
            buttonUnite.Click += mainview.LoadSystemeUnitForm;
            buttonAutreAgri.Click += LoadExploitation;
            buttonexploitation_agri.Click += LoadExploitation;
            buttonClassification.Click += mainview.LoadClassifcationForm;
            classificationsToolStripMenuItem.Click += mainview.LoadClassifcationForm;
            buttonProduct.Click += mainview.LoadProductForm;
            productsToolStripMenuItem.Click += mainview.LoadProductForm;
            buttoncharges.Click += mainview.LoadChargeFormOpe;
            chargesOpérationnellesToolStripMenuItem.Click += mainview.LoadChargeFormOpe;
            chargesStructurellesToolStripMenuItem.Click += mainview.LoadChargeFormStruc;
            externalitésToolStripMenuItem.Click += mainview.LoadExternaliteForm;
            buttonExtern.Click += mainview.LoadExternaliteForm;
            buttonPeriode.Click += mainview.LoadPeriodeForm;
            buttonChargeStruct.Click += mainview.LoadChargeFormStruc;
            buttondepensefamille.Click += mainview.LoadExpenseFamilyForm;
            buttonRecetteFamille.Click += mainview.LoadIncomeFamilyForm;
            buttonDépenseDivers.Click += mainview.LoadExpenseEntrepriseForm;
            buttonRecetteDivers.Click += mainview.LoadIncomeEntrepriseForm;
            miscExpensesToolStripMenuItem.Click += mainview.LoadExpenseEntrepriseForm;
            otherRevenueToolStripMenuItem.Click += mainview.LoadIncomeEntrepriseForm;
            revenuesToolStripMenuItem.Click += mainview.LoadIncomeFamilyForm;
            expensesToolStripMenuItem.Click += mainview.LoadExpenseFamilyForm;
            buttonDataExplotation.Click += mainview.ShowResultForm;
            currentFarmresultToolStripMenuItem.Click += mainview.ShowResultForm;
            buttonEncoursAgri.Click += mainview.ShowResultForm;
            buttonNotes.Click += mainview.LoadNoteForm;
            calendarsToolStripMenuItem.Click += mainview.LoadPeriodeForm;
            buttonCulture.Click += buttonActiviteClick;
            buttonAnimaux.Click += buttonActiviteClick;
            buttonPérennes.Click += buttonActiviteClick;
            buttonpluriannuelle.Click += buttonActiviteClick;
            HerdsToolStripMenuItem.Click += mainview.LoadTroupeauxForm;
            buttontroupeaux.Click += mainview.buttontroupeaux_Click;
            importGPSPointToolStripMenuItem.Click += mainview.importGPSPointToolStripMenuItem_Click;
            manageGPSPointToolStripMenuItem.Click += mainview.manageGPSPointToolStripMenuItem_Click;
            mananeLineColorToolStripMenuItem.Click += mainview.mananeLineColorToolStripMenuItem_Click;
            dictionnaryDefinitionToolStripMenuItem.Click += mainview.dictionnaryDefinitionToolStripMenuItem_Click;
            pictureBoxActivity.MouseEnter += mainview.MouseEnterImageActivity;
            pictureBoxActivity.MouseLeave += mainview.MouseLeaveImageActivity;
            pictureBoxRegion.MouseEnter += mainview.MouseEnterImageRegion;
            pictureBoxRegion.MouseLeave += mainview.MouseLeaveImageRegion;
            pictureBoxExploitation.MouseEnter += mainview.MouseEnterImageExploitation;
            pictureBoxExploitation.MouseLeave += mainview.MouseLeaveImageExploitation;
            pictureBoxExploitation.Click += LoadExploitation;
            pictureBoxActivity.Click += buttonactivity_Click;
            pictureBoxRegion.Click += LoadTerritoire;
            buttonprix.Click += mainview.buttonprix_Click;
            buttonQuantités.Click += mainview.buttonQuantités_Click;
            buttonregion.Click += LoadTerritoire;
            ButtonPhases.Click += mainview.ButtonPhaseClick;
            phasesToolStripMenuItem.Click += mainview.ButtonPhaseClick;
            buttonvariable.Click += mainview.ButtonVariableClick;
            variableToolStripMenuItem.Click += mainview.ButtonVariableClick;
            this.FormClosing += FormClosing2;
            aboutToolStripMenuItem.Click += mainview.ButtonAboutClick;
            priceToolStripMenuItem.Click += mainview.buttonprix_Click;
            quantityToolStripMenuItem.Click += mainview.buttonQuantités_Click;
        }

        #endregion

        #region GUI

        /// <summary>
        /// Gestion des raccourcis clavier
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.O))
            {
                bool open = Program.Open();
                Disable_EnableControl(open);
                if (open) Inititalizeview();
                return true;
            }
            if (keyData == (Keys.Control | Keys.N))
            {
                bool newfile = Program.New(); ;
                Disable_EnableControl(newfile);
                if (newfile) Inititalizeview();
                return true;
            }
            if (keyData == (Keys.Control | Keys.S))
            {
                Program.Save(false);
                if (Text.Contains("*"))
                {
                    Text = Text.Remove(Text.Length - 1, 1);
                }
                return true;
            }
            if (keyData == (Keys.Control | Keys.Q))
            {
                this.Close();
                return true;
            }
            if (keyData == (Keys.Alt | Keys.F4))
            {
                this.Close();
                return true;
            }
            if (keyData == Keys.F1)
            {
                helpToolStripMenuItem1_Click(null, new EventArgs());
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void buttonactivity_Click(object sender, EventArgs e)
        {
            try
            {

                LoadView("hide");
                LoadDatagridview("Activite");
            }
              catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Load the data to insert in the data grid view
        /// </summary>
        /// <param name="Type"></param>
        internal void LoadDatagridview(string Type, string filter = "")
        {
            try
            {
                BasedataTable.Clear();
                BasedataTable.Columns.Clear();
                List<string> list = new List<string>();
                string mcommande = "";
                switch (Type)
                {
                    case "Activite":
                        mcommande = ActivityQuery.SelectActivityQuery(filter);
                        mTypeOfGrid = "Activite";
                        panelDataExploitation.Visible = false;
                        panelboutonExp.Visible = false;
                        Point locationPanel = new Point(5, 20);
                        panelActivity.Location = locationPanel;
                        panelActivity.Visible = true;
                        panelActivity.BackColor = Color.LightBlue;
                        MainPanel.BackColor = Color.LightBlue;
                        this.BackColor = Color.LightBlue;
                        buttonAddActivity.Location = new Point(5, 330);
                        buttonRemoveActivity.Location = new Point(100, 330);
                        buttonCancelActivity.Location = new Point(510, 330);
                        buttonOkActivity.Location = new Point(595, 330);
                        pictureBoxexport.Location = new Point(5, 370);
                        buttonRefresh.Location = new Point(40, 370);
                        Home.Location = new Point(5, 410);
                        Point point = new Point(5, 13);
                        dataGridView1.Location = point;
                        Size size2 = new Size(710, 310);
                        dataGridView1.Size = size2;
                        Size size = new Size(750, 530);
                        this.Size = size;
                        panelActivity.Dock = DockStyle.Fill;
                        break;
                    case "Exploitation":
                        list = GetClassificationList();
                        mcommande = ExploitationQuery.LoadGridExploitationQuery();
                        mTypeOfGrid = "Exploitation";
                        panelDataExploitation.Visible = true;
                        panelboutonExp.Visible = true;
                        panelActivity.BackColor = Color.FromArgb(192, 255, 192);
                        MainPanel.BackColor = Color.FromArgb(192, 255, 192);
                        Point point2 = new Point(138, 12);
                        dataGridView1.Location = point2;
                        buttonOkActivity.Location = new Point(625, 375);
                        buttonCancelActivity.Location = new Point(540, 375);
                        buttonAddActivity.Location = new Point(138, 375);
                        buttonRemoveActivity.Location = new Point(230, 375);
                        pictureBoxexport.Location = new Point(5, 420);
                        buttonRefresh.Location = new Point(40, 420);
                        Home.Location = new Point(5, 470);
                        Size size3 = new Size(585, 360);
                        dataGridView1.Size = size3;
                        Size size4 = new Size(750, 530);
                        this.Size = size4;
                        panelActivity.Dock = DockStyle.Fill;
                        break;
                }
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mcommande, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(BasedataTable);
                if (Type == "Activite") BasedataTable = TranslateType(BasedataTable);
                else
                {
                    foreach (string item in list)
                    {
                        BasedataTable.Columns.Add(item);
                    }
                    BasedataTable = SetClassification(list, BasedataTable);
                    SetEncoursRowColor();
                }
                dataGridView1.DataSource = BasedataTable;
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private DataTable TranslateType(DataTable table)
        {
            try
            {
                foreach (DataRow row in table.Rows)
                {
                    row.SetField(2, Translation.Translate(row.ItemArray[2].ToString(), langue));
                }
                return table;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return table;
            }
        }

        private void SetEncoursRowColor()
        {
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (row.Cells[0].Value.ToString() == Commun.GetIdExpl().ToString())
            //    {
            //        row.DefaultCellStyle.BackColor = Color.Green;
            //    }
            //    else row.DefaultCellStyle.BackColor = Color.White;

            //}
        }

        /// <summary>
        /// A chaque sélection de l'utilisateur sur une le GridView, on affiche soit les activités, soit
        /// l'exploitation dans le détail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id;
                int.TryParse(dataGridView1.CurrentRow.Cells[0].Value.ToString(), out id);
                if (mCurrentId == id)
                {
                    dataGridView1_CellContentDoubleClick(sender, e);
                    return;
                }
                else
                {
                    string query = "Update " + mTypeOfGrid + " Set Encours = '0' Where ID <> '" + id + "';";
                    SQlQueryExecuter.RunQuery(query);
                    query = "Update " + mTypeOfGrid + " Set Encours = '1' Where ID ='" + id + "';";
                    mCurrentId = id;
                    SQlQueryExecuter.RunQuery(query);
                    SetEncoursRowColor();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /// <summary>
        /// Retourne l'Id de la ligne envoyée en paramètre
        /// </summary>
        /// <param name="Rowindex"></param>
        /// <returns></returns>
        private int GetId(int Rowindex)
        {
            int id = -1;
            try
            {
                if (dataGridView1.Rows[Rowindex].Cells[0].Value.ToString() != null)
                {
                    int.TryParse(dataGridView1.Rows[Rowindex].Cells[0].Value.ToString(), out id);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }

        private void Home_Click(object sender, EventArgs e)
        {
            LoadView(Properties.Settings.Default.UserView);
            MainPanel.BackColor = Color.FromArgb(192, 192, 255);
        }

        private void buttonAddActivity_Click(object sender, EventArgs e)
        {
            try
            {

                string[] values = new string[BasedataTable.Columns.Count];
                BasedataTable.Rows.Add(values);

            }
            catch (Exception Ex)
            {

            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // si on est sur la colonne nom qui vient d'être ajouté
                if (dataGridView1.CurrentRow.Index == e.RowIndex)
                { // on charge le form détail pour que l'utilisateur puisse rentrer les infos de la nouvelle activité/exploitation/territoire
                    switch (mTypeOfGrid)
                    {
                        case "Activite":
                            ActivityMainForm activity = new ActivityMainForm(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                            activity.ShowDialog();
                            break;
                        case "Exploitation":
                            ExploitationMainForm exploitation = new ExploitationMainForm(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                            exploitation.ShowDialog();
                            break;
                        case "Territoire":
                            //ExploitationMainForm exploitation = new ExploitationMainForm(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), mTypeOfGrid);
                            //exploitation.ShowDialog();
                            break;
                    }
                    dataGridView1.ReadOnly = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        internal void pictureBox8_MouseEnter(object sender, EventArgs e)
        {
            if (pictureBoxexport != null)
            {
                pictureBoxexport.BorderStyle = BorderStyle.FixedSingle;
            }

        }

        internal void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            if (pictureBoxexport != null)
            {
                pictureBoxexport.BorderStyle = BorderStyle.None;
            }
        }

        private void buttonRemoveActivity_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult = MessageBox.Show(Translation.Translate("You are about to delete this data permanently, do you want to continue ?", langue), "Warning", MessageBoxButtons.YesNo);
                if (DialogResult == DialogResult.No) return;
                dataGridView1.AllowUserToDeleteRows = true;
                DataGridViewSelectedRowCollection UserSelectedRow = dataGridView1.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    mListIndexToDelete.Add(GetId(index));
                    dataGridView1.Rows.RemoveAt(index);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonOkActivity_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (int index in mListIndexToDelete)
                {
                    string query = SQLQueryBuilder.DeleteQuery(index, mTypeOfGrid);
                    SQlQueryExecuter.RunQuery(query);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void activitéToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //ReportForm report = new ReportForm();
            //report.ShowDialog();
        }

        private void olympeViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                WriteXML.Write("UserView", "Olympe");
                Properties.Settings.Default.UserView = "Olympe";
                LoadView(Properties.Settings.Default.UserView);
                MainPanel.BackColor = Color.FromArgb(192, 192, 255);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void LoadExploitation(object sender, EventArgs e)
        {
            try
            {
                LoadView("Hide");
                System.Drawing.Point locationPanel = new System.Drawing.Point(0, 20);
                panelActivity.Location = locationPanel;
                panelActivity.Visible = true;
                Size size = new Size(725, 515);
                Size = size;
                panelActivity.Size = size;
                LoadDatagridview("Exploitation");
                this.WindowState = FormWindowState.Normal;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void LoadTerritoire(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Form territoryform = new TerritoryForm();
                territoryform.ShowDialog();
                this.Show();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void LoadView(string view)
        {
            try
            {
                switch (view)
                {
                    case "Olympe":
                        panelActivity.Dock = DockStyle.None;
                        panelOlympe.Visible = true;
                        panelstandard.Visible = false;
                        panelActivity.Visible = false;
                        Point point = new Point(0, 28);
                        panelOlympe.Location = point;
                        Size size = new Size(750, 620);
                        this.Size = size;
                        panelOlympe.Size = size;
                        this.WindowState = FormWindowState.Normal;
                        break;
                    case "standard":
                        panelActivity.Dock = DockStyle.None;
                        panelOlympe.Visible = false;
                        panelstandard.Visible = true;
                        panelActivity.Visible = false;
                        Point point2 = new Point(5, 5);
                        panelstandard.Location = point2;
                        Size size2 = new Size(740, 345);
                        this.Size = size2;
                        size2 = new Size(900, 500);
                        panelstandard.Size = size2;
                        this.WindowState = FormWindowState.Normal;
                        break;
                    case "Hide":
                        panelActivity.Dock = DockStyle.None;
                        panelOlympe.Visible = false;
                        panelstandard.Visible = false;
                        this.WindowState = FormWindowState.Normal;
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void standardViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                WriteXML.Write("UserView", "standard");
                Properties.Settings.Default.UserView = "standard";
                LoadView(Properties.Settings.Default.UserView);
                MainPanel.BackColor = Color.FromArgb(192, 192, 255);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonActiviteClick(object sender, EventArgs e)
        {
            try
            {
                LoadView("Hide");
               
                if (sender.ToString().Contains(Translation.Translate("Crop", langue)))
                {
                    LoadDatagridview("Activite", "Crop");
                }

                if (sender.ToString().Contains(Translation.Translate("Animals", langue)))
                {
                    LoadDatagridview("Activite", "Animals");
                }

                if (sender.ToString().Contains(Translation.Translate("Pluriannual", langue)))
                {
                    LoadDatagridview("Activite", "Pluriannuelle");
                }

                if (sender.ToString().Contains(Translation.Translate("Perennial", langue)))
                {
                    LoadDatagridview("Activite", "Perenne");
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (dataGridView1.CurrentRow.Cells[1].Value.ToString() != "")
                {
                    dataGridView1.ReadOnly = true;
                    this.Hide();
                    int id;
                    int.TryParse(dataGridView1.CurrentRow.Cells[0].Value.ToString(), out id);
                    switch (mTypeOfGrid)
                    {
                        case "Activite":
                            ActivityMainForm activity = new ActivityMainForm(id);
                            activity.ShowDialog();
                            break;
                        case "Exploitation":
                            ExploitationMainForm exploitation = new ExploitationMainForm(id);
                            exploitation.ShowDialog();
                            break;
                        case "Territoire":
                            //ActivityMainForm activity = new ActivityMainForm(idAct);
                            //activity.ShowDialog();
                            break;

                    }
                    this.Show();
                }
                else // l'utilisateur ajoute un nom à la nouvelle activité, donc la colonne nom est éditable
                {
                    dataGridView1.ReadOnly = false;
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private List<string> GetClassificationList()
        {
            string query = SQLQueryBuilder.SelectQuery("Classifications", "Distinct Groupe");
            return SQlQueryExecuter.RunQueryReaderStr("Groupe", query);
        }

        private DataTable SetClassification(List<string> listClassif, DataTable BasedataTable)
        {
            string query;
            List<string> listValue = new List<string>();
            List<int> listid = new List<int>();
            foreach (string item in listClassif)
            {
                for (int i = 1; i <= 10; i++)
                {
                    query = "Select IdExploitation,Valeur_" + i +
                    " FROM Caract_Classifications " +
                    " WHERE Classification_" + i + " ='" + item + "';";
                    listid = SQlQueryExecuter.RunQueryReaderInt("IdExploitation", query);
                    listValue = SQlQueryExecuter.RunQueryReaderStr("Valeur_" + i, query);
                    if (Commun.ListHasValue(listid))
                    {
                        foreach (DataColumn column in BasedataTable.Columns)
                        {
                            if (column.ColumnName == item)
                            {
                                foreach (DataRow row in BasedataTable.Rows)
                                {
                                    foreach (int id in listid)
                                    {
                                        int index = listid.IndexOf(id);
                                        if (row.ItemArray[0].ToString() == id.ToString())
                                        {
                                            row.SetField<string>(column, listValue[index]);
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                    }
                }
            }

            foreach (DataRow row in BasedataTable.Rows)
            {

                int index;
                int id;
                int.TryParse(row.ItemArray[0].ToString(), out id);
                if (listid.Contains(id))
                {
                    foreach (string itemClassif in listClassif)
                    {
                        foreach (DataColumn column in BasedataTable.Columns)
                        {
                            if (column.ColumnName == itemClassif)
                            {
                                index = listClassif.IndexOf(itemClassif);
                                row.SetField<string>(column, listValue[index]);
                                break;
                            }
                        }
                    }
                }
            }
            return BasedataTable;
        }

        private void Home_MouseEnter(object sender, EventArgs e)
        {
            Home.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxHome2.BorderStyle = BorderStyle.Fixed3D;
        }

        private void Home_MouseLeave(object sender, EventArgs e)
        {
            Home.BorderStyle = BorderStyle.None;
            pictureBoxHome2.BorderStyle = BorderStyle.None;
        }

        private void panelOlympe_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(panelOlympe.ClientRectangle, Color.Blue, Color.DarkBlue, 1);

            ColorBlend cblend = new ColorBlend(3);
            Color color1 = Color.FromArgb(164, 165, 226);
            Color color2 = Color.FromArgb(164, 165, 226);
            Color color3 = Color.FromArgb(192, 192, 255);

            cblend.Colors = new Color[3] { color2, color2, color2 };
            cblend.Positions = new float[3] { 0f, 0.25f, 1f };

            linearGradientBrush.InterpolationColors = cblend;

            e.Graphics.FillRectangle(linearGradientBrush, panelOlympe.ClientRectangle);
        }

        private void buttonAnimaux_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                //MessageBox.Show(Translation.Translate("The user guide document is not finished yet, but it will be soon... ;)", langue));
                string langueStr = "";
                if (langue == 0) langueStr = "Eng";
                if (langue == 1) langueStr = "Fr";
                if (langue == 2) langueStr = "Esp";
                if (File.Exists(Application.StartupPath + "\\" +Translation.Translate("Help", langue) + "_OMEGA_" + langueStr + ".pdf"))
                {
                    System.Diagnostics.Process.Start(Application.StartupPath + "\\" + Translation.Translate("Help", langue) + "_OMEGA_" + langueStr + ".pdf");
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }


        #endregion

        #region Gestion Fichier

        private void FormClosing2(object sender, FormClosingEventArgs e)
        {
            if (Properties.Settings.Default.DoitSauvegarger)
            {
                exitform.ShowDialog();
                switch (exitform.OutPutValue)
                { // 1 : save and quit, 2 quit no save, 3 cancel
                    case 1:
                        Program.Save(false);
                        if (Text.Contains("*"))
                        {
                            Text = Text.Remove(Text.Length - 1, 1);
                        }
                        Properties.Settings.Default.DoitSauvegarger = false;
                        e.Cancel = false;
                        break;
                    case 2:
                        e.Cancel = false;
                        Properties.Settings.Default.DoitSauvegarger = false;
                        break;
                    case 3:
                        e.Cancel = true;
                        return;
                    default:
                        break;
                }
            }
            else
            {
                e.Cancel = false;
            }
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.Save(true);
            if (Text.Contains("*"))
            {
                Text = Text.Remove(Text.Length - 1, 1);
            }
        }

        private void OpenToolStripMenu_Click(object sender, EventArgs e)
        {
            try
            {

                bool open = Program.Open();
                Disable_EnableControl(open);
                if (open)
                {
                    Inititalizeview();
                    this.Text = Translation.Translate("Home Page", langue) + " - " + Properties.Settings.Default.DataPath + ".omg";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void saveToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Program.Save(false);
            if (Text.Contains("*"))
            {
                Text = Text.Remove(Text.Length - 1, 1);
            }
        }

        private void NewToolStripMenu_Click(object sender, EventArgs e)
        {
            bool newfile = Program.New(); ;
            Disable_EnableControl(newfile);
            if (newfile)
            {
                Inititalizeview();
                this.Text = Translation.Translate("Home Page", langue) + " - " + Properties.Settings.Default.DataPath + ".omg";
            }
        }

        /// <summary>
        /// Cliquer sur fermeture du logiciel, on vérifie si l'utilisateur à bien sauvegarder ses données
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.DoitSauvegarger)
            {
                exitform.ShowDialog();
                switch (exitform.OutPutValue)
                { // 1 : save and quit, 2 quit no save, 3 cancel
                    case 1:
                        Program.Save(false);
                        if (Text.Contains("*"))
                        {
                            Text = Text.Remove(Text.Length - 1, 1);
                        }
                        Properties.Settings.Default.DoitSauvegarger = false;
                        this.Close();
                        break;
                    case 2:
                        Properties.Settings.Default.DoitSauvegarger = false;
                        this.Close();
                        break;
                    case 3:
                        return;
                    default:
                        break;
                }
            }
            else this.Close();
        }

        #endregion
        
        private void buttonoutPut_Click(object sender, EventArgs e)
        {
            EtatSortieForm form = new EtatSortieForm();
            form.ShowDialog();
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                Commun.SetCurrentFont(fontDialog1.Font.Name, fontDialog1.Font.Size, fontDialog1.Font.Style.ToString());
            }
        }

        private void tVAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            if (!panelDataExploitation.Visible)
            {
                LoadDatagridview("Activite");
            }
            else
            {
                LoadDatagridview("Exploitation");
            }
        }

        private void buttonVariantesExploi_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count != 1)
                {
                    MessageBox.Show(Translation.Translate("Please select only 1 row to duplicate.",langue));
                    return;
                }
                else
                {
                    DataGridViewRow CurrentRow = dataGridView1.SelectedRows[0];
                    int.TryParse(CurrentRow.Cells[0].Value.ToString(), out int id);
                    string[] newvalue = new string[BasedataTable.Columns.Count - 1];
                    string newName = "";
                    for (int i = 0; i < CurrentRow.Cells.Count - 1; i++)
                    {
                        if (i == 1)
                        {
                            int num = CheckExploitationName(CurrentRow.Cells[i].Value.ToString());
                            newvalue[i] = CurrentRow.Cells[i].Value.ToString() + "_" + num;
                            newName = newvalue[i];
                        }
                        else
                        {
                            newvalue[i] = CurrentRow.Cells[i].Value.ToString();
                        }
                    }
                    if (CopyTable(id, newName)) BasedataTable.Rows.Add(newvalue);
                    else MessageBox.Show(Translation.Translate("Error while copying data.Please, check the log file for more information.",langue));
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private int CheckExploitationName(string nom)
        {
            int index = 0;
            foreach (DataRow row in BasedataTable.Rows)
            {
                if (row.ItemArray[1].ToString().Contains(nom))
                {
                    index++;
                }
            }
            return index;
        }

        private bool CopyTable(int id,string nom)
        {
            try
            {
                string query = "Select * From Exploitation Where ID = '" + id + "';";
                string[] reader = SQlQueryExecuter.RunQueryReader(query);

                query = "Insert into Exploitation (Nom,Encours,Principale,IdExploitationPrincipale)" +
                   " VALUES ('" + nom + "','0','" + reader[3] + "','" + reader[5] + "');";
                SQlQueryExecuter.RunQuery(query);
                int newId = SQLQueryBuilder.FindId("Exploitation", "ID", "Nom", nom);
                ExploitationQuery.CopyExploitation("Caract_Exploitation", id, newId);
                ExploitationQuery.CopyExploitation("Agri_Produits", id, newId);
                ExploitationQuery.CopyExploitation("Agri_Perenne", id, newId);
                ExploitationQuery.CopyExploitation("Agri_Pluriannuelle", id, newId);
                ExploitationQuery.CopyExploitation("Agri_Animaux", id, newId);
                ExploitationQuery.CopyExploitation("Agri_Charges", id, newId);
                ExploitationQuery.CopyExploitation("Result_Calcul", id, newId);
                ExploitationQuery.CopyExploitation("Agri_Assol", id, newId);
                ExploitationQuery.CopyExploitation("Agri_Immo", id, newId);
                ExploitationQuery.CopyExploitation("Agri_Occc", id, newId);
                ExploitationQuery.CopyExploitation("Agri_Variable", id, newId);
                ExploitationQuery.CopyExploitation("SHP_Info", id, newId);
                ExploitationQuery.CopyExploitation("Agri_DefSim", id, newId);
                ExploitationQuery.CopyExploitation("Agri_EmpCT", id, newId);
                ExploitationQuery.CopyExploitation("Agri_EmpLT", id, newId);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return false;
            }
            return true;
        }
        
        private void panelOlympe_SizeChanged(object sender, EventArgs e)
        {
            panelOlympe.Refresh();
        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            if (panelOlympe.Visible)
            {
                MainPanel.BackColor = Color.FromArgb(164, 165, 226);
            }
            if (panelstandard.Visible)
            {
                MainPanel.BackColor = Color.FromArgb(164, 165, 226);
            }
            if (mTypeOfGrid == "Activite")
            {
                MainPanel.BackColor = Color.LightBlue;
                panelActivity.BackColor = Color.LightBlue;
                BackColor = Color.LightBlue;
            }
        }

        private void pictureBoxHome2_Click(object sender, EventArgs e)
        {
            LoadView("standard");
            WriteXML.Write("UserView", "standard");
            MainPanel.BackColor = Color.FromArgb(192, 192, 255);
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            try
            {
                if (mTypeOfGrid == "Activite")
                {
                    this.BackColor = Color.LightBlue;
                    MainPanel.BackColor = Color.LightBlue;
                    panelActivity.BackColor = Color.LightBlue;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void pictureBoxexport_Click(object sender, EventArgs e)
        {
            Export.RunExportTable(dataGridView1, mTypeOfGrid);
        }

        #region Panel

        private GraphicsPath GetRoundRectagle(Rectangle b, int r)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(b.X, b.Y, r, r, 180, 90);
            path.AddArc(b.X + b.Width - r - 1, b.Y, r, r, 270, 90);
            path.AddArc(b.X + b.Width - r - 1, b.Y + b.Height - r - 1, r, r, 0, 90);
            path.AddArc(b.X, b.Y + b.Height - r - 1, r, r, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        private void panelAtelier_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Color TitleBackColor = Color.FromArgb(14, 14, 255);
            Color TitleForeColor = Color.White;
            HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
            Font TitleFont = new Font("Corbel", 11, FontStyle.Bold);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, panelAtelier.ClientRectangle, this);
            var rect = ClientRectangle;
            using (var path = GetRoundRectagle(e.ClipRectangle, 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
                Color color1 = Color.FromArgb(50, 50, 255);
                Color color3 = Color.FromArgb(141, 141, 255);
                var brush = new LinearGradientBrush(panelAtelier.ClientRectangle, color1, color1, 90);
                e.Graphics.FillPath(brush, path);
                e.Graphics.SetClip(rect);
                var clip = e.Graphics.ClipBounds;
                var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
                e.Graphics.FillPath(brush2, path);
                rect.Location = new Point(10, 0);
                rect.Width = 100;
                rect.Height = 20;
                TextRenderer.DrawText(e.Graphics, Translation.Translate("Production ", langue), TitleFont, rect, TitleForeColor);
                e.Graphics.SetClip(clip);
                Pen pen = new Pen(TitleBackColor, 1);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void panelAleas_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Color TitleBackColor = Color.FromArgb(14, 14, 255);
            Color TitleForeColor = Color.White;
            HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
            Font TitleFont = new Font("Corbel", 11, FontStyle.Bold);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, panelAleas.ClientRectangle, this);
            var rect = ClientRectangle;
            using (var path = GetRoundRectagle(e.ClipRectangle, 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
                Color color1 = Color.FromArgb(50, 50, 255);
                Color color3 = Color.FromArgb(141, 141, 255);
                var brush = new LinearGradientBrush(panelAleas.ClientRectangle, color1, color1, 90);
                e.Graphics.FillPath(brush, path);
                e.Graphics.SetClip(rect);
                var clip = e.Graphics.ClipBounds;
                var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
                e.Graphics.FillPath(brush2, path);
                rect.Location = new Point(10, 0);
                rect.Width = 100;
                rect.Height = 20;
                TextRenderer.DrawText(e.Graphics, Translation.Translate("Hazard", langue), TitleFont, rect, TitleForeColor);
                e.Graphics.SetClip(clip);
                Pen pen = new Pen(TitleBackColor, 1);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void paneldonnéebase_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Color TitleBackColor = Color.FromArgb(14, 14, 255);
            Color TitleForeColor = Color.White;
            HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
            Font TitleFont = new Font("Corbel", 10, FontStyle.Bold);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, paneldonnéebase.ClientRectangle, this);
            var rect = ClientRectangle;
            using (var path = GetRoundRectagle(e.ClipRectangle, 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
                Color color1 = Color.FromArgb(50, 50, 255);
                Color color3 = Color.FromArgb(141, 141, 255);
                var brush = new LinearGradientBrush(paneldonnéebase.ClientRectangle, color1, color1, 90);
                e.Graphics.FillPath(brush, path);
                e.Graphics.SetClip(rect);
                var clip = e.Graphics.ClipBounds;
                var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
                e.Graphics.FillPath(brush2, path);
                rect.Location = new Point(0, 0);
                rect.Width = 125;
                rect.Height = 20;
                TextRenderer.DrawText(e.Graphics, Translation.Translate("Data", langue), TitleFont, rect, TitleForeColor);
                e.Graphics.SetClip(clip);
                Pen pen = new Pen(TitleBackColor, 1);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void panelDefinition_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Color TitleBackColor = Color.FromArgb(14, 14, 255);
            Color TitleForeColor = Color.White;
            HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
            Font TitleFont = new Font("Corbel", 10, FontStyle.Bold);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, panelDefinition.ClientRectangle, this);
            var rect = ClientRectangle;
            using (var path = GetRoundRectagle(e.ClipRectangle, 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
                Color color1 = Color.FromArgb(50, 50, 255);
                Color color3 = Color.FromArgb(141, 141, 255);
                var brush = new LinearGradientBrush(panelDefinition.ClientRectangle, color1, color1, 90);
                e.Graphics.FillPath(brush, path);
                e.Graphics.SetClip(rect);
                var clip = e.Graphics.ClipBounds;
                var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
                e.Graphics.FillPath(brush2, path);
                rect.Location = new Point(0, 0);
                rect.Width = 125;
                rect.Height = 20;
                TextRenderer.DrawText(e.Graphics, Translation.Translate("Definition", langue), TitleFont, rect, TitleForeColor);
                e.Graphics.SetClip(clip);
                Pen pen = new Pen(TitleBackColor, 1);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void panelNote_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Color TitleBackColor = Color.FromArgb(14, 14, 255);
            Color TitleForeColor = Color.White;
            HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
            Font TitleFont = new Font("Corbel", 10, FontStyle.Bold);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, panelNote.ClientRectangle, this);
            var rect = ClientRectangle;
            using (var path = GetRoundRectagle(e.ClipRectangle, 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
                Color color1 = Color.FromArgb(50, 50, 255);
                Color color3 = Color.FromArgb(141, 141, 255);
                var brush = new LinearGradientBrush(panelNote.ClientRectangle, color1, color1, 90);
                e.Graphics.FillPath(brush, path);
                e.Graphics.SetClip(rect);
                var clip = e.Graphics.ClipBounds;
                var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
                e.Graphics.FillPath(brush2, path);
                rect.Location = new Point(0, 0);
                rect.Width = 125;
                rect.Height = 20;
                TextRenderer.DrawText(e.Graphics, Translation.Translate("Note", langue), TitleFont, rect, TitleForeColor);
                e.Graphics.SetClip(clip);
                Pen pen = new Pen(TitleBackColor, 1);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void panelexploitation_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Color TitleBackColor = Color.FromArgb(14, 14, 255);
            Color TitleForeColor = Color.White;
            HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
            Font TitleFont = new Font("Corbel", 10, FontStyle.Bold);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, panelexploitation.ClientRectangle, this);
            var rect = ClientRectangle;
            using (var path = GetRoundRectagle(e.ClipRectangle, 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
                Color color1 = Color.FromArgb(50, 50, 255);
                Color color3 = Color.FromArgb(141, 141, 255);
                var brush = new LinearGradientBrush(panelexploitation.ClientRectangle, color1, color1, 90);
                e.Graphics.FillPath(brush, path);
                e.Graphics.SetClip(rect);
                var clip = e.Graphics.ClipBounds;
                var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
                e.Graphics.FillPath(brush2, path);
                rect.Location = new Point(0, 0);
                rect.Width = 125;
                rect.Height = 20;
                TextRenderer.DrawText(e.Graphics, Translation.Translate("Farm ", langue), TitleFont, rect, TitleForeColor);
                e.Graphics.SetClip(clip);
                Pen pen = new Pen(TitleBackColor, 1);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Color TitleBackColor = Color.FromArgb(14, 14, 255);
            Color TitleForeColor = Color.White;
            Font TitleFont = new Font("Corbel", 10, FontStyle.Bold);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, panelClassif.ClientRectangle, this);
            var rect = ClientRectangle;
            using (var path = GetRoundRectagle(e.ClipRectangle, 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
                Color color1 = Color.FromArgb(50, 50, 255);
                Color color3 = Color.FromArgb(141, 141, 255);
                var brush = new LinearGradientBrush(panelClassif.ClientRectangle, color1, color1, 90);
                e.Graphics.FillPath(brush, path);
            }
        }

        private void panelOutput_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Color TitleBackColor = Color.FromArgb(14, 14, 255);
            Color TitleForeColor = Color.White;
            HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
            Font TitleFont = new Font("Corbel", 10, FontStyle.Bold);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, ClientRectangle, this);
            var rect = ClientRectangle;
            using (var path = GetRoundRectagle(e.ClipRectangle, 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
                Color color1 = Color.FromArgb(50, 50, 255);
                Color color3 = Color.FromArgb(141, 141, 255);
                var brush = new LinearGradientBrush(ClientRectangle, color1, color1, 90);
                e.Graphics.FillPath(brush, path);
                e.Graphics.SetClip(rect);
                var clip = e.Graphics.ClipBounds;
                var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
                e.Graphics.FillPath(brush2, path);
                rect.Location = new Point(0, 0);
                rect.Width = 125;
                rect.Height = 20;
                TextRenderer.DrawText(e.Graphics, Translation.Translate("Output", langue), TitleFont, rect, TitleForeColor);
                e.Graphics.SetClip(clip);
                Pen pen = new Pen(TitleBackColor, 1);
                e.Graphics.DrawPath(pen, path);
            }
        }

        private void panelPourAtelier_Paint(object sender, PaintEventArgs e)
        {
            base.OnPaint(e);
            Color TitleForeColor = Color.White;
            Font TitleFont = new Font("Corbel", 9);
            var rect = ClientRectangle;

            using (var path = GetRoundRectagle(e.ClipRectangle, 10))
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
                Color color1 = Color.FromArgb(50, 50, 255);
                var brush = new LinearGradientBrush(ClientRectangle, color1, color1, 90);
                e.Graphics.FillPath(brush, path);
                e.Graphics.SetClip(rect);
            }
            rect = new Rectangle(0, 0, 120, 13);
            e.Graphics.SetClip(rect);
            var clip = e.Graphics.ClipBounds;
            rect.Location = new Point(-15, -3);
            TextRenderer.DrawText(e.Graphics, Translation.Translate("Production ", langue), TitleFont, rect, TitleForeColor);
            e.Graphics.SetClip(clip);
        }

        private void panelstandard_Paint(object sender, PaintEventArgs e)
        {

            panelstandard.Location = new Point(0, 25);
            panelstandard.Size = new Size(900, 500);

            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(ClientRectangle, Color.Blue, Color.DarkBlue, 1);

            ColorBlend cblend = new ColorBlend(3);
            Color color2 = Color.FromArgb(164, 165, 226);

            cblend.Colors = new Color[3] { color2, color2, color2 };
            cblend.Positions = new float[3] { 0f, 0.25f, 1f };

            linearGradientBrush.InterpolationColors = cblend;

            e.Graphics.FillRectangle(linearGradientBrush, ClientRectangle);
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                NotesForm form = new NotesForm(mTypeOfGrid);
                form.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }

}

