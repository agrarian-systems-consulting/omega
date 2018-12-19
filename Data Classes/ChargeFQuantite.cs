using OMEGA.Forms;
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
    class ChargeFQuantite
    {

        #region variables Globales
        private DataTable mChargeTable = new DataTable("Charges");
        private string mainQuery;
        private Color color1;
        private Color color2;
        private Color color3;
        private PictureBox pictureBox;
        internal bool mDataSaved = false;
        private int mCurrentid;
        private Button mbuttonOk;
        internal Form chargeForm;
        private Button mbuttonDuplicate;
        private Button mbuttonRemove;
        private Button mbuttonSave;
        private Button mbuttonCancel;
        private bool showProposals = true;
        private Button mbuttongroup;
        private Subro.Controls.DataGridViewGrouper grouper = new Subro.Controls.DataGridViewGrouper();
        private int mIdAct;
        private int mType;
        private string mType2;
        private int langue = Properties.Settings.Default.Langue;
        private int columnIndex;
        #endregion

        /// <summary>
        /// Datagridview of the inputs
        /// </summary>
        internal DataGridView DataGridView { get; set; }


        /// <summary>
        /// Cbnstructeur d'une charge en fonction d'un produit
        /// </summary>
        /// <param name="mygrid"></param>
        /// <param name="buttonok"></param>
        /// <param name="buttonsave"></param>
        /// <param name="buttoncancel"></param>
        /// <param name="buttonRemove"></param>
        /// <param name="buttonDuplicate"></param>
        /// <param name="type"></param>
        /// <param name="listId"></param>
        /// <param name="idAct"></param>
        internal ChargeFQuantite(DataGridView mygrid, Button buttonok, Button buttonsave,
           Button buttoncancel, Button buttonRemove, Button buttonDuplicate, int type,
           Button bttngroup, List<int> listId = null, int idAct = 0, PictureBox ptcbox = null, Form form = null,
           string type2 = "")
        {
            try
            {
                mbuttongroup = bttngroup;
                mbuttonOk = buttonok;
                mbuttonDuplicate = buttonDuplicate;
                mbuttonRemove = buttonRemove;
                mbuttonSave = buttonsave;
                mbuttonCancel = buttoncancel;
                DataGridView = mygrid;
                mType = type;
                pictureBox = ptcbox;
                mType2 = type2;

                Size newSize = new Size(543, 603);
                //this.Size = newSize;
                if (mType2 == "" || mType2 == "annuelle")
                {
                    mainQuery = ChargeQuery.MainQuery(listId, mType, idAct);
                }
                if (mType2 == "perenne")
                {
                    mainQuery = ChargeQuery.MainQuery2(listId, mType, idAct);
                }
                if (mType2 == "pluriannuelle")
                {
                    mainQuery = ChargeQuery.MainQuery3(listId, mType, idAct);
                }
                mIdAct = idAct;
                chargeForm = form;
                if (File.Exists(Properties.Settings.Default.FichierTraduction))
                {
                    SetCaption();
                }
                LoadGridViewData();
                DataCheck();
                RenameColumnHeader();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        /// <summary>
        /// Translate the forms feature
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
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Reanme the columns
        /// </summary>
        private void RenameColumnHeader()
        {
            int langue = Properties.Settings.Default.Langue;
            DataGridView.Columns[2].HeaderText = Translation.Translate("Product", langue);
            DataGridView.Columns[3].HeaderText = Translation.Translate("Unit activity", langue);
            DataGridView.Columns[4].HeaderText = Translation.Translate("Cost", langue);
            DataGridView.Columns[5].HeaderText = Translation.Translate("Unit activity", langue);
            DataGridView.ColumnHeadersHeight = 20;
        }

        /// <summary>
        /// Load Data on the gridView
        /// </summary>
        private void LoadGridViewData()
        {
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mChargeTable);
                //mChargeTable = FilterTable(mChargeTable);
                DataGridView.DataSource = mChargeTable;
                SetViewProperty();
                SetUnitColumn();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /// <summary>
        /// Adjust the view features
        /// </summary>
        private void SetViewProperty()
        {
            try
            {
                DataGridView.Columns[2].ReadOnly = true;
                DataGridView.Columns[3].ReadOnly = true;
                DataGridView.Columns[4].ReadOnly = true;
                DataGridView.Columns[5].ReadOnly = true;
                DataGridView.Columns[0].Visible = false;
                DataGridView.Columns[1].Visible = false;
                DataGridView.RowHeadersWidth = 21;
                DataGridView.ColumnHeadersHeight = 25;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }


        private void SetUnitColumn()
        {
            try
            {
                string query = "";
                foreach (DataGridViewRow row in DataGridView.Rows)
                {
                    int.TryParse(row.Cells[0].Value.ToString(), out int idcharge);
                    query = "Select UAte From SystemeUnite " +
                      " Join Charges on Charges.IdSysUnit = SystemeUnite.IdSysUnit" +
                      " Where Charges.ID = '" + idcharge + "';'";
                    List<string> list = SQlQueryExecuter.RunQueryReaderStr("UAte", query);
                    if (Commun.ListHasValue(list)) row.Cells[5].Value = list[0];

                    int.TryParse(row.Cells[1].Value.ToString(), out int idproduit);
                    query = "Select UAte From SystemeUnite " +
                        "Join Produits on Produits.IdSysUnit = SystemeUnite.IdSysUnit" +
                        " Where Produits.Id = '" + idproduit + "';'";
                     list = SQlQueryExecuter.RunQueryReaderStr("UAte", query);
                    if (Commun.ListHasValue(list)) row.Cells[3].Value = list[0];
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Save the data modified by the user
        /// </summary>
        private void SaveData()
        {
            if (mDataSaved == false)
            {
                try
                {
                    string query = "";
                  
                    // onsupprime tous
                    foreach (DataGridViewRow row in DataGridView.Rows)
                    {
                        query = "Delete From Caract_Activite Where IdActivite = '" + mIdAct + "' AND IdCharges = '" + row.Cells[0].Value.ToString()
                            + "' AND IdProduits = '" + row.Cells[1].Value.ToString() +"';";
                        SQlQueryExecuter.RunQuery(query);
                    }

                    // on insert tout dans caract_Activite
                    foreach (DataGridViewRow row in DataGridView.Rows)
                    {
                         query = ChargeQuery.InsertChargeInCaract_ActQuery(row, mIdAct);
                         SQlQueryExecuter.RunQuery(query);
                    }

                    // on insert dans Quantite
                    

                    foreach (DataGridViewRow row in DataGridView.Rows)
                    {
                        // l'utilisateur ajout une charge
                        //if (item.Value.Modification == false)
                        //{
                        int.TryParse(row.Cells[0].Value.ToString(), out int id);
                        if (mType2 == "" || mType2 == "annuelle")
                        {        
                            query = "Select ID From Charge_Quantite Where IdCharges = '" + id +
                                    "' AND IdActivite = '" + mIdAct + "';";
                            List<string> list = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                            if (Commun.ListHasValue(list))
                            {
                                query = ChargeQuery.UpdateQuantiteQuery(row, mIdAct);
                            }
                            else
                            {
                                query = ChargeQuery.AddQuantiteQuery(row, mIdAct);
                            }
                        }
                        if (mType2 == "perenne")
                        {
                            query = "Select ID From Charge_Quantite Where IdCharges = '" + id +
                                    "' AND IdActivite = '" + mIdAct + "';";
                            List<string> list = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                            if (Commun.ListHasValue(list))
                            {
                                query = ChargeQuery.UpdatePerenneQuery(row, mIdAct, GetPhaseList());
                            }
                            else
                            {
                                query = ChargeQuery.AddPerenneQuery(row, mIdAct, GetPhaseList());
                            }
                        }
                        if (mType2 == "pluriannuelle")
                        {
                            query = "Select ID From Charge_Pluriannuelle Where IdCharges = '" + id +
                                    "' AND IdActivite = '" + mIdAct + "';";
                            List<string> list = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                            if (Commun.ListHasValue(list))
                            {
                                query = ChargeQuery.UpdatePluriannuelleQuery(row, mIdAct, GetPhaseList());
                            }
                            else
                            {
                                query = ChargeQuery.AddPluriannuelleQuery(row, mIdAct, GetPhaseList());
                            }
                        }
                        SQlQueryExecuter.RunQuery(query);
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                }
            }
        }

        /// <summary>
        /// Event that fires when the user wants to add a new line
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
                    if (i == 0) rowvalue[i] = (GetLastIndex() + 1).ToString();
                    else rowvalue[i] = null;
                }
                mChargeTable.Rows.Add(rowvalue);
                // the ID of the dico might be the same than in the table or not
                // it's not a big deal if not as long as we insert the new record,
                // the final ID on the table does not matter.
              
                mCurrentid = GetLastIndex();
                SetScrollBarPosition();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Event that fires when the user wants to save the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonSaveData(object sender, EventArgs e)
        {
            try
            {
                SaveData();
                DataCheck();
                ResetMainQuery("Activite");
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                mChargeTable.Clear();
                adapter.Fill(mChargeTable);
                DataGridView.DataSource = mChargeTable;
                DataGridView.Refresh();
                ManageColor();
                mDataSaved = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Event that fires when the user wants remove a line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonRemoveRow(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection UserSelectedRow = DataGridView.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    DataGridView.Rows.RemoveAt(index);
                }
                if (!chargeForm.Text.Contains("*"))
                {
                    chargeForm.Text = chargeForm.Text + "*";
                }
                mDataSaved = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }
             
        /// <summary>
        /// Event that fires when the user wants cancel and close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void buttonCancel(object sender, EventArgs e)
        {
            DialogResult dialogResult = new DialogResult();
            if (mDataSaved == false)
            {
                dialogResult = MessageBox.Show(Translation.Translate("Data has been modified but not saved, do you really want to exit ?", langue), "Warning", MessageBoxButtons.YesNo);
            }
            if (dialogResult == DialogResult.No)
            {
                SaveData();
            }
            try
            {
                ChargesForm chargesForm = (ChargesForm)sender;
                chargesForm.Close();
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
            }

        }

        /// <summary>
        /// Event that fires when the user clicks on a row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (showProposals) FillAutolistAfterClick(e, e.ColumnIndex);
                if (mCurrentid <= 0) mCurrentid = GetId(e.RowIndex);
          
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user click on a cell to add data.
        /// It helps the user to fill the cell if he/she wants to add the same value
        /// </summary>
        /// <param name="e"></param>
        /// <param name="column"></param>
        private void FillAutolistAfterClick(DataGridViewCellEventArgs e, int column)
        {
            try
            {
                double decalage = 1.2;
                List<string> list = new List<string>();
                string query = "";
                ContextMenu contextmenu = new ContextMenu();
                List<MenuItem> ListItem = new List<MenuItem>();
                list = switchfunction(column);
                columnIndex = column;
                if (list.Count == 0) return;
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

        private List<string> switchfunction(int NumCol)
        {
            List<string> list = new List<string>();
            string query = "";
            switch (NumCol)
            {
                case 2: //product
                    query = "Select Nom From Produits;";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    break;
                case 4: //charge
                    query = "Select Nom from Charges;";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    break;
            }
            return list;
        }

        /// <summary>
        /// Occurs when the user click on an item on the autofill list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Click(object sender, EventArgs e)
        {
            try
            {
                string[] array = sender.ToString().Split(new string[] { "Text:" }, StringSplitOptions.None);
                DataGridView.CurrentCell.Value = array[1].Trim();
                string table ="";
                if (columnIndex == 2)
                {
                    table = "Produits";
                    columnIndex = 3;
                }
                else
                {
                    table = "Charges";
                    columnIndex = 5;
                }
                int idProd = SQLQueryBuilder.FindId(table, "ID", "Nom", array[1].Trim());
                if (columnIndex == 3) DataGridView.CurrentRow.Cells[1].Value = idProd;
                if (columnIndex == 5) DataGridView.CurrentRow.Cells[0].Value = idProd;
                string query = "Select Uate From SystemeUnite Join "+ table + " On "+ table + ".IdSysUnit = SystemeUnite.IdSysUnit " +
                    " WHERE "+table+".Id = '" + idProd + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("UAte", query);
                if (Commun.ListHasValue(list)) DataGridView.CurrentRow.Cells[columnIndex].Value = list[0];

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// occurs when the user finished to edit a cell (when he/she leaves the cell after editing it)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridView1CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {

                    //DialogResult dialogResult = new DialogResult();
                    //if (Commun.NameExists(DataGridView.Rows[e.RowIndex].Cells[1].Value.ToString(), "Charges", "Nom"))
                    //{
                    //    dialogResult = MessageBox.Show(Translation.Translate("An item with this name already exists. It may generate some errors on reports or calculs." +
                    //        " Do you want to continue ?", langue), "Warning", MessageBoxButtons.YesNo);
                    //    if (dialogResult == DialogResult.No)
                    //    {
                    //        DataGridView.Rows[e.RowIndex].Cells[1].Value = "";
                    //        return;
                    //    }
                    //}
                }
                // the parameters "true" tells that the user edited a cell, so we have the save it
          
                if (e.ColumnIndex == 0)
                {
                    ManageColor();
                    SetFocusToNewRow();
                    DataGridView.Refresh();
                }
                DataGridView.Refresh();
                if (!chargeForm.Text.Contains("*"))
                {
                    chargeForm.Text = chargeForm.Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            mDataSaved = false;
        }

        /// <summary>
        /// Occurs after a value changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridViewCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataGridView.Refresh();
                if (!chargeForm.Text.Contains("*"))
                {
                    chargeForm.Text = chargeForm.Text + "*";
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user sort the column of th gridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridView1_Sorted(object sender, EventArgs e)
        {
            ManageColor();
        }

        /// <summary>
        /// Occurs When the button proposal is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //internal void ButtonPropositionClick(object sender, EventArgs e)
        //{
        //    int langue = Properties.Settings.Default.Langue;
        //    if (mbuttonProposition.Text.Contains(Translation.Translate("Enable", langue)))
        //    {
        //        showProposals = true;
        //        mbuttonProposition.Text = Translation.Translate("Disable autofill proposal", langue);
        //    }
        //    else if (mbuttonProposition.Text.Contains(Translation.Translate("Disable", langue)))
        //    {
        //        showProposals = false;
        //        mbuttonProposition.Text = Translation.Translate("Enable autofill proposal", langue);
        //    }
        //}

        /// <summary>
        /// Set the scroll bar to put the current line in the visual area of the gridview.
        /// Not very efficient but helps the user to find the value he/she added.
        /// </summary>
        private void SetScrollBarPosition()
        {
            try
            {
                if (DataGridView.SortOrder == SortOrder.Ascending)
                {
                    DataGridView.FirstDisplayedScrollingRowIndex = 0;
                }
                else
                {
                    DataGridView.FirstDisplayedScrollingRowIndex = mChargeTable.Rows.Count / 2;
                }
            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// Event that fires when the user clicks on the export button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ExportTable(object sender, EventArgs e)
        {
            Export.RunExportTable(mChargeTable);
        }

        /// <summary>
        ///  Event that fires when the user selected an item on the list
        ///  It's added to the GridView.
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="tab"></param>
        /// <param name="dvg"></param>
        internal void AddSelectedCharge(int ID, string tab, DataGridView dvg)
        {
            try
            {
                string query = "";
                string[] rowvalue = null;

                if (mType2 == "" || mType2 == "annuelle")
                {
                    if (tab == "Charges")
                    {
                        query = ChargeQuery.AddSelectedChargeQuery(ID,mIdAct);
                        rowvalue = SQlQueryExecuter.RunQueryReader(query);
                        mChargeTable.Rows.Add(rowvalue);
                    }
                    if (tab == "TabChargesPied")
                    {
                        query = ChargeQuery.AddSelectedChargeQuery2(ID);
                        rowvalue = SQlQueryExecuter.RunQueryReader(query);
                        dvg.Rows.Add(rowvalue);
                    }
                }
                if (mType2 == "perenne")
                {
                    query = ChargeQuery.AddSelectedChargeQuery2(ID, mIdAct);
                    rowvalue = SQlQueryExecuter.RunQueryReader(query);
                    mChargeTable.Rows.Add(rowvalue);
                }
                if (mType2 == "pluriannuelle")
                {
                    query = ChargeQuery.AddSelectedChargeQuery3(ID, mIdAct);
                    rowvalue = SQlQueryExecuter.RunQueryReader(query);
                    mChargeTable.Rows.Add(rowvalue);
                }

                mDataSaved = false;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        /// <summary>
        /// Refresh the view to see all the modification made by the user
        /// </summary>
        /// <param name="table"></param>
        internal void RefreshView(string table)
        {
            try
            {
                ResetMainQuery(table);
                DataCheck();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                mChargeTable.Clear();
                adapter.Fill(mChargeTable);
                DataGridView.DataSource = mChargeTable;
                DataGridView.Refresh();
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }

        }

        /// <summary>
        /// Reset the main query to Update user value in the datatable
        /// </summary>
        /// <param name="table"></param>
        private void ResetMainQuery(string table)
        {
            try
            {
                List<int> listId = SQLQueryBuilder.GetListID(table, "Charges", mIdAct);

                if (mType2 == "" || mType2 == "annuelle")
                {
                    mainQuery = ChargeQuery.MainQuery(listId, mType, mIdAct);
                }
                if (mType2 == "perenne")
                {
                    mainQuery = ChargeQuery.MainQuery2(listId, mType, mIdAct);
                }
                if (mType2 == "pluriannuelle")
                {
                    mainQuery = ChargeQuery.MainQuery3(listId, mType, mIdAct);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }


        internal List<int> GetPhaseList()
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in DataGridView.Rows)
            {
                //if (row.Cells[1].Value.ToString() == item.Nom)
                //{
                    foreach (DataGridViewColumn column in DataGridView.Columns)
                    {
                        if (column.Name.Contains("Ph"))
                        {
                            int.TryParse(row.Cells[column.Index].Value.ToString(), out int temp);
                            list.Add(temp);
                        }
                    }
                //}
            }
            return list;
        }
           
        /// <summary>
        /// Check the data of the Quantite table
        /// </summary>
        private void DataCheck()
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Charges", "ID");
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                List<int> list2 = new List<int>();
                foreach (int index in list)
                {
                    if (mType2 == "" || mType2 == "annuelle")
                    {

                        query = "Select ID From Charge_Quantite Where IdCharges = '" + index + "';";
                        list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (list2.Count == 0)
                        {
                            query = "Insert Into Charge_Quantite (IDCharges,Quantite_Avant_1,Quantite_1)" +
                                "VALUES ('" + index + "','0','0');";
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                    if (mType2 == "perenne")
                    {
                        query = "Select ID From Charge_Perenne Where IdCharges = '" + index + "';";
                        list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (list2.Count == 0)
                        {
                            query = "Insert Into Charge_Perenne (IdCharges)" +
                                "VALUES ('" + index + "');";
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                    if (mType2 == "pluriannuelle")
                    {
                        query = "Select ID From Charge_Pluriannuelle Where IDCharges = '" + index + "';";
                        list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (list2.Count == 0)
                        {
                            query = "Insert Into Charge_Pluriannuelle (IdCharges)" +
                                "VALUES ('" + index + "');";
                            SQlQueryExecuter.RunQuery(query);
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
                if (mType2 == "" || mType2 == "annuelle")
                {
                    if (autoText != null)
                    {
                        switch (DataGridView.CurrentCell.ColumnIndex)
                        {
                            case 0: // groupe charge
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
                            case 6://TVA
                                addItems(DataCollection, 6);
                                break;
                            default:
                                autoText.AutoCompleteMode = AutoCompleteMode.None;
                                return;
                        }
                    }
                }
                if (mType2 == "perenne" || mType2 == "pluriannuelle")
                {
                    if (autoText != null)
                    {
                        switch (DataGridView.CurrentCell.ColumnIndex)
                        {
                            case 0: // groupe charge
                                addItems(DataCollection, 0);
                                break;
                            case 2: // unité Atelier
                                addItems(DataCollection, 2);
                                break;
                            case 4: // TVA
                                addItems(DataCollection, 6);
                                break;
                        }
                    }
                    autoText.AutoCompleteCustomSource = DataCollection;
                }
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
                    case 0: // groupe charge
                        query = ChargeQuery.AutoCompleteQuery();
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
                    case 6: // TVA
                        query = SQLQueryBuilder.AutoCompleteQueryTVA();
                        list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
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
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
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
        /// Retourne l'Id de la ligne envoyée en paramètre
        /// </summary>
        /// <param name="Rowindex"></param>
        /// <returns></returns>
        private int GetId(int Rowindex)
        {
            int id = -1;
            try
            {
                int index = 0;
                foreach (DataGridViewColumn column in DataGridView.Columns)
                {
                    if (column.Name == "ID")
                    {
                        int.TryParse(DataGridView.Rows[Rowindex].Cells[index].Value.ToString(), out id);
                    }
                    index++;
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
            if (mChargeTable.Rows.Count > 0)
            {
                int id;
                int.TryParse(mChargeTable.Rows[mChargeTable.Rows.Count - 1].ItemArray[0].ToString(), out id);
                return id;
            }
            else return 0;
        }

        private void SetFocusToNewRow()
        {
            /*try
            {
                if (DataGridView.CurrentCell.ColumnIndex == 0)
                {
                    for (int i = 0; i <= DataGridView.Rows.Count - 1; i++)
                    {
                        if (DataGridView.Rows[i].Cells[1].Value.ToString() == "")
                        {
                            DataGridView.CurrentCell = DataGridView.Rows[i].Cells[0];
                            DataGridView.CurrentCell.Selected = true;
                            DataGridView.FirstDisplayedScrollingRowIndex = i;
                            break;
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }*/
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
        /// Set the color of the gridview
        /// </summary>
        private void ManageColor()
        {
            try
            {
                if (color1 != null && color2 != null)
                {
                    Commun.Setbackground(DataGridView, color1, color2);
                    string query = SQLQueryBuilder.UpdateColorQuery(color1, color2, color3);
                    if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Group the data by group 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void EnableGroupe(object sender, EventArgs e)
        {
            try
            {
                if (mbuttongroup.Text.Contains(Translation.Translate("Enable", langue)))
                {
                    grouper.DataGridView = DataGridView;
                    grouper.SetGroupOn(DataGridView.Columns[0]);
                    grouper.DisplayGroup += grouper_DisplayGroup;
                    grouper.CollapseAll();
                    mbuttongroup.Text = Translation.Translate("Disable group", langue); ;
                    return;
                }
                if (mbuttongroup.Text.Contains(Translation.Translate("Disable", langue)))
                {
                    grouper.DataGridView = DataGridView;
                    grouper.RemoveGrouping();
                    grouper.DisplayGroup -= grouper_DisplayGroup;
                    mbuttongroup.Text = Translation.Translate("Enable Groupe", langue);
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

        /// <summary>
        /// Set the color for the group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void grouper_DisplayGroup(object sender, Subro.Controls.GroupDisplayEventArgs e)
        {
            e.BackColor = (e.Group.GroupIndex % 2) == 0 ? color1 : color2;
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

            }
        }

        /// <summary>
        /// Retourne le numéro de colonne de l'ID
        /// </summary>
        /// <returns></returns>
        private int GetNumColonneId()
        {
            int id = 0;
            try
            {
              id = 0; 
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }

        /// <summary>
        /// refresh the view when the user change the size of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SizeChanged(object sender, EventArgs e)
        {
            RefreshView("");
        }
    }
}

