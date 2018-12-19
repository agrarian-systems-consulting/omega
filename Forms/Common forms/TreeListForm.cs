using OMEGA.Data_Classes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    internal partial class TreeListForm : Form
    {
        internal TreeListForm(ShowCommonFormResult showCommonForm)
        {
            InitializeComponent();
            showCommonForm.TreeViewDico = treeView1;
            showCommonForm.LoadFirstNodeTreeView();
            showCommonForm.LoadSecondNodepProd_Char();
            showCommonForm.LoadSecondNodeImmo();
            showCommonForm.LoadSecondeNodeVariable();
            showCommonForm.LoadSecondeNodeAnimals();
            showCommonForm.LoadSecondeNodeExpInc();
            showCommonForm.LoadSecondNodeTVA();
            showCommonForm.LoadSecondNodeIncomeProduct();
            showCommonForm.LoadSecondNodeCost();
            showCommonForm.LoadSecondNodeSurfCult();
            showCommonForm.LoadSecondNodePeriode();

            treeView1.NodeMouseClick += new TreeNodeMouseClickEventHandler (showCommonForm.treeViewNodeMouseClick);
           
        }

    }
}
