using OMEGA.Forms;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using OMEGA.SQLQuery;

namespace OMEGA.Data_Classes
{
    internal class ShowCommonFormResult
    {
        private TreeListForm treeForm;
        private CalculForm calculForm;
        private double Userdouble1 = -1;
        private string CurrentTable;
        private DataTable CurrentDataTable;
        private TextBox currentTextBox { get; set; }
        internal DataGridView DataGridViewCalcul { get; set; }
        private DataGridView CurrentDataGridView { get; set; }
        internal TreeView TreeViewDico { get; set; }
        internal string Operator { get; set; } = "";
        internal string mValueToReport { get; set; } = "";

        internal ShowCommonFormResult(TextBox txtbox, DataGridView dgv,string table,DataTable tbl  = null)
        {
            currentTextBox = txtbox;
            CurrentDataGridView = dgv;
            CurrentTable = table;
            CurrentDataTable = tbl;
            if (treeForm == null) treeForm = new TreeListForm(this);
            if(calculForm == null) calculForm = new CalculForm(this);
         
        }

        internal ShowCommonFormResult()
        {


        }

        internal void buttonDicoClick(object sender, EventArgs e)

        {
            try
            {
                if (treeForm.Visible == false)
                {
                    treeForm = new TreeListForm(this);
                    treeForm.Show();
                }
                else treeForm.BringToFront();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        internal void buttonCalculClick(object sender, EventArgs e)
        {
            try
            {
                if (calculForm.Visible == false)
                {
                    calculForm = new CalculForm(this);
                    calculForm.Show();
                }
                else calculForm.BringToFront();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        internal void dataGridViewCellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                currentTextBox.AppendText(DataGridViewCalcul.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() + " ");
                DataGridViewCalcul.CurrentCell = DataGridViewCalcul.Rows[e.RowIndex].Cells[e.ColumnIndex];
                Operator = DataGridViewCalcul.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        internal void treeViewNodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (currentTextBox != null)
                {
                    if (e.Node == getLastNode(e.Node)) currentTextBox.AppendText(e.Node.FullPath + Environment.NewLine);
                    else return;
                    FindValue(e.Node);
                }
                else if (CurrentDataGridView != null & CurrentDataTable != null)
                {
                    if (e.Node == getLastNode(e.Node))
                    {
                        string[] newValues = new string[3];
                        newValues[0] = e.Node.Text;
                        if (e.Node.Parent == null) return;
                        if (e.Node.Parent.Parent == null)
                        {
                            newValues[1] = "";
                            newValues[2] = e.Node.Parent.Text;
                        }
                        else
                        {
                            newValues[1] = e.Node.Parent.Text;
                            newValues[2] = e.Node.Parent.Parent.Text;
                        }
                        CurrentDataTable.Rows.Add(newValues);
                    }
                }
                else return;
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private TreeNode getLastNode(TreeNode subroot)
        {
            if (subroot.Nodes.Count == 0)
                return subroot;

            return getLastNode(subroot.Nodes[subroot.Nodes.Count - 1]);
        }

        internal void TextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                // the input is a numeric
                if (!char.IsControl(e.KeyChar) && char.IsDigit(e.KeyChar))
                {
                    e.Handled = false;
                }
                // the input is space or a return
                else if ((e.KeyChar == '\b') || (e.KeyChar == ' '))
                {
                    e.Handled = false;
                }
                // the input is an operator
                else if ((e.KeyChar == '+') || (e.KeyChar == '-') || (e.KeyChar == '/') || (e.KeyChar == '*'))
                {
                    e.Handled = false;
                    Operator = e.KeyChar.ToString();
                }
                // allow only on coma
                else if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') < 0))
                {
                    e.Handled = false;
                }
                else if (e.KeyChar == '\r')  // the input is enter
                {
                    e.Handled = false;
                    EnterClicked();

                }
                else // the input is not allowed
                {
                    e.Handled = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        internal void buttonReportClick(object sender, EventArgs e)
        {
            if (mValueToReport != "")
            {
                DataGridViewSelectedRowCollection ListuserRow = CurrentDataGridView.SelectedRows;
                // if (ListuserRow.Count == 1)
                {
                    foreach (DataGridViewColumn column in CurrentDataGridView.Columns)
                    {
                        if (column.Index > CurrentDataGridView.CurrentCell.ColumnIndex)
                        {
                            CurrentDataGridView.CurrentRow.Cells[column.Index].Value = mValueToReport;
                        }
                    }
                }
                //else MessageBox.Show("Merci de reselectionner la cellule à copier");
            }
        }

        internal void EnterClicked()
        {
            try
            {
                double tempdouble;
                string[] ArrTxtBox = SplitTextBox();
                if (ArrTxtBox.Length >= 2 && Operator != "" && Operator.Length == 1)
                {
                    if (double.TryParse(ArrTxtBox[0], out tempdouble) && Userdouble1 == 0)
                    {
                        Userdouble1 = tempdouble;
                    }
                    if (double.TryParse(ArrTxtBox[ArrTxtBox.Length - 1], out tempdouble))
                    {
                        Calcul(tempdouble);
                        Operator = "";
                        ResetTextBox();
                        if (CurrentDataGridView.CurrentCell.ColumnIndex > 1)
                        {
                            CurrentDataGridView.CurrentCell.Value = Userdouble1;
                            mValueToReport = CurrentDataGridView.CurrentCell.Value.ToString();
                        }
                    }
                }
                currentTextBox.SelectionStart = Math.Max(0, currentTextBox.Text.Length - 1);
                currentTextBox.SelectionLength = 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        internal void EnterClicked(double userdouble)
        {
            try
            {
                
                Calcul(userdouble);
                Operator = "";
                ResetTextBox();
                if (CurrentDataGridView.CurrentCell.ColumnIndex > 1)
                {
                    CurrentDataGridView.CurrentCell.Value = Userdouble1;
                    mValueToReport = CurrentDataGridView.CurrentCell.Value.ToString();
                }
                    
                currentTextBox.SelectionStart = Math.Max(0, currentTextBox.Text.Length);
                currentTextBox.SelectionLength = 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void FindValue(TreeNode node)
        {
            List<string> list = new List<string>();
            double Userdouble2 = -1;
            switch (CurrentTable)
            {
                case "Produit":
                    list = FindProduitValue(node);
                    break;
                case "Charge":
                    list = FindChargeValue(node);
                    break;
                case "EtatSortie":
                    break;
            }
            
            if (Commun.ListHasValue(list))
            {
                if (Userdouble1 == -1)
                    double.TryParse(list[0], out Userdouble1);
                else
                    double.TryParse(list[0], out Userdouble2);
                if (Userdouble1 != -1 && Userdouble2 != -1)
                {
                    EnterClicked(Userdouble2);
                }
            }
        }

        

        private void Calcul(double value)
        {
            if (Operator.Equals("+"))
            {
                Userdouble1 = Userdouble1 + value;
            }

            if (Operator.Equals("-"))
            {
                Userdouble1 = Userdouble1 - value;
            }

            if (Operator.Equals("*"))
            {
                Userdouble1 = Userdouble1 * value;
            }

            if (Operator.Equals("/"))
            {
                try
                {
                    Userdouble1 = Userdouble1 / value;
                }
                catch (ArithmeticException e)
                {
                    Userdouble1 = 0;
                }
            }

            if (Operator.Equals("EXP"))
            {
                Userdouble1 = Math.Exp(Userdouble1);
            }

            if (Operator.Equals("LOG"))
            {
                Userdouble1 = Math.Log(Userdouble1);
            }           
        }
        
        private void ResetTextBox()
        {
            currentTextBox.Text = "(" + currentTextBox.Text + ")";
        }

        private string[] SplitTextBox()
        {
            try
            {
                string textToSplit = "";
                //we don't look at everything that is between ( ) 
                int index = currentTextBox.Text.IndexOf(')');
                if (index > 0)
                {
                    textToSplit = currentTextBox.Text.Substring(index + 1);
                }
                else
                {
                    textToSplit = currentTextBox.Text;
                }

                string[] ArrTxtBox;
                char c = ' ';
                if ( Operator != "")
                {
                    c = Operator.First();
                    ArrTxtBox = textToSplit.Split(c);
                    return ArrTxtBox;
                }
                return null;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return null;
        }

        internal void LoadGrid()
        {

            DataGridViewCalcul.Columns.Add("calcul", "calcul");
            DataGridViewCalcul.Rows.Add(new object[] { "+" });//0
            DataGridViewCalcul.Rows.Add(new object[] { "-" });//1
            DataGridViewCalcul.Rows.Add(new object[] { "*" });//2
            DataGridViewCalcul.Rows.Add(new object[] { "/" });//3
            DataGridViewCalcul.Rows.Add(new object[] { "<" });//4
            DataGridViewCalcul.Rows.Add(new object[] { "<=" });//5
            DataGridViewCalcul.Rows.Add(new object[] { ">" });//6
            DataGridViewCalcul.Rows.Add(new object[] { ">=" });//7
            DataGridViewCalcul.Rows.Add(new object[] { "%" });//8
            DataGridViewCalcul.Rows.Add(new object[] { "#" });//9
            DataGridViewCalcul.Rows.Add(new object[] { " " });//10
            DataGridViewCalcul.Rows.Add(new object[] { "N" });//11
            DataGridViewCalcul.Rows.Add(new object[] { "An" });//12
            DataGridViewCalcul.Rows.Add(new object[] { " " });//13
            DataGridViewCalcul.Rows.Add(new object[] { "LOG" });//14
            DataGridViewCalcul.Rows.Add(new object[] { "EXP" });//15
            DataGridViewCalcul.Rows.Add(new object[] { " " });//16
            DataGridViewCalcul.Rows.Add(new object[] { "SI ()" });//17
            DataGridViewCalcul.Rows.Add(new object[] { "SINON SI ()" });//18
            DataGridViewCalcul.Rows.Add(new object[] { "SINON" });//19
            DataGridViewCalcul.Rows.Add(new object[] { "ALORS" });//20
            DataGridViewCalcul.Rows.Add(new object[] { " " });//21
            DataGridViewCalcul.Rows.Add(new object[] { "ET" });//22
            DataGridViewCalcul.Rows.Add(new object[] { "OU" });//23
            DataGridViewCalcul.Rows.Add(new object[] { "()" });//24

            DataGridViewCalcul.RowHeadersVisible = false;
            DataGridViewCalcul.ColumnHeadersVisible = false;
            DataGridViewCalcul.BorderStyle = BorderStyle.None;
            DataGridViewCalcul.RowTemplate.Height = 10;

        }

        internal void LoadFirstNodeTreeView()
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;
                TreeViewDico.Nodes.Add(Translation.Translate("Item", langue));//0
                TreeViewDico.Nodes.Add(Translation.Translate("Balance", langue));//1
                TreeViewDico.Nodes.Add(Translation.Translate("Farm account", langue));//2
                TreeViewDico.Nodes.Add(Translation.Translate("Incomes Products", langue));//3
                TreeViewDico.Nodes.Add(Translation.Translate("Expenses Supplies", langue));//4
                TreeViewDico.Nodes.Add(Translation.Translate("Fix Costs", langue));//5
                TreeViewDico.Nodes.Add(Translation.Translate("Misc Expenses", langue));//6
                TreeViewDico.Nodes.Add(Translation.Translate("Misc Incomes", langue));//7
                TreeViewDico.Nodes.Add(Translation.Translate("Family Expenses", langue));//8
                TreeViewDico.Nodes.Add(Translation.Translate("Family Incomes", langue));//9
                TreeViewDico.Nodes.Add(Translation.Translate("Variables", langue));//10
                TreeViewDico.Nodes.Add(Translation.Translate("Indicators", langue));//11
                TreeViewDico.Nodes.Add(Translation.Translate("Product Quantity", langue));//12-
                TreeViewDico.Nodes.Add(Translation.Translate("Cost Quantity", langue));//13-
                TreeViewDico.Nodes.Add(Translation.Translate("Product Price", langue));//14-
                TreeViewDico.Nodes.Add(Translation.Translate("Cost Price", langue));//15-
                TreeViewDico.Nodes.Add(Translation.Translate("Fixed Assets", langue));//16
                TreeViewDico.Nodes.Add(Translation.Translate("Loan", langue));//17
                TreeViewDico.Nodes.Add(Translation.Translate("LT Loan", langue));//18
                TreeViewDico.Nodes.Add(Translation.Translate("ST Loan", langue));//19
                TreeViewDico.Nodes.Add(Translation.Translate("Private Loan", langue));//20
                TreeViewDico.Nodes.Add(Translation.Translate("Subsidy", langue));//21
                TreeViewDico.Nodes.Add(Translation.Translate("Vat", langue));//22
                TreeViewDico.Nodes.Add(Translation.Translate("Crop area", langue));//23
                TreeViewDico.Nodes.Add(Translation.Translate("Tree crop Area", langue));//24
                TreeViewDico.Nodes.Add(Translation.Translate("List of Animals", langue));//25
                TreeViewDico.Nodes.Add(Translation.Translate("Calendar", langue));//26
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
           
        }

        internal void LoadSecondNodepProd_Char()
        {
            try
            {
                string query = "Select distinct Def_Categ.Nom from Def_Categ " +
              " Join Produits on Def_Categ.IdDefCateg = produits.IdDefCateg";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);

                foreach (string item in list)
                {
                    TreeViewDico.Nodes[12].Nodes.Add(item);
                    TreeViewDico.Nodes[14].Nodes.Add(item);
                }

                query = "Select distinct Def_Categ.Nom from Def_Categ" +
                " Join Charges on Def_Categ.IdDefCateg = Charges.IdDefCateg";
                list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);

                foreach (string item in list)
                {
                    TreeViewDico.Nodes[13].Nodes.Add(item);
                    TreeViewDico.Nodes[15].Nodes.Add(item);
                }

                List<string> ListNode = new List<string>();
                foreach (TreeNode item in TreeViewDico.Nodes[12].Nodes)
                {
                    ListNode.Add(item.Text);
                }
                foreach (string item in ListNode)
                {
                    query = "Select Produits.Nom from Produits " +
                      " Join Def_Categ on Def_Categ.IdDefCateg = produits.IdDefCateg " +
                     " Join Agri_Produits on Agri_Produits.IdProduits = produits.Id" +
                      " Where Def_Categ.Nom ='" + item+"';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    foreach (string item2 in list)
                    {
                        int index = GetNodeIndex(item, 12);
                        TreeViewDico.Nodes[12].Nodes[index].Nodes.Add(item2);
                    }
                }

                ListNode.Clear();
                foreach (TreeNode item in TreeViewDico.Nodes[13].Nodes)
                {
                    ListNode.Add(item.Text);
                }
                foreach (string item in ListNode)
                {
                    query = "Select Charges.Nom from Charges " +
                     " Join Def_Categ on Def_Categ.IdDefCateg = Charges.IdDefCateg " +
                     " Join Agri_Charges on Agri_Charges.IdCharges = Charges.Id" +
                     " Where Def_Categ.Nom ='" + item + "';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    foreach (string item2 in list)
                    {
                        int index = GetNodeIndex(item, 13);
                        TreeViewDico.Nodes[13].Nodes[index].Nodes.Add(item2);
                    }
                }

                ListNode.Clear();
                foreach (TreeNode item in TreeViewDico.Nodes[14].Nodes)
                {
                    ListNode.Add(item.Text);
                }
                foreach (string item in ListNode)
                {
                    query = "Select Produits.Nom from Produits " +
                      " Join Def_Categ on Def_Categ.IdDefCateg = produits.IdDefCateg " +
                      "Where Def_Categ.Nom ='" + item + "';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    foreach (string item2 in list)
                    {
                        int index = GetNodeIndex(item, 14);
                        TreeViewDico.Nodes[14].Nodes[index].Nodes.Add(item2);
                    }
                }

                ListNode.Clear();
                foreach (TreeNode item in TreeViewDico.Nodes[15].Nodes)
                {
                    ListNode.Add(item.Text);
                }
                foreach (string item in ListNode)
                {
                    query = "Select Charges.Nom from Charges " +
                      " Join Def_Categ on Def_Categ.IdDefCateg = Charges.IdDefCateg " +
                       "Where Def_Categ.Nom ='" + item + "';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    foreach (string item2 in list)
                    {
                        int index = GetNodeIndex(item,15);
                        TreeViewDico.Nodes[15].Nodes[index].Nodes.Add(item2);
                    }
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        internal void LoadSecondNodeImmo()
        {
            string query = "Select Name From Item_ImmoGlobale Where ID = '2' OR ID = '1' OR  ID = '3' OR ID = '6';";
            List<string> list = SQlQueryExecuter.RunQueryReaderStr("Name", query);
            foreach (string item in list)
            {
                TreeViewDico.Nodes[16].Nodes.Add(item);
            }
        }

        internal void LoadSecondNodePeriode()
        {
            string query = "Select distinct Nom From Def_Calendrier;";
            List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
            foreach (string item in list)
            {
                TreeViewDico.Nodes[26].Nodes.Add(item);
            }
        }

        internal void LoadSecondNodeTVA()
        {
            string query = "Select Nom From TVA;";
            List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
            foreach (string item in list)
            {
                TreeViewDico.Nodes[22].Nodes.Add(item);
            }
        }

        internal void LoadSecondNodeIncomeProduct()
        {
            try
            {
                List<string> ListNode = new List<string>();
                string query = "Select distinct Def_Categ.Nom From Agri_Produits" +
                    " Join Produits on Produits.Id = Agri_Produits.IdProduits" +
                    " Join Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg" +
                    " Where Def_Categ.IdDefinitions = '1' and Agri_Produits.IdExploitations = '" + Commun.GetIdExpl() + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string item in list)
                {
                    TreeViewDico.Nodes[3].Nodes.Add(item);
                }
                foreach (TreeNode item in TreeViewDico.Nodes[3].Nodes)
                {
                    ListNode.Add(item.Text);
                }
                foreach (string item in ListNode)
                {
                    query = "Select Produits.Nom from Produits" +
                       " Join Agri_Produits on Produits.Id = Agri_Produits.IdProduits" +
                       " Join Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg " +
                       " Where Def_Categ.Nom ='" + item + "' AND Def_Categ.IdDefinitions = '1'" +
                       " AND Agri_Produits.IdExploitations = '" + Commun.GetIdExpl() + "'; ";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    foreach (string item2 in list)
                    {
                        int index = GetNodeIndex(item, 3);
                        TreeViewDico.Nodes[3].Nodes[index].Nodes.Add(item2);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void LoadSecondNodeSurfCult()
        {
            try
            {
                List<string> ListNode = new List<string>();
                string query = "Select distinct Def_Categ.Nom From Agri_Assol" +
                    " Join Caract_Activite on Agri_Assol.IdActivite = Caract_Activite.IdActivite" +
                    " Join Produits on Produits.Id = Caract_Activite.IdProduits" +
                    " Join Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg" +
                    " Where Def_Categ.IdDefinitions = '1' and Agri_Assol.IdExploitations = '" + Commun.GetIdExpl() + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string item in list)
                {
                    TreeViewDico.Nodes[23].Nodes.Add(item);
                }
                foreach (TreeNode item in TreeViewDico.Nodes[23].Nodes)
                {
                    ListNode.Add(item.Text);
                }
                foreach (string item in ListNode)
                {
                    query = "Select distinct Activite.Nom From Agri_Assol" +
                     " Join Activite on Activite.Id = Agri_Assol.IdActivite" +
                     " Join Caract_Activite on Agri_Assol.IdActivite = Caract_Activite.IdActivite" +
                     " Join Produits on Produits.Id = Caract_Activite.IdProduits" +
                     " Join Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg" +
                     " Where Def_Categ.IdDefinitions = '1' and Agri_Assol.IdExploitations = '" + Commun.GetIdExpl() + "';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    foreach (string item2 in list)
                    {
                        int index = GetNodeIndex(item, 23);
                        if (IsItemInCategorie(item, item2,"Produits"))
                        {
                            TreeViewDico.Nodes[23].Nodes[index].Nodes.Add(item2);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private bool IsItemInCategorie(string categorie, string element,string table)
        {
            int idDefCateg = SQLQueryBuilder.FindId("Def_Categ", "IdDefCateg", "Nom", categorie);

            string query = "Select IdDefCateg From " + table + " Where Nom ='" + element + "';";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("IdDefCateg", query);

            if (Commun.ListHasValue(list))
            {
                if (idDefCateg == list[0]) return true;
                else return true;
            }
            else return true;
        }

        internal void LoadSecondNodeCost()
        {
            try
            {
                List<string> ListNode = new List<string>();
                string query = "Select distinct Def_Categ.Nom From Agri_Charges" +
                    " Join Charges on Charges.Id = Agri_Charges.IdCharges" +
                    " Join Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg" +
                    " Where Def_Categ.IdDefinitions = '2' and Agri_Charges.IdExploitations = '" + Commun.GetIdExpl() + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string item in list)
                {
                    TreeViewDico.Nodes[4].Nodes.Add(item);
                }
                foreach (TreeNode item in TreeViewDico.Nodes[4].Nodes)
                {
                    ListNode.Add(item.Text);
                }
                foreach (string item in ListNode)
                {
                    query = "Select Charges.Nom from Charges" +
                       " Join Agri_Charges on Charges.Id = Agri_Charges.IdCharges" +
                       " Join Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg " +
                       " Where Def_Categ.Nom ='" + item + "' AND Def_Categ.IdDefinitions = '2'" +
                       " AND Agri_Charges.IdExploitations = '" + Commun.GetIdExpl() + "'; ";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    foreach (string item2 in list)
                    {
                        int index = GetNodeIndex(item, 4);
                        if (IsItemInCategorie(item, item2, "Charges"))
                        {
                            TreeViewDico.Nodes[4].Nodes[index].Nodes.Add(item2);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void LoadSecondeNodeExpInc()
        {
            try
            {
                string query = "Select * from Expense_Income,Result_Calcul" +
                " Where Family = '1' and Expense = '1'" +
                " and table_Origine = 'Expense_Income'" +
                " and IdExploitations  = '" + Commun.GetIdExpl() + "'" +
                " and Result_Calcul.Nom = Expense_Income.Nom  ;";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string item in list)
                {
                    TreeViewDico.Nodes[8].Nodes.Add(item);
                }
                query = "Select * from Expense_Income,Result_Calcul" +
                   " Where Family = '0' and Expense = '1'" +
                   " and table_Origine = 'Expense_Income'" +
                   " and IdExploitations  = '" + Commun.GetIdExpl() + "'" +
                   " and Result_Calcul.Nom = Expense_Income.Nom  ;";
                list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string item in list)
                {
                    TreeViewDico.Nodes[6].Nodes.Add(item);
                }
                query = "Select * from Expense_Income,Result_Calcul" +
                   " Where Family = '1' and Expense = '0'" +
                   " and table_Origine = 'Expense_Income'" +
                   " and IdExploitations  = '" + Commun.GetIdExpl() + "'" +
                   " and Result_Calcul.Nom = Expense_Income.Nom  ;";
                list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string item in list)
                {
                    TreeViewDico.Nodes[9].Nodes.Add(item);
                }
                query = "Select * from Expense_Income,Result_Calcul" +
                  " Where Family = '0' and Expense = '0'" +
                  " and table_Origine = 'Expense_Income'" +
                  " and IdExploitations  = '" + Commun.GetIdExpl() + "'" +
                  " and Result_Calcul.Nom = Expense_Income.Nom  ;";
                list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string item in list)
                {
                    TreeViewDico.Nodes[7].Nodes.Add(item);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        
        internal void LoadSecondeNodeVariable()
        {
            try
            {
                List<string> ListNode = new List<string>();
                string query = "Select distinct Categorie From Variable;";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Categorie", query);
                foreach (string item in list)
                {
                    TreeViewDico.Nodes[10].Nodes.Add(item);
                }

                foreach (TreeNode item in TreeViewDico.Nodes[10].Nodes)
                {
                    ListNode.Add(item.Text);
                }
                foreach (string item in ListNode)
                {
                    query = "Select Nom from Variable " +
                       "Where Categorie ='" + item + "';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    foreach (string item2 in list)
                    {
                        int index = GetNodeIndex(item, 10);
                        TreeViewDico.Nodes[10].Nodes[index].Nodes.Add(item2);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        internal void LoadSecondeNodeAnimals()
        {

            string query = "Select * from Activite Where Idtype = '5';";//5 = animals
            List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
            foreach (string item in list)
            {
                TreeViewDico.Nodes[25].Nodes.Add(item);
            }
            List<string> ListNode = new List<string>();
            foreach (TreeNode item in TreeViewDico.Nodes[25].Nodes)
            {
                ListNode.Add(item.Text);
            }
            foreach (string item in ListNode)
            {
                query = "Select Produits.Nom from Produits" +
                    " Join Caract_Activite on Produits.Id = Caract_Activite.IdProduits " +
                    " Join Activite on Caract_Activite.IdActivite = Activite.ID " +
                   " Where Activite.Nom ='" + item + "';";
                list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string item2 in list)
                {
                    int index = GetNodeIndex(item, 25);
                    TreeViewDico.Nodes[25].Nodes[index].Nodes.Add(item2);
                }
            }

        }

        private bool IsParentNode(TreeNode subroot)
        {
            if (subroot.Parent == null)
                return true;
            else
                return false;
        }

        private int GetNodeIndex(string item,int NumNode)
        {
            int index = 0;
           
            TreeNode myTreeNode = new TreeNode();
            foreach (TreeNode node in TreeViewDico.Nodes[NumNode].Nodes)
            {
                if (node.Text == item)
                {
                    myTreeNode = node;
                    break;
                }
            }
            index = TreeViewDico.Nodes[NumNode].Nodes.IndexOf(myTreeNode);
            
            return index;
        }

        private List<string> FindProduitValue(TreeNode node)
        {
            int langue = Properties.Settings.Default.Langue;
            List<string> list = new List<string>();
            try
            {
                if (node.FullPath.Contains(Translation.Translate("Product Quantity", langue)))
                {
                    string query = "Select Quantite_1 From Produits" +
                        " Join Prod_Quantite on Produits.ID = Prod_Quantite.IdProduits" +
                        " Where Produits.Nom = '" + node.Text + "';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Quantite_1", query);
                }
                if (node.FullPath.Contains(Translation.Translate("Product Price", langue)))
                {
                    string query = "Select Prix From Produits Where Nom = '" + node.Text + "';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Prix", query);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

            return list;
        }

        private List<string> FindChargeValue(TreeNode node)
        {
            int langue = Properties.Settings.Default.Langue;
            List<string> list = new List<string>();
            try
            {
                if (node.FullPath.Contains(Translation.Translate("Cost Quantity", langue)))
                {
                    string query = "Select Quantite_1 From Charges" +
                        "Join Charge_Quantite on Charges.ID = Charge_Quantite.IdCharges" +
                        " Where Charges.Nom = '" + node.Text + "';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Quantite_1", query);
                }
                if (node.FullPath.Contains(Translation.Translate("Cost Price", langue)))
                {
                    string query = "Select Prix From Charges Where Nom = '" + node.Text + "';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Prix", query);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

            return list;
        }

    }
}
