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
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System.Drawing.Drawing2D;

namespace OMEGA.Forms.ResultUserControl
{
    internal partial class UserControlExternalites : UserControl
    {
        private DataTable Agri_Datatable = new DataTable();
        private ShowCommonFormResult showCommonForm;
        private int IdExp;
        private Form resultForm;
        private Color color1 = Commun.GetColor("ARVB1");
        private Color color2 = Commun.GetColor("ARVB2");
        private Color color3 = Commun.GetColor("ARVB3");
        private int langue = Properties.Settings.Default.Langue;
        private int compteur = 0;
        private int mType = 0;


        internal UserControlExternalites(int type,Form form)
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            showCommonForm = new ShowCommonFormResult(textBox1, dataGridView1, "Externalite");
            buttonDico.Click += new EventHandler(showCommonForm.buttonDicoClick);
            buttonCalcul.Click += new EventHandler(showCommonForm.buttonCalculClick);
            textBox1.KeyPress += new KeyPressEventHandler(showCommonForm.TextBox_KeyPress);
            dataGridView1.CellClick += dataGridView1_CellClick;
            buttonReport.Click += new EventHandler(showCommonForm.buttonReportClick);
            SetIdExp();
            LoadGrid(type);
            resultForm = form;
            Translate();
            mType = type;
        }

        private void Translate()
        {
            try
            {
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                buttonDico.Text = Translation.Translate("Dico", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
                buttonList.Text = Translation.Translate("Externalities", langue);
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private void SetIdExp()
        {
            string query = "Select ID From exploitation Where Encours = '1';";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
            if (Commun.ListHasValue(list))
            {
                IdExp = list[0];
            }
        }

        private void buttonList_Click(object sender, EventArgs e)
        {
            string query = "select ID,Nom From Externalites";
            string table = "Externalites";
            ListeForm listeform = new ListeForm(query, table, "Agri_Externalite", null, null, dataGridView1, Agri_Datatable);
            listeform.Show();

        }

        private void LoadGrid(int type)
        {
            try
            {
                int IDdefcateg;
                if (type == 0) // Extern négatives
                {
                    IDdefcateg = 14;
                } 
                // externalite positives
                else IDdefcateg = 13;

                string mainquery = ExternaliteQuery.MainQueryExternaliteResult(IDdefcateg);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainquery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(Agri_Datatable);
                Agri_Datatable = AddYearToTable(Agri_Datatable);
                Agri_Datatable = AddValuesToTable(Agri_Datatable);
                dataGridView1.DataSource = Agri_Datatable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.AllowUserToAddRows = false;
                string query = "Select Formule From Agri_Externalites" +
                " Where IdExploitations = '" + IdExp + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Formule", query);
                if (Commun.ListHasValue(list))
                {
                    textBox1.Text = list[0];
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private DataTable AddYearToTable(DataTable table)
        {
            int An0 = 0;
            string query = "Select An_0 From Agri_DefSim Where IdExploitations = '" + IdExp + "'";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
            if (Commun.ListHasValue(list))
            {
                An0 = list[0];
            }
            else An0 = DateTime.Now.Year;
            for (int i = 1; i <= 10; i++)
            {
                table.Columns.Add(An0.ToString());
                An0++;
            }
            return table;
        }

        private DataTable AddValuesToTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    for (int i = 3; i < 13; i++)
                    {
                        string query = "Select Valeur From Result_Calcul " +
                        "Where Table_Origine = 'Agri_Externalites' " +
                        " AND Annee = '" + table.Columns[i].ColumnName +
                        "' AND Nom = '" + row.ItemArray[1].ToString() +
                        "' AND IdExploitations = '" + IdExp + "';";
                        List<string> list = SQlQueryExecuter.RunQueryReaderStr("Valeur", query);
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool update = false;
                int id = 0;
                string query = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    query = " select * From Agri_Externalites Where IdExternalites = '" + row.Cells[0].Value.ToString() +
                        "' AND IdExploitations = '" + IdExp + "';";
                    List<int> list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (!Commun.ListHasValue(list2))
                    {
                        query = "Insert into Agri_Externalites (IdExternalites,Formule,IdExploitations) VALUES ('" + row.Cells[0].Value.ToString()
                            + "','" + textBox1.Text + "','" + IdExp + "')";
                    }
                    else
                    {
                        id = list2[0];
                        query = "Update Agri_Externalites Set Formule = '" + textBox1.Text + "' WHERE Id = '" + id + "';";
                    }
                    SQlQueryExecuter.RunQuery(query);

                    for (int i = 3; i < 13; i++)
                    {
                        query = "Select * From Result_Calcul Where table_Origine = 'Agri_Externalites' " +
                       "AND Nom = '" + row.Cells[1].Value.ToString() +
                       "' AND Annee = '" + dataGridView1.Columns[i].HeaderText +
                       "' AND IdExploitations = '" + IdExp + "';";
                        List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (Commun.ListHasValue(list))
                        {
                            update = true;
                            id = list[0];
                        }
                        else update = false;
                        if (update)
                        {
                            query = "Update Result_Calcul Set Valeur = '" + row.Cells[i].Value.ToString() +
                                "' WHERE ID = '" + id + "';";
                        }
                        else
                        {
                            query = "Insert Into Result_calcul (table_Origine,Nom,Annee,Valeur,IdExploitations) " +
                                "VALUES ('Agri_Externalites','" + row.Cells[1].Value.ToString() + "','" +
                                 dataGridView1.Columns[i].HeaderText + "','" + row.Cells[i].Value.ToString() +
                                 "','" + IdExp + "');";
                        }
                        SQlQueryExecuter.RunQuery(query);
                    }
                }
                resultForm.Text = resultForm.Text.Substring(resultForm.Text.Length - 1, 1);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 2)
            {
                buttonList.Enabled = true;
            }
            else
            {
                buttonList.Enabled = false;
                dataGridView1.ReadOnly = true;
            }
            int id;
            int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out id);
            string query = "Select Formule From Agri_Externalites" +
               " Where IdExploitations = '" + IdExp +
               "' AND IdExternalites = '" + id + "';";
            List<string> list = SQlQueryExecuter.RunQueryReaderStr("Formule", query);
            if (Commun.ListHasValue(list))
            {
                textBox1.Text = list[0];
            }
        }

        private void TextBoxClicked(object sender, EventArgs e)
        {
            textBox1.SelectionStart = Math.Max(0, textBox1.Text.Length);
            textBox1.SelectionLength = 0;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel2.ClientRectangle);
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            if (!resultForm.Text.Contains("*"))
            {
                resultForm.Text = resultForm.Text + "*";
            }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;
            try
            {
                if (!resultForm.Text.Contains("*"))
                {
                    resultForm.Text = resultForm.Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void buttonMoins10_Click(object sender, EventArgs e)
        {
            compteur--;
            LoadGrid(mType);
        }

        private void buttonPlus10_Click(object sender, EventArgs e)
        {
            compteur++;
            LoadGrid(mType);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            resultForm.Close();
        }
    }
}
