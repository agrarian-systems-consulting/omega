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
    internal partial class QueryTerritoryForm : Form
    {
        private int langue = Properties.Settings.Default.Langue;


        internal QueryTerritoryForm()
        {
            try
            {
                InitializeComponent();
                LoadComboBoxDisplayHide();
                LoadComboBoxMainType();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadComboBoxDisplayHide()
        {
            try
            {
                List<string> list = new List<string>();
                list.Add("Display only");
                list.Add("Display all");
                list.Add("Hide only");
                list.Add("Hide all");

                comboBoxdisplayhide.DataSource = list;
                UpdateQueryTextbox();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadComboBoxMainType()
        {
            try
            {
                List<string> list = new List<string>();
                list.Add(Translation.Translate("Territory", langue));
                list.Add(Translation.Translate("Farm", langue));
                list.Add(Translation.Translate("Activity", langue));
                list.Add("");

                comboBoxMainType.DataSource = list;
                UpdateQueryTextbox();
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadComboBoxSecondeType()
        {
           try
           {
                List<string> list = new List<string>();

                if (comboBoxMainType.Text == Translation.Translate("Farm", langue))
                {
                    list.Add(Translation.Translate("Primary", langue));
                    list.Add(Translation.Translate("Secondary", langue));
                }
                else if (comboBoxMainType.Text == Translation.Translate("Activity", langue))
                {
                    string query = "Select Nom from Type Where ID <> '7';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                }
                list.Add(Translation.Translate("That contains words :", langue));
                list.Add(Translation.Translate("That doesn't contain words :", langue));
                list.Add("");
                comboBoxSecondType.DataSource = list;
                UpdateQueryTextbox();
           }
            catch (Exception Ex)
           {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
           }
        }

        private void LoadLastBoxes()
        {
            try
            {
                List<string> list = new List<string>();
                Point point;
                if (comboBoxSecondType.Text.Contains(Translation.Translate("words", langue)))
                {
                    point = new Point(459, 16);
                    comboBoxFiltre.Visible = false;
                    textBoxMot.Location = point;
                }
                else if (comboBoxMainType.Text.Contains(Translation.Translate("Activity", langue)))
                {
                    point = new Point(659, 16);
                    comboBoxFiltre.Visible = true;
                    textBoxMot.Location = point;
                    List<string> list2 = new List<string>();
                    string query = "Select Nom From Exploitation;";
                    list2 = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    list.Add(Translation.Translate("From all working farm", langue));
                    foreach (string item in list2)
                    {
                        list.Add(Translation.Translate("From ", langue) + item);
                    }
                }
                list.Add(Translation.Translate("That contains words :", langue));
                list.Add(Translation.Translate("That doesn't contain words :", langue));
                list.Add("");
                comboBoxFiltre.DataSource = list;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void comboBoxMainType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                LoadComboBoxSecondeType();
                UpdateQueryTextbox();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void comboBoxSecondType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadLastBoxes();
                UpdateQueryTextbox();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void comboBoxFiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxFiltre.Text.Contains(Translation.Translate("words", langue)))
                {
                    textBoxMot.Visible = true;
                }
                else textBoxMot.Visible = false;
                UpdateQueryTextbox();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void UpdateQueryTextbox()
        {
            try
            {
                if (comboBoxFiltre.Visible)
                    textBoxQuery.Text = comboBoxdisplayhide.Text + " " + comboBoxMainType.Text + " " + comboBoxSecondType.Text + " " + comboBoxFiltre.Text + " " + textBoxMot.Text;
                else
                    textBoxQuery.Text = comboBoxdisplayhide.Text + " " + comboBoxMainType.Text + " " + comboBoxSecondType.Text + " " + textBoxMot.Text;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void textBoxMot_TextChanged(object sender, EventArgs e)
        {
            UpdateQueryTextbox();
        }
    }
}
