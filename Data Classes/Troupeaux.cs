using OMEGA.Forms;
using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace OMEGA.Data_Classes
{

    class Troupeaux
    {
        
 #region variables Globales
        private DataTable mTroupeauxTable = new DataTable("Produits");
        private string mainQuery;
        private Boolean mDataSaved = false;
        private int mKeyIndex;
        private int mCurrentid;
        private Button mbuttonOk;
       private int langue = Properties.Settings.Default.Langue;
        private Button mbuttonCancel;
        private Button mbuttonDuplicate;
        private Button mbuttonRemove;
        private Button mbuttonSave;
        private Color color1;
        private Color color2;
        private Color color3;
        private bool showProposals;
        private Button mbuttonProposition;
        private Button mbuttongroup;
        private Subro.Controls.DataGridViewGrouper grouper = new Subro.Controls.DataGridViewGrouper();
        private PictureBox pictureBox;
        #endregion

        internal List<int> ListeRecordToDelete { get; set; }
        internal DataGridView DataGridView { get; set; }
        internal Dictionary<int, InfoUserTroupeaux> DicoInfoUserTroupeaux { get; set; }
        internal event EventHandler<MenuContextClickEvent> ClickOnMenuContext;

        internal Troupeaux(DataGridView mygrid, Button buttonok, Button buttonsave,
           Button buttoncancel, Button buttonRemove, Button buttonDuplicate, PictureBox ptcbox
            , Button bttngroupe, Button bttnpropo)
        {
            try
            {
                mbuttonOk = buttonok;
                mbuttongroup = bttngroupe;
                mbuttonProposition = bttnpropo;
                mbuttonDuplicate = buttonDuplicate;
                mbuttonRemove = buttonRemove;
                mbuttonSave = buttonsave;
                pictureBox = ptcbox;
                DataGridView = mygrid;
                mbuttonCancel = buttoncancel;
                mainQuery = TroupeauxQuery.MainQuery();
                ListeRecordToDelete = new List<int>();
                LoadGridViewData();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// fill the gridview with the data on the table
        /// </summary>
        private void LoadGridViewData()
        {
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mTroupeauxTable);
                DataGridView.DataSource = mTroupeauxTable;
                DataGridView.Columns[0].Visible = false;
                DataGridView.Columns[0].Resizable = DataGridViewTriState.False;
                DataGridView.Columns[3].Visible = false;
                for (int i = 1; i <= DataGridView.Columns.Count - 1; i++)
                {
                    DataGridView.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                RenameColumnHeader();
                CreateDico();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


    /// <summary>
    /// Creation of the dictionnary that represent the gridview and that contains
    /// the data inserted by the user
    /// </summary>
    private void CreateDico()
        {
            try
            { 
                int key = 0;
                DicoInfoUserTroupeaux = new Dictionary<int, InfoUserTroupeaux>();
                foreach (DataRow row in mTroupeauxTable.Rows)
                {
                    InfoUserTroupeaux InfoUserTroupeaux = new InfoUserTroupeaux();
                    int.TryParse(row.ItemArray[0].ToString(), out key);
                    DicoInfoUserTroupeaux.Add(key, InfoUserTroupeaux);
                }
                mKeyIndex = Commun.GetSeqId("Def_Bestiaux") + 1;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void RenameColumnHeader()
        {
            try
            { 
                int langue = Properties.Settings.Default.Langue;
                DataGridView.Columns[1].HeaderText = Translation.Translate("Group", langue);
                DataGridView.Columns[2].HeaderText = Translation.Translate("Name", langue);
                DataGridView.Columns[3].HeaderText = Translation.Translate("Val Inventaire", langue);
                DataGridView.Columns[4].HeaderText = Translation.Translate("Price", langue);
                DataGridView.Columns[5].HeaderText = Translation.Translate("TVA", langue);
                DataGridView.Columns[6].HeaderText = Translation.Translate("Origine", langue);
                DataGridView.Columns[7].HeaderText = Translation.Translate("Generates", langue);
                DataGridView.Columns[8].HeaderText = Translation.Translate("%", langue);
                DataGridView.Columns[9].HeaderText = Translation.Translate("Generates", langue);
                DataGridView.Columns[10].HeaderText = Translation.Translate("%", langue);
                DataGridView.Columns[11].HeaderText = Translation.Translate("Generates", langue);
                DataGridView.Columns[12].HeaderText = Translation.Translate("%", langue);
                DataGridView.Columns[13].HeaderText = Translation.Translate("Generates", langue);
                DataGridView.Columns[14].HeaderText = Translation.Translate("%", langue);
                DataGridView.ColumnHeadersHeight = 12;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SaveData(bool DeletefFromTable)
        {
            if (mDataSaved == false)
            {
                try
                {
                    string query;
                    string[] reader;
                    // DeleteGroupe();
                    if (DeletefFromTable)
                    {
                        foreach (int index in ListeRecordToDelete)
                        {
                            query = SQLQueryBuilder.DeleteQuery(index, "Def_Bestiaux");
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }

                    foreach (KeyValuePair<int, InfoUserTroupeaux> item in DicoInfoUserTroupeaux)
                    {

                        // si l'utilisateur créer un nouveau produit, on l'ajoute dans la table
                        //if (item.Value.AddtoDefBestiauxTable)
                        {
                            // si le groupe n'existe pas, on le rajoute
                            if (item.Value.GroupeExiste == false)
                            {
                                query = SQLQueryBuilder.AddNewgGroupeQuery(item.Value.Groupe, 10);
                                SQlQueryExecuter.RunQuery(query);

                                query = SQLQueryBuilder.SelectGroupeQuery(item.Value.Groupe);
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
                            if (item.Value.Modification == false)
                            {
                                query = TroupeauxQuery.AddItemInGroupQuery(item);
                                SQlQueryExecuter.RunQuery(query);

                                item.Value.ID = SQLQueryBuilder.FindId("Def_Bestiaux", "IdBestiaux", "Nom", item.Value.Nom);

                            }
                            else 
                            {
                                query = TroupeauxQuery.UpdateValueQuery(item);
                                SQlQueryExecuter.RunQuery(query);
                            }
                            item.Value.AddtoDefBestiauxTable = false;
                        }
                    }
                    SetColor();
                    Properties.Settings.Default.DoitSauvegarger = true;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                }
            }
        }

        internal void ButtonAdd(object sender, EventArgs e)
        {
            try
            {
                // we add a new row in the table with a new ID
                string[] rowvalue = new string[DataGridView.ColumnCount];
                for (int i = 0; i <= DataGridView.ColumnCount - 1; i++)
                {
                    if (i == 0) rowvalue[i] = (GetLastIndex() + 1).ToString();
                    else rowvalue[i] = null;
                }
                mTroupeauxTable.Rows.Add(rowvalue);
                // the ID of the dico might be the same than in the table or not
                // it's not a big deal if not as long as we insert the new record,
                // the final ID on the table does not matter.
                DicoInfoUserTroupeaux.Add(GetLastIndex(), new InfoUserTroupeaux());
                mCurrentid = GetLastIndex();
                //DataGridView.CurrentCell = DataGridView.Rows[0].Cells[0];
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
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
                mTroupeauxTable.Clear();
                adapter.Fill(mTroupeauxTable);
                DataGridView.DataSource = mTroupeauxTable;
                DataGridView.Refresh();
                mDataSaved = true;
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
                DataGridView.AllowUserToDeleteRows = true;
                DataGridViewSelectedRowCollection UserSelectedRow = DataGridView.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    if (!ListeRecordToDelete.Contains(GetId(index))) ListeRecordToDelete.Add(GetId(index));
                    DataGridView.Rows.RemoveAt(index);
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
                DataGridViewSelectedRowCollection ListuserRow = DataGridView.SelectedRows;
                string[] rowvalue = new string[12];
                if (ListuserRow.Count == 1)
                {
                    for (int j = 0; j <= ListuserRow.Count - 1; j++)
                    {
                        rowvalue = new string[DataGridView.ColumnCount];
                        for (int i = 0; i <= DataGridView.ColumnCount - 1; i++)
                        {
                           rowvalue[i] = ListuserRow[j].Cells[i].Value.ToString();
                        }
                        //mdataGridView1.Rows.Add(rowvalue);
                        mTroupeauxTable.Rows.Add(rowvalue);
                    }
                    // id produit = clé dico
                    int id = Commun.GetMaxId("IdBestiaux", "Def_Bestiaux") + 1;
                    while (DicoInfoUserTroupeaux.ContainsKey(id))
                    {
                        id++;
                    }
                    DicoInfoUserTroupeaux.Add(id, new InfoUserTroupeaux());
                    rowvalue[0] = id.ToString();
                    mTroupeauxTable.Rows.Add(rowvalue);
                    DicoInfoUserTroupeaux[id].Groupe = rowvalue[1];
                    DicoInfoUserTroupeaux[id].Nom = rowvalue[2];
                    DicoInfoUserTroupeaux[id].ValInventaire = rowvalue[3];
                    DicoInfoUserTroupeaux[id].TVA = rowvalue[5];
                    DicoInfoUserTroupeaux[id].Origine = rowvalue[6];
                    int tempint;
                    double result = 0;
                    double.TryParse(rowvalue[4], out result);
                    DicoInfoUserTroupeaux[id].Price = result;
                    DicoInfoUserTroupeaux[id].data1 = rowvalue[7];
                    double.TryParse(rowvalue[8], out result);
                    DicoInfoUserTroupeaux[id].percent1 = result;
                    DicoInfoUserTroupeaux[id].data2 = rowvalue[9];
                    double.TryParse(rowvalue[10], out result);
                    DicoInfoUserTroupeaux[id].percent2 = result;
                    DicoInfoUserTroupeaux[id].data3 = rowvalue[11];
                    double.TryParse(rowvalue[12], out result);
                    DicoInfoUserTroupeaux[id].percent3 = result;
                    DicoInfoUserTroupeaux[id].data4 = rowvalue[13];
                    double.TryParse(rowvalue[14], out result);
                    DicoInfoUserTroupeaux[id].percent4 = result;
                    int.TryParse(rowvalue[0], out tempint);
                    DicoInfoUserTroupeaux[id].IdDefCateg = tempint;
                    DicoInfoUserTroupeaux[id].GroupeExiste = true;
                    DicoInfoUserTroupeaux[id].Modification = false;
                    DicoInfoUserTroupeaux[id].AddtoDefBestiauxTable = true;
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
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        internal void dataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 0) return;
                if(showProposals) FillAutolistAfterClick(e);
                mCurrentid = GetId(e.RowIndex);
                SetDicoValue(e.RowIndex, false);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void dataGridView1CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                // the parameters "true" tells that the user edited a cell, so we have the save it
                mCurrentid = GetId(e.RowIndex);
                SetDicoValue(e.RowIndex, true);
                if (e.ColumnIndex == 0)
                {
                    DataGridView.FirstDisplayedScrollingRowIndex = DataGridView.Rows[e.RowIndex].Index;
                    SetFocusToNewRow();
                    DataGridView.Refresh();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void FillAutolistAfterClick(DataGridViewCellEventArgs e)
        {
            try
            {
                ContextMenu contextmenu = new ContextMenu();
                List<MenuItem> ListItem = new List<MenuItem>();
                string query = "";
                switch (DataGridView.CurrentCell.ColumnIndex)
                {
                    case 1: //group
                        query = TroupeauxQuery.AutoCompleteQuery();
                        break;
                    case 5: //TVA
                        query = SQLQueryBuilder.AutoCompleteQueryTVA();
                        break;
                    default: return;
                }
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
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
                Point point = new Point(MyCell.X + MyCell.Width / 2, MyCell.Y + MyCell.Height / 2);
                contextmenu.Show(DataGridView, point);

                foreach (MenuItem item in ListItem)
                {
                    item.Click += Item_Click;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ExportTable(object sender, EventArgs e)
        {
            Export.RunExportTable(mTroupeauxTable);
        }

        internal void keyPress(object sender, KeyPressEventArgs e)
        {
            Control control = DataGridView.EditingControl;
            control.Hide();
        }

        internal void NoteForm(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("Def_Bestiaux");
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
                    contextmenu.MenuItems.Add("Hide column");
                    contextmenu.MenuItems.Add("Show all columns");
                    MenuItem itemHide = new MenuItem("Hide column");
                    itemHide.Tag = "Hide";
                    MenuItem itemShow = new MenuItem("Show all columns");
                    itemHide.Tag = "Show";
                    Point pos = new Point(e.X, e.Y);
                    var hitTestInfo = DataGridView.HitTest(e.X, e.Y);
                    DataGridView.ContextMenu = contextmenu;
                    contextmenu.Show(DataGridView, pos);
                    ClickOnMenuContext?.Invoke(this, new MenuContextClickEvent(hitTestInfo.ColumnIndex, contextmenu));
                   // this.ClickOnMenuContext += new EventHandler<MenuContextClickEvent>(HideColum);
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
                DataGridView.CurrentCell.Value = array[1].Trim();
                DataGridView.Refresh();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
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
        /*internal void AddSelectedProduct(int ID, string tab, DataGridView dvg = null)
        {
            try
            {
                if (tab == "TabProduits")
                {
                    string query = ProduitQuery.AddSelectedProduitQuery(ID);
                    string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
                    if (rowvalue[7] == "") rowvalue[7] = "0";
                    if (rowvalue[8] == "") rowvalue[8] = "0";
                    if (rowvalue[9] == "") rowvalue[9] = "0";
                    mTroupeauxTable.Rows.Add(rowvalue);

                    if (DicoInfoUserTroupeaux.ContainsKey(ID) == false)
                    { DicoInfoUserTroupeaux.Add(ID, new InfoUserTroupeaux()); }
                    DicoInfoUserTroupeaux[ID].Groupe = rowvalue[0];
                    DicoInfoUserTroupeaux[ID].Nom = rowvalue[1];
                    DicoInfoUserTroupeaux[ID].U_ate = rowvalue[2];
                    DicoInfoUserTroupeaux[ID].U_Ent = rowvalue[3];
                    DicoInfoUserTroupeaux[ID].U_Glo = rowvalue[4];
                    int tempint;
                    double tempdouble = 0;
                    double.TryParse(rowvalue[5], out tempdouble);
                    DicoInfoUserTroupeaux[ID].Price = tempdouble;
                    DicoInfoUserTroupeaux[ID].TVA = rowvalue[6];
                    int.TryParse(rowvalue[10], out tempint);
                    DicoInfoUserTroupeaux[ID].IdDefCateg = tempint;
                    int.TryParse(rowvalue[11], out tempint);
                    DicoInfoUserTroupeaux[ID].IdsystUnit = tempint;
                    int.TryParse(rowvalue[12], out tempint);
                    DicoInfoUserTroupeaux[ID].IdTVA = tempint;
                    double.TryParse(rowvalue[7], out tempdouble);
                    DicoInfoUserTroupeaux[ID].Qte1 = tempdouble;
                    double.TryParse(rowvalue[8], out tempdouble);
                    DicoInfoUserTroupeaux[ID].Qte2 = tempdouble;
                    double.TryParse(rowvalue[9], out tempdouble);
                    DicoInfoUserTroupeaux[ID].QteAv1 = tempdouble;
                    DicoInfoUserTroupeaux[ID].GroupeExiste = true;
                    DicoInfoUserTroupeaux[ID].Modification = false;
                    DicoInfoUserTroupeaux[ID].AddToProductTable = false;
                    DicoInfoUserTroupeaux[ID].ID = ID;
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


        }*/

        private void SetDicoValue(int rowindex, bool add)
        {
            // "Select IdDefBestiaux,DefCateg.Nom,Def_Bestiaux.Nom,ValInv,Prix, TVA.Nom,Origine," +
            // "Donne1,DonnePcent1,Donne2,DonnePcent2,Donne3,DonnePcent3,Donne4,DonnePcent4 "

            try
            {
                int tempint;
                double tempdouble;
                if (DicoInfoUserTroupeaux[mCurrentid].OldValueInCell == null)
                {
                    DicoInfoUserTroupeaux[mCurrentid].OldValueInCell = new string[15];
                    for (int i = 0; i <= 14; i++)
                    {
                        DicoInfoUserTroupeaux[mCurrentid].OldValueInCell[i] = DataGridView.Rows[rowindex].Cells[i].Value.ToString();
                    }
                }
                int.TryParse(DataGridView.Rows[rowindex].Cells[0].Value.ToString(), out tempint);
                DicoInfoUserTroupeaux[mCurrentid].ID = tempint;
                DicoInfoUserTroupeaux[mCurrentid].Groupe = DataGridView.Rows[rowindex].Cells[1].Value.ToString();
                DicoInfoUserTroupeaux[mCurrentid].Nom = DataGridView.Rows[rowindex].Cells[2].Value.ToString();
                DicoInfoUserTroupeaux[mCurrentid].ValInventaire = DataGridView.Rows[rowindex].Cells[3].Value.ToString();
                double.TryParse(DataGridView.Rows[rowindex].Cells[4].Value.ToString(),out tempdouble);
                DicoInfoUserTroupeaux[mCurrentid].Price = tempdouble;
                DicoInfoUserTroupeaux[mCurrentid].TVA = DataGridView.Rows[rowindex].Cells[5].Value.ToString();
                DicoInfoUserTroupeaux[mCurrentid].Origine = DataGridView.Rows[rowindex].Cells[6].Value.ToString();
                DicoInfoUserTroupeaux[mCurrentid].data1 = DataGridView.Rows[rowindex].Cells[7].Value.ToString();
                double.TryParse(DataGridView.Rows[rowindex].Cells[8].Value.ToString(), out tempdouble);
                DicoInfoUserTroupeaux[mCurrentid].percent1 = tempdouble;
                DicoInfoUserTroupeaux[mCurrentid].data2 = DataGridView.Rows[rowindex].Cells[9].Value.ToString();
                double.TryParse(DataGridView.Rows[rowindex].Cells[10].Value.ToString(), out tempdouble);
                DicoInfoUserTroupeaux[mCurrentid].percent2 = tempdouble;
                DicoInfoUserTroupeaux[mCurrentid].data3 = DataGridView.Rows[rowindex].Cells[11].Value.ToString();
                double.TryParse(DataGridView.Rows[rowindex].Cells[12].Value.ToString(), out tempdouble);
                DicoInfoUserTroupeaux[mCurrentid].percent3 = tempdouble;
                DicoInfoUserTroupeaux[mCurrentid].data4 = DataGridView.Rows[rowindex].Cells[13].Value.ToString();
                double.TryParse(DataGridView.Rows[rowindex].Cells[14].Value.ToString(), out tempdouble);
                DicoInfoUserTroupeaux[mCurrentid].percent4 = tempdouble;

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
                foreach (int key in DicoInfoUserTroupeaux.Keys)
                {
                    // if the unity Ate is not set, then none of this ID has been modified
                    // so we dont have any info to add/insert 

                    string query;
                    string[] reader;
                    // if the groupe existe we dont need to create it
                    query = SQLQueryBuilder.SelectGroupeQuery(DicoInfoUserTroupeaux[key].Groupe);
                    if (Commun.ListHasValue(SQlQueryExecuter.RunQueryReaderStr("IdDefCateg", query)))
                        DicoInfoUserTroupeaux[key].GroupeExiste = true;
                    else DicoInfoUserTroupeaux[key].GroupeExiste = false;


                    //  looking for ID groupe
                    DicoInfoUserTroupeaux[key].IdDefCateg = SQLQueryBuilder.FindId("Def_Categ", "IdDefCateg", "Nom", DicoInfoUserTroupeaux[key].Groupe);

                    //on cherche s'il s'agit d'une modification
                    if (key > Commun.GetSeqId("Def_Bestiaux"))
                    {
                        DicoInfoUserTroupeaux[key].Modification = false;
                    }
                    else if (DicoInfoUserTroupeaux[key].OldValueInCell != null)
                    {
                        // looking for the old name, if it has a value then it has been modified
                        if (DicoInfoUserTroupeaux[key].OldValueInCell[1] == "")
                        {
                            DicoInfoUserTroupeaux[key].Modification = false;
                            DicoInfoUserTroupeaux[key].AddtoDefBestiauxTable = true;
                        }
                        else
                        {
                            DicoInfoUserTroupeaux[key].Modification = true;
                            DicoInfoUserTroupeaux[key].ID = key;
                        }
                    }

                    //  looking for ID TVA
                    DicoInfoUserTroupeaux[key].IdTVA = SQLQueryBuilder.FindId("TVA", "IdTva", "Nom", DicoInfoUserTroupeaux[key].TVA);

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
                mTroupeauxTable.Clear();
                adapter.Fill(mTroupeauxTable);
                DataGridView.DataSource = mTroupeauxTable;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
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
                mainQuery = TroupeauxQuery.MainQuery();
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
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
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
                    switch (DataGridView.CurrentCell.ColumnIndex)
                    {
                        case 1: // groupe product
                            addItems(DataCollection, 1);
                            break;
                        case 5://TVA
                            addItems(DataCollection, 5);
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
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
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
                    case 1: // group
                        query = TroupeauxQuery.AutoCompleteQuery();
                        list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        break;
                    case 5: // TVA
                        query = SQLQueryBuilder.AutoCompleteQueryTVA();
                        list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        break;
                }

                foreach (string item in list)
                {
                    col.Add(item);
                }
                mDataSaved = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
}

        /// <summary>
        /// Retourne l'Id de la ligne envoyée en paramètre
        /// </summary>
        /// <param name="Rowindex"></param>
        /// <returns></returns>
        private int GetId(int Rowindex)
        {
            try
            { 
            int id = -1;
            try
            {
                    if (DataGridView.Rows[Rowindex].Cells[12].Value.ToString() != null)
                    {
                        int.TryParse(DataGridView.Rows[Rowindex].Cells[0].Value.ToString(), out id);
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                }
                return id;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return 0;
            }
        }

        /// <summary>
        /// return the last index in the table
        /// </summary>
        /// <returns></returns>
        private int GetLastIndex()
        {
            try
            { 
                if (mTroupeauxTable.Rows.Count > 0)
                {
                    int id;
                    int.TryParse(mTroupeauxTable.Rows[mTroupeauxTable.Rows.Count - 1].ItemArray[0].ToString(), out id);
                    return id;
                }
                else return 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return 0;
            }
        }

        private void SetFocusToNewRow()
        {
            
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
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ManageColor()
        {
            if (color1 != null && color2 != null)
            {
                Commun.Setbackground(DataGridView, color1, color2);
                string query = SQLQueryBuilder.UpdateColorQuery(color1, color2,color3);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
            }
        }

        internal void EnableGroupe(object sender, EventArgs e)
        {
            try
            { 
                if (mbuttongroup.Text.Contains("Enable"))
                {
                    grouper.DataGridView = DataGridView;
                    grouper.SetGroupOn(DataGridView.Columns[1]);
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
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }



        internal void grouper_DisplayGroup(object sender, Subro.Controls.GroupDisplayEventArgs e)
        {
            e.BackColor = (e.Group.GroupIndex % 2) == 0 ? color1 : color2;
        }

        internal void dataGridView1_Sorted(object sender, EventArgs e)
        {
            ManageColor();
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
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

    }
}
