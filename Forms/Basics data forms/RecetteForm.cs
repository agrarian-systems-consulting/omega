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
    internal partial class RecetteForm : Form
    {

        private ExpenseIncome expenseIncome;

        /// <summary>
        /// When the form is loaded, we pass the parameters to say if its an expense or a income
        /// for the family or for the farm
        /// ExpenseOrIncome : 1 => income - ExpenseOrIncome : 0 => Expense
        /// familyOrEntreprises : 1 => family - familyOrEntreprises : 0 => Entreprise
        /// </summary>
        /// <param name="ExpenseOrIncome"></param>
        /// <param name="familyOrEntreprises"></param>
        internal RecetteForm(int ExpenseOrIncome,int familyOrEntreprises)
        {
            InitializeComponent();
            dataGridViewRecDep.Font = Commun.GetCurrentFont();
            expenseIncome = new ExpenseIncome(dataGridViewRecDep,buttonOk,buttonCancel,
            buttonDuplicate,buttonRemove,buttonSave,buttonAdd,
            ExpenseOrIncome, familyOrEntreprises,pictureBoxExport,this);

            SubscribeToEvent();
        }

        private void SubscribeToEvent()
        {

            buttonAdd.Click += expenseIncome.ButtonAddClick;
            buttonCancel.Click += expenseIncome.ButtonCancelClick;
            buttonDuplicate.Click += expenseIncome.ButtonDuplicateClick;
            buttonRemove.Click += expenseIncome.ButtonRemoveClick;
            pictureBoxExport.Click += expenseIncome.ButtonExportClick;
            pictureBoxExport.MouseEnter += expenseIncome.pictureBox_MouseEnter;
            pictureBoxExport.MouseLeave += expenseIncome.pictureBox_MouseLeave;
            buttonOk.Click += expenseIncome.ButtonOkClick;
            buttonSave.Click += expenseIncome.ButtonSaveClick;
            dataGridViewRecDep.CellEndEdit += expenseIncome.dataGridViewCellClick;
            dataGridViewRecDep.EditingControlShowing += expenseIncome.DataGridView1EditingControlShowing;
            buttonNotes.Click += expenseIncome.buttonNotes_Click;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
