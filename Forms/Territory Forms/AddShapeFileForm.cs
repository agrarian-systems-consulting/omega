using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace OMEGA.Forms.Territory_Forms
{
    internal partial class AddShapeFileForm : Form
    {
        private int langue = Properties.Settings.Default.Langue;

        internal AddShapeFileForm()
        {
            InitializeComponent();
            Translate();
        }


        private void LoadComboBoxType()
        {
            try
            {
                if (groupBox2.Enabled)
                {
                    DataTable table = new DataTable();
                    string query = SQLQueryBuilder.SelectQuery("Type", "*");
                    SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                    adapter.Fill(table);
                    comboBoxtype.DataSource = table;
                    comboBoxtype.ValueMember = "Nom";
                    comboBoxname.Enabled = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
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

        private void buttonload_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog DialogueOpenFile = new OpenFileDialog();
                DialogueOpenFile.Filter = "fichiers ShapeFile (*.shp)|*.shp| Tous les fichiers (*.*)|*.*";
                DialogueOpenFile.RestoreDirectory = true;
                DialogueOpenFile.ValidateNames = true;
                DialogueOpenFile.SupportMultiDottedExtensions = false;
                DialogueOpenFile.CheckFileExists = false;
                DialogueOpenFile.CheckPathExists = true;
                DialogueOpenFile.FilterIndex = 1;

                if (DialogueOpenFile.ShowDialog() == DialogResult.OK)
                {
                    if (Commun.LoadShapeFile(DialogueOpenFile.FileName))
                    {
                        textBoxLoad.Text = DialogueOpenFile.FileName;
                        textBoxName.Text = Commun.RemoveExtension(DialogueOpenFile.SafeFileName, 3);
                        LoadComboBoxType();
                        LoadComboBoxName();
                    }
                    textBoxLoad.Text = DialogueOpenFile.FileName;
                    textBoxName.Text = Commun.RemoveExtension(DialogueOpenFile.SafeFileName, 3);
                    LoadComboBoxType();
                    LoadComboBoxName();
                } 
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Program. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBoxLoad.Text == "")
                {
                    MessageBox.Show(Translation.Translate("Please load a shapefile",langue));
                    return;
                }
                //if (comboBoxtype.Text == "")
                //{
                //    MessageBox.Show("Merci de selectionner un type pour ce shapefile");
                //    return;
                //}
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttoncancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void comboBoxtype_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                LoadComboBoxName();
                if (comboBoxname.Text != "" && comboBoxtype.Text != "")
                {
                    buttonOK.Enabled = true;
                }
                else
                {
                    buttonOK.Enabled = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxMainMap.Checked)
            {
                groupBox2.Enabled = false;
                comboBoxtype.Text = "";
                comboBoxname.Text = "";
                buttonOK.Enabled = true;
            }
            else
            {
                groupBox2.Enabled = true;
                LoadComboBoxType();
                LoadComboBoxName();
                buttonOK.Enabled = false;
            }
        }

        private void checkBox1_MouseEnter(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Help;
        }

        private void checkBox1_MouseLeave(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        private void checkBox1_MouseHover(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.Help;
        }

        private void comboBoxname_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                textBoxName.Text = comboBoxname.Text;
                if (comboBoxname.Text != "" && comboBoxtype.Text != "")
                {
                    buttonOK.Enabled = true;
                }
                else
                {
                    buttonOK.Enabled = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void textBoxLoad_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxLoad.Text != "" && textBoxName.Text != "")
                {
                    buttonOK.Enabled = true;
                }
                else
                {
                    buttonOK.Enabled = false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBoxLoad.Text != "" && textBoxName.Text != "")
                {
                    buttonOK.Enabled = true;
                }
                else
                {
                    buttonOK.Enabled = false;
                }
                DialogResult dialogResult = new DialogResult();
                if (Commun.NameExists(textBoxName.Text, "SHP_Info", "Nom"))
                {
                    dialogResult = MessageBox.Show(Translation.Translate("A ShapeFile with this name already exists. It may generate some errors " +
                        "on reports or calculs. Do you want to continue ?",langue), "Warning", MessageBoxButtons.YesNo);
                    if (dialogResult == DialogResult.No)
                    {
                        textBoxName.Text = "";
                        buttonOK.Enabled = false;
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

        private void Translate()
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;
                labelname.Text = Translation.Translate("Shapefile name :", langue);
                labelLoad.Text = Translation.Translate("Load shapefile :", langue);
                checkBoxMainMap.Text = Translation.Translate("Territory map", langue);
                groupBox1.Text = Translation.Translate("Shapefile information", langue);
                groupBox2.Text = Translation.Translate("Data type information", langue);
                labeltype.Text = Translation.Translate("Type", langue);
                labelnametype.Text = Translation.Translate("Name", langue);
                buttoncancel.Text = Translation.Translate("Cancel", langue);
                buttonOK.Text = Translation.Translate("OK", langue);
                this.Text = Translation.Translate("Add shapefile", langue);
                labelLoad.Text = Translation.Translate("Load shapefile", langue);
               
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}
