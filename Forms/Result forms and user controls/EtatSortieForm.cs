using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Forms.Result_forms_and_user_controls
{
    public partial class EtatSortieForm : Form
    {
        int langue = Properties.Settings.Default.Langue;

        public EtatSortieForm()
        {
            InitializeComponent();
            LoadFirstNode();
            LoadSecondNode();
        }

        private void LoadFirstNode()
        {
            try
            {
                string query = "Select distinct Nom From Def_EtatSortie;";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);

                foreach (string item in list)
                {
                    treeView1.Nodes.Add(item);
                }
                treeView1.Nodes.Add("[...]");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadSecondNode()
        {
            try
            {
                string query = "Select Nom,IdDefEtatSortie From EtatSortie;";
                List<int> listidES = SQlQueryExecuter.RunQueryReaderInt("IdDefEtatSortie", query);
                List<string> listnom = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                int index1stNode = 0;

                foreach (string nom in listnom)
                {
                    index1stNode = 0;
                    foreach (TreeNode node in treeView1.Nodes)
                    {
                        if (node.Text == GetNomDefEtatSortieFromId(listidES[listnom.IndexOf(nom)]))
                        {
                            treeView1.Nodes[index1stNode].Nodes.Add(nom);
                        }
                        index1stNode++;
                    }
                }

                index1stNode = 0;
                foreach (TreeNode node in treeView1.Nodes)
                {
                    if (node.Text != "[...]")
                    {
                        treeView1.Nodes[index1stNode].Nodes.Add("[...]");
                        index1stNode++;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private string GetNomDefEtatSortieFromId(int Id)
        {
            try
            {
                string query = "Select Nom From Def_EtatSortie Where ID ='" + Id + "';";
                return SQlQueryExecuter.RunQueryReaderStr("Nom", query)[0];
            }

             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                if (e.Node.Text == "[...]")
                {
                    string inputmsg = "";
                    if (IsParentNode(e.Node))
                    {
                        if (Commun.InputBox("Add Item", "Add new Categorie", ref inputmsg) == DialogResult.OK)
                        {
                            if (Commun.NameExists(inputmsg, "Def_EtatSortie", "Nom"))
                            {
                                DialogResult dialogResult = MessageBox.Show(Translation.Translate("An item with this name already exists. It may generate some errors on reports or calculs. Do you want to continue ?",langue), "Warning", MessageBoxButtons.YesNo);
                                if (dialogResult == DialogResult.No)
                                {
                                    return;
                                }
                            }
                            treeView1.Nodes.Add(inputmsg);
                            string query = "Insert into Def_EtatSortie (Nom) VALUES ('" + inputmsg + "');";
                            SQlQueryExecuter.RunQuery(query);
                            AddNodeAdd(inputmsg);
                        }
                    }
                    else
                    {
                        if (Commun.InputBox("Add Item", "Add new rapport for the categorie " + e.Node.Parent.Text, ref inputmsg) == DialogResult.OK)
                        {
                            int id = SQLQueryBuilder.FindId("Def_EtatSortie", "ID", "Nom", e.Node.Parent.Text);
                            string query = "Insert into EtatSortie (IdDefEtatSortie,Nom) VALUES ('" + id + "','" + inputmsg + "');";
                            SQlQueryExecuter.RunQuery(query);
                            AddNodeUser(e.Node.Parent.Text, inputmsg);
                        }
                    }
                }
                else
                {
                    if (!IsParentNode(e.Node))
                    {
                        StandardForm form = new StandardForm(0, "EtatSortie",e.Node.Text,null,e.Node.Parent.Text);
                        form.ShowDialog();
                    }
                }
            }
           
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void AddNodeUser(string nodetext,string newvalue)
        {
            try
            {
                treeView1.Nodes[GetIdNode(nodetext)].Nodes.RemoveAt(GetId2Node("[...]"));
                treeView1.Nodes[GetIdNode(nodetext)].Nodes.Add(newvalue);
                AddNodeAdd(nodetext);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void AddNodeAdd(string nodetext)
        {
            treeView1.Nodes[GetIdNode(nodetext)].Nodes.Add("[...]");
        }

        private int GetIdNode(string text)
        {
            int i = 0;
            foreach (TreeNode node in treeView1.Nodes)
            {
                if (node.Text == text)
                {
                    return i;
                }
                i++;
            }
            return 0;
        }

        private int GetId2Node(string text)
        {
            int i = 0;
            foreach (TreeNode node in treeView1.Nodes[GetIdNode(text)].Nodes)
            {
                if (node.Text == text)
                {
                    return i;
                }
                i++;
            }
            return 0;
        }

        private bool IsParentNode(TreeNode subroot)
        {
            if (subroot.Parent == null)
                return true;
            else
                return false;
        }

        private TreeNode getLastNode(TreeNode subroot)
        {
            if (subroot.Nodes.Count == 0)
                return subroot;

            return getLastNode(subroot.Nodes[subroot.Nodes.Count - 1]);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string inputmsg = "";
                if (Commun.InputBox("Add Item", "Add new Categorie", ref inputmsg) == DialogResult.OK)
                {
                    if (Commun.NameExists(inputmsg, "Def_EtatSortie", "Nom"))
                    {
                        DialogResult dialogResult = MessageBox.Show(Translation.Translate("An item with this name already exists. It may generate some errors on reports or calculs. Do you want to continue ?",langue), "Warning", MessageBoxButtons.YesNo);
                        if (dialogResult == DialogResult.No)
                        {
                            return;
                        }
                    }
                    treeView1.Nodes.Add(inputmsg);
                    string query = "Insert into Def_EtatSortie (Nom) VALUES ('" + inputmsg + "');";
                    SQlQueryExecuter.RunQuery(query);
                    AddNodeAdd(inputmsg);
                }
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonModify_Click(object sender, EventArgs e)
        {
            try
            {
                int index1stnode = 0;
         
                string textToChange = "";
                foreach (TreeNode node in treeView1.Nodes)
                {
                    if (treeView1.Nodes[index1stnode].IsSelected)
                    {
                        textToChange = node.Text;
                        if (Commun.InputBox("Modify Item", "New Value :", ref textToChange) == DialogResult.OK)
                        {
                            int id =  SQLQueryBuilder.FindId("Def_EtatSortie", "ID", "Nom", node.Text);
                            string query = "Update Def_EtatSortie Set Nom ='" + textToChange + "' Where ID = '" + id + "';";
                            SQlQueryExecuter.RunQuery(query);
                            node.Text = textToChange;
                            return;
                        }
                    }
                    index1stnode++;
                }

                index1stnode = 0;
                int index2ndnode = 0;
                foreach (TreeNode node in treeView1.Nodes)
                {
                    index2ndnode = 0;
                    foreach (TreeNode node2 in node.Nodes)
                    {
                        if (treeView1.Nodes[index1stnode].Nodes[index2ndnode].IsSelected)
                        {
                            textToChange = node2.Text;
                            if (Commun.InputBox("Modify Item", "New Value :", ref textToChange) == DialogResult.OK)
                            {
                                int id = SQLQueryBuilder.FindId("EtatSortie", "ID", "Nom", node2.Text);
                                string query = "Update EtatSortie Set Nom ='" + textToChange + "' Where ID = '" + id + "';";
                                SQlQueryExecuter.RunQuery(query);
                                node2.Text = textToChange;
                                return;
                            }
                        }
                        index2ndnode++;
                    }
                    index1stnode++;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                int index1stnode = 0;

                foreach (TreeNode node in treeView1.Nodes)
                {
                    if (treeView1.Nodes[index1stnode].IsSelected)
                    {
                        int id = SQLQueryBuilder.FindId("Def_EtatSortie", "ID", "Nom", node.Text);
                        string query = "Delete From Def_EtatSortie Where ID = '" + id + "';";
                        SQlQueryExecuter.RunQuery(query);
                        break; 
                    }
                    index1stnode++;
                }
                treeView1.Nodes.RemoveAt(index1stnode);

                index1stnode = 0;
                int index2ndnode = 0;
                foreach (TreeNode node in treeView1.Nodes)
                {
                    index2ndnode = 0;
                    foreach (TreeNode node2 in node.Nodes)
                    {
                        if (treeView1.Nodes[index1stnode].Nodes[index2ndnode].IsSelected)
                        {
                            int id = SQLQueryBuilder.FindId("EtatSortie", "ID", "Nom", node2.Text);
                            string query = "Delete From EtatSortie Where ID = '" + id + "';";
                            SQlQueryExecuter.RunQuery(query);
                            break;
                        }
                        index2ndnode++;
                    }
                    index1stnode++;
                }
                treeView1.Nodes[index1stnode].Nodes.RemoveAt(index2ndnode);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}
