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
    internal partial class UserControlVariables : UserControl
    {
        private int mIdExpl = Commun.GetIdExpl();
        private DataTable tableVariable = new DataTable();
        private double mValueToReport;
        private Color color1 = Commun.GetColor("ARVB1");
        private Color color2 = Commun.GetColor("ARVB2");
        private Color color3 = Commun.GetColor("ARVB3");
        private int langue = Properties.Settings.Default.Langue;
        private ResultForm resultForm;

        internal UserControlVariables(ResultForm form)
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            LoadDataGridView();
            ManageColor();
            RenameHeader();
            Translate();
            resultForm = form;
        }

        private void LoadDataGridView()
        {
            string query = "Select Variable.ID,Variable.nom,Variable.Categorie, " + SelectAn() + " From Agri_Variable" +
                " Join Variable on Variable.ID = Agri_Variable.IdVariable ;";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
            adapter.Fill(tableVariable);

            dataGridView1.DataSource = tableVariable;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.Columns[0].Visible = false;

        }

        private void Translate()
        {
            try
            {
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                //buttonDico.Text = Translation.UpdateCaption("Dico", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
                //buttonAdd1.Text = Translation.UpdateCaption("Add", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private void RenameHeader()
        {
            try
            {
                int year = Commun.GetYear();
                int index = 0;
                for(int i = 3;i<13;i++)
                {
                    dataGridView1.Columns[i].HeaderText = (year + index).ToString();
                    index++;
                }
                dataGridView1.Columns[1].HeaderText = Translation.Translate("Name", langue);
                dataGridView1.Columns[2].HeaderText = Translation.Translate("Categorie", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private string SelectAn()
        {
            string AN = "";
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    AN = AN + "An" + i + ",";
                }
                int index = AN.LastIndexOf(',');
                AN = AN.Remove(index);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return AN;
        }

        private void buttonVariable_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Select ID,Nom From Variable";
                ListeForm listeForm = new ListeForm(query, "variable", "", null, null, dataGridView1, tableVariable);
                listeForm.Show();
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
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
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
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
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
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            string query = "";

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                query = "Select ID from Agri_Variable Where IdExploitation = '" + mIdExpl +
                         "' AND IdVariable = '" + row.Cells[0].Value.ToString() + "';";
                List<int> list= SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (Commun.ListHasValue(list))
                {
                    query = "Update Agri_Variable Set " + Update10An(row) + " WHERE ID ='" + list[0]+"';";
                }
                else
                {
                    query = "Insert into Agri_Variable (IdExploitation,IdVariable," + Get10An() + ")" +
                     "Values ('" + mIdExpl + "','" + row.Cells[0].Value.ToString() + "'," + Insert10An(row) + ");";
                }
                SQlQueryExecuter.RunQuery(query);
            }

          
        }

        private string Get10An()
        {
            string An = "";
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    An = An + "An" + i + ",";
                }
                int index = An.LastIndexOf(',');
                An = An.Remove(index);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return An;
        }

        private string Update10An(DataGridViewRow row)
        {
            string An = "";
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    An = An + "An" + i + "='" + row.Cells[i + 2].Value.ToString() + "',";
                }
                int index = An.LastIndexOf(',');
                An = An.Remove(index);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return An;
        }

        private string Insert10An(DataGridViewRow row)
        {
            string An = "";
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    An = An + "'" + row.Cells[i + 2].Value.ToString() + "',";
                }
                int index = An.LastIndexOf(',');
                An = An.Remove(index);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return An;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            resultForm.Close();
        }
    }
}
