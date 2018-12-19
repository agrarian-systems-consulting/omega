using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Drawing;
using OMEGA.SQLQuery;
using OMEGA.Forms;
using OMEGA.SQLQuery.SpecificQueryBuilder;

namespace OMEGA.Data_Classes
{
    class Variable
    {
        #region variables Globales
        private Subro.Controls.DataGridViewGrouper grouper = new Subro.Controls.DataGridViewGrouper();
        private DataTable tableVariable = new DataTable();
        private string mainQuery;
        private Boolean mDataSaved = false;
        private int mKeyIndex;
        private int mCurrentid;
        private Button mbuttonOk;
        private Button mbuttonCancel;
        private Button mbuttonDuplicate;
        private Button mbuttonRemove;
        private int langue = Properties.Settings.Default.Langue;
        private Button mbuttonSave;
        private Button mbuttonProposition;
        private Button mButtongroupe;
        private Color color1;
        private Color color2;
        private PictureBox pictureBox;
        private bool showProposals = true;
        private VariableForm mFormVariable;
        private string mCurrentUGlo;
        #endregion

        private DataGridView mdataGridView;
           

        internal Variable(VariableForm form)
        {
            mFormVariable = form;
            mbuttonCancel = mFormVariable.buttonCancel;
            mbuttonOk = mFormVariable.buttonOk;
            mbuttonDuplicate = mFormVariable.buttonDuplicate;
            mbuttonSave = mFormVariable.buttonSave;
            pictureBox = mFormVariable.pictureBoxExport;
            mButtongroupe = mFormVariable.buttonGroupe;
            mdataGridView = mFormVariable.dataGridView1;
            mbuttonRemove = mFormVariable.buttonRemove;
            mbuttonProposition = mFormVariable.buttonProposition;
            LoadGridVariable();
            CreateDico();
        }


