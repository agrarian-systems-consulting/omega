using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class SystemUniteForm : Form
    {
        internal SystemUniteForm()
        {
            InitializeComponent();
        }

        private SQLiteConnection mySQLiteConnection;
        private DataTable mSystUnitTable = new DataTable("[SystemeUnite]");
        private DataTable mUserSystemUnitTable = new DataTable("[SystemeUnite]");
        private string mcommande = "SELECT * FROM SystemeUnite";
        private Boolean mDataSaved = false;
        private CurrencyManager mcurrencyManagerDataGridView;
        private Color color1;
        private Color color2;
        private Color color3;

        internal SystemUniteForm(int langue)
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            if (File.Exists(Properties.Settings.Default.FichierTraduction))
            {
                SetCaption(langue);
            }
            try
            {
                mySQLiteConnection = new SQLiteConnection(Properties.Settings.Default.ConnectionString);
                mySQLiteConnection.Open();
                LoadGridViewData();
                //Disconneted the data from the datasource to allow user so change one combobox without affecting the other one
                mcurrencyManagerDataGridView.SuspendBinding();
                mySQLiteConnection.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void LoadGridViewData()
        {
            try
            {

                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mcommande, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mSystUnitTable);
                // we use 2 tables :one with the data on the table
                // the 2nde one, is the table the user will interact with
                // and we he/she save we will go use sql query to populate the
                // table on the datatable.
                adapter.Fill(mUserSystemUnitTable);
                dataGridView1.DataSource = mSystUnitTable;

                // 0 = idTva
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[0].Width = 1;
                int langue = Properties.Settings.Default.Langue;
                dataGridView1.Columns[1].HeaderText = Translation.Translate("Unity activity", langue);
                dataGridView1.Columns[2].HeaderText = Translation.Translate("Unity farm", langue);
                dataGridView1.Columns[3].HeaderText = Translation.Translate("Unity global", langue);
                dataGridView1.Columns[4].HeaderText = Translation.Translate("Ratio 2/1", langue);
                dataGridView1.Columns[5].HeaderText = Translation.Translate("Ratio 3/2", langue);
                dataGridView1.Columns[6].HeaderText = Translation.Translate("Currency", langue);

                mcurrencyManagerDataGridView = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                mcurrencyManagerDataGridView.Position = 0;

                pictureBoxExport.MouseLeave += pictureBoxExport_MouseLeave;
                pictureBoxExport.MouseEnter += pictureBoxExport_MouseEnter;
                pictureBoxExport.Click += pictureBoxExport_Click;

                SetComboBoxColumn();

                this.Text = Translation.Translate("Systeme Unite", langue);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }

        }


        private void SetComboBoxColumn()
        {

        }




        private void AddRow()
        {
            // on ajoute une ligne dans le gridview
            mcurrencyManagerDataGridView.AddNew();
            // on ajoute une ligne dans la table user qui permmettra de mettre à jour les données à la sauvegarde 
            mUserSystemUnitTable.Rows.Add();
            mcurrencyManagerDataGridView.SuspendBinding();
            mDataSaved = false;
        }

        private void SaveData()
        {
            //SQLiteCommand UpdateCommande = new SQLiteCommand();
          
            string SQLQuery;

            // mise à jour de la table avec les valeurs modifiées par l'utilisateur
            SQLQuery = SQLQueryBuilder.DropQuery(mSystUnitTable.TableName);
            SQlQueryExecuter.RunQuery(SQLQuery);

            SQLQuery = "CREATE TABLE `SystemeUnite` (`IdSysUnit` INTEGER,`UAte`	TEXT,`UEnt`	TEXT,`UGlobal`	TEXT,`Ratio21`	TEXT,`Ratio32`	TEXT,`Monnaie`	TEXT,PRIMARY KEY(`IdSysUnit`)); ";
            SQlQueryExecuter.RunQuery(SQLQuery);

            foreach (DataRow row in mUserSystemUnitTable.Rows)
            {
                try
                {
                    SQLQuery = "INSERT INTO " + mSystUnitTable.TableName + " (UAte,UEnt,UGlobal,Ratio21,Ratio32,Monnaie) VALUES ('" + row.ItemArray[1].ToString().Trim()
                        + "','" + row.ItemArray[2].ToString().Trim() + "','" + row.ItemArray[3].ToString().Trim() + 
                        "','" + row.ItemArray[4].ToString().Trim() + "',' " + row.ItemArray[5].ToString().Trim() +
                        "','" + row.ItemArray[6].ToString().Trim() + "');" ;
                    SQlQueryExecuter.RunQuery(SQLQuery).ToString();
                }
                catch
                {
                    // gestion si on supprime une ligne dans la table, cela supprime pas complétement mais on ne veut pas la mettre dans les requetes
                    // de sauvegarde donc try/catch
                }
            }
        }

        private void SetCaption(int langue)
        {
            this.Text = Translation.Translate("System Unit Form", langue);
            buttonOk.Text = Translation.Translate("OK", langue);
            buttonRemove.Text = Translation.Translate("Remove", langue);
            buttonCancel.Text = Translation.Translate("Cancel", langue);
            buttonAdd.Text = Translation.Translate("Add", langue);
            buttonSave.Text = Translation.Translate("Save", langue);
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (mDataSaved == false)
            {
                SaveData();
            }
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            AddRow();
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            dataGridView1.AllowUserToDeleteRows = true;

            mcurrencyManagerDataGridView.SuspendBinding();
            DataGridViewSelectedRowCollection UserSelectedRow = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in UserSelectedRow)
            {
                mUserSystemUnitTable.Rows[row.Index].Delete();
                mcurrencyManagerDataGridView.RemoveAt(row.Index);
            }

            mDataSaved = false;
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 6)
                {
                    if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
                    {
                        e.Handled = true;
                    }

                    // only allow one decimal point
                    if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1) || (e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
                    {
                        e.Handled = true;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveData();
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mcommande, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
            mSystUnitTable.Clear();
            adapter.Fill(mSystUnitTable);
            dataGridView1.DataSource = mSystUnitTable;
            mDataSaved = true;
            Properties.Settings.Default.DoitSauvegarger = true;
            ManageColor();
        }

        private void pictureBoxExport_Click(object sender, EventArgs e)
        {
            Export.RunExportTable(mSystUnitTable);
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
            if (mcurrencyManagerDataGridView.Position < mcurrencyManagerDataGridView.Count - 1)
                mcurrencyManagerDataGridView.Position = mcurrencyManagerDataGridView.Position + 1;
        }


        private void dataGridView1_CellValueChanged_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            mUserSystemUnitTable.Rows[e.RowIndex].SetField(e.ColumnIndex, dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString());
            mDataSaved = false;
        }

        private void pictureBoxExport_MouseEnter(object sender, EventArgs e)
        {
            pictureBoxExport.BorderStyle = BorderStyle.FixedSingle;
        }

        private void pictureBoxExport_MouseLeave(object sender, EventArgs e)
        {
            pictureBoxExport.BorderStyle = BorderStyle.None;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.ColumnIndex == 6)
            {
                //lors d'un click droit on peu caché les colonnes inutiles
                ContextMenu contextmenu = new ContextMenu();
                MenuItem itemoui = new MenuItem("oui");
                MenuItem itemnon = new MenuItem("non");
                contextmenu.MenuItems.Add(itemoui);
                contextmenu.MenuItems.Add(itemnon);
                dataGridView1.ContextMenu = contextmenu;
                Rectangle MyCell = dataGridView1.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                Point point = new Point(MyCell.X,MyCell.Y);
                contextmenu.Show(dataGridView1, point);
                itemoui.Click += ItemOui_Click;
                itemnon.Click += ItemNon_Click;
            }
        }

        private void ItemOui_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = "Oui";

        }

        private void ItemNon_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell.Value = "Non";
        }

        private void dataGridViewDetailRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                var grid = sender as DataGridView;
                var rowIdx = (e.RowIndex + 1).ToString();

                var centerFormat = new StringFormat()
                {
                    // right alignment might actually make more sense for numbers
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                Font corbel = new Font("Corbel", 10);
                var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
                e.Graphics.DrawString(rowIdx, corbel, SystemBrushes.ControlText, headerBounds, centerFormat);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void SetColor()
        {
            try
            {
                string query = "Select * From Couleur;";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ARVB1", query);
                if (Commun.ListHasValue(list))
                {
                    color1 = Color.FromArgb(list[0]);
                }
                List<int> list2 = SQlQueryExecuter.RunQueryReaderInt("ARVB2", query);
                if (Commun.ListHasValue(list2))
                {
                    color2 = Color.FromArgb(list2[0]);
                }
                List<int> list3 = SQlQueryExecuter.RunQueryReaderInt("ARVB3", query);
                if (Commun.ListHasValue(list3))
                {
                    color3 = Color.FromArgb(list3[0]);
                }
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ManageColor()
        {
            if (color1 != null && color2 != null)
            {
                Commun.Setbackground(dataGridView1, color1, color2);
                Commun.SetbackgroundHeader(dataGridView1, color3);
                string query = SQLQueryBuilder.UpdateColorQuery(color1, color2, color3);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
            }
        }

        private void SystemUniteForm_Load(object sender, EventArgs e)
        {
            SetColor();
            dataGridView1.RowPostPaint += dataGridViewDetailRowPostPaint;
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

        /// <summary>
        /// Gestion des raccourcis clavier
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 5 || dataGridView1.CurrentCell.ColumnIndex == 4)
            {
                if (IsNumeric(keyData))
                {
                    return false;
                }
                else if (keyData == Keys.Back)
                {
                    return false;
                }
                else if (!(dataGridView1.CurrentCell.EditedFormattedValue.ToString().Contains(",") || dataGridView1.CurrentCell.EditedFormattedValue.ToString().Contains(".")))
                {
                    if (keyData.ToString() == "Oemcomma" || keyData.ToString() == "OemPeriod" || keyData.ToString() == "Decimal")
                    {
                        return false;
                    }
                    else return true;

                }
                else return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private bool IsNumeric(Keys keyData)
        {
            if (keyData.ToString().Contains("NumPad") || (keyData.ToString().Contains("D") && keyData.ToString().Length == 2))
                return true;
            else return false;
        }
    }
}
