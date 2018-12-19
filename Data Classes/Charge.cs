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
    /// <summary>
    /// Represents a Cost 
    /// </summary>
    internal class Charge
    {
        #region variables Globales
        private DataTable mChargeTable = new DataTable("Charges");
        private string mainQuery;
        private Color color1;
        private Color color2;
        private PictureBox pictureBox;
        internal bool mDataSaved = false;
        private string mCurrentTva;
        private Button mbuttonOk;
        internal Form chargeForm;
        private Button mbuttonDuplicate;
        private Button mbuttonRemove;
        private Button mbuttonSave;
        private Button mbuttonCancel;
        private bool showProposals = true;
        private Button mbuttonProposition;
        private Button mbuttongroup;
        private Subro.Controls.DataGridViewGrouper grouper = new Subro.Controls.DataGridViewGrouper();
        private int mIdAct;
        private int mType;
        private string mType2;
        private int langue = Properties.Settings.Default.Langue;
        #endregion

        /// <summary>
        /// Datagridview of the inputs
        /// </summary>
        internal DataGridView DataGridView { get; set; }

        /// <summary>
        /// List of record deleted by the user
        /// </summary>
        internal List<int> ListeRecordToDelete { get; set; }

        /// <summary>
        /// Constuction d'une charge
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
        internal Charge(DataGridView mygrid, Button buttonok, Button buttonsave,
           Button buttoncancel, Button buttonRemove, Button buttonDuplicate, Button buttonPropal, int type,
           Button bttngroup, List<int> listId = null, int idAct = 0, PictureBox ptcbox = null, Form form = null,
           string type2 = "")
        {
            try
            {
                mbuttongroup = bttngroup;
                mbuttonProposition = buttonPropal;
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
                if ( mType2 == "perenne")
                {
                    mainQuery = ChargeQuery.MainQuery2(listId, mType, idAct);
                }
                if ( mType2 == "pluriannuelle")
                {
                    mainQuery = ChargeQuery.MainQuery3(listId, mType, idAct);
                }
                ListeRecordToDelete = new List<int>();
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
                if (mbuttongroup != null) mbuttongroup.Text = Translation.Translate("Enable group", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Reanme the columns
        /// </summary>
        private void RenameColumnHeader()
        {
            int langue = Properties.Settings.Default.Langue;
            DataGridView.Columns[0].HeaderText = Translation.Translate("Group", langue);
            DataGridView.Columns[1].HeaderText = Translation.Translate("Name", langue);
            DataGridView.Columns[2].HeaderText = Translation.Translate("Unit activity", langue);
            DataGridView.Columns[3].HeaderText = Translation.Translate("Unit farm", langue);
            DataGridView.Columns[4].HeaderText = Translation.Translate("Unit global", langue);
            DataGridView.Columns[5].HeaderText = Translation.Translate("Price", langue);
            DataGridView.Columns[6].HeaderText = Translation.Translate("TVA", langue);
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
                mChargeTable = FilterTable(mChargeTable);
                DataGridView.DataSource = mChargeTable;
                SetViewProperty();
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
                if (mType2 == "" || mType2 == "annuelle")
                {
                    DataGridView.Columns[2].ReadOnly = true;
                    DataGridView.Columns[3].ReadOnly = true;

                    DataGridView.Columns[4].ReadOnly = true;

                    if (mIdAct == 0)
                    {
                        DataGridView.Columns[7].ReadOnly = false;
                        DataGridView.Columns[8].ReadOnly = false;
                        DataGridView.Columns[7].Visible = false;
                        DataGridView.Columns[8].Visible = false;
                    } 
                    DataGridView.Columns[6].ReadOnly = true;
                    DataGridView.Columns[9].Visible = false;
                    DataGridView.Columns[10].Visible = false;
                    DataGridView.Columns[11].Visible = false;
                    DataGridView.Columns[12].Visible = false;
                    
                }
                if (mType2 == "perenne" || mType2 == "pluriannuelle")
                {
                    DataGridView.Columns[2].ReadOnly = true;//Unité
                    DataGridView.Columns[4].ReadOnly = true;//TVA

                    DataGridView.Columns[GetNumColonneId() - 3].Visible = false;
                    DataGridView.Columns[GetNumColonneId() - 2].Visible = false;
                    DataGridView.Columns[GetNumColonneId() - 1].Visible = false;
                    DataGridView.Columns[GetNumColonneId()].Visible = false;
                }

                DataGridView.RowHeadersWidth = 21;
                DataGridView.ColumnHeadersHeight = 25;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }


        /// <summary>
        /// We need to filter data because of the 'quantity' and 'quantity before 1' columns.
        /// Since the quantities depend of the activity, for the same cost (Charge in french), with the 'join' clause
        /// on the mainqueyr to Load data, we see as many times the same cost as its present on the quantity table.
        /// </summary>
        /// <param name="chargeTable"></param>
        /// <returns>Datable filtred</returns>
        private DataTable FilterTable(DataTable chargeTable)
        {
            try
            {
                int rowindex = 0;
                List<DataRow> ListRowToDelete = new List<DataRow>();
                bool rowDelete = false;
                string id = "";
                string nextid = "";

                foreach (DataRow row in chargeTable.Rows)
                {
                    if (rowindex == chargeTable.Rows.Count - 1) break;
                    id = row.ItemArray[10].ToString();
                    nextid = chargeTable.Rows[rowindex + 1].ItemArray[10].ToString();
                    if (id == nextid)
                    {
                        //double qty;
                        //if (double.TryParse(row.ItemArray[11].ToString(), out qty) || double.TryParse(row.ItemArray[12].ToString(), out qty))
                        //{
                        //    ListRowToDelete.Add(chargeTable.Rows[rowindex + 1]);
                        //    rowDelete = true;
                        //}
                        //if ((double.TryParse(chargeTable.Rows[rowindex + 1].ItemArray[11].ToString(), out qty)
                        //   || double.TryParse(chargeTable.Rows[rowindex + 1].ItemArray[12].ToString(), out qty))
                        //   && !rowDelete)
                        //{
                        //    ListRowToDelete.Add(row);
                        //    rowDelete = true;
                        //}
                        if (!ListRowToDelete.Contains(chargeTable.Rows[rowindex + 1]))
                        {
                            ListRowToDelete.Add(chargeTable.Rows[rowindex + 1]);
                        }
                        rowDelete = false;
                    }
                    rowindex++;
                }
                foreach (DataRow row in ListRowToDelete)
                {
                    chargeTable.Rows.Remove(row);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return chargeTable;
        }

        /// <summary>
        /// Save the data modified by the user
        /// </summary>
        /// <param name="DeleteFromTable"></param>
        private void SaveData(bool DeleteFromTable)
        {
            
                try
                {
                    string query="";
                    int IdDefCateg = 0;
                    int IdTVA = 0;
                    int IdUnite = 0;
                    int IdCharge = 0;
                    string NomTVA = "";
                    string Nom = "";
                    string NomDefCateg = "";
                    string NomUAte = "";
                    string price = "";
                    foreach (int index in ListeRecordToDelete)
                    {
                        if (mIdAct != 0)
                        {
                            query = SQLQueryBuilder.DeleteQuery("Caract_Activite","Where IdCharges = '"+index+"' AND IdActivite = '"+mIdAct+"'");
                            SQlQueryExecuter.RunQuery(query);
                        }
                        else
                        {
                            query = SQLQueryBuilder.DeleteQuery(index, "Charges");
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                    ListeRecordToDelete.Clear();

                    foreach (DataGridViewRow row in DataGridView.Rows)
                    {
                        NomDefCateg = row.Cells[0].Value.ToString();
                        NomTVA = row.Cells[6].Value.ToString();
                        NomUAte = row.Cells[2].Value.ToString();
                        price = row.Cells[5].Value.ToString();
                        Nom = row.Cells[1].Value.ToString();
                        if (!GroupeExiste(NomDefCateg))
                        {
                            query = SQLQueryBuilder.AddNewgGroupeQuery(Nom, 2);
                            SQlQueryExecuter.RunQuery(query);
                        }
                        
                        IdDefCateg = SQLQueryBuilder.FindId("Def_Categ", "IdDefCateg", "Nom", NomDefCateg);
                        IdTVA = SQLQueryBuilder.FindId("TVA", "IdTVA", "Nom", NomTVA);
                        IdUnite = SQLQueryBuilder.FindId("SystemeUnite", "IdSysUnit", "UAte", NomUAte);
                        IdCharge = SQLQueryBuilder.FindId("Charges", "ID", "Nom", row.Cells[1].Value.ToString());
                        if (IdCharge == 0)
                        {
                        int.TryParse(row.Cells[GetNumColonneId()].Value.ToString(), out IdCharge);
                        }
                        query = "Select * From Charges Where ID = '" + IdCharge + "';";

                        List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (Commun.ListHasValue(list))
                        {
                            query = ChargeQuery.UpdateValueQuery(IdDefCateg, Nom, IdUnite, price, IdTVA, 0, list[0]);
                            SQlQueryExecuter.RunQuery(query);
                        }
                        else
                        {
                            query = ChargeQuery.InsertChargeQuery(IdDefCateg, Nom, IdUnite, price,IdTVA,0, mType);
                            SQlQueryExecuter.RunQuery(query);
                            IdCharge = SQLQueryBuilder.FindId("Charges", "ID", "Nom", row.Cells[1].Value.ToString());
                        }
                        
                        if (mIdAct != 0)
                        {
                            if (mType2 == "annuelle" || mType2 == "")
                            {
                                double.TryParse(row.Cells[7].Value.ToString(), out double QteAv1);
                                double.TryParse(row.Cells[8].Value.ToString(), out double Qte1);

                                query = "Select ID From Charge_Quantite Where IdCharges = '" + IdCharge +
                                "' AND IdActivite = '" + mIdAct + "';";
                                List<string> list2 = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                                if (Commun.ListHasValue(list2))
                                {
                                    query = ChargeQuery.UpdateQuantiteQuery(QteAv1, Qte1, IdCharge, mIdAct);
                                }
                                else
                                {
                                    query = ChargeQuery.AddQuantiteQuery(QteAv1, Qte1, IdCharge, mIdAct);
                                }
                            }
                            if (mType2 == "perenne")
                            {
                                double.TryParse(row.Cells[7].Value.ToString(), out double QteAv1);
                                List<double> listphase = GetPhaseList(row.Cells[1].Value.ToString());

                                query = "Select ID From Charge_Perenne Where IdCharges = '" + IdCharge +
                                           "' AND IdActivite = '" + mIdAct + "';";
                                List<string> list2 = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                                if (Commun.ListHasValue(list2))
                                {
                                    query = ChargeQuery.UpdatePerenneQuery(listphase,QteAv1, IdCharge, mIdAct);
                                }
                                else
                                {
                                    query = ChargeQuery.AddPerenneQuery(listphase, QteAv1, IdCharge, mIdAct);
                                }
                            }
                            if (mType2 == "pluriannuelle")
                            {
                                double.TryParse(row.Cells[7].Value.ToString(), out double QteAv1);
                                List<double> listphase = GetPhaseList(row.Cells[2].Value.ToString());

                               
                                query = "Select ID From Charge_Pluriannuelle Where IdCharges = '" + IdCharge +
                                          "' AND IdActivite = '" + mIdAct + "';";
                                List<string> list2 = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                                if (Commun.ListHasValue(list2))
                                {
                                    query = ChargeQuery.UpdatePluriannuelleQuery(listphase, QteAv1, IdCharge, mIdAct);
                                }
                                else
                                {
                                    query = ChargeQuery.AddPluriannuelleQuery(listphase, QteAv1, IdCharge, mIdAct);
                                }
                            }
                            SQlQueryExecuter.RunQuery(query);

                            query = "SELECT IdActivite FROM Caract_Activite WHERE IdCharges = '" + IdCharge +
                                    "' AND IdActivite  = '" + mIdAct + "';";
                            List<string> ListField = SQlQueryExecuter.RunQueryReaderStr("IdActivite", query);
                            if (ListField.Count == 0)
                            {
                                query = "INSERT INTO Caract_Activite (IdActivite,IdCharges) " +
                                    "VALUES ( '" + mIdAct + "','" + IdCharge + "');";
                                SQlQueryExecuter.RunQuery(query);
                            }
                        }
                    }
                    mDataSaved = true;
                    if (chargeForm.Text.Contains("*"))
                    {
                        chargeForm.Text = chargeForm.Text.Remove(chargeForm.Text.Length - 1, 1);
                    }
                    ManageColor();
                 
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                }
            
        }

        private bool GroupeExiste(string NomdefCateg)
        {
            try
            {
                string query = "Select * From Def_Categ Where Nom = '" + NomdefCateg + "';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("IdDefCateg", query);
                if (Commun.ListHasValue(list)) return true;
                else return false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return true;
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
                    if (i == 10) rowvalue[i] = (GetLastIndex() + 1).ToString();
                    else rowvalue[i] = null;
                }
                mChargeTable.Rows.Add(rowvalue);
                // the ID of the dico might be the same than in the table or not
                // it's not a big deal if not as long as we insert the new record,
                // the final ID on the table does not matter.
                SetScrollBarPosition();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Event that fires when the user wants to save the data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonSaveData(object sender, SaveDataEvent e)
        {
            try
            {
                SaveData(e.deleteOnTable);
                DataCheck();
                ResetMainQuery(e.table);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                mChargeTable.Clear();
                adapter.Fill(mChargeTable);
                DataGridView.DataSource = mChargeTable;
                mChargeTable = FilterTable(mChargeTable);
                DataGridView.Refresh();
                ManageColor();

                mDataSaved = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Event that fires when the user wants remove a line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonRemoveRow(object sender,EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection UserSelectedRow = DataGridView.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    ListeRecordToDelete.Add(GetId(index));
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
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        /// <summary>
        /// Event that fires when the user wants to duplicate a line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonDuplicate(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection ListuserRow = DataGridView.SelectedRows;
                string[] rowvalue = new string[10];
                if (ListuserRow.Count == 1)
                {
                    for (int j = 0; j <= ListuserRow.Count - 1; j++)
                    {
                        rowvalue = new string[DataGridView.ColumnCount];
                        for (int i = 0; i <= DataGridView.ColumnCount - 1; i++)
                        {
                            rowvalue[i] = ListuserRow[j].Cells[i].Value.ToString();
                        }
                        //dataGridView1.Rows.Add(rowvalue);
                        mChargeTable.Rows.Add(rowvalue);
                    }
                    int id = Commun.GetMaxId("ID", "Charges") + 1;
                    
                    mDataSaved = false;
                    if (!chargeForm.Text.Contains("*"))
                    {
                        chargeForm.Text = chargeForm.Text + "*";
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
        /// Event that fires when the user wants cancel and close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void buttonCancel(object sender, EventArgs e)
        {
            DialogResult dialogResult = new DialogResult();
            if (mDataSaved == false)
            {
                dialogResult = MessageBox.Show(Translation.Translate("Data has been modified but not saved, do  really want to exit ?",langue), "Warning", MessageBoxButtons.YesNo);
            }
            if (dialogResult == DialogResult.No)
            {
                SaveData(true);
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

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
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
                double decalage = 1;
                List<string> list = new List<string>();
                string query = "";
                ContextMenu contextmenu = new ContextMenu();
                List<MenuItem> ListItem = new List<MenuItem>();
                list = switchfunction(column);
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
            if (mType2 == "annuelle" || mType2 == "")
            {
                switch (NumCol)
                {
                    case 0: //group
                        query = "Select Nom From Def_Categ Where IdDefinitions = '2';";
                        list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        break;
                    case 2: //unit activite
                        query = "Select UAte From SystemeUnite;";
                        list = SQlQueryExecuter.RunQueryReaderStr("UAte", query);
                        break;
                    case 3: // unit exploi
                        query = "Select UEnt From SystemeUnite;";
                        list = SQlQueryExecuter.RunQueryReaderStr("UEnt", query);
                        break;
                    case 4: // unit global
                        query = "Select UGlobal From SystemeUnite;";
                        list = SQlQueryExecuter.RunQueryReaderStr("UGlobal", query);
                        break;
                    case 6://TVA
                        query = "Select Nom From TVA;";
                        list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        break;
                }
            }
            if (mType2 == "perenne" || mType2 == "pluriannuelle")
            {
                switch (NumCol)
                {
                    case 0: //group
                        query = "Select Nom From Def_Categ Where IdDefinitions = '2';";
                        list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        break;
                    case 2: //unit activite
                        query = "Select UAte From SystemeUnite;";
                        list = SQlQueryExecuter.RunQueryReaderStr("UAte", query);
                        break;
                    case 4://TVA
                        query = "Select Nom From TVA;";
                        list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        break;
                }
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
                if (DataGridView.CurrentCell.ColumnIndex == 6) mCurrentTva = array[1].Trim();
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
                    DialogResult dialogResult = new DialogResult();
                    if (Commun.NameExists(DataGridView.Rows[e.RowIndex].Cells[1].Value.ToString(), "Charges", "Nom"))
                    {
                        dialogResult = MessageBox.Show(Translation.Translate("An item with this name already exists. It may generate some errors on reports or calculs." +
                            " Do you want to continue ?",langue), "Warning", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            DataGridView.Rows[e.RowIndex].Cells[1].Value = "";
                            return;
                        }
                    }
                }
          
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
            DataGridView.Refresh();
            if (!chargeForm.Text.Contains("*"))
            {
                chargeForm.Text = chargeForm.Text + "*";
            }
            if (e.ColumnIndex == 2)
            {
                string query = "Select * From SystemeUnite Where UAte = '" + DataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() + "';";
                List<string> listUEnt = SQlQueryExecuter.RunQueryReaderStr("UEnt", query);
                List<string> listUGlo = SQlQueryExecuter.RunQueryReaderStr("UGlobal", query);
                if (Commun.ListHasValue(listUEnt) && Commun.ListHasValue(listUGlo))
                {
                    DataGridView.Rows[e.RowIndex].Cells[3].Value = listUEnt[0];
                    DataGridView.Rows[e.RowIndex].Cells[4].Value = listUGlo[0];
                }
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
        internal void ButtonPropositionClick(object sender, EventArgs e)
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
        internal void AddSelectedCharge(int ID,string tab,DataGridView dvg)
        {
            try
            {
                string query = "";
                string[] rowvalue = null;

                if (mType2 == "" || mType2 == "annuelle")
                {
                    if (tab == "Charges")
                    {
                        query = ChargeQuery.AddSelectedChargeQuery(ID, mIdAct);
                        rowvalue = SQlQueryExecuter.RunQueryReader(query);
                        if (rowvalue[7] == "") rowvalue[7] = "0";
                        if (rowvalue[8] == "") rowvalue[8] = "0";
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
                    for (int i = 0;i < rowvalue.Length;i++)
                    {
                        if (rowvalue[i] == "") rowvalue[i] = "0";
                    }
                    mChargeTable.Rows.Add(rowvalue);
                }
                if( mType2 == "pluriannuelle")
                {
                    query = ChargeQuery.AddSelectedChargeQuery3(ID, mIdAct);
                    rowvalue = SQlQueryExecuter.RunQueryReader(query);
                    for (int i = 0; i < rowvalue.Length; i++)
                    {
                        if (rowvalue[i] == "") rowvalue[i] = "0";
                    }
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
                mChargeTable = FilterTable(mChargeTable);
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
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
             
            }
        }

      
        internal List<double> GetPhaseList(string charge)
        {
            List<double> list = new List<double>();
            foreach (DataGridViewRow row in DataGridView.Rows)
            {
                if (row.Cells[1].Value.ToString() == charge)
                {
                    foreach (DataGridViewColumn column in DataGridView.Columns)
                    {
                        if (column.Name.Contains("Ph"))
                        {
                            double.TryParse(row.Cells[column.Index].Value.ToString(), out double temp);
                            list.Add(temp);
                        }
                    }
                }
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

                        query = "Select ID From Charge_Quantite Where IdCharges = '" + index + "' and IdActivite = '"+ mIdAct + "';";
                        list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (list2.Count == 0)
                        {
                            query = "Insert Into Charge_Quantite (IDCharges,IdActivite)" +
                                "VALUES ('" + index + "','"+mIdAct+"');";
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                    if (mType2 == "perenne")
                    {
                        query = "Select ID From Charge_Perenne Where IdCharges = '" + index + "' and IdActivite = '" + mIdAct + "';";
                        list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (list2.Count == 0)
                        {
                            query = "Insert Into Charge_Perenne (IDCharges,IdActivite)" +
                                "VALUES ('" + index + "','" + mIdAct + "');";
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                    if (mType2 == "pluriannuelle")
                    {
                        query = "Select ID From Charge_Pluriannuelle Where IDCharges = '" + index + "' and IdActivite = '" + mIdAct + "';";
                        list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (list2.Count == 0)
                        {
                            query = "Insert Into Charge_Pluriannuelle (IDCharges,IdActivite)" +
                                "VALUES ('" + index + "','" + mIdAct + "');";
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
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
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
                int.TryParse(mChargeTable.Rows[mChargeTable.Rows.Count - 1].ItemArray[10].ToString(), out id);
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
            catch   (Exception Ex)
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
                Commun.Setbackground(DataGridView, color1, color2);
                string query = SQLQueryBuilder.UpdateColorQuery(color1, color2);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
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
        internal int GetNumColonneId()
        {
            int id = -1;
            try
            {
                if (mType2 == "" || mType2 == "annuelle")
                {
                    id = 12;
                }
                if (mType2 == "pluriannuelle")
                {
                    id = 13;
                }
                if (mType2 == "perenne")
                {
                    id = DataGridView.Columns.Count - 1;
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
        /// refresh the view when the user change the size of the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SizeChanged(object sender, EventArgs e)
        {
            SaveData(false);
            RefreshView("");
        }
    }
}
