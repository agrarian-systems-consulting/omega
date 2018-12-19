using OMEGA.Forms;
using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace OMEGA.Data_Classes
{
    internal class Periode
    {
        #region variables Globales
        private DataTable mPeriodeTable = new DataTable("Def_Calendrier");
        private DataTable mPeriodeTableDetail = new DataTable("Def_Calendrier 2");
        private string mainQuery;
        private int langue = Properties.Settings.Default.Langue;
        private string mainQueryDetail;
        private List<int> ListeRecordToDelete;
        private Boolean mDataSaved = false;
        private int mCurrentPeriodeid;
        private PictureBox pictureBox;
        private DataGridView mdataGridViewperiode;
        private Panel mpanelPeriode;
        private Color color1;
        private PeriodeForm periodform;
        private Color color2;
        private Color color3;
        private Button mbuttonAdd;
        private Button mbuttonOk;
        private Button mbuttonSave;
        private Button mbuttonValidate;
        private Button mbuttonCancel;
        private RadioButton mRadioButtnMonth;
        private RadioButton mRadionButtnWeek;
        private RadioButton mRadioButtn2Week;
        private RadioButton mRadionButtnUser;
        private Button mButtonAddDetail;
        private Button mButtonRemoveDtail;
        private GroupBox mGroupBoxTemplate;
        private int mIdAct;
        private bool mforActOrExpl;
        private string mType;
        private int mIdExp;
        #endregion

        internal DataGridView DataGridViewPeriodeDetail { get; set; }

        internal Periode(Button buttonOK, Button buttonSave, Button buttonCancel, Button buttonAdd,
           DataGridView dgvPeriode, DataGridView dgvPeriodeDetail, Panel panelPeriode, List<int> listid,
            int IdAct, RadioButton radBttnMonth, RadioButton radBttnWeek, RadioButton radBttn2Week,
            RadioButton radBttnUser, Button buttonAddDetail, Button buttonRemoveDetail, GroupBox GrpboxTemplate,
            bool forActivity = false, PictureBox ptcbox = null, PeriodeForm form = null, Button buttonvalidate = null)
        {
            mdataGridViewperiode = dgvPeriode;
            DataGridViewPeriodeDetail = dgvPeriodeDetail;
            mpanelPeriode = panelPeriode;
            mbuttonOk = buttonOK;
            mbuttonSave = buttonSave;
            pictureBox = ptcbox;
            mbuttonCancel = buttonCancel;
            mbuttonAdd = buttonAdd;
            mRadioButtnMonth = radBttnMonth;
            mRadionButtnWeek = radBttnWeek;
            mRadioButtn2Week = radBttn2Week;
            mRadionButtnUser = radBttnUser;
            mButtonRemoveDtail = buttonRemoveDetail;
            mbuttonValidate = buttonvalidate;
            mButtonAddDetail = buttonAddDetail;
            mGroupBoxTemplate = GrpboxTemplate;
            mIdAct = IdAct;
            mforActOrExpl = forActivity;
            ListeRecordToDelete = new List<int>();
            mainQuery = PeriodeQuery.MainQuery(0);
            periodform = form;
            if (File.Exists(Properties.Settings.Default.FichierTraduction))
            {
                SetCaption();
            }
            LoadPeriodeDataGrid();
        }
        
        internal Periode(Button buttonOK, Button buttonCancel, Button buttonAdd, DataGridView dgvPeriode,
           DataGridView dgvPeriodeDetail, Panel panelPeriode, List<int> listid,
           int IdAct, string type, bool forActOrExpl = false, PeriodeForm form = null)
        {
            mdataGridViewperiode = dgvPeriode;
            DataGridViewPeriodeDetail = dgvPeriodeDetail;
            mpanelPeriode = panelPeriode;
            mbuttonOk = buttonOK;
            if (buttonCancel != null) mbuttonCancel = buttonCancel;
            mbuttonAdd = buttonAdd;
            mIdAct = IdAct;
            mIdExp = IdAct;
            mforActOrExpl = forActOrExpl;
            periodform = form;
            mainQuery = PeriodeQuery.MainQuery(0);
            if (File.Exists(Properties.Settings.Default.FichierTraduction))
            {
                SetCaption();
            }
            mType = type;
            LoadPeriodeDataGrid();
        }

        private void SetCaption()
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;
                mbuttonOk.Text = Translation.Translate("Save", langue);
                if (mbuttonValidate != null) mbuttonValidate.Text = Translation.Translate("Validate", langue);
                if (mbuttonCancel != null) mbuttonCancel.Text = Translation.Translate("Cancel", langue);
                mbuttonAdd.Text = Translation.Translate("Add", langue);
                mButtonRemoveDtail.Text = Translation.Translate("Remove", langue);
                periodform.buttonRemovePeriode.Text = Translation.Translate("Remove", langue);
                mGroupBoxTemplate.Text = Translation.Translate("Template", langue);
                mRadioButtn2Week.Text = Translation.Translate("2 Weeks", langue);
                mRadioButtnMonth.Text = Translation.Translate("Month", langue);
                mRadionButtnWeek.Text = Translation.Translate("Week", langue);

            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadPeriodeDataGrid()
        {
            try
            {
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mPeriodeTable);
                mdataGridViewperiode.DataSource = mPeriodeTable;
                mdataGridViewperiode.Columns[0].Visible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void LoadPeriodeDetailDataGrid(int id)
        {
            try
            {
                mainQueryDetail = PeriodeQuery.MainQueryDetail(id);
                mPeriodeTableDetail.Clear();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQueryDetail, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mPeriodeTableDetail);
                RenameColumnHeader();
                if (mPeriodeTableDetail.Columns.Count < 6)
                {
                    mPeriodeTableDetail.Columns.Add(Translation.Translate("Days", langue));
                    mPeriodeTableDetail.Columns.Add(Translation.Translate("Usefull days", langue));
                    mPeriodeTableDetail.Columns.Add(Translation.Translate("Usefull hours", langue));
                }
                SetNewColumnValue();
                DataGridViewPeriodeDetail.DataSource = mPeriodeTableDetail;
                mRadioButtnMonth.Checked = false;
                mRadionButtnWeek.Checked = false;
                mRadioButtn2Week.Checked = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void LoadPeriodeDetailDataGridForActivity(int id)
        {
            try
            {
                mainQueryDetail = PeriodeQuery.MainQueryDetailForActivity(id, mIdAct);

                mCurrentPeriodeid = id;
                mPeriodeTableDetail.Clear();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQueryDetail, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(mPeriodeTableDetail);
                if (mPeriodeTableDetail.Columns.Count < 2 && mType == "Act")
                {
                    AddColumn();
                }
                SetNewColumnValueForActivity();
                DataGridViewPeriodeDetail.DataSource = mPeriodeTableDetail;
                AddTotalLine();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void AddColumn()
        {
            try
            {
                int Idtype = SQLQueryBuilder.FindId("Activite", "IdType", "ID", mIdAct);
                if (Idtype == 0) Idtype = 1; //nul....
                switch (Idtype)
                {
                    case 1://annuelle
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant 1 F", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("1 F", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant 1 H", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("1 H", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant 1 T", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("1 T", langue));
                        break;
                    case 5://animaux
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant 1 F", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("1 F", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant 1 H", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("1 H", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant 1 T", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("1 T", langue));
                        break;
                    case 6: //pluriannuelle
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant 1 F", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("1 F", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("2 F", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("3 F", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("4 F", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant 1 H", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("1 H", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("2 H", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("3 H", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("4 H", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant 1 T", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("1 T", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("2 T", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("3 T", langue));
                        mPeriodeTableDetail.Columns.Add(Translation.Translate("4 T", langue));
                        break;
                    case 4: //perenne
                        string query = "Select * From Ate_CatPhase Where Nom = '" + SQLQueryBuilder.FindName("Activite", "Nom", "ID", mIdAct) + "';";
                        string[] reader = SQlQueryExecuter.RunQueryReader(query);
                        int nbphase = Commun.GetNbPhase(mIdAct);
                        
                        for (int i = 1; i <= nbphase; i++)
                        {
                            if (i == 1) mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant", langue) + " " + i + " " + Translation.Translate("F", langue));
                            mPeriodeTableDetail.Columns.Add(i + " " + Translation.Translate("F", langue));
                        }
                        for (int i = 1; i <= nbphase; i++)
                        {
                            if (i == 1) mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant", langue) + " " + i + " " + Translation.Translate("H", langue));
                            mPeriodeTableDetail.Columns.Add(i + " " + Translation.Translate("H", langue));
                        }
                        for (int i = 1; i <= nbphase; i++)
                        {
                            if (i == 1) mPeriodeTableDetail.Columns.Add(Translation.Translate("Avant", langue) + " " + i + " " + Translation.Translate("T", langue));
                            mPeriodeTableDetail.Columns.Add(i + " " + Translation.Translate("T", langue));
                        }
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void RenameColumnHeader()
        {
            try
            {
                if (DataGridViewPeriodeDetail.Columns.Count <= 0) return;
                DataGridViewPeriodeDetail.Columns[0].HeaderText = Translation.Translate(Translation.Translate("Period",langue), langue);
                DataGridViewPeriodeDetail.Columns[1].HeaderText = Translation.Translate("Begining", langue);
                DataGridViewPeriodeDetail.Columns[2].HeaderText = Translation.Translate("End", langue);
                DataGridViewPeriodeDetail.Columns[3].HeaderText = Translation.Translate("Hours per person", langue);
                DataGridViewPeriodeDetail.Columns[4].HeaderText = Translation.Translate("Availability (%)", langue);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetNewColumnValue()
        {
            // 0 = periode, 1= j début, 2 = j fin 3 , 4 = h/j, 5 = %dispo
            // format date : DD/MM
            int i = 0;
            try
            {
                foreach (DataRow row in mPeriodeTableDetail.Rows)
                {
                    i++;
                    string jDebut = row.ItemArray[1].ToString();
                    jDebut = jDebut.Substring(0, 2);
                    string Jfin = row.ItemArray[2].ToString();
                    Jfin = Jfin.Substring(0, 2);
                    double jdebutint;
                    double.TryParse(jDebut, out jdebutint);
                    double jfinint;
                    double.TryParse(Jfin, out jfinint);
                    double nbjour = jfinint - jdebutint + 1;
                    if (nbjour < 0) nbjour = 7;// pour régler le problème du changement de mois
                    row.SetField<double>(Translation.Translate("Days", langue), nbjour);
                    string Perdispo = row.ItemArray[4].ToString();
                    double PerdispoInt;
                    double.TryParse(Perdispo, out PerdispoInt);
                    row.SetField<double>(Translation.Translate("Usefull days", langue), nbjour * (PerdispoInt / 100));
                    double H_pInt;
                    string H_p = row.ItemArray[3].ToString();
                    double.TryParse(H_p, out H_pInt);
                    row.SetField<double>(Translation.Translate("Usefull hours", langue), (nbjour * (PerdispoInt / 100)) * H_pInt);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + i);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ButtonCopyFromClick(object sender, EventArgs e)
        {
            try
            { 
                StandardForm form = new StandardForm(0, "ListePeriode");
                form.ShowDialog();
                RefreshTablePeriodeDetail();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message );
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SetNewColumnValueForActivity()
        {
            // 0 = periode, 1= qte av1, 2 =qte1
            int i = 0;
            try
            {
                int Id=0;
                foreach (DataRow row in mPeriodeTableDetail.Rows)
                {
                   string query = PeriodeQuery.SelectTravailQuery(mIdAct, mCurrentPeriodeid, row.ItemArray[0].ToString());
                    List<double> list = new List<double>();
                    list = SQlQueryExecuter.RunQueryReaderDouble("Qte_Av1_H", query);
                    if (Commun.ListHasValue(list))
                    {
                        row.SetField<double>("Avant 1 H", list[0]);
                    }
                    list = SQlQueryExecuter.RunQueryReaderDouble("Qte_Av1_F", query);
                    if (Commun.ListHasValue(list))
                    {
                        row.SetField<double>("Avant 1 F", list[0]);
                    }
                    if (mType == "Act")
                    {
                        int Idtype = SQLQueryBuilder.FindId("Activite", "IdType", "ID", mIdAct);
                        switch (Idtype)
                        {
                            case 1:
                                SetRowField(query, row, "1", "F");
                                SetRowField(query, row, "1", "H");
                                break;
                            case 6:
                                SetRowField(query, row, "1", "F");
                                SetRowField(query, row, "1", "H");
                                SetRowField(query, row, "2", "F");
                                SetRowField(query, row, "2", "H");
                                SetRowField(query, row, "3", "F");
                                SetRowField(query, row, "3", "H");
                                SetRowField(query, row, "4", "F");
                                SetRowField(query, row, "4", "H");
                                break;
                            case 4:
                                string query2 = "Select * From Ate_CatPhase Where Nom = '" + SQLQueryBuilder.FindName("Activite", "Nom", "ID", mIdAct) + "';";
                                string[] reader = SQlQueryExecuter.RunQueryReader(query2);
                                int nbphase = Commun.GetNbPhase(mIdAct);
                                for (int j = 1; j <= nbphase; j++)
                                {
                                    SetRowField(query, row, j.ToString(), "F");
                                    SetRowField(query, row, j.ToString(), "H");
                                }
                                break;
                        }
                    }
                }
                if (mType == "Act") CalculTotalColumn();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message + i);
            }
        }

        private void SetRowField(string query,DataRow row,string index,string genre)
        {
            List<double> list = new List<double>();
            list = SQlQueryExecuter.RunQueryReaderDouble("Qte_"+index+"_"+genre, query);
            if (Commun.ListHasValue(list))
            {
                row.SetField<double>(index +" "+genre, list[0]);
            }
        }

        private void CalculTotalColumn()
        {
            try
            {
                double temp1;
                double temp3 = 0;
                List<double> listTemp = new List<double>(); 
                int indexColonne = 0;
                int NumCol = 1;
                int nbphase = 1;
                int Idtype = SQLQueryBuilder.FindId("Activite", "IdType", "ID", mIdAct);
                switch (Idtype)
                {
                    case 1:
                        nbphase = 1;
                        break;
                    case 4:
                        string query = "Select * From Ate_CatPhase Where Nom = '" + SQLQueryBuilder.FindName("Activite", "Nom", "ID", mIdAct) + "';";
                        string[] reader = SQlQueryExecuter.RunQueryReader(query);
                        nbphase = Commun.GetNbPhase(mIdAct);
                        break;
                    case 6:
                        nbphase = 4;
                        break;
                }
                for (NumCol = 1; NumCol <= nbphase;NumCol++)
                {
                    foreach (DataRow row in mPeriodeTableDetail.Rows)
                    {
                        indexColonne = 0;
                        listTemp.Clear();
                        temp3 = 0;
                        foreach (DataColumn column in mPeriodeTableDetail.Columns)
                        {
                            if (column.ColumnName.Contains(NumCol.ToString()) && column.ColumnName.Contains(Translation.Translate("Avant", langue)) && !column.ColumnName.Contains(Translation.Translate("T", langue)))
                            {
                                double.TryParse(row.ItemArray[indexColonne].ToString(), out temp1);
                                listTemp.Add(temp1);
                            }
                            indexColonne++;
                        }
                        foreach (double item in listTemp)
                        {
                            temp3 = temp3 + item;
                        }
                        indexColonne = 0;
                        listTemp.Clear();
                        foreach (DataColumn column in mPeriodeTableDetail.Columns)
                        {
                            if (column.ColumnName.Contains(NumCol.ToString()) && column.ColumnName.Contains(Translation.Translate("T", langue)) && !column.ColumnName.Contains(Translation.Translate("Avant", langue)))
                            {
                                indexColonne++;
                                string columnName = (indexColonne).ToString() + " " + Translation.Translate("T", langue);
                                row.SetField<double>(columnName, temp3);
                                break;
                            }
                        }
                        //string columnName =  Translation.Translate("Avant", langue) + " 1 " + Translation.Translate("T", langue);
                        //row.SetField<double>(columnName, temp3);
                        temp3 = 0;
                        listTemp.Clear();
                        indexColonne = 0;
                        foreach (DataColumn column in mPeriodeTableDetail.Columns)
                        {
                            if (column.ColumnName.Contains(NumCol.ToString()) && !column.ColumnName.Contains(Translation.Translate("Avant", langue)) && !column.ColumnName.Contains(Translation.Translate("T", langue)))
                            {
                                double.TryParse(row.ItemArray[indexColonne].ToString(), out temp1);
                                listTemp.Add(temp1);
                            }
                            indexColonne++;
                        }
                        foreach (double item in listTemp)
                        {
                            temp3 = temp3 + item;
                        }
                        indexColonne = 1;
                        listTemp.Clear();
                        foreach (DataColumn column in mPeriodeTableDetail.Columns)
                        {
                            if (column.ColumnName.Contains(NumCol.ToString()) && column.ColumnName.Contains(Translation.Translate("T", langue)) && !column.ColumnName.Contains(Translation.Translate("Avant", langue)))
                            {
                                row.SetField<double>(column.ColumnName, temp3);
                                indexColonne++;
                                break;
                            }
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

        private void AddTotalLine()
        {
            try
            { 
                double totalQte1 = 0;
                double totalAvQte1 = 0;
                double totalQte12 = 0;
                double totalAvQte12 = 0;
                double totalQteT = 0;
                double totalAvQteT = 0;
                double tempdouble;
                foreach (DataGridViewRow row in DataGridViewPeriodeDetail.Rows)
                {
                    double.TryParse(row.Cells[1].Value.ToString(), out tempdouble);
                    totalAvQte1 += tempdouble;
                    double.TryParse(row.Cells[2].Value.ToString(), out tempdouble);
                    totalQte1 += tempdouble;
                    double.TryParse(row.Cells[3].Value.ToString(), out tempdouble);
                    totalAvQte12 += tempdouble;
                    double.TryParse(row.Cells[4].Value.ToString(), out tempdouble);
                    totalQte12 += tempdouble;
                }
                totalQteT = totalQte1 + totalQte12;
                totalAvQteT = totalAvQte1 + totalAvQte12;
                mPeriodeTableDetail.Rows.Add(new object[] {"Total Annuel", totalAvQte1, totalQte1, totalAvQte12, totalQte12,totalAvQteT, totalQteT });
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message );
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void dataGridViewPeriode_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string userValue = mdataGridViewperiode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
                int.TryParse(mdataGridViewperiode.Rows[e.RowIndex].Cells[0].Value.ToString(), out mCurrentPeriodeid);
                if (mCurrentPeriodeid > 0)
                {
                    string query = "UPDATE Def_Calendrier SET Nom = '" + userValue + "' WHERE IdPeriode = '" + mCurrentPeriodeid + "';";
                    SQlQueryExecuter.RunQuery(query);
                    if (mforActOrExpl)LoadPeriodeDetailDataGridForActivity(mCurrentPeriodeid);
                    else LoadPeriodeDetailDataGrid(mCurrentPeriodeid);
                    // modification
                }
                else
                {
                    if (mforActOrExpl) LoadPeriodeDetailDataGridForActivity(-1);
                    else LoadPeriodeDetailDataGrid(-1);
                    // rajout d'une nouvelle ligne
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void buttonAddPeriode(object sender, EventArgs e)
        {
            try
            {
                string[] rowvalue = new string[mdataGridViewperiode.ColumnCount];
                for (int i = 0; i <= mdataGridViewperiode.ColumnCount - 1; i++)
                {
                    if (i == 0) rowvalue[i] = Commun.GetMaxId("IdPeriode","Def_Calendrier").ToString() ;
                    else  rowvalue[i] = null;
                }
                mPeriodeTable.Rows.Add(rowvalue);
                if (mbuttonValidate != null) mbuttonValidate.Enabled = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void RefreshTablePeriodeDetail()
        {
            try
            {
                if (mforActOrExpl) SetNewColumnValueForActivity();
                else SetNewColumnValue();
                DataGridViewPeriodeDetail.DataSource = mPeriodeTableDetail;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ButtonOkClick(object sender, EventArgs e)
        {
            try
            {
                string query;
                if (mCurrentPeriodeid <= 0) // si l'id de la periode =-1 alors on vient de créer cette periode
                {
                    mCurrentPeriodeid = Commun.GetMaxId("IdPeriode","Def_Calendrier")+1;
                }

                foreach (int index in ListeRecordToDelete)
                {
                    query = "Delete From Def_Calendrier Where IdPeriode = '" + index + "';";
                    SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();

                if (!mDataSaved)
                {
                    query = "DELETE FROM Def_Calendrier WHERE IdPeriode = '" + mCurrentPeriodeid + "';";
                    SQlQueryExecuter.RunQuery(query);
                    foreach (DataGridViewRow row in DataGridViewPeriodeDetail.Rows)
                    {
                        query = PeriodeQuery.InsertDefCalQuery(row, mdataGridViewperiode.CurrentRow.Cells[1].Value.ToString(), mCurrentPeriodeid);
                        SQlQueryExecuter.RunQuery(query);
                        mDataSaved = true;
                    }

                    /*query = "SELECT IdPeriode FROM Caract_Activite WHERE IdPeriode = '" + mCurrentPeriodeid + "'" +
                    "AND IdActivite = '"+mIdAct+"'";
                    List<string> list = SQlQueryExecuter.RunQueryReaderStr("IdPeriode", query);
                    if (list.Count < 1)
                    {
                        query = "INSERT INTO Caract_Activite  (IdActivite,IdPeriode)  " +
                        "VALUES ('" + mIdAct  + "','" + mCurrentPeriodeid + "')";
                        SQlQueryExecuter.RunQuery(query);
                    }*/
                    if (periodform != null)
                    {
                        //periodform.DialogResult = DialogResult.OK;
                    }
                    //CalculTotalColumn();
                    Properties.Settings.Default.DoitSauvegarger = true;
                }
            }
            catch ( Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ButtonOkClickForActivity(object sender, EventArgs e)
        {
            try
            {
                if (!mDataSaved)
                {
                    string query = "";
                    foreach (DataGridViewRow row in DataGridViewPeriodeDetail.Rows)
                    {
                        if (!row.Cells[0].Value.ToString().Contains("Total"))
                        {
                            query = PeriodeQuery.SelectTravailQuery(mIdAct, mCurrentPeriodeid, row.Cells[0].Value.ToString());

                            if (mType == "Act")
                            {
                                List<int> listint = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                                int Idtype = SQLQueryBuilder.FindId("Activite", "IdType", "ID", mIdAct);
                                if (Commun.ListHasValue(listint))
                                {
                                    query = GetUpdateQuery(Idtype, listint[0], row);
                                }
                                else
                                {
                                    query = PeriodeQuery.InsertTravailQuery(row.Cells[0].Value.ToString(), mCurrentPeriodeid, mIdAct, GetInsertFieldQuery(Idtype), GetInsertQuery(Idtype, row));
                                }
                            }
                            SQlQueryExecuter.RunQuery(query);
                            if (mType == "Act") CalculTotalColumn();
                            mDataSaved = true;
                        }
                    }
                }
                Properties.Settings.Default.DoitSauvegarger = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal string GetInsertFieldQuery(int type)
        {
            string query = "";
            try
            {
                if (type == 0) type = 1;
                switch (type)
                {
                    case 1://annuelle
                        query = "Qte_Av1_F,Qte_Av1_H,Qte_Av1_T,Qte_1_F,Qte_1_H,Qte_1_T";
                        break;
                    case 5://animaux
                        query = "Qte_Av1_F,Qte_Av1_H,Qte_Av1_T,Qte_1_F,Qte_1_H,Qte_1_T";
                        break;
                    case 4://perenne
                        string query2 = "Select * From Ate_CatPhase Where Nom = '" + SQLQueryBuilder.FindName("Activite", "Nom", "ID", mIdAct) + "';";
                        string[] reader = SQlQueryExecuter.RunQueryReader(query2);
                        int nbphase = Commun.GetNbPhase(mIdAct);
                        query = "Qte_Av1_F,Qte_Av1_H,Qte_Av1_T";
                        for (int i = 1; i <= nbphase; i++)
                        {
                            query = query + ",Qte_" + i + "_F," + "Qte_" + i + "_H," + "Qte_" + i + "_T";
                        }
                        break;
                    case 6: //pluriannuelle
                        query = "Qte_Av1_F,Qte_Av1_H,Qte_Av1_T";
                        for (int i = 1; i <= 4; i++)
                        {
                            query = query + ",Qte_" + i + "_F" + ",Qte_" + i + "_H" + ",Qte_" + i + "_T";
                        }
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return query;
        }

        internal string GetInsertQuery(int type,DataGridViewRow row)
        {
            string query = "";
            try
            {
                if (type == 0) type = 1;
                switch (type)
                {
                    case 1://annuelle
                        query = ",'" + row.Cells[GetNumColAv1("F")].Value.ToString() + "','" + row.Cells[GetNumColAv1("H")].Value.ToString() + "','" + row.Cells[GetNumColAv1("T")].Value.ToString()+"'";
                        query = query + ",'" + row.Cells[GetNumCol("F", 1)].Value.ToString() + "','" + row.Cells[GetNumCol("H", 1)].Value.ToString() + "','" + row.Cells[GetNumCol("T", 1)].Value.ToString()+"'";
                        break;
                    case 5://animaux
                        query = ",'" + row.Cells[GetNumColAv1("F")].Value.ToString() + "','" + row.Cells[GetNumColAv1("H")].Value.ToString() + "','" + row.Cells[GetNumColAv1("T")].Value.ToString() + "'";
                        query = query + ",'" + row.Cells[GetNumCol("F", 1)].Value.ToString() + "','" + row.Cells[GetNumCol("H", 1)].Value.ToString() + "','" + row.Cells[GetNumCol("T", 1)].Value.ToString() + "'";
                        break;
                    case 6://pluriannuelle
                        query = ",'" + row.Cells[GetNumColAv1("F")].Value.ToString() + "','" + row.Cells[GetNumColAv1("H")].Value.ToString() + "','" + row.Cells[GetNumColAv1("T")].Value.ToString() + "'";
                        for (int i = 1; i <= 4; i++)
                        {
                            query = query + ",'" + row.Cells[GetNumCol("F", i)] + "','" + row.Cells[GetNumCol("H",i)].Value.ToString() + "','" + row.Cells[GetNumCol("T",i)].Value.ToString() + "'";
                        }
                        break;
                    case 4: //perenne
                        string query2 = "Select * From Ate_CatPhase Where Nom = '" + SQLQueryBuilder.FindName("Activite", "Nom", "ID", mIdAct) + "';";
                        string[] reader = SQlQueryExecuter.RunQueryReader(query2);
                        int nbphase = Commun.GetNbPhase(mIdAct);
                        query = ",'" + row.Cells[GetNumColAv1("F")].Value.ToString() + "','" + row.Cells[GetNumColAv1("H")].Value.ToString() + "','" + row.Cells[GetNumColAv1("T")].Value.ToString() + "'";
                        for (int i = 1; i <= nbphase; i++)
                        {
                            query = query + ",'" + row.Cells[GetNumCol("F", i)].Value.ToString() + "','" + row.Cells[GetNumCol("H", i)].Value.ToString() + "','" + row.Cells[GetNumCol("T", i)].Value.ToString() + "'";
                        }
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return query; ;
        }

        internal string GetUpdateQuery(int type,int ID,DataGridViewRow row)
        {
            string query = "";
;            try
             {
                switch (type)
                {
                    case 1://annuelle
                        query = "Update Travail Set Qte_1_H = '" + row.Cells[2].Value.ToString() + "'," +
                                   "Qte_Av1_H = '" + row.Cells[1].Value.ToString() + "', Qte_1_F = '" +
                                   row.Cells[4].Value.ToString() + "',Qte_Av1_F = '" + row.Cells[3].Value.ToString() +
                                  "', Qte_1_T = '" + row.Cells[6].Value.ToString() + "',Qte_Av1_T = '" +
                                  row.Cells[5].Value.ToString() +
                                   "' Where ID = '" + ID + "';";
                        break;
                    case 6: //pluriannuelle
                        query = "Update Travail Set Qte_Av1_T ='" + row.Cells[GetNumColAv1("T")].Value.ToString() +
                            ", Qte_Av1_F ='" + row.Cells[GetNumColAv1("F")].Value.ToString() +
                            ", Qte_Av1_H ='" + row.Cells[GetNumColAv1("H")].Value.ToString() +
                            QteUpdateQuery(row) + " WHERE ID = '" + ID + "';";
                        break;
                    case 4: //perenne
                        query = "Update Travail Set Qte_Av1_T ='" + row.Cells[GetNumColAv1("T")].Value.ToString() +
                            ", Qte_Av1_F ='" + row.Cells[GetNumColAv1("F")].Value.ToString() +
                            ", Qte_Av1_H ='" + row.Cells[GetNumColAv1("H")].Value.ToString() +
                            QteUpdateQuery(row) + " WHERE ID = '"+ID+"';";
                        break;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return query;
        }

        internal string QteUpdateQuery(DataGridViewRow row)
        {
            string query = ",";
            try
            {
                for (int i =1; i<=4; i++)
                {
                    query = query + "Qte_" + i + "_F ='" + row.Cells[GetNumCol("F", i)].Value.ToString() +
                       ",Qte_" + i + "_T ='" + row.Cells[GetNumCol("T", i)].Value.ToString() +
                       ",Qte_" + i + "_H ='" + row.Cells[GetNumCol("H", i)].Value.ToString() ;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return query;
        }

        internal int GetNumColAv1(string genre)
        {
            try
            {
                int indexColumn = 0;
                foreach (DataGridViewColumn column in DataGridViewPeriodeDetail.Columns)
                {
                    if (column.Name.Contains(Translation.Translate("Avant", langue)) && column.Name.Contains(Translation.Translate(genre, langue)))
                    {
                        return indexColumn;
                    }
                    indexColumn++;
                }
                return 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return 0;
            }
        }

        internal int GetNumCol(string genre,int numphase)
        {
            try
            {
                int indexColumn = 0;
                foreach (DataGridViewColumn column in DataGridViewPeriodeDetail.Columns)
                {
                    if (column.Name.Contains((numphase).ToString()) && column.Name.Contains(Translation.Translate(genre, langue)) && !column.Name.Contains(Translation.Translate("Avant", langue)))
                    {
                        return indexColumn;
                    }
                    indexColumn++;
                }
                return 0;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return 0;
            }
        }

        internal void buttonRemove(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedCellCollection UserSelectCell = mdataGridViewperiode.SelectedCells;
                foreach (DataGridViewCell cell in UserSelectCell)
                {
                    int index = cell.RowIndex;
                    if (!ListeRecordToDelete.Contains(GetId(index)))
                    {
                        ListeRecordToDelete.Add(GetId(index));
                    }
                    mdataGridViewperiode.Rows.RemoveAt(index);
                }
                DataGridViewPeriodeDetail.DataSource = null;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        internal void dataGridViewDetailPeriodeCellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 3 || e.ColumnIndex == 4)
            {
                DataGridViewPeriodeDetail.ReadOnly = false;
            }
            else
            {
                DataGridViewPeriodeDetail.ReadOnly = true;
            }
        }

        internal void radioBttnMoisCheckedChanged(object sender, EventArgs e)
        {
            try
            { 
                if (mRadioButtnMonth.Checked == true)
                {
                    if (AskUserToDelete("Mois"))
                    {
                        RefreshTablePeriodeDetail();
                    }
                }
                RenameColumnHeader();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message );
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }

        }
        internal void radioButton2weeksCheckChanged(object sender, EventArgs e)
        {
            try
            { 
                if (mRadioButtn2Week.Checked == true)
                {
                    if (AskUserToDelete("2 Semaines"))
                    {
                        RefreshTablePeriodeDetail();
                    }
                }
                RenameColumnHeader();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message );
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }
        internal void radioButtonWeekCheckChanged(object sender, EventArgs e)
        {
            try
            { 
                if (mRadionButtnWeek.Checked == true)
                {
                    if (AskUserToDelete("Semaine"))
                    {
                        RefreshTablePeriodeDetail();
                    }
                }
                RenameColumnHeader();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message );
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        internal void dataGridViewPeriodeCellClickForActivity(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
                if(e.RowIndex >= 0)
                {
               
                    int.TryParse(mdataGridViewperiode.Rows[e.RowIndex].Cells[0].Value.ToString(), out mCurrentPeriodeid);
                    //if (e.ColumnIndex == 2)
                    //int.TryParse(mdataGridViewperiode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out mIdAct);
                    LoadPeriodeDetailDataGridForActivity(mCurrentPeriodeid);
                    SetColor();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message );
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        internal void DatagridDetailPeriodKeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.') && (e.KeyChar != ','))
                {
                    e.Handled = true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        
        internal void dataGridViewDetailPeriodeCellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
                int result;
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 0) return;
                if (!(int.TryParse(DataGridViewPeriodeDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out result)))
                {
                    MessageBox.Show(Translation.Translate("Please, insert a whole number",langue));
                    DataGridViewPeriodeDetail.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = "";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message );
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        internal void dataGridViewPeriodeEndEdit(object sender , DataGridViewCellEventArgs e)
        {
            try
            { 
               
                if (mdataGridViewperiode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                {
                    if (SQLQueryBuilder.FindId("Def_Calendrier", "IdPeriode", "Nom", mdataGridViewperiode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) != 0)
                    {
                        int.TryParse(mdataGridViewperiode.Rows[e.RowIndex].Cells[0].Value.ToString(), out mCurrentPeriodeid);
                    }
                    else
                    {
                        mCurrentPeriodeid = -1;
                    }
                    if (mforActOrExpl) LoadPeriodeDetailDataGridForActivity(mCurrentPeriodeid);
                    else LoadPeriodeDetailDataGrid(mCurrentPeriodeid);
                }
                SetColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message );
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        internal void dataGridViewDetailPeriode_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            SetNewColumnValue();
        }

        internal void dataGridViewPeriode_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            { 
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 0) return;
                if (mdataGridViewperiode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() != "")
                {
                    if (SQLQueryBuilder.FindId("Def_Calendrier","IdPeriode", "Nom", mdataGridViewperiode.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString()) != 0 )
                    {
                        int.TryParse(mdataGridViewperiode.Rows[e.RowIndex].Cells[0].Value.ToString(), out mCurrentPeriodeid);
                    }
                    else
                    {
                        mCurrentPeriodeid = -1;
                    }
                    if (mforActOrExpl) LoadPeriodeDetailDataGridForActivity(mCurrentPeriodeid);
                    else LoadPeriodeDetailDataGrid(mCurrentPeriodeid);
                }
                else mdataGridViewperiode.ReadOnly = false;
                SetColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                    Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        internal void ButtonCopyClick(object sender, EventArgs e)
        {
            try
            { 
                string value = DataGridViewPeriodeDetail.CurrentCell.Value.ToString();
                int currentRowIndex = DataGridViewPeriodeDetail.CurrentCell.RowIndex;
                foreach (DataGridViewRow row in DataGridViewPeriodeDetail.Rows)
                {
                    if (row.Index >= currentRowIndex)
                    row.Cells[DataGridViewPeriodeDetail.CurrentCell.ColumnIndex].Value = value;
                }
                CalculTotalColumn();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message );
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        internal void ButtonCopyClick2(object sender, EventArgs e)
        {
            try
            {
                string value = DataGridViewPeriodeDetail.CurrentCell.Value.ToString();
                int currentRowIndex = DataGridViewPeriodeDetail.CurrentCell.RowIndex;
                foreach (DataGridViewRow row in DataGridViewPeriodeDetail.Rows)
                {
                    if (row.Index >= currentRowIndex)
                        row.Cells[DataGridViewPeriodeDetail.CurrentCell.ColumnIndex].Value = value;
                }
                SetNewColumnValue();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal void ButtonValidateName(object sender, EventArgs e)
        {
            try
            {
                string nom = mdataGridViewperiode.Rows[mdataGridViewperiode.CurrentRow.Index].Cells[1].Value.ToString();
                string query = " Select * From Def_Calendrier Where Nom = '" + nom + "';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (!Commun.ListHasValue(list))
                {
                    mCurrentPeriodeid = Commun.GetMaxId("IdPeriode", "Def_Calendrier") + 1;
                    query = "Insert Into Def_Calendrier (IdPeriode,Nom) VALUES ('" + mCurrentPeriodeid + "','" + nom + "');";
                    SQlQueryExecuter.RunQuery(query);
                }
                LoadPeriodeDetailDataGrid(mCurrentPeriodeid);
                mbuttonValidate.Enabled = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        internal void buttonExport(object sender, EventArgs e)
        {
            Export.RunExportTable(mPeriodeTableDetail);
        }

        internal void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            try
            { 
                pictureBox.BorderStyle = BorderStyle.FixedSingle;
            }
            catch
            {

            }
        }

        internal void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                pictureBox.BorderStyle = BorderStyle.None;
            }
            catch
            {

            }
           
        }

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
                MessageBox.Show(Ex.Message );
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }
                
        /// <summary>
        /// Retourne l'Id de la ligne envoyée en paramètre
        /// </summary>
        /// <param name="Rowindex"></param>
        /// <returns></returns>
        private int GetId(int Rowindex)
        {
            int id = -1;
            try
            {
                if (mdataGridViewperiode.Rows[Rowindex].Cells[0].Value.ToString() != null)
                {
                    int.TryParse(mdataGridViewperiode.Rows[Rowindex].Cells[0].Value.ToString(), out id);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }

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

        private void ManageColor()
        {
            if (color1 != null && color2 != null)
            {
                Commun.Setbackground(DataGridViewPeriodeDetail, color1, color2);
                string query = SQLQueryBuilder.UpdateColorQuery(color1, color2,color3);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
            }
        }
        
        private bool CheckTable()
        {
            string query = "SELECT * FROM Def_Calendrier WHERE IdPeriode = '" + mCurrentPeriodeid + "';";
            bool hasValue = true;
            try
            {
                string[] reader = SQlQueryExecuter.RunQueryReader(query);
                if (reader.Length-2 > 1)
                {
                    if (reader[0] != null)
                    {
                        hasValue = true;
                    }
                    else hasValue = false;
                }
                else hasValue = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return hasValue;
        }

        #region Template Calendrier


        private bool AskUserToDelete(string templateName)
        {
            try
            { 
                if (CheckTable())
                {
                    DialogResult dialogresult = MessageBox.Show(Translation.Translate("Attention des enregistrements ont déja été sauvegardé pour cette période.",langue)+ ((char)13)
                                           + Translation.Translate("Do you want to replace theses records with the template :", langue) + templateName + " ?", "Warning", MessageBoxButtons.YesNo);
                    if (dialogresult == DialogResult.Yes)
                    {
                        string query = "DELETE FROM Def_Calendrier WHERE IdPeriode = '" + mCurrentPeriodeid + "';";
                        int RowDeleted = SQlQueryExecuter.RunQuery(query);
                        MessageBox.Show(RowDeleted + Translation.Translate("  record(s) deleted.", langue));
                        LoadTemplate(templateName);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                 LoadTemplate(templateName);
                 return true;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return false;
            }
        }

        private void LoadTemplate(string templateName)
        {
            try
            {
                if (mPeriodeTableDetail.Columns.Count == 0) LoadPeriodeDetailDataGrid(mCurrentPeriodeid);
                switch (templateName)
                {
                    case "Mois":
                        FillTemplateMois();
                        break;
                    case "2 Semaines":
                        FillTemplate2Semaines();
                        break;
                    case "Semaine":
                        FillTemplateSemaine();
                        break;
                }
                mDataSaved = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void FillTemplate2Semaines()
        {
            try
            {
                mPeriodeTableDetail.Clear();
                for (int i = 0; i < 24; i++)
                {
                    mPeriodeTableDetail.Rows.Add();Translation.Translate("Beginning", langue);
                }   
                
                mPeriodeTableDetail.Rows[0].SetField<string>(Translation.Translate("Beginning",langue), "01/01");
                mPeriodeTableDetail.Rows[0].SetField<string>(Translation.Translate("End",langue), "15/01");
                mPeriodeTableDetail.Rows[0].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("January",langue) +" 1");
                mPeriodeTableDetail.Rows[1].SetField<string>(Translation.Translate("Beginning",langue), "16/01");
                mPeriodeTableDetail.Rows[1].SetField<string>(Translation.Translate("End",langue), "31/01");
                mPeriodeTableDetail.Rows[1].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("January",langue) +" 2");
                mPeriodeTableDetail.Rows[2].SetField<string>(Translation.Translate("Beginning",langue), "01/02");
                mPeriodeTableDetail.Rows[2].SetField<string>(Translation.Translate("End",langue), "14/02");
                mPeriodeTableDetail.Rows[2].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("February", langue) + " 1");
                mPeriodeTableDetail.Rows[3].SetField<string>(Translation.Translate("Beginning",langue), "15/02");
                mPeriodeTableDetail.Rows[3].SetField<string>(Translation.Translate("End",langue), "28/02");
                mPeriodeTableDetail.Rows[3].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("February", langue) + " 2");
                mPeriodeTableDetail.Rows[4].SetField<string>(Translation.Translate("Beginning",langue), "01/03");
                mPeriodeTableDetail.Rows[4].SetField<string>(Translation.Translate("End",langue), "15/03");
                mPeriodeTableDetail.Rows[4].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("March", langue) + " 1");
                mPeriodeTableDetail.Rows[5].SetField<string>(Translation.Translate("Beginning",langue), "16/03");
                mPeriodeTableDetail.Rows[5].SetField<string>(Translation.Translate("End",langue), "31/03");
                mPeriodeTableDetail.Rows[5].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("March", langue) + " 2");
                mPeriodeTableDetail.Rows[6].SetField<string>(Translation.Translate("Beginning",langue), "01/04");
                mPeriodeTableDetail.Rows[6].SetField<string>(Translation.Translate("End",langue), "14/04");
                mPeriodeTableDetail.Rows[6].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("April", langue) + " 1");
                mPeriodeTableDetail.Rows[7].SetField<string>(Translation.Translate("Beginning",langue), "15/04");
                mPeriodeTableDetail.Rows[7].SetField<string>(Translation.Translate("End",langue), "30/04");
                mPeriodeTableDetail.Rows[7].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("April", langue) + " 2");
                mPeriodeTableDetail.Rows[8].SetField<string>(Translation.Translate("Beginning",langue), "01/05");
                mPeriodeTableDetail.Rows[8].SetField<string>(Translation.Translate("End",langue), "15/05");
                mPeriodeTableDetail.Rows[8].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("May", langue) + " 1");
                mPeriodeTableDetail.Rows[9].SetField<string>(Translation.Translate("Beginning",langue), "16/05");
                mPeriodeTableDetail.Rows[9].SetField<string>(Translation.Translate("End",langue), "31/05");
                mPeriodeTableDetail.Rows[9].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("May", langue) + " 2");
                mPeriodeTableDetail.Rows[10].SetField<string>(Translation.Translate("Beginning",langue), "01/06");
                mPeriodeTableDetail.Rows[10].SetField<string>(Translation.Translate("End",langue), "14/06");
                mPeriodeTableDetail.Rows[10].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("June", langue) + " 1");
                mPeriodeTableDetail.Rows[11].SetField<string>(Translation.Translate("Beginning",langue), "15/06");
                mPeriodeTableDetail.Rows[11].SetField<string>(Translation.Translate("End",langue), "30/06");
                mPeriodeTableDetail.Rows[11].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("June", langue) + " 2");
                mPeriodeTableDetail.Rows[12].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("July", langue) + " 1");
                mPeriodeTableDetail.Rows[12].SetField<string>(Translation.Translate("Beginning",langue), "01/07");
                mPeriodeTableDetail.Rows[12].SetField<string>(Translation.Translate("End",langue), "15/07");
                mPeriodeTableDetail.Rows[13].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("July", langue) + " 2");
                mPeriodeTableDetail.Rows[13].SetField<string>(Translation.Translate("Beginning",langue), "16/07");
                mPeriodeTableDetail.Rows[13].SetField<string>(Translation.Translate("End",langue), "31/07");
                mPeriodeTableDetail.Rows[14].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("August", langue) + " 1");
                mPeriodeTableDetail.Rows[14].SetField<string>(Translation.Translate("Beginning",langue), "01/08");
                mPeriodeTableDetail.Rows[14].SetField<string>(Translation.Translate("End",langue), "15/08");
                mPeriodeTableDetail.Rows[15].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("August", langue) + " 2");
                mPeriodeTableDetail.Rows[15].SetField<string>(Translation.Translate("Beginning",langue), "16/08");
                mPeriodeTableDetail.Rows[15].SetField<string>(Translation.Translate("End",langue), "31/08");
                mPeriodeTableDetail.Rows[16].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("September", langue) + " 1");
                mPeriodeTableDetail.Rows[16].SetField<string>(Translation.Translate("Beginning",langue), "01/09");
                mPeriodeTableDetail.Rows[16].SetField<string>(Translation.Translate("End",langue), "14/09");
                mPeriodeTableDetail.Rows[17].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("September", langue) + " 2");
                mPeriodeTableDetail.Rows[17].SetField<string>(Translation.Translate("Beginning",langue), "15/09");
                mPeriodeTableDetail.Rows[17].SetField<string>(Translation.Translate("End",langue), "30/09");
                mPeriodeTableDetail.Rows[18].SetField<string>(Translation.Translate("Beginning",langue), "01/10");
                mPeriodeTableDetail.Rows[18].SetField<string>(Translation.Translate("End",langue), "14/10");
                mPeriodeTableDetail.Rows[18].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("October", langue) + " 1");
                mPeriodeTableDetail.Rows[19].SetField<string>(Translation.Translate("Beginning",langue), "15/10");
                mPeriodeTableDetail.Rows[19].SetField<string>(Translation.Translate("End",langue), "31/10");
                mPeriodeTableDetail.Rows[19].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("October", langue) + " 2");
                mPeriodeTableDetail.Rows[20].SetField<string>(Translation.Translate("Beginning",langue), "01/11");
                mPeriodeTableDetail.Rows[20].SetField<string>(Translation.Translate("End",langue), "15/11");
                mPeriodeTableDetail.Rows[20].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("November", langue) + " 1");
                mPeriodeTableDetail.Rows[21].SetField<string>(Translation.Translate("Beginning",langue), "16/11");
                mPeriodeTableDetail.Rows[21].SetField<string>(Translation.Translate("End",langue), "30/11");
                mPeriodeTableDetail.Rows[21].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("November", langue) + " 2");
                mPeriodeTableDetail.Rows[22].SetField<string>(Translation.Translate("Beginning",langue), "01/12");
                mPeriodeTableDetail.Rows[22].SetField<string>(Translation.Translate("End",langue), "15/12");
                mPeriodeTableDetail.Rows[22].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("December", langue) + " 1");
                mPeriodeTableDetail.Rows[23].SetField<string>(Translation.Translate("Beginning",langue), "16/12");
                mPeriodeTableDetail.Rows[23].SetField<string>(Translation.Translate("End",langue), "31/12");
                mPeriodeTableDetail.Rows[23].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("December", langue) + " 2");
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }


        }

        private void FillTemplateSemaine()
        {
            try
            {
                mPeriodeTableDetail.Clear();
                for (int i = 0; i < 52; i++)
                {
                    int numsemaine;
                    numsemaine = i + 1;
                    mPeriodeTableDetail.Rows.Add();
                    mPeriodeTableDetail.Rows[i].SetField<string>(Translation.Translate("Period",langue), "Semaine " + numsemaine);
                }
                mPeriodeTableDetail.Rows[0].SetField<string>(Translation.Translate("Beginning",langue), "01/01");
                mPeriodeTableDetail.Rows[0].SetField<string>(Translation.Translate("End",langue), "07/01");
                mPeriodeTableDetail.Rows[1].SetField<string>(Translation.Translate("Beginning",langue), "08/01");
                mPeriodeTableDetail.Rows[1].SetField<string>(Translation.Translate("End",langue), "14/01");
                mPeriodeTableDetail.Rows[2].SetField<string>(Translation.Translate("Beginning",langue), "15/01");
                mPeriodeTableDetail.Rows[2].SetField<string>(Translation.Translate("End",langue), "21/01");
                mPeriodeTableDetail.Rows[3].SetField<string>(Translation.Translate("Beginning",langue), "22/01");
                mPeriodeTableDetail.Rows[3].SetField<string>(Translation.Translate("End",langue), "28/01");
                mPeriodeTableDetail.Rows[4].SetField<string>(Translation.Translate("Beginning",langue), "29/01");
                mPeriodeTableDetail.Rows[4].SetField<string>(Translation.Translate("End",langue), "04/02");
                mPeriodeTableDetail.Rows[5].SetField<string>(Translation.Translate("Beginning",langue), "05/02");
                mPeriodeTableDetail.Rows[5].SetField<string>(Translation.Translate("End",langue), "11/02");
                mPeriodeTableDetail.Rows[6].SetField<string>(Translation.Translate("Beginning",langue), "12/02");
                mPeriodeTableDetail.Rows[6].SetField<string>(Translation.Translate("End",langue), "18/02");
                mPeriodeTableDetail.Rows[7].SetField<string>(Translation.Translate("Beginning",langue), "19/02");
                mPeriodeTableDetail.Rows[7].SetField<string>(Translation.Translate("End",langue), "25/02");
                mPeriodeTableDetail.Rows[8].SetField<string>(Translation.Translate("Beginning",langue), "26/02");
                mPeriodeTableDetail.Rows[8].SetField<string>(Translation.Translate("End",langue), "04/03");
                mPeriodeTableDetail.Rows[9].SetField<string>(Translation.Translate("Beginning",langue), "05/03");
                mPeriodeTableDetail.Rows[9].SetField<string>(Translation.Translate("End",langue), "11/03");
                mPeriodeTableDetail.Rows[10].SetField<string>(Translation.Translate("Beginning",langue), "12/03");
                mPeriodeTableDetail.Rows[10].SetField<string>(Translation.Translate("End",langue), "18/03");
                mPeriodeTableDetail.Rows[11].SetField<string>(Translation.Translate("Beginning",langue), "19/03");
                mPeriodeTableDetail.Rows[11].SetField<string>(Translation.Translate("End",langue), "25/03");
                mPeriodeTableDetail.Rows[12].SetField<string>(Translation.Translate("Beginning",langue), "26/03");
                mPeriodeTableDetail.Rows[12].SetField<string>(Translation.Translate("End",langue), "01/04");
                mPeriodeTableDetail.Rows[13].SetField<string>(Translation.Translate("Beginning",langue), "02/04");
                mPeriodeTableDetail.Rows[13].SetField<string>(Translation.Translate("End",langue), "08/04");
                mPeriodeTableDetail.Rows[14].SetField<string>(Translation.Translate("Beginning",langue), "09/04");
                mPeriodeTableDetail.Rows[14].SetField<string>(Translation.Translate("End",langue), "15/04");
                mPeriodeTableDetail.Rows[15].SetField<string>(Translation.Translate("Beginning",langue), "16/04");
                mPeriodeTableDetail.Rows[15].SetField<string>(Translation.Translate("End",langue), "22/04");
                mPeriodeTableDetail.Rows[16].SetField<string>(Translation.Translate("Beginning",langue), "23/04");
                mPeriodeTableDetail.Rows[16].SetField<string>(Translation.Translate("End",langue), "29/04");
                mPeriodeTableDetail.Rows[17].SetField<string>(Translation.Translate("Beginning",langue), "30/04");
                mPeriodeTableDetail.Rows[17].SetField<string>(Translation.Translate("End",langue), "06/05");
                mPeriodeTableDetail.Rows[18].SetField<string>(Translation.Translate("Beginning",langue), "07/05");
                mPeriodeTableDetail.Rows[18].SetField<string>(Translation.Translate("End",langue), "13/05");
                mPeriodeTableDetail.Rows[19].SetField<string>(Translation.Translate("Beginning",langue), "14/05");
                mPeriodeTableDetail.Rows[19].SetField<string>(Translation.Translate("End",langue), "20/05");
                mPeriodeTableDetail.Rows[20].SetField<string>(Translation.Translate("Beginning",langue), "21/05");
                mPeriodeTableDetail.Rows[20].SetField<string>(Translation.Translate("End",langue), "27/05");
                mPeriodeTableDetail.Rows[21].SetField<string>(Translation.Translate("Beginning",langue), "28/05");
                mPeriodeTableDetail.Rows[21].SetField<string>(Translation.Translate("End",langue), "03/06");
                mPeriodeTableDetail.Rows[22].SetField<string>(Translation.Translate("Beginning",langue), "04/06");
                mPeriodeTableDetail.Rows[22].SetField<string>(Translation.Translate("End",langue), "10/06");
                mPeriodeTableDetail.Rows[23].SetField<string>(Translation.Translate("Beginning",langue), "11/06");
                mPeriodeTableDetail.Rows[23].SetField<string>(Translation.Translate("End",langue), "17/06");
                mPeriodeTableDetail.Rows[24].SetField<string>(Translation.Translate("Beginning",langue), "18/06");
                mPeriodeTableDetail.Rows[24].SetField<string>(Translation.Translate("End",langue), "24/06");
                mPeriodeTableDetail.Rows[25].SetField<string>(Translation.Translate("Beginning",langue), "25/06");
                mPeriodeTableDetail.Rows[25].SetField<string>(Translation.Translate("End",langue), "01/07");
                mPeriodeTableDetail.Rows[26].SetField<string>(Translation.Translate("Beginning",langue), "02/07");
                mPeriodeTableDetail.Rows[26].SetField<string>(Translation.Translate("End",langue), "08/07");
                mPeriodeTableDetail.Rows[27].SetField<string>(Translation.Translate("Beginning",langue), "09/07");
                mPeriodeTableDetail.Rows[27].SetField<string>(Translation.Translate("End",langue), "15/07");
                mPeriodeTableDetail.Rows[28].SetField<string>(Translation.Translate("Beginning",langue), "16/07");
                mPeriodeTableDetail.Rows[28].SetField<string>(Translation.Translate("End",langue), "22/07");
                mPeriodeTableDetail.Rows[29].SetField<string>(Translation.Translate("Beginning",langue), "23/07");
                mPeriodeTableDetail.Rows[29].SetField<string>(Translation.Translate("End",langue), "29/07");
                mPeriodeTableDetail.Rows[30].SetField<string>(Translation.Translate("Beginning",langue), "30/07");
                mPeriodeTableDetail.Rows[30].SetField<string>(Translation.Translate("End",langue), "05/08");
                mPeriodeTableDetail.Rows[31].SetField<string>(Translation.Translate("Beginning",langue), "06/08");
                mPeriodeTableDetail.Rows[31].SetField<string>(Translation.Translate("End",langue), "12/08");
                mPeriodeTableDetail.Rows[32].SetField<string>(Translation.Translate("Beginning",langue), "13/08");
                mPeriodeTableDetail.Rows[32].SetField<string>(Translation.Translate("End",langue), "19/08");
                mPeriodeTableDetail.Rows[33].SetField<string>(Translation.Translate("Beginning",langue), "20/08");
                mPeriodeTableDetail.Rows[33].SetField<string>(Translation.Translate("End",langue), "26/08");
                mPeriodeTableDetail.Rows[34].SetField<string>(Translation.Translate("Beginning",langue), "27/08");
                mPeriodeTableDetail.Rows[34].SetField<string>(Translation.Translate("End",langue), "02/09");
                mPeriodeTableDetail.Rows[35].SetField<string>(Translation.Translate("Beginning",langue), "03/09");
                mPeriodeTableDetail.Rows[35].SetField<string>(Translation.Translate("End",langue), "09/09");
                mPeriodeTableDetail.Rows[36].SetField<string>(Translation.Translate("Beginning",langue), "10/09");
                mPeriodeTableDetail.Rows[36].SetField<string>(Translation.Translate("End",langue), "16/09");
                mPeriodeTableDetail.Rows[37].SetField<string>(Translation.Translate("Beginning",langue), "17/09");
                mPeriodeTableDetail.Rows[37].SetField<string>(Translation.Translate("End",langue), "23/09");
                mPeriodeTableDetail.Rows[38].SetField<string>(Translation.Translate("Beginning",langue), "24/09");
                mPeriodeTableDetail.Rows[38].SetField<string>(Translation.Translate("End",langue), "30/09");
                mPeriodeTableDetail.Rows[39].SetField<string>(Translation.Translate("Beginning",langue), "01/10");
                mPeriodeTableDetail.Rows[39].SetField<string>(Translation.Translate("End",langue), "07/10");
                mPeriodeTableDetail.Rows[40].SetField<string>(Translation.Translate("Beginning",langue), "08/10");
                mPeriodeTableDetail.Rows[40].SetField<string>(Translation.Translate("End",langue), "14/10");
                mPeriodeTableDetail.Rows[41].SetField<string>(Translation.Translate("Beginning",langue), "15/10");
                mPeriodeTableDetail.Rows[41].SetField<string>(Translation.Translate("End",langue), "21/10");
                mPeriodeTableDetail.Rows[42].SetField<string>(Translation.Translate("Beginning",langue), "22/10");
                mPeriodeTableDetail.Rows[42].SetField<string>(Translation.Translate("End",langue), "28/10");
                mPeriodeTableDetail.Rows[43].SetField<string>(Translation.Translate("Beginning",langue), "29/10");
                mPeriodeTableDetail.Rows[43].SetField<string>(Translation.Translate("End",langue), "04/11");
                mPeriodeTableDetail.Rows[44].SetField<string>(Translation.Translate("Beginning",langue), "05/11");
                mPeriodeTableDetail.Rows[44].SetField<string>(Translation.Translate("End",langue), "11/11");
                mPeriodeTableDetail.Rows[45].SetField<string>(Translation.Translate("Beginning",langue), "12/11");
                mPeriodeTableDetail.Rows[45].SetField<string>(Translation.Translate("End",langue), "18/11");
                mPeriodeTableDetail.Rows[46].SetField<string>(Translation.Translate("Beginning",langue), "19/11");
                mPeriodeTableDetail.Rows[46].SetField<string>(Translation.Translate("End",langue), "25/11");
                mPeriodeTableDetail.Rows[47].SetField<string>(Translation.Translate("Beginning",langue), "26/11");
                mPeriodeTableDetail.Rows[47].SetField<string>(Translation.Translate("End",langue), "02/12");
                mPeriodeTableDetail.Rows[48].SetField<string>(Translation.Translate("Beginning",langue), "03/12");
                mPeriodeTableDetail.Rows[48].SetField<string>(Translation.Translate("End",langue), "09/12");
                mPeriodeTableDetail.Rows[49].SetField<string>(Translation.Translate("Beginning",langue), "10/12");
                mPeriodeTableDetail.Rows[49].SetField<string>(Translation.Translate("End",langue), "16/12");
                mPeriodeTableDetail.Rows[50].SetField<string>(Translation.Translate("Beginning",langue), "17/12");
                mPeriodeTableDetail.Rows[50].SetField<string>(Translation.Translate("End",langue), "23/12");
                mPeriodeTableDetail.Rows[51].SetField<string>(Translation.Translate("Beginning",langue), "24/12");
                mPeriodeTableDetail.Rows[51].SetField<string>(Translation.Translate("End",langue), "31/12");
               
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void FillTemplateMois()
        {
            try
            {
                mPeriodeTableDetail.Clear();
                for (int i = 0; i < 12; i++)
                {
                    mPeriodeTableDetail.Rows.Add();
                }
                mPeriodeTableDetail.Rows[0].SetField<string>(Translation.Translate("Beginning",langue), "01/01");
                mPeriodeTableDetail.Rows[0].SetField<string>(Translation.Translate("End",langue), "31/01");
                mPeriodeTableDetail.Rows[0].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("January", langue));
                mPeriodeTableDetail.Rows[1].SetField<string>(Translation.Translate("Beginning",langue), "01/02");
                mPeriodeTableDetail.Rows[1].SetField<string>(Translation.Translate("End",langue), "28/02");
                mPeriodeTableDetail.Rows[1].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("February", langue));
                mPeriodeTableDetail.Rows[2].SetField<string>(Translation.Translate("Beginning",langue), "01/03");
                mPeriodeTableDetail.Rows[2].SetField<string>(Translation.Translate("End",langue), "31/03");
                mPeriodeTableDetail.Rows[2].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("March", langue));
                mPeriodeTableDetail.Rows[3].SetField<string>(Translation.Translate("Beginning",langue), "01/04");
                mPeriodeTableDetail.Rows[3].SetField<string>(Translation.Translate("End",langue), "30/04");
                mPeriodeTableDetail.Rows[3].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("April", langue));
                mPeriodeTableDetail.Rows[4].SetField<string>(Translation.Translate("Beginning",langue), "01/05");
                mPeriodeTableDetail.Rows[4].SetField<string>(Translation.Translate("End",langue), "31/05");
                mPeriodeTableDetail.Rows[4].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("May", langue));
                mPeriodeTableDetail.Rows[5].SetField<string>(Translation.Translate("Beginning",langue), "01/06");
                mPeriodeTableDetail.Rows[5].SetField<string>(Translation.Translate("End",langue), "30/06");
                mPeriodeTableDetail.Rows[5].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("June", langue));
                mPeriodeTableDetail.Rows[6].SetField<string>(Translation.Translate("Beginning",langue), "01/07");
                mPeriodeTableDetail.Rows[6].SetField<string>(Translation.Translate("End",langue), "31/07");
                mPeriodeTableDetail.Rows[6].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("July", langue));
                mPeriodeTableDetail.Rows[7].SetField<string>(Translation.Translate("Beginning",langue), "01/08");
                mPeriodeTableDetail.Rows[7].SetField<string>(Translation.Translate("End",langue), "31/08");
                mPeriodeTableDetail.Rows[7].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("August", langue));
                mPeriodeTableDetail.Rows[8].SetField<string>(Translation.Translate("Beginning",langue), "01/09");
                mPeriodeTableDetail.Rows[8].SetField<string>(Translation.Translate("End",langue), "30/09");
                mPeriodeTableDetail.Rows[8].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("September", langue));
                mPeriodeTableDetail.Rows[9].SetField<string>(Translation.Translate("Beginning",langue), "01/10");
                mPeriodeTableDetail.Rows[9].SetField<string>(Translation.Translate("End",langue), "31/10");
                mPeriodeTableDetail.Rows[9].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("October", langue));
                mPeriodeTableDetail.Rows[10].SetField<string>(Translation.Translate("Beginning",langue), "01/11");
                mPeriodeTableDetail.Rows[10].SetField<string>(Translation.Translate("End",langue), "30/11");
                mPeriodeTableDetail.Rows[10].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("November", langue));
                mPeriodeTableDetail.Rows[11].SetField<string>(Translation.Translate("Beginning",langue), "01/12");
                mPeriodeTableDetail.Rows[11].SetField<string>(Translation.Translate("End",langue), "31/12");
                mPeriodeTableDetail.Rows[11].SetField<string>(Translation.Translate("Period",langue), Translation.Translate("December", langue));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message); Log.WriteLog(Ex.Message, GetType().Name + " . Function :  "  + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}
 #endregion

    

