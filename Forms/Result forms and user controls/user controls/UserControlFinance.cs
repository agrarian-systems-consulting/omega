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

namespace OMEGA.Forms.Result_forms_and_user_controls.user_controls
{
    public partial class UserControlFinance : UserControl
    {
        private int IdExpl = Commun.GetIdExpl();
        private int langue = Properties.Settings.Default.Langue;
        private List<int> ListeRecordToDelete = new List<int>();
        private bool mDataSaved = true;
        private DataTable table = new DataTable();
        private ResultForm resultForm;
        private string mType = "";

        public UserControlFinance(string type, ResultForm form)
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            mType = type;
            resultForm = form;
            Translate();
            switch (type)
            {
                case "Long Terme":
                    loadComboboxNbVariation();
                    LoadGridViewLongTerme();
                    break;
                case "Court Terme":
                    LoadGridViewCourtTerme();
                    labelTitle.Text = "Court Terme";
                    break;
                case "Placement":
                    LoadGridViewPlacement();
                    labelTitle.Text = "Placement";
                    break;
                case "Subvention":
                    LoadGridViewSubvention();
                    labelTitle.Text = "Subvention";
                    break;
                case "Occ":
                    LoadGridViewOcc();
                    labelTitle.Text = "Ouveture crédit";
                    break;
                case "Global":
                    LoadGridViewGlobal();
                    labelTitle.Text = "Global";
                    break;
            }
            SubscribeToEvent();
        }

