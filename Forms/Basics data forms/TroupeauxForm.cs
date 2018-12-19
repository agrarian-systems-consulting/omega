using OMEGA.Data_Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class TroupeauxForm : Form
    {

        private Troupeaux troupeaux;
        internal event EventHandler<SaveDataEvent> SaveData;

        internal TroupeauxForm()
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            TranslateCaption();
            troupeaux = new Troupeaux(dataGridView1, buttonOk, buttonSave, buttonCancel,
            buttonRemove, buttonDuplicate, pictureBoxExport,buttongroup,buttonproposition);
          
        }

        private void TranslateCaption()
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;
                buttonOk.Text = Translation.Translate("OK", langue);
                buttonAdd.Text = Translation.Translate("Add", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonDuplicate.Text = Translation.Translate("Duplicate", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
                buttongroup.Text = Translation.Translate("Enable group", langue);
                buttonproposition.Text = Translation.Translate("Disable autofill", langue);
                this.Text = Translation.Translate("Herds Form", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void TroupeauxForm_Load(object sender, EventArgs e)
        {
            buttonRemove.Click += new EventHandler(troupeaux.ButtonRemoveRow);
            buttonDuplicate.Click += new EventHandler(troupeaux.ButtonDuplicate);
            buttonCancel.Click += new EventHandler(troupeaux.buttonCancel);
            pictureBoxExport.Click += new EventHandler(troupeaux.ExportTable);
            buttonAdd.Click += new EventHandler(troupeaux.ButtonAdd);
            pictureBoxExport.MouseLeave += new EventHandler(troupeaux.pictureBox_MouseLeave);
            pictureBoxExport.MouseEnter += new EventHandler(troupeaux.pictureBox_MouseEnter);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(troupeaux.dataGridView1CellClick);
            SaveData += new EventHandler<SaveDataEvent>(troupeaux.ButtonSaveData);
            dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(troupeaux.dataGridView1EditingControlShowing);
            dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(troupeaux.dataGridView1CellEndEdit);
            dataGridView1.MouseClick += new MouseEventHandler(troupeaux.dataGridViewMouseClick);
            dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(troupeaux.dataGridViewDetailRowPostPaint);
            this.MouseClick += new MouseEventHandler(troupeaux.dataGridViewMouseClick);
            dataGridView1.CellClick += new DataGridViewCellEventHandler(troupeaux.dataGridView1CellClick);
            dataGridView1.KeyPress += new KeyPressEventHandler(troupeaux.keyPress);
            this.KeyPress += new KeyPressEventHandler(troupeaux.keyPress);
            buttongroup.Click += new EventHandler(troupeaux.EnableGroupe);
            buttonproposition.Click += new EventHandler(troupeaux.ButtonPropositionClick);
            dataGridView1.Sorted += new EventHandler(troupeaux.dataGridView1_Sorted);
            dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(troupeaux.dataGridViewDetailRowPostPaint);
            buttonNotes.Click += new EventHandler(troupeaux.NoteForm);
            troupeaux.SetColor();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(this, new SaveDataEvent(true, ""));
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(this, new SaveDataEvent(true, ""));
            this.Close();
        }

        private void dataGridView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Control control = dataGridView1.EditingControl;
            control.Hide();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Control control = dataGridView1.EditingControl;
            control.Hide();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            Control control = dataGridView1.EditingControl;
            control.Hide();
        }

        private void toolTip1_Popup(object sender, PopupEventArgs e)
        {

        }
    }
}
