using OMEGA.Forms.Result_forms_and_user_controls.user_controls;
using OMEGA.Forms.ResultUserControl;
using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    public partial class ResultForm : Form
    {
        private int id;
        private int langue = Properties.Settings.Default.Langue;

        internal ResultForm(int Id)
        {
            try
            {
                id = Id;
                InitializeComponent();
                SetCaption();
                SetTitle();
                SubscribeToEvent();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetTitle()
        {
            try
            {
                string query = SQLQueryBuilder.SelectQuery("Exploitation", "Nom", "where ID = '" + id + "'");
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                if (Commun.ListHasValue(list))
                {
                    this.Text = Translation.Translate("Result for farm :", Properties.Settings.Default.Langue) + " " + list[0];
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SubscribeToEvent()
        {
            try
            {
                MiscExpensesToolStripMenuItem.Click += ShowExpenseIncomeUserControl;
                MiscIncomesToolStripMenuItem.Click += ShowExpenseIncomeUserControl;
                FamilyExpensesToolStripMenuItem.Click += ShowExpenseIncomeUserControl;
                FamilyIncomesToolStripMenuItem.Click += ShowExpenseIncomeUserControl;
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetCaption()
        {
            try
            {
                MiscExpensesToolStripMenuItem.Text = Translation.Translate("Misc Expenses", langue);
                MiscIncomesToolStripMenuItem.Text = Translation.Translate("Misc Revenues", langue);
                FamilyExpensesToolStripMenuItem.Text = Translation.Translate("Family Expenses", langue);
                FamilyIncomesToolStripMenuItem.Text = Translation.Translate("Family Revenues", langue);
                productsToolStripMenuItem.Text = Translation.Translate("Product", langue);
                privatesToolStripMenuItem.Text = Translation.Translate("Privates", langue);
                rESULTToolStripMenuItem.Text = Translation.Translate("Results", langue);
                cOMPARAISONToolStripMenuItem.Text = Translation.Translate("Comparaison", langue);
                diversToolStripMenuItem.Text = Translation.Translate("Miscellaneous", langue);
                definitionToolStripMenuItem.Text = Translation.Translate("Definition", langue);
                productionToolStripMenuItem.Text = Translation.Translate("Production", langue);
                assolementToolStripMenuItem.Text = Translation.Translate("Cropping plan", langue);
                pérennesToolStripMenuItem.Text = Translation.Translate("Perennial", langue);
                pluriannuelToolStripMenuItem.Text = Translation.Translate("Pluriannual", langue);
                animauxToolStripMenuItem.Text = Translation.Translate("Animals", langue);
                opérationnellesToolStripMenuItem.Text = Translation.Translate("Operational", langue);
                structurellesToolStripMenuItem.Text = Translation.Translate("Structural", langue);
            }

            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
       
        private void definitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlDefinition defUsercontrol = new UserControlDefinition(id, this);
                panel1.Controls.Add(defUsercontrol);
                Size size = new Size(695,500);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
              catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void variablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlVariables userControlVariables = new UserControlVariables(this);
                Size size = new Size(1070, 469);
                panel1.Controls.Add(userControlVariables);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void assolementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlProduction userControlAssol = new UserControlProduction("Surface",this);
                panel1.Controls.Add(userControlAssol);
                Size size = new Size(1070, 480);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void productsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlProduct userControlProd = new UserControlProduct(this);
                panel1.Controls.Add(userControlProd);
                Size size = new Size(1000, 530);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ShowExpenseIncomeUserControl(object sender, EventArgs e)
        {
            try
            {

                int type = 0;
                if (sender.ToString() == Translation.Translate("Family Revenues", langue)) type = 1;
                if (sender.ToString() == Translation.Translate("Family Expenses", langue)) type = 2;
                if (sender.ToString() == Translation.Translate("Misc Expenses", langue)) type = 3;
                if (sender.ToString() == Translation.Translate("Misc Revenues", langue)) type = 4;


                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlExpenseIncome userControl = new UserControlExpenseIncome(type, id);
                panel1.Controls.Add(userControl);
                Size size = new Size(1070, 469);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void opérationnellesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlCharges userControl = new UserControlCharges(0, this);
                panel1.Controls.Add(userControl);
                Size size = new Size(1000, 480);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void structurellesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlCharges userControl = new UserControlCharges(1, this);
                panel1.Controls.Add(userControl);
                Size size = new Size(1000, 480);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void positivesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlExternalites userControl = new UserControlExternalites(1, this);
                panel1.Controls.Add(userControl);
                Size size = new Size(1070, 480);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void négativesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlExternalites userControl = new UserControlExternalites(0, this);
                panel1.Controls.Add(userControl);
                Size size = new Size(1070, 480);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void rESULTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlResultat userControl = new UserControlResultat(this);
                panel1.Controls.Add(userControl);
                Size size = new Size(1070, 515);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void animauxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlProduction userControlAssol = new UserControlProduction("Animals",this);
                panel1.Controls.Add(userControlAssol);
                Size size = new Size(1070, 469);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void cOMPARAISONToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlResultat userControl = new UserControlResultat(this);
                panel1.Controls.Add(userControl);
                Size size = new Size(1070, 515);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControl userControl = new UserControlStock(this);
                panel1.Controls.Add(userControl);
                Size size = new Size(1070, 515);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void globalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControl userControl = new UserControlImmoGlobal();
                panel1.Controls.Add(userControl);
                Size size = new Size(1000, 415);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void petitMatérielToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControl userControl = new UserControlImmoPetitMateriel(this);
                panel1.Controls.Add(userControl);
                Size size = new Size(600, 515);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
        }

        private void pérennesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlProduction userControlAssol = new UserControlProduction("Perenial",this);
                Size size = new Size(1070, 469);
                panel1.Controls.Add(userControlAssol);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void pluriannuelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlProduction userControlAssol = new UserControlProduction("Pluriannual",this);
                Size size = new Size(1070, 469);
                panel1.Controls.Add(userControlAssol);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void longTermeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlFinance userControlAssol = new UserControlFinance("Long Terme",this);
                Size size = new Size(1070, 469);
                panel1.Controls.Add(userControlAssol);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void courtTermeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlFinance userControlAssol = new UserControlFinance("Court Terme",this);
                Size size = new Size(1070, 469);
                panel1.Controls.Add(userControlAssol);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void placementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlFinance userControlAssol = new UserControlFinance("Placement",this);
                Size size = new Size(1070, 469);
                panel1.Controls.Add(userControlAssol);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void occToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlFinance userControlAssol = new UserControlFinance("Occ",this);
                Size size = new Size(750, 469);
                panel1.Controls.Add(userControlAssol);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void subventionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in panel1.Controls)
                {
                    panel1.Controls.Remove(control);
                }
                UserControlFinance userControlAssol = new UserControlFinance("Subvention",this);
                Size size = new Size(750, 469);
                panel1.Controls.Add(userControlAssol);
                this.Size = size;
                AdaptView();
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void ResultForm_Resize(object sender, EventArgs e)
        {
            AdaptView();
        }

        private void AdaptView()
        {
            foreach (Control control in panel1.Controls)
            {

                Size Size2 = this.Size;
                int height = Size2.Height - 70;
                int width = Size2.Width - 25;
                Size GoodSize = new Size(width, height);
                control.Size = GoodSize;
                foreach (Control control2 in control.Controls)
                {
                    if (control2.Name.Contains("panel1"))
                    {
                        control2.Size = GoodSize;
                    }
                }
                Properties.Settings.Default.DoitSauvegarger = true;
            }
        }

        private void ResultForm_Load(object sender, EventArgs e)
        {

        }
    }
}
