using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Forms.Territory_Forms
{
    public partial class NofileFoundForm : Form
    {
        private string filePath = "";
        private int langue = Properties.Settings.Default.Langue;
        public NofileFoundForm(string path)
        {
            InitializeComponent();
            filePath = path;
            this.FormClosed += OnClose;
            LoadTexts();
        }

        private void LoadTexts()
        {
            try
            {
                label1.Text = Translation.Translate("The specified file was not found on the path", langue) + " : " + filePath + Environment.NewLine +
                    Translation.Translate("Could you check if this file has been deleted or moved away.", langue);

                checkBox1.Text = Translation.Translate("Do not see this message again ? (the file path will be deleted from the current database)", langue);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void OnClose(object sender,EventArgs e)
        {
            try
            {
                if (checkBox1.Checked)
                {
                    string query = "Delete From SHP_Info Where path LIKE '" + filePath + "';";
                    SQlQueryExecuter.RunQuery(query);
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
