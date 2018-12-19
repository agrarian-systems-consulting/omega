using OMEGA.Data_Classes;
using System;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class VariableForm : Form
    {

        private Variable variable;
        internal event EventHandler<SaveDataEvent> SaveData;

        internal VariableForm()
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            variable = new Variable(this);
            SubscribeEvent();
        }

        private void SubscribeEvent()
        {
            buttonAdd.Click += variable.ButtonAdd;
            buttonRemove.Click += variable.ButtonRemoveRow;
            buttonCancel.Click += variable.buttonCancel;
            buttonDuplicate.Click += variable.ButtonDuplicate;
            buttonGroupe.Click += variable.ButtonPropositionClick;
            dataGridView1.CellClick += variable.dataGridView1CellClick;
            dataGridView1.CellEndEdit += variable.dataGridView1CellEndEdit;
            dataGridView1.RowPostPaint += variable.dataGridViewDetailRowPostPaint;
            dataGridView1.EditingControlShowing += variable.dataGridView1EditingControlShowing;
            dataGridView1.MouseClick += variable.dataGridViewMouseClick;
            pictureBoxExport.MouseEnter += variable.pictureBox_MouseEnter;
            pictureBoxExport.MouseLeave += variable.pictureBox_MouseLeave;
            pictureBoxExport.Click += variable.ExportTable;
            dataGridView1.Sorted += variable.dataGridView1_Sorted;
            buttonGroupe.Click += variable.EnableGroupe;
            buttonProposition.Click += variable.ButtonPropositionClick;
            SaveData += new EventHandler<SaveDataEvent>(variable.ButtonSaveData);
            variable.SetColor();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(this, new SaveDataEvent(true, "Variable"));
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(this, new SaveDataEvent(true, "Variable"));
            this.Close();
        }
    }
}
