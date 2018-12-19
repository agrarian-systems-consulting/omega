using OMEGA.Forms;
using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Windows.Forms;

namespace OMEGA.Data_Classes
{
    class Classification
    {
        #region Variables
        private DataTable mtableClassif = new DataTable();
        private DataGridView mDatagridview;
        private bool mDataSaved;
        private Dictionary<int, InfoUserClassification> DicoInfoUserClassif;
        private ClassificationForm classificationForm;
        private PictureBox pictureBox;
        private Color color1;
        private Color color2;
        private Color color3;
        private int langue = Properties.Settings.Default.Langue;
        private Button mbuttongroup;
        private Subro.Controls.DataGridViewGrouper grouper = new Subro.Controls.DataGridViewGrouper();
        #endregion

        /// <summary>
        /// Constructor of a classification 
        /// </summary>
        /// <param name="dgv"></param>
        /// <param name="form"></param>
        /// <param name="mypictureBox"></param>
        /// <param name="buttongroup"></param>
        internal Classification(DataGridView dgv, ClassificationForm form, PictureBox mypictureBox,Button buttongroup)
        {
            mDatagridview = dgv;
            LoadDatagrid();
            CreateDico();
            pictureBox = mypictureBox;
            ListeRecordToDelete = new List<int>();
            classificationForm = form;
            mbuttongroup = buttongroup;
        }

        /// <summary>
        /// Load Data on the gridView
        /// </summary>
        private void LoadDatagrid()
        {
            string query = "Select * from Classifications";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
            adapter.Fill(mtableClassif);
            mDatagridview.DataSource = mtableClassif;
            mDatagridview.Columns[0].Visible = false;
        }

