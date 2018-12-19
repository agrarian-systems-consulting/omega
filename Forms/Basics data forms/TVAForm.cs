using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using OMEGA.SQLQuery;

namespace OMEGA.Forms
{
    internal partial class TVAForm : Form
    {
        private SQLiteConnection mySQLiteConnection;
        private DataTable mTVATable = new DataTable("TVA");
        private DataTable mUserTVATable = new DataTable("User TVA");
        private string mcommande = "SELECT  IdTVA, Nom, Taux, Defaut, Immo FROM TVA";
        private Boolean mUpdateComboboxTVA = false;
        private Boolean mUpdateComboboxImmo = false;
        private Boolean mEndInitializing = false;
        private Boolean mDataSaved = false;
        private int langue = Properties.Settings.Default.Langue;
        private CurrencyManager mcurrencyManagerDataGridView;

        internal TVAForm(int langue)
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();

            if (File.Exists(Properties.Settings.Default.FichierTraduction))
            {
                SetCaption(langue);
            } 
            try

            {
                string test = Properties.Settings.Default.ConnectionString;
                mySQLiteConnection = new SQLiteConnection(test);
                mySQLiteConnection.Open();
                LoadGridViewData();
                LoadComboBoxData(comboBoxTVA ,"Nom","IdTva",3);
                LoadComboBoxData(comboBoxImmo, "Nom", "IdTva", 4);
                //Disconneted the data from the datasource to allow user so change one combobox without affecting the other one
                mcurrencyManagerDataGridView.SuspendBinding();
                mySQLiteConnection.Close();
                mEndInitializing = true;
                this.Text = Translation.Translate("VAT", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadGridViewData()
        {
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mcommande, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mTVATable);
                // we use 2 tables :one with the data on the table
                // the 2nde one, is the table the user will interact with
                // and we he/she save we will go use sql query to populate the
                // table on the datatable.
                adapter.Fill(mUserTVATable);
                dataGridView1.DataSource = mTVATable;

                // 0 = idTva, 3 = defaut, 4 = Immo
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].Width = 0;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Visible = false;

                 mcurrencyManagerDataGridView = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                 mcurrencyManagerDataGridView.Position = 0;

                pictureBoxExport.MouseLeave += pictureBoxExport_MouseLeave;
                pictureBoxExport.MouseEnter += pictureBoxExport_MouseEnter;
                pictureBoxExport.Click += pictureBoxExport_Click;

            }
                catch ( Exception e)
            {
                MessageBox.Show(e.Message);
            }
           
        }

