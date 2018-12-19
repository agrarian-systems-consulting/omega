using System;
using System.Collections.Generic;
using OMEGA.Data_Classes;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class ExternaliteForm : Form
    {
        private Externalite externalite;
        internal event EventHandler<SaveDataEvent> SaveData;
        

        internal ExternaliteForm(int langue)
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            if (File.Exists(Properties.Settings.Default.FichierTraduction))
            {
                SetCaption(langue);
            }

            try
                {
                    externalite = new Externalite(dataGridView1,  buttonOk, buttonSave,
                    buttonCancel, buttonRemove, buttonDuplicate, buttonProposition,buttongroupe
                    ,null,0,pictureBox8,this);
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
        }


        private void ExternaliteForm_Load(object sender, EventArgs e)
        {
            try
            {
                // when the form is loaded, we subscribe to the events 
                buttonRemove.Click += new EventHandler(externalite.ButtonRemoveRow);
                buttonDuplicate.Click += new EventHandler(externalite.ButtonDuplicate);
                buttonCancel.Click += new EventHandler(externalite.buttonCancel);
                buttonAdd.Click += new EventHandler(externalite.ButtonAdd);
                pictureBox8.Click += new EventHandler(externalite.ExportTable);
                pictureBox8.MouseLeave += new EventHandler(externalite.pictureBox_MouseLeave);
                pictureBox8.MouseEnter += new EventHandler(externalite.pictureBox_MouseEnter);
                dataGridView1.CellClick += new DataGridViewCellEventHandler(externalite.dataGridView1CellClick);
                dataGridView1.CellEndEdit += new DataGridViewCellEventHandler(externalite.dataGridView1CellEndEdit);
                dataGridView1.RowPostPaint += new DataGridViewRowPostPaintEventHandler(externalite.dataGridViewDetailRowPostPaint);
                SaveData += new EventHandler<SaveDataEvent>(externalite.ButtonSaveData);
                dataGridView1.EditingControlShowing += new DataGridViewEditingControlShowingEventHandler(externalite.dataGridView1EditingControlShowing);
                buttongroupe.Click += new EventHandler(externalite.EnableGroupe);
                buttonProposition.Click += new EventHandler(externalite.ButtonPropositionClick);
                externalite.SetColor();
               
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        
        private void SetCaption(int langue)
        {
            try
            {
                this.Text = Translation.Translate("Externalite", langue);
                buttonOk.Text = Translation.Translate("OK", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonAdd.Text = Translation.Translate("Ajouter", langue);
                buttonDuplicate.Text = Translation.Translate("Duplicate", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(this, new SaveDataEvent(true, ""));
            this.Close();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveData?.Invoke(this, new SaveDataEvent(true, ""));
        }


    }
}
