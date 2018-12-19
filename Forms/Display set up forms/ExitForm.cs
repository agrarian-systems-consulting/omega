using System;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class ExitForm : Form
    {
        int langue = Properties.Settings.Default.Langue;

        internal ExitForm(bool showCancel = true)
        {
            InitializeComponent();
            label1.Text = Translation.Translate("Warning, some data has been modified.", langue);
            buttonCancel.Text = Translation.Translate("Cancel", langue);
            buttonsavequit.Text = Translation.Translate("Save and quit", langue);
            buttonquitnosave.Text = Translation.Translate("Quit without saving", langue);
            if (!showCancel) buttonCancel.Visible = false;
        }



        internal int OutPutValue { get; set; }
        internal bool ShowForm { get; set; } = true;

        private void buttonsavequit_Click(object sender, EventArgs e)
        {
            OutPutValue = 1;
            ShowForm = false;
            this.Close();
        }

        private void buttonquitnosave_Click(object sender, EventArgs e)
        {
            OutPutValue = 2;
            ShowForm = false;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            OutPutValue = 3;
            ShowForm = false;
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ExitForm_Load(object sender, EventArgs e)
        {
            langue = Properties.Settings.Default.Langue;
            label1.Text = Translation.Translate("Warning, some data has been modified.", langue);
            buttonCancel.Text = Translation.Translate("Cancel", langue);
            buttonsavequit.Text = Translation.Translate("Save and quit", langue);
            buttonquitnosave.Text = Translation.Translate("Quit without saving", langue);
        }
    }
}
