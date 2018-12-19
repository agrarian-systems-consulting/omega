using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Forms.Territory_Forms
{

    public partial class FicheAttributForm : Form
    {
        List<UserControlLigne> listlignes = new List<UserControlLigne>();
        //UserControlLigne ligneToDelete = new UserControlLigne();

        private string mCodePoint;
        private int mIdShapefile;

        public FicheAttributForm(string code)
        {
            try
            {
                InitializeComponent();
                mCodePoint = code;
                LoadDataGridView();
                LoadComboBoxVillage();
                LoadComboBoxNom();
                SetDateTimePicker();
                textBoxMaison.KeyPress += textBoxKeyPress;
                textBoxOccup.KeyPress += textBoxKeyPress;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        public FicheAttributForm(string code,int IdShapefile)
        {
            try
            {
                InitializeComponent();
                mCodePoint = code;
                mIdShapefile = IdShapefile;
                LoadDataGridView();
                LoadComboBoxVillage();
                LoadComboBoxNom();
                SetDateTimePicker();
                LoadOccup();
                textBoxMaison.KeyPress += textBoxKeyPress;
                textBoxOccup.KeyPress += textBoxKeyPress;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void groupBoxHabitat_Enter(object sender, EventArgs e)
        {

        }

        private void checkBoxhabitat_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxMaison.Enabled = checkBoxhabitat.Checked;
                textBoxOccup.Enabled = checkBoxhabitat.Checked;
                if (!checkBoxhabitat.Checked)
                {
                    textBoxMaison.Text = "0";
                    textBoxOccup.Text = "0";
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadComboBoxNom()
        {
            try
            {
                string query = "Select * from person";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("NOM", query);
                comboBoxNom.DataSource = list;
                comboBoxNom.Text = "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetDateTimePicker()
        {
            try
            {
                string query = "Select Date From GPS Where code ='" + mCodePoint + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("DATE", query);
                if (Commun.ListHasValue(list))
                {
                    dateTimePicker1.Text = list[0];
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void LoadComboBoxVillage()
        {
            try
            {

                DataTable table = new DataTable();
                string query = SQLQueryBuilder.SelectQuery("Village","distinct ID,Nom");
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table);
                comboBoxVillage.DataSource = table;
                comboBoxVillage.ValueMember = "Nom";
                comboBoxVillage.Text = "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadDataGridView()
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("GPS","CODE, X , Y , ALT","Where code ='" + mCodePoint + "'");
                DataTable table = new DataTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table);
                dataGridView1.DataSource = table;
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

        private void loadOccupationsite()
        {



        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        internal void textBoxKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonadd_Click(object sender, EventArgs e)
        {
            try
            {
                UserControlLigne ligne = new UserControlLigne(listlignes.Count, this);
                int pos = panelOccup.VerticalScroll.Value;
                panelOccup.VerticalScroll.Value = 0;
                panelOccup.VerticalScroll.Enabled = true;
                panelOccup.VerticalScroll.Visible = true;

                ligne.Location = new Point(0, 3 + 28 * listlignes.Count);
                panelOccup.Controls.Add(ligne);
                listlignes.Add(ligne);
                LoadComboBoxNature(ligne.comboBoxNature);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            //ligne.buttonDelete.Click += ButtonDelete_Click;
        }

        private void ResetLinePosition()
        {
            try
            {
                int index = 0;
                foreach (Control myControl in panelOccup.Controls)
                {
                    if (myControl is UserControlLigne)
                    {
                        myControl.Location = new Point(0, 3 + 28 * index);
                    }
                    index++;
                }
            }
              catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        internal void ButtonDelete_Click(object sender, DeleteLineEvent e)
        {
            try
            {
                if (panelOccup.Controls.Count >1)
                {
                    panelOccup.Controls.RemoveAt(e.Index);
                    ResetLinePosition();
                }
                else
                {
                    MessageBox.Show("Impossible to remove all the occupation");
                }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadComboBoxNature(ComboBox myCombobox)
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Type", "Nom");
                List<string> list2 = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                myCombobox.DataSource = list2;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void SaveInfoFiche(int Idshapefile)
        {
            try
            {
                int IdOccup = SaveMainInfo(Idshapefile);

                SaveGPSPointInfo();

                SaveInfoOccup(IdOccup);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private int SaveMainInfo(int IDshapefile)
        {
            int IdOccup = -1;
            try
            {
                int IdEnqueteur = GetIdEnqueteur(comboBoxNom.Text);
                int IdVillage = GetIdVillage(comboBoxVillage.Text);
                string query = SQLQueryBuilder.SelectQuery("OCCUP", "*", "WHERE ID_SHP = '" + IDshapefile + "'");
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (Commun.ListHasValue(list))
                {
                    query = TerritoryQuery.UpdateFicheAttQuery(dateTimePicker1.Text, IdEnqueteur, textBoxMaison.Text, textBoxOccup.Text, IdVillage, textBoxRem.Text, IDshapefile);
                    IdOccup = list[0];
                }
                else
                {
                    query = TerritoryQuery.InsertFicheAttQuery(dateTimePicker1.Text, IdEnqueteur, textBoxMaison.Text, textBoxOccup.Text, IdVillage, textBoxRem.Text, IDshapefile);
                    IdOccup = Commun.GetSeqId("OCCUP");
                }
                SQlQueryExecuter.RunQuery(query);
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return IdOccup;
        }

        private void SaveGPSPointInfo()
        {
            try
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    string query = SQLQueryBuilder.SelectQuery("GPS", "*", "WHERE CODE = '" + row.Cells[0].Value.ToString() + "'");
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        query = "UPDATE GPS set X ='" + row.Cells[1].Value.ToString() + "',Y = '" + row.Cells[2].Value.ToString()
                            + "',ALT = '" + row.Cells[3].Value.ToString() + "' WHERE CODE = '" + row.Cells[0].Value.ToString() + "'; ";
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

        private void SaveInfoOccup(int IdOccup)
        {
            try
            {
                if (IdOccup == -1) IdOccup = Commun.GetSeqId("OCCUP");
                string query = SQLQueryBuilder.SelectQuery("OCCUP_AC", "*", "WHERE ID_OCC_SPA = '" + IdOccup + "'");
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                UserControlLigne ligne = new UserControlLigne();
                foreach (Control myControl in panelOccup.Controls)
                {
                    if (myControl is UserControlLigne)
                    {
                        ligne = (UserControlLigne)myControl;
                        if (Commun.ListHasValue(list))
                        {
                            query = "UPDATE OCCUP_AC set ID_NATURE ='" + ligne.comboBoxNature.SelectedIndex + "',DETAIL = '" + ligne.textBox1.Text
                                + "' WHERE ID_OCC_SPA = '" + IdOccup + "'; ";
                        }
                        else
                        {
                            query = "Insert into OCCUP_AC (ID_NATURE,DETAIL,ID_OCC_SPA)" +
                                " VALUES ('" + ligne.comboBoxNature.SelectedIndex + "','" + ligne.textBox1.Text + "','" + IdOccup
                                 + "');";
                        }
                        SQlQueryExecuter.RunQuery(query);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private int GetIdEnqueteur(string nom)
        {
            string query = SQLQueryBuilder.SelectQuery("Person","*","where Nom = '" + nom + "'");
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
            if (!Commun.ListHasValue(list))
            {
                query = "Insert Into Person (Nom) VALUES ('" + nom + "');";
                SQlQueryExecuter.RunQuery(query);
                return Commun.GetSeqId("Person") - 1;
            }
            else
            {
                return list[0];
            }
        }

        private int GetIdVillage(string nom)
        {
            string query = SQLQueryBuilder.SelectQuery("Village","*","Where Nom = '" + nom + "'");
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
            if (Commun.ListHasValue(list))
            {
                return list[0];
            }
            else
            {
                return 0;
            }
        }

        private void LoadOccup()
        {
            try
            {
                string query = " Select Type.Nom from Type " +
                                " Join Occup_Ac on Occup_Ac.ID_Nature = Type.ID " +
                                " Join Occup on Occup_Ac.Id_Occ_SPA = Occup.ID" +
                                " Join SHP_Info on Occup.Id_SHP = SHP_Info.ID" +
                                " WHERE Occup.ID_SHP = '" + mIdShapefile + "';";
                List<string> Listnomtype = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string nom in Listnomtype)
                {

                    UserControlLigne ligne2 = new UserControlLigne(Listnomtype.IndexOf(nom), this);
                    LoadComboBoxNature(ligne2.comboBoxNature);
                    ligne2.comboBoxNature.Text = nom;
                    ligne2.Location = new Point(0, 3 + 28 * listlignes.Count);
                    panelOccup.Controls.Add(ligne2);
                    listlignes.Add(ligne2);

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
