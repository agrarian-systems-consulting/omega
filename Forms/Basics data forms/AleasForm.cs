using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    /// <summary>
    /// Classe Aléas permet de faire varier les résultats en fois qu'ils sont modélisés
    /// </summary>
    internal partial class AleasForm : Form
    {
        private int langue = Properties.Settings.Default.Langue;
        private bool mIsPrice;
        private List<int> ListRecordToDelete = new List<int>();
        private string table;
        DataTable AleaTable = new DataTable();
        private int currentIdAleaCateg;

        internal AleasForm(bool isPrice)
        {
            try
            {
                InitializeComponent();
                mIsPrice = isPrice;
                LoadFirstNode();
                LoadSecondNode();
                LoadAleas();
                this.Size = new Size(340, 496);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadFirstNode()
        {
            try
            { 
                treeView1.Nodes.Add(Translation.Translate("Product", langue));
                treeView1.Nodes.Add(Translation.Translate("Cost", langue));
                if (!mIsPrice)
                {
                    treeView1.Nodes.Add(Translation.Translate("Externalities", langue));
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadSecondNode()
        {
            try
            { 
                for (int i = 0; i <= treeView1.Nodes.Count-1;i++)
                {
                    treeView1.Nodes[i].Nodes.Add(Translation.Translate("Trending", langue));
                    treeView1.Nodes[i].Nodes.Add(Translation.Translate("Scenarii", langue));
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadAleas()
        {
            try
            { 
                foreach (TreeNode FirstNode in treeView1.Nodes)
                {
                    foreach (TreeNode SecondNode in FirstNode.Nodes)
                    {
                        int IdAleas = GetIdAleas(FirstNode.Text, SecondNode.Text);
                        string query = SQLQueryBuilder.SelectQuery("Alea_Categ","Nom","Where IdAleas = '" + IdAleas + "'");
                        List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        if (Commun.ListHasValue(list))
                        {
                            foreach (string item in list)
                            {
                                SecondNode.Nodes.Add(item);
                            }
                        }
                    }
                }
                foreach (TreeNode FirstNode in treeView1.Nodes)
                {
                    foreach (TreeNode SecondNode in FirstNode.Nodes)
                    {
                        SecondNode.Nodes.Add("[...]");
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private int GetIdAleas(string FirstNodeName, string SecondNodeName)
        {
            int id = 0;
            try
            {

            
                string trending = Translation.Translate("Trending", langue);
                if (FirstNodeName.Contains(Translation.Translate("Product", langue)))
                {
                    if (mIsPrice)
                    {
                        if (SecondNodeName == trending) id = 1;
                        else id = 3;
                    }
                    else
                    {
                        if (SecondNodeName == trending) id = 5;
                        else id = 7;
                    }
               
                }
                else if (FirstNodeName.Contains(Translation.Translate("Cost", langue)))
                {
                    if (mIsPrice)
                    {
                        if (SecondNodeName == trending) id = 2;
                        else id = 4;
                    }
                    else
                    {
                        if (SecondNodeName == trending) id = 6;
                        else id = 8;
                    }
                }
                else if (FirstNodeName.Contains(Translation.Translate("Externalities", langue)))
                {
                    if (SecondNodeName == trending) id = 9;
                    else id = 10;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return id;
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node.Text == "[...]")
                {
                    string NewItemName = "";
                    if (Commun.InputBox(Translation.Translate("Add new item", langue),
                        Translation.Translate("Add new ", langue) +
                        Translation.Translate(e.Node.Parent.Text, langue)
                        + " in " + Translation.Translate(e.Node.Parent.Parent.Text, langue),
                        ref NewItemName) == DialogResult.OK)
                    {
                        string query;
                        if (NewItemName != "")
                        {
                            query = "Insert Into Alea_Categ (Nom,IdAleas) " +
                            "VALUES ('" + NewItemName + "','" + GetIdAleas(e.Node.Parent.Parent.Text, e.Node.Parent.Text) + "');";
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                }
                else if (e.Node.Text != "" && e.Node.Equals(getLastNode(e.Node)))
                {
                    table = Translation.Translate(e.Node.Parent.Parent.Text, 1);
                    if (table.LastIndexOf('s') == -1)
                    {
                        table = table + "s";
                    }
                    currentIdAleaCateg = SQLQueryBuilder.FindId("Alea_Categ", "ID", "Nom", e.Node.Text);
                    LoadAleaGrid();
                    panelDatagrid.Visible = true;
                    this.Size = new Size(1182, 496);
                }
            }

             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private void LoadAleaGrid()
        {
            try
            {
                AleaTable.Clear();
                string query = AleasQuery.GetMainQuery(mIsPrice,table,currentIdAleaCateg,Get100An());
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(AleaTable);
                dataGridView1.DataSource = AleaTable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Visible = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                //dataGridView1.Columns[2].Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void GetBase(string table,string nom,string activite)
        {
            string query = "";
            string tableqte;
            if (mIsPrice)
            {
                query = "Select Prix From " + table + " Where Nom = '" + nom + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Prix", query);
            }
            else
            {
                if (table == "Produits") tableqte = "Prod";
                else tableqte = "Charge";

                query = "Select Quantite_1 From " + tableqte + "_Quantite" +
                    " Join " + table + " on Id" + table + "= " + table + ".ID" +
                    " WHERE ";
            }
        }

        private string Get100An()
        {
            string An = "";
            for (int i = 1; i <= 100; i++)
            {
                An = An + "An" + i + ",";
            }
            int index = An.LastIndexOf(',');
            An = An.Remove(index);
            return An;
        }

        private TreeNode getLastNode(TreeNode subroot)
        {
            if (subroot.Nodes.Count == 0)
                return subroot;

            return getLastNode(subroot.Nodes[subroot.Nodes.Count - 1]);
        }

        private void buttonCopyIdentique_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns )
                {
                    if (column.Index >= dataGridView1.CurrentCell.ColumnIndex && column.Index > 2)
                    {
                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[column.Index].Value = dataGridView1.CurrentCell.Value.ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonCopy123_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    if (column.Index >= dataGridView1.CurrentCell.ColumnIndex && column.Index >2)
                    {
                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[column.Index].Value = (column.Index - 4);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonCopy0_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    if (column.Index >= dataGridView1.CurrentCell.ColumnIndex && column.Index > 2)
                    {
                        dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[column.Index].Value = "";
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    query = " Select * From Alea_Item Where ID = '" + row.Cells[0].Value.ToString()+"';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        query = AleasQuery.UpdateQuery(UpdateAn(row), list[0]);
                    }
                    else
                    {
                        query = AleasQuery.InsertQuery(table, InsertAn1(row), row, currentIdAleaCateg, InsertAn2(row));
                    }
                    SQlQueryExecuter.RunQuery(query);

                }
                foreach (int i in ListRecordToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(i, "Alea_Item");
                    SQlQueryExecuter.RunQuery(query);
                }
                ListRecordToDelete.Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        
        private string UpdateAn(DataGridViewRow row)
        {
            string update = "";

            for (int i = 1; i<= 100;i++)
            {

                update = update + "An" + i + "= '" + row.Cells[i + 5].Value.ToString() + "'," ;
            }
            update = update.Remove(update.Length - 1, 1);
            return update;
        }

        private string InsertAn1(DataGridViewRow row)
        {
            string insert = "";

            for (int i = 1; i <= 100; i++)
            {
                insert = insert + "An"+ i  + ",";
            }
            insert = insert.Remove(insert.Length - 1, 1);
            return insert;
        }
        
        private string InsertAn2(DataGridViewRow row)
        {
            string insert = "";

            for (int i = 1; i <= 100; i++)
            {
                insert = insert + "'" + row.Cells[i + 4].Value.ToString() + "',";
            }
            insert = insert.Remove(insert.Length - 1, 1);
            return insert;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection UserSelectedRow = dataGridView1.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    ListRecordToDelete.Add(GetId(index));
                    dataGridView1.Rows.RemoveAt(index);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private int GetId(int rowindex)
        {
            int id = 0;
            try
            {
              int.TryParse(dataGridView1.Rows[rowindex].Cells[0].Value.ToString(), out id);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            buttonSave_Click(this, e);
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Export.RunExportTable(dataGridView1, "Aléas");
        }

        private void buttonNote_Click(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("Alea_Item");
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonProdChar_Click(object sender, EventArgs e)
        {
            try
            {
                if (mIsPrice)
                {
                    string query = "Select ID,Nom From " + table + ";";
                    ListeForm form = new ListeForm(query, "Alea", table, null, null, dataGridView1, AleaTable);
                    form.ShowDialog();
                       
                }
                else
                {
                    string query = "Select ID,Nom From " + table + ";";
                    ListeForm form = new ListeForm(query, "Alea2", table, null, null, dataGridView1, AleaTable);
                    form.ShowDialog();

                    foreach (int ID in form.listIdAlea)
                    {
                        query = AleasQuery.SelectActiviteQuery(table, ID);
                        List<string> list = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                        if (Commun.ListHasValue(list))
                        {

                            query = AleasQuery.SelectAleaID(table, list[0], currentIdAleaCateg, Get100An());
                               
                            string[] reader = SQlQueryExecuter.RunQueryReader(query);
                            for(int i = 0; i<reader.Length-1;i++)
                            {
                                if (i == 1) reader[i] = ID.ToString();
                                if (i == 2) reader[i] = SQLQueryBuilder.FindName(table, "Nom", "Id", ID);
                                if (i == 3) reader[i] = SQLQueryBuilder.FindName("Activite", "Nom", "Id", list[0]);
                                if (i >= 5) reader[i] = "100";
                            }
                            AleaTable.Rows.Add(reader);
                        }
                    }
                }
            }
            catch
            {

            }
        }
    }
}
