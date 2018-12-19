using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System.Windows.Forms;
using OMEGA.SQLQuery;
using System.Drawing;
using OMEGA.Forms;

namespace OMEGA.Data_Classes
{
    /// <summary>
    /// Cette classe représente les recettes/dépenses 
    /// </summary>
    class ExpenseIncome
    {
        #region Variables
        private DataGridView mDatagridView;
        private DataTable mIncomeExpenseTable = new DataTable("RecDep");
        private string mainQuery;
        private Boolean mDataSaved = false;
        private int langue = Properties.Settings.Default.Langue;
        private Button mbuttonOk;
        private Button mbuttonAdd;
        private Button mbuttonCancel;
        private Button mbuttonDuplicate;
        private Button mbuttonRemove;
        private Button mbuttonSave;
        private Color color1;
        private Color color2;
        private Color color3;
        private PictureBox pictureBox;
        private int mFamily;
        private int mExpense;
        private List<int> ListeRecordToDelete;
        private Form mExpIncForm;
        #endregion

        /// <summary>
        /// Constructor of expense/income
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="bttnok"></param>
        /// <param name="bttnCancel"></param>
        /// <param name="bttnDupli"></param>
        /// <param name="bttnremove"></param>
        /// <param name="bttnsave"></param>
        /// <param name="bttnadd"></param>
        /// <param name="expenseorincome"></param>
        /// <param name="FamilyorFarm"></param>
        /// <param name="pctbox"></param>
        internal ExpenseIncome(DataGridView dgv, Button bttnok, Button bttnCancel, 
            Button bttnDupli, Button bttnremove, Button bttnsave,
            Button bttnadd, int expenseorincome, int FamilyorFarm,
            PictureBox pctbox,Form ExpIncForm)
        {
            mDatagridView = dgv;
            mbuttonAdd = bttnadd;
            mbuttonOk = bttnok;
            pictureBox = pctbox;
            mbuttonRemove = bttnremove;
            mbuttonSave = bttnsave;
            mbuttonDuplicate = bttnDupli;
            mbuttonCancel = bttnCancel;
            mFamily = FamilyorFarm;
            mExpIncForm = ExpIncForm;
            mExpense = expenseorincome;
            ListeRecordToDelete = new List<int>();
            mainQuery = Expense_IncomeQuery.MainQuery(mFamily, mExpense);
            LoadGrid();
            FormName(mFamily, mExpense);
            SetColor();
            SetCaption();
        }

        private void FormName(int family,int expense)
        {
            if (family  == 1 && expense == 1)
            {
                mExpIncForm.Text = Translation.Translate("Family Expenses", langue);
            }
            if (family == 0 && expense == 1)
            {
                mExpIncForm.Text = Translation.Translate("Misc Expenses", langue);
            }
            if (family == 1 && expense == 0)
            {
                mExpIncForm.Text = Translation.Translate("Family Incomes", langue);
            }
            if (family == 0 && expense == 0)
            {
                mExpIncForm.Text = Translation.Translate("Misc Incomes", langue);
            }
        }

