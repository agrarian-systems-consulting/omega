using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OMEGA.Forms.ResultUserControl
{
    internal partial class UserControlDefinition : UserControl
    {
        private int mIdExpl;
        private Form resultForm;
        private int langue = Properties.Settings.Default.Langue;     

        internal UserControlDefinition(int id,Form form)
        {
            InitializeComponent();
            mIdExpl = id;
            LoadInfo();
            resultForm = form;
            Translate();
            LoadComboBoxAlea1();
            
            
        }

        private void Translate()
        {
            try
            {
                groupBoxAléa.Text = Translation.Translate("Alea", langue);
                LabelChargePrice.Text = Translation.Translate("Price", langue);
                LabelCostQte.Text = Translation.Translate("Quantity", langue);
                LabelExternaliteQte.Text = Translation.Translate("Externalities", langue);
                LabelProductPrice.Text = Translation.Translate("Product", langue);
                LabelProductQte.Text = Translation.Translate("Quantity", langue);
                groupBox1.Text = Translation.Translate("PRICE", langue);
                groupBox2.Text = Translation.Translate("QUANTITY", langue);
                radioButtonWith.Text = Translation.Translate("With", langue);
                radioButtonWithout.Text = Translation.Translate("Without", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
                buttonQuit.Text = Translation.Translate("Quit", langue);
            }
            catch
            {

            }
        }

        internal void LoadInfo()
        {
            try
            {
                LoadTextbox();

                //Loadcombobox();

                List<InfoClassification> ListClassif = FillList();

                List<List<string>> MegaList = FillMegaList();

                FillDataGridView(ListClassif, MegaList);

                InsertRecord();

                comboBoxExtQte2.SelectedValueChanged += ReloadComboBox;
                comboBoxCostPrice.SelectedValueChanged += ReloadComboBox;
                comboBoxProductPrice.SelectedValueChanged += ReloadComboBox;

                LoadAlea();


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadAlea()
        {
            try
            {
                string query = "Select TypeAlea From Agri_DefSim Where IdExploitations = '" + mIdExpl + "';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("TypeAlea", query);
                if (Commun.ListHasValue(list))
                {
                    //radioButtonWith.Checked = true;
                }
                else
                {
                    //radioButtonWithout.Checked = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ReloadComboBox(object sender, EventArgs e)
        {
            if (comboBoxProductPrice.Text == Translation.Translate("Trending", langue)) LoadComboBoxAlea2(1, comboBoxProdPrice2);
            if (comboBoxProductPrice.Text == Translation.Translate("Scenario", langue)) LoadComboBoxAlea2(3, comboBoxProdPrice2);
            if (comboBoxCostPrice.Text == Translation.Translate("Trending", langue)) LoadComboBoxAlea2(2, comboBoxCostPrice2);
            if (comboBoxCostPrice.Text == Translation.Translate("Scenario", langue)) LoadComboBoxAlea2(4, comboBoxCostPrice2);
            if (comboBoxProductQte.Text == Translation.Translate("Trending", langue)) LoadComboBoxAlea2(5, comboBoxProdQte2);
            if (comboBoxProductQte.Text == Translation.Translate("Scenario", langue)) LoadComboBoxAlea2(7, comboBoxProdQte2);
            if (comboBoxCostQte.Text == Translation.Translate("Trending", langue)) LoadComboBoxAlea2(6, comboBoxCostQte2);
            if (comboBoxCostQte.Text == Translation.Translate("Scenario", langue)) LoadComboBoxAlea2(8, comboBoxCostQte2);
            if (comboBoxExterQte.Text == Translation.Translate("Trending", langue)) LoadComboBoxAlea2(9, comboBoxExtQte2);
            if (comboBoxExterQte.Text == Translation.Translate("Scenario", langue)) LoadComboBoxAlea2(10, comboBoxExtQte2);
        }
        

        /// <summary>
        /// if the farm does not have Def record, we insert an empty one
        /// </summary>
        private void InsertRecord()
        {
            string query = "Select * from Agri_DefSim Where IdExploitations = '"+mIdExpl+"';";
            if (!Commun.ArrayHasValue(SQlQueryExecuter.RunQueryReader(query)))
            {
                query = "Insert Into Agri_DefSim (IdExploitations) VALUES ('" + mIdExpl + "');";
                SQlQueryExecuter.RunQuery(query);
            }

            query = "Select * from Caract_Classifications Where IdExploitation = '" + mIdExpl + "';";
            if (!Commun.ArrayHasValue(SQlQueryExecuter.RunQueryReader(query)))
            {
                query = "Insert Into Caract_Classifications (IdExploitation) VALUES ('" + mIdExpl + "');";
                SQlQueryExecuter.RunQuery(query);
            }
        }

        private List<InfoClassification> FillList()
        {
            List<InfoClassification> ListClassif = new List<InfoClassification>();
            try
            {

                 for (int i = 1; i < 10; i++)
                 {
                    InfoClassification newinfo = new InfoClassification();
                    string query1 = "Select Distinct Groupe From Classifications";
                    List<string> list2 = SQlQueryExecuter.RunQueryReaderStr("Groupe", query1);
                    if (i-1 <= list2.Count - 1) newinfo.Name = list2[i-1];
                    else break;

                    string query = "Select Valeur_" + i + " from caract_classifications" +
                     " Where Caract_Classifications.IdExploitation = '" + mIdExpl + "';";
                      list2 = SQlQueryExecuter.RunQueryReaderStr("Valeur_" + i, query);
                     if (Commun.ListHasValue(list2))
                     {
                      newinfo.Value = list2[0];
                     }
                     ListClassif.Add(newinfo);
                     
                 }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return ListClassif;
        }

        private List<List<string>> FillMegaList()
        {
            List<List<string>> MegaList = new List<List<string>>();
            try
            {
                string query = "Select Distinct Groupe From Classifications";
                List<string> list2 = SQlQueryExecuter.RunQueryReaderStr("Groupe", query);
                foreach(string item in list2)
                {
                    query = "Select distinct Nom from Classifications Where Groupe = '"+item+"'; ";
                    List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    if (Commun.ListHasValue(list))
                    {
                        MegaList.Add(list);
                    }
                }
          
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return MegaList;
        }

        private void FillDataGridView(List<InfoClassification> ListClassif, List<List<string>> MegaList)
        {
            try
            {
                for (int i = 0; i <= ListClassif.Count - 1; i++)
                {
                   DataGridViewComboBoxColumn dgvComboBoxColumn = new DataGridViewComboBoxColumn();
                   dgvComboBoxColumn.HeaderText = ListClassif[i].Name;
                   dgvComboBoxColumn.Name = ListClassif[i].Name;
                   foreach (string item in MegaList[i])
                   {
                       dgvComboBoxColumn.Items.Add(item);
                   }
                   dataGridView1.Columns.Add(dgvComboBoxColumn);
                }

            DataGridViewRow row2 = new DataGridViewRow();
            dataGridView1.Rows.Add(row2);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                for (int i = 0; i <= ListClassif.Count - 1; i++)
                {
                    DataGridViewComboBoxCell comboBoxCell = (DataGridViewComboBoxCell)(row.Cells[i]);
                    comboBoxCell.Value = ListClassif[i].Value;
                }
            }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void LoadTextbox()
        {
            try
            {
                string query = "Select Nom from Exploitation Where EnCours = '1';";
                string[] reader = SQlQueryExecuter.RunQueryReader(query);
                if (reader.Length >= 1)
                {
                    if (reader[0] != null) textBoxName.Text = reader[0];
                }

                query = "Select Caract_Exploitation.NumVariante from Caract_Exploitation " +
                    " Where Caract_Exploitation.IdExploitation = '" + mIdExpl +
                    "' AND Caract_Exploitation.NumVariante is not null;";
                reader = SQlQueryExecuter.RunQueryReader(query);
                if (reader.Length >= 1)
                {
                    if (reader[0] != null) textBoxVar.Text = reader[0];

                }
                query = SQLQueryBuilder.SelectQuery("Agri_DefSim", "An_0","Where IdExploitations = '" + mIdExpl + "'");
                reader = SQlQueryExecuter.RunQueryReader(query);
                if (Commun.ArrayHasValue(reader))
                {
                    textBoxYearBegin.Text = reader[0];
                }
                else textBoxYearBegin.Text = DateTime.Now.Year.ToString();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ButtonQuit_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void ButtonSave_Click(object sender, EventArgs e)
        {
           
        }

        //private void Loadcombobox()
        //{
        //    try
        //    {
        //        int[] listyear = new int[9] {10,20,30,40,50,60,70,80,90};
        //        comboBoxNbYear.DataSource = listyear;
        
        //        string[] reader;
        //        string query = SQLQueryBuilder.SelectQuery("Agri_defSim", "NbAnSim",
        //            " Where IdExploitation = '" + mIdExpl + "'");
        //        reader = SQlQueryExecuter.RunQueryReader(query);
        //        if (Commun.ArrayHasValue(reader))
        //        {
        //            comboBoxNbYear.Text = reader[0];
        //        }
        //        else comboBoxNbYear.Text = "10";

        //        int[] listmonth = new int[12] { 1, 2, 3, 4, 5, 6, 7, 8, 9,10,11,12};
        //        comboBoxMonth.DataSource = listmonth;

        //        query = SQLQueryBuilder.SelectQuery("Agri_defSim", "MDeb",
        //        " Where Idexploitation = '" + mIdExpl + "'");
        //        reader = SQlQueryExecuter.RunQueryReader(query);
        //        if (Commun.ArrayHasValue(reader))
        //        {
        //            comboBoxMonth.Text = reader[0];
        //        }
        //        else comboBoxMonth.Text = "1";

        //        //LoadComboBoxResult();

        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageBox.Show(Ex.Message);
        //    }
        //}

        private void comboBoxResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //if (comboBoxResult.Text != "") textBoxYearBegin.Text = comboBoxResult.Text.Substring(0, 4);

                string query = "Update Agri_DefSim Set an_0 = '" + textBoxYearBegin.Text +
                "' Where Idexploitations = '" + mIdExpl + "';";
                //SQlQueryExecuter.RunQuery(query);
                if (resultForm != null)
                {
                    if (!resultForm.Text.Contains("*"))
                    {
                        resultForm.Text = resultForm.Text + "*";
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            
        }
        
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            LinearGradientBrush linearGradientBrush = new LinearGradientBrush(panel1.ClientRectangle, Color.White, Color.White, 1);

            ColorBlend cblend = new ColorBlend(3);
            Color color1 = Color.FromArgb(137, 198, 138);
            Color color2 = Color.FromArgb(164, 226, 165);
            Color color3 = Color.FromArgb(192, 255, 192);

            cblend.Colors = new Color[3] { color1, color2, color3 };
            cblend.Positions = new float[3] { 0f, 0.25f, 1f };

            linearGradientBrush.InterpolationColors = cblend;

            e.Graphics.FillRectangle(linearGradientBrush, panel1.ClientRectangle);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 0) return;
                if (resultForm != null)
                {
                    if (!resultForm.Text.Contains("*"))
                    {
                        resultForm.Text = resultForm.Text + "*";
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void radioButtonWith_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (resultForm != null)
                {
                    if (!resultForm.Text.Contains("*"))
                    {
                        resultForm.Text = resultForm.Text + "*";
                    }
                }
                panelAlea.Visible = radioButtonWith.Checked;
                resultForm.Size = new Size(700, 480);
                buttonBack.Location = new Point(7, 265);

                if (radioButtonWith.Checked)
                {
                    Point point = new Point(574, 240);
                    buttonSave.Location = point;
                    point = new Point(504, 240);
                    buttonQuit.Location = point;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void radioButtonWithout_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (resultForm != null)
                {
                    if (!resultForm.Text.Contains("*"))
                    {
                        resultForm.Text = resultForm.Text + "*";
                    }
                }
                this.Size = new Size(700, 370);
                comboBoxProdPrice2.Text = "";
                comboBoxProdQte2.Text = "";
                comboBoxCostPrice2.Text = "";
                comboBoxExtQte2.Text = "";
                comboBoxCostQte2.Text = "";
                panelAlea.Visible = false;
                buttonBack.Location = new Point(7, 265);
                resultForm.Size = new Size(700, 370);
                if (radioButtonWithout.Checked)
                {
                    Point point = new Point(574, 240);
                    buttonSave.Location = point;
                    point = new Point(504, 240);
                    buttonQuit.Location = point;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void textBoxVar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (resultForm != null)
                {
                    if (!resultForm.Text.Contains("*"))
                    {
                        resultForm.Text = resultForm.Text + "*";
                    }
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void textBoxYearBegin_TextChanged(object sender, EventArgs e)
        {
            if (resultForm != null)
            {
                if (!resultForm.Text.Contains("*"))
                {
                    resultForm.Text = resultForm.Text + "*";
                }
            }
        }

        private void comboBoxMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (resultForm != null)
            {
                if (!resultForm.Text.Contains("*"))
                {
                    resultForm.Text = resultForm.Text + "*";
                }
            }
            
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            if (resultForm != null)
            {
                if (!resultForm.Text.Contains("*"))
                {
                    resultForm.Text = resultForm.Text + "*";
                }
            }
        }

        private void LoadComboBoxAlea1()
        {
            try
            {
                string[] array = new string[3];
                array[0] = Translation.Translate("None", langue);
                array[1] = Translation.Translate("Trending", langue);
                array[2] = Translation.Translate("Scenario", langue);
                List<string> list = new List<string>();
                list.Add(Translation.Translate("None", langue));
                list.Add(Translation.Translate("Trending", langue));
                list.Add(Translation.Translate("Scenario", langue));
                comboBoxProductPrice.DataSource = array;
                comboBoxCostPrice.DataSource = list;
                list = new List<string>();
                list.Add(Translation.Translate("None", langue));
                list.Add(Translation.Translate("Trending", langue));
                list.Add(Translation.Translate("Scenario", langue));
                comboBoxProductQte.DataSource = list;
                list = new List<string>();
                list.Add(Translation.Translate("None", langue));
                list.Add(Translation.Translate("Trending", langue));
                list.Add(Translation.Translate("Scenario", langue));
                comboBoxCostQte.DataSource = list;
                list = new List<string>();
                list.Add(Translation.Translate("None", langue));
                list.Add(Translation.Translate("Trending", langue));
                list.Add(Translation.Translate("Scenario", langue));
                comboBoxExterQte.DataSource = list;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadComboBoxAlea2(int idAlea,ComboBox box)
        {
            try
            {
                box.DataSource = null;
                string query = "Select * From Alea_Categ Where IdAleas = '" + idAlea + "'";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                box.DataSource = list;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonSave_Click_1(object sender, EventArgs e)
        {
            try
            {
                string query;

                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (dataGridView1.Rows[0].Cells[i].Value != null)
                    {
                        int index = i + 1;
                        query = "Update Caract_classifications Set Valeur_" + index + "='" + dataGridView1.Rows[0].Cells[i].Value.ToString() +
                            "' , Classification_" + index + "='" + dataGridView1.Columns[i].Name +
                           "' Where IdExploitation = '" + mIdExpl + "';";
                        SQlQueryExecuter.RunQuery(query);
                        index++;
                    }
                }

                query = "Update Agri_DefSim Set an_0 = '" + textBoxYearBegin.Text +
                    "' Where Idexploitations = '" + mIdExpl + "';";
                SQlQueryExecuter.RunQuery(query);

                query = "Update Agri_DefSim Set MDeb = '" + comboBoxMonth.Text +
                    "' Where Idexploitations = '" + mIdExpl + "';";
                SQlQueryExecuter.RunQuery(query);

                if (comboBoxProdPrice2.Text != "")
                {
                    query = "Update Agri_DefSim Set TypeAlea = '" + SQLQueryBuilder.FindId("Alea_Categ", "ID", "Nom", comboBoxProdPrice2.Text) +
                    "' Where Idexploitations = '" + mIdExpl + "';";
                    SQlQueryExecuter.RunQuery(query);

                    query = "Update Agri_DefSim Set NoPrixProd = '" + SQLQueryBuilder.FindId("Alea_Item", "ID", "IdAleaCateg", SQLQueryBuilder.FindId("Alea_Categ", "ID", "Nom", comboBoxProdPrice2.Text).ToString()) +
                  "' Where Idexploitations = '" + mIdExpl + "';";
                    SQlQueryExecuter.RunQuery(query);
                }

                //query = "Update Agri_DefSim Set NbAnSim = '" + comboBoxNbYear.Text + 
                //    "' Where Idexploitation = '" + mIdExpl + "';";
                //SQlQueryExecuter.RunQuery(query);
                resultForm.Text = resultForm.Text.Remove(resultForm.Text.Length - 1, 1);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            resultForm.Close();
        }
    }

    internal class InfoClassification
    {
        internal string Name {get; set;}
        internal string Value { get; set; }
    }


}
