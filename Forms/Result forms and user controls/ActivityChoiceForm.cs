using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OMEGA.Forms.ResultUserControl
{
    internal partial class ActivityChoiceForm : Form
    {

        private int mIdExpl;
        private List<string> selectedActivities = new List<string>();
        private int langue = Properties.Settings.Default.Langue;

        internal ActivityChoiceForm()
        {
            try
            {
                InitializeComponent();
                mIdExpl = Commun.GetIdExpl();
                LoadCheckListBox();
                toolTip1.SetToolTip(checkedListBox1, Translation.Translate("Only 1 item can be selected.", langue));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }



        private void LoadCheckListBox()
        {
            try
            {
                string query = ActivityQuery.SelectActivityQuery(mIdExpl);
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string item in list)
                {
                    checkedListBox1.Items.Add(item);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void button_All_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                for (int i = 0; i <= checkedListBox1.Items.Count - 1; i++)
                {
                    checkedListBox1.SetItemChecked(index, true);
                    if (!selectedActivities.Contains(checkedListBox1.GetItemText(i)))
                    {
                        selectedActivities.Add(checkedListBox1.Items[index].ToString());
                    }
                    index++;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonNone_Click(object sender, EventArgs e)
        {
            try
            {
                int index = 0;
                for (int i = 0; i <= checkedListBox1.Items.Count - 1; i++)
                {
                    checkedListBox1.SetItemChecked(index, false);
                    index++;
                }
                selectedActivities.Clear();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            try
            {
                string selectedItemText = checkedListBox1.SelectedItem.ToString();
                int idAct = SQLQueryBuilder.FindId("Activite", "ID", "Nom", selectedItemText);
                StandardForm form = new StandardForm(selectedActivities, idAct);
                form.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int index = 0;
            //try
            //{
            //    for (int i = 0; i <= checkedListBox1.Items.Count - 1; i++)
            //    {
            //        if (checkedListBox1.GetItemChecked(i))
            //        {
            //            if (!selectedActivities.Contains(checkedListBox1.GetItemText(i)))
            //            {
            //                selectedActivities.Add(checkedListBox1.GetItemText(i));
            //            }
            //        }
            //    }
            //    index++;

            //}
            //catch (Exception Ex)
            //{
            //    MessageBox.Show(Ex.Message);
            //    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            //}
        }

        private void checkedListBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            int index = 0;
            try
            {
                for (int i = 0; i <= checkedListBox1.Items.Count - 1; i++)
                {

                    if (i == checkedListBox1.SelectedIndex)
                    {
                        checkedListBox1.SetItemChecked(i, true);
                    }
                    else
                    {
                        checkedListBox1.SetItemChecked(i, false);
                    }

                    //if (checkedListBox1.GetItemChecked(i))
                    //{


                    //    if (!selectedActivities.Contains(checkedListBox1.GetItemText(i)))
                    //    {
                    //        selectedActivities.Add(checkedListBox1.SelectedItem.ToString());
                    //    }
                    //}
                }
                index++;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}