        /// <summary>
        /// Set the dico to contain the data of the user
        /// </summary>
        private void CreateDico()
        {
            try
            {
                int key = 0;
                DicoInfoUserClassif = new Dictionary<int, InfoUserClassification>();
                foreach (DataRow row in mtableClassif.Rows)
                {
                    InfoUserClassification infoUser = new InfoUserClassification();
                    int.TryParse(row.ItemArray[0].ToString(), out key);
                    DicoInfoUserClassif.Add(key, infoUser);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }

        }
        
        /// <summary>
        /// Occurs when the user clicks on the OK button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void Buttonok(object sender, EventArgs e)
        {
            try
            {
                SaveData(sender, e);
                classificationForm.DialogResult = DialogResult.OK;
                classificationForm.Close();
                
            }
            catch ( Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user clicks on the button cancel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void buttonCancel(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = new DialogResult();
                if (!mDataSaved)
                {
                    dialogResult = MessageBox.Show(Translation.Translate("Data has been modified but not saved, do you really want to exit ?",langue), "Warning", MessageBoxButtons.YesNo);
                }
                if (dialogResult == DialogResult.No)
                {
                    SaveData(sender, e);
                }
                classificationForm.Close();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user save the data contains on the dico
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void SaveData(object sender, EventArgs e)
        {
            try
            {
                string query;
                // DeleteGroupe();
                foreach (int index in ListeRecordToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(index, "Classifications");
                    SQlQueryExecuter.RunQuery(query);
                }

                foreach (KeyValuePair<int, InfoUserClassification> item in DicoInfoUserClassif)
                {

                    if (item.Value.Groupe == null && item.Value.Nom == null)
                    {
                        continue;
                    }

                    // l'utilisateur ajout un produit dans un groupe
                    if (item.Value.Modification == false)
                    {
                        query = "INSERT INTO Classifications (Groupe,Nom) VALUES ('" + item.Value.Groupe + "','" + item.Value.Nom + "') ;";
                        SQlQueryExecuter.RunQuery(query);
                    }
                    // l'utilisateur met à jour une donnée
                    if (item.Value.Modification)
                    {
                        query = "UPDATE Classifications SET Groupe = '" + item.Value.Groupe + "', Nom ='" + item.Value.Nom + "' " +
                        "WHERE ID = '" + item.Key.ToString() + "';";
                        if (SQlQueryExecuter.RunQuery(query) < 1)
                        {// If the update query did not work = we did not find the id then we insert a new record
                            query = "INSERT INTO Classifications (Groupe,Nom) VALUES ('" + item.Value.Groupe + "','" + item.Value.Nom + "') ;";
                            SQlQueryExecuter.RunQuery(query);
                        }
                    }
                    mDataSaved = true;
                    if (classificationForm.Text.Contains("*"))
                    {
                        classificationForm.Text = classificationForm.Text.Remove(classificationForm.Text.Length - 1, 1);
                    }
                }
                DicoInfoUserClassif.Clear();
                CreateDico();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when a value changed on the datagridview
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridView1CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            InfoUserClassification infoUser = new InfoUserClassification();
            try
            {
                int id;
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
                if ((int.TryParse(mDatagridview.Rows[e.RowIndex].Cells[0].Value.ToString(), out id)))
                {
                    if (e.ColumnIndex == 1)
                    {
                        DicoInfoUserClassif[id].Groupe = mDatagridview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    }
                    if (e.ColumnIndex == 2)
                    {
                        DicoInfoUserClassif[id].Nom = mDatagridview.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                    }
                    mDataSaved = false;
                    if (!classificationForm.Text.Contains("*"))
                    {
                        classificationForm.Text = classificationForm.Text + "*";
                    }
                }
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        /// <summary>
        /// Occurs when the user clicks on a cell
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
        {
            InfoUserClassification infoUser = new InfoUserClassification();
            try
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 0) return;
                int id;
                if ((int.TryParse(mDatagridview.Rows[e.RowIndex].Cells[0].Value.ToString(), out id)))
                {
                    if (!DicoInfoUserClassif.ContainsKey(id)) DicoInfoUserClassif.Add(id, infoUser);
                    if (mDatagridview.CurrentRow.Cells[1].Value.ToString() == "" && mDatagridview.CurrentRow.Cells[2].Value.ToString() == "")
                    {
                        DicoInfoUserClassif[id].Modification = false;
                    }
                    else
                    {
                        DicoInfoUserClassif[id].Modification = true;
                    }
                }
                FillAutolistAfterClick(e, e.ColumnIndex);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Return the last index of the table
        /// </summary>
        /// <returns></returns>
        internal int GetLastIndex()
        {
            if (mtableClassif.Rows.Count > 0)
            {
                int id;
                int.TryParse(mtableClassif.Rows[mtableClassif.Rows.Count - 1].ItemArray[0].ToString(), out id);
                return id;
            }
            else return 0;
        }

        /// <summary>
        /// Return the previous index of the table
        /// </summary>
        /// <param name="rowID"></param>
        /// <returns></returns>
        internal string GetPreviousIndex(int rowID)
        {
            return mtableClassif.Rows[rowID - 1].ItemArray[0].ToString();
        }

        /// <summary>
        /// Occurs when the user clicks on the button Add
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonAdd(object sender, EventArgs e)
        {
            try
            {
                string[] rowvalue = new string[mDatagridview.ColumnCount - 1];
                for (int i = 0; i < mDatagridview.ColumnCount - 1; i++)
                {
                    if (i == 0) rowvalue[i] = (GetLastIndex() + 1).ToString();
                    else rowvalue[i] = null;
                }
                mtableClassif.Rows.Add(rowvalue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Export the data into a .csv file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ExportTable(object sender, EventArgs e)
        {
            Export.RunExportTable(mtableClassif);
        }

        /// <summary>
        /// Duplicate the line selected by the user
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonDuplicate(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection ListuserRow = mDatagridview.SelectedRows;
                string[] rowvalue = new string[10];
                if (ListuserRow.Count == 1)
                {
                    for (int j = 0; j <= ListuserRow.Count - 1; j++)
                    {
                        rowvalue = new string[mDatagridview.ColumnCount - 1];
                        for (int i = 0; i < mDatagridview.ColumnCount - 1; i++)
                        {
                            rowvalue[i] = ListuserRow[j].Cells[i].Value.ToString();
                        }
                        mtableClassif.Rows.Add(rowvalue);
                    }
                    int id = FindIdClassif();
                    if (DicoInfoUserClassif.ContainsKey(id) == false)
                    { DicoInfoUserClassif.Add(id, new InfoUserClassification()); }
                    DicoInfoUserClassif[id].Groupe = rowvalue[1];
                    DicoInfoUserClassif[id].Nom = rowvalue[2];

                }
                else
                {
                    MessageBox.Show(Translation.Translate("Error,Plase reselect the row to duplicate",langue));
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private int FindIdClassif()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete the selected rows
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void ButtonRemoveRow(object sender, EventArgs e)
        {
            try
            {
                mDatagridview.AllowUserToDeleteRows = true;
                DataGridViewSelectedRowCollection UserSelectedRow = mDatagridview.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    ListeRecordToDelete.Add(GetId(index));
                    mDatagridview.Rows.RemoveAt(index);
                }
                mDataSaved = false;
                if (!classificationForm.Text.Contains("*"))
                {
                    classificationForm.Text = classificationForm.Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// return the id of the row ssend as parameter
        /// </summary>
        /// <param name="Rowindex"></param>
        /// <returns></returns>
        private int GetId(int Rowindex)
        {
            int id = -1;
            try
            {
                if (mDatagridview.Rows[Rowindex].Cells[0].Value.ToString() != null)
                {
                    int.TryParse(mDatagridview.Rows[Rowindex].Cells[0].Value.ToString(), out id);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }
        internal List<int> ListeRecordToDelete { get; set; }
        internal void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            pictureBox.BorderStyle = BorderStyle.FixedSingle;
        }
        internal void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            pictureBox.BorderStyle = BorderStyle.None;
        }

        /// <summary>
        /// Get the arvb name of the color in the datatable
        /// </summary>
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
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Set the color if the gridview
        /// </summary>
        private void ManageColor()
        {
            if (color1 != null && color2 != null)
            {
                Commun.Setbackground(mDatagridview, color1, color2);
                string query = SQLQueryBuilder.UpdateColorQuery(color1, color2,color3);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
            }
        }

        /// <summary>
        /// Allows user to group or disgroup the gridview data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void EnableGroupe(object sender, EventArgs e)
        {
            try
            {
                if (mbuttongroup.Text.Contains("Enable"))
                {
                    grouper.DataGridView = mDatagridview;
                    grouper.SetGroupOn(mDatagridview.Columns[1]);
                    grouper.DisplayGroup += grouper_DisplayGroup;
                    grouper.CollapseAll();
                    mbuttongroup.Text = "Disable Groupe";
                }
                else if (mbuttongroup.Text.Contains("Disable"))
                {
                    grouper.DataGridView = mDatagridview;
                    grouper.RemoveGrouping();
                    grouper.DisplayGroup -= grouper_DisplayGroup;
                    mbuttongroup.Text = "Enable Groupe";
                    ManageColor();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }

        }

        /// <summary>
        /// Set the color for the group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void grouper_DisplayGroup(object sender, Subro.Controls.GroupDisplayEventArgs e)
        {
            e.BackColor = (e.Group.GroupIndex % 2) == 0 ? color1 : color2;
        }

        /// <summary>
        /// Reset the color when the user sort the grid view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal void dataGridView1_Sorted(object sender, EventArgs e)
        {
            ManageColor();
        }

        /// <summary>
        /// Put the No on the row header
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            }
           
        }

        /// <summary>
        /// Display a list that allows the user to select if he/she wants to put the same name
        /// </summary>
        /// <param name="e"></param>
        /// <param name="column"></param>
        private void FillAutolistAfterClick(DataGridViewCellEventArgs e, int column)
        {
            try
            {
                double decalage = 1.05;
                List<string> list = new List<string>();
                string query = "";
                ContextMenu contextmenu = new ContextMenu();
                List<MenuItem> ListItem = new List<MenuItem>();
                switch (column)
                {

                    case 1: //group
                        query = "Select distinct Groupe From Classifications;";
                        list = SQlQueryExecuter.RunQueryReaderStr("Groupe", query);
                        break;
                }
                foreach (string item in list)
                {
                    MenuItem menuitem = new MenuItem(item);
                    ListItem.Add(menuitem);
                }
                foreach (MenuItem item in ListItem)
                {
                    contextmenu.MenuItems.Add(item);
                }

                mDatagridview.ContextMenu = contextmenu;
                Rectangle MyCell = mDatagridview.GetCellDisplayRectangle(e.ColumnIndex, e.RowIndex, true);
                int x = (int)Math.Ceiling(MyCell.X * decalage);
                int y = (int)Math.Ceiling(MyCell.Y * decalage);
                Point point = new Point(x, y);
                contextmenu.Show(mDatagridview, point);

                foreach (MenuItem item in ListItem)
                {
                    item.Click += Item_Click;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Occurs when the user select a value on the auto list proposal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Item_Click(object sender, EventArgs e)
        {
            try
            {
                string[] array = sender.ToString().Split(new string[] { "Text:" }, StringSplitOptions.None);
                mDatagridview.CurrentCell.Value = array[1].Trim();
                mDatagridview.Refresh();
                mDataSaved = false;
                if (!classificationForm.Text.Contains("*"))
                {
                    classificationForm.Text = classificationForm.Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }







    }
}
