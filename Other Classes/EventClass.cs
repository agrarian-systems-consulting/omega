using OMEGA.Data_Classes;
using OMEGA.Forms;
using System;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace OMEGA
{
    class EventClass
    {
    }

    /// <summary>
    /// Represents The event that occurs when the user do a right click
    /// </summary>
    internal class MenuContextClickEvent : EventArgs
    {
        /// <summary>
        /// The column index where the right clicked occured
        /// </summary>
        internal int columnindex { get; set; }
        /// <summary>
        /// The Context Menu to show to the user
        /// </summary>
        internal ContextMenu contextmenu { get; set; }
        /// <summary>
        /// Load the Context Menu
        /// </summary>
        /// <param name="index"></param>
        /// <param name="data"></param>
        internal MenuContextClickEvent(int index, ContextMenu data)
        {
            columnindex = index;
            contextmenu = data;
        }

    }

    internal class SaveDataEvent : EventArgs
    {
        internal bool deleteOnTable { get; set; }
        internal string table { get; set; }
        internal SaveDataEvent(bool delete, string tbl)
        {
            table = tbl;
            deleteOnTable = delete;
        }

    }

    internal class DeleteLineEvent : EventArgs
    {
        internal int Index { get; set; }

        internal DeleteLineEvent(int index)
        {
            Index = index;
        }
    }

    internal class AddRowEvent : EventArgs
    {
        internal CurrencyManager currencymanager { get; set; }
        internal AddRowEvent(CurrencyManager currency)
        {
            currencymanager = currency;
        }
    }

    /// <summary>
    /// Event that handle the user clicking on a List button.
    /// </summary>
    internal class ShowListEvent : EventArgs
    {
        /// <summary>
        /// Represents the activity where the user was when he clicked on the list button
        /// </summary>
        internal ActivityMainForm activity { get; set; }
        /// <summary>
        /// Represents the exploitation where the user was when he clicked on the list button
        /// </summary>
        internal ExploitationMainForm exploitation { get; set; }
        /// <summary>
        /// Represents the query to run to fill the list
        /// </summary>
        internal string query { get; set; }
        /// <summary>
        /// Represents the table where ???
        /// </summary>
        internal string table { get; set; }
        /// <summary>
        /// Represents the currentTab to add any item on the list
        /// </summary>
        internal string currentTab { get; set; }
        /// <summary>
        /// Represents the gridview to add any item on the list
        /// </summary>
        internal DataGridView currentGridView { get; set; }

        internal DataTable currentDataTable { get; set; }

        internal Produit Produit { get; set; }

        internal Charge Charge { get; set; }

        internal int ID { get; set; }

        /// <summary>
        /// This custom event allow the user to add products, charges or externalities to the current tab of an activity or a working farm.
        /// the parameters are : tbl = name of the table of the element, tab = the current tab opened, activitymainform so we know its added to an Activity
        /// Ewploitation so we know it's added to an explotation, dvg = datagridview to add the item selected
        /// </summary>
        /// <param name="tbl"></param>
        /// <param name="qry"></param>
        /// <param name="tab"></param>
        /// <param name="act"></param>
        /// <param name="explo"></param>
        /// <param name="dvg"></param>
        internal ShowListEvent(string tbl, string qry,string tab, ActivityMainForm act = null,
            ExploitationMainForm explo = null, DataGridView dgv = null,DataTable dataTable = null,
            Produit prod=null, Charge chge =null,int Id = 0)
        {
            table = tbl ;
            query = qry;
            currentTab = tab ;
            activity = act;
            exploitation = explo;
            currentGridView = dgv;
            currentDataTable = dataTable;
            Produit = prod;
            Charge = chge;
            ID = Id;
        }
    }

    /// <summary>
    /// Represents the event that occurs when the user click on Showing control.
    /// </summary>
    internal class ClickOnShowingControlEvent : EventArgs
    {
       internal  ClickOnShowingControlEvent()
       {

       }

    }


    class MyCheckedListBox : CheckedListBox
    {
      
        private const int SB_VERT = 1;

        // ScrollBar interfaces
       // private const int SIF_TRACKPOS = 0x10;
       // private const int SIF_RANGE = 0x01;
       // private const int SIF_POS = 0x04;
       // private const int SIF_PAGE = 0x02;
       // private const int SIF_ALL = SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS;

        // ListView messages
        private const uint LVM_SCROLL = 0x1014;
        private const int LVM_FIRST = 0x1000;
        //private const int LVM_SETGROUPINFO = (LVM_FIRST + 147);

        public event ScrollEventHandler Scroll;
        public ScrollBar MyScrollBar;

        public enum ScrollBarCommands : int
        {
          //  SB_LINEUP = 0,
          //  SB_LINELEFT = 0,
           // SB_LINEDOWN = 1,
          //  SB_LINERIGHT = 1,
           // SB_PAGEUP = 2,
          //  SB_PAGELEFT = 2,
          //  SB_PAGEDOWN = 3,
          //  SB_PAGERIGHT = 3,
           // SB_THUMBPOSITION = 4,
           // SB_THUMBTRACK = 5,
            //SB_TOP = 6,
            //SB_LEFT = 6,
            //SB_BOTTOM = 7,
            //SB_RIGHT = 7,
          // SB_ENDSCROLL = 8
        }


        protected virtual void OnScroll(ScrollEventArgs e)
        {
            Scroll?.Invoke(this, e);
        }
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == 0x115)
            { // Trap WM_VSCROLL
                ScrollEventArgs sargs = new ScrollEventArgs(ScrollEventType.EndScroll, GetScrollPos(this.Handle, SB_VERT));
                Scroll(this, sargs);
            }
        }
        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            //base.AddItemsCore(ColorPanel());
            base.OnDrawItem(e);
            Rectangle rectangle = base.GetItemRectangle(e.Index);
        }


        public int ScrollPosition
        {
            get
            {
                return GetScrollPos(this.Handle, SB_VERT);
            }
            set
            {
                int prevPos;
                int scrollVal=0;
                prevPos = GetScrollPos(this.Handle, SB_VERT);
                scrollVal = -(prevPos - value);

                SendMessage(this.Handle, LVM_SCROLL, (IntPtr)0, (IntPtr)scrollVal);
            }
        }


        [DllImport("user32.dll")]
        public static extern int SendMessage(
              int hWnd,      // handle to destination window
              uint Msg,       // message
              long wParam,  // first message parameter
              long lParam   // second message parameter
              );

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, int wMsg,
                                       int wParam, int lParam);

        [DllImport("user32.dll")]
        static extern int SendMessage(IntPtr hWnd, uint wMsg,
                                       IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int GetScrollPos(IntPtr hWnd, int nBar);

    }


}
