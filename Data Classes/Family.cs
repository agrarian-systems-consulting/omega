using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using Subro.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Data_Classes
{
    /// <summary>
    /// cette classe représente la famille
    /// </summary>
    internal class Family
    {

        #region variables Globales
        private DataTable FamilyTable = new DataTable("Def_Calendrier");
        private string mainQuery;
        private Boolean mDataSaved = false;
        private PictureBox pictureBox;
        private Color color1;
        private int langue = Properties.Settings.Default.Langue;
        private Color color2;
        private Color color3;
        private bool showProposals;
        private Button mbuttonProposition;
        private Button mbuttongroup;
        private Subro.Controls.DataGridViewGrouper grouper = new Subro.Controls.DataGridViewGrouper();
        private Button mbuttonAdd;
        private Button mbuttonOk;
        private Button mbuttonDuplicate;
        private Button mbuttonRemove;
        private Button mbuttonSave;
        private Button mbuttonCancel;
        internal DataGridView mDataGridView;
        private int mCurrentid;
        internal List<int> ListeRecordToDelete { get; set; }
        internal Subro.Controls.DataGridViewGrouper Grouper
        {
            get { return grouper; }
            set { grouper = value; }
        }
       

        private int mIdExpl;
        #endregion

        internal Family(DataGridView dgv, Button buttonadd,Button buttonremove, Button buttonduplicate, Button buttonOK,
            Button buttonsave, Panel panel,Button buttoncancel,List<int> listId,Button bttngroup, Button buttonPropo,
            int idExpl,PictureBox ptcbox)
        {
            mbuttonProposition = buttonPropo;
            mbuttongroup = bttngroup;
            mbuttonAdd = buttonadd;
            mbuttonOk = buttonOK;
            pictureBox = ptcbox;
            mbuttonDuplicate = buttonduplicate;
            mbuttonRemove = buttonremove;
            mbuttonSave = buttonsave;
            mbuttonCancel = buttoncancel;
            mDataGridView = dgv;
            mIdExpl = idExpl;
            mainQuery = FamilyQuery.MainQuery(listId, mIdExpl);
            ListeRecordToDelete = new List<int>();
            LoadDataGridView();
            SetCaption();
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
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadDataGridView()
        {
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(FamilyTable);
                mDataGridView.DataSource = FamilyTable;
                //mdataGridView1.Columns[0].Visible = false;
                mDataGridView.Columns[0].Visible = false;
                SetComboBoxColumn();
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        internal void ButtonAddClick(object sender, EventArgs e)
        {
            try
            {
                string[] ValuetoAdd = new string[7];
                {
                    for (int i = 0; i <= mDataGridView.ColumnCount - 1; i++)
                    {
                        if (i == 0) ValuetoAdd[i] = (GetLastIndex() + 1).ToString();
                        else if (i == 4) ValuetoAdd[i] = "0";
                        else if (i == 6) ValuetoAdd[i] =GetExploitationName();
                        else ValuetoAdd[i] = null;
                    }
                    FamilyTable.Rows.Add(ValuetoAdd);
                    mDataSaved = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private string GetExploitationName()
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Exploitation", "Nom", "Where ID = '" + mIdExpl + "'");
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                if (Commun.ListHasValue(list))
                {
                    return list[0];
                }
                else return "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return "";
            }
           
        }

        internal void AddSelectedFamilyt(int ID, string currentTab, DataGridView currentGridView)
        {
            try
            {
                string query = FamilyQuery.AddSelectedFamilyQuery(ID);
                string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
                FamilyTable.Rows.Add(rowvalue);
               
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        internal void ButtonRemoveClick (object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection UserSelectedRow = mDataGridView.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    ListeRecordToDelete.Add(GetId(index));
                    mDataGridView.Rows.RemoveAt(index);
                }

                mDataSaved = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ButtonExportClick (object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        internal void ButtonCancelClick(object sender , EventArgs e)
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
            }
        }

        internal void ButtonSaveClick(object sender, SaveDataEvent e)
        {
            try
            {
                SaveData(e.deleteOnTable);
                ResetMainQuery();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                FamilyTable.Clear();
                adapter.Fill(FamilyTable);
                mDataGridView.DataSource = FamilyTable;
                mDataGridView.Refresh();

                mDataSaved = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ButtonDuplicateClick(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
             }
        }

        private void SaveData(bool DeleteFromTable)
        {
            int IdFamille = 0;
            double Age = 0;
            string Nom = "";
            string Role = "";
            string Reponsable = "";
            string Sexe = "";
            string Expl = "";
            if (!mDataSaved)
            {
                try
                {
                    string query = "";

                    foreach (DataGridViewRow row in mDataGridView.Rows)
                    {
                        int.TryParse(row.Cells[0].ToString(), out IdFamille);
                        double.TryParse(row.Cells[4].Value.ToString(), out Age);
                        Nom = row.Cells[1].Value.ToString();
                        Reponsable = row.Cells[3].Value.ToString();
                        Role = row.Cells[2].Value.ToString();
                        Sexe = row.Cells[5].Value.ToString();
                        Expl = row.Cells[6].Value.ToString();

                        foreach (int index in ListeRecordToDelete)
                        {
                            query = SQLQueryBuilder.DeleteQuery("Family", "Where ID = '" + index + "' AND IdExploitation = '" + mIdExpl + "'");
                            SQlQueryExecuter.RunQuery(query);
                        }
                        ListeRecordToDelete.Clear();

                        if (IdFamille == 0)
                        {
                            IdFamille = SQLQueryBuilder.FindId("Family", "ID", "Nom = '" + Nom + "'AND IdExploitation = '" + mIdExpl + "'");
                        }
                        query = "Select * From Family Where ID = '" + IdFamille + "';";

                        List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (Commun.ListHasValue(list))
                        {
                            query = FamilyQuery.UpdateValueQuery(Nom,Role,Reponsable,mIdExpl,Age,Sexe ,IdFamille);
                            SQlQueryExecuter.RunQuery(query);
                        }
                        else
                        {
                            query = FamilyQuery.InsertValueQuery(Nom, Role, Reponsable, mIdExpl, Age, Sexe, IdFamille);
                            SQlQueryExecuter.RunQuery(query);
                            //IdFamille = SQLQueryBuilder.FindId("Family", "ID", "Nom", row.Cells[1].Value.ToString());
                        }
                    }

                    Properties.Settings.Default.DoitSauvegarger = true;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                }
            }
        }

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

                    case 1: //group
                        decalage = 1.5;
                        query = "Select Nom From Family;";
                        list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        break;
                    case 2: //unit activite
                        decalage = 1.2;
                        query = "Select Role From Family;";
                        list = SQlQueryExecuter.RunQueryReaderStr("UAte", query);
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

                mDataGridView.ContextMenu = contextmenu;
                Rectangle MyCell = mDataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                int x = (int)Math.Ceiling(MyCell.X * decalage);
                int y = (int)Math.Ceiling(MyCell.Y * decalage);
                Point point = new Point(x, y);
                contextmenu.Show(mDataGridView, point);

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

        private void Item_Click(object sender, EventArgs e)
        {
            try
            {
                string[] array = sender.ToString().Split(new string[] { "Text:" }, StringSplitOptions.None);
                mDataGridView.CurrentCell.Value = array[1].Trim();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }

        }

        /// <summary>
        /// Refresh the view to see all the modification made by the user
        /// </summary>
        /// <param name="table"></param>
        internal void RefreshView()
        {
            try
            {
                ResetMainQuery();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                FamilyTable.Clear();
                adapter.Fill(FamilyTable);
                mDataGridView.DataSource = FamilyTable;
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private void ResetMainQuery()
        {
            try
            {
                List<int> listId = FamilyQuery.GetListID(mIdExpl);
                mainQuery = FamilyQuery.MainQuery(listId, mIdExpl);
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
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
                // the parameters "true" tells that the user edited a cell, so we have the save it
                mDataSaved = false;
                mDataGridView.Refresh();
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
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
                if (showProposals) FillAutolistAfterClick(e, e.ColumnIndex);
                mCurrentid = GetId(e.RowIndex);
                if (e.ColumnIndex == 3)
                {
                    //lors d'un click droit on peu caché les colonnes inutiles
                    ContextMenu contextmenu = new ContextMenu();
                    string oui = Translation.Translate("Yes", langue);
                    string non = Translation.Translate("No", langue);
                    MenuItem itemoui = new MenuItem(oui);
                    MenuItem itemnon = new MenuItem(non);
                    contextmenu.MenuItems.Add(itemoui);
                    contextmenu.MenuItems.Add(itemnon);
                    mDataGridView.ContextMenu = contextmenu;
                    Rectangle MyCell = mDataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    Point point = new Point(MyCell.X, MyCell.Y);
                    contextmenu.Show(mDataGridView, point);
                    itemoui.Click += ItemOui_Click;
                    itemnon.Click += ItemNon_Click;
                }
                if (e.ColumnIndex == 5)
                {
                    //lors d'un click droit on peu caché les colonnes inutiles
                    ContextMenu contextmenu = new ContextMenu();
                    string m = Translation.Translate("M", langue);
                    string f = Translation.Translate("W", langue);
                    MenuItem itemM = new MenuItem(m);
                    MenuItem itemF = new MenuItem(f);
                    contextmenu.MenuItems.Add(itemM);
                    contextmenu.MenuItems.Add(itemF);
                    mDataGridView.ContextMenu = contextmenu;
                    Rectangle MyCell = mDataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    Point point = new Point(MyCell.X, MyCell.Y);
                    contextmenu.Show(mDataGridView, point);
                    itemM.Click += ItemM_Click;
                    itemF.Click += ItemF_Click;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

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
                    switch (mDataGridView.CurrentCell.ColumnIndex)
                    {
                        case 1: // nom
                            addItems(DataCollection, 1);
                            break;
                        case 2: // role
                            addItems(DataCollection, 2);
                            break;
                        case 5: // role
                            addItems(DataCollection, 3);
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
                case 1: // nom
                    query = FamilyQuery.AutoCompleteQuery("Nom");
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    break;
                case 2: // Role
                        list.Add(Translation.Translate("Yes", langue));
                        list.Add(Translation.Translate("No", langue));
                        break;
                 case 3: // Sexe
                     list.Add(Translation.Translate("W", langue));
                     list.Add(Translation.Translate("M", langue));
                    break;
                }

            foreach (string item in list)
            {
                col.Add(item);
            }
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
        /// return the last index in the table
        /// </summary>
        /// <returns></returns>
        private int GetLastIndex()
        {
            try
            { 
            if (FamilyTable.Rows.Count > 0)
            {
                    int id;
                int.TryParse(FamilyTable.Rows[FamilyTable.Rows.Count - 1].ItemArray[0].ToString(), out  id);
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
                if (mDataGridView.Rows[Rowindex].Cells[0].Value.ToString() != null)
                {
                    int.TryParse(mDataGridView.Rows[Rowindex].Cells[0].Value.ToString(), out id);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }
        private void SetFocusToNewRow()
        {
            try
            {
                if (mDataGridView.CurrentCell.ColumnIndex == 1)
                {
                    for (int i = 0; i <= mDataGridView.Rows.Count - 1; i++)
                    {
                        if (mDataGridView.Rows[i].Cells[2].Value.ToString() == "")
                        {
                            mDataGridView.CurrentCell = mDataGridView.Rows[i].Cells[1];
                            mDataGridView.CurrentCell.Selected = true;
                            mDataGridView.FirstDisplayedScrollingRowIndex = i;
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
                Commun.Setbackground(mDataGridView, color1, color2);
                string query = SQLQueryBuilder.UpdateColorQuery(color1, color2,color3);
                if (!(color1.Name == "0" && color2.Name == "0"))  SQlQueryExecuter.RunQuery(query);
            }
        }

        internal void EnableGroupe(object sender, EventArgs e)
        {
            try
            { 
                if (mbuttongroup.Text.Contains("Enable"))
                {
                    grouper.DataGridView = mDataGridView;
                    grouper.SetGroupOn(mDataGridView.Columns[0]);
                    grouper.DisplayGroup += grouper_DisplayGroup;
                    grouper.CollapseAll();
                    mbuttongroup.Text = "Disable Groupe";
                    return;
                }
                if (mbuttongroup.Text.Contains("Disable"))
                {
                    grouper.DataGridView = mDataGridView;
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
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
 
        private void ItemOui_Click(object sender, EventArgs e)
        {
            mDataGridView.CurrentCell.Value = "Oui";
        }

        private void ItemNon_Click(object sender, EventArgs e)
        {
            mDataGridView.CurrentCell.Value = "Non";
        }

        private void ItemM_Click(object sender, EventArgs e)
        {
            mDataGridView.CurrentCell.Value = Translation.Translate("M",langue);
        }

        private void ItemF_Click(object sender, EventArgs e)
        {
            mDataGridView.CurrentCell.Value = Translation.Translate("W", langue);
        }

        private void SetComboBoxColumn()
        {
            try
            {
                foreach (DataGridViewRow row in mDataGridView.Rows)
                {
                    if (mDataGridView.Rows[0].Cells[3].Value.ToString() == "1")
                    {
                        mDataGridView.Rows[0].Cells[3].Value = "oui";
                    }
                    else
                    {
                        mDataGridView.Rows[0].Cells[3].Value = "non";
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
}
