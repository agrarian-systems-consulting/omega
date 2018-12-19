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

namespace OMEGA.Forms
{
    /// <summary>
    /// represents the Form that holds the list f the operators  to run calculations
    /// </summary>
    internal partial class CalculForm : Form
    {
        /// <summary>
        /// Load the form that show all the operators 
        /// </summary>
        /// <param name="showCommonForm"></param>
        internal CalculForm(ShowCommonFormResult showCommonForm)
        {
            InitializeComponent();
            showCommonForm.DataGridViewCalcul = dataGridView1;

            showCommonForm.LoadGrid();

            dataGridView1.CellClick += new DataGridViewCellEventHandler(showCommonForm.dataGridViewCellClick);
        }

        
    }
}
