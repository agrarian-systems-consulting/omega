using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class ChargeUsedForm : Form
    {

        private int mIdExpl = Commun.GetIdExpl();
        private int mIdCharge;
        internal ChargeUsedForm()
        {
            InitializeComponent();
        }

        private void ChargeUsedForm_Load(object sender, EventArgs e)
        {
            LoadFirstNodeTreeView();
            LoadSecondNodeTreeView();
            LoadCombobox();
            treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler(TreeViewNodeMouseClick);

        }

        private void ReloadValue()
        {
            SetIdCharge();
            LoadGridView();
        }

        private void LoadFirstNodeTreeView()
        {
            string query = "Select Nom From Def_Categ Where IdDefinitions = '2';";
            List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
            foreach(string item in list)
            {
                treeView1.Nodes.Add(item);
            }
        }

        private void LoadSecondNodeTreeView()
        {
            int index = 0;
            foreach(TreeNode node in treeView1.Nodes)
            {
                string query = "Select Charges.Nom From Charges" +
                    " Join Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg" +
                    " Where Def_Categ.Nom  = '"+node.Text+"';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string item in list)
                {
                    treeView1.Nodes[index].Nodes.Add(item);
                }
                index++;
            }
        }

        private void LoadCombobox()
        {
            string firstyear;
            string query = SQLQueryBuilder.SelectQuery("Agri_DefSim", "An_0","Where IdExploitations = '"+ mIdExpl + "'");
            string [] reader = SQlQueryExecuter.RunQueryReader(query);
            if (Commun.ArrayHasValue(reader))
            {
                firstyear = reader[0];
            }
            else firstyear = DateTime.Now.Year.ToString();
            int firstyearint;
            int.TryParse(firstyear, out firstyearint);
            int[] listyear = new int[10];
            for (int i = 0;i<10;i++)
            {
                listyear[i] = firstyearint;
                firstyearint++;
            }
            comboBoxYear.DataSource = listyear;
            comboBoxYear.Text = firstyear;
        }

        private void TreeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == getLastNode(e.Node))
            {
                textBoxusedof.Text = e.Node.Text;
                FillTextBoxUnity(e.Node.Text);
                ReloadValue();
            } 
        }

        private void FillTextBoxUnity(string charge)
        {
            string query = "Select UEnt From SystemeUnite" +
                " Join Charges on Charges.IdSysUnit = SystemeUnite.IdSysUnit" +
                " Where Charges.Nom = '" + charge + "';";
            List<string> list = SQlQueryExecuter.RunQueryReaderStr("UEnt", query);
            if (Commun.ListHasValue(list))
            {
                textBoxunity.Text = list[0];
            }
        }

        private void LoadGridView()
        {
            DataTable table = new DataTable();
            string query = "Select distinct Activite.ID, Activite.Nom as 'Culture' From Activite" +
                " Join Caract_Activite on Caract_Activite.IdActivite = Activite.ID " +
                " Join Charge_Quantite on Charge_Quantite.IdActivite = Activite.ID" +
                " Where Caract_Activite.IdCharges = '" + mIdCharge + "';";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
            adapter.Fill(table);

            table = AddAreaColumn(table);

            table = AddQtyColumn(table);

            table = AddQtyTotalColumn(table);
            
            dataGridView1.DataSource = table;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.RowHeadersVisible = false;

        }

        private DataTable AddQtyTotalColumn(DataTable table)
        {
            double surface = 0;
            double qte = 0;
            table.Columns.Add("Quantité");
            foreach (DataRow row in table.Rows)
            {
                surface = Commun.GetDoubleFromString(row.ItemArray[2].ToString());
                qte = Commun.GetDoubleFromString(row.ItemArray[4].ToString());
                row.SetField<double>("Quantité", surface * qte);

            }

            return table;
        }

        private DataTable AddAreaColumn(DataTable table)
        {
            try
            {
                table.Columns.Add("Surface");
                int index = comboBoxYear.SelectedIndex + 1;
                if (index == 0) index = 1;
                foreach (DataRow row in table.Rows)
                {
                    string query = "Select An" + index + " From Agri_Assol " +
                        " Where IdExploitations = '" + mIdExpl + "' And IdActivite = '" +
                        row.ItemArray[0].ToString() + "';";
                    List<double> list = SQlQueryExecuter.RunQueryReaderDouble("An" + index, query);
                    if (Commun.ListHasValue(list))
                    {
                        row.SetField<double>("Surface", list[0]);
                    }
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return table;
        }

        private DataTable AddQtyColumn(DataTable table)
        {
            try
            {
                table.Columns.Add("Qté/ha Avt");
                table.Columns.Add("Qté/ha");

                foreach (DataRow row in table.Rows)
                {
                    string query = "Select Charge_Quantite.Quantite_Avant_1,Charge_Quantite.Quantite_1 From Charge_Quantite" +
                    " Where IdCharges = '" + mIdCharge + "' AND IdActivite = '" + row.ItemArray[0].ToString() + "';";
                    List<double> list = SQlQueryExecuter.RunQueryReaderDouble("Quantite_Avant_1", query);
                    if (Commun.ListHasValue(list))
                    {
                        row.SetField<double>("Qté/ha Avt", list[0]);
                    }
                    list = SQlQueryExecuter.RunQueryReaderDouble("Quantite_1", query);
                    if (Commun.ListHasValue(list))
                    {
                        row.SetField<double>("Qté/ha", list[0]);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return table;
        }

        private TreeNode getLastNode(TreeNode subroot)
        {
            if (subroot.Nodes.Count == 0)
                return subroot;

            return getLastNode(subroot.Nodes[subroot.Nodes.Count - 1]);
        }
        
        private void SetIdCharge()
        {
            try
            {
                string query = "Select ID From Charges Where Nom = '" + textBoxusedof.Text + "';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (Commun.ListHasValue(list))
                {
                    mIdCharge = list[0];
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void comboBoxYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadValue();
        }
    }
}
