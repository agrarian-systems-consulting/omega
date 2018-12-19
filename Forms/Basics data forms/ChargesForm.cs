using OMEGA.Data_Classes;
using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class ChargesForm : Form
    {
        private Charge charge;

        internal event EventHandler<SaveDataEvent> SaveData;
        internal ChargesForm(int langue, int type)
        {
            InitializeComponent();
            try
            {
                dataGridView1.Font = Commun.GetCurrentFont();
                string query = SQLQueryBuilder.SelectQuery("Charges", "ID");
                List<int> ListID = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                charge = new Charge(dataGridView1, buttonOk, buttonSave,
                buttonCancel, buttonRemove, buttonDuplicate, buttonliste, type,
                buttonGroupe, ListID, 0, null,this );
                dataGridView1 = charge.DataGridView;
                buttonOk.Click += buttonOk_Click;
                buttonSave.Click += buttonSave_Click;
                SetCaption(langue);
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void SetCaption(int langue)
        {
            try
            {
                this.Text = Translation.Translate("Cost form", langue);
                buttonOk.Text = Translation.Translate("OK", langue);
                buttonUnite.Text = Translation.Translate("Units", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonAdd.Text = Translation.Translate("Add", langue);
                //buttonAdd.Text = Translation.UpdateCaption("Add", langue);
                buttonDuplicate.Text = Translation.Translate("Duplicate", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
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

        private void ChargesForm_Load_1(object sender, EventArgs e)
        {
            try
            {
                // when the form is loaded, we subscribe to the events 
                buttonRemove.Click += new EventHandler(charge.ButtonRemoveRow);
                buttonDuplicate.Click += new EventHandler(charge.ButtonDuplicate);
                buttonCancel.Click += new EventHandler(charge.buttonCancel);
                dataGridView1.CellClick += new DataGridViewCellEventHandler(charge.dataGridView1CellClick);
                dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(charge.dataGridView1CellEndEdit);
                buttonAdd.Click += new EventHandler(charge.ButtonAdd);
                SaveData += new EventHandler<SaveDataEvent>(charge.ButtonSaveData);
                dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(charge.dataGridView1EditingControlShowing);
                dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(charge.dataGridViewCellValueChanged);
                buttonliste.Click += new EventHandler(charge.ButtonPropositionClick);
                dataGridView1.Sorted += new EventHandler(charge.dataGridView1_Sorted);
                buttonGroupe.Click += new EventHandler(charge.EnableGroupe);
                dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(charge.dataGridViewDetailRowPostPaint);
                this.SizeChanged += new EventHandler(charge.SizeChanged);
                charge.SetColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxSearch.Text != "")
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

        private void button2_Click(object sender, EventArgs e)
        {
            SystemUniteForm uniteForm = new SystemUniteForm(Properties.Settings.Default.Langue);
            uniteForm.ShowDialog();
        }
    }
}
