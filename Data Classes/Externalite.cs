using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using System.IO;
using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System.Drawing;

namespace OMEGA.Data_Classes
{
    /// <summary>
    /// Cette classe représente les externalités 
    /// </summary>
    internal class Externalite
    {
        
        #region variables Globales
        private SQLiteConnection mySQLiteConnection;
        private DataTable mExternaliteTable = new DataTable("Externalites");
        private string mainQuery;
        internal Boolean mDataSaved = false;
        private int mKeyIndex;
        private PictureBox PictureBox;
        private int mCurrentid;
        private Button mbuttonOk;
        private Color color1;
        private Color color2;
        private Color color3;
        private Form ExternaliteForm;
        private bool showProposals = true;
        private Button mbuttonProposition;
        private Button mbuttongroup;
        private Subro.Controls.DataGridViewGrouper grouper = new Subro.Controls.DataGridViewGrouper();
        private Button mbuttonDuplicate;
        private Button mbuttonRemove;
        private Button mbuttonSave;
        private Button mbuttonCancel;
        private int mIdAct;
        internal List<int> ListeRecordToDelete { get; set; }
        private int langue = Properties.Settings.Default.Langue;
        internal DataGridView DataGridView { get; set; }
        internal Dictionary<int, InfoUserExternalite> DicoInfoUserExternalite { get; set; }
        #endregion

