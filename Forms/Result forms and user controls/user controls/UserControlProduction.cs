using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SQLite;
using OMEGA.SQLQuery;
using System.Drawing.Drawing2D;
using System.Drawing;
using OMEGA.SQLQuery.SpecificQueryBuilder;

namespace OMEGA.Forms.ResultUserControl
{
    internal partial class UserControlProduction : UserControl
    {
        private DataTable dataTable = new DataTable();
        private double mValueToReport = -1;
        private List<int> ListeRecordToDelete = new List<int>();
        private string mType;
        private int mIdExpl = Commun.GetIdExpl();
        private Color color1 = Commun.GetColor("ARVB1");
        private Color color2 = Commun.GetColor("ARVB2");
        private Color color3 = Commun.GetColor("ARVB3");
        private int langue = Properties.Settings.Default.Langue;
        private int An0;
        private ResultForm resultform;
        private int compteur=0;

        internal UserControlProduction(string type,ResultForm form)
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            mType = type;
            LoadGrid();
            buttonReport.Click += buttonReport_Click;
            dataGridView1.CellClick += dataGridView_CellClick;
            dataGridView1.CellEndEdit += dataGridView_CellEndEdit;
            buttonSave.Click += buttonSave_Click;
            buttonCulture.Click += buttonCulture_Click;
            ManageColor();
            Translate();
            RenameHeaderColumn();
            resultform = form;
        }