        private void Translate()
        {
            try
            {
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                // buttonDico.Text = Translation.UpdateCaption("Dico", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
                buttonAdd1.Text = Translation.Translate("Add", langue);
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        #region Long Terme
        private void loadComboboxNbVariation()
        {
            try
            {
                List<int> list = new List<int>();
                list.Add(1);
                list.Add(2);
                list.Add(3);
                list.Add(4);
                list.Add(5);
                comboBoxnbVariation.DataSource = list;

                string query = "Select Nombre_Variation From Agri_empLT Where IdExploitation = '" + IdExpl + "';";
                List<string> list2 = SQlQueryExecuter.RunQueryReaderStr("Nombre_Variation", query);
                if (Commun.ListHasValue(list2))
                {
                    comboBoxnbVariation.Text = list2[0];
                }
                else comboBoxnbVariation.Text = "1";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadGridViewLongTerme()
        {
            try
            {
                table.Clear();
                table.Columns.Clear();
                string query = " Select ID,Nom, Montant, Type, Periodicite, DateReal as Realisation_jj_mm_aaaa " +
                    ", DateRemb as Remboursement_jj_mm_aaaa, Ent_Pri," + GetNbVariation() + " From Agri_emPLT;";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                dataGridView1.Columns[0].Visible = false;
                RenameHeader();
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
                dataGridView1.Columns[1].HeaderText = Translation.Translate("Name", langue);
                dataGridView1.Columns[2].HeaderText = Translation.Translate("Amount", langue);
                dataGridView1.Columns[3].HeaderText = Translation.Translate("Type", langue);
                dataGridView1.Columns[4].HeaderText = Translation.Translate("Periodicity", langue);
                dataGridView1.Columns[5].HeaderText = Translation.Translate("Implementation", langue);
                dataGridView1.Columns[6].HeaderText = Translation.Translate("Reimbursement", langue);
                dataGridView1.Columns[6].Width = 130;
                dataGridView1.Columns[7].HeaderText = Translation.Translate("Farm/Private", langue);
                dataGridView1.Columns[7].Width = 165;
                if (mType == "Long Terme")
                {
                    int.TryParse(comboBoxnbVariation.Text, out int nbvar);
                    int index = 1;
                    for (int i = 1; i <= nbvar; i++)
                    {
                        dataGridView1.Columns[7 + index].HeaderText = Translation.Translate("Duration ", langue) + i;
                        index++;
                        dataGridView1.Columns[7 + index].HeaderText = Translation.Translate("Rate ", langue) + i;
                        index++;
                    }
                }  

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private string GetNbVariation()
        {
            try
            {
                string query = "";
                int.TryParse(comboBoxnbVariation.Text, out int nbvar);
                for (int i = 1; i <= nbvar; i++)
                {
                    query = query + "Duree" + i + ", Taux" + i + ",";
                }
                query = query.Remove(query.Length - 1, 1);
                return query;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }
        
        private void ButtonSaveDataLongTerme(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    query = "Select * From Agri_empLT Where Nom = '" + row.Cells[1].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        query = " Update Agri_empLT set Nom ='" + row.Cells[1].Value.ToString() +
                            "', Montant = '" + row.Cells[2].Value.ToString() + "', Type ='" + row.Cells[3].Value.ToString() +
                            "', Periodicite = '" + row.Cells[4].Value.ToString() + "', DateRemb = '" + row.Cells[5].Value.ToString() +
                            "', DateReal = '" + row.Cells[6].Value.ToString() + "', Ent_Pri = '" + row.Cells[7].Value.ToString() +
                            updateNbVariation(row);

                    }
                    else
                    {
                        query = "Insert Into Agri_empLT (Nom,Montant,Type,Periodicite,DateRemb,DateReal,Ent_Pri,IdExploitation," + GetInsertNbVariation(row)
                            + ") VALUES ('" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() +
                            "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() +
                            "','" + row.Cells[5].Value.ToString() + "','" + row.Cells[6].Value.ToString() +
                            "','" + row.Cells[7].Value.ToString() + "','" + IdExpl
                            + "','" + GetInsert2NbVariation(row) + ");";
                    }
                    SQlQueryExecuter.RunQuery(query);
                }
                foreach (int index in ListeRecordToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(index, "Agri_empLT");
                    SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();
                mDataSaved = true;
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private string updateNbVariation(DataGridViewRow row)
        {
            try
            {
                string query = "";
                int num = 1;
                int index = 1;
                int.TryParse(comboBoxnbVariation.Text, out int nbvar);
                for (int i = 1; i <= nbvar; i++)
                {
                    query = "Duree" + num + " ='" + row.Cells[7 + index].Value.ToString() + "',";
                    index++;
                    query = "Taux" + num + " ='" + row.Cells[7 + index].Value.ToString() + "',";
                    index++;
                    num++;
                }
                query = query.Remove(query.Length - 1, 1);
                return query;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }

        private string GetInsertNbVariation(DataGridViewRow row)
        {
            try
            {
                string query = "";
                int.TryParse(comboBoxnbVariation.Text, out int nbvar);
                for (int i = 1; i <= nbvar; i++)
                {
                    query = query + "Duree" + i + ",";
                    query = query + "Taux" + i + ",";
                }
                query = query.Remove(query.Length - 1, 1);
                return query;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }

        private string GetInsert2NbVariation(DataGridViewRow row)
        {
            try
            {
                string query = "";
                int index = 0;
                int.TryParse(comboBoxnbVariation.Text, out int nbvar);
                for (int i = 1; i <= 2 * nbvar; i++)
                {
                    query = query + row.Cells[7 + i].Value.ToString() + "','";
                    index++;
                }
                query = query.Remove(query.Length - 2, 2);

                return query;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }

        /// <summary>
        /// Event that fires when the user wants remove a line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonRemoveRow(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection UserSelectedRow = dataGridView1.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    ListeRecordToDelete.Add(GetId(index));
                    dataGridView1.Rows.RemoveAt(index);
                }
                mDataSaved = false;
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
                int id = 0;
                if (dataGridView1.Rows[index].Cells[0].Value != null)
                {
                    int.TryParse(dataGridView1.Rows[index].Cells[0].Value.ToString(), out id);
                }
                return id;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return 0;
            }
        }

        /// <summary>
        /// Event that fires when the user wants to add a new line
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonAdd(object sender, EventArgs e)
        {
            try
            {
                // we add a new row in the table with a new ID
                string[] rowvalue = new string[dataGridView1.ColumnCount];
                table.Rows.Add(rowvalue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void comboBoxnbVariation_SelectedIndexChanged(object sender, EventArgs e)
        {
            table.Clear();
            table.Columns.Clear();
            LoadGridViewLongTerme();
        }
        
        #endregion

                
        #region Court Terme
        private void LoadGridViewCourtTerme()
        {
            try
            {
                table.Clear();
                table.Columns.Clear();
                string query = " Select ID,Nom, Montant,Taux, Type, DateReal as Realisation_jj_mm_aaaa " +
                    ", DateRemb as Remboursement_jj_mm_aaaa, Ent_Pri From Agri_EmpCT;";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                labelNbRemb.Visible = false;
                comboBoxnbVariation.Visible = false;
                RenameHeader();
                dataGridView1.Columns[0].Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ButtonSaveDataCourtTerme(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    query = "Select * From Agri_EmpCT Where Nom = '" + row.Cells[1].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        query = " Update Agri_EmpCT set Nom ='" + row.Cells[1].Value.ToString() +
                            "', Montant = '" + row.Cells[2].Value.ToString() + "', Taux ='" + row.Cells[3].Value.ToString() +
                            "', Type = '" + row.Cells[4].Value.ToString() + "', DateReal = '" + row.Cells[5].Value.ToString() +
                            "', DateRemb = '" + row.Cells[6].Value.ToString() + "', Ent_Pri = '" + row.Cells[7].Value.ToString() + "';";
                    }
                    else
                    {
                        query = "Insert Into Agri_EmpCT (Nom,Montant,Taux,Type,DateReal,DateRemb,l,Ent_Pri,IdExploitation) " +
                            "VALUES ('" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() +
                            "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() +
                            "','" + row.Cells[5].Value.ToString() + "','" + row.Cells[6].Value.ToString() +
                            "','" + row.Cells[7].Value.ToString() + "','" + IdExpl + "');";
                    }
                    SQlQueryExecuter.RunQuery(query);
                }
                foreach (int index in ListeRecordToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(index, "Agri_EmpCT");
                    SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();
                mDataSaved = true;
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }


        #endregion


        #region Placement
        private void LoadGridViewPlacement()
        {
            try
            {
                table.Clear();
                table.Columns.Clear();
                string query = " Select ID,Nom, Montant,Taux, Type, DateReal as Realisation_jj_mm_aaaa " +
                    ", DateTerme as Terme, Ent_Pri From Agri_EmpCT From Agri_EmpCT;";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                labelNbRemb.Visible = false;
                comboBoxnbVariation.Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ButtonSaveDataPlacement(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    query = "Select * From Agri_Plact Where Nom = '" + row.Cells[1].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        query = " Update Agri_Plact set Nom ='" + row.Cells[1].Value.ToString() +
                            "', Montant = '" + row.Cells[2].Value.ToString() + "', Taux ='" + row.Cells[3].Value.ToString() +
                            "', Type = '" + row.Cells[4].Value.ToString() + "', DateReal = '" + row.Cells[5].Value.ToString() +
                            "', DateTerme = '" + row.Cells[6].Value.ToString() + "', Ent_Pri = '" + row.Cells[7].Value.ToString() + "';";
                    }
                    else
                    {
                        query = "Insert Into Agri_Plact (Nom,Montant,Taux,Type,DateReal,DateTerme,l,Ent_Pri,IdExploitation) " +
                            "VALUES ('" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() +
                            "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() +
                            "','" + row.Cells[5].Value.ToString() + "','" + row.Cells[6].Value.ToString() +
                            "','" + row.Cells[7].Value.ToString() + "','" + IdExpl + "');";
                    }
                    SQlQueryExecuter.RunQuery(query);
                }
                foreach (int index in ListeRecordToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(index, "Agri_Plact");
                    SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();
                mDataSaved = true;
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }


        #endregion

        
        #region Subvention
        private void LoadGridViewSubvention()
        {
            try
            {
                table.Clear();
                table.Columns.Clear();
                string query = " Select ID,Nom, Montant, DateReal as Realisation_jj_mm_aaaa " +
                    ", Duree From Agri_Sub;";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                labelNbRemb.Visible = false;
                comboBoxnbVariation.Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ButtonSaveDataSubvention(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    query = "Select * From Agri_Sub Where Nom = '" + row.Cells[1].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        query = " Update Agri_Sub set Nom ='" + row.Cells[1].Value.ToString() +
                            "', Montant = '" + row.Cells[2].Value.ToString()  +"', DateReal = '" + row.Cells[3].Value.ToString() +
                            "', Duree = '" + row.Cells[4].Value.ToString() + "';";
                    }
                    else
                    {
                        query = "Insert Into Agri_Sub (Nom,Montant,DateReal,Duree,IdExploitation) " +
                            "VALUES ('" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() +
                            "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() +
                            "','" + IdExpl + "');";
                    }
                    SQlQueryExecuter.RunQuery(query);
                }
                foreach (int index in ListeRecordToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(index, "Agri_Sub");
                    SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();
                mDataSaved = true;
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }


        #endregion


        #region Ouverture crédit

        private void LoadGridViewOcc()
        {
            try
            {
                table.Clear();
                table.Columns.Clear();
                table = AddYearToGrid(table);
                string query = " Select ID,Annee, Montant, Pcent as Pourcentage%,Taux,Frais " +
                    " From Agri_Occc;";
                List<int> listA = SQlQueryExecuter.RunQueryReaderInt("Annee", query);
                List<double> listM = SQlQueryExecuter.RunQueryReaderDouble("Montant", query);
                List<double> listPcent = SQlQueryExecuter.RunQueryReaderDouble("Pcent", query);
                List<double> listT = SQlQueryExecuter.RunQueryReaderDouble("Taux", query);
                List<double> listF = SQlQueryExecuter.RunQueryReaderDouble("Frais", query);

                
                foreach(DataRow row in table.Rows)
                {
                    foreach(int item in listA)
                    {
                        if (row.ItemArray[1].ToString() == item.ToString())
                        {
                            row.SetField(2, listM[listA.IndexOf(item)]);
                            row.SetField(3, listPcent[listA.IndexOf(item)]);
                            row.SetField(4, listT[listA.IndexOf(item)]);
                            row.SetField(5, listF[listA.IndexOf(item)]);
                            break;
                        }
                    }
                  
                }
                
                dataGridView1.DataSource = table;
                labelNbRemb.Visible = false;
                comboBoxnbVariation.Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ButtonSaveDataOcc(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    query = "Select * From Agri_Occc Where Annee = '" + row.Cells[1].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        query = " Update Agri_Occc set Montant ='" + row.Cells[2].Value.ToString() +
                            "', Pcent  = '" + row.Cells[3].Value.ToString() + "', Taux = '" + row.Cells[4].Value.ToString() +
                            "', Frais = '" + row.Cells[5].Value.ToString() + "';";
                    }
                    else
                    {
                     
                    }
                    SQlQueryExecuter.RunQuery(query);
                }

                foreach (int index in ListeRecordToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(index, "Agri_Sub");
                    SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();
                mDataSaved = true;
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private DataTable  AddYearToGrid(DataTable table)
        {
            int year = GetYear();
            for (int i= year; i<= year+10; i++)
            {
                table.Rows.Add(new string[] { i.ToString() });
            }
            return table;
        }

        private int GetYear()
        {
            int An0 = 0;
            string query = "Select An_0 From Agri_DefSim Where IdExploitations = '" + IdExpl + "'";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
            if (Commun.ListHasValue(list))
            {
                An0 = list[0];
            }
            else An0 = DateTime.Now.Year;
            return An0;
        }

        #endregion


        #region Global

        private void LoadGridViewGlobal()
        {
            try
            {
                table.Clear();
                table.Columns.Clear();
                string query = " Select ID,Nom, Montant, DateReal as Realisation_jj_mm_aaaa " +
                    ", Duree From Agri_Sub;";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table);
                dataGridView1.DataSource = table;
                labelNbRemb.Visible = false;
                comboBoxnbVariation.Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ButtonSaveDataGlobal(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {

                    query = "Select * From Agri_Sub Where Nom = '" + row.Cells[1].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(list))
                    {
                        query = " Update Agri_Sub set Nom ='" + row.Cells[1].Value.ToString() +
                            "', Montant = '" + row.Cells[2].Value.ToString() + "', DateReal = '" + row.Cells[3].Value.ToString() +
                            "', Duree = '" + row.Cells[4].Value.ToString() + "';";
                    }
                    else
                    {
                        query = "Insert Into Agri_Sub (Nom,Montant,DateReal,Duree,IdExploitation) " +
                            "VALUES ('" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() +
                            "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() +
                            "','" + IdExpl + "');";
                    }
                    SQlQueryExecuter.RunQuery(query);
                }
                foreach (int index in ListeRecordToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(index, "Agri_Sub");
                    SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();
                mDataSaved = true;
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        #endregion

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex == 3 && mType == "Long Terme")
                {
                    FillAutolistAfterClick(e, 1);
                }
                if (e.ColumnIndex == 4 && mType == "Long Terme")
                {
                    FillAutolistAfterClick(e, 2);
                }
                if (e.ColumnIndex == 4 && mType == "Court Terme")
                {
                    FillAutolistAfterClick(e, 4);
                }
                if (e.ColumnIndex == 4 && mType == "Placemement")
                {
                    FillAutolistAfterClick(e, 5);
                }

                if (e.ColumnIndex == 7)
                {
                    FillAutolistAfterClick(e, 3);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Display a list that allows the user to select if he/she wants to put the same name
        /// </summary>
        /// <param name="e"></param>
        /// <param name="column"></param>
        private void FillAutolistAfterClick(DataGridViewCellEventArgs e, int column)
        {
            try
            {
                double decalage = 1;
                List<string> list = new List<string>();
                ContextMenu contextmenu = new ContextMenu();
                List<MenuItem> ListItem = new List<MenuItem>();
                switch (column)
                {

                    case 1: //remboursement
                        //decalage = 2;
                        list.Add(Translation.Translate("Constant", langue));
                        list.Add(Translation.Translate("Variable", langue));
                        list.Add(Translation.Translate("Différé", langue));
                        break;
                    case 2: //periodicité
                        decalage = 1.15;
                        list.Add(Translation.Translate("Annual", langue));
                        list.Add(Translation.Translate("Semetre", langue));
                        list.Add(Translation.Translate("Trimestre", langue));
                        list.Add(Translation.Translate("Mensuel", langue));
                        break;
                    case 3: // Ent pri
                        decalage = 1.15;
                        list.Add(Translation.Translate("Entreprise", langue));
                        list.Add(Translation.Translate("Privé", langue));
                        break;
                    case 4: // type court terme  
                        decalage = 1.15;
                        list.Add(Translation.Translate("Normal", langue));
                        list.Add(Translation.Translate("Précompté", langue));
                        break;
                    case 5: // type Placement
                        decalage = 1.15;
                        list.Add(Translation.Translate("Annual", langue));
                        list.Add(Translation.Translate("To the end", langue));
                        break;
                }
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
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user select a value on the auto list proposal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Click(object sender, EventArgs e)
        {
            try
            {
                string[] array = sender.ToString().Split(new string[] { "Text:" }, StringSplitOptions.None);
                dataGridView1.CurrentCell.Value = array[1].Trim();
                dataGridView1.Refresh();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SubscribeToEvent()
        {
            buttonAdd1.Click += ButtonAdd;
            buttonRemove.Click += ButtonRemoveRow;
            switch (mType)
            {
                case "Long Terme":
                    buttonSave.Click += ButtonSaveDataLongTerme;
                    break;
                case "Court Terme":
                    buttonSave.Click += ButtonSaveDataCourtTerme;
                    break;
                case "Placement":
                    buttonSave.Click += ButtonSaveDataPlacement;
                    break;
                case "Subvention":
                    buttonSave.Click += ButtonSaveDataSubvention;
                    break;
                case "Global":
                    buttonSave.Click += ButtonSaveDataGlobal;
                    break;
            }
           
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex == 5 || e.ColumnIndex == 6)
                {
                    if (dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Length != 10
                      || !dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString().Contains("/"))
                    {
                        MessageBox.Show("Date format should be : jj/mm/aaaa. Please put a correct date format.");
                        dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            resultForm.Close();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
