using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Forms.Main_Forms
{
    /// <summary>
    /// Classe non utilisée
    /// </summary>
    public partial class VarianteForm : Form
    {
        private string ExplName;
        private int level;

        public VarianteForm(string nom)
        {
            InitializeComponent();
            ExplName = nom;
            LoadTreeView();
        }

        private void LoadTreeView()
        {
            treeView1.Nodes.Add(ExplName);
            treeView1.Nodes[0].Nodes.Add("[...]");
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

            try
            {
                if (e.Node.Text == "[...]")
                {
                    level = 1;
                    level = getParentNode(e.Node);
                }
            }
            catch
            {

            }
        }

        private TreeNode getLastNode(TreeNode subroot)
        {
            if (subroot.Nodes.Count == 0)
                return subroot;

            return getLastNode(subroot.Nodes[subroot.Nodes.Count - 1]);
        }

        private int getParentNode(TreeNode subroot)
        {
            if (subroot.Parent.Text == ExplName)
                return level;
            else
            {
                level++;
                getParentNode(subroot.Parent);
            }
                
            return 0;
        }


    }
}
