using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using OMEGA.SQLQuery;

namespace OMEGA.Forms.ResultUserControl
{
    internal partial class UserControlImmoGlobal : UserControl
    {
        private int langue = Properties.Settings.Default.Langue;
        private int mIdExp = Commun.GetIdExpl();
        private DataTable TableImmo = new DataTable();
        private double mValueToReport;
        private Color color1 = Commun.GetColor("ARVB1");
        private Color color2 = Commun.GetColor("ARVB2");
        private Color color3 = Commun.GetColor("ARVB3");

        internal UserControlImmoGlobal()
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            LoadDataGrid();
        }

        private void Translate()
        {
            try
            {
                buttonCancel.Text = Translation.Translate("Cancel", langue);
               //button.Text = Translation.UpdateCaption("Remove", langue);
                // buttonDico.Text = Translation.UpdateCaption("Dico", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
               // buttonAdd1.Text = Translation.UpdateCaption("Add", langue);
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadDataGrid()
        {
            try
            {
                TableImmo.Columns.Add(Translation.Translate("Name", langue));
                string query = SQLQueryBuilder.SelectQuery("Agri_DefSim", "An_0", "Where IdExploitations = '" + mIdExp + "'");
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
                int year = 0;
                if (Commun.ListHasValue(list))
                {
                    year = list[0];
                }
                else year = DateTime.Now.Year;
                for (int i = 1; i <= 10; i++)
                {
                    TableImmo.Columns.Add(year.ToString());
                    year = year + 1;
                }
                for (int i = 1; i <= 7; i++)
                {
                    TableImmo.Rows.Add();
                }
                SetRowValue();
                dataGridView1.DataSource = TableImmo;
                dataGridView1.Columns[0].Width = 100;
                dataGridView1.AllowUserToAddRows = false;
                ManageColor();
                SetValeurAmmor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetRowValue()
        {
            try
            {
                string query = "";
                int index = 0;
                string An = "An1";
                int numAn = 1;
                int columnindex = 0;
                query = SQLQueryBuilder.SelectQuery("Item_ImmoGlobale", "Name", "Where ID = '1' OR ID ='2' OR ID = '3' OR ID = '4' OR ID ='5' OR ID = '6' OR ID = '7'");
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Name", query);
                foreach (DataRow row in TableImmo.Rows)
                {
                    row.SetField<string>(Translation.Translate("Name", langue), Translation.Translate(list[index], langue));
                    columnindex = 0;
                    An = "An1";
                    numAn = 1;
                    foreach (DataColumn column in TableImmo.Columns)
                    {
                        int year;
                        if (int.TryParse(column.ColumnName, out year))
                        {
                            if (An.Length == 3)
                            {
                                An = An.Remove(2, 1);
                                An = An + numAn.ToString();
                                numAn++;
                            }
                            query = "Select * From Agri_ImmoGlobal Where IdExploitation = '" + mIdExp + "' And NoItem = '" + (index + 1) + "';";
                            List<string> list2 = SQlQueryExecuter.RunQueryReaderStr(An, query);
                            if (list2.Count >= 1)
                            {
                                if (list2[0] == "") list2[0] = " ";
                                row.SetField<string>(year.ToString(), list2[0]);
                            }
                        }
                        columnindex++;
                    }
                    index++;
                }
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
                string query = SQLQuery.SQLQueryBuilder.UpdateColorQuery(color1, color2, color3);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                int index = 1;
                string An = "An1";
                int numAn = 1;
                int columnindex = 0;
                query = SQLQueryBuilder.SelectQuery("Item_ImmoGlobale", "Name", "Where ID = '1' OR ID = '2' OR ID = '3' OR ID = '4' OR ID = '5' OR ID = '6' OR ID = '7'");
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Name", query);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    columnindex = 0;
                    An = "An1";
                    numAn = 1;
                    foreach (DataColumn column in TableImmo.Columns)
                    {
                        int year;
                        if (int.TryParse(column.ColumnName, out  year))
                        {
                            if (An.Length == 3)
                            {
                                An = An.Remove(2, 1);
                                An = An + numAn.ToString();
                                numAn++;
                            }
                            query = "Select * From Agri_ImmoGlobal Where IdExploitation = '" + mIdExp + "' And NoItem = '" + index + "';";
                            List<string> list2 = SQlQueryExecuter.RunQueryReaderStr(An, query);
                            if (list2.Count != 0)
                            {
                                query = "Update Agri_ImmoGlobal Set " + An + " = '" + row.Cells[columnindex].Value.ToString() +
                                 "' Where IdExploitation = '" + mIdExp + "' And NoItem = '" + index + "';";
                            }
                            else
                            {
                                query = "Insert into Agri_ImmoGlobal (IdExploitation, NoItem," + An + ") " +
                                 " Values ('" + mIdExp + "','" + index + "','" + row.Cells[columnindex].Value.ToString() + "');";
                            }
                            SQlQueryExecuter.RunQuery(query);
                        }
                        columnindex++;
                    }
                    index++;
                }
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonReport_Click(object sender, EventArgs e)
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

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            TableImmo.Clear();
            TableImmo.Columns.Clear();
            LoadDataGrid();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                //Val résiduelle (n+1) = val résiduelle(n) - amortissement(n) + achat(n) – 
                //[revente(n) + moins value(n) - plus value(n)] + production d'immo(n).
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 0 || e.ColumnIndex >= 10) return;
                double currentvalue =0;

                if (e.RowIndex == 0 && e.ColumnIndex >1)
                {
                    double.TryParse(dataGridView1.Rows[0].Cells[e.ColumnIndex].Value.ToString(), out double valeurR);
                    double.TryParse(dataGridView1.Rows[1].Cells[e.ColumnIndex].Value.ToString(), out double ammor);
                    double.TryParse(dataGridView1.Rows[2].Cells[e.ColumnIndex].Value.ToString(), out double achat);
                    double.TryParse(dataGridView1.Rows[3].Cells[e.ColumnIndex].Value.ToString(), out double plusvalue);
                    double.TryParse(dataGridView1.Rows[4].Cells[e.ColumnIndex].Value.ToString(), out double moinsvalue);
                    double.TryParse(dataGridView1.Rows[5].Cells[e.ColumnIndex].Value.ToString(), out double productImmo);
                    double.TryParse(dataGridView1.Rows[6].Cells[e.ColumnIndex].Value.ToString(), out double revente);

                    currentvalue = valeurR - ammor + achat - (revente + moinsvalue - plusvalue) + productImmo;

                    dataGridView1.Rows[0].Cells[e.ColumnIndex + 1].Value = currentvalue;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetValeurAmmor()
        {
            double currentvalue = 0;
            try
            {
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                   
                    if (column.Index >= 1 && column.Index < 10)
                    {
                        double.TryParse(dataGridView1.Rows[0].Cells[column.Index].Value.ToString(), out double valeurR);
                        double.TryParse(dataGridView1.Rows[1].Cells[column.Index].Value.ToString(), out double ammor);
                        double.TryParse(dataGridView1.Rows[2].Cells[column.Index].Value.ToString(), out double achat);
                        double.TryParse(dataGridView1.Rows[3].Cells[column.Index].Value.ToString(), out double plusvalue);
                        double.TryParse(dataGridView1.Rows[4].Cells[column.Index].Value.ToString(), out double moinsvalue);
                        double.TryParse(dataGridView1.Rows[5].Cells[column.Index].Value.ToString(), out double productImmo);
                        double.TryParse(dataGridView1.Rows[6].Cells[column.Index].Value.ToString(), out double revente);

                        currentvalue = valeurR - ammor + achat - (revente + moinsvalue - plusvalue) + productImmo;

                        dataGridView1.Rows[0].Cells[column.Index+1].Value = currentvalue;
                    }
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
