using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using OMEGA.Data_Classes;
using OMEGA.SQLQuery;
using OMEGA.Graphs;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System.Data.SQLite;
using System.Drawing.Drawing2D;
using System.Windows.Forms.DataVisualization.Charting;
using OMEGA.Other_Classes;

namespace OMEGA.Forms.ResultUserControl
{
    internal partial class UserControlResultat : UserControl
    {

        private ShowCommonFormResult showCommunFormResult;
        private int langue = Properties.Settings.Default.Langue;
        private int currentIdPeriode;
        private int IdExp = Commun.GetIdExpl();
        private ResultForm resultform;
        /// <summary>
        /// Load the tab result for the current Farm
        /// </summary>
        internal UserControlResultat(ResultForm form)
        {
            try
            {
                InitializeComponent();
                showCommunFormResult = new ShowCommonFormResult(null, null, "Result");
                SubscribeToEvent();
                HidePanel();
                LoadCurrency();
                resultform = form;
                Results.Text = Translation.Translate("Results", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void HidePanel()
        {
            panelGraphique.Visible = false;
            panelStandard.Visible = false;
            panelCalendars.Visible = false;
        }

        private void SubscribeToEvent()
        {
            try
            {
                buttonStandard.Click += LoadStandardPanel;
                buttonGraphique.Click += LoadGraphicPanel;
                buttonDico.Click += showCommunFormResult.buttonDicoClick;
                dataGridViewListeCalendar.CellClick += DataGridViewCalendarCellClick;
                // buttontotal.Click += ButtonTotalClick;
                buttongraphe.Click += ButtonGrapheClick;
                buttonValues.Click += ButtonValueClick;
                buttonCalendar.Click += LoadCalendarPanel;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        
        private void LoadCurrency()
        {
            string query = "Select UEnt From SystemeUnite Where Monnaie = 'Oui'; ";
            List<string> list = SQlQueryExecuter.RunQueryReaderStr("UEnt", query);
            if (Commun.ListHasValue(list))
            {
                textBoxCurrency.Text = list[0];
            }
            textBoxCurrency.ReadOnly = true;
        }

        private void loadYear()
        {
            int first;
            string query = SQLQueryBuilder.SelectQuery("Agri_DefSim", "An_0");
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
            if (Commun.ListHasValue(list))
            {
                first = list[0];
            }
            else first = DateTime.Now.Year;

            List<int> ListeYear = new List<int>();

            for (int i = 1; i <= 10; i++)
            {
                ListeYear.Add(first);
                first++;
            }

            comboBoxyear.DataSource = ListeYear;
        }


        #region Travail

        private void LoadCalendarPanel(object sender, EventArgs e)
        {
            try
            {
                panelGraphique.Visible = false; ;
                panelStandard.Visible = false;
                panelCalendars.Visible = true;


                Point point = new Point(113, 39);
                panelCalendars.Location = point;

                DataTable table = new DataTable();
                string mainQuery = PeriodeQuery.MainQuery(0);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainQuery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(table);
                dataGridViewListeCalendar.DataSource = table;
                dataGridViewListeCalendar.AllowUserToAddRows = false;
                dataGridViewListeCalendar.RowHeadersVisible = false;
                dataGridViewListeCalendar.Columns[0].Visible = false;

                loadYear();

            }
            catch (Exception Ex)
            {

            }
        }

        private void DataGridViewCalendarCellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0)
                {
                    int.TryParse(dataGridViewListeCalendar.Rows[e.RowIndex].Cells[0].Value.ToString(), out currentIdPeriode);
                }
            }
            catch (Exception Ex)
            {

            }
        }

        private void ButtonGrapheClick(object sender, EventArgs e)
        {
            try
            {
                buttonExport.Visible = false;
                chart1.Visible = true;
                panelgraphecalendar.Visible = true;
                panelGridCalendar.Visible = false;
                Point point = new Point(219, 3);
                panelgraphecalendar.Location = point;
                Size size = new Size(696, 385);
                panelgraphecalendar.Size = size;
                chart1.Size = size;
                int index = 0;
                int nbPoint = 0;
                chart1.Series.Clear();

                Dictionary<string, List<double>> dicoTravailleurActivite = new Dictionary<string, List<double>>();

                List<int> listActivite = GetActivityOfExploitation(IdExp);
               
                dicoTravailleurActivite = CalculValeurDico(listActivite);

                if (dicoTravailleurActivite == null) return;
                
                foreach (KeyValuePair<string, List<double>> item in dicoTravailleurActivite)
                {
                    chart1.Series.Add(item.Key);
                    chart1.Series[index].ChartType = SeriesChartType.StackedColumn;
                    foreach (double value in item.Value)
                    {
                        chart1.Series[index].Points.Add(value);
                        if (index == 0) nbPoint++; 
                    }
                    index++;
                }

                chart1.Series.Add("Nb travailleur");
                chart1.Series[index].ChartType = SeriesChartType.Line;
                chart1.Series[index].BorderWidth = 7;
                chart1.Series[index].Color = Color.Red;
                double.TryParse(textBoxnbworker.Text, out double nbtravailleur);
                for (int j = 0;j< nbPoint; j++)
                {
                    chart1.Series[index].Points.Add(nbtravailleur);
                }
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private Dictionary<string, List<double>> CalculValeurDico(List<int> listActivite)
        {
            Dictionary<string, List<double>> dicoTravailleurActivite = new Dictionary<string, List<double>>();
            try
            {
                Dictionary<string, List<double>> dicoActivite = new Dictionary<string, List<double>>();
                List<string> listPeriode = GetListePeriode();
                List<double> listQty = new List<double>();
                string query = "";
                int index = 0;
                double total = 0;
                double.TryParse(comboBoxyear.Text, out double annee);
                
                if (listActivite.Count == 0)
                {
                    MessageBox.Show(Translation.Translate("This working farm has no activity on this calendar, no graph to load", langue));
                    return null;
                }

                foreach (int activite in listActivite)
                {
                    query = "Select Nom From Activite Where ID ='" + activite + "';";
                    string nom = SQlQueryExecuter.RunQueryReaderStr("Nom", query)[0];
                    dicoActivite.Add(nom, new List<double>());
                    int Idtype = SQLQueryBuilder.FindId("Activite", "IdType", "Nom", nom);
                    query = "Select * From Travail Where IdPeriode = '" + currentIdPeriode + "' and IdActivite = '" + activite + "';";
                    // ne prend que si valeur dans 1er phase pour perenne et plurianuelle, faire changement si année différente.
                    listQty = SQlQueryExecuter.RunQueryReaderDouble("Qte_1_T", query);
                    dicoActivite[nom] = listQty;
                }


                List<double> listeTotal = new List<double>();
                index = 0;
                while (index <= listQty.Count - 1)
                {
                    foreach (KeyValuePair<string, List<double>> item in dicoActivite)
                    {
                        if (item.Value.Count>index) total = item.Value[index] + total;
                    }
                    index++;
                    listeTotal.Add(total);
                    total = 0;
                }

                List<string> listNom = SQlQueryExecuter.RunQueryReaderStr("Nom_Periode", query);
                query = "Select Heures_Utilisees From Def_Calendrier Where IdPeriode = '" + currentIdPeriode + "'; ";
                List<double> listH = SQlQueryExecuter.RunQueryReaderDouble("Heures_Utilisees", query);
                List<double> listTravailleurT = new List<double>();

                foreach (double item in listH)
                {
                    listTravailleurT.Add(listeTotal[listH.IndexOf(item)] / item);
                }

                Dictionary<string, List<double>> dicoTravailleurActivitePourcent = new Dictionary<string, List<double>>();
                foreach (KeyValuePair<string, List<double>> item in dicoActivite)
                {
                    dicoTravailleurActivite.Add(item.Key, new List<double>());
                    foreach (double value in item.Value)
                    {
                        dicoTravailleurActivite[item.Key].Add(value / listH[item.Value.IndexOf(value)]);
                    }
                }

                index = 0;
                Dictionary<string, List<double>> dicoSommeTravailleurActivite = new Dictionary<string, List<double>>();


                for (int i = 0; i < listTravailleurT.Count; i++)
                {
                    foreach (KeyValuePair<string, List<double>> item in dicoTravailleurActivitePourcent)
                    {
                        dicoTravailleurActivite[item.Key][i] = (dicoTravailleurActivitePourcent[item.Key][i] * listTravailleurT[i] / 100);
                    }
                }
                List<double> listSommeTravailleurActivite = new List<double>();
                double temp = 0;
                for (int i = 0; i < listTravailleurT.Count; i++)
                {
                    foreach (KeyValuePair<string, List<double>> item in dicoTravailleurActivite)
                    {
                        temp = item.Value[i] + temp;
                    }
                    listSommeTravailleurActivite.Add(temp);
                    temp = 0;
                }

                foreach (KeyValuePair<string, List<double>> item in dicoActivite)
                {
                    dicoTravailleurActivitePourcent.Add(item.Key, new List<double>());
                    foreach (double value in item.Value)
                    {
                        dicoTravailleurActivitePourcent[item.Key].Add(((value / listH[item.Value.IndexOf(value)]) * 100) / listSommeTravailleurActivite[item.Value.IndexOf(value)]);
                    }
                }

                for (int i = 0; i < listTravailleurT.Count; i++)
                {
                    foreach (KeyValuePair<string, List<double>> item in dicoTravailleurActivitePourcent)
                    {
                        dicoTravailleurActivite[item.Key][i] = (dicoTravailleurActivitePourcent[item.Key][i] * listTravailleurT[i] / 100);
                    }
                }

                index = 0;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return dicoTravailleurActivite;
        }

        private List<string> GetListePeriode()
        {
            string query = "Select Nom_Periode From Travail Where IdPeriode ='" + currentIdPeriode + "';";
            return SQlQueryExecuter.RunQueryReaderStr("Nom_Periode", query);
        }

        private void ButtonValueClick(object sender, EventArgs e)
        {
            try
            {
                panelgraphecalendar.Visible = false;
                panelGridCalendar.Visible = true;
                dataGridViewValues.Visible = true;
                Point point = new Point(219, 3);
                panelGridCalendar.Location = point;
                buttonExport.Visible = true;
                Size size = new Size(696, 385);
                panelGridCalendar.Size = size;
                DataTable table = new DataTable();
                int IdExpl = GetIdExploitation();
                List<int> listActivite = GetActivityOfExploitation(IdExpl);

                SetGridView(listActivite, table);

                table = LoadGrid(listActivite, table);

                table = AddLineBesoin(table);

                table = AddLineHeure(table);

                table = AddLineTravailleurN(table);

                table = AddLineTravailleurD(table);

                table = AddLineDispo(table);

                table = AddLineHeureManqueEtPlus(table);

                dataGridViewValues.DataSource = table;
                dataGridViewValues.AllowUserToAddRows = false;
                dataGridViewValues.ReadOnly = true;
                dataGridViewValues.Font = Commun.GetCurrentFont();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private DataTable AddLineHeureManqueEtPlus(DataTable table)
        {
            try
            {
                int indexlastrow = table.Rows.Count;
                string[] HeureManquanteValue = new string[table.Columns.Count];
                string[] HeureExcedValue = new string[table.Columns.Count];
                double HeureManquante = 0;
                for (int i = 0; i <= table.Columns.Count - 1; i++)
                {
                    if (i == 0)
                    {
                        HeureManquanteValue[i] = "Heures manquantes";
                        HeureExcedValue[i] = "Heures excedentaires";
                    }
                    else if (i == 1)
                    {
                        HeureManquanteValue[i] = "";
                        HeureExcedValue[i] = "";
                    }
                    else
                    {
                        double BesoinTravail;
                        double HeureDispo;
                        double.TryParse(table.Rows[indexlastrow - 5].ItemArray[i].ToString(), out BesoinTravail);
                        double.TryParse(table.Rows[indexlastrow-1].ItemArray[i].ToString(), out  HeureDispo);
                        HeureManquante = BesoinTravail - HeureDispo;
                        if (HeureManquante >0)
                        {
                            HeureManquanteValue[i] = HeureManquante.ToString();
                        }
                        else
                        {
                            HeureManquante = Math.Abs(HeureManquante);
                            HeureExcedValue[i] = HeureManquante.ToString();
                        }
                    }
                }
                table.Rows.Add(HeureManquanteValue);
                table.Rows.Add(HeureExcedValue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return table;
        }

        private DataTable AddLineDispo(DataTable table)
        {
            try
            {
                int indexlastrow = table.Rows.Count;
                string[] value = new string[table.Columns.Count];
                double heuredispo = 0;
                for (int i = 0; i <= table.Columns.Count - 1; i++)
                {
                    if (i == 0) value[i] = "Heures Disponibles";
                    else if (i == 1) value[i] = "";
                    else
                    {
                        double dispoParTravailleur;
                        double TravailleurDispo;
                        double.TryParse(table.Rows[indexlastrow - 3].ItemArray[i].ToString(), out  dispoParTravailleur);
                        double.TryParse(table.Rows[indexlastrow-1 ].ItemArray[i].ToString(), out  TravailleurDispo);
                        heuredispo = dispoParTravailleur * TravailleurDispo;
                        heuredispo = Math.Round(heuredispo, 2);
                        value[i] = heuredispo.ToString();
                    }
                }
                table.Rows.Add(value);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return table;
        }

        private DataTable AddLineTravailleurD(DataTable table)
        {
           try
           {
                string nb = textBoxnbworker.Text;
                string[] value = new string[table.Columns.Count];
                for (int i=0;i<=table.Columns.Count-1;i++)
                {
                    if (i == 0) value[i] = "Travailleurs Disponibles";
                    else if (i == 1) value[i] = "";
                    else value[i] = nb;
                }
                table.Rows.Add(value);

           }
            catch (Exception Ex)
           {
                MessageBox.Show(Ex.Message);
           }
            return table;
        }

        private DataTable AddLineTravailleurN(DataTable table)
        {
            try
            {
                int indexlastrow = table.Rows.Count ;
                string[] value = new string[table.Columns.Count];
                double travailleurNec = 0;
                for (int i = 0; i <= table.Columns.Count - 1; i++)
                {
                    if (i == 0) value[i] = "Travailleurs Nécessaire";
                    else if (i == 1) value[i] = "";
                    else
                    {
                        double besoinTravail;
                        double dispoTravailleur;
                        double.TryParse(table.Rows[indexlastrow - 2].ItemArray[i].ToString(), out  besoinTravail);
                        double.TryParse(table.Rows[indexlastrow - 1].ItemArray[i].ToString(), out  dispoTravailleur);
                        if (dispoTravailleur > 0)
                        {
                            travailleurNec = besoinTravail / dispoTravailleur;
                        }
                        travailleurNec = Math.Round(travailleurNec, 2);
                        value[i] = travailleurNec.ToString();
                    }
                }
                table.Rows.Add(value);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return table;
        }

        private DataTable AddLineHeure(DataTable table)
        {
            try
            { 
                string query ="Select Heures_Utilisees From Def_Calendrier Where IdPeriode = '" + currentIdPeriode + "'; ";
                List<double> list = SQlQueryExecuter.RunQueryReaderDouble("Heures_Utilisees", query);
                string[] value = new string[list.Count + 2];
                for (int i = 0; i <= list.Count+1; i++)
                {
                    if (i == 0) value[i] = "Dispo par travailleur";
                    else if (i == 1) value[i] = "";
                    else value[i] = list[i - 2].ToString();
                }
                table.Rows.Add(value);
                }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return table;
        }

        private DataTable AddLineBesoin(DataTable table)
        {
            try
            {
                table.Rows.Add();
                List<double> list = new List<double>();
                double temp = 0;
                double temp2 = 0;
                int columnindex = 0;
                foreach (DataColumn column in table.Columns)
                {
                    temp2 = 0;
                    if (column.ColumnName.Length > 1)
                    {
                        foreach (DataRow row in table.Rows)
                        {
                            double.TryParse(row.ItemArray[columnindex].ToString(), out temp);
                            temp2 = temp2 + temp;
                        }
                        list.Add(temp2);
                    }
                    columnindex++;
                }

                string[] value = new string[list.Count + 1];
                for (int i = 0; i <= list.Count; i++)
                {
                    if (i == 0) value[i] = "Besoin en travail";
                    else value[i] = list[i - 1].ToString();
                }
                table.Rows.Add(value);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            
            return table;
        }

        private void SetGridView(List<int> listAct,DataTable table)
        {
           try
            {
                table.Columns.Add(" ");
                table.Columns.Add("Total");
                string query = " select Per_Act From Def_Calendrier Where IdPeriode = '" + currentIdPeriode + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Per_Act", query);
                foreach (string item in list)
                {
                    table.Columns.Add(item);
                }

                foreach (int id in listAct)
                {
                    table.Rows.Add();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }


        }

        private int GetIdExploitation()
        {
            string query = " Select ID From Exploitation where Encours = '1';";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
            if (Commun.ListHasValue(list))
            {
                return list[0];
            }
            else
                return 0;
        }

        private List<int> GetActivityOfExploitation(int idExpl)
        {
     
            string query = "Select distinct Travail.IdPeriode,Travail.IdActivite From Travail " +
                    " Join Caract_Exploitation on Caract_Exploitation.IdActivite = Travail.IdActivite "+
                    "Where Travail.IdPeriode = '" + currentIdPeriode +
                    "'And  Caract_Exploitation.IdExploitation = '" + idExpl + "'; ";
             List<int> list2 = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);
                 
            return list2;

        }

        private DataTable LoadGrid (List<int> listActivite,DataTable table)
        {
            try
            {
                int index = 0;
                int rowIndex = 0;
                double total=0;
                foreach (int id in listActivite)
                {
                    string query = "Select * From Travail Where IdActivite = '" + id + "' and IdPeriode = '" + currentIdPeriode + "';";
                    List<double> listQty = SQlQueryExecuter.RunQueryReaderDouble("Qte_1_T", query);
                    List<string> listNom = SQlQueryExecuter.RunQueryReaderStr("Nom_Periode", query);
                    index = 0;
                    total = 0;
                    foreach (double item in listQty)
                    {
                        total = item + total;
                        table.Rows[rowIndex].SetField<double>(listNom[index], item);
                        index++;
                    }
                    string query2 = "Select Nom From Activite Where ID = '" + id + "';";
                    List<string> listActiviteName = SQlQueryExecuter.RunQueryReaderStr("Nom", query2);

                    if (Commun.ListHasValue(listActiviteName)) table.Rows[rowIndex].SetField<string>(" ", listActiviteName[0]);
                    table.Rows[rowIndex].SetField<double>("Total", total);
                    rowIndex++;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
          
            return table;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (!panelCalendars.Visible)
            {
                switch (keyData)
                {
                    case Keys.EraseEof:
                        return false;
                    default:
                        return true;
                }
            }
            else return false;
        }

        #endregion

        
        #region rapport
        private void LoadStandardPanel(object sender, EventArgs e)
        {
            try
            {
                panelGraphique.Visible = false;
                panelStandard.Visible = true;
                panelCalendars.Visible = false;
                buttonExport.Visible = false;
                int langue = Properties.Settings.Default.Langue;

                DataTable table = new DataTable();
                table.Columns.Add("Nom");
                table.Rows.Add(new object[] { Translation.Translate("Synthesis", langue) });//0
                table.Rows.Add(new object[] { Translation.Translate("Receipts/Expenses", langue) });//1
                table.Rows.Add(new object[] { Translation.Translate("Rec Exp Sumary", langue) });//2
                table.Rows.Add(new object[] { Translation.Translate("Quantities", langue) });//3
                table.Rows.Add(new object[] { Translation.Translate("Farm Account", langue) });//4
                table.Rows.Add(new object[] { Translation.Translate("Balance", langue) });//5
                table.Rows.Add(new object[] { Translation.Translate("Family", langue) });//6
                table.Rows.Add(new object[] { Translation.Translate("Margin", langue) });//7
                table.Rows.Add(new object[] { Translation.Translate("Externalities", langue) });//8
                table.Rows.Add(new object[] { Translation.Translate("Output Forms", langue) });//9
                table.Rows.Add(new object[] { Translation.Translate("Finance", langue) });//10
                table.Rows.Add(new object[] { Translation.Translate("Fixed Assets", langue) });//11
                table.Rows.Add(new object[] { Translation.Translate("VAT", langue) });//12
                table.Rows.Add(new object[] { Translation.Translate("Area", langue) });//13
                table.Rows.Add(new object[] { Translation.Translate("Tree Crops account", langue) });//14
                table.Rows.Add(new object[] { Translation.Translate("Stocks", langue) });//15
                table.Rows.Add(new object[] { Translation.Translate("Long term", langue) });//16
                table.Rows.Add(new object[] { Translation.Translate("Fixed Assets", langue) });//17
                table.Rows.Add(new object[] { Translation.Translate("Used of", langue) });//18

                dataGridViewStd.DataSource = table;
                dataGridViewStd.AllowUserToAddRows = false;
                dataGridViewStd.RowHeadersVisible = false;
                dataGridViewStd.ColumnHeadersVisible = false;
                dataGridViewStd.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dataGridViewStd.RowTemplate.Height = 18;
                dataGridViewStd.Columns[0].Width = 175;
                dataGridViewStd.AllowUserToResizeColumns = false;
                dataGridViewStd.AllowUserToResizeRows = false;
                dataGridViewStd.AllowUserToDeleteRows = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void dataGridViewStd_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == 1)
                {
                    StandardForm form = new StandardForm(0, "RecetteDepense");
                    form.ShowDialog();
                }
                if (e.RowIndex == 2)
                {
                    StandardForm form = new StandardForm(0, "Grand Poste");
                    form.ShowDialog();
                }
                if (e.RowIndex == 3)
                {
                    StandardForm form = new StandardForm(0, "quantite");
                    form.ShowDialog();
                }
                if (e.RowIndex == 4)
                {
                    StandardForm form = new StandardForm(0, "CEG");
                    form.ShowDialog();
                }
                if (e.RowIndex == 7)
                {
                    ActivityChoiceForm form = new ActivityChoiceForm();
                    form.ShowDialog();
                }
                if (e.RowIndex == 10)
                {
                    StandardForm form = new StandardForm(0, "Immo");
                    form.ShowDialog();
                }
                if (e.RowIndex == 13)
                {
                    StandardForm form = new StandardForm(0, "surface");
                    form.ShowDialog();
                }
                if (e.RowIndex == 18)
                {
                    Form form = new ChargeUsedForm();
                    form.ShowDialog();
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            
        }

        #endregion


        #region Graphe
        private void LoadGraphicPanel(object sender, EventArgs e)
        {
            try
            {
                panelGraphique.Visible = true; ;
                panelStandard.Visible = false;
                panelCalendars.Visible = false;
                buttonExport.Visible = false;
                Point point = new Point(113, 39);
                panelGraphique.Location = point;

                LoadGraphicGrid();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadGraphicGrid()
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;

                DataTable table = new DataTable();
                table.Columns.Add("Nom");
                table.Rows.Add(new object[] { Translation.Translate("Balance", langue) });//0
                table.Rows.Add(new object[] { Translation.Translate("Added Balance", langue) });//1
                table.Rows.Add(new object[] { Translation.Translate("Total product Income", langue) });//2
                table.Rows.Add(new object[] { Translation.Translate("Total structural cost", langue) });//3
                table.Rows.Add(new object[] { Translation.Translate("Total Misc expenses", langue) });//4
                table.Rows.Add(new object[] { Translation.Translate("Total Misc incomes", langue) });//5
                table.Rows.Add(new object[] { Translation.Translate("Total Family expenses", langue) });//6
                table.Rows.Add(new object[] { Translation.Translate("Total Family incomes", langue) });//7
                table.Rows.Add(new object[] { Translation.Translate("Margin", langue) });//8

                dataGridView2.DataSource = table;
                dataGridView2.AllowUserToAddRows = false;
                dataGridView2.RowHeadersVisible = false;
                dataGridView2.ColumnHeadersVisible = false;
                dataGridView2.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dataGridView2.RowTemplate.Height = 18;
                dataGridView2.Columns[0].Width = 175;
                dataGridView2.AllowUserToResizeColumns = false;
                dataGridView2.AllowUserToResizeRows = false;
                dataGridView2.AllowUserToDeleteRows = false;
            }
             catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex == 0)
                {
                    DataTable table = Calcul.GetSolde();
                    Dictionary<string, List<double>> dico = new Dictionary<string, List<double>>();
                    List<double> list = new List<double>();
                    dico.Add("Solde", new List<double>());
                    for (int nbrow = table.Rows.Count-1; nbrow >= 0; nbrow--)
                    {
                        for (int numCol = 1; numCol < 11; numCol++)
                        {
                            double.TryParse(table.Rows[nbrow].ItemArray[numCol].ToString(), out double value);
                            list.Add(value);
                        }
                        break;
                    }
                    dico["Solde"] = list;
                    GraphForm form = new GraphForm(dico, "line","solde");
                    form.ShowDialog();
                }
                if (e.RowIndex == 1)
                {
                    DataTable table = Calcul.GetSoldCumule();
                    Dictionary<string, List<double>> dico = new Dictionary<string, List<double>>();
                    List<double> list = new List<double>();
                    dico.Add("Solde Cumule", new List<double>());
                    for (int nbrow = table.Rows.Count-1; nbrow >= 0; nbrow--)
                    {
                        for (int numCol = 1; numCol < 11; numCol++)
                        {
                            double.TryParse(table.Rows[nbrow].ItemArray[numCol].ToString(), out double value);
                            list.Add(value);
                        }
                        break;
                    }
                    dico["Solde Cumule"] = list;
                    GraphForm form = new GraphForm(dico, "spline","solde cumulé");
                    form.ShowDialog();
                }
                if (e.RowIndex == 2)
                {
                    DataTable table = Calcul.GetTotalProduit();
                    Dictionary<string, List<double>> dico = new Dictionary<string, List<double>>();
                    List<double> list = new List<double>();
                    List<double> listTotal = new List<double>();
                    List<List<double>> listList = new List<List<double>>();
                    dico.Add("Total Produit", new List<double>());
                    for (int nbrow = table.Rows.Count - 1; nbrow >= 0; nbrow--)
                    {
                        list = new List<double>();
                        for (int numCol = 1; numCol < 11; numCol++)
                        {
                            double.TryParse(table.Rows[nbrow].ItemArray[numCol].ToString(), out double value);
                            list.Add(value);
                        }
                        listList.Add(list);
                    }
                    double temp = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        temp = 0;
                        foreach (List<double> templist in listList)
                        {
                            temp = templist[i] + temp;
                        }
                        listTotal.Add(temp);
                    }

                    dico["Total Produit"] = listTotal;
                    GraphForm form = new GraphForm(dico, "spline","Total produit");
                    form.ShowDialog();
                }
                if (e.RowIndex == 3)
                {
                    DataTable table = Calcul.GetTotalChargeStructure();
                    Dictionary<string, List<double>> dico = new Dictionary<string, List<double>>();
                    List<double> list = new List<double>();
                    List<double> listTotal = new List<double>();
                    List<List<double>> listList = new List<List<double>>();
                    dico.Add("Total Charge Structure", new List<double>());
                    for (int nbrow = table.Rows.Count-1; nbrow >= 0; nbrow--)
                    {
                        list = new List<double>();
                        for (int numCol = 1; numCol < 11; numCol++)
                        {
                            double.TryParse(table.Rows[nbrow].ItemArray[numCol].ToString(), out double value);
                            list.Add(value);
                        }
                        listList.Add(list);
                    }
                    double temp = 0;
                    for (int i = 0;i<list.Count;i++)
                    {
                        temp = 0;
                        foreach (List<double> templist in listList)
                        {
                            temp = templist[i] + temp;
                        }
                        listTotal.Add(temp);
                    }
                    
                    dico["Total Charge Structure"] = listTotal;
                    GraphForm form = new GraphForm(dico, "spline");
                    form.ShowDialog();
                }
                if (e.RowIndex == 4)
                {
                    DataTable table = Calcul.GetTotalExt_Inc(0,1);
                    Dictionary<string, List<double>> dico = new Dictionary<string, List<double>>();
                    List<double> list = new List<double>();
                    List<double> listTotal = new List<double>();
                    List<List<double>> listList = new List<List<double>>();
                    dico.Add("Total Dépense diverse", new List<double>());
                    for (int nbrow = table.Rows.Count - 1; nbrow >= 0; nbrow--)
                    {
                        list = new List<double>();
                        for (int numCol = 1; numCol < 11; numCol++)
                        {
                            double.TryParse(table.Rows[nbrow].ItemArray[numCol].ToString(), out double value);
                            list.Add(value);
                        }
                        listList.Add(list);
                    }
                    double temp = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        temp = 0;
                        foreach (List<double> templist in listList)
                        {
                            temp = templist[i] + temp;
                        }
                        listTotal.Add(temp);
                    }

                    dico["Total Dépense diverse"] = listTotal;
                    GraphForm form = new GraphForm(dico, "spline","Total dépense diverse");
                    form.ShowDialog();
                }
                if (e.RowIndex == 5)
                {
                    DataTable table = Calcul.GetTotalExt_Inc(0, 0);
                    Dictionary<string, List<double>> dico = new Dictionary<string, List<double>>();
                    List<double> list = new List<double>();
                    List<double> listTotal = new List<double>();
                    List<List<double>> listList = new List<List<double>>();
                    dico.Add("Total Recette diverse", new List<double>());
                    for (int nbrow = table.Rows.Count - 1; nbrow >= 0; nbrow--)
                    {
                        list = new List<double>();
                        for (int numCol = 1; numCol < 11; numCol++)
                        {
                            double.TryParse(table.Rows[nbrow].ItemArray[numCol].ToString(), out double value);
                            list.Add(value);
                        }
                        listList.Add(list);
                    }
                    double temp = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        temp = 0;
                        foreach (List<double> templist in listList)
                        {
                            temp = templist[i] + temp;
                        }
                        listTotal.Add(temp);
                    }

                    dico["Total Recette diverse"] = listTotal;
                    GraphForm form = new GraphForm(dico, "spline", "Total recette diverse");
                    form.ShowDialog();
                }
                if (e.RowIndex == 6)
                {
                    DataTable table = Calcul.GetTotalExt_Inc(1, 1);
                    Dictionary<string, List<double>> dico = new Dictionary<string, List<double>>();
                    List<double> list = new List<double>();
                    List<double> listTotal = new List<double>();
                    List<List<double>> listList = new List<List<double>>();
                    dico.Add("Total Dépense Famille", new List<double>());
                    for (int nbrow = table.Rows.Count - 1; nbrow >= 0; nbrow--)
                    {
                        list = new List<double>();
                        for (int numCol = 1; numCol < 11; numCol++)
                        {
                            double.TryParse(table.Rows[nbrow].ItemArray[numCol].ToString(), out double value);
                            list.Add(value);
                        }
                        listList.Add(list);
                    }
                    double temp = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        temp = 0;
                        foreach (List<double> templist in listList)
                        {
                            temp = templist[i] + temp;
                        }
                        listTotal.Add(temp);
                    }

                    dico["Total Dépense Famille"] = listTotal;
                    GraphForm form = new GraphForm(dico, "spline", "Total dépense famille");
                    form.ShowDialog();
                }
                if (e.RowIndex == 7)
                {
                    DataTable table = Calcul.GetTotalExt_Inc(1, 0);


                    Dictionary<string, List<double>> dico = new Dictionary<string, List<double>>();
                    List<double> list = new List<double>();
                    List<double> listTotal = new List<double>();
                    List<List<double>> listList = new List<List<double>>();
                    dico.Add("Total Recette Famille", new List<double>());
                    for (int nbrow = table.Rows.Count - 1; nbrow >= 0; nbrow--)
                    {
                        list = new List<double>();
                        for (int numCol = 1; numCol < 11; numCol++)
                        {
                            double.TryParse(table.Rows[nbrow].ItemArray[numCol].ToString(), out double value);
                            list.Add(value);
                        }
                        listList.Add(list);
                    }
                    double temp = 0;
                    for (int i = 0; i < list.Count; i++)
                    {
                        temp = 0;
                        foreach (List<double> templist in listList)
                        {
                            temp = templist[i] + temp;
                        }
                        listTotal.Add(temp);
                    }

                    dico["Total Recette Famille"] = listTotal;
                    GraphForm form = new GraphForm(dico, "spline", "Total recette famille");
                    form.ShowDialog();
                }
                if (e.RowIndex == 8)
                {
                    DataTable table = Calcul.TotalMargeForGraph();
                    Dictionary<string, List<double>> dico = new Dictionary<string, List<double>>();
                    List<double> list = new List<double>();
                    dico.Add("Total Marge", new List<double>());
                    list = new List<double>();
                    for (int nbrow = table.Rows.Count - 1; nbrow >= 0; nbrow--)
                    {
                        for (int numCol = 1; numCol < 11; numCol++)
                        {
                            double.TryParse(table.Rows[nbrow].ItemArray[numCol].ToString(), out double value);
                            list.Add(value);
                        }
                        break;
                    }

                    dico["Total Marge"] = list;
                    GraphForm form = new GraphForm(dico, "spline");
                    form.ShowDialog();

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        #endregion
        
       
        #region panel
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
        }

        private GraphicsPath GetGraphicpathRectangle(Rectangle b, int r)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(b);
            return path;
        }

        private GraphicsPath GetRoundRectagle(Rectangle b, int r)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(b.X, b.Y, r, r, 180, 90);
            path.AddArc(b.X + b.Width - r - 1, b.Y, r, r, 270, 90);
            path.AddArc(b.X + b.Width - r - 1, b.Y + b.Height - r - 1, r, r, 0, 90);
            path.AddArc(b.X, b.Y + b.Height - r - 1, r, r, 90, 90);
            path.CloseAllFigures();
            return path;
        }

        private void panelmonnaie_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                Color TitleBackColor = Color.FromArgb(1, 69, 19);
                Color TitleForeColor = Color.White;
                HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
                Font TitleFont = new Font("Corbel", 10, FontStyle.Bold);
                GroupBoxRenderer.DrawParentBackground(e.Graphics, panelmonnaie.ClientRectangle, this);
                var rect = panelmonnaie.ClientRectangle;
                int r = 10;
                var path = GetRoundRectagle(e.ClipRectangle, r);

                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
                Color color1 = Color.FromArgb(90, 144, 88);
                Color color3 = Color.FromArgb(90, 144, 88);
                var brush = new LinearGradientBrush(panelmonnaie.ClientRectangle, color1, color3, 90);
                e.Graphics.FillPath(brush, path);
                e.Graphics.SetClip(rect);
                var clip = e.Graphics.ClipBounds;
                var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
                e.Graphics.FillPath(brush2, path);
                rect.Location = new Point(0, 0);
                rect.Width = 125;
                rect.Height = 20;
                TextRenderer.DrawText(e.Graphics, Translation.Translate("Currency", langue), TitleFont, rect, TitleForeColor);
                e.Graphics.SetClip(clip);
                Pen pen = new Pen(TitleBackColor, 1);
                e.Graphics.DrawPath(pen, path);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            Color TitleBackColor = Color.FromArgb(1, 69, 19);
            Color TitleForeColor = Color.White;
            HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
            Font TitleFont = new Font("Corbel", 10, FontStyle.Bold);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, ClientRectangle, this);
            var rect = ClientRectangle;
            int r = 10;
            var path = GetRoundRectagle(e.ClipRectangle, r);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
            Color color1 = Color.FromArgb(90, 144, 88);
            Color color3 = Color.FromArgb(90, 144, 88);
            var brush = new LinearGradientBrush(ClientRectangle, color1, color3, 90);
            e.Graphics.FillPath(brush, path);
            e.Graphics.SetClip(rect);
            var clip = e.Graphics.ClipBounds;
            var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
            e.Graphics.FillPath(brush2, path);
            rect.Location = new Point(0, 0);
            rect.Width = 125;
            rect.Height = 20;
            TextRenderer.DrawText(e.Graphics, Translation.Translate("About",langue), TitleFont, rect, TitleForeColor);
            e.Graphics.SetClip(clip);
            Pen pen = new Pen(TitleBackColor, 1);
            e.Graphics.DrawPath(pen, path);
        }
        #endregion

        private string NextValue(string[] array, int index)
        {
            return array[index + 1];
        }

        private bool HasNextValue(string[] array, int index)
        {
            if (index + 1 < array.Length)
                return true;
            else
                return false;
        }

        private void groupBoxcalendrier_Enter(object sender, EventArgs e)
        {

        }

        private void groupBoxcalendrier_Paint(object sender, PaintEventArgs e)
        {
            Color TitleBackColor = Color.FromArgb(1, 69, 19);
            Color TitleForeColor = Color.White;
            HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
            Font TitleFont = new Font("Corbel", 10, FontStyle.Bold);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, ClientRectangle, this);
            var rect = ClientRectangle;
            int r = 10;
            var path = GetRoundRectagle(e.ClipRectangle, r);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
            Color color1 = Color.FromArgb(90, 144, 88);
            Color color3 = Color.FromArgb(90, 144, 88);
            var brush = new LinearGradientBrush(ClientRectangle, color1, color3, 90);
            e.Graphics.FillPath(brush, path);
            e.Graphics.SetClip(rect);
            var clip = e.Graphics.ClipBounds;
            var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
            e.Graphics.FillPath(brush2, path);
            rect.Location = new Point(0, 0);
            rect.Width = 125;
            rect.Height = 20;
            TextRenderer.DrawText(e.Graphics, Translation.Translate("Calendar",langue), TitleFont, rect, TitleForeColor);
            e.Graphics.SetClip(clip);
            Pen pen = new Pen(TitleBackColor, 1);
            e.Graphics.DrawPath(pen, path);
        }

        private void groupBoxResults_Paint(object sender, PaintEventArgs e)
        {
            Color TitleBackColor = Color.FromArgb(1, 69, 19);
            Color TitleForeColor = Color.White;
            HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
            Font TitleFont = new Font("Corbel", 10, FontStyle.Bold);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, ClientRectangle, this);
            var rect = ClientRectangle;
            int r = 10;
            var path = GetRoundRectagle(e.ClipRectangle, r);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
            Color color1 = Color.FromArgb(90, 144, 88);
            Color color3 = Color.FromArgb(90, 144, 88);
            var brush = new LinearGradientBrush(ClientRectangle, color1, color3, 90);
            e.Graphics.FillPath(brush, path);
            e.Graphics.SetClip(rect);
            var clip = e.Graphics.ClipBounds;
            var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
            e.Graphics.FillPath(brush2, path);
            rect.Location = new Point(0, 0);
            rect.Width = 125;
            rect.Height = 20;
            TextRenderer.DrawText(e.Graphics, Translation.Translate("Vizualisation",langue), TitleFont, rect, TitleForeColor);
            e.Graphics.SetClip(clip);
            Pen pen = new Pen(TitleBackColor, 1);
            e.Graphics.DrawPath(pen, path);
        }

        private void labelcalendrier_Paint(object sender, PaintEventArgs e)
        {
            Color TitleBackColor = Color.FromArgb(90, 144, 88);
            Color TitleForeColor = Color.Black;
            HatchStyle TitleHatchStyle = HatchStyle.Horizontal;
            Font TitleFont = new Font("Corbel", 8);
            GroupBoxRenderer.DrawParentBackground(e.Graphics, ClientRectangle, this);
            var rect = ClientRectangle;
            int r = 10;
            var path = GetGraphicpathRectangle(e.ClipRectangle, r);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            rect = new Rectangle(0, 0, rect.Width, TitleFont.Height + Padding.Bottom + Padding.Top);
            Color color1 = Color.FromArgb(90, 144, 88);
            Color color3 = Color.FromArgb(90, 144, 88);
            var brush = new LinearGradientBrush(ClientRectangle, color1, color3, 90);
            e.Graphics.FillPath(brush, path);
            e.Graphics.SetClip(rect);
            var clip = e.Graphics.ClipBounds;
            var brush2 = new HatchBrush(TitleHatchStyle, TitleBackColor, TitleBackColor);
            e.Graphics.FillPath(brush2, path);
            rect.Location = new Point(0, 0);
            rect.Width = 120;
            rect.Height = 10;
            TextRenderer.DrawText(e.Graphics, Translation.Translate("Calendar", langue), TitleFont, rect, TitleForeColor);
            e.Graphics.SetClip(clip);
            Pen pen = new Pen(TitleBackColor, 1);
            e.Graphics.DrawPath(pen, path);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            resultform.Close();
        }

        private void buttonCalendar_Click(object sender, EventArgs e)
        {

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            if (dataGridViewValues.Rows.Count > 1 && dataGridViewValues.Columns.Count > 3)
            {
                Export.RunExportTable(dataGridViewValues, "Travail");
            }
        }

        private void comboBoxyear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
