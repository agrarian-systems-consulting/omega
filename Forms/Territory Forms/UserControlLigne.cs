using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Forms.Territory_Forms
{
    public partial class UserControlLigne : UserControl
    {
        internal EventHandler<DeleteLineEvent> RowDeleting;
        internal int Index;
        internal FicheAttributForm MainForm;

        public UserControlLigne(int index,FicheAttributForm form)
        {
            InitializeComponent();
            Index = index;
            MainForm = form;
        }

        public UserControlLigne()
        {
            InitializeComponent();
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                RowDeleting = new EventHandler<DeleteLineEvent>(MainForm.ButtonDelete_Click);
                RowDeleting.Invoke(this, new DeleteLineEvent(Index));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }


    }
}
