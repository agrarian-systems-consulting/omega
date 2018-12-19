using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace OMEGA.Data_Classes
{

    /// <summary>
    /// This class represents all the minor features for a crop 
    /// </summary>
    internal class CritereCulture
    {

        private string CritereType;
        private int idAct;
        internal DataTable table { get; set; } = new DataTable();
        private int mCurrentItemtId;
        private DataGridView mCurrentDataGridView;
        private int Idtype;
        private List<int> ListeRecordToDelete = new List<int>();

        /// <summary>
        /// Constructor of the class
        /// </summary>
        /// <param name="type"></param>
        /// <param name="id"></param>
        /// <param name="dgv"></param>
        /// <param name="list"></param>
        internal CritereCulture (string type,int id,DataGridView dgv = null,List<int> list = null)
        {
            CritereType = type;
            idAct = id;
            mCurrentDataGridView = dgv;
            Idtype = SQLQueryBuilder.FindId("Activite", "IdType", "ID", idAct);
        }

        /// <summary>
        /// Load the datagridview with the right of feature 
        /// </summary>
        /// <param name="dgv1"></param>
        /// <param name="dgv2"></param>
        internal void LoadDataGridView(DataGridView dgv1, DataGridView dgv2 = null)
        {
            try
            {
                switch (CritereType)
                {
                    case "Avance":
                        LoadDataGridViewAvance(dgv1, dgv2);
                        break;
                    case "Prod Immo":
                        LoadDataGridViewProdImmo(dgv1, dgv2);
                        break;
                    case "PiedHa":
                        LoadDataGridViewPiedHa(dgv1);
                        break;
                    case "ProdPied":
                        LoadDataGridViewProdPied(dgv1);
                        break;
                    case "ChPied":
                        LoadDataGridViewChPied(dgv1);
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
        /// Load Ch/pied feature
        /// </summary>
        /// <param name="dgv"></param>
        private void LoadDataGridViewChPied(DataGridView dgv)
        {
            try
            {
                string query = "";
                switch (Idtype)
                {
                    case 1: //annuelle
                        query = CritereCultureQuery.ItemPiedMainQuery(idAct,"IdCharges","Charges");
                        break;
                    case 4: //pluriannuelle
                        query = CritereCultureQuery.ItemPiedMainQuery2(idAct, "IdCharges", "Charges");
                        break;
                    case 6: //perenne
                        query = CritereCultureQuery.ItemPiedMainQuery3(idAct, "IdCharges", "Charges");
                        break;
                    default: return;
                }
                table.Clear();
                table.Columns.Clear();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table);

                dgv.DataSource = table;
                dgv.RowHeadersVisible = false;
                dgv.AllowUserToAddRows = false;
                dgv.Columns[0].Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
}

        /// <summary>
        /// Load Prod Pied tab and gridview
        /// </summary>
        /// <param name="dgv"></param>
        private void LoadDataGridViewProdPied(DataGridView dgv)
        {
            try
            {
                string query = "";
                switch (Idtype)
                {
                    case 1: //annuelle
                        query = CritereCultureQuery.ItemPiedMainQuery(idAct, "IdProduits", "Produits");
                        break;
                    case 4: //pluriannuelle
                        query = CritereCultureQuery.ItemPiedMainQuery2(idAct, "IdProduits", "Produits");
                        break;
                    case 6: //perenne
                        query = CritereCultureQuery.ItemPiedMainQuery3(idAct, "IdProduits", "Produits");
                        break;
                    default: return;
                }

                table.Clear();
                table.Columns.Clear();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table);

                dgv.DataSource = table;
                dgv.RowHeadersVisible = false;
                dgv.AllowUserToAddRows = false;
                dgv.Columns[0].Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
}

        /// <summary>
        /// Load Pied Ha tab and grid view
        /// </summary>
        /// <param name="dgv"></param>
        private void LoadDataGridViewPiedHa(DataGridView dgv)
        {
            try
            {
                string query = "";
                switch (Idtype)
                {
                    case 1: //annuelle
                        query = CritereCultureQuery.PiedHaMainQuery(idAct);
                        break;
                    case 4: //pluriannuelle
                        query = CritereCultureQuery.PiedHaMainQuery2(idAct);
                        break;
                   case 6: //perenne
                        query = CritereCultureQuery.PiedHaMainQuery3(idAct);
                        break;
                    default: return;
                }
                 
                DataTable mytbl = new DataTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mytbl);

                if(mytbl.Rows.Count ==0)
                {
                    mytbl.Rows.Add();
                }

                dgv.DataSource = mytbl;
                dgv.RowHeadersVisible = true;
                dgv.AllowUserToAddRows = false;
                dgv.Columns[0].Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
}

        /// <summary>
        /// Load Avance feature ans grid view
        /// </summary>
        /// <param name="dgv1"></param>
        /// <param name="dgv2"></param>
        private void LoadDataGridViewAvance(DataGridView dgv1, DataGridView dgv2)
        {
            try
            { 

                DataTable table2 = new DataTable();

                string query = SQLQueryBuilder.SelectQuery("Avance", "Numero,Avant_1,_1,ID","Where numero <> '13' AND IdActivite = '"+idAct+"'");
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table2);

                if (table2.Rows.Count ==0)
                {
                    for (int i =1;i<=12;i++)
                    {
                        table2.Rows.Add(new object[] { i, 0, 0 });
                    }
                }

                dgv1.DataSource = table2;
                dgv1.Columns[0].Visible = false;
                dgv1.RowHeadersVisible = false;
                dgv1.AllowUserToAddRows = false;

                DataTable mytbl2 = new DataTable();
                mytbl2.Columns.Add("1");
                mytbl2.Columns.Add("2");
                mytbl2.Rows.Add((new object[] { "Avant 1", "" }));
                mytbl2.Rows.Add((new object[] { "1", "" }));

                dgv2.DataSource = mytbl2;
                dgv2.ColumnHeadersVisible = false;
                dgv2.RowHeadersVisible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Load grid view prod Immo
        /// </summary>
        /// <param name="dgv1"></param>
        /// <param name="dgv2"></param>
        private void LoadDataGridViewProdImmo(DataGridView dgv1, DataGridView dgv2)
        {
            try
            {

            DataTable mytbl = new DataTable();
            string query = SQLQueryBuilder.SelectQuery("ProdImmo", "type,Avant_1,_1,ID");
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
            adapter.Fill(mytbl);

            dgv1.DataSource = mytbl;
            dgv1.RowHeadersVisible = false;
            dgv1.AllowUserToAddRows = false;

            DataTable mytbl2 = new DataTable();
            mytbl2.Columns.Add("1");
            mytbl2.Columns.Add("2");
            mytbl2.Rows.Add((new object[] { "Avant 1", "" }));
            mytbl2.Rows.Add((new object[] { "1", "" }));

            dgv2.DataSource = mytbl2;
            dgv2.ColumnHeadersVisible = false;
            dgv2.RowHeadersVisible = false;
            dgv2.AllowUserToAddRows = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        /// <summary>
        /// Save data with the right feature
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="list"></param>
        internal void SaveData(DataGridView dgv,List<int> list )
        {
            try
            {
                switch (CritereType)
                {
                    case "Avance":
                       SaveDataAvance(dgv);
                        break;
                    case "Prod Immo":
                        SaveDataProdImmo(dgv);
                        break;
                    case "PiedHa":
                        SaveDataPied_Ha(dgv);
                        break;
                    case "ProdPied":
                        SaveDataProdPied(dgv);
                        break;
                    case "ChPied":
                        SaveDataChPied(dgv);
                        break;
                }
                //if (list != null)
                //{
                //    foreach (int i in list)
                //    {
                //        string query = SQLQueryBuilder.DeleteQuery(i, table);
                //        SQlQueryExecuter.RunQuery(query);
                //    }
                //}
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        /// <summary>
        /// Save Avance
        /// </summary>
        /// <param name="dgv"></param>
        private void SaveDataAvance(DataGridView dgv)
        {
            try
            { 
                string query;
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    query = "select * From Avance Where numero <> 13 And IdActivite = '" + idAct + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (list.Count == 12)
                    {
                        query = " Update Avance Set Avant_1 = '" + row.Cells[1].Value.ToString()
                           + "', _1 = '" + row.Cells[2].Value.ToString() +
                           "' WHERE numero = '" + row.Cells[0].Value.ToString() + 
                           "' AND IdActivite = '"+idAct+"';";
                    }
                    else
                    {
                        query = "Insert Into Avance (numero, IdActivite, Avant_1,_1)" +
                            "Values ('" + row.Cells[0].Value.ToString() + "','" + idAct + "','" +
                            row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() + "');";
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

        /// <summary>
        /// Save data Prod Immo
        /// </summary>
        /// <param name="dgv"></param>
        private void SaveDataProdImmo(DataGridView dgv)
        {
            string query;
            foreach (DataGridViewRow row in dgv.Rows)
            {
                query = " Update ProdImmo Set Avant_1 = '" + row.Cells[1].Value.ToString()
                    + "' , _1 = '" + row.Cells[2].Value.ToString() +
                    "' WHERE numero = '" + row.Cells[0].Value.ToString() + "';";
                SQlQueryExecuter.RunQuery(query);
            }

        }

        /// <summary>
        /// Save data Pied/ha
        /// </summary>
        /// <param name="dgv"></param>
        private void SaveDataPied_Ha(DataGridView dgv)
        {
            try
            {
                string query;

                foreach (int id in ListeRecordToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(id, "PiedHa");
                    SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    query = "select * From PiedHa Where ID = '" + row.Cells[0].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                   
                    if (Commun.ListHasValue(list))
                    {
                        switch (Idtype)
                        {
                            case 1:
                                query = CritereCultureQuery.PiedHaUpdateQuery(list[0], row);
                                break;
                            case 4:
                                query = CritereCultureQuery.PiedHaUpdateQuery2(list[0], row, dgv.ColumnCount);
                                break;
                            case 6:
                                query = CritereCultureQuery.PiedHaUpdateQuery3(list[0], row);
                                break;
                            default: return;
                        }
                    }
                    else
                    {
                        switch (Idtype)
                        {
                            case 1:
                                query = CritereCultureQuery.PiedHaInsertQuery(row,idAct);
                                break;
                            case 4:
                                query = CritereCultureQuery.PiedHaInsertfieldQuery2(dgv.ColumnCount) + " VALUES " + CritereCultureQuery.PiedHaInsertQuery2(row, idAct, dgv.ColumnCount);
                                break;
                            case 6:
                                query = CritereCultureQuery.PiedHaInsertQuery3(row, idAct);
                                break;
                            default: return;
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

        /// <summary>
        /// Save data Prod Pied
        /// </summary>
        /// <param name="dgv"></param>
        private void SaveDataProdPied(DataGridView dgv)
        {
            try
            { 
               
                string query;
                foreach (int id in ListeRecordToDelete)
                {
                    query = CritereCultureQuery.ItemPiedDeleteQuery(id,"IdProduits",idAct);
                    SQlQueryExecuter.RunQuery(query);
                    query = CritereCultureQuery.ItemPiedDeleteQueryCaractAct(id, "IdProduits", idAct);
                    SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();
                foreach (DataGridViewRow row in dgv.Rows)
                {
                    query = "select * From Item_Pied Where ID = '" + row.Cells[0].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        switch (Idtype)
                        {
                            case 1://annuelle
                                query = CritereCultureQuery.ItemPiedUpdateQuery(row,idAct,"IdProduits",list[0],"Produits");
                                break;
                            case 4://perenne
                                query = CritereCultureQuery.ItemPiedUpdateQuery2(row, idAct, "IdProduits", list[0], dgv.ColumnCount,"Produits");
                                break;
                            case 6://pluriannuelle
                                query = CritereCultureQuery.ItemPiedUpdateQuery3(row,idAct, "IdProduits", list[0], "Produits");
                                break;
                            default: return;
                        }
                    }
                    else
                    {
                        switch (Idtype)
                        {
                            case 1:
                                query = CritereCultureQuery.ItemPiedInsertQuery(row, idAct,"IdProduits","Produits");
                                break;
                            case 4:
                                query = CritereCultureQuery.ItemPiedInsertfieldQuery2(dgv.ColumnCount, "IdProduits") + " VALUES " + CritereCultureQuery.ItemPiedInsertQuery2(row, idAct, dgv.ColumnCount);
                                break;
                            case 6:
                                query = CritereCultureQuery.ItemPiedInsertQuery3(row, idAct, "IdProduits");
                                break;
                            default: return;
                        }
                    }
                    SQlQueryExecuter.RunQuery(query);
                }
                LoadDataGridViewChPied(dgv);
                CopyInCaractActivite("Produits", dgv);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// >Save data ChPied
        /// </summary>
        /// <param name="dgv"></param>
        private void SaveDataChPied(DataGridView dgv)
        {
            try
            { 
                string query;
                foreach (int id in ListeRecordToDelete)
                {
                    query = CritereCultureQuery.ItemPiedDeleteQuery(id, "IdProduits", idAct);
                    SQlQueryExecuter.RunQuery(query);
                    query = CritereCultureQuery.ItemPiedDeleteQueryCaractAct(id, "IdProduits", idAct);
                    SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();

                foreach (DataGridViewRow row in dgv.Rows)
                {
                    query = "select * From Item_Pied Where ID = '" + row.Cells[0].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        switch (Idtype)
                        {
                            case 1://annuelle
                                query = CritereCultureQuery.ItemPiedUpdateQuery(row,idAct,"IdCharges",list[0],"Charges");
                                break;
                            case 4://perenne
                                query = CritereCultureQuery.ItemPiedUpdateQuery2(row, idAct, "IdCharges", list[0], dgv.ColumnCount,"Charges");
                                break;
                            case 6://pluriannuelle
                                query = CritereCultureQuery.ItemPiedUpdateQuery3(row,idAct, "IdCharges", list[0], "Charges");
                                break;
                            default: return;
                        }
                    }
                    else
                    {
                        switch (Idtype)
                        {
                            case 1:
                                query = CritereCultureQuery.ItemPiedInsertQuery(row, idAct,"IdCharges","Charges");
                                break;
                            case 4:
                                query = CritereCultureQuery.ItemPiedInsertfieldQuery2(dgv.ColumnCount, "IdCharges") + " VALUES " + CritereCultureQuery.ItemPiedInsertQuery2(row, idAct, dgv.ColumnCount);
                                break;
                            case 6:
                                query = CritereCultureQuery.ItemPiedInsertQuery3(row, idAct, "IdCharges");
                                break;
                            default: return;
                        }
                    }
                    SQlQueryExecuter.RunQuery(query);
                }
                LoadDataGridViewChPied(dgv);
                CopyInCaractActivite("Charges",dgv);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void CopyInCaractActivite(string table, DataGridView dgv)
        {
            foreach (DataGridViewRow row in dgv.Rows)
            {
                int Id = SQLQueryBuilder.FindId(table, "ID", "Nom", row.Cells[1].Value.ToString());
                string query = "Select * From Caract_Activite Where IdActivite = '" + idAct +
                             "' AND Id" + table + " ='" + Id + "';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("Id" + table, query);
                if (!Commun.ListHasValue(list))
                {
                    query = "Insert into Caract_Activite (IdActivite,Id" + table + ") VALUES ('" + idAct + "','" + Id + "')";
                    SQlQueryExecuter.RunQuery(query);
                }
            }
        }

        /// <summary>
        /// Add cost or product to the right table charge 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dgv"></param>
        /// <param name="type"></param>
        internal void AddSelectedItem(int id, DataGridView dgv,string type)
        {
            try
            {
                mCurrentItemtId = id;
                string query;
                if (type == "Charges") query = ChargeQuery.AddSelectedChargeQuery2(id);
                else query = ProduitQuery.AddSelectedProduitQuery2(id);

                string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
                table.Rows.Add(rowvalue);


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Copy the value to other column
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonCopyClick(object sender, EventArgs e)
        {
            try
            {
                if (mCurrentDataGridView != null)
                {
                    string value = mCurrentDataGridView.CurrentCell.Value.ToString();
                    int currentRowIndex = mCurrentDataGridView.CurrentCell.RowIndex;
                    foreach (DataGridViewRow row in mCurrentDataGridView.Rows)
                    {
                        if (row.Index >= currentRowIndex)
                            row.Cells[mCurrentDataGridView.CurrentCell.ColumnIndex].Value = value;
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
        /// Remove the data of the selectedcell
        /// </summary>
        /// <param name="selectedCells"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        internal List<int> RemoveData(DataGridViewSelectedCellCollection selectedCells,string type)
        {
            try
            {
                foreach (DataGridViewCell cell in selectedCells)
                {
                    int index = cell.RowIndex;
                    if (!ListeRecordToDelete.Contains(GetId(index)))
                    {
                        ListeRecordToDelete.Add(GetId(index));
                    }
                    mCurrentDataGridView.Rows.RemoveAt(index);
                }
                
                return ListeRecordToDelete;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return null;
            }
        }

        /// <summary>
        /// Find the id of the row index send as parameter
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private int GetId(int index)
        {
            try
            {
                int NumColumn = 0;
                switch (CritereType)
                {
                    case "Avance":
                        NumColumn = 3;
                        break;
                    case "Prod Immo":
                        NumColumn = 3;
                        break;
                    case "PiedHa":
                        NumColumn = 0;
                        break;
                    case "ProdPied":
                        NumColumn = 1;
                        break;
                    case "ChPied":
                        NumColumn = 1;
                        break;
                }
                int id;
                int.TryParse(mCurrentDataGridView.CurrentRow.Cells[NumColumn].Value.ToString(), out id);
                return id;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return 0;
            }
           
        }
    }
}
