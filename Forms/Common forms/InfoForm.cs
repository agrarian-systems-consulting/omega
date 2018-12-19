using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Forms.Common_forms
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
            GetVersion();
        }

        private void GetVersion()
        {
            labelNoVers.Text = "1.0.0";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void InfoForm_Load(object sender, EventArgs e)
        {
            int langue = Properties.Settings.Default.Langue;
            this.Text = Translation.Translate("About", langue);
            labelDev.Text = Translation.Translate("Licence :", langue);
            labelVersion.Text = Translation.Translate("Version :", langue);
            label1.Text = Translation.Translate("Institutions :", langue);
        }
    }
}
