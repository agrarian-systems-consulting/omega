using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class NotesForm : Form
    {
        private string table;
        private bool TextModified;
        private int langue = Properties.Settings.Default.Langue;

        internal NotesForm(string tbl)
        {
            try
            {
                InitializeComponent();
                table = tbl;
                LoadText();
                Translate();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void Translate()
        {
            buttonCancel.Text = Translation.Translate("Cancel", langue);
            this.Text = Translation.Translate("Note", langue);
        }


        private void LoadText()
        {
            try
            {
                string query = "Select Valeur From Notes Where Notes.Tables ='" + table + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Valeur", query);
                if (Commun.ListHasValue(list))
                {
                    textBox1.Text = list[0];
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            
        }


        private void buttonok_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Select * From Notes Where Notes.Tables ='" + table + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("ID", query);
                if (Commun.ListHasValue(list))
                {
                    int id;
                    int.TryParse(list[0], out id);
                    query = "UPDATE Notes Set Valeur = '" + textBox1.Text + "' WHERE ID = '" + id + "'";
                }
                else
                {
                    query = "Insert into Notes (Tables, Valeur) " +
                        " VALUES ('" + table + "','" + textBox1.Text + "');";
                }
                SQlQueryExecuter.RunQuery(query);
                TextModified = false;
                this.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = new DialogResult();
            if (TextModified)
            {
                dialogResult = MessageBox.Show(Translation.Translate("Data has been modified but not saved, do you really want to exit ?",langue), "Warning", MessageBoxButtons.YesNo);
            }
            else
            {
                this.Close();
            }
            if (dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextModified = true;
        }
    }
}