        /// <summary>
        /// Load the data on the gridView
        /// </summary>
        private void LoadGrid()
        {
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mIncomeExpenseTable);
                mDatagridView.DataSource = mIncomeExpenseTable;
                mDatagridView.Columns[0].Visible = false;
                mDatagridView.AllowUserToAddRows = false;
                mDatagridView.Columns[2].ReadOnly = true;
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

  
        /// <summary>
        /// Translate text
        /// </summary>
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
                mbuttonAdd.Text = Translation.Translate("Add", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        /// <summary>
        /// Occurs when the user add a line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonAddClick(object sender, EventArgs e)
        {
            try
            {
                // we add a new row in the table with a new ID
                string[] rowvalue = new string[mDatagridView.ColumnCount];
                for (int i = 0; i <= mDatagridView.ColumnCount - 1; i++)
                {
                    if (i == 0) rowvalue[i] = (GetLastIndex() + 1).ToString();
                    else rowvalue[i] = null;
                }
                mIncomeExpenseTable.Rows.Add(rowvalue);
                mDataSaved = false;
                ManageColor();
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ButtonCancelClick(object sender, EventArgs e)
        {
          
        }
        
        /// <summary>
        /// Duplicate the line selected by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonDuplicateClick(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection ListuserRow = mDatagridView.SelectedRows;
                string[] rowvalue = new string[10];
                if (ListuserRow.Count == 1)
                {
                    for (int j = 0; j <= ListuserRow.Count - 1; j++)
                    {
                        rowvalue = new string[mDatagridView.ColumnCount];
                        for (int i = 0; i <= mDatagridView.ColumnCount-1 ; i++)
                        {
                            rowvalue[i] = ListuserRow[j].Cells[i].Value.ToString();
                        }
                        mIncomeExpenseTable.Rows.Add(rowvalue);
                    }
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

        /// <summary>
        /// remove the lines selected by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonRemoveClick(object sender, EventArgs e)
        {
            try
            {
                mDatagridView.AllowUserToDeleteRows = true;
                DataGridViewSelectedRowCollection UserSelectedRow = mDatagridView.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    ListeRecordToDelete.Add(GetId(index));
                    mDatagridView.Rows.RemoveAt(index);
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
        /// Occurs when the user save the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonSaveClick(object sender, EventArgs e)
        {
            try
            {
                SaveData(true);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                mIncomeExpenseTable.Clear();
                adapter.Fill(mIncomeExpenseTable);
                mDatagridView.DataSource = mIncomeExpenseTable;
                mDatagridView.Refresh();

                mDataSaved = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user click on a cell, set the dico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                FillAutolistAfterClick(e, e.ColumnIndex);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
      

        /// <summary>
        /// Export the data on a .csv file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonExportClick(object sender, EventArgs e)
        {
            Export.RunExportTable(mIncomeExpenseTable); 
        }

        /// <summary>
        /// Occurs when the user click on OK
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonOkClick(object sender, EventArgs e)
        {
            ButtonSaveClick(sender, e);
            
        }

        /// <summary>
        /// Save the data 
        /// </summary>
        /// <param name="DeleteFromTable"></param>
        private void SaveData(bool DeleteFromTable)
        {
            if (mDataSaved == false)
            {
                try
                {
                    string query;
                    // DeleteGroupe();
                    if (DeleteFromTable)
                    {
                        foreach (int index in ListeRecordToDelete)
                        {
                            query = SQLQueryBuilder.DeleteQuery(index, "Expense_Income");
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }

                    foreach (DataGridViewRow row in mDatagridView.Rows)
                    {
                        query = "Select * From Expense_Income where Nom ='" + row.Cells[1].Value.ToString() +
                            "' AND Family ='" + mFamily + "' AND Expense ='" + mExpense + "';";

                        List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (Commun.ListHasValue(list))
                        {
                            query = Expense_IncomeQuery.UpdateQuery(row.Cells[1].Value.ToString(),SQLQueryBuilder.FindId("TVA","IdTVA","Nom", row.Cells[2].Value.ToString()),list[0]);
                            SQlQueryExecuter.RunQuery(query);
                        }
                        else
                        {
                            query = Expense_IncomeQuery.InsertQuery(mFamily, mExpense, row.Cells[1].Value.ToString(), SQLQueryBuilder.FindId("TVA", "IdTVA", "Nom", row.Cells[2].Value.ToString()));
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                    ManageColor();
                    Properties.Settings.Default.DoitSauvegarger = true;
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                }
            }
        }


        /// <summary>
        /// Help the user if he/she wants to add a group that already exists
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DataGridView1EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                TextBox autoText = e.Control as TextBox;
                autoText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                addItems(DataCollection);
               
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
        internal void addItems(AutoCompleteStringCollection col)
        {
            string query = ""; ;
            List<string> list = new List<string>();
            query = SQLQueryBuilder.AutoCompleteQueryTVA();
            list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
            foreach (string item in list)
            {
                col.Add(item);
            }
            mDataSaved = false;
            //if (!produitForm.Text.Contains("*"))
            //{
            //    produitForm.Text = produitForm.Text + "*";
            //}
        }

        /// <summary>
        /// Display a list that allows the user to select if he/she wants to put the same name
        /// </summary>
        /// <param name="e"></param>
        /// <param name="column"></param>
        private void FillAutolistAfterClick(DataGridViewCellEventArgs e, int column)
        {
            try
            {
                double decalage = 1;
                List<string> list = new List<string>();
                ContextMenu contextmenu = new ContextMenu();
                List<MenuItem> ListItem = new List<MenuItem>();
                string query = "Select Nom From TVA;";
                list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
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

                mDatagridView.ContextMenu = contextmenu;
                decalage = 1.15;
                Rectangle MyCell = mDatagridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                int x = (int)Math.Ceiling(MyCell.X * decalage);
                int y = (int)Math.Ceiling(MyCell.Y * decalage);
                Point point = new Point(x, y);
                contextmenu.Show(mDatagridView, point);

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
        /// Occurs when the user select a value on the auto list proposal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Click(object sender, EventArgs e)
        {
            try
            {
                string[] array = sender.ToString().Split(new string[] { "Text:" }, StringSplitOptions.None);
                mDatagridView.CurrentCell.Value = array[1].Trim();
                mDatagridView.Refresh();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
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
                if (mIncomeExpenseTable.Rows.Count > 0)
                {
                    int id;
                    int.TryParse(mIncomeExpenseTable.Rows[mIncomeExpenseTable.Rows.Count - 1].ItemArray[0].ToString(), out id);
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

        /// <summary>
        /// Return the id of the row index 
        /// </summary>
        /// <param name="Rowindex"></param>
        /// <returns></returns>
        private int GetId(int Rowindex)
        {
            int id = -1;
            try
            {
                if (mDatagridView.Rows[Rowindex].Cells[0].Value.ToString() != null)
                {
                    int.TryParse(mDatagridView.Rows[Rowindex].Cells[0].Value.ToString(), out id);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }

        internal void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
        }

        internal void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            pictureBox.BorderStyle = BorderStyle.None;
        }

        internal void buttonNotes_Click(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm(mExpIncForm.Text);
            form.ShowDialog();
        }

        internal void RenameColumnHeader()
        {
            try
            {
                mDatagridView.Columns[1].HeaderText = Translation.Translate("Name", langue);
                mDatagridView.Columns[2].HeaderText = Translation.Translate("VAT", langue);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Select the color in the database to put in the mDataGridView
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
            if (color1 != null && color2 != null)
            {
                Commun.Setbackground(mDatagridView, color1, color2);
                string query = SQLQueryBuilder.UpdateColorQuery(color1, color2, color3);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
            }
        }

    }

    
}
