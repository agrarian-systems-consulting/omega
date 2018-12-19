using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using OMEGA.SQLQuery;
using System.Drawing.Drawing2D;

namespace OMEGA.Forms.ResultUserControl
{
    internal partial class UserControlImmoPetitMateriel : UserControl
    {
        private DataTable tableImmo = new DataTable();
        private int mIdExp = Commun.GetIdExpl();
        private ResultForm resultForm;

        internal UserControlImmoPetitMateriel(ResultForm form)
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            LoadDataGrid();
            resultForm = form;
        }

        private void LoadDataGrid()
        {
            try
            {
                string query = "Select Year,Value,Nom From Immo_PetitMateriel Join TVA on TVA.IdTva = Immo_PetitMateriel.IdTVA;";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(tableImmo);

                query = SQLQueryBuilder.SelectQuery("Agri_DefSim", "An_0", "Where IdExploitation = '" + mIdExp + "'");
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
                int year = 0;
                if (Commun.ListHasValue(list))
                {
                    year = list[0];
                }
                else year = DateTime.Now.Year;

                while (tableImmo.Rows.Count < 10)
                {
                    tableImmo.Rows.Add();
                }
              
                SetValueRow(year);

                dataGridView1.DataSource = tableImmo;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToOrderColumns = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetValueRow(int year)
        {
            try
            {
                foreach (DataRow row in tableImmo.Rows)
                {
                    row.SetField<int>("Year", year);
                    year++;
                    string query = "Select * From Immo_PetitMateriel Where IdExploitation = '" + mIdExp + "' and Year ='" +
                      row.ItemArray[0].ToString() + "';";
                    List<string> list = SQlQueryExecuter.RunQueryReaderStr("Value", query);
                    if (Commun.ListHasValue(list)) row.SetField<string>("Value", list[0]);
                    else row.SetField<string>("Value"," ");
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
                if (e.ColumnIndex == 2)
                {
                    double decalage = 1;
                    List<string> list = new List<string>();
                    string query = "";
                    ContextMenu contextmenu = new ContextMenu();
                    List<MenuItem> ListItem = new List<MenuItem>();

                    query = "Select Nom From TVA;";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);

                    foreach (string item in list)
                    {
                        MenuItem menuitem = new MenuItem(item);
                        ListItem.Add(menuitem);
                    }
                    foreach (MenuItem item in ListItem)
                    {
                        contextmenu.MenuItems.Add(item);
                    }

                    dataGridView1.ContextMenu = contextmenu;
                    Rectangle MyCell = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                    int x = (int)Math.Ceiling(MyCell.X * decalage);
                    int y = (int)Math.Ceiling(MyCell.Y * decalage);
                    Point point = new Point(x, y);
                    contextmenu.Show(dataGridView1, point);

                    foreach (MenuItem item in ListItem)
                    {
                        item.Click += Item_Click;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            try
            {
                string[] array = sender.ToString().Split(new string[] { "Text:" }, StringSplitOptions.None);
                dataGridView1.CurrentCell.Value = array[1].Trim();
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
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string query = "";
                    int IdTVA = SQLQueryBuilder.FindId("TVA", "IdTVa", "Nom", row.Cells[2].Value.ToString());
                     query = "Select * From Immo_PetitMateriel Where IdExploitation = '" + mIdExp + "' AND Year ='" +
                        row.Cells[0].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        query = "Update Immo_Petitmateriel Set Value = '" + row.Cells[1].Value.ToString() + "'" +
                            " ,IdTVA = '" + IdTVA + "' Where Id = '" + list[0] + "';";
                    }
                    else
                    {
                        query = "insert into Immo_PetitMateriel  (IdExploitation,Value,Year,IdTVA) VALUES ('" +
                            mIdExp + "','" + row.Cells[1].Value.ToString() + "','" + row.Cells[0].Value.ToString() + "','" + IdTVA + "'); ";
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

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            resultForm.Close();
        }
    }
}
