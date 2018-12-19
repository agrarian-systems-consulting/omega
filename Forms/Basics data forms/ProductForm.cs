using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace OMEGA.Forms
{
    internal partial class ProductForm : Form
    {
        private Produit produit;
        private Subro.Controls.DataGridViewGrouper grouper = new Subro.Controls.DataGridViewGrouper();
        internal event EventHandler<SaveDataEvent> SaveData;
        internal string type = "";
      
        internal ProductForm(int langue)
        {
            InitializeComponent();
            try
            {
                dataGridView1.Font = Commun.GetCurrentFont();
                if (File.Exists(Properties.Settings.Default.FichierTraduction))
                {
                SetCaption(langue);
                }
                string query = SQLQueryBuilder.SelectQuery("Produits", "ID");
                List<int> ListID = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                //CurrencyManager currencyManager = (CurrencyManager)BindingContext[dataGridView1.DataSource];
                produit = new Produit(dataGridView1,  buttonOk, buttonSave,
                    buttonCancel, buttonRemove, buttonDuplicate,buttonliste,buttonenablegroupe,
                    ListID,0,pictureBoxExport,this, type);
                dataGridView1 = produit.mDataGridView;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        
   
        private void ProductForm_Load(object sender, EventArgs e)
        {
            // when the form is loaded, we subscribe to the events 
            buttonRemove.Click += new EventHandler(produit.ButtonRemoveRow);
            buttonAdd.Click += new EventHandler(produit.ButtonAdd);
            buttonDuplicate.Click += new EventHandler(produit.ButtonDuplicate);
            buttonCancel.Click += new EventHandler(produit.buttonCancel);
            pictureBoxExport.Click += new EventHandler(produit.ExportTable);
            dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(produit.dataGridView1_CellValueChanged);
            pictureBoxExport.MouseLeave += new EventHandler(produit.pictureBox_MouseLeave);
            pictureBoxExport.MouseEnter += new EventHandler(produit.pictureBox_MouseEnter);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(produit.DataGridView1CellClick);
            SaveData += new EventHandler<SaveDataEvent>(produit.ButtonSaveData);
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(produit.DataGridView1EditingControlShowing);
            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(produit.DataGridView1CellEndEdit);
            dataGridView1.MouseClick += new MouseEventHandler(produit.DataGridViewMouseClick);
            dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(produit.DataGridViewDetailRowPostPaint);
            this.MouseClick += new MouseEventHandler(produit.DataGridViewMouseClick);
            buttonliste.Click += new EventHandler(produit.ButtonPropositionClick);
            buttonNotes.Click += new EventHandler(produit.NoteForm);
            dataGridView1.Sorted += new EventHandler(produit.DataGridView1_Sorted);
            buttonenablegroupe.Click += produit.EnableGroupe;
            produit.SetColor();
        }

        private void SetCaption(int langue)
        {
            try
            {
                this.Text = Translation.Translate("Product Form", langue);
                buttonUnit.Text = Translation.Translate("Units", langue);
                buttonOk.Text = Translation.Translate("OK", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonAdd.Text = Translation.Translate("Add", langue);
                buttonDuplicate.Text = Translation.Translate("Duplicate", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
                buttonliste.Text = Translation.Translate("Disable autofill proposal", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(this, new SaveDataEvent(true, ""));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        
        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(this, new SaveDataEvent(true, ""));
        }
        
        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }

        /// <summary>
        /// Gestion des raccourcis clavier
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="keyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == 5)
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

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxSearch.Text!="")
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[1].Value.ToString().Contains(textBoxSearch.Text))
                        {
                            dataGridView1.CurrentCell = row.Cells[1];
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void buttonUnit_Click(object sender, EventArgs e)
        {
            SystemUniteForm uniteForm = new SystemUniteForm(Properties.Settings.Default.Langue);
            uniteForm.ShowDialog();
        }

       
    }
 }

