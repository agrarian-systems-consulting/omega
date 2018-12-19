
using OMEGA.Data_Classes;
using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    /// <summary>
    /// Ce fenêtre permet de voir la liste d'un élément lorsqu'il est chargé.
    /// </summary>
    internal partial class ListeForm : Form
    {
        internal bool ValueAdded = false;
        internal List<string> ListProduct = new List<string>();
        private DataTable dataTable = new DataTable();
        private ActivityMainForm activite;
        private ExploitationMainForm exploitation;
        private string table;
        private string currentTab;
        private DataGridView currentGridView;
        private DataTable currentdataTable;
        private int id;
        private int mIdAct;
        internal List<int> listIdAlea = new List<int>();

        /// <summary>
        /// COnstruteur pour voir la liste des elements à rajouter
        /// </summary>
        /// <param name="mainQuery"></param>
        /// <param name="exploit"></param>
        /// <param name="tbl"></param>
        internal ListeForm(string mainQuery, string tbl,string tab, ActivityMainForm act, ExploitationMainForm exploit,DataGridView dvg,
            DataTable dtb = null,Produit prod = null, Charge chge = null,int IdAct = 0)
        {
            InitializeComponent();
            exploitation = exploit;
            activite = act;
            currentTab = tab;
            table = tbl;
            currentGridView = dvg;
            currentdataTable = dtb;
            mIdAct = IdAct;
            LoadGridView(mainQuery);
        }

        private void LoadGridView(string mainQuery)
        {
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToOrderColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out id);
                switch (table)
                {
                    case "Stock":
                        AddProduitToStock();
                        break;
                    case "phase":
                        AddProduitToPhase();
                        break;
                    case "variable":
                        AddVariable();
                        break;
                    case "Produits":
                        if (activite != null)
                        {
                            if (activite.Produit == null)
                            {
                                activite.CritereCulture.AddSelectedItem(id, currentGridView, "Produits");
                            }
                            else 
                            {
                                activite.Produit.AddSelectedProduct(id, currentTab, currentGridView);
                           
                            }
                        } 
                        else if (exploitation == null) AddProduitToAgriProduit();
                        if (exploitation != null) exploitation.Produit.AddSelectedProduct(id, currentTab, currentGridView);
                        else if (activite == null) AddProduitToAgriProduit();
                        break;
                    case "Charges":
                        if (activite != null)
                        {
                            if (activite.Charge == null)
                            {
                                activite.CritereCulture.AddSelectedItem(id, currentGridView, "Charges");
                            }
                            else
                            {
                                activite.Charge.AddSelectedCharge(id, currentTab, currentGridView);
                            }
                        } 
                        else if (exploitation == null)
                        {
                            AddChargeToAgriCharge();
                            break;
                        }
                        if (exploitation != null) exploitation.Charge.AddSelectedCharge(id, currentTab, currentGridView);
                        else if (activite == null)
                        {
                            AddChargeToAgriCharge();
                            break;
                        }
                        break;
                    case "Externalites":
                        if (activite != null) activite.Externalite.AddSelectedExternalite(id);
                        if (exploitation != null) exploitation.Externalite.AddSelectedExternalite(id);
                        break;
                    case "Activite":
                        string query = ActivityQuery.AddSelectedActiviteQuery(id);
                        string[] reader = SQlQueryExecuter.RunQueryReader(query);
                        if (reader != null)
                        {
                            if (reader.Length > 1)
                            {
                                currentdataTable.Rows.Add(reader);
                            }
                        }
                        break;
                    case "Family":
                        if (exploitation != null) exploitation.Family.AddSelectedFamilyt(id, currentTab, currentGridView);
                        else if (activite == null) AddProduitToAgriProduit();
                        break;
                    case "Alea":
                        AddItemToAlea();
                        break;
                    case "Alea2":
                        AddItemToAlea2();
                        break;
                    case "Expense_Income":
                        AddExpenseIncome();
                        break;
                    case "ProdPied":
                        AddProduitPied();
                        break;
                    case "ChargesPied":
                        AddChargePied();
                        break;
                    case "ChargesCharges":
                        AddChargesCharges();
                        break;
                    case "ProduitsCharges":
                        AddProduitsCharges();
                        break;
                }
                ValueAdded = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void AddChargesCharges()
        {
            int idtype = SQLQueryBuilder.FindId("Activite", "ID", "IdType", mIdAct);
            string query = CritereCultureQuery.AddItemToGridQuery(id, idtype, "Charges", "IdCharges", mIdAct);
            string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
            currentdataTable.Rows.Add(rowvalue);
        }

        private void AddProduitsCharges()
        {
            int idtype = SQLQueryBuilder.FindId("Activite", "ID", "IdType", mIdAct);
            string query = CritereCultureQuery.AddItemToGridQuery(id, idtype, "Charges", "IdCharges", mIdAct);
            string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
            currentdataTable.Rows.Add(rowvalue);
        }


        private void AddChargePied()
        {
            int idtype = SQLQueryBuilder.FindId("Activite", "ID", "IdType", mIdAct);
            string query = CritereCultureQuery.AddItemToGridQuery(id, idtype, "Charges", "IdCharges", mIdAct);
            string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
            currentdataTable.Rows.Add(rowvalue);
        }

        private void AddProduitPied()
        {
            int idtype = SQLQueryBuilder.FindId("Activite", "ID", "IdType", mIdAct);
            string query = CritereCultureQuery.AddItemToGridQuery(id, idtype, "Produits", "IdProduits", mIdAct);
            string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
            currentdataTable.Rows.Add(rowvalue);
        }

        private void AddExpenseIncome()
        {
            string query = Expense_IncomeQuery.AddItemQuery(id);
            string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
            currentdataTable.Rows.Add(rowvalue);
        }


        private void AddItemToAlea()
        {
            string query = SQLQueryBuilder.AddItemToAleaQuery(currentTab, id);
            string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
            currentdataTable.Rows.Add(rowvalue);
        }
        private void AddItemToAlea2()
        {
            string query = SQLQueryBuilder.AddItemToAleaQuery(currentTab, id);
            string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
            listIdAlea.Add(id);
        }

        private void AddProduitToAgriProduit()
        {
            string query = ProduitQuery.AddSelectedProduitQuery3(id);
            string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
            currentdataTable.Rows.Add(rowvalue);
        }

        internal void AddProduitToStock()
        {
            string query = ProduitQuery.AddSelectedProduitQuery4(id);
            string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
            currentdataTable.Rows.Add(rowvalue);
            rowvalue[0] = "Prix Revient";
            currentdataTable.Rows.Add(rowvalue);
        }

        internal void AddVariable()
        {
            string query = VariableQuery.AddSelectedVariableQuery(id);
            string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
            currentdataTable.Rows.Add(rowvalue);
        }

        internal void AddProduitToPhase()
        {
            string query = ProduitQuery.AddSelectedProduitQuery5(id);
            string[] rowvalue =   SQlQueryExecuter.RunQueryReader(query);
            currentdataTable.Rows.Add(rowvalue);
        }

        private void AddChargeToAgriCharge()
        {
            string query = ChargeQuery.AddSelectedChargeQuery3(id);
            string[] rowvalue = SQlQueryExecuter.RunQueryReader(query);
            currentdataTable.Rows.Add(rowvalue);
        }
    }
}
