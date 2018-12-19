using OMEGA.Forms.Territory_Forms;
using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class PointGPSForm : Form
    {
        private DataTable mGPSTable = new DataTable();
        private List<int> ListeRecordToDelete = new List<int>();
        internal PointGPSForm()
        {
            InitializeComponent();
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            string query = "Select * from GPS;";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
            adapter.Fill(mGPSTable);
            dataGridView1.DataSource = mGPSTable;
            dataGridView1.AllowUserToAddRows = false;
            RenameHeader();
        }

        private void RenameHeader()
        {
            int langue = Properties.Settings.Default.Langue;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].HeaderText = Translation.Translate("GPS point name", langue);
            dataGridView1.Columns[2].HeaderText = Translation.Translate("Lattitude", langue);
            dataGridView1.Columns[3].HeaderText = Translation.Translate("Longitude", langue);
            dataGridView1.Columns[4].HeaderText = Translation.Translate("Altitude", langue);
            dataGridView1.Columns[5].HeaderText = Translation.Translate("Date", langue);
            dataGridView1.Columns[6].HeaderText = Translation.Translate("Import Name", langue);
            dataGridView1.Columns[7].Visible = false;
            buttonremove.Text = Translation.Translate("Remove", langue);
            buttonadd.Text = Translation.Translate("Add", langue);

        }



        private void buttonadd_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                string[] valueToAdd = new string[8];
                valueToAdd[2] = "0";
                valueToAdd[3] = "0";
                valueToAdd[4] = "0";
                valueToAdd[5] = DateTime.Now.Date.ToString();
                valueToAdd[7] = "0";
                mGPSTable.Rows.Add(valueToAdd);
               
            }
            catch
            {

            }
            
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            try
            {
                int id = 0;
                string query = "";
                foreach (int item in ListeRecordToDelete)
                {
                     query = SQLQueryBuilder.DeleteQuery(item, "GPS");
                     SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    query = "Select * From GPS Where ID ='" + row.Cells[0].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);

                    if (Commun.ListHasValue(list))
                    {
                        query = "Update GPS set CODE = '" + row.Cells[1].Value.ToString() +
                        "', X ='" + row.Cells[2].Value.ToString() + "', Y = '" + row.Cells[3].Value.ToString() +
                        "', ALT = '" + row.Cells[4].Value.ToString() + "', DATE = '" + row.Cells[5].Value.ToString() +
                        "', import_name = '" + row.Cells[6].Value.ToString() + "', isImport = '" + row.Cells[7].Value.ToString() +
                        "' WHERE ID = '" + (id + 1) + "';";
                    }
                    else
                    {
                        query = "Insert Into GPS (CODE,X,Y,ALT,DATE,import_name,isImport) " +
                              " Values ('" + row.Cells[1].Value.ToString() +
                              "','" + row.Cells[2].Value.ToString() + "','" + row.Cells[3].Value.ToString() +
                              "','" + row.Cells[4].Value.ToString() + "','" + row.Cells[5].Value.ToString() +
                              "','" + row.Cells[6].Value.ToString() + "','" + row.Cells[7].Value.ToString() + "');";
                    }
                    SQlQueryExecuter.RunQuery(query);
                }
                this.Close();
            }
       
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
           
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonremove_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection UserSelectedRow = dataGridView1.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    if (!ListeRecordToDelete.Contains(GetId(index))) ListeRecordToDelete.Add(GetId(index));
                    dataGridView1.Rows.RemoveAt(index);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private int GetId(int index)
        {
            try
            {
                int id;
                int.TryParse(dataGridView1.Rows[index].Cells[0].Value.ToString(), out id);
                return id;
            }
            catch
            {
                return 0;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 0) return;
                FicheAttributForm ficheForm = new FicheAttributForm(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                //ficheForm.ShowDialog();

                if (ficheForm.DialogResult == DialogResult.OK)
                {
                    //ficheForm.SaveInfoFiche(0);
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