        private void LoadGridVariable()
        {
            try
            {
                 mainQuery = "Select Id,Categorie,Nom,UAte,UEnt,UGlobal From Variable " +
                               "Join SystemeUnite on Variable.IdSysUnite = SystemeUnite.IdSysUnit" +
                               " UNION ALL " +
                               "Select Id,Categorie,Nom,null,null,null From Variable WHERE Variable.IdSysUnite = 0;";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(tableVariable);

                mdataGridView.DataSource = tableVariable;
                SetViewProperty();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal List<int> ListeRecordToDelete { get; set; } = new List<int>();
        internal Dictionary<int, InfoUserVariable> DicoInfoUserVariable { get; set; }

        private void SetViewProperty()
        {
            mdataGridView.Columns[3].ReadOnly = true;
            mdataGridView.Columns[4].ReadOnly = true;
            mdataGridView.Columns[5].ReadOnly = true;

            mdataGridView.Columns[0].Visible = false;
            mdataGridView.RowHeadersWidth = 21;
            mdataGridView.AllowUserToAddRows = false;
            mdataGridView.AllowUserToDeleteRows = false;
        }

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

        private void SetCaption()
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;
                mbuttonOk.Text = Translation.Translate("OK", langue);
                mbuttonRemove.Text = Translation.Translate("Remove", langue);
                mbuttonCancel.Text = Translation.Translate("Cancel", langue);
                mbuttonDuplicate.Text = Translation.Translate("Duplicate", langue);
                mbuttonSave.Text = Translation.Translate("Save", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Creation of the dictionnary that represent the gridview and that contains
        /// the data insert by the user
        /// </summary>
        private void CreateDico()
        {
            try
            {
                int key = 0;
                DicoInfoUserVariable = new Dictionary<int, InfoUserVariable>();
                foreach (DataRow row in tableVariable.Rows)
                {
                    InfoUserVariable infoUserProduit = new InfoUserVariable();
                    int.TryParse(row.ItemArray[0].ToString(), out key);
                    DicoInfoUserVariable.Add(key, infoUserProduit);
                }
                mKeyIndex = Commun.GetSeqId("Variable") + 1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void RenameColumnHeader()
        {
            int langue = Properties.Settings.Default.Langue;
            mdataGridView.Columns[1].HeaderText = Translation.Translate("Group", langue);
            mdataGridView.Columns[2].HeaderText = Translation.Translate("Name", langue);
            mdataGridView.Columns[3].HeaderText = Translation.Translate("Unit Activity", langue);
            mdataGridView.Columns[3].Width = 86;
            mdataGridView.Columns[4].HeaderText = Translation.Translate("Unit work Farm", langue);
            mdataGridView.Columns[4].Width = 86;
            mdataGridView.Columns[5].HeaderText = Translation.Translate("Unit global", langue);
            mdataGridView.Columns[5].Width = 86;
            mdataGridView.ColumnHeadersHeight = 20;
        }

        private void SaveData(bool DeletefFromTable)
        {
            if (mDataSaved == false)
            {
                try
                {
                    string query;
                    // DeleteGroupe();
                    if (DeletefFromTable)
                    {
                        foreach (int index in ListeRecordToDelete)
                        {
                            query = SQLQueryBuilder.DeleteQuery(index, "Variable");
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }

                    foreach (KeyValuePair<int, InfoUserVariable> item in DicoInfoUserVariable)
                    {
                        // si l'utilisateur créer un nouveau produit, on l'ajoute dans la table
                        if (item.Value.AddToVariableTable)
                        {
                            // l'utilisateur ajout un produit dans un groupe
                            if (!item.Value.Modification)
                            {
                                query = VariableQuery.AddItemInGroupQuery(item);
                                SQlQueryExecuter.RunQuery(query);

                                query = SQLQueryBuilder.MaxIdQuery("Variable");
                                List<int> list = SQlQueryExecuter.RunQueryReaderInt("MAX(ID)", query);
                                if (Commun.ListHasValue(list))
                                {
                                    item.Value.ID = list[0];
                                }
                            }
                            else
                            {
                                query = VariableQuery.UpdateValueQuery(item);
                                SQlQueryExecuter.RunQuery(query);
                            }
                            item.Value.AddToVariableTable = false;
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                }
            }
        }

        internal void ButtonAdd(object sender, EventArgs e)
        {
            try
            {
                // we add a new row in the table with a new ID
                string[] rowvalue = new string[mdataGridView.ColumnCount];
                for (int i = 0; i <= mdataGridView.ColumnCount - 1; i++)
                {
                    if (i == 0) rowvalue[i] = (GetLastIndex() + 1).ToString();
                    else rowvalue[i] = null;
                }
                tableVariable.Rows.Add(rowvalue);
                // the ID of the dico might be the same than in the table or not
                // it's not a big deal if not as long as we insert the new record,
                // the final ID on the table does not matter.
                if (!DicoInfoUserVariable.ContainsKey(GetLastIndex()))
                {
                    DicoInfoUserVariable.Add(GetLastIndex(), new InfoUserVariable());
                }
                mCurrentid = GetLastIndex();
                SetScrollBarPosition();
                // mdataGridView.CurrentCell = mdataGridView.Rows[0].Cells[0];
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetScrollBarPosition()
        {
            try
            {
                if (mdataGridView.SortOrder == SortOrder.Ascending)
                {
                    mdataGridView.FirstDisplayedScrollingRowIndex = 0;
                }
                else
                {
                    mdataGridView.FirstDisplayedScrollingRowIndex = mdataGridView.RowCount;
                }
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        internal void ButtonSaveData(object sender, SaveDataEvent e)
        {
            try
            {
                FillInfoUser();
                SaveData(e.deleteOnTable);
                ResetMainQuery(e.table);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                tableVariable = new DataTable();
                adapter.Fill(tableVariable);
                mdataGridView.DataSource = tableVariable;
                mdataGridView.Refresh();
                mDataSaved = true;
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ButtonRemoveRow(object sender, EventArgs e)
        {
            try
            {
                mdataGridView.AllowUserToDeleteRows = true;
                DataGridViewSelectedRowCollection UserSelectedRow = mdataGridView.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    if (!ListeRecordToDelete.Contains(GetId(index))) ListeRecordToDelete.Add(GetId(index));
                    mdataGridView.Rows.RemoveAt(index);
                }

                mDataSaved = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        internal void ButtonDuplicate(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection ListuserRow = mdataGridView.SelectedRows;
                string[] rowvalue = new string[13];
                if (ListuserRow.Count == 1)
                {
                    for (int j = 0; j <= ListuserRow.Count - 1; j++)
                    {
                        rowvalue = new string[mdataGridView.ColumnCount];
                        for (int i = 0; i <= mdataGridView.ColumnCount - 1; i++)
                        {
                            rowvalue[i] = ListuserRow[j].Cells[i].Value.ToString();
                        }
                    }
                    // id produit = clé dico
                    int id = Commun.GetMaxId("ID", "Variable") + 1;
                    while (DicoInfoUserVariable.ContainsKey(id))
                    {
                        id++;
                    }
                    DicoInfoUserVariable.Add(id, new InfoUserVariable());
                    rowvalue[0] = id.ToString();
                    tableVariable.Rows.Add(rowvalue);

                    DicoInfoUserVariable[id].Categorie = rowvalue[1];
                    DicoInfoUserVariable[id].Nom = rowvalue[2];
                    DicoInfoUserVariable[id].U_ate = rowvalue[3];
                    DicoInfoUserVariable[id].U_Ent = rowvalue[4];
                    DicoInfoUserVariable[id].U_Glo = rowvalue[5];
                    int tempint;
                    int.TryParse(rowvalue[0], out tempint);
                    DicoInfoUserVariable[id].ID = tempint;
                    DicoInfoUserVariable[id].Modification = false;
                    DicoInfoUserVariable[id].AddToVariableTable = true;
                    mDataSaved = false;
                }
                else
                {
                    MessageBox.Show(Translation.Translate("Error,Plase reselect the row to duplicate",langue));
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void buttonCancel(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = new DialogResult();
                if (mDataSaved == false)
                {
                    dialogResult = MessageBox.Show(Translation.Translate("Data has been modified but not saved, do you really want to exit ?", langue), "Warning", MessageBoxButtons.YesNo);
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

        internal void dataGridView1_Sorted(object sender, EventArgs e)
        {
            ManageColor();
        }

        internal void dataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (showProposals) FillAutolistAfterClick(e, e.ColumnIndex);
                mCurrentid = GetId(e.RowIndex);
                SetDicoValue(e.RowIndex, true);
                if (e.ColumnIndex == 5 && DicoInfoUserVariable[mCurrentid].U_Glo == "")
                {
                    DicoInfoUserVariable[mCurrentid].U_Glo = mdataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void FillAutolistAfterClick(DataGridViewCellEventArgs e, int column)
        {
            try
            {
                double decalage = 1.15;
                List<string> list = new List<string>();
                string query = "";
                ContextMenu contextmenu = new ContextMenu();
                List<MenuItem> ListItem = new List<MenuItem>();
                switch (column)
                {

                    case 1: //group
                        query = "Select distinct Categorie From Variable;";
                        list = SQlQueryExecuter.RunQueryReaderStr("Categorie", query);
                        break;
                    case 3: //unit activite
                        query = "Select UAte From SystemeUnite;";
                        list = SQlQueryExecuter.RunQueryReaderStr("UAte", query);
                        break;
                    case 4: // unit exploi
                        query = "Select UEnt From SystemeUnite;";
                        list = SQlQueryExecuter.RunQueryReaderStr("UEnt", query);
                        break;
                    case 5: // unit global
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

                mdataGridView.ContextMenu = contextmenu;
                Rectangle MyCell = mdataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                int x = (int)Math.Ceiling(MyCell.X * decalage);
                int y = (int)Math.Ceiling(MyCell.Y * decalage);
                Point point = new Point(x, y);
                contextmenu.Show(mdataGridView, point);

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

        private void Item_Click(object sender, EventArgs e)
        {
            try
            {
                string[] array = sender.ToString().Split(new string[] { "Text:" }, StringSplitOptions.None);
                mdataGridView.CurrentCell.Value = array[1].Trim();
                if (mdataGridView.CurrentCell.ColumnIndex == 5)
                {
                    mCurrentUGlo = array[1].Trim();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void dataGridView1CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // the parameters "true" tells that the user edited a cell, so we have the save it
                SetDicoValue(e.RowIndex, true);
                mDataSaved = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ExportTable(object sender, EventArgs e)
        {
            Export.RunExportTable(tableVariable);
        }

        internal void NoteForm(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("Variable");
            form.ShowDialog();
        }

        internal void dataGridViewMouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    //lors d'un click droit on peu caché les colonnes inutiles
                    ContextMenu contextmenu = new ContextMenu();
                    MenuItem itemHide = new MenuItem("Hide column");
                    MenuItem itemShow = new MenuItem("Show all columns");
                    contextmenu.MenuItems.Add(itemHide);
                    contextmenu.MenuItems.Add(itemShow);
                    Point pos = new Point(e.X, e.Y);
                    var hitTestInfo = mdataGridView.HitTest(e.X, e.Y);
                    mdataGridView.ContextMenu = contextmenu;
                    if (hitTestInfo.ColumnIndex > 0)
                    {
                        contextmenu.Show(mdataGridView, pos);
                        itemHide.Click += itemHide_Click(this, new EventArgs(), hitTestInfo.ColumnIndex);
                        itemShow.Click += itemShow_Click;
                    }
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private EventHandler itemHide_Click(object sender, EventArgs e, int index)
        {
            mdataGridView.Columns[index].Visible = false;
            return null;
        }
        private void itemShow_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in mdataGridView.Columns)
            {
                if (column.Index < 9)
                {
                    column.Visible = true;
                }
            }
        }

        internal void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
        }

        internal void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            pictureBox.BorderStyle = BorderStyle.None;
        }

        /// <summary>
        /// Add the product selected on the list to the current tab
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="tab"></param>
        /// <param name="dvg"></param>
        internal void AddSelectedProduct(int ID, string tab, DataGridView dvg = null)
        {
            try
            {
                if (tab == "TabProduits")
                {
                    string query = ProduitQuery.AddSelectedProduitQuery(ID);
                    string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
                    tableVariable.Rows.Add(rowvalue);

                    if (DicoInfoUserVariable.ContainsKey(ID) == false)
                    { DicoInfoUserVariable.Add(ID, new InfoUserVariable()); }
                    DicoInfoUserVariable[ID].Categorie = rowvalue[1];
                    DicoInfoUserVariable[ID].Nom = rowvalue[2];
                    DicoInfoUserVariable[ID].U_ate = rowvalue[3];
                    DicoInfoUserVariable[ID].U_Ent = rowvalue[4];
                    DicoInfoUserVariable[ID].U_Glo = rowvalue[5];
                    DicoInfoUserVariable[ID].Modification = false;
                    DicoInfoUserVariable[ID].AddToVariableTable = false;
                    DicoInfoUserVariable[ID].ID = ID;
                    mDataSaved = false;
                }
                if (tab == "TabProduitsPied")
                {
                    string query = ProduitQuery.AddSelectedProduitQuery2(ID);
                    string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
                    dvg.Rows.Add(rowvalue);
                    // on ajoute l'ID produit dans la table ite/pied pour ensuite pouvoir modifier
                    query = "Insert into Item_pied (IdProduits) VALUES ('" + ID + "')";
                    SQlQueryExecuter.RunQuery(query);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }


        }

        private void SetDicoValue(int rowindex, bool add)
        {
            // 0 = groupe, 1 = nom, 2 = UATE, 3 = UENT , 4 = UGLo , 5 = Prix
            // 6 = TVA , 7 = Quantite 1  , 8 = qtAv1 , 9 = idDefCateg
            // 10 = idSysunit , 11 = ID TVA , 12 = Id produit
            try
            {
                int tempint;
                if (mCurrentid == -1) return;
                if (!DicoInfoUserVariable.ContainsKey(mCurrentid)) return;
                if (DicoInfoUserVariable[mCurrentid].OldValueInCell == null)
                {
                    DicoInfoUserVariable[mCurrentid].OldValueInCell = new string[6];
                    for (int i = 0; i <= 5; i++)
                    {
                        DicoInfoUserVariable[mCurrentid].OldValueInCell[i] = mdataGridView.Rows[rowindex].Cells[i].Value.ToString();
                    }
                }
                DicoInfoUserVariable[mCurrentid].Categorie = mdataGridView.Rows[rowindex].Cells[1].Value.ToString();
                DicoInfoUserVariable[mCurrentid].Nom = mdataGridView.Rows[rowindex].Cells[2].Value.ToString();
                DicoInfoUserVariable[mCurrentid].U_ate = mdataGridView.Rows[rowindex].Cells[3].Value.ToString();
                DicoInfoUserVariable[mCurrentid].U_Ent = mdataGridView.Rows[rowindex].Cells[4].Value.ToString();
                DicoInfoUserVariable[mCurrentid].U_Glo = mdataGridView.Rows[rowindex].Cells[5].Value.ToString();
                int.TryParse(mdataGridView.Rows[rowindex].Cells[0].Value.ToString(), out tempint);
                DicoInfoUserVariable[mCurrentid].ID = tempint;
                DicoInfoUserVariable[mCurrentid].AddToVariableTable = add;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Fill the dictionnary used to save the user's values
        /// </summary>
        private void FillInfoUser()
        {
            try
            {
                foreach (int key in DicoInfoUserVariable.Keys)
                {
                    // if the unity Ate is not set, then none of this ID has been modified
                    // so we dont have any info to add/insert 
                    
                    if (DicoInfoUserVariable[key].U_ate != null)
                    {
                        if (DicoInfoUserVariable[key].U_Glo == "")
                        {
                            DicoInfoUserVariable[key].U_Glo = mCurrentUGlo;
                        }
                        String[] reader;
                        // on cherche l'id systeme unit,
                        string query = SQLQueryBuilder.GetIdSystemeUniteQuery(DicoInfoUserVariable[key].U_ate, DicoInfoUserVariable[key].U_Ent, DicoInfoUserVariable[key].U_Glo);
                        reader = SQlQueryExecuter.RunQueryReader(query);
                        if (reader.Length > 0)
                        {
                            if (reader[0] != null)
                            {
                               int id;
                                int.TryParse(reader[0], out id);
                                DicoInfoUserVariable[key].IdsystUnit = id;
                            }
                            else
                            {
                                DicoInfoUserVariable[key].IdsystUnit = 0;
                            }
                        }

                        //on cherche s'il s'agit d'une modification
                        if (key > Commun.GetSeqId("Variable"))
                        {
                            DicoInfoUserVariable[key].Modification = false;
                        }
                        else if (DicoInfoUserVariable[key].OldValueInCell != null)
                        {
                            // looking for the old name, if it has a value then it has been modified
                            if (DicoInfoUserVariable[key].OldValueInCell[1] == "")
                            {
                                DicoInfoUserVariable[key].Modification = false;
                                DicoInfoUserVariable[key].AddToVariableTable = true;
                            }
                            else
                            {
                                DicoInfoUserVariable[key].Modification = true;
                                DicoInfoUserVariable[key].ID = key;
                            }
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void RefreshView(string table)
        {
            try
            {
                ResetMainQuery(table);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                tableVariable.Clear();
                adapter.Fill(tableVariable);
                mdataGridView.DataSource = tableVariable;
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /// <summary>
        /// Reset the main query with the new value added or deleted by the user
        /// </summary>
        /// <param name="table"></param>
        private void ResetMainQuery(string table)
        {
            try
            {
           // string query = "Select Id,Groupe,Nom,UAte,UEnt,UGlobal From Variable " +
                    //    "Join SystemeUnite on Variable.IdSysUnite = SystemeUnite.IdSysUnite;";
           // mainQuery = query;
        }
        catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

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
                //MessageBox.Show(Ex.Message);
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
                if (autoText != null)
                {
                    switch (mdataGridView.CurrentCell.ColumnIndex)
                    {
                        case 1: // groupe product
                            addItems(DataCollection, 0);
                            break;
                        case 3: // unité Atelier
                            addItems(DataCollection, 2);
                            break;
                        case 4: // unité Entreprise
                            addItems(DataCollection, 3);
                            break;
                        case 5: // unité Global
                            addItems(DataCollection, 4);
                            break;
                        default:
                            autoText.AutoCompleteMode = AutoCompleteMode.None;
                            return;
                    }
                }
                autoText.AutoCompleteCustomSource = DataCollection;
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
            string query = ""; ;
            List<string> list = new List<string>();
            switch (type)
            {
                case 1: // groupe produit
                    query = ProduitQuery.AutoCompleteQuery();
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    break;
                case 3: // Unity Atelier
                    query = SQLQueryBuilder.AutoCompleteQueryUnity("UAte");
                    list = SQlQueryExecuter.RunQueryReaderStr("UAte", query);
                    break;
                case 4: // Unity Atelier
                    query = SQLQueryBuilder.AutoCompleteQueryUnity("UEnt");
                    list = SQlQueryExecuter.RunQueryReaderStr("UEnt", query);
                    break;
                case 5: // Unity Atelier
                    query = SQLQueryBuilder.AutoCompleteQueryUnity("UGlobal");
                    list = SQlQueryExecuter.RunQueryReaderStr("UGlobal", query);
                    break;
            }

            foreach (string item in list)
            {
                col.Add(item);
            }
            mDataSaved = false;
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
                if (mdataGridView.Rows[Rowindex].Cells[0].Value != null)
                {
                    int.TryParse(mdataGridView.Rows[Rowindex].Cells[0].Value.ToString(), out id);
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
        /// return the last index in the table
        /// </summary>
        /// <returns></returns>
        private int GetLastIndex()
        {
            try
            {
                if (tableVariable.Rows.Count > 0)
                {
                    int id;
                    int.TryParse(tableVariable.Rows[tableVariable.Rows.Count - 1].ItemArray[0].ToString(), out id);
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
            //try
            //{
            //    if (mdataGridView.CurrentCell.ColumnIndex == 0)
            //    {
            //        for (int i = 0; i <= mdataGridView.Rows.Count - 1; i++)
            //        {
            //            if (mdataGridView.Rows[i].Cells[1].Value.ToString() == "")
            //            {
            //                mdataGridView.CurrentCell = mdataGridView.Rows[i].Cells[0];
            //                mdataGridView.CurrentCell.Selected = true;
            //                mdataGridView.FirstDisplayedScrollingRowIndex = i;
            //                mdataGridView.Refresh();
            //                break;
            //            }
            //        }
            //    }
            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            //}
        }

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

        private void ManageColor()
        {
            if (color1 != null && color2 != null)
            {
                Commun.Setbackground(mdataGridView, color1, color2);
            }
        }

        internal void EnableGroupe(object sender, EventArgs e)
        {
            try
            {
                if (mButtongroupe.Text.Contains("Enable"))
                {
                    grouper.DataGridView = mdataGridView;
                    grouper.SetGroupOn(mdataGridView.Columns[1]);
                    grouper.DisplayGroup += grouper_DisplayGroup;
                    grouper.CollapseAll();
                    mButtongroupe.Text = "Disable Groupe";
                    return;
                }
                if (mButtongroupe.Text.Contains("Disable"))
                {
                    grouper.DataGridView = mdataGridView;
                    grouper.RemoveGrouping();
                    grouper.DisplayGroup -= grouper_DisplayGroup;
                    mButtongroupe.Text = "Enable Groupe";
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
            try
            {
                e.BackColor = (e.Group.GroupIndex % 2) == 0 ? color1 : color2;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}

