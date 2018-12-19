using OMEGA.Data_Classes;
using System;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class ClassificationForm : Form
    {
        private Classification classification;
        private int langue = Properties.Settings.Default.Langue;

        internal ClassificationForm()
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            classification = new Classification(dataGridView1,this,pictureBoxExport,buttongroup);
            SetCaption();
        }

        private void ClassificationForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                // when the form is loaded, we subscribe to the events 
                buttonRemove.Click += new EventHandler(classification.ButtonRemoveRow);
                buttonDuplicate.Click += new EventHandler(classification.ButtonDuplicate);
                buttonCancel.Click += new EventHandler(classification.buttonCancel);
                pictureBoxExport.Click += new EventHandler(classification.ExportTable);
                dataGridView1.CellClick += new DataGridViewCellEventHandler(classification.dataGridView1CellClick);
                buttonAdd.Click += new EventHandler(classification.ButtonAdd);
                buttonSave.Click += new EventHandler(classification.SaveData);
                pictureBoxExport.MouseLeave += new EventHandler(classification.pictureBox_MouseLeave);
                pictureBoxExport.MouseEnter += new EventHandler(classification.pictureBox_MouseEnter);
                buttonOk.Click += new EventHandler(classification.Buttonok);
                buttongroup.Click += new EventHandler(classification.EnableGroupe);
                dataGridView1.CellValueChanged += new DataGridViewCellEventHandler(classification.dataGridView1CellValueChanged);
                dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(classification.dataGridViewDetailRowPostPaint);
                dataGridView1.Sorted += new EventHandler(classification.dataGridView1_Sorted);
                classification.SetColor();
                //SaveData += new EventHandler<SaveDataEvent>(classification.SaveData);
                //AddRow += new EventHandler<AddRowEvent>(classification.ButtonAdd);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void SetCaption()
        {
            try
            {
                buttonOk.Text = Translation.Translate("OK", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonDuplicate.Text = Translation.Translate("Duplicate", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
                buttonAdd.Text = Translation.Translate("Add", langue);
                buttongroup.Text = Translation.Translate("Enable group", langue);
                this.Text = Translation.Translate("Classification", langue);
            }
               catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void buttonNotes_Click(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("Classification");
            form.ShowDialog();
        }
    }
}
