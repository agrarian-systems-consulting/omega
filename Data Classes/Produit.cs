using OMEGA.Data_Classes;
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

namespace OMEGA
{
   internal class Produit 
    {
        #region variables Globales
        private Subro.Controls.DataGridViewGrouper grouper = new Subro.Controls.DataGridViewGrouper();
        private DataTable mproductTable = new DataTable("Produits");
        private string mainQuery;
        internal Boolean mDataSaved = false;
        private int mKeyIndex;
        internal int mCurrentid;
        private Button mbuttonOk;
        private Button mbuttonCancel;
        private Button mbuttonDuplicate;
        private Form produitForm;
        private Button mbuttonRemove;
        private Button mbuttonSave;
        private Button mbuttonProposition;
        private Button mButtongroupe;
        private Color color1;
        private Color color2;
        private int mIdAct;
        private string mCurrentTva;
        private PictureBox pictureBox;
        private bool showProposals = true;
        private string mType;
        private int langue = Properties.Settings.Default.Langue;
        #endregion

        internal List<int> ListeRecordToDelete { get; set; }
        internal DataGridView mDataGridView { get; set; }

        /// <summary>
        /// Constructor of a product
        /// </summary>
        /// <param name="mygrid"></param>
        /// <param name="buttonok"></param>
        /// <param name="buttonsave"></param>
        /// <param name="buttoncancel"></param>
        /// <param name="buttonRemove"></param>
        /// <param name="buttonDuplicate"></param>
        /// <param name="buttonProposition"></param>
        /// <param name="buttongrpe"></param>
        /// <param name="listId"></param>
        /// <param name="idAct"></param>
        /// <param name="ptcbox"></param>
        internal Produit(DataGridView mygrid, Button buttonok , Button buttonsave,
           Button buttoncancel, Button buttonRemove, Button buttonDuplicate , Button buttonProposition,
           Button buttongrpe, List<int> listId = null,int idAct = 0, PictureBox ptcbox=null,Form form = null,
           string type = "")
        {
            try
            {
                mButtongroupe = buttongrpe;
                mbuttonProposition = buttonProposition;
                mbuttonOk = buttonok;
                mbuttonDuplicate = buttonDuplicate;
                mbuttonRemove = buttonRemove;
                mbuttonSave = buttonsave;
                pictureBox = ptcbox;
                mDataGridView = mygrid;
                mbuttonCancel = buttoncancel;
                if (type == "" || type == "annuelle")
                {
                    mainQuery = ProduitQuery.MainQuery(listId, idAct);
                }
                else if ( type == "pluriannuelle")
                {
                    mainQuery = ProduitQuery.MainQuery3(listId, idAct, SQLQueryBuilder.FindName("Activite","Nom","ID",idAct));
                }
                else if (type == "perenne")
                {
                    mainQuery = ProduitQuery.MainQuery2(listId, idAct, SQLQueryBuilder.FindName("Activite", "Nom", "ID", idAct));
                }
                mType = type;
                ListeRecordToDelete = new List<int>();
                mIdAct = idAct;
                produitForm = form;
                if (File.Exists(Properties.Settings.Default.FichierTraduction))
                {
                    SetCaption();
                }
                LoadGridViewData();
                RenameColumnHeader();

               // Commun.CleanDataTableSeqId("Produits");
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
                DataCheck();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mproductTable);
                mDataGridView.DataSource = mproductTable;
                mproductTable = FilterTable(mproductTable);
                RenameColumnHeader();
                SetViewProperty();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                Log.WriteLog(e.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Adjust the view features
        /// </summary>
        private void SetViewProperty()
        {
            try
            {
                if (mType == "" || mType == "annuelle")
                {
                    mDataGridView.Columns[2].ReadOnly = true;
                    mDataGridView.Columns[3].ReadOnly = true;
                    mDataGridView.Columns[4].ReadOnly = true;
                    mDataGridView.Columns[6].ReadOnly = true;

                    mDataGridView.Columns[9].Visible = false;
                    mDataGridView.Columns[10].Visible = false;
                    mDataGridView.Columns[11].Visible = false;
                    mDataGridView.Columns[12].Visible = false;
                    if (mIdAct == 0)
                    {
                        mDataGridView.Columns[7].Visible = false;
                        mDataGridView.Columns[8].Visible = false;
                    }
                }
                if (mType == "perenne" || mType == "pluriannuelle")
                {
                    mDataGridView.Columns[2].ReadOnly = true;//Unité
                    mDataGridView.Columns[4].ReadOnly = true;//TVA

                    mDataGridView.Columns[GetNumColonneId() - 3].Visible = false;
                    mDataGridView.Columns[GetNumColonneId() - 2].Visible = false;
                    mDataGridView.Columns[GetNumColonneId() - 1].Visible = false;
                    mDataGridView.Columns[GetNumColonneId()].Visible = false;
                }

                mDataGridView.RowHeadersWidth = 30;
                mDataGridView.ColumnHeadersHeight = 30;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs When the button proposal is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonPropositionClick(object sender, EventArgs e)
        {
            try
            {
               int langue = Properties.Settings.Default.Langue;
               if (mbuttonProposition.Text.Contains(Translation.Translate("Enable",langue)))
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
        /// Filter the table product to delete duplicate data
        /// </summary>
        /// <param name="productTable"></param>
        /// <returns></returns>
        private DataTable FilterTable(DataTable productTable)
        {
            try
            { 
                int rowindex = 0;
                List<DataRow> ListRowToDelete = new List<DataRow>();
                bool rowDelete = false;
                string id = "";
                string nextid = "";

                foreach (DataRow row in productTable.Rows)
                {
                    if (rowindex == productTable.Rows.Count-1) break;
                    id = productTable.Rows[rowindex].ItemArray[GetNumColonneId()].ToString();
                    nextid = productTable.Rows[rowindex + 1].ItemArray[GetNumColonneId()].ToString();
                    if (id == nextid)
                    {
                        //double qty;
                        //if (double.TryParse(row.ItemArray[7].ToString(),out qty) || double.TryParse(row.ItemArray[8].ToString(), out qty))
                        //{
                        //    ListRowToDelete.Add(productTable.Rows[rowindex + 1]);
                        //    rowDelete = true;
                        //}
                        //if ((double.TryParse(productTable.Rows[rowindex + 1].ItemArray[7].ToString(), out  qty) || double.TryParse(productTable.Rows[rowindex + 1].ItemArray[8].ToString(), out qty)) && !rowDelete)
                        //{
                        //    ListRowToDelete.Add(row);
                        //    rowDelete = true;
                        //}
                        if (!ListRowToDelete.Contains(productTable.Rows[rowindex + 1]))
                        {
                            ListRowToDelete.Add(productTable.Rows[rowindex + 1]);
                        }
                        rowDelete = false;
                    }
                    rowindex++;
                }
                foreach (DataRow row in ListRowToDelete)
                {
                    productTable.Rows.Remove(row);
                }
                return productTable;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return productTable;
            }
        }

        internal void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex == 2)
                {
                    string query = "Select * From SystemeUnite Where UAte = '" + mDataGridView.Rows[e.RowIndex].Cells[2].Value.ToString() + "';";
                    List<string> listUEnt = SQlQueryExecuter.RunQueryReaderStr("UEnt", query);
                    List<string> listUGlo = SQlQueryExecuter.RunQueryReaderStr("UGlobal", query);
                    if (Commun.ListHasValue(listUEnt) && Commun.ListHasValue(listUGlo))
                    {
                        mDataGridView.Rows[e.RowIndex].Cells[3].Value = listUEnt[0];
                        mDataGridView.Rows[e.RowIndex].Cells[4].Value = listUGlo[0];
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
        /// Check the quantity data
        /// </summary>
        private void DataCheck()
        {
            try
            { 
                string query = SQLQueryBuilder.SelectQuery("Produits", "ID");
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                List<int> list2 = new List<int>();
                foreach (int index in list)
                {
                    if (mType == "" || mType == "annuelle")
                    {
                        query = "Select ID From Prod_Quantite Where IdProduits = '" + index + "' and IdActivite = '" + mIdAct + "';";
                        list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (list2.Count == 0)
                        {
                            query = "Insert Into Prod_Quantite (IdProduits,IdActivite)" +
                                "VALUES ('" + index + "','"+ mIdAct+"');";
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                    if (mType == "perenne")
                    {
                        query = "Select ID From Prod_Perenne Where IdProduits = '" + index + "' and IdActivite = '" + mIdAct + "';";
                        list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (list2.Count == 0)
                        {
                            query = "Insert Into Prod_Perenne (IdProduits,IdActivite)" +
                                "VALUES ('" + index+ "','" + mIdAct + "');";
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                    if (mType == "pluriannuelle")
                    {
                        query = "Select ID From Prod_Pluriannuel Where IdProduits = '" + index + "' and IdActivite = '" + mIdAct + "';";
                        list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (list2.Count == 0)
                        {
                            query = "Insert Into Prod_Pluriannuel (IdProduits,IdActivite)" +
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
        /// Translate the text to user language
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
                if (mButtongroupe != null) mButtongroupe.Text = Translation.Translate("Enable group", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

   
        /// <summary>
        /// Rename the column of the mDataGridView
        /// </summary>
        private void RenameColumnHeader()
        {
            int langue = Properties.Settings.Default.Langue;
            if (mType == "annuelle" || mType =="")
            {
                mDataGridView.Columns[0].HeaderText = Translation.Translate("Group", langue);
                mDataGridView.Columns[1].HeaderText = Translation.Translate("Name", langue);
                mDataGridView.Columns[2].HeaderText = Translation.Translate("Unit activity", langue);
                mDataGridView.Columns[3].HeaderText = Translation.Translate("Unit farm", langue);
                mDataGridView.Columns[4].HeaderText = Translation.Translate("Unit global", langue);
                mDataGridView.Columns[5].HeaderText = Translation.Translate("Price", langue);
                mDataGridView.Columns[6].HeaderText = Translation.Translate("TVA", langue);
            }
            if (mType == "perenne")
            {
                mDataGridView.Columns[0].HeaderText = Translation.Translate("Group", langue);
                mDataGridView.Columns[1].HeaderText = Translation.Translate("Name", langue);

            }
            if (mType == "pluriannuelle")
            {
                mDataGridView.Columns[0].HeaderText = Translation.Translate("Group", langue);
                mDataGridView.Columns[1].HeaderText = Translation.Translate("Name", langue);

            }
                mDataGridView.ColumnHeadersHeight = 40;
        }
        
        /// <summary>
        /// Save the data on the table
        /// </summary>
        /// <param name="DeletefFromTable"></param>
        private void SaveData(bool DeletefFromTable)
        {

            try
            {
                string query = "";
                int IdDefCateg = 0;
                int IdTVA = 0;
                int IdUnite = 0;
                int IdProduit = 0;
                string NomTVA = "";
                string Nom = "";
                string NomDefCateg = "";
                string NomUAte = "";
                string price = "";
                foreach (int index in ListeRecordToDelete)
                {
                    if (mIdAct != 0)
                    {
                        query = SQLQueryBuilder.DeleteQuery("Caract_Activite", "Where IdProduits = '" + index + "' AND IdActivite = '" + mIdAct + "'");
                        SQlQueryExecuter.RunQuery(query);
                    }
                    else
                    {
                        query = SQLQueryBuilder.DeleteQuery(index, "Produits");
                        SQlQueryExecuter.RunQuery(query);
                    }
                }
                ListeRecordToDelete.Clear();

                foreach (DataGridViewRow row in mDataGridView.Rows)
                {
                    NomDefCateg = row.Cells[0].Value.ToString();
                    NomTVA = row.Cells[6].Value.ToString();
                    NomUAte = row.Cells[2].Value.ToString();
                    price = row.Cells[5].Value.ToString();
                    Nom = row.Cells[1].Value.ToString();
                    if (!GroupeExiste(NomDefCateg))
                    {
                        query = SQLQueryBuilder.AddNewgGroupeQuery(NomDefCateg, 1);
                        SQlQueryExecuter.RunQuery(query);
                    }

                    IdDefCateg = SQLQueryBuilder.FindId("Def_Categ", "IdDefCateg", "Nom", NomDefCateg);
                    IdTVA = SQLQueryBuilder.FindId("TVA", "IdTVA", "Nom", NomTVA);
                    IdUnite = SQLQueryBuilder.FindId("SystemeUnite", "IdSysUnit", "UAte", NomUAte);
                    IdProduit = SQLQueryBuilder.FindId("Produits", "ID", "Nom", row.Cells[1].Value.ToString());
                    if (IdProduit == 0)
                    {
                        int.TryParse(row.Cells[GetNumColonneId()].Value.ToString(), out IdProduit);
                    }
                    query = "Select * From Produits Where ID = '" + IdProduit + "';";

                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        query = ProduitQuery.UpdateValueQuery(IdDefCateg, Nom, IdUnite, price, IdTVA, list[0]);
                        SQlQueryExecuter.RunQuery(query);
                    }
                    else
                    {
                        query = ProduitQuery.InsertProduitQuery(IdDefCateg, Nom, IdUnite, price, IdTVA);
                        SQlQueryExecuter.RunQuery(query);
                        IdProduit = SQLQueryBuilder.FindId("Produits", "ID", "Nom", row.Cells[1].Value.ToString());
                    }

                    if (mIdAct != 0)
                    {
                        if (mType == "annuelle" || mType == "")
                        {
                            double.TryParse(row.Cells[7].Value.ToString(), out double QteAv1);
                            double.TryParse(row.Cells[8].Value.ToString(), out double Qte1);

                            query = "Select ID From Prod_Quantite Where IdProduits = '" + IdProduit +
                            "' AND IdActivite = '" + mIdAct + "';";
                            List<string> list2 = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                            if (Commun.ListHasValue(list2))
                            {
                                query = ProduitQuery.UpdateQuantiteQuery(QteAv1, Qte1, IdProduit, mIdAct);
                            }
                            else
                            {
                                query = ProduitQuery.AddQuantiteQuery(QteAv1, Qte1, IdProduit, mIdAct);
                            }
                        }
                        if (mType == "perenne")
                        {
                            double.TryParse(row.Cells[7].Value.ToString(), out double QteAv1);
                            List<double> listphase = GetPhaseList(row.Cells[1].Value.ToString());

                            query = "Select ID From Prod_Perenne Where IdProduits = '" + IdProduit +
                                       "' AND IdActivite = '" + mIdAct + "';";
                            List<string> list2 = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                            if (Commun.ListHasValue(list2))
                            {
                                query = ProduitQuery.UpdatePerenneQuery(listphase, QteAv1, IdProduit, mIdAct);
                            }
                            else
                            {
                                query = ProduitQuery.AddPerenneQuery(listphase, QteAv1, IdProduit, mIdAct);
                            }
                        }
                        if (mType == "pluriannuelle")
                        {
                            double.TryParse(row.Cells[7].Value.ToString(), out double QteAv1);
                            List<double> listphase = GetPhaseList(row.Cells[2].Value.ToString());


                            query = "Select ID From Charge_Pluriannuelle Where IdProduits = '" + IdProduit +
                                      "' AND IdActivite = '" + mIdAct + "';";
                            List<string> list2 = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                            if (Commun.ListHasValue(list2))
                            {
                                query = ProduitQuery.UpdatePluriannuelleQuery(listphase, QteAv1, IdProduit, mIdAct);
                            }
                            else
                            {
                                query = ProduitQuery.AddPluriannuelleQuery(listphase, QteAv1, IdProduit, mIdAct);
                            }
                        }
                        SQlQueryExecuter.RunQuery(query);

                        query = "SELECT IdActivite FROM Caract_Activite WHERE IdProduits = '" + IdProduit +
                                "' AND IdActivite  = '" + mIdAct + "';";
                        List<string> ListField = SQlQueryExecuter.RunQueryReaderStr("IdActivite", query);
                        if (ListField.Count == 0)
                        {
                            query = "INSERT INTO Caract_Activite (IdActivite,IdProduits) " +
                                "VALUES ( '" + mIdAct + "','" + IdProduit + "');";
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                }
                mDataSaved = true;
                if (produitForm.Text.Contains("*"))
                {
                    produitForm.Text = produitForm.Text.Remove(produitForm.Text.Length - 1, 1);
                    ManageColor();

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private bool GroupeExiste(string nomDefCateg)
        {
            try
            {
                string query = "Select * From Def_Categ Where Nom = '" + nomDefCateg + "';";
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
        /// Occurs when the user add a new row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonAdd(object sender, EventArgs e)
        {
            try
            {
                // we add a new row in the table with a new ID
                string[] rowvalue = new string[mDataGridView.ColumnCount];
                for (int i = 0; i <= mDataGridView.ColumnCount - 1; i++)
                {
                    if (i == GetNumColonneId()) rowvalue[i] = (GetLastIndex() + 1).ToString();
                    else rowvalue[i] = null;
                }
                 mproductTable.Rows.Add(rowvalue);
                 SetScrollBarPosition();
                 
                // mDataGridView.CurrentCell = mDataGridView.Rows[0].Cells[0];
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Reset the position on the scroll bar
        /// </summary>
        private void SetScrollBarPosition()
        {
            try
            {
                if (mDataGridView.SortOrder == SortOrder.Ascending)
                {
                    mDataGridView.FirstDisplayedScrollingRowIndex = 0;
                }
                else
                {
                    mDataGridView.FirstDisplayedScrollingRowIndex = mDataGridView.RowCount;
                }
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
           
        }

        /// <summary>
        /// Occurs when the user clicks on the button save
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
                mproductTable = new DataTable("Produits");
                adapter.Fill(mproductTable);
                mDataGridView.DataSource = mproductTable;
                mDataGridView.Refresh();
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
        /// Occurs when the user delete a row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonRemoveRow(object sender, EventArgs e)
        {
            try
            {
                mDataGridView.AllowUserToDeleteRows = true;
                DataGridViewSelectedRowCollection UserSelectedRow = mDataGridView.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    if (!ListeRecordToDelete.Contains(GetId(index))) ListeRecordToDelete.Add(GetId(index));
                    mDataGridView.Rows.RemoveAt(index);
                }

                mDataSaved = false;
                if (!produitForm.Text.Contains("*"))
                {
                    produitForm.Text = produitForm.Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        /// <summary>
        /// Occurs when the user duplicates a row
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonDuplicate(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection ListuserRow = mDataGridView.SelectedRows;
                string[] rowvalue = new string[13];
                if (ListuserRow.Count == 1)
                {
                    for (int j = 0; j <= ListuserRow.Count - 1; j++)
                    {
                        rowvalue = new string[mDataGridView.ColumnCount];
                        for (int i = 0; i <= mDataGridView.ColumnCount - 1; i++)
                        {
                            if ((i == 7 || i == 8) && ListuserRow[j].Cells[i].Value.ToString() == "")
                            {
                                rowvalue[i] = "0";
                            }
                            else rowvalue[i] = ListuserRow[j].Cells[i].Value.ToString();
                        }
                    }
                    // id produit = clé dico
                    int id =Commun.GetMaxId("ID","Produits")+1;
                    if (!produitForm.Text.Contains("*"))
                    {
                        produitForm.Text = produitForm.Text + "*";
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
        /// Occurs when the iser click on cancel button
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
        /// Occurs when the user sort the grid view, to reset the background color of the line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DataGridView1_Sorted(object sender, EventArgs e)
        {
            ManageColor();
        }

        /// <summary>
        /// Occurs when the user click on a cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (showProposals) FillAutolistAfterClick(e, e.ColumnIndex);
                mCurrentid = GetId(e.RowIndex);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Display a list that allows the user to select if he/she wants to put the same name
        /// </summary>
        /// <param name="e"></param>
        /// <param name="column"></param>
        private void FillAutolistAfterClick(DataGridViewCellEventArgs e,int column)
        {
            try
            { 
                double decalage = 1;
                List<string> list = new List<string>();
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

                mDataGridView.ContextMenu = contextmenu;
                decalage = 1.15;
                Rectangle MyCell = mDataGridView.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                int x = (int) Math.Ceiling(MyCell.X * decalage);
                int y = (int) Math.Ceiling(MyCell.Y * decalage);
                Point point = new Point( x, y);
                contextmenu.Show(mDataGridView, point);

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

        private List<string> switchfunction (int NumCol)
        {
            List<string> list = new List<string>();
            string query = "";
            if (mType == "annuelle" || mType == "")
            {
                switch (NumCol)
                {
                    case 0: //group
                        query = "Select Nom From Def_Categ Where IdDefinitions = '1';";
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
            if  (mType == "perenne" || mType == "pluriannuelle")
            {
                switch (NumCol)
                {
                    case 0: //group
                        query = "Select Nom From Def_Categ Where IdDefinitions = '1';";
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
            return list;
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
                mDataGridView.CurrentCell.Value = array[1].Trim();
                mDataGridView.Refresh();
                if (mDataGridView.CurrentCell.ColumnIndex == 6) mCurrentTva = array[1].Trim();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user leave a cell he/she have just edited
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DataGridView1CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 1)
                {
                    DialogResult dialogResult = new DialogResult();
                    if (Commun.NameExists(mDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString(), "Produits", "Nom"))
                    {
                        dialogResult = MessageBox.Show(Translation.Translate("A product with this name already exists. It may generate some errors " +
                            "on reports or calculs. Do you want to continue ?",langue), "Warning", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            mDataGridView.Rows[e.RowIndex].Cells[1].Value = "";
                            return;
                        }
                    }
                }
                if (e.ColumnIndex == 0)
                {
                    mDataGridView.FirstDisplayedScrollingRowIndex = mDataGridView.Rows[e.RowIndex].Index;
                    SetFocusToNewRow();
                    mDataGridView.Refresh();
                }
                mDataSaved = false;
                if (!produitForm.Text.Contains("*"))
                {
                    produitForm.Text = produitForm.Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Export the data into a .csv file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ExportTable(object sender, EventArgs e)
        {
            Export.RunExportTable(mproductTable);
        }

        /// <summary>
        /// Open then note form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void NoteForm(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("Produits");
            form.ShowDialog();
        }

        /// <summary>
        /// Occurs when the user click on a cell with the right click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DataGridViewMouseClick(object sender, MouseEventArgs e)
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
                    var hitTestInfo = mDataGridView.HitTest(e.X, e.Y);
                    mDataGridView.ContextMenu = contextmenu;
                    if (hitTestInfo.ColumnIndex > 0)
                    {
                        contextmenu.Show(mDataGridView, pos);
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

        /// <summary>
        /// Occurs when the user clicks on "hide a column"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private EventHandler itemHide_Click(object sender, EventArgs e,int index)
        {
            mDataGridView.Columns[index].Visible = false;
            return null;
        }

        /// <summary>
        /// Occurs when the user clicks on "show a column"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private void itemShow_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in mDataGridView.Columns)
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
        internal void AddSelectedProduct(int ID,string tab,DataGridView dvg = null)
        {
            try
            {
                string query = "";
                string[] rowvalue = null;

                if (mType == "" || mType == "annuelle")
                {
                    if (tab == "Produits")
                    {
                        query = ProduitQuery.AddSelectedProduitQuery(ID);
                        rowvalue = SQlQueryExecuter.RunQueryReader(query);
                        mproductTable.Rows.Add(rowvalue);
                    }
                    if (tab == "TabProduits")
                    {
                        query = ProduitQuery.AddSelectedProduitQuery(ID);
                        rowvalue = SQlQueryExecuter.RunQueryReader(query);
                        for (int i = 0; i < rowvalue.Length; i++)
                        {
                            if (rowvalue[i] == "") rowvalue[i] = "0";
                        }
                        mproductTable.Rows.Add(rowvalue);
                    }
                    if (tab == "TabChargesPied")
                    {
                        //query = ProduitQuery.AddSelectecQuery(ID);
                        //rowvalue = SQlQueryExecuter.RunQueryReader(query);
                        //dvg.Rows.Add(rowvalue);
                    }
                }
                if (mType == "perenne")
                {
                    query = ProduitQuery.AddSelectedProduitQuery2(ID, mIdAct);
                    rowvalue = SQlQueryExecuter.RunQueryReader(query);
                    ChechRowValues(rowvalue);
                    mproductTable.Rows.Add(rowvalue);
                   
                }
                if (mType == "pluriannuelle")
                {
                    query = ProduitQuery.AddSelectedProduitQuery3(ID, mIdAct);
                    rowvalue = SQlQueryExecuter.RunQueryReader(query);
                    //dvg.Rows.Add(rowvalue);
                    mproductTable.Rows.Add(rowvalue);
                }

                mDataSaved = false;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        private string[] ChechRowValues(string[] arr)
        {
            for (int i = 0;i<arr.Length;i++)
            {
                if (arr[i] == "")
                {
                    arr[i] = "0";
                }
            }
            return arr;
        }
        
      
        internal  List<int> GetPhaseList( InfoUserProduit item)
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in mDataGridView.Rows)
            {
                if (row.Cells[1].Value.ToString() == item.Nom)
                {
                    foreach (DataGridViewColumn column in mDataGridView.Columns)
                    {
                        if (column.Name.Contains("Ph"))
                        {
                            int.TryParse(row.Cells[column.Index].Value.ToString(), out int temp);
                            list.Add(temp);
                        }
                    }
                }
            }
            return list;
        }

        internal List<double> GetPhaseList(string produit)
        {
            List<double> list = new List<double>();
            foreach (DataGridViewRow row in mDataGridView.Rows)
            {
                if (row.Cells[1].Value.ToString() == produit)
                {
                    foreach (DataGridViewColumn column in mDataGridView.Columns)
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
        /// rRefresch the value after being edited by the ser
        /// </summary>
        /// <param name="table"></param>
        internal void RefreshView(string table)
        {
            try
            {
                ResetMainQuery(table);
                DataCheck();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                mproductTable.Clear();
                adapter.Fill(mproductTable);
                mDataGridView.DataSource = mproductTable;
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
                List<int> listId = SQLQueryBuilder.GetListID(table, "Produits", mIdAct);

                if (mType == "" || mType == "annuelle")
                {
                    mainQuery = ProduitQuery.MainQuery(listId, mIdAct);
                }
                else if (mType == "pluriannuelle")
                {
                    mainQuery = ProduitQuery.MainQuery3(listId, mIdAct, SQLQueryBuilder.FindName("Activite", "Nom", "ID", mIdAct));
                }
                else if (mType == "perenne")
                {
                    mainQuery = ProduitQuery.MainQuery2(listId, mIdAct, SQLQueryBuilder.FindName("Activite", "Nom", "ID", mIdAct));
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Put the No in the row header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void DataGridViewDetailRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
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
        internal void DataGridView1EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                TextBox autoText = e.Control as TextBox;
                autoText.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                autoText.AutoCompleteSource = AutoCompleteSource.CustomSource;
                AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();

                if (mType == "" || mType == "annuelle")
                {
                    if (autoText != null)
                    {
                        switch (mDataGridView.CurrentCell.ColumnIndex)
                        {
                            case 0: // groupe product
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
                if (mType == "perenne" || mType == "pluriannuelle")
                {
                    if (autoText != null)
                    {
                        switch (mDataGridView.CurrentCell.ColumnIndex)
                        {
                            case 0: // groupe product
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
        internal void addItems(AutoCompleteStringCollection col,int type)
        {
            string query = ""; ;
            List<string> list = new List<string>();
            switch (type)
            {
                case 0: // groupe produit
                    query = ProduitQuery.AutoCompleteQuery();
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
           
            foreach(string item in list)
            {
                col.Add(item);
            }
            mDataSaved = false;
            if (!produitForm.Text.Contains("*"))
            {
                produitForm.Text = produitForm.Text + "*";
            }
        }

        /// <summary>
        /// Retourne l'Id de la ligne envoyée en paramètre
        /// </summary>
        /// <param name="Rowindex"></param>
        /// <returns></returns>
        internal int GetId(int Rowindex)
        {
            int id = -1;
            try
            {
                if (mType == "annuelle")
                {
                    if (mDataGridView.Rows[Rowindex].Cells[12].Value != null)
                    {
                        int.TryParse(mDataGridView.Rows[Rowindex].Cells[12].Value.ToString(), out id);
                    }
                }
                if (mType == "pluriannuel")
                {
                    if (mDataGridView.Rows[Rowindex].Cells[13].Value != null)
                    {
                        int.TryParse(mDataGridView.Rows[Rowindex].Cells[13].Value.ToString(), out id);
                    }
                }
                if (mType == "perenne" || mType == null || mType == "")
                {
                    int index = 0;
                    foreach (DataGridViewColumn column in mDataGridView.Columns)
                    {
                        if (column.Name == "ID")
                        {
                            int.TryParse(mDataGridView.Rows[Rowindex].Cells[index].Value.ToString(), out id);
                        }
                        index++;
                    }
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
        /// Retourne l'Id de la ligne envoyée en paramètre
        /// </summary>
        /// <returns></returns>
        internal int GetNumColonneId()
        {
            int id = -1;
            try
            {
                id = mDataGridView.Columns.Count-1;
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
                if (mproductTable.Rows.Count > 0)
                {
                    int id;
                    int.TryParse(mproductTable.Rows[mproductTable.Rows.Count - 1].ItemArray[GetNumColonneId()].ToString(), out id);
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
                if (mDataGridView.CurrentCell.ColumnIndex == 0)
                {
                    for (int i = 0; i <= mDataGridView.Rows.Count - 1; i++)
                    {
                        if (mDataGridView.Rows[i].Cells[1].Value.ToString () == "")
                        {
                            mDataGridView.CurrentCell = mDataGridView.Rows[i].Cells[0];
                            mDataGridView.CurrentCell.Selected = true;
                            mDataGridView.FirstDisplayedScrollingRowIndex =i;
                            mDataGridView.Refresh();
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
        /// Color the row 
        /// </summary>
        private void ManageColor()
        {
            if (color1 != null && color2 != null)
            {
                Commun.Setbackground(mDataGridView, color1, color2);
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
                if (mButtongroupe.Text.Contains(Translation.Translate("Enable", langue)))
                {
                    grouper.DataGridView = mDataGridView;
                    grouper.SetGroupOn(mDataGridView.Columns[0]);
                    grouper.DisplayGroup += grouper_DisplayGroup;
                    grouper.CollapseAll();
                    mButtongroupe.Text = Translation.Translate("Disable group", langue); ;
                    return;
                }
                if (mButtongroupe.Text.Contains(Translation.Translate("Disable", langue)))
                {
                    grouper.DataGridView = mDataGridView;
                    grouper.RemoveGrouping();
                    grouper.DisplayGroup -= grouper_DisplayGroup;
                    mButtongroupe.Text = Translation.Translate("Enable Groupe",langue);
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
        /// Diplay the groups
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