        internal Externalite(DataGridView mygrid, Button buttonok, Button buttonsave,
        Button buttoncancel, Button buttonRemove, Button buttonDuplicate, Button buttonPropo,
        Button bttngroup, List<int> listId = null, int idAct = 0, PictureBox ptcbox = null, 
        Form form = null)
        {
            try
            {
                mbuttonProposition = buttonPropo;
                mbuttongroup = bttngroup;
                mbuttonOk = buttonok;
                mbuttonDuplicate = buttonDuplicate;
                mbuttonRemove = buttonRemove;
                mbuttonSave = buttonsave;
                mbuttonCancel = buttoncancel;
                DataGridView = mygrid;
                PictureBox = ptcbox;
                mySQLiteConnection = new SQLiteConnection(Properties.Settings.Default.ConnectionString);
                mainQuery = ExternaliteQuery.MainQuery(listId);
                ListeRecordToDelete = new List<int>();
                mIdAct = idAct;
                ExternaliteForm = form;
            if (File.Exists(Properties.Settings.Default.FichierTraduction))
            {
                SetCaption();
            }
                LoadGridViewData();
                RenameColumnHeader();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Log.WriteLog(e.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Translate the text
        /// </summary>
        private void SetCaption()
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;
                if (mbuttonOk != null) mbuttonOk.Text = Translation.Translate("OK", langue);
                mbuttonRemove.Text = Translation.Translate("Remove", langue);
                mbuttonCancel.Text = Translation.Translate("Cancel", langue);
                mbuttonDuplicate.Text = Translation.Translate("Duplicate", langue);
                mbuttonSave.Text = Translation.Translate("Save", langue);
                mbuttongroup.Text = Translation.Translate("Enable group", langue);
                mbuttonProposition.Text = Translation.Translate("Disable autofill proposal", langue);
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Load the data on the grid view
        /// </summary>
        private void LoadGridViewData()
        {
            try
            {

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mExternaliteTable);
                DataGridView.DataSource = mExternaliteTable;
                DataGridView.Columns[6].Visible = false;
                DataGridView.Columns[7].Visible = false;
                DataGridView.Columns[5].Visible = false;
                DataGridView.Columns[0].Resizable = DataGridViewTriState.False;
                CreateDico();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Log.WriteLog(e.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Create the dico that contains the user value
        /// </summary>
        private void CreateDico()
        {
            try
            { 
                int key = 0;
                DicoInfoUserExternalite = new Dictionary<int, InfoUserExternalite>();
                foreach (DataRow row in mExternaliteTable.Rows)
                {
                    InfoUserExternalite InfoUserExternalite = new InfoUserExternalite();
                    int.TryParse(row.ItemArray[7].ToString(), out key);
                    DicoInfoUserExternalite.Add(key, InfoUserExternalite);
                
                }
                mKeyIndex = Commun.GetSeqId("Externalites") + 1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Rename the column header
        /// </summary>
        private void RenameColumnHeader()
        {
            try
            { 
                int langue = Properties.Settings.Default.Langue;
                DataGridView.Columns[0].HeaderText = Translation.Translate("Type", langue);
                DataGridView.Columns[1].HeaderText = Translation.Translate("Name", langue);
                DataGridView.Columns[2].HeaderText = Translation.Translate("Unit activity", langue);
                DataGridView.Columns[3].HeaderText = Translation.Translate("Unit farm", langue);
                DataGridView.Columns[4].HeaderText = Translation.Translate("Unit global", langue);
                DataGridView.ColumnHeadersHeight = 40;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Save the data on the table
        /// </summary>
        /// <param name="DeletefFromTable"></param>
        private void SaveData(bool DeletefFromTable)
        {
            if (mDataSaved == false)
            {
                try
                {
                    //SQLiteCommand UpdateCommande = new SQLiteCommand();
                      
                    string query;
                    string[] reader;
                    // DeleteGroupe();
                    if (DeletefFromTable)
                    {
                        foreach (int index in ListeRecordToDelete)
                        {
                        query = SQLQueryBuilder.DeleteQuery(index, "Externalites");
                        SQlQueryExecuter.RunQuery(query);
                        }
                    }


                    foreach (KeyValuePair<int, InfoUserExternalite> item in DicoInfoUserExternalite)
                    {
                        // si l'utilisateur créer un nouveau externality, on l'ajoute dans la table
                        if (item.Value.AddToExternalityTable)
                        {

                            if (!item.Value.GroupeExiste)
                            {
                                query = SQLQueryBuilder.AddNewgGroupeQuery(item.Value._Type, 3);
                                SQlQueryExecuter.RunQuery(query);

                                query = SQLQueryBuilder.SelectGroupeQuery(item.Value._Type);
                                reader = SQlQueryExecuter.RunQueryReader(query);
                                if (reader.Length > 0)
                                {
                                    if (reader[0] != null)
                                    {
                                        int id;
                                        int.TryParse(reader[0], out id);
                                        item.Value.IdDefCateg = id;
                                    }
                                }
                            }

                            // l'utilisateur ajout un produit dans un groupe
                            if (!item.Value.Modification)
                            {
                                query = ExternaliteQuery.AddItemInGroupQuery(item);
                                SQlQueryExecuter.RunQuery(query);
                                item.Value.ID = FindIdExternalite(item.Key);
                            }
                            else
                            {
                                if (item.Value.ID == 0 && item.Value.Nom != "")
                                {
                                    item.Value.ID = SQLQueryBuilder.FindId("Externalite", "ID", "Nom", item.Value.Nom);
                                }
                                query = ExternaliteQuery.UpdateValueQuery(item);
                                SQlQueryExecuter.RunQuery(query);
                            }
                            item.Value.AddToExternalityTable = false;
                        }
                        ManageColor();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                }
            }
        }

        /// <summary>
        /// Occurs when the user clicks on add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonAdd(object sender, EventArgs e)
        {
            try
            {
                // we add a new row in the table with a new ID
                string[] rowvalue = new string[DataGridView.ColumnCount];
                for (int i = 0; i <= DataGridView.ColumnCount - 1; i++)
                {
                    if (i == 7) rowvalue[i] = (GetLastIndex() + 1).ToString();
                    else rowvalue[i] = null;
                }
                mExternaliteTable.Rows.Add(rowvalue);
                // the ID of the dico might be the same than in the table or not
                // it's not a big deal if not as long as we insert the new record,
                // the final ID on the table does not matter.
                DicoInfoUserExternalite.Add(GetLastIndex()+1, new InfoUserExternalite());
                mCurrentid = GetLastIndex();
                DataGridView.CurrentCell = DataGridView.Rows[DataGridView.Rows.Count - 1].Cells[0];
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user clicks on save
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonSaveData(object sender, SaveDataEvent e)
        {
            try
            {
                FillInfoUser();
                SaveData(e.deleteOnTable);
                if (e.table != "") ResetMainQuery(e.table);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                mExternaliteTable.Clear();
                adapter.Fill(mExternaliteTable);
                DataGridView.DataSource = mExternaliteTable;
                DataGridView.Refresh();
                mDataSaved = true;
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the remove the selected row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonRemoveRow(object sender, EventArgs e)
        {
            try
            {
                DataGridView.AllowUserToDeleteRows = true;
                DataGridViewSelectedRowCollection UserSelectedRow = DataGridView.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    ListeRecordToDelete.Add(GetId(index));
                    DataGridView.Rows.RemoveAt(index);
                }

                mDataSaved = false;
                //if (!ExternaliteForm.Text.Contains("*"))
                //{
                //    ExternaliteForm.Text = ExternaliteForm.Text + "*";
                //}
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        /// <summary>
        /// Duplicate the selected value by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonDuplicate(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection ListuserRow = DataGridView.SelectedRows;
                string[] rowvalue = new string[8];
                if (ListuserRow.Count == 1)
                {
                    for (int j = 0; j <= ListuserRow.Count - 1; j++)
                    {
                        rowvalue = new string[DataGridView.ColumnCount];
                        for (int i = 0; i <= DataGridView.ColumnCount - 1; i++)
                        {
                            rowvalue[i] = ListuserRow[j].Cells[i].Value.ToString();
                        }
                    }
                    int id = Commun.GetMaxId("ID", "Externalites") + 1;
                    while (DicoInfoUserExternalite.ContainsKey(id))
                    {
                        id++;
                    }
                    DicoInfoUserExternalite.Add(id, new InfoUserExternalite());
                    rowvalue[7] = id.ToString();
                    mExternaliteTable.Rows.Add(rowvalue);

                    DicoInfoUserExternalite[id]._Type = rowvalue[0];
                    DicoInfoUserExternalite[id].Nom = rowvalue[1];
                    DicoInfoUserExternalite[id].U_ate = rowvalue[2];
                    DicoInfoUserExternalite[id].U_Ent = rowvalue[3];
                    DicoInfoUserExternalite[id].U_Glo = rowvalue[4];
                    int tempint;
                    int.TryParse(rowvalue[5], out tempint);
                    DicoInfoUserExternalite[id].IdDefCateg = tempint;
                    int.TryParse(rowvalue[6], out tempint);
                    DicoInfoUserExternalite[id].IdsystUnit = tempint;
                    int.TryParse(rowvalue[7], out tempint);
                    DicoInfoUserExternalite[id].ID = tempint;
                    DicoInfoUserExternalite[id].Modification = false;
                    DicoInfoUserExternalite[id].AddToExternalityTable = true;
                    mDataSaved = false;
                    mDataSaved = false;
                    if (!ExternaliteForm.Text.Contains("*"))
                    {
                        ExternaliteForm.Text = ExternaliteForm.Text + "*";
                    }
                }
                else
                {
                    MessageBox.Show(Translation.Translate("Error,Plase reselect the row to duplicate",langue));
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        /// <summary>
        /// Occurs when the user clicks on the button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void buttonCancel(object sender, EventArgs e)
        {
            try
            { 
                DialogResult dialogResult = new DialogResult();
                if (mDataSaved == false)
                {
                    dialogResult = MessageBox.Show(Translation.Translate("Data has been modified but not saved, do you really want to exit ?",langue), "Warning", MessageBoxButtons.YesNo);
                }
                if (dialogResult == DialogResult.No)
                {
                    SaveData(true);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }
    
        /// <summary>
        /// Occurs when the user save the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void buttonValidateData(object sender, EventArgs e)
        {
            try
            {
                // id = clé du dico aussi => cela permet de trouver celui que l'on modifie
                if (mCurrentid <= 0)
                {
                    InfoUserExternalite infoUser = new InfoUserExternalite();
                    DicoInfoUserExternalite.Add(mKeyIndex + 1, infoUser);
                    mKeyIndex = mKeyIndex + 1;
                    mCurrentid = mKeyIndex;
                }
                if (DicoInfoUserExternalite[mCurrentid] != null)
                {
                    //this.mbuttonOk.Enabled = true;
                    //this.mbuttonSave.Enabled = true;
                    
                    mDataSaved = true;

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Display the list of proposition to display 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonPropositionClick(object sender, EventArgs e)
        {
            try
            { 
                int langue = Properties.Settings.Default.Langue;
                if (mbuttonProposition.Text.Contains(Translation.Translate("Enable", langue)))
                {
                    showProposals = true;
                    mbuttonProposition.Text = Translation.Translate("Disable autofill proposal", langue);
                }
                else if (mbuttonProposition.Text.Contains(Translation.Translate("Disable", langue)))
                {
                    showProposals = false;
                    mbuttonProposition.Text = Translation.Translate("Enable autofill proposal", langue);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user clicks on a cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex <= -1) return;
                FillAutolistAfterClick(e, e.ColumnIndex);
                mCurrentid = GetId(e.RowIndex);
                SetDicoValue(e.RowIndex, false);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user finishes to edit a cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridView1CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                SetDicoValue(e.RowIndex, true);
                if (e.ColumnIndex == 0)
                {
                    DataGridView.FirstDisplayedScrollingRowIndex = DataGridView.Rows[e.RowIndex].Index;
                    SetFocusToNewRow();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Fill the list to help the user to select an item that already exist
        /// </summary>
        /// <param name="e"></param>
        /// <param name="column"></param>
        private void FillAutolistAfterClick(DataGridViewCellEventArgs e, int column)
        {
            try
            { 
                double decalage = 1;
                List<string> list = new List<string>();
                string query = "";
                ContextMenu contextmenu = new ContextMenu();
                List<MenuItem> ListItem = new List<MenuItem>();
                switch (column)
                {
                    case 0: //group
                        decalage = 1.1;
                        query = "Select Nom From Def_Categ Where IdDefinitions = '3'";
                        list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        break;
                    case 2: //unit activite
                        decalage = 1.1;
                        query = "Select UAte From SystemeUnite;";
                        list = SQlQueryExecuter.RunQueryReaderStr("UAte", query);
                        break;
                    case 3: // unit exploi
                        decalage = 1.1;
                        query = "Select UEnt From SystemeUnite;";
                        list = SQlQueryExecuter.RunQueryReaderStr("UEnt", query);
                        break;
                    case 4: // unit global
                        decalage = 1.1;
                        query = "Select UGlobal From SystemeUnite;";
                        list = SQlQueryExecuter.RunQueryReaderStr("UGlobal", query);
                        break;
                }
                foreach (string item in list)
                {
                    MenuItem menuitem = new MenuItem(item);
                    ListItem.Add(menuitem);
                }
                foreach (MenuItem item in ListItem)
                {
                    contextmenu.MenuItems.Add(item);
                }

                DataGridView.ContextMenu = contextmenu;
                Rectangle MyCell = DataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                int x = (int)Math.Ceiling(MyCell.X * decalage);
                int y = (int)Math.Ceiling(MyCell.Y * decalage);
                Point point = new Point(x, y);
                contextmenu.Show(DataGridView, point);

                foreach (MenuItem item in ListItem)
                {
                    item.Click += Item_Click;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user click on an item of the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Click(object sender, EventArgs e)
        {
            try
            {
                string[] array = sender.ToString().Split(new string[] { "Text:" }, StringSplitOptions.None);
                DataGridView.CurrentCell.Value = array[1].Trim();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        /// <summary>
        /// Help the user if he/she wants to add a group that already exists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridView1EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {

                TextBox autoText = e.Control as TextBox;
                autoText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                autoText.AutoCompleteCustomSource = DataCollection;
                autoText.AcceptsReturn = false;
                autoText.AcceptsTab = false;
                if (autoText != null)
                {
                    switch (DataGridView.CurrentCell.ColumnIndex)
                    {
                        case 0: // groupe Externality
                            addItems(DataCollection, 0);
                            break;
                        case 2: // unité Atelier
                            addItems(DataCollection, 2);
                            break;
                        case 3: // unité Entreprise
                            addItems(DataCollection, 3);
                            break;
                        case 4: // unité Global
                            addItems(DataCollection, 4);
                            break;
                        default:
                            autoText.AutoCompleteMode = AutoCompleteMode.None;
                            return;
                    }
                }
                SetFocusToNewRow();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Add item to the autocompletion list
        /// </summary>
        /// <param name="col"></param>
        /// <param name="type"></param>
        internal void addItems(AutoCompleteStringCollection col, int type)
        {
            try
            { 
                string query = ""; ;
                List<string> list = new List<string>();
                switch (type)
                {
                    case 0: // groupe produit
                        query = ExternaliteQuery.AutoCompleteQuery();
                        list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        break;
                    case 2: // Unity Atelier
                        query = SQLQueryBuilder.AutoCompleteQueryUnity("UAte");
                        list = SQlQueryExecuter.RunQueryReaderStr("UAte", query);
                        break;
                    case 3: // Unity Atelier
                        query = SQLQueryBuilder.AutoCompleteQueryUnity("UEnt");
                        list = SQlQueryExecuter.RunQueryReaderStr("UEnt", query);
                        break;
                    case 4: // Unity Atelier
                        query = SQLQueryBuilder.AutoCompleteQueryUnity("UGlobal");
                        list = SQlQueryExecuter.RunQueryReaderStr("UGlobal", query);
                        break;
                }

                foreach (string item in list)
                {
                    col.Add(item);
                }
                mDataSaved = false;
                if (!ExternaliteForm.Text.Contains("*"))
                {
                    ExternaliteForm.Text = ExternaliteForm.Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Load the combobox 
        /// </summary>
        /// <param name="myCombobox"></param>
        /// <param name="fieldName"></param>
        internal void LoadComboBoxData(ComboBox myCombobox, string fieldName)
        {
            try
            {
                    DataTable SystemUnit = new DataTable();
                    string SQLQuery = "SELECT " + fieldName + " FROM SystemeUnite;";
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(SQLQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                    adapter.Fill(SystemUnit);
                    List<string> mylist = new List<string>();
                    foreach (DataRow row in SystemUnit.Rows)
                    {
                        mylist.Add(row.Field<string>(fieldName));
                    }
                    myCombobox.DataSource = mylist;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Export the data on a .scv file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ExportTable(object sender, EventArgs e)
        {
            Export.RunExportTable(mExternaliteTable);
        }

        /// <summary>
        /// On ajoute l'externalité sélectionnée dans l'ensemble en question
        /// </summary>
        /// <param name="ID"></param>
        internal void AddSelectedExternalite(int ID)
        {
            try
            { 
                string query = ExternaliteQuery.AddSelectedExtQuery(ID);
                string [] rowvalue = SQlQueryExecuter.RunQueryReader(query);
                mExternaliteTable.Rows.Add(rowvalue);

                if (DicoInfoUserExternalite.ContainsKey(ID) == false)
                { DicoInfoUserExternalite.Add(ID, new InfoUserExternalite()); }
                DicoInfoUserExternalite[ID]._Type = rowvalue[0];
                DicoInfoUserExternalite[ID].Nom = rowvalue[1];
                DicoInfoUserExternalite[ID].U_ate = rowvalue[2];
                DicoInfoUserExternalite[ID].U_Ent = rowvalue[3];
                DicoInfoUserExternalite[ID].U_Glo = rowvalue[4];
                int tempint;
                int.TryParse(rowvalue[5], out tempint);
                DicoInfoUserExternalite[ID].IdDefCateg = tempint;
                int.TryParse(rowvalue[6], out tempint);
                DicoInfoUserExternalite[ID].IdsystUnit = tempint;
                int.TryParse(rowvalue[7], out tempint);
                DicoInfoUserExternalite[ID].ID = tempint;
                DicoInfoUserExternalite[ID].Modification = false;
                DicoInfoUserExternalite[ID].AddToExternalityTable = false;
                mDataSaved = false;
                if (!ExternaliteForm.Text.Contains("*"))
                {
                    ExternaliteForm.Text = ExternaliteForm.Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Set the dico with the data on the datagridview
        /// </summary>
        /// <param name="rowindex"></param>
        /// <param name="add"></param>
        private void SetDicoValue(int rowindex, bool add)
        {
            try
            { 
                // 0 = groupe, 1 = nom, 2 = UATE, 3 = UENT , 4 = UGLo ,
                //, 5= idDefCateg 1 , 6 = idSysunit  , 7 = Idexternalite
                int tempint;
                if (!DicoInfoUserExternalite.ContainsKey(mCurrentid))
                { DicoInfoUserExternalite.Add(mCurrentid, new InfoUserExternalite()); }
                if (DicoInfoUserExternalite[mCurrentid].OldValueInCell == null)
                {
                    DicoInfoUserExternalite[mCurrentid].OldValueInCell = new string[8];
                    for (int i = 0; i <= 7; i++)
                    {
                        DicoInfoUserExternalite[mCurrentid].OldValueInCell[i] = DataGridView.Rows[rowindex].Cells[i].Value.ToString();
                    }
                }
                DicoInfoUserExternalite[mCurrentid]._Type = DataGridView.Rows[rowindex].Cells[0].Value.ToString();
                DicoInfoUserExternalite[mCurrentid].Nom = DataGridView.Rows[rowindex].Cells[1].Value.ToString();
                DicoInfoUserExternalite[mCurrentid].U_ate = DataGridView.Rows[rowindex].Cells[2].Value.ToString();
                DicoInfoUserExternalite[mCurrentid].U_Ent = DataGridView.Rows[rowindex].Cells[3].Value.ToString();
                DicoInfoUserExternalite[mCurrentid].U_Glo = DataGridView.Rows[rowindex].Cells[4].Value.ToString();
                int.TryParse(DataGridView.Rows[rowindex].Cells[5].Value.ToString(), out tempint);
                DicoInfoUserExternalite[mCurrentid].IdDefCateg = tempint;
                int.TryParse(DataGridView.Rows[rowindex].Cells[6].Value.ToString(), out tempint);
                DicoInfoUserExternalite[mCurrentid].IdsystUnit = tempint;
                int.TryParse(DataGridView.Rows[rowindex].Cells[7].Value.ToString(), out tempint);
                DicoInfoUserExternalite[mCurrentid].ID = tempint;
                DicoInfoUserExternalite[mCurrentid].AddToExternalityTable = add;
            }
            catch (Exception Ex)
            {
                    MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// We fill the information the user created in the dictionnary
        /// </summary>
        /// <param name="key"></param>
        private void FillInfoUser()
        {
            try
            {
                foreach (int key in DicoInfoUserExternalite.Keys)
                { 
                // if the unity Ate is not set, then none of this ID has been modified
                // so we dont have any info to add/insert 
                    if (DicoInfoUserExternalite[key]._Type != null)
                    {
                        string[] reader;
                        // on cherche l'id system unit,
                        string query = SQLQueryBuilder.GetIdSystemeUniteQuery(DicoInfoUserExternalite[key].U_ate, DicoInfoUserExternalite[key].U_Ent, DicoInfoUserExternalite[key].U_Glo);
                        reader = SQlQueryExecuter.RunQueryReader(query);

                        if (reader.Length > 0)
                        {
                            if (reader[0] != null)
                            {
                                int id = -1;
                                int.TryParse(reader[0], out id);
                                DicoInfoUserExternalite[key].IdsystUnit = id;
                            }
                        }

                    //creation of new Externality = added in the table
                    DicoInfoUserExternalite[key].AddToExternalityTable = true;

                    //  looking for ID groupe
                    DicoInfoUserExternalite[key].IdDefCateg = SQLQueryBuilder.FindId("Def_Categ", "IdDefCateg", "Nom", DicoInfoUserExternalite[key]._Type);


                    // if the groupe exist we dont need to create it
                    query = SQLQueryBuilder.SelectGroupeQuery(DicoInfoUserExternalite[key]._Type);
                    if (Commun.ListHasValue(SQlQueryExecuter.RunQueryReaderStr("IdDefCateg", query)))
                        DicoInfoUserExternalite[key].GroupeExiste = true;
                    else DicoInfoUserExternalite[key].GroupeExiste = false;

                        //check if it's a modification
                        if (key > Commun.GetSeqId("Externalites"))
                        {
                            DicoInfoUserExternalite[key].Modification = false;
                        }
                        else if (DicoInfoUserExternalite[key].OldValueInCell != null)
                        {
                            // looking for the old name, if it has a value then it has been modified
                            if (DicoInfoUserExternalite[key].OldValueInCell[1] == "")
                            {
                                    DicoInfoUserExternalite[key].Modification = false;
                                    DicoInfoUserExternalite[key].AddToExternalityTable  = true;
                            }
                            else
                            {
                                    DicoInfoUserExternalite[key].Modification = true;
                                    DicoInfoUserExternalite[key].ID = key;
                            }
                        }
                        else
                        {
                            DicoInfoUserExternalite[key].Modification = false;
                            DicoInfoUserExternalite[key].AddToExternalityTable = true;
                        }
                    }
                    
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Return the Externality ID
        /// </summary>
        /// <returns></returns>
        private int FindIdExternalite(int key)
        {

            int id = 0;
            try
            {
                id = SQLQueryBuilder.FindId("Externalites", "ID", "Nom", DicoInfoUserExternalite[key].Nom);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }

        /// <summary>
        /// Refresh the view to update with the new data
        /// </summary>
        /// <param name="table"></param>
        internal void RefreshView(string table)
        {
            try
            {
                ResetMainQuery(table);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                mExternaliteTable.Clear();
                adapter.Fill(mExternaliteTable);
                DataGridView.DataSource = mExternaliteTable;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Reset the query to add the modification the user made
        /// </summary>
        /// <param name="table"></param>
        private void ResetMainQuery(string table)
        {
            try
            {
                List<int> listId = SQLQueryBuilder.GetListID(table,"Externalites",mIdAct);
                mainQuery = ExternaliteQuery.MainQuery(listId);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

            
        }

        internal void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            if (PictureBox != null)
            {
                PictureBox.BorderStyle = BorderStyle.FixedSingle;
            }   
        }

        internal void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            if (PictureBox != null)
            {
                PictureBox.BorderStyle = BorderStyle.None;
            }
        }

        /// <summary>
        /// Return the id of the rowIndex
        /// </summary>
        /// <param name="Rowindex"></param>
        /// <returns></returns>
        private int GetId(int Rowindex)
            {
                int id = -1;
                try
                {
                if (Rowindex <= -1) return 0;
                    if (DataGridView.Rows[Rowindex].Cells[7].Value.ToString() != null)
                    {
                        int.TryParse(DataGridView.Rows[Rowindex].Cells[7].Value.ToString(), out id);
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                }
            return id;
        }

        /// <summary>
        /// return the last index in the table
        /// </summary>
        /// <returns></returns>
        private int GetLastIndex()
        {
            try
            { 
                if (mExternaliteTable.Rows.Count > 0)
                {
                    int id;
                    int.TryParse(mExternaliteTable.Rows[mExternaliteTable.Rows.Count - 1].ItemArray[7].ToString(), out id);
                    return id;
                }
                else return 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return 0;
            }
        }

        private void SetFocusToNewRow()
        {
            try
            {
                if (DataGridView.CurrentCell.ColumnIndex == 0)
                {
                    for (int i = 0; i <= DataGridView.Rows.Count - 1; i++)
                    {
                        if (DataGridView.Rows[i].Cells[7].Value.ToString() == mCurrentid.ToString())
                        {
                            DataGridView.CurrentCell = DataGridView.Rows[i].Cells[0];
                            break;
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

        /// <summary>
        /// Get the arvb name of the color in the datatable
        /// </summary>
        internal void SetColor()
        {
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
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Set the color on the datagridview's row
        /// </summary>
        private void ManageColor()
        {
            if (color1 != null && color2 != null)
            {
                Commun.Setbackground(DataGridView, color1, color2);
                string query = SQLQueryBuilder.UpdateColorQuery(color1, color2,color3);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
            }
        }

        /// <summary>
        /// Group the data on the datagridview
        /// </summary>
        internal void EnableGroupe(object sender, EventArgs e)
        {
            try
            { 
                if (mbuttongroup.Text.Contains("Enable"))
                {
                    grouper.DataGridView = DataGridView;
                    grouper.SetGroupOn(DataGridView.Columns[0]);
                    grouper.DisplayGroup += grouper_DisplayGroup;
                    grouper.CollapseAll();
                    mbuttongroup.Text = "Disable Groupe";
                    return;
                }
                if (mbuttongroup.Text.Contains("Disable"))
                {
                    grouper.DataGridView = DataGridView;
                    grouper.RemoveGrouping();
                    grouper.DisplayGroup -= grouper_DisplayGroup;
                    mbuttongroup.Text = "Enable Groupe";
                    ManageColor();
                    return;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void grouper_DisplayGroup(object sender, Subro.Controls.GroupDisplayEventArgs e)
        {
            e.BackColor = (e.Group.GroupIndex % 2) == 0 ? color1 : color2;
        }

        /// <summary>
        /// Update the color after the user sorted the datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridView1_Sorted(object sender, EventArgs e)
        {
            ManageColor();
        }

        /// <summary>
        /// Put the No on the row header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridViewDetailRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            { 
                var grid = sender as DataGridView;
                var rowIdx = (e.RowIndex + 1).ToString();

                var centerFormat = new StringFormat()
                {
                    // right alignment might actually make more sense for numbers
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                Font corbel = new Font("Corbel", 10);
                var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
                e.Graphics.DrawString(rowIdx, corbel, SystemBrushes.ControlText, headerBounds, centerFormat);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

    }
}

