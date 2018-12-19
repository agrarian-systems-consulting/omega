using OMEGA.Forms;
using OMEGA.Forms.Common_forms;
using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace OMEGA.Data_Classes
{
    /// <summary>
    /// ICi on gère une partie des traductions et évènement utilisateur de la page principale
    /// </summary>
    class MainView
    {
        private ExitForm exitform = new ExitForm();
        private MainForm mMainForm;
        private Color color1 = new Color();
        private Color color2 = new Color();
        private Color color3 = new Color();
        private Subro.Controls.DataGridViewGrouper grouper = new Subro.Controls.DataGridViewGrouper();
        private DataGridView dataGridView;
        private Button mbuttongroup;
        private int langue = Properties.Settings.Default.Langue;

        internal MainView(MainForm form, DataGridView dgv, Button bttngrp)
        {
            mMainForm = form;
            dataGridView = dgv;
            mbuttongroup = bttngrp;
        }

        internal void translateText()
        {
            langue = Properties.Settings.Default.Langue;
            mMainForm.buttonAnimaux.Text = Translation.Translate("Animals", langue);
            mMainForm.buttonOkActivity.Text = Translation.Translate("Save", langue);
            mMainForm.buttonCulture.Text = Translation.Translate("Crop", langue);
            mMainForm.ButtonPhases.Text = Translation.Translate("Phasis", langue);
            mMainForm.buttonProduct.Text = Translation.Translate("Products", langue);
            mMainForm.buttoncharges.Text = Translation.Translate("Cost", langue);
            mMainForm.buttonTVA.Text = Translation.Translate("TVA", langue);
            mMainForm.buttontroupeaux.Text = Translation.Translate("Herds", langue);
            mMainForm.buttonpluriannuelle.Text = Translation.Translate("Pluriannual", langue);
            mMainForm.buttonPérennes.Text = Translation.Translate("Perennial", langue);
            mMainForm.buttonprix.Text = Translation.Translate("Price", langue);
            mMainForm.buttonQuantités.Text = Translation.Translate("Quantity", langue);
            mMainForm.buttonoutPut.Text = Translation.Translate("Out put", langue);
            mMainForm.buttonClassification.Text = Translation.Translate("Classification", langue);
            mMainForm.buttonComparaison.Text = Translation.Translate("Compare", langue);
            mMainForm.buttonAutreAgri.Text = Translation.Translate("Other", langue);
            mMainForm.buttonEncoursAgri.Text = Translation.Translate("Current", langue);
            mMainForm.buttonUnite.Text = Translation.Translate("Unity", langue);
            mMainForm.buttonRecetteDivers.Text = Translation.Translate("Misc Incomes", langue);
            mMainForm.buttonRecetteFamille.Text = Translation.Translate("Family Incomes", langue);
            mMainForm.buttondepensefamille.Text = Translation.Translate("Family Expenses", langue);
            mMainForm.buttonDépenseDivers.Text = Translation.Translate("Misc Expenses", langue);
            mMainForm.buttonvariable.Text = Translation.Translate("Variables", langue);
            mMainForm.buttonExtern.Text = Translation.Translate("Externalities", langue);
            mMainForm.buttonChargeStruct.Text = Translation.Translate("Structural costs", langue);
            mMainForm.buttonPeriode.Text = Translation.Translate("Periode", langue);
            mMainForm.buttonIndicateur.Text = Translation.Translate("Indicators", langue);
            mMainForm.buttonNotes.Text = Translation.Translate("Notes", langue);
            mMainForm.labelAtelier.Text = Translation.Translate("For", langue) + " " + Translation.Translate("Production ", langue);
            mMainForm.labelEntr.Text = Translation.Translate("For", langue) + " " + Translation.Translate("Firm", langue);
            mMainForm.labelFamily.Text = Translation.Translate("For", langue) + " " + Translation.Translate("Family", langue);
            mMainForm.labelDivers.Text = Translation.Translate("For", langue) + " " + Translation.Translate("Misc", langue);
        }

        internal void ButtonVariableClick(object sender, EventArgs e)
        {
            Form form = new VariableForm();
            form.ShowDialog();
            Properties.Settings.Default.DoitSauvegarger = true;
        }

        internal void ButtonAboutClick(object sender, EventArgs e)
        {
            Form form = new InfoForm();
            form.ShowDialog();
        }



        internal void LoadTVAForm(object sender, EventArgs e)
        {
            Form tvaform = new Forms.TVAForm(Properties.Settings.Default.Langue);
            tvaform.ShowDialog();
            if (tvaform.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void LoadSystemeUnitForm(object sender, EventArgs e)
        {
            Form SystemunitForm = new Forms.SystemUniteForm(Properties.Settings.Default.Langue);
            SystemunitForm.ShowDialog();
            if (SystemunitForm.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void LoadClassifcationForm(object sender, EventArgs e)
        {
            ClassificationForm classiffForm = new ClassificationForm();
            classiffForm.ShowDialog();
            if (classiffForm.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void LoadProductForm(object sender, EventArgs e)
        {
            Form productform = new Forms.ProductForm(Properties.Settings.Default.Langue);
            productform.ShowDialog();
            if (productform.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void LoadChargeFormStruc(object sender, EventArgs e)
        {
            Form chargesform = new ChargesForm(Properties.Settings.Default.Langue,1);
            chargesform.ShowDialog();
            if (chargesform.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void LoadChargeFormOpe(object sender, EventArgs e)
        {
            Form chargesform = new ChargesForm(Properties.Settings.Default.Langue,0);
            chargesform.ShowDialog();
            if (chargesform.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void LoadExternaliteForm(object sender, EventArgs e)
        {
            Form externform = new Forms.ExternaliteForm(Properties.Settings.Default.Langue);
            externform.ShowDialog();
            if (externform.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void LoadExpenseFamilyForm(object sender, EventArgs e)
        {
            
            RecetteForm form = new RecetteForm(1, 1);
            form.Show();
            Properties.Settings.Default.DoitSauvegarger = true;
        }

        internal void LoadIncomeFamilyForm(object sender, EventArgs e)
        {
            RecetteForm form = new RecetteForm(0,1);
            form.Show();
            Properties.Settings.Default.DoitSauvegarger = true;
        }

        internal void LoadExpenseEntrepriseForm(object sender, EventArgs e)
        {
            RecetteForm form = new RecetteForm(1,0);
            form.Show();
            Properties.Settings.Default.DoitSauvegarger = true;
        }

        internal void LoadIncomeEntrepriseForm(object sender, EventArgs e)
        {
            RecetteForm form = new RecetteForm(0,0);
            form.Show();
        }

        internal void LoadNoteForm(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("MainView");
            form.Show();
            Properties.Settings.Default.DoitSauvegarger = true;
        }

        internal void buttontroupeaux_Click(object sender, EventArgs e)
        {
            Form form = new TroupeauxForm();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void importGPSPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new ImportGPSDataForm();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void manageGPSPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new PointGPSForm();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void mananeLineColorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form form = new ColorForm();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void dictionnaryDefinitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form DicoForm = new Forms.DictionnaryForm(Properties.Settings.Default.Langue);
            DicoForm.ShowDialog();
            if (DicoForm.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void ButtonPhaseClick(object sender, EventArgs e)
        {
            StandardForm form = new StandardForm(0, "phase");
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }

        }

        internal void SetColor()
        {
            try
            {
                string query = "Select * From Couleur;";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ARVB1", query);
                if (Commun.ListHasValue(list))
                {
                    color1 = Color.FromArgb(list[0]);
                }
                List<int> list2 = SQlQueryExecuter.RunQueryReaderInt("ARVB2", query);
                if (Commun.ListHasValue(list2))
                {
                    color2 = Color.FromArgb(list2[0]);
                }
                List<int> list3 = SQlQueryExecuter.RunQueryReaderInt("ARVB3", query);
                if (Commun.ListHasValue(list3))
                {
                    color3 = Color.FromArgb(list3[0]);
                }
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
     
        internal void ShowResultForm(object sender, EventArgs e)
        {
            try
            { 
                mMainForm.Hide();
                int id=0;
                string query = SQLQueryBuilder.SelectQuery("Exploitation", "ID", "Where Encours = '1';");
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (Commun.ListHasValue(list))
                {
                    id = list[0];
                }
            
                ResultForm resultForm = new ResultForm(id);
                resultForm.ShowDialog();
                mMainForm.Show();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void LoadPeriodeForm(object sender, EventArgs e)
        {
            Form periodeform = new PeriodeForm()  ;
            periodeform.ShowDialog();
            if (periodeform.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }

        }

        internal void LoadTroupeauxForm(object sender, EventArgs e)
        {
            Form form = new TroupeauxForm();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        private void ManageColor()
        {
            if (color1 != null && color2 != null)
            {
                Commun.Setbackground(dataGridView, color1, color2);
                string query = SQLQueryBuilder.UpdateColorQuery(color1, color2,color3);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
            }
        }

        internal void EnableGroupe(object sender, EventArgs e)
        {
            try
            {
                if (mbuttongroup.Text.Contains("Enable"))
                {
                    grouper.DataGridView = dataGridView;
                    grouper.SetGroupOn(dataGridView.Columns[2]);
                    grouper.DisplayGroup += grouper_DisplayGroup;
                    grouper.CollapseAll();
                    mbuttongroup.Text = "Disable Groupe";
                    return;
                }
                if (mbuttongroup.Text.Contains("Disable"))
                {
                    grouper.DataGridView = dataGridView;
                    grouper.RemoveGrouping();
                    grouper.DisplayGroup -= grouper_DisplayGroup;
                    mbuttongroup.Text = "Enable Groupe";
                    ManageColor();
                    return;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void grouper_DisplayGroup(object sender, Subro.Controls.GroupDisplayEventArgs e)
        {
            e.BackColor = (e.Group.GroupIndex % 2) == 0 ? color1 : color2;
        }

        internal void dataGridView1_Sorted(object sender, EventArgs e)
        {
            ManageColor();
        }

        internal void dataGridViewDetailRowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            try
            {
                var grid = sender as DataGridView;
                var rowIdx = (e.RowIndex + 1).ToString();

                var centerFormat = new StringFormat()
                {
                    // right alignment might actually make more sense for numbers
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                Font corbel = new Font("Corbel", 10);
                var headerBounds = new Rectangle(e.RowBounds.Left, e.RowBounds.Top, grid.RowHeadersWidth, e.RowBounds.Height);
                e.Graphics.DrawString(rowIdx, corbel, SystemBrushes.ControlText, headerBounds, centerFormat);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void MouseEnterImageActivity(object sender, EventArgs e)
        {
            mMainForm.pictureBoxActivity.BorderStyle = BorderStyle.None;
        }
        internal void MouseLeaveImageActivity(object sender, EventArgs e)
        {
            mMainForm.pictureBoxActivity.BorderStyle = BorderStyle.Fixed3D;
        }
        internal void MouseEnterImageRegion(object sender, EventArgs e)
        {
            mMainForm.pictureBoxRegion.BorderStyle = BorderStyle.None;
        }
        internal void MouseLeaveImageRegion(object sender, EventArgs e)
        {
            mMainForm.pictureBoxRegion.BorderStyle = BorderStyle.Fixed3D;
        }
        internal void MouseEnterImageExploitation(object sender, EventArgs e)
        {
            mMainForm.pictureBoxExploitation.BorderStyle = BorderStyle.None;
        }
        internal void MouseLeaveImageExploitation(object sender, EventArgs e)
        {
            mMainForm.pictureBoxExploitation.BorderStyle = BorderStyle.Fixed3D;
        }

        internal void buttonprix_Click(object sender, EventArgs e)
        {
            Form AleasForm = new AleasForm(true);
            AleasForm.ShowDialog();
            if (AleasForm.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        internal void buttonQuantités_Click(object sender, EventArgs e)
        {
            Form AleasForm = new AleasForm(false);
            AleasForm.ShowDialog();
            if (AleasForm.DialogResult == DialogResult.OK)
            {
                mMainForm.Text = mMainForm.Text + "*";
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

    }
}

