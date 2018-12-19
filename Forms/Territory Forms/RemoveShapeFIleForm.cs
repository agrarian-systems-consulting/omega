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

namespace OMEGA.Forms.Territory_Forms
{
    internal partial class RemoveShapeFIleForm : Form
    {
        private int langue = Properties.Settings.Default.Langue;

        internal RemoveShapeFIleForm(List<InfoShapeFile> listshapefile)
        {
            

            try
            {

                InitializeComponent();
                FillCheckedList(listshapefile);
                Translate();
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void Translate()
        {
            try
            {
                label1.Text = Translation.Translate("Select layers to remove", langue);
                buttonAll.Text = Translation.Translate("All", langue);
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                None.Text = Translation.Translate("None", langue);
                this.Text = Translation.Translate("Remove layers", langue);
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void FillCheckedList(List<InfoShapeFile> listshapefile)
        {
            try
            {
                foreach (InfoShapeFile shapefile in listshapefile)
                {
                    checkedListBox1.Items.Add(shapefile.FileName + " (" + shapefile.FilePath + ")", false);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonok_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void buttonAll_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= checkedListBox1.Items.Count - 1; i++)
                {
                    checkedListBox1.SetItemChecked(i, true);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void None_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i <= checkedListBox1.Items.Count - 1; i++)
                {
                    checkedListBox1.SetItemChecked(i, false);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }
    }
}
