using OMEGA.Data_Classes;
using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class DictionnaryForm : Form
    {
        private DataTable mTableOccupation = new DataTable("DICO_Nature");
        private DataTable mTableSousOccupation = new DataTable("Sous_occupation");
        private DataTable mTableVillage = new DataTable("Village");
        private DataTable mCurrentDataTable = new DataTable();
        private Boolean mDataSaved = true;
        private List<int> mListRecordTodelete = new List<int>();
        private List<Nature> mListNature = new List<Nature>();
        private List<Village> mListVillage = new List<Village>();

        internal DictionnaryForm(int langue)
        {
            InitializeComponent();
            foreach (Control control in this.Controls)
            {
                control.Font = Commun.GetCurrentFont();
            }
            if (File.Exists(Properties.Settings.Default.FichierTraduction))
            {
                SetCaption();
            }
        }

        private void SetCaption()
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;
                buttonOk.Text = Translation.Translate("OK", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonAdd.Text = Translation.Translate("Add", langue);
                buttonDuplicate.Text = Translation.Translate("Duplicate", langue);
                buttonValidate.Text = Translation.Translate("Save", langue);
                buttonVillagedico.Text = Translation.Translate("Dictionnary of Villages", langue);
                buttonTypeOccupation.Text = Translation.Translate("Dictionnary of site occupation", langue);
                buttonsoustypeoccupation.Text = Translation.Translate("Dictionnary of sub  occupation site", langue);            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void buttonTypeOccupation_Click(object sender, EventArgs e)
        {
            //LoadDataGridView(mTableVillage, "SELECT Nature as 'Type occupation',A_Preciser as 'à préciser ?' FROM DICO_NATURE;",dataGridViewOccup);
            if (mTableOccupation.Rows.Count <= 0)
            {
                string Selectquery = "SELECT Id,Nature as 'Type occupation',A_Preciser as 'à préciser ?' FROM DICO_NATURE;";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(Selectquery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mTableOccupation);
                dataGridViewOccup.DataSource = mTableOccupation;
            }
            dataGridViewOccup.Visible = true;
            mCurrentDataTable = mTableOccupation;
            dataGridViewVillage.Visible = false;
            datagridviewSSoccup.Visible = false;
            datagridviewSSoccup.AllowUserToAddRows = false;
            dataGridViewVillage.AllowUserToAddRows = false;
            dataGridViewOccup.AllowUserToAddRows = false;
            datagridviewSSoccup.AllowUserToDeleteRows = false;
            dataGridViewVillage.AllowUserToDeleteRows = false;
            dataGridViewOccup.AllowUserToDeleteRows = false;

        }

        private void buttonVillagedico_Click(object sender, EventArgs e)
        {
            // LoadDataGridView(mTableVillage, "SELECT Nom,CAN as Canton,DEP as Département,PROV as Province,LAT as Latittude,LON as Longitude FROM VILLAGE;",dataGridViewVillage);
            if (mTableVillage.Rows.Count <= 0)
            {
                string Selectquery = "SELECT Id, Nom,CAN as Canton,DEP as Département,PROV as Province,LAT as Latittude,LON as Longitude FROM VILLAGE;";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(Selectquery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mTableVillage);
                dataGridViewVillage.DataSource = mTableVillage;
            }
            dataGridViewVillage.Visible = true;
            mCurrentDataTable = mTableVillage;
            dataGridViewOccup.Visible = false;
            datagridviewSSoccup.Visible = false;
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            Export.RunExportTable(mCurrentDataTable);
        }

        private void buttonDuplicate_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView MycurrentGrid = GetCurrentGridView();
                if (MycurrentGrid != null)
                {
                    DataGridViewSelectedRowCollection ListuserRow = MycurrentGrid.SelectedRows;
                    InfoUserProduit infoUser = new InfoUserProduit();
                    string[] rowvalue = new string[10];
                    if (ListuserRow.Count == 1)
                    {
                        for (int j = 0; j <= ListuserRow.Count - 1; j++)
                        {
                            rowvalue = new string[MycurrentGrid.ColumnCount];
                            for (int i = 0; i < MycurrentGrid.ColumnCount; i++)
                            {
                                rowvalue[i] = ListuserRow[j].Cells[i].Value.ToString();
                            }
                            //mUserProductTable.Rows.Add(rowvalue);
                            //dataGridView1.Rows.Add(rowvalue);
                            mCurrentDataTable.Rows.Add(rowvalue);
                        }
                    }
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridView MycurrentGrid = GetCurrentGridView();
                if (MycurrentGrid != null)
                {
                    MycurrentGrid.AllowUserToDeleteRows = true;

                    //mcurrencyManagerDataGridView.SuspendBinding();
                    DataGridViewSelectedRowCollection UserSelectedRow = MycurrentGrid.SelectedRows;
                    foreach (DataGridViewRow row in UserSelectedRow)
                    {
                        int index = row.Index;
                        mListRecordTodelete.Add(GetId(index));
                        MycurrentGrid.Rows.RemoveAt(index);
                    }
                    mDataSaved = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Retourne le gridview courant
        /// </summary>
        /// <returns></returns>
        private DataGridView GetCurrentGridView()
        {
            DataGridView MycurrentGrid = null;
            try
            {
                switch (mCurrentDataTable.TableName.ToString())
                {
                    case "Village":
                        MycurrentGrid = dataGridViewVillage;
                        break;
                    case "DICO_Nature":
                        MycurrentGrid = dataGridViewOccup;
                        break;
                    case "Sous_occupation":
                        MycurrentGrid = datagridviewSSoccup;
                        break;
                    default:
                        return MycurrentGrid;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return MycurrentGrid;
        }

        /// <summary>
        /// Retourne l'Id de la ligne envoyé en paramètre
        /// </summary>
        /// <param name="Rowindex"></param>
        /// <returns></returns>
        private int GetId(int Rowindex)
        {
            int id=-1;
            try
            {
                DataGridView MycurrentGrid = GetCurrentGridView();
                if (MycurrentGrid.Rows[Rowindex].Cells[0].Value.ToString() != null)
                {
                    int.TryParse(MycurrentGrid.Rows[Rowindex].Cells[0].Value.ToString(), out id);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }

        /// <summary>
        /// retourne l'ID de la ligne précédente à <paramref name="RowIndex"/>
        /// </summary>
        /// <param name="RowIndex"></param>
        /// <returns></returns>
        private int GetPQteiousId(int RowIndex)
        {
            DataGridView MycurrentGrid = GetCurrentGridView();
            int id;
            if (RowIndex > 0)
            {
                int.TryParse(MycurrentGrid.Rows[RowIndex - 1].Cells[0].Value.ToString(), out id);
            }
            else
            {
                id = GetFirstId();
            }

            return id;
        }

        /// <summary>
        /// Retourne le premier Id de la table courante
        /// </summary>
        /// <returns></returns>
        private int GetFirstId()
        {
            int id = 1;
            string[] reader;
            string query = SQLQueryBuilder.FirstIdQuery(mCurrentDataTable.TableName);
            reader = SQlQueryExecuter.RunQueryReader(query);
            if (reader.Length > 0)
            {
                if (reader[0] != null)
                {
                    int.TryParse(reader[0], out id);
                }
            }
            return id;
        }

        /// <summary>
        /// Retourne le dernier ID de la table courante
        /// </summary>
        /// <returns></returns>
        private int GetMaxId()
        {
            int id = 1;
            string[] reader;
            string query = SQLQueryBuilder.MaxIdQuery(mCurrentDataTable.TableName);
            reader = SQlQueryExecuter.RunQueryReader(query);
            if (reader.Length > 0)
            {
                if (reader[0] != null)
                {
                    int.TryParse(reader[0], out id);
                }
            }
            return id;

        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
            }
            catch ( Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            } 
        }

        private void SaveData()
        {
            try
            {
                if (mDataSaved == false)
                {
                    // si suppresion de ligne
                    foreach (int item in mListRecordTodelete)
                    {
                        string query = SQLQueryBuilder.DeleteQuery(item, mCurrentDataTable.TableName);
                        SQlQueryExecuter.RunQuery(query);
                    }

                    // si ajout ou modification de ligne dans VILLAGE
                    foreach (Village item in mListVillage)
                    {
                        string query = DictionnaryQuery.AddOrUpdateDicoQuery(item,GetId(item.IdRow));
                        SQlQueryExecuter.RunQuery(query);

                    }

                    // si ajout ou modification dans nature = occupation
                    foreach (Nature item in mListNature)
                    {
                        string query = DictionnaryQuery.AddOrUpdateDicoQuery(item, GetId(item.IdRow));
                        SQlQueryExecuter.RunQuery(query);
                    }

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }


        }

        private int GetNbRow()
        {
            int nbrow=0;
            string[] reader;
            string query = SQLQueryBuilder.NbRowQuery(mCurrentDataTable.TableName);
            reader = SQlQueryExecuter.RunQueryReader(query);
            if (reader.Length > 0)
            {
                if (reader[0] != null)
                {
                    int.TryParse(reader[0], out nbrow);
                }
            }
            return nbrow;
        }

        private void dataGridViewVillage_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool lignetrouve = false;
                int IdCurrentRow = GetId(e.RowIndex);
                int CountNbRowInDb = GetNbRow();
                int indexListe = 0;

                // on ajoute une nouvelle ligne
                if (IdCurrentRow <= 0) // 
                {
                    for (int i = 0; i <= mListVillage.Count - 1; i++) // on cherche ou ajoute la ligne avec l'id en cours de modification
                    {
                        if (mListVillage[i].IdRow == CountNbRowInDb + (dataGridViewVillage.Rows.Count - CountNbRowInDb))
                        {
                            lignetrouve = true;
                            indexListe = i;
                            break;
                        }
                    }
                    if (lignetrouve == false)
                    {
                        Village village = new Village();
                        village.IdRow = CountNbRowInDb + (dataGridViewVillage.Rows.Count - CountNbRowInDb);
                        village.Update = false;
                        mListVillage.Add(village);
                        indexListe = mListVillage.IndexOf(village);
                    }
                }
                else // on update une valeur
                {
                    for (int i = 0; i <= mListVillage.Count - 1; i++) // on cherche ou ajoute la ligne avec l'id en cours de modification
                    {
                        if (mListVillage[i].IdRow == IdCurrentRow)
                        {
                            lignetrouve = true;
                            indexListe = i;
                            mListVillage[indexListe].Update = true;
                            break;
                        }
                    }
                    if (lignetrouve == false)
                    {
                        Village village = new Village();
                        village.IdRow = IdCurrentRow;
                        village.Update = true;
                        mListVillage.Add(village);
                        indexListe = mListVillage.IndexOf(village);
                    }
                }// on met les valeurs de toutes la ligne dans la liste à chaque fois, comme cela on récupère toutes les valeurs pour mise à jour/création
                mListVillage[indexListe].Nom = dataGridViewVillage.Rows[e.RowIndex].Cells[1].Value.ToString();
                mListVillage[indexListe].Canton = dataGridViewVillage.Rows[e.RowIndex].Cells[2].Value.ToString();
                mListVillage[indexListe].Dept = dataGridViewVillage.Rows[e.RowIndex].Cells[3].Value.ToString();
                mListVillage[indexListe].Province = dataGridViewVillage.Rows[e.RowIndex].Cells[4].Value.ToString();
                double lat = 0;
                double.TryParse(dataGridViewVillage.Rows[e.RowIndex].Cells[5].Value.ToString(), out lat);
                mListVillage[indexListe].Lattitude = lat;
                double longit = 0;
                double.TryParse(dataGridViewVillage.Rows[e.RowIndex].Cells[6].Value.ToString(), out longit);
                mListVillage[indexListe].Longitude = longit;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            DataGridView MycurrentGrid = GetCurrentGridView();
            CurrencyManager mcurrencyManagerDataGridView = (CurrencyManager)BindingContext[MycurrentGrid.DataSource]; ;
            mcurrencyManagerDataGridView.AddNew();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SaveData();
            this.Close();
        }

        private void dataGridViewOccup_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                bool lignetrouve = false;
                int IdCurrentRow = GetId(e.RowIndex);
                int CountNbRowInDb = GetNbRow();
                int indexListe = 0;

                // on ajoute une nouvelle ligne
                if (IdCurrentRow <= 0) // 
                {
                    for (int i = 0; i <= mListVillage.Count - 1; i++) // on cherche ou ajoute la ligne avec l'id en cours de modification
                    {
                        if (mListNature[i].IdRow == CountNbRowInDb + (dataGridViewVillage.Rows.Count - CountNbRowInDb))
                        {
                            lignetrouve = true;
                            indexListe = i;
                            break;
                        }
                    }
                    if (lignetrouve == false)
                    {
                        Nature occupation = new Nature();
                        occupation.IdRow = CountNbRowInDb + (dataGridViewVillage.Rows.Count - CountNbRowInDb);
                        occupation.Update = false;
                        mListNature.Add(occupation);
                        indexListe = mListNature.IndexOf(occupation);
                    }
                }
                else // on update une valeur
                {
                    for (int i = 0; i <= mListVillage.Count - 1; i++) // on cherche ou ajoute la ligne avec l'id en cours de modification
                    {
                        if (mListNature[i].IdRow == IdCurrentRow)
                        {
                            lignetrouve = true;
                            indexListe = i;
                            mListNature[indexListe].Update = true;
                            break;
                        }
                    }
                    if (lignetrouve == false)
                    {
                        Nature occupation = new Nature();
                        occupation.IdRow = CountNbRowInDb + (dataGridViewVillage.Rows.Count - CountNbRowInDb);
                        occupation.Update = true;
                        mListNature.Add(occupation);
                        indexListe = mListNature.IndexOf(occupation);
                    }
                }// on met les valeurs de toutes la ligne dans la liste à chaque fois, comme cela on récupère toutes les valeurs pour mise à jour/création
                mListNature[indexListe].Occupation = dataGridViewVillage.Rows[e.RowIndex].Cells[1].Value.ToString();
               // mListNature[indexListe].A_preciser = dataGridViewVillage.Rows[e.RowIndex].Cells[2].Value.ToString();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }
    }

    internal class Nature
    {
        internal string Occupation { get; set; }
        internal int A_preciser { get; set; }
        internal int IdRow { get; set; }
        internal bool Update { get; set; }
    }

    internal class Village
    {
        private bool update;

        internal string Nom { get; set; }
        internal string Canton { get; set; }
        internal string Dept { get; set; }
        internal string Province { get; set; }
        internal double Lattitude { get; set; }
        internal double Longitude { get; set; }
        internal int IdRow { get; set; }
        internal bool Update
        {
            get { return update; }
            set { update = value; }
        }
    }

}

