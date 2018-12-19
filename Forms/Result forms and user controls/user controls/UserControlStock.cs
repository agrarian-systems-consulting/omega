using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OMEGA.SQLQuery;
using System.Data.SQLite;
using System.Drawing.Drawing2D;

namespace OMEGA.Forms.ResultUserControl
{
    internal partial class UserControlStock : UserControl
    {

        private DataTable stockTable = new DataTable();
        private int mIdExpl = Commun.GetIdExpl();
        private int mIdProduit;
        private double mValueToReport;
        private Color color1 = Commun.GetColor("ARVB1");
        private Color color2 = Commun.GetColor("ARVB2");
        private Color color3 = Commun.GetColor("ARVB3");
        private ResultForm resultForm;
        internal UserControlStock(ResultForm form)
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            AddColumnToTable();
            LoadGrid();
            ManageColor();
            resultForm = form;
        }
        
        private void AddColumnToTable()
        {
            stockTable.Columns.Add("Produits");
            stockTable = AddYearColumn(stockTable);
        }

        private void LoadGrid()
        {
           try
           {
                string query = "Select * From Agri_StockIni Where IdExploitation = '" + mIdExpl + "';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("IdProduit", query);

                int idproduit = -1;
                foreach (int id in list)
                {
                    if (id != idproduit)
                    {
                        query = "Select * From Agri_StockIni Where IdExploitation = '" + mIdExpl +
                         "' AND IdProduit = '" + id + "';";
                        List<int> list2 = SQlQueryExecuter.RunQueryReaderInt("Prix", query);
                        foreach (int prix in list2)
                        {
                            string[] rowToAdd = new string[11];
                            query = "select Nom from Produits Where ID = '" + id + "';";
                            SQLiteDataReader reader = SQlQueryExecuter.RunQueryDataReader(query);
                            while (reader.Read())
                            {
                                if (prix == 0) rowToAdd[0] = reader.GetValue(0).ToString();
                                else rowToAdd[0] = "Prix Revient";

                            }
                            query = "Select An1,An2,An3,An4,An5,An6,An7,An8,An9,An10,IdProduit From Agri_StockIni " +
                                " Where IdExploitation = '" + mIdExpl +
                                "' And IdProduit = '" + id + "' And Prix = '" + prix + "';";
                            reader = SQlQueryExecuter.RunQueryDataReader(query);
                            while (reader.Read())
                            {
                                rowToAdd[1] = reader.GetValue(0).ToString();
                                rowToAdd[2] = reader.GetValue(1).ToString();
                                rowToAdd[3] = reader.GetValue(2).ToString();
                                rowToAdd[4] = reader.GetValue(3).ToString();
                                rowToAdd[5] = reader.GetValue(4).ToString();
                                rowToAdd[6] = reader.GetValue(5).ToString();
                                rowToAdd[7] = reader.GetValue(6).ToString();
                                rowToAdd[8] = reader.GetValue(7).ToString();
                                rowToAdd[9] = reader.GetValue(8).ToString();
                                rowToAdd[10] = reader.GetValue(9).ToString();
                            }
                            stockTable.Rows.Add(rowToAdd);
                        }
                    }
                    idproduit = id;
                }
                dataGridView1.DataSource = stockTable;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToOrderColumns = false;
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
                Commun.Setbackground(dataGridView1, color1, color2);
                string query = SQLQueryBuilder.UpdateColorQuery(color1, color2, color3);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
            }
        }