        private void LoadGrid()
        {
            try
            {
                switch (mType)
                {
                    case "Animals":
                        loadDataGridViewAnimaux();
                        buttonCulture.Text = Translation.Translate("Animals", Properties.Settings.Default.Langue);
                        buttonSurface.Visible = false;
                        labelAssolement.Text = Translation.Translate("Animals", Properties.Settings.Default.Langue);
                        break;
                    case "Surface":
                        loadDataGridView();
                        break;
                    case "Perenial":
                        LoadDataGridViewPerenne();
                        buttonCulture.Text = Translation.Translate("Perenial", Properties.Settings.Default.Langue);
                        buttonSurface.Visible = false;
                        labelAssolement.Text = Translation.Translate("Perenial", Properties.Settings.Default.Langue);
                        break;
                    case "Pluriannual":
                        LoadDataGridViewPluriannual();
                        buttonCulture.Text = Translation.Translate("Pluriannual", Properties.Settings.Default.Langue);
                        buttonSurface.Visible = false;
                        labelAssolement.Text = Translation.Translate("Pluriannual", Properties.Settings.Default.Langue);
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }


        private void loadDataGridViewAnimaux()
        {
            try
            {
                dataTable.Clear();
                dataTable.Columns.Clear();
                string query = ResultQuery.MainQueryAnimaux(mIdExpl);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(dataTable);
                dataTable = AddYearToTable(dataTable);
                dataTable = AddValuesToTable(dataTable,true);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].ReadOnly = true;
                int year = DateTime.Now.Year;
                for (int i = 2; i <= 11; i++)
                {
                    dataGridView1.Columns[i].HeaderText = year.ToString();
                    year++;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void RenameHeaderColumn()
        {
            try
            {
               switch (mType)
               {
                    case "Animals":
                        break;
                    case "Surface":
                        break;
                    case "Perenial":
                        dataGridView1.Columns[1].HeaderText = Translation.Translate("Name", langue);
                        dataGridView1.Columns[2].HeaderText = Translation.Translate("Species", langue);
                        dataGridView1.Columns[3].HeaderText = Translation.Translate("Area", langue);
                        dataGridView1.Columns[4].HeaderText = Translation.Translate("Planting", langue);
                        dataGridView1.Columns[5].HeaderText = Translation.Translate("Lifting", langue);
                        break;
                    case "Pluriannual":
                        dataGridView1.Columns[1].HeaderText = Translation.Translate("Name", langue);
                        dataGridView1.Columns[2].HeaderText = Translation.Translate("Species", langue);
                        dataGridView1.Columns[3].HeaderText = Translation.Translate("Area", langue);
                        dataGridView1.Columns[4].HeaderText = Translation.Translate("Planting", langue);
                        dataGridView1.Columns[5].HeaderText = Translation.Translate("Lifting", langue);
                        break;
               }
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void Translate()
        {
            try
            {
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void loadDataGridView()
        {
            try
            {
                string query =  ResultQuery.MainQueryAssol(mIdExpl);
                dataTable.Clear();
                dataTable.Columns.Clear();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(dataTable);
                dataTable = AddYearToTable(dataTable);
                dataTable = AddValuesToTable(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].ReadOnly = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private DataTable AddYearToTable(DataTable table)
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Agri_DefSim", "An_0", "Where IdExploitations = '" + mIdExpl + "'");
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
                if (Commun.ListHasValue(list))
                {
                    An0 = list[0] + (compteur * 10); ;
                }
                else An0 = DateTime.Now.Year;
                for (int i = 1; i <= 10; i++)
                {
                    table.Columns.Add(An0.ToString());
                    An0++;
                }
                An0 = An0 - 10;
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return table;
        }

        private DataTable AddValuesToTable(DataTable table,bool animaux = false)
        {
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    for (int i = 2; i < 12; i++)
                    {

                        string query = "";
                            if (animaux) query = ResultQuery.AddValueAnimauxQuery(row, table, i, mIdExpl);
                            else query = ResultQuery.AddValueAssolQuery(row, table, i, mIdExpl);
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
                    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                }

            }
            return table;
        }

        private int GetYear()
        {
            int An0 = 0;
            try
            {
                string query = "Select An_0 From Agri_DefSim Where IdExploitations = '" + mIdExpl + "'";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
                if (Commun.ListHasValue(list))
                {
                    An0 = list[0];
                }
                else An0 = DateTime.Now.Year;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return An0;
        }

        /// <summary>
        /// Set the color of the gridview
        /// </summary>
        private void ManageColor()
        {
            try
            {
                if (color1 != null && color2 != null)
                {
                    Commun.Setbackground(dataGridView1, color1, color2);
                    string query = SQLQuery.SQLQueryBuilder.UpdateColorQuery(color1, color2, color3);
                    if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadDataGridViewPerenne()
        {
            try
            {
                dataTable.Clear();
                dataTable.Columns.Clear();
                string query = "Select Activite.Id, Activite.nom, Def_Categ.Nom,Agri_Perenne.Surface,Agri_Perenne.AnPlant,Agri_Perenne.AnArr" +
                    " From Agri_Perenne JOIN Activite On Agri_Perenne.IdActivite = Activite.ID " +
                    " JOIN Caract_Activite on Caract_Activite.IdActivite = Activite.ID " +
                    " JOIN Produits on Caract_Activite.IdProduits = Produits.ID " +
                    " JOIN Def_Categ on Def_Categ.IdDefCateg = Produits.IdDefCateg " +
                    " Where Agri_Perenne.IdExploitations = '" + mIdExpl + "';";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadDataGridViewPluriannual()
        {
            try
            {
                dataTable.Clear();
                dataTable.Columns.Clear();
                string query = "Select Activite.Id, Activite.nom, Def_Categ.Nom,Agri_Pluriannuelle.Surface,Agri_Pluriannuelle.AnPlant,Agri_Pluriannuelle.AnArr" +
                    " From Agri_Pluriannuelle JOIN Activite On Agri_Pluriannuelle.IdActivite = Activite.ID " +
                    " JOIN Caract_Activite on Caract_Activite.IdActivite = Activite.ID " +
                    " JOIN Produits on Caract_Activite.IdProduits = Produits.ID " +
                     " JOIN Def_Categ on Def_Categ.IdDefCateg = Produits.IdDefCateg " +
                    " Where Agri_Pluriannuelle.IdExploitations = '" + mIdExpl + "';";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].ReadOnly = true;
                dataGridView1.Columns[2].ReadOnly = true;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonCulture_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "";
                if (mType == "Surface")
                {
                    query = "Select Activite.ID,Activite.nom From Activite ;";
                }
                else
                {
                    query = "Select  Activite.ID,Activite.nom From Activite " +
                   " Join Type on Type.Id = Activite.IdType " +
                   " Where Type.Nom LIKE '" + mType + "';";
                }
               
                ListeForm mlisteform = new ListeForm(query, "Activite", "", null, null, null, dataTable);
                mlisteform.Show();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        internal void buttonReport_Click(object sender, EventArgs e)
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

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
               if(! double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out mValueToReport))
                { mValueToReport = -1; }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
               if(!double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out mValueToReport))
                { mValueToReport = -1; }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                string table = "";
                bool year = true;
                switch (mType)
                {
                    case "Animals":
                        table = "Agri_Animaux";
                        year = true;
                        break;
                    case "Surface":
                        table = "Agri_Assol";
                        year = true;
                        break;
                    case "Perenial":
                        table = "Agri_Perenne";
                        year = false;
                        break;
                    case "Pluriannual":
                        table = "Agri_Pluriannuelle";
                        year = false;
                        break;
                }

                // delete the records seletected by the user
                foreach (int index in ListeRecordToDelete)
                {

                    string query = SQLQueryBuilder.DeleteQuery(table, " Where IdActivite = '" + index + "' AND IdExploitations = '" + mIdExpl + "';");
                    SQlQueryExecuter.RunQuery(query);

                }
                ListeRecordToDelete.Clear();

                // insert or update the new records

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    bool update;
                    int id;
                    int.TryParse(dataGridView1.Rows[row.Index].Cells[0].Value.ToString(), out id);
                    string query = "Select * from " + table + " Where IdActivite = '" + id + "' AND IDExploitations = '"+mIdExpl+"';";
                    string[] reader = SQlQueryExecuter.RunQueryReader(query);
                    List<string> list = SQlQueryExecuter.RunQueryReaderStr("ID",query);
                    if (year)
                    {
                        if (!Commun.ListHasValue(list))
                        {
                            query = "Insert into "+table+" (IdActivite,IdExploitations) VALUES ('" + row.Cells[0].Value.ToString()
                                + "','" + mIdExpl+"')";
                        }
                        else
                        {
                            //
                        }
                        SQlQueryExecuter.RunQuery(query);

                        for (int i = 1; i < 11; i++)
                        {

                            
                                query = "Select * From Result_Calcul Where table_Origine = '" + table + "' " +
                                  "AND Nom = '" + row.Cells[ 1].Value.ToString() +
                                  "' AND Annee = '" + dataGridView1.Columns[i + 1].HeaderText +
                                  "' AND IdExploitations = '" + mIdExpl + "';";
                                List<int> list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                                if (Commun.ListHasValue(list2))
                                {
                                    update = true;
                                    id = list2[0];
                                }
                                else update = false;
                                if (update)
                                {
                                    query = "Update Result_Calcul Set Valeur = '" + row.Cells[i + 1].Value.ToString() +
                                        "' WHERE ID = '" + id + "';";
                                }
                                else
                                {
                                    query = "Insert Into Result_calcul (table_Origine,Nom,Annee,Valeur,IdExploitations) " +
                                        "VALUES ('" + table + "','" + row.Cells[ 1].Value.ToString() + "','" +
                                            dataGridView1.Columns[i + 1].HeaderText + "','" + row.Cells[i + 1].Value.ToString() +
                                            "','" + mIdExpl + "');";
                                }
                                SQlQueryExecuter.RunQuery(query);
                            
                          
                        }
                    }
                    else
                    {
                        if (Commun.ArrayHasValue(reader))
                        {
                            query = "update " + table + " Set Surface = '" + row.Cells[3].Value.ToString() +
                               "', AnPlant = '" + row.Cells[4].Value.ToString() + "'" +
                               ", AnArr = '" + row.Cells[5].Value.ToString() + "'" +
                               " WHERE IdActivite ='" + id + "' AND IdExploitations ='"+mIdExpl+"';";
                        }
                        else
                        {
                            query = "insert into " + table + " (IdActivite,Surface,AnPlant,AnArr,IdExploitations)" +
                                " VALUES ('" + row.Cells[0].Value.ToString() + "','" + row.Cells[3].Value.ToString() +
                                "','" + row.Cells[4].Value.ToString() + "','" + row.Cells[5].Value.ToString() +
                                "','" + mIdExpl + "')";
                        }
                    }
                    SQlQueryExecuter.RunQuery(query);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonSurface_Click(object sender, EventArgs e)
        {
            try
            {

                StandardForm surfaceform = new StandardForm(0, "surface");
                surfaceform.Show();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Retourne l'Id de la ligne envoyée en paramètre
        /// </summary>
        /// <param name="Rowindex"></param>
        /// <returns></returns>
        private int GetId(int Rowindex)
        {
            int id = -1;
            try
            {
                if (dataGridView1.Rows[Rowindex].Cells[0].Value.ToString() != null)
                {
                    int.TryParse(dataGridView1.Rows[Rowindex].Cells[0].Value.ToString(), out id);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AllowUserToDeleteRows = true;
                DataGridViewSelectedRowCollection UserSelectedRow = dataGridView1.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    ListeRecordToDelete.Add(GetId(index));
                    dataGridView1.Rows.RemoveAt(index);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void buttonNotes_Click(object sender, EventArgs e)
        {
            try
            {
                NotesForm form = new NotesForm(mType);
                form.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
            
        }

        private void dataGridView1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void buttonPlus10_Click(object sender, EventArgs e)
        {
            compteur++;
            LoadGrid();
        }

        private void buttonMoins10_Click(object sender, EventArgs e)
        {
            compteur--;
            LoadGrid();
        }

        private void buttonReport_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            resultform.Close();
        }
    }
}
