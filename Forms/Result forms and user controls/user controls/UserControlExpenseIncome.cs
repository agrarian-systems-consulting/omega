using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OMEGA.Data_Classes;
using System.Data.SQLite;
using System.Drawing.Drawing2D;
using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;

namespace OMEGA.Forms.ResultUserControl
{
    internal partial class UserControlExpenseIncome : UserControl
    {
        private int mIdExp;
        private ShowCommonFormResult showCommonForm;
        private Color color1 = Commun.GetColor("ARVB1");
        private Color color2 = Commun.GetColor("ARVB2");
        private Color color3 = Commun.GetColor("ARVB3");
        private int Compteur = 0;
        int langue = Properties.Settings.Default.Langue;
        private int mType = 0;
        DataTable dataTable = new DataTable();
        private double mValueToReport = 0;

        internal UserControlExpenseIncome(int type, int id)
        {
            InitializeComponent();
            showCommonForm = new ShowCommonFormResult(textBox1,dataGridView1,"Expense_Income");
            dataGridView1.Font = Commun.GetCurrentFont();
            buttonDico.Click += new EventHandler(showCommonForm.buttonDicoClick);
            buttonCalcul.Click += new EventHandler(showCommonForm.buttonCalculClick);
            textBox1.KeyPress += new KeyPressEventHandler(showCommonForm.TextBox_KeyPress);
            textBox1.Click += TextBoxClicked;
            dataGridView1.CellClick += dataGridView1_CellClick;
            buttonReport.Click += new EventHandler(showCommonForm.buttonReportClick);
            mIdExp = id;
            LoadGrid(type);
          
            ManageColor();
            mType = type;


        }

        private void LoadGrid(int type)
        {
            int langue = Properties.Settings.Default.Langue;
            string mainquery = "";
            string whereClause = "";
            switch (type)
            {
                case 1: // family incomes
                    whereClause =  "WHERE Expense = '0' and Family = '1';";
                    labelExpInc.Text = Translation.Translate("Family Incomes", langue);
                    buttonList.Text = Translation.Translate("Misc Expenses", langue);
                    break;
                case 2: // family expenses
                    whereClause = "WHERE Expense = '1' and Family = '1';";
                    labelExpInc.Text = Translation.Translate("Family Expenses", langue);
                    buttonList.Text = Translation.Translate("Misc Expenses", langue);
                    break;
                case 3: // misc expenses
                    whereClause = "WHERE Expense = '1' and Family = '0';";
                    labelExpInc.Text = Translation.Translate("Misc Expenses", langue);
                    buttonList.Text = Translation.Translate("Misc Expenses", langue);
                    break;
                case 4: // misc incomes
                    whereClause = "WHERE Expense = '0' and Family = '0';";
                    labelExpInc.Text = Translation.Translate("Misc Incomes", langue);
                    buttonList.Text = Translation.Translate("Misc Expenses", langue);
                    break;
            }

            mainquery = "Select distinct Expense_Income.Nom From Expense_Income " +
            " Join Result_Calcul on Expense_Income.Nom = Result_Calcul.Nom " + whereClause;
            
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainquery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
            adapter.Fill(dataTable);

            dataTable = AddYearToTable(dataTable);

            dataTable = AddValuesToTable(dataTable);

            dataGridView1.DataSource = dataTable;
        }

        private void Translate()
        {
            try
            {
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                // buttonDico.Text = Translation.UpdateCaption("Dico", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
                switch (mType)
                {
                    case 1: // family incomes
                        buttonList.Text = Translation.Translate("Family Incomes", langue);
                        break;
                    case 2: // family expenses
                        buttonList.Text = Translation.Translate("Family Expenses", langue);
                        break;
                    case 3: // misc expenses
                        buttonList.Text = Translation.Translate("Misc Expenses", langue);
                        break;
                    case 4: // misc incomes
                        buttonList.Text = Translation.Translate("Misc Incomes", langue);
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

        private DataTable AddValuesToTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    for (int i = 1; i < 11; i++)
                    {
                        string query = ResultQuery.AddValueExpIncQuery(table,row,i,mIdExp);
                        List<int> list = SQlQueryExecuter.RunQueryReaderInt("Valeur", query);
                        if (Commun.ListHasValue(list))
                        {
                            row.SetField<string>(table.Columns[i], list[0].ToString());
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                
            }
            return table;
        }

        private DataTable AddYearToTable(DataTable table)
        {
            int An0 = 0;
            string query = SQLQueryBuilder.SelectQuery("Agri_DefSim", "An_0","Where IdExploitations = '" + mIdExp + "'");
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
            if (Commun.ListHasValue(list))
            {
                An0 = list[0] + (Compteur*10);
            }
            else An0 = DateTime.Now.Year;
            for (int i = 1; i <= 10; i++)
            {
                table.Columns.Add(An0.ToString());
                An0++;
            }
            return table;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 1)
                {
                    buttonList.Enabled = true;
                }
                else
                {
                    buttonList.Enabled = false;
                } 
                if (!double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out mValueToReport))
                {
                    mValueToReport = -1; 
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        
        private void TextBoxClicked(object sender, EventArgs e)
        {
            textBox1.SelectionStart = Math.Max(0, textBox1.Text.Length);
            textBox1.SelectionLength = 0;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool update = false;
                int id = 0;
                string query = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    for (int i = 1; i < 11; i++)
                    {
                        query = ResultQuery.SelectExpIncQuery(row, dataGridView1, i,mIdExp);
                        List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (Commun.ListHasValue(list))
                        {
                            update = true;
                            id = list[0];
                        }
                        else update = false;
                        string value = "";
                        if (row.Cells[i].Value.ToString() == "") value = "0";
                        else value = row.Cells[i].Value.ToString();
                        if (update)
                        {
                            query = ResultQuery.UpdateExpIncQuery(value, id); 
                        }
                        else
                        {
                            query = ResultQuery.InsertExpIncQuery(row, dataGridView1, i, value,mIdExp);
                        }
                        SQlQueryExecuter.RunQuery(query);
                        ManageColor();

                    }
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
           //LinearGradientBrush linearGradientBrush = new LinearGradientBrush(panel1.ClientRectangle, Color.White, Color.White, 1);
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
        }

        private void buttonMoins10_Click(object sender, EventArgs e)
        {
            Compteur++;
            LoadGrid(mType);
        }

        private void buttonPlus10_Click(object sender, EventArgs e)
        {
            Compteur--;
            LoadGrid(mType);
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            string query = "";
            switch (mType)
            {
                case 1: // family incomes
                    query = SQLQueryBuilder.SelectQuery("Expense_Income", "ID,Nom", "WHERE Expense = '0' and Family = '1'");
                    break;
                case 2: // family expenses
                    query = SQLQueryBuilder.SelectQuery("Expense_Income", "ID,Nom", "WHERE Expense = '1' and Family = '1'");
                    break;
                case 3: // misc expenses
                    query = SQLQueryBuilder.SelectQuery("Expense_Income", "ID,Nom", "WHERE Expense = '1' and Family = '0'");
                    break;
                case 4: // misc incomes
                    query = SQLQueryBuilder.SelectQuery("Expense_Income", "ID,Nom", "WHERE Expense = '0' and Family = '0'");
                    break;
            }
            ListeForm listeform = new ListeForm(query, "Expense_Income", "Expense_Income", null, null, dataGridView1, dataTable);
            listeform.Show();
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
                if (e.RowIndex < 0) return; 
                if (e.ColumnIndex < 0) return; 
                if (!double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out mValueToReport))
                {
                    mValueToReport = -1;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}