        private void LoadComboBoxData(ComboBox myCombobox, string fieldDisplay, string fieldValue, int numColumn )
        {
            try
            {   // Creation of a new database here so the combobox are not binding to each other, otherwise the user modification 
                // more complexe and sometime impossible to manage easly, this is the best Workaround I found so far...
                DataTable Mytable = new DataTable();
                Mytable.Columns.Add("IdTva",typeof(int));
                Mytable.Columns.Add("Nom",typeof(string));
                if (numColumn == 3)
                {
                    Mytable.Columns.Add("TVA", typeof(string));
                }
                else
                {
                    Mytable.Columns.Add("Immo", typeof(string));
                }
               
                foreach (DataRow row in mTVATable.Rows)
                {
                    Mytable.Rows.Add(row.ItemArray[0], row.ItemArray[1].ToString(), row.ItemArray[numColumn].ToString());
                }

                myCombobox.DataSource = Mytable;
                myCombobox.DisplayMember = fieldDisplay;
                myCombobox.ValueMember = fieldValue;

                foreach (DataRow row in mTVATable.Rows)
                {
                    if (row.ItemArray[numColumn].ToString().Trim() == "1")
                    {
                        myCombobox.SelectedValue = row.ItemArray[0].ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void AddRow()
        {
            try
            {
                // on ajoute une ligne dans le gridview
                mcurrencyManagerDataGridView.AddNew();
                // on ajoute une ligne dans la table user qui permmettra de mettre à jour les données à la sauvegarde 
                mUserTVATable.Rows.Add();
                mcurrencyManagerDataGridView.SuspendBinding();
                mDataSaved = false;
                if (!Text.Contains("*"))
                {
                    Text = Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SaveData()
        {
            try
            {
                //SQLiteCommand UpdateCommande = new SQLiteCommand();
                string query;

                if (mUpdateComboboxTVA)
                {
                    // the SelectedValue is the id of the datatable, 
                    // if it's changed by the user, we have to update into the table with SQL query.
                    query = "UPDATE " + mTVATable.TableName + " SET Defaut = '1' WHERE idTva = '" + comboBoxTVA.SelectedValue + "' ;";
                    SQlQueryExecuter.RunQuery(query);

                    // we put 0 (=false) in the others records because only one can be set to 1 ( = true)
                    query = "UPDATE " + mTVATable.TableName + " SET Defaut = '0' WHERE idTva <> '" + comboBoxTVA.SelectedValue + "' ;";
                    SQlQueryExecuter.RunQuery(query);
                    mUpdateComboboxTVA = false;

                    // we update the usertable to have the same default  tva 
                    foreach (DataRow row in mUserTVATable.Rows)
                    {
                        if (row.ItemArray[0].ToString() == comboBoxTVA.SelectedValue.ToString())
                        {
                            row.SetField<string>("Defaut", "1");
                        }
                        else
                        {
                            row.SetField<string>("Defaut", "0");
                        }
                    }
                }

                if (mUpdateComboboxImmo)
                {
                    // the SelectedValue is the id of the datatable, 
                    // if it's changed by the user, we have to update into the table with SQL query.
                    query = "UPDATE " + mTVATable.TableName + " SET Immo = '1' WHERE idTva = '" + comboBoxImmo.SelectedValue + "' ;";
                    SQlQueryExecuter.RunQuery(query);

                    // we put 0 (=false) in the others records because only one can be set to 1 ( = true)
                    query = "UPDATE " + mTVATable.TableName + " SET Immo = '0' WHERE idTva <> '" + comboBoxImmo.SelectedValue + "' ;";
                    SQlQueryExecuter.RunQuery(query);
                    mUpdateComboboxImmo = false;

                    // we update the usertable to have the same  immo tva 
                    foreach (DataRow row in mUserTVATable.Rows)
                    {
                        if (row.ItemArray[0].ToString() == comboBoxImmo.SelectedValue.ToString())
                        {
                            row.SetField<string>("Immo", "1");
                        }
                        else
                        {
                            row.SetField<string>("Immo", "0");
                        }
                    }
                }

                // mise à jour de la table avec les valeurs modifiées par l'utilisateur
                query = SQLQueryBuilder.DropQuery("TVA");
                SQlQueryExecuter.RunQuery(query);

                query = " CREATE TABLE `TVA` (`IdTva`	INTEGER, `Nom`	TEXT NOT NULL, `Taux`	TEXT, `Defaut`	TEXT, `Immo`	TEXT, PRIMARY KEY(`IdTva`) );";
                SQlQueryExecuter.RunQuery(query);

                foreach (DataRow row in mUserTVATable.Rows)
                {
                    try
                    {
                        query = "INSERT INTO " + mTVATable.TableName + " (Nom, Taux, Defaut, Immo) VALUES ('" + row.ItemArray[1].ToString() + "','" + row.ItemArray[2].ToString() + "',' " + row.ItemArray[3].ToString() + "','" + row.ItemArray[4].ToString() + "');";
                        SQlQueryExecuter.RunQuery(query);
                    }
                    catch
                    {
                        // gestion si on supprime une ligne dans la table, cela supprime pas complétement mais on ne veut pas la mettre dans les requetes
                        // de sauvegarde donc try/catch
                    }
                }
                Text = Text.Remove(Text.Length - 1, 1);
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetCaption(int langue)
        {

            this.Text = Translation.Translate("TVA Form", langue);
            buttonOk.Text = Translation.Translate("OK", langue);
            buttonRemove.Text = Translation.Translate("Remove", langue);
            buttonCancel.Text = Translation.Translate("Cancel", langue);
            buttonAdd.Text = Translation.Translate("Add", langue);
            buttonSave.Text = Translation.Translate("Save", langue);
            groupBoxDefault.Text = Translation.Translate("Default", langue);
            LabelTVA.Text = Translation.Translate("TVA", langue);
            labelImmo.Text = Translation.Translate("Immo", langue);
          
        }

        private void comboBoxTVA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (mEndInitializing)
            {
                mUpdateComboboxTVA = true;
            }
        }
         
        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (mDataSaved == false)
            {
                SaveData();
            }
            this.DialogResult = DialogResult.OK;
           this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = new DialogResult();
                if (mDataSaved == false)
                {
                    dialogResult = MessageBox.Show(Translation.Translate("Data has been modified but not saved, do you really want to exit ?",langue), "Warning", MessageBoxButtons.YesNo);
                }
                if (dialogResult == DialogResult.Yes)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AllowUserToDeleteRows = true;

                mcurrencyManagerDataGridView.SuspendBinding();
                DataGridViewSelectedRowCollection UserSelectedRow = dataGridView1.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    mUserTVATable.Rows[row.Index].Delete();
                    mcurrencyManagerDataGridView.RemoveAt(row.Index);
                }

                mDataSaved = false;
                if (!Text.Contains("*"))
                {
                    Text = Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

       

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveData();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mcommande, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                mTVATable.Clear();
                adapter.Fill(mTVATable);
                dataGridView1.DataSource = mTVATable;
                LoadComboBoxData(comboBoxTVA, "Nom", "IdTva", 3);
                LoadComboBoxData(comboBoxImmo, "Nom", "IdTva", 4);
                mDataSaved = true;
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void pictureBoxExport_Click(object sender, EventArgs e)
        {
            Export.RunExportTable(mTVATable);
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        private void buttonUp_Click(object sender, EventArgs e)
        {
          if (mcurrencyManagerDataGridView.Position > 0)
           mcurrencyManagerDataGridView.Position = mcurrencyManagerDataGridView.Position - 1; 
        }

        private void buttonDown_Click(object sender, EventArgs e)
        {
            if (mcurrencyManagerDataGridView.Position < mcurrencyManagerDataGridView.Count -1)
             mcurrencyManagerDataGridView.Position = mcurrencyManagerDataGridView.Position + 1; 
        }

        private void DataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex < 0) return;
            if (e.RowIndex < 0) return;
            if (e.ColumnIndex == 2)
            {
                double taux;
                if (Double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out taux) == false)
                {
                    MessageBox.Show(Translation.Translate("Please, insert a whole number",langue));
                    return;
                }
            }
            mUserTVATable.Rows[e.RowIndex].SetField(e.ColumnIndex, dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            mDataSaved = false;
            if (!Text.Contains("*"))
            {
                Text = Text + "*";
            }
            
        }

        private void comboBoxImmo_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (mEndInitializing)
            {
                mUpdateComboboxImmo = true;
            }
        }

        private void pictureBoxExport_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxExport.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBoxExport_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxExport.BorderStyle = BorderStyle.None;
        }

        private void buttonNotes_Click(object sender, EventArgs e)
        {
            try
            {
                NotesForm form = new NotesForm(this.Text);
                form.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void pictureBoxExport_Click_1(object sender, EventArgs e)
        {

        }
    }

    class RecordInfo
    {
        internal String Nom { get; set; }
        internal Double Taux { get; set; }

    }
}
