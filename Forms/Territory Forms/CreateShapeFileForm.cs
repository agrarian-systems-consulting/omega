using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace OMEGA.Forms.Territory_Forms
{
    public partial class CreateShapeFileForm : Form
    {

        internal List<string> ListeFiltre = new List<string>();
        private int langue = Properties.Settings.Default.Langue;

        public CreateShapeFileForm()
        {
            InitializeComponent();
            LoadComboBoximport();
            Translate();
        }

        private void LoadComboBoximport()
        {
            string query = SQLQueryBuilder.SelectQuery("GPS","distinct import_name");
            List<string> listimport = SQlQueryExecuter.RunQueryReaderStr("import_name", query);
            comboBox1Import.DataSource = listimport;
            comboBox1Import.Text = "";
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBoxpath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void checkBoxMainMap_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMainMap.Checked)
            {
                comboBoxname.Enabled = false;
                comboBoxtype.Enabled = false;
                comboBoxname.Text = "";
                comboBoxtype.Text = "";
                buttonOk.Enabled = true;
            }
            else
            {
                comboBoxname.Enabled = true;
                comboBoxtype.Enabled = true;
                LoadComboBoxType();
                LoadComboBoxName();
                buttonOk.Enabled = false;
            }
        }

        private void LoadComboBoxType()
        {
            if (groupBox2.Enabled)
            {
                DataTable table = new DataTable();
                string query = SQLQueryBuilder.SelectQuery("Type","*");
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table);
                comboBoxtype.DataSource = table;
                comboBoxtype.ValueMember = "Nom";
                comboBoxtype.Text = "";
            }
        }

        private void LoadComboBoxName()
        {
            try
            {
                if (groupBox2.Enabled)
                {
                    string type = comboBoxtype.Text;
                    int IdType = comboBoxtype.SelectedIndex;
                    IdType += 1;
                    if (IdType != 6 && IdType != 0)
                    {
                        string query = SQLQueryBuilder.SelectQuery("Activite", "*", "Where IdType = '" + IdType + "'");
                        List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        comboBoxname.DataSource = list;
                        comboBoxname.Text = "";
                    }
                    if (IdType == 7)
                    {
                        string query = SQLQueryBuilder.SelectQuery("Exploitation", "*");
                        List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                        comboBoxname.DataSource = list;
                        comboBoxname.Text = "";
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void textBoxname_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxname.Text != "")
                {
                    groupBox2.Enabled = true;
                    buttonOk.Enabled = true;
                }
                else
                {
                    groupBox2.Enabled = false;
                    buttonOk.Enabled = false;
                }

                DialogResult dialogResult = new DialogResult();
                if (Commun.NameExists(textBoxname.Text, "SHP_Info", "Nom"))
                {
                    dialogResult = MessageBox.Show(Translation.Translate("An item with this name already exists. It may generate some errors on reports or calculs. Do you want to continue ?", langue), "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        textBoxname.Text = "";
                        buttonOk.Enabled = false;
                        return;
                    }
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void textBoxpath_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxname.Text != "")
                {
                    groupBox2.Enabled = true;
                    buttonOk.Enabled = true;
                }
                else
                {
                    groupBox2.Enabled = false;
                    buttonOk.Enabled = false;
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void comboBoxtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxname.Text != "" )
            {
                buttonOk.Enabled = true;
            }
            else
            {
                buttonOk.Enabled = false;
            }
            LoadComboBoxName();
        }

        private void LoadComboBoxFilter()
        {
            string query = SQLQueryBuilder.SelectQuery("GPS", "distinct CODE","Where import_name = '" + comboBox1Import.Text + "'");
            ListeFiltre = SQlQueryExecuter.RunQueryReaderStr("CODE", query);
        }

        private void comboBox1Import_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadComboBoxFilter();
        }

        private void comboBoxname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxname.Text != "")
            {
                buttonOk.Enabled = true;
                textBoxname.Text = comboBoxname.Text;
            }
            else
            {
                buttonOk.Enabled = false;
            }
        }

        private void Translate()
        {
            int langue = Properties.Settings.Default.Langue;
            labelname.Text = Translation.Translate("Shapefile name :", langue);
            labelpath.Text = Translation.Translate("select folder to save the file", langue);
            checkBoxMainMap.Text = Translation.Translate("Territory map", langue);
            groupboxfileinfo.Text = Translation.Translate("Shapefile information", langue);
            groupBox2.Text = Translation.Translate("Data type information", langue);
            labeltype.Text = Translation.Translate("Type", langue);
            labelImport.Text = Translation.Translate("Select the import to load", langue);
            labelImportAll.Text = Translation.Translate("Multiple elements?", langue);
            labelnametype.Text = Translation.Translate("Name", langue);
            buttonCancel.Text = Translation.Translate("Cancel", langue);
            buttonOk.Text = Translation.Translate("OK", langue);
            this.Text = Translation.Translate("Create shapefile", langue);
            labelpath.Text = Translation.Translate("Path to save the new shapefile :", langue);
            labelOr.Text = Translation.Translate("OR", langue);
            groupBox1.Text = Translation.Translate("Import management", langue);
            toolTip1.SetToolTip(labelImportAll, Translation.Translate("If the import contains a couple of lines or a couple of polygone in the same .gpx file, check yes.", langue));
            labelSaveChoice.Text = Translation.Translate("Save the shapefile on this dataset", langue);
            labelpath.Text = Translation.Translate("Path to save the new shapefile", langue);
            radioButtonNo.Text = Translation.Translate("No", langue);
            radioButtonNo1.Text = Translation.Translate("No", langue);
            radioButtonYes.Text = Translation.Translate("Yes", langue);
            radioButtonYes1.Text = Translation.Translate("Yes", langue);

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            labelpath.Enabled = true;
            buttonBrowse.Enabled = true;
            textBoxpath.Enabled = true;
        }

        private void radioButtonYes1_CheckedChanged(object sender, EventArgs e)
        {
            labelpath.Enabled = false;
            buttonBrowse.Enabled = false;
            textBoxpath.Enabled = false;
        }
    }
}