        private DataTable AddYearColumn(DataTable table)
        {
            try
            {
                string firstyear;
                string query = SQLQueryBuilder.SelectQuery("Agri_DefSim", "An_0", "Where IdExploitations = '" + mIdExpl + "'");
                string[] reader = SQlQueryExecuter.RunQueryReader(query);
                if (Commun.ArrayHasValue(reader))
                {
                    firstyear = reader[0];
                }
                else firstyear = DateTime.Now.Year.ToString();
                int firstyearint;
                int.TryParse(firstyear, out  firstyearint);
                firstyearint = firstyearint - 10; 
                for (int i = 0; i < 10; i++)
                {
                    table.Columns.Add(firstyearint.ToString());
                    firstyearint++;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            
            return table;
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "select ID,Nom From Produits";
                ListeForm listeform = new ListeForm(query, "Stock", "Stock", null, null, dataGridView1, stockTable);
                listeform.Show();
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SaveData()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    int prix;
                    if (row.Cells[0].Value.ToString() == "Prix Revient") prix = 1;
                    else
                    {
                        prix = 0;
                        mIdProduit = GetIdFromName(row.Cells[0].Value.ToString());
                    }

                    string query;
                    string mainquery = "select * from Agri_StockIni Where IdExploitation = '" + mIdExpl +
                        "' AND IdProduit = '" + mIdProduit + "' And Prix = '" + prix + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", mainquery);
                    if (!Commun.ListHasValue(list))
                    {
                        query = "Insert into Agri_StockIni (IdProduit,IdExploitation,An1,An2" +
                            ",An3,An4,An5,An6,An7,An8,An9,An10,Prix) VALUES  " +
                            "('" + mIdProduit + "','" + mIdExpl + "','" + row.Cells[1].Value.ToString() +
                            "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[3].Value.ToString() +
                            "','" + row.Cells[4].Value.ToString() + "','" + row.Cells[5].Value.ToString() +
                            "','" + row.Cells[6].Value.ToString() + "','" + row.Cells[7].Value.ToString() +
                            "','" + row.Cells[8].Value.ToString() + "','" + row.Cells[9].Value.ToString() +
                            "','" + row.Cells[10].Value.ToString() + "','" + prix + "');";
                    }
                    else
                    {
                        query = "Update Agri_StockIni Set An1 = '" + row.Cells[1].Value.ToString() +
                         "', An2 = '" + row.Cells[2].Value.ToString() + "', An3 = '" + row.Cells[3].Value.ToString() +
                         "', An4 = '" + row.Cells[4].Value.ToString() + "', An5 = '" + row.Cells[5].Value.ToString() +
                         "', An6 = '" + row.Cells[6].Value.ToString() + "', An2 = '" + row.Cells[7].Value.ToString() +
                         "', An8 = '" + row.Cells[8].Value.ToString() + "', An9 = '" + row.Cells[9].Value.ToString() +
                         "', An10 = '" + row.Cells[10].Value.ToString() + "' Where IdExploitation = '" + mIdExpl +
                         "' And IdProduit = '" + mIdProduit + "' And Prix = '" + prix + "';";
                    }
                    SQlQueryExecuter.RunQuery(query);
                    ManageColor();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveData();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            try
            {
                // The value we report is a surface, its initialized at -1, so if the parse failed, it stay -1
                // meaning that we dont want to report this value
                if (mValueToReport > 0)
                {
                    DataGridViewSelectedRowCollection ListuserRow = dataGridView1.SelectedRows;
                    // if (ListuserRow.Count == 1)
                    {
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            if (column.Index > dataGridView1.CurrentCell.ColumnIndex)
                            {
                                dataGridView1.CurrentRow.Cells[column.Index].Value = mValueToReport;
                            }
                        }
                    }
                    //else MessageBox.Show("Merci de reselectionner la cellule à copier");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
          
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
                double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out mValueToReport);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private int GetIdFromName(string produit)
        {
            string query = "Select ID From Produits Where Nom = '" + produit + "';";
            List<int> list=  SQlQueryExecuter.RunQueryReaderInt("ID", query);
            if (Commun.ListHasValue(list))
            {
                return list[0];
            }
           else return 0;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
                double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out mValueToReport);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void UserControlStock_Load(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
        }

        private void Home_Click(object sender, EventArgs e)
        {
           
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            resultForm.Close();
        }
    }
}
