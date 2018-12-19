using OMEGA.Data_Classes;
using OMEGA.Other_Classes;
using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OMEGA.Forms
{
    /// <summary>
    /// Standard form est utilisé plusieurs fois si on veut juste afficher un datagridview
    /// le constructeur va donc gérer le type de standard form qu'il faut.
    /// </summary>
    internal partial class StandardForm : Form
    {
        private double mTotalValeur;
        private int mIdAct;
        private int mIdEtatSortie;
        private int mNbProduct;
        private int mNbChargeOpe;
        private int mNbChargeStruc;
        private int mNbMiscExp;
        private int mNbFamExp;
        private int mNbMiscInc;
        private int mNbFraisFinLT;
        private int mNbFraisFinCT;
        private int mNbFamInc;
        private int mColorSetCount = 0;
        private int mIdExpl;
        private string mValueToReport = "";
        private Bitmap bmp;
        private DataTable phaseTable = new DataTable();
        private DataTable EtatSortieTable = new DataTable();
        private List<int> listeRecordToDelete = new List<int>();
        private string FilterEtatSortieName = "";
        private string FilterDefEtatSortieName = "";
        private Color color1;
        private Color color2;
        private int langue = Properties.Settings.Default.Langue;

        internal StandardForm(int id, string type, string table = "", DataGridView datagridview = null, string filter2 = "")
        {
            InitializeComponent();
            SetColor();
            switch (type)
            {
                case "marge":
                    mIdAct = id;
                    LoadgridMarge();
                    Text = Translation.Translate("Marging", langue);
                    break;
                case "surface":
                    SetIdExpl();
                    LoadgridSurface();
                    Text = Translation.Translate("Area",langue);
                    break;
                case "ListePeriode":
                    LoadListePeriode();
                    dataGridView1.CellClick += DataGridView1CellClick;
                    Text = Translation.Translate("Periode", langue);
                    break;
                case "RecetteDepense":
                    SetIdExpl();
                    LoadGridRecetteDepense();
                    Text = Translation.Translate("Balance", langue);
                    break;
                case "quantite":
                    SetIdExpl();
                    LoadGridQUANTITE();
                    Text = Translation.Translate("Quantity", langue);
                    break;
                case "phase":
                    LoadPhaseGrid();
                    SetColor();
                    ManageColor();
                    Text = Translation.Translate("Phasis", langue);
                    break;
                case "Immo":
                    SetIdExpl();
                    LoadresultImmo();
                    Text = Translation.Translate("Immo", langue);
                    break;
                case "EtatSortie":
                    SetIdExpl();
                    mIdEtatSortie = id;
                    FilterEtatSortieName = table;
                    FilterDefEtatSortieName = filter2;
                    LoadEtatSortie();
                    Text = Translation.Translate("Output", langue);
                    break;
                case "Resultat EtatSortie":
                    SetIdExpl();
                    LoadResultatEtatSortie(datagridview);
                    Text = Translation.Translate("Result", langue);
                    break;
                case "CEG":
                    SetIdExpl();
                    LoadGridCEG();
                    Text = Translation.Translate("Ffarm account", langue);
                    break;
                case "Grand Poste":
                    SetIdExpl();
                    LoadGridDepRecGP();
                    Text = Translation.Translate("Grand poste", langue);
                    break;
            }
        }

        internal StandardForm(List<string> list,int IdAct)
        {
            InitializeComponent();
            SetIdExpl();
            SetColor();
            List<int> listIdAct = GetIdFromName(list);
            mIdAct = IdAct;
            LoadgridMarge();
        }

        #region Marge
        private void LoadgridMarge()
        {
            DataTable dtbl = new DataTable();
            dtbl.Columns.Add(" ");
            dtbl.Columns.Add(Translation.Translate("Name",langue));
            dtbl.Columns.Add(Translation.Translate("Group",langue));
            dtbl.Columns.Add(Translation.Translate("Unity",langue));
            dtbl.Columns.Add(Translation.Translate("Price",langue));
            dtbl.Columns.Add(Translation.Translate("Quantity",langue));
            dtbl.Columns.Add(Translation.Translate("Value",langue) + "1");

            dtbl.Rows.Add((new object[] { Translation.Translate("Product",langue) }));
            KeyValuePair<double, DataTable> Value = new KeyValuePair<double, DataTable>();

            Value = Calcul.AddElement(dtbl, "Produits",mIdAct,mTotalValeur,mNbProduct,mNbChargeOpe);
            dtbl = Value.Value;
            mTotalValeur = Value.Key;
            dtbl.Rows.Add((new object[] { Translation.Translate("Total Product", langue), "", "", "", "", "", mTotalValeur }));
            double totalproduit = mTotalValeur;
            mTotalValeur = 0;

            dtbl.Rows.Add((new object[] { Translation.Translate("Cost", langue) }));
            Value = Calcul.AddElement(dtbl, "Charges", mIdAct, mTotalValeur, mNbProduct, mNbChargeOpe);
            dtbl = Value.Value;
            mTotalValeur = Value.Key + mTotalValeur;
            dtbl.Rows.Add((new object[] { Translation.Translate("Total Cost", langue) , "", "", "", "", "", mTotalValeur }));
            double totalcharge = mTotalValeur;
            double marge = totalproduit - totalcharge;
            dtbl.Rows.Add((new object[] { Translation.Translate("Margin", langue), "", "", "", "", "", marge }));

            //dtbl.Rows.Add((new object[] { "Travail" }));
            //dtbl = AddPeriode(dtbl);

            // dtbl = FilterTable(dtbl);

            dataGridView1.DataSource = dtbl;
            dataGridView1.AllowUserToAddRows = false;
            Point point = new Point(3, 3);
            dataGridView1.Location = point;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormattingMarge);

        }

        private void dataGridView1_CellFormattingMarge(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (mColorSetCount < 500)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                // line prodcutv
                if (row.Cells[0].Value.ToString() == Translation.Translate("Product", langue))
                    row.DefaultCellStyle.BackColor = Color.PaleGreen;

                else if (row.Cells[0].Value.ToString() == Translation.Translate("Cost", langue))
                    row.DefaultCellStyle.BackColor = Color.PaleGreen;

                else if (row.Cells[0].Value.ToString().Contains(Translation.Translate("Margin", langue)))
                    row.DefaultCellStyle.BackColor = Color.Red;

                else if (row.Cells[0].Value.ToString().Contains(Translation.Translate("Total", langue)))
                    row.DefaultCellStyle.BackColor = Color.Green;

                else row.DefaultCellStyle.BackColor = Color.PaleGreen;

               
                mColorSetCount++;
            }
        }

       

        #endregion

        #region Marge Somme
        private void LoadgridMargeSomme(List<int> listid)
        {
            try
            {
                DataTable dtbl = new DataTable();
                dtbl.Columns.Add(" ");
                for (int i = 2018; i < 2028; i++)
                {
                    dtbl.Columns.Add(i.ToString());
                }
                dtbl.Rows.Add((new object[] { "Produits" }));

                foreach (int Idactivite in listid)
                {
                    dtbl = AddElement(dtbl, "Produits", Idactivite);
                }

                dtbl.Rows.Add((new object[] { "Total Produits", mTotalValeur, mTotalValeur, mTotalValeur,
            mTotalValeur, mTotalValeur, mTotalValeur , mTotalValeur, mTotalValeur, mTotalValeur, mTotalValeur}));
                double totalproduit = mTotalValeur;
                mTotalValeur = 0;

                dtbl.Rows.Add((new object[] { "Charges" }));
                foreach (int Idactivite in listid)
                {
                    dtbl = AddElement(dtbl, "Charges", Idactivite);
                }

                dtbl.Rows.Add((new object[] { "Total Charges", mTotalValeur, mTotalValeur, mTotalValeur,
            mTotalValeur, mTotalValeur, mTotalValeur , mTotalValeur, mTotalValeur, mTotalValeur, mTotalValeur}));
                double totalcharge = mTotalValeur;
                double marge = totalproduit - totalcharge;
                dtbl.Rows.Add((new object[] { "Marge", marge, marge, marge,marge,
            marge, marge, marge , marge, marge, marge}));

                //dtbl.Rows.Add((new object[] { "Travail" }));
                //dtbl = AddPeriode(dtbl);

                // dtbl = FilterTable(dtbl);

                dataGridView1.DataSource = dtbl;
                dataGridView1.AllowUserToAddRows = false;
                Point point = new Point(3, 3);
                dataGridView1.Location = point;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AllowUserToOrderColumns = false;
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                foreach (DataGridViewColumn column in dataGridView1.Columns)
                {
                    column.SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormattingMarge);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }

        }

        private DataTable AddElement(DataTable dtbl, string table, int IdAct)
        {
            try
            {
                List<int> listId = SQLQueryBuilder.GetListID("Activite", table, IdAct);

                string query = "";
                if (table == "Charges")
                {
                    query = ChargeQuery.SelectChargeOftheActivityQuery(listId, 0, IdAct);
                }
                else if (table == "Produits")
                {
                    query = ProduitQuery.SelectProductOftheActivityQuery2(listId, IdAct);
                }

                string previousname = "";
                SQLiteDataReader reader = SQlQueryExecuter.RunQueryDataReader(query);

                string[] ValueToAdd = new string[11];
                while (reader.Read())
                {
                    if (reader != null)
                    {
                        double qte;
                        //  0 = nom , 1= prix , 2 = qté ,
                        ValueToAdd[0] = reader.GetValue(0).ToString();

                        double prix = Commun.GetDoubleFromString(reader.GetValue(1).ToString());
                        double.TryParse(reader.GetValue(2).ToString(), out qte);
                        double valeur = prix * qte;

                        ValueToAdd[1] = valeur.ToString();
                        ValueToAdd[2] = valeur.ToString();
                        ValueToAdd[3] = valeur.ToString();
                        ValueToAdd[4] = valeur.ToString();
                        ValueToAdd[5] = valeur.ToString();
                        ValueToAdd[6] = valeur.ToString();
                        ValueToAdd[7] = valeur.ToString();
                        ValueToAdd[8] = valeur.ToString();
                        ValueToAdd[9] = valeur.ToString();
                        ValueToAdd[10] = valeur.ToString();
                        if (previousname != ValueToAdd[0])
                        {
                            if (TableContainsElement(reader.GetValue(0).ToString(), dtbl).Key)
                            {
                                int rowindex = TableContainsElement(reader.GetValue(0).ToString(), dtbl).Value;
                                for (int i = 1; i <= 10; i++)
                                {
                                    double oldvalue;
                                    double.TryParse(dtbl.Rows[rowindex].ItemArray[i].ToString(), out oldvalue);
                                    valeur = valeur + oldvalue;
                                    dtbl.Rows[rowindex].SetField<string>(i, valeur.ToString());
                                }
                            }
                            else
                            {
                                dtbl.Rows.Add(ValueToAdd);
                            }
                            mTotalValeur = mTotalValeur + valeur;
                            if (table == "Charges")
                            {
                                mNbChargeOpe = 1 + mNbChargeOpe;
                            }
                            else
                            {
                                mNbProduct = 1 + mNbProduct;
                            }
                            previousname = ValueToAdd[0];
                        }
                        //{
                        //  

                        //    dtbl.Rows.Add(ValueToAdd);
                        //    // we get the number of row for the charges and the products

                        //    previousname = ValueToAdd[1];
                        //}
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return dtbl;
        }

        private KeyValuePair<bool, int> TableContainsElement(string nomElement, DataTable table)
        {
            int index = 0;
            KeyValuePair<bool, int> data = new KeyValuePair<bool, int>(false, 0);
            foreach (DataRow row in table.Rows)
            {
                if (nomElement == row.ItemArray[0].ToString())
                {
                    data = new KeyValuePair<bool, int>(true, index);
                    return data;
                }
                index++;
            }
            return data;
        }

        private List<int> GetIdFromName(List<string> list)
        {
            List<int> Mylist = new List<int>();
            try
            {
                foreach (string activite in list)
                {
                    string query = SQLQueryBuilder.SelectQuery("Activite", "ID", "Where nom = '" + activite + "'");
                    List<int> listid = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(listid))
                    {
                        if (!Mylist.Contains(listid[0]))
                            Mylist.Add(listid[0]);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return Mylist;
        }

        #endregion

        #region Surface
        private void LoadgridSurface()
        {
            try
            {
                DataTable dtbl = new DataTable();
                dtbl.Columns.Add(" ");
                dtbl.Columns.Add("Nom");
                int year = Commun.GetYear();
                for (int i = 1; i <= 10; i++)
                {
                    dtbl.Columns.Add(year.ToString());
                    year++;
                }

                dtbl.Rows.Add((new object[] { "Culture" }));

                dtbl = AddSurface(dtbl);

                dtbl = CalculTotal(dtbl);

                dataGridView1.DataSource = dtbl;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.ReadOnly = true;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            // dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting);
        }

        private DataTable AddSurface(DataTable dtbl)
        {
            string query = "select Activite.nom" +
            " FROM Agri_Assol Join Activite On Activite.Id = Agri_Assol.IdActivite" +
            " WHERE IdExploitations = '" + mIdExpl + "';";
            List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
            List<double> listdouble = new List<double>();
            foreach (string item in list)
            {
                string[] valueToadd = new string[12];
                for (int i = 2; i < 12; i++)
                {
                    query = "Select Valeur From Result_Calcul " +
                          "Where Table_Origine = 'Agri_Assol' " +
                          " AND Annee = '" + dtbl.Columns[i].ColumnName +
                          "' AND Nom = '" + item +
                          "' AND IdExploitations = '" + mIdExpl + "';";
                    List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Valeur", query);
                    valueToadd[0] = "";
                    valueToadd[1] = item;
                    valueToadd[i] = list2[0].ToString();
                }
                dtbl.Rows.Add(valueToadd);
            }
            return dtbl;
        }

        private DataTable CalculTotal(DataTable dtbl)
        {
            try
            {
                string[] valueToadd = new string[12];
                double totalvalue = 0;
                int columnIndex = -1;
                valueToadd[0] = "TOTAL Culture";
                valueToadd[1] = " ";
                foreach (DataColumn column in dtbl.Columns)
                {
                    columnIndex++;
                    totalvalue = 0;
                    if (IsNumeric(column.ColumnName))
                    {
                        foreach (DataRow row in dtbl.Rows)
                        {
                            double temp;
                            if (double.TryParse(row.ItemArray[columnIndex].ToString(), out temp))
                            {
                                totalvalue = totalvalue + temp;
                            }

                        }
                        valueToadd[columnIndex] = totalvalue.ToString();
                    }
                }
                dtbl.Rows.Add(valueToadd);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return dtbl;
        }
        #endregion

        #region Periode
        private void LoadListePeriode()
        {
            try
            {
                string query = ActivityQuery.SelectActivityQuery("");
                DataTable PeriodeTable = new DataTable();
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(PeriodeTable);
                dataGridView1.DataSource = PeriodeTable;
                Size size = new Size(110, 400);
                this.Size = size;
                Point point = new Point(2, 2);
                dataGridView1.Location = point;
                size = new Size(108, 350);
                dataGridView1.Size = size;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[2].Visible = false;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.RowHeadersVisible = false;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
           

        }

        private DataTable AddPeriode(DataTable dtbl)
        {

            string query = PeriodeQuery.SelectPeriodeOftheActivityQuery();
            SQLiteDataReader reader = SQlQueryExecuter.RunQueryDataReader(query);
            string[] ValueToAdd = new string[7];
            while (reader.Read())
            {
                if (reader != null)
                {
                    ValueToAdd[0] = "";
                    ValueToAdd[1] = reader.GetValue(0).ToString();
                }
            }


            return dtbl;

        }

        private void DataGridView1CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int id;
                int CurrentIdActivite = GetIdCurrentActivite();
                int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out id);
                string query = SQLQueryBuilder.SelectQuery("Travail", "*", "Where IdActivite = '" + id + "'");
                List<int> listIdPeriode = SQlQueryExecuter.RunQueryReaderInt("IdPeriode", query);
                if (Commun.ListHasValue(listIdPeriode))
                {
                    int index = listIdPeriode.Count - 1;
                    query = SQLQueryBuilder.SelectQuery("Travail", "*", "Where IdActivite = '" + id + "' AND IdPeriode = '" + listIdPeriode[0] + "'");
                    List<double> listQte1 = SQlQueryExecuter.RunQueryReaderDouble("Qte_1", query);
                    List<double> listAvQte1 = SQlQueryExecuter.RunQueryReaderDouble("Qte_Av1", query);
                    List<string> listNomPeriode = SQlQueryExecuter.RunQueryReaderStr("Nom_Periode", query);
                    if (Commun.ListHasValue(listQte1) && Commun.ListHasValue(listAvQte1) && Commun.ListHasValue(listNomPeriode))
                    {
                        if (CheckCurrentActivite(CurrentIdActivite))
                        {
                            for (int i = 0; i <= index; i++)
                            {
                                query = "Insert Into Travail (IdPeriode, Nom_Periode,IdActivite,Qte_1,Qte_Av1)" +
                                    "VALUES ('" + listIdPeriode[0] + "','" + listNomPeriode[i] + "','" + CurrentIdActivite +
                                    "','" + listQte1[i] + "','" + listAvQte1[i] + "');";
                                SQlQueryExecuter.RunQuery(query);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(Translation.Translate("This activity does not have any work calendar.",langue));
                    }
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private bool CheckCurrentActivite(int id)
        {
            string query = "Select * from Travail Where IdActivite = '" + id + "';";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
            DialogResult dialogResult = new DialogResult();
            if (Commun.ListHasValue(list))
            {
                dialogResult = MessageBox.Show(Translation.Translate("This activity has a calendar, do you want to replace it by the one selected ?",langue), "Warning", MessageBoxButtons.YesNo);

                if (dialogResult == DialogResult.Yes)
                {
                    query = "Delete From Travail Where IdActivite = '" + id + "';";
                    SQlQueryExecuter.RunQuery(query);
                    return true;
                }
                else return false;
            }
            else return true;
        }
        #endregion

        #region Result Incomes/Expenses

        private int GetYear()
        {
            int An0 = 0;
            string query = "Select An_0 From Agri_DefSim Where IdExploitations = '" + mIdExpl + "'";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
            if (Commun.ListHasValue(list))
            {
                An0 = list[0];
            }
            else An0 = DateTime.Now.Year;
            return An0;
        }

        private void LoadGridRecetteDepense()
        {
            try
            {

                DataTable dtbl = new DataTable();
                dtbl.Columns.Add(" ");

                int year = GetYear();
                for (int i = year; i < year+10; i++)
                {
                    dtbl.Columns.Add(i.ToString());
                }

                dtbl.Rows.Add((new object[] { Translation.Translate("Products",langue) }));
                KeyValuePair<int, DataTable> value = Calcul.GetElementRec_Dep(dtbl, "Produits", 0, mNbProduct);
                dtbl = value.Value;
                mNbProduct = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Produits", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);
            
                dtbl.Rows.Add((new object[] { Translation.Translate("Operational Cost", langue) }));
                value = Calcul.GetElementRec_Dep(dtbl, "Charges", 0, mNbChargeOpe);
                dtbl = value.Value;
                mNbChargeOpe = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Charges Operationnelles", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Structural Cost", langue) }));
                value = Calcul.GetElementRec_Dep(dtbl, "Charges", 1, mNbChargeStruc);
                dtbl = value.Value;
                mNbChargeStruc = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Charges Structurelles", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc, mNbChargeStruc);

                dtbl = Calcul.GetMarge(dtbl);

                dtbl.Rows.Add((new object[] { Translation.Translate("Misc expense", langue) }));
                value = Calcul.GetElementRec_Dep(dtbl, "Misc Expense", 0, mNbMiscExp,0,1);
                dtbl = value.Value;
                mNbMiscExp = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Misc Expense", mNbProduct+1, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc, mNbChargeStruc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Family expense", langue) }));
                value = Calcul.GetElementRec_Dep(dtbl, "Family Expense", 0, mNbFamExp,1,1);
                dtbl = value.Value;
                mNbFamExp = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Family Expense", mNbProduct+1, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc, mNbChargeStruc);

   
                dtbl.Rows.Add((new object[] { Translation.Translate("Misc Income", langue) }));
                value = Calcul.GetElementRec_Dep(dtbl, "Misc income", 0, mNbMiscInc,0,0);
                dtbl = value.Value;
                mNbMiscInc = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Misc Income", mNbProduct+1, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc, mNbChargeStruc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Family Income", langue) }));
                value = Calcul.GetElementRec_Dep(dtbl, "Family income", 0, mNbFamInc,1,0);
                dtbl = value.Value;
                mNbFamInc = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Family income", mNbProduct+1, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc, mNbChargeStruc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Immobilisation", langue) }));
                dtbl = Calcul.Immobilisation(dtbl);

                dtbl = Calcul.GetSolde(dtbl);

                dtbl = Calcul.GetSoldCumule(dtbl);

              
                dataGridView1.DataSource = dtbl;

                AdjustGridViewDisplayForReport();

                dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting2);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void dataGridView1_CellFormatting2(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (mColorSetCount < 10000)
                {
                    Font Corbel = new Font("Corbel", 10, FontStyle.Bold);
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    // line total
                    if (row.Cells[0].Value.ToString().Contains("TOTAL"))
                    {
                        row.DefaultCellStyle.Font = Corbel;
                        row.DefaultCellStyle.ForeColor = Color.Blue;
                    }
                    // line solde
                    else if (row.Cells[0].Value.ToString().Contains("SOLDE") || row.Cells[0].Value.ToString().Contains("Result") || row.Cells[0].Value.ToString().Contains("MARGE"))
                    {
                        row.DefaultCellStyle.Font = Corbel;
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else if (row.Cells[0].Value.ToString().First<char>() != ' ')
                    {
                        row.DefaultCellStyle.Font = Corbel;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        Corbel = new Font("Corbel", 10);
                        row.DefaultCellStyle.Font = Corbel;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    mColorSetCount++;
                }
            }
             catch (Exception Ex)
            {
               // MessageBox.Show(Ex.Message);
              //  Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        #endregion

        #region Result QUANTITE

        private void LoadGridQUANTITE()
        {
            try
            {

                DataTable dtbl = new DataTable();
                dtbl.Columns.Add(" ");
                dtbl.Columns.Add("Unité");
                for (int i = 2018; i < 2028; i++)
                {
                    dtbl.Columns.Add(i.ToString());
                }

                dtbl.Rows.Add((new object[] { "Produits" }));
                //dtbl = AddElementQUANTITE(dtbl, "Produits");
                dtbl = Calcul.GetElementAnnualQuantite(dtbl, "Produits", "Agri_Produits", 0, 0);
                dtbl = Calcul.GetElementPerenneQuantite(dtbl, "Produits", "Agri_Perenne", 0, 0);
                dtbl = Calcul.GetElementPluriannualQuantite(dtbl, "Produits", "Agri_Pluriannuelle", 0, 0);

                dtbl.Rows.Add((new object[] { "Charges" }));
                dtbl = Calcul.GetElementAnnualQuantite(dtbl, "Charges", "Agri_Charges", 0, 0);
                dtbl = Calcul.GetElementPerenneQuantite(dtbl, "Charges", "Agri_Perenne", 0, 0);
                dtbl = Calcul.GetElementPluriannualQuantite(dtbl, "Charges", "Agri_Pluriannuelle", 0, 0);
                //dtbl = Calcul.GetElementQuantite(dtbl, "Charges", mNbProduct, mNbChargeOpe);
                //dtbl = AddElementQUANTITE(dtbl, "Charges");

                dataGridView1.DataSource = dtbl;

                AdjustGridViewDisplayForReport();

                dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting3);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }

        }


        private DataTable AddElementQUANTITE(DataTable dtbl, string table)
        {
            try
            {
                string IdTable = "";
                string nomtable = "";
                switch (table)
                {
                    case "Produits":
                        IdTable = "IdProduits";
                        nomtable = "Prod_Quantite";
                        break;
                    case "Charges":
                        IdTable = "IdCharges";
                        nomtable = "Charge_Quantite";
                        break;
                }
                string query = "";

                query = "Select distinct Def_Categ.Nom From Def_Categ " +
                    " Join " + table + " on Def_Categ.IdDefCateg = " + table + ".IdDefCateg " +
                    " Join Caract_Exploitation on Caract_Exploitation." + IdTable + "=" + table + ".ID";
                if (table == "Charges")
                {
                    query = query + " Where Charges.Structurelle = '0' " +
                        "AND IdExploitation = '" + mIdExpl + "';";
                }
                else query = query + " Where IdExploitation = '" + mIdExpl + "';";


                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                IdTable = IdTable.Remove(IdTable.Length - 1, 1);
                foreach (string groupe in list)
                {
                    dtbl.Rows.Add((new object[] { "   " + groupe }));

                    query = "Select " + table + ".Nom,UEnt as 'Unité'," + nomtable + ".Quantite_1 From " + nomtable + " " +
                      " Join  Def_Categ  on Def_Categ.IdDefCateg = " + table + ".IdDefCateg " +
                    //" Join Agri_" + table + " on Agri_" + table + "." + IdTable + " = " + table + ".ID " +
                    " Join Caract_Exploitation on Caract_Exploitation." + IdTable + "s =" + table + ".ID" +
                    " JOIN SystemeUnite on " + table + ".IdSysUnit = SystemeUnite.IdSysUnit " +
                    " Join " + table + " on " + table + ".ID = " + nomtable + "." + IdTable +
                    " Where Def_Categ.Nom = '" + groupe + "' And IdExploitation = '" + mIdExpl + "';";

                    SQLiteDataReader reader = SQlQueryExecuter.RunQueryDataReader(query);
                    string[] ValueToAdd = new string[12];
                    while (reader.Read())
                    {
                        if (reader != null)
                        {
                            // 0 = nom , 1 = unité , 2 = quantite
                            ValueToAdd[0] = "      " + reader.GetValue(0).ToString();
                            ValueToAdd[1] = reader.GetValue(1).ToString();
                            ValueToAdd[2] = reader.GetValue(2).ToString();
                            ValueToAdd[3] = reader.GetValue(2).ToString();
                            ValueToAdd[4] = reader.GetValue(2).ToString();
                            ValueToAdd[5] = reader.GetValue(2).ToString();
                            ValueToAdd[6] = reader.GetValue(2).ToString();
                            ValueToAdd[7] = reader.GetValue(2).ToString();
                            ValueToAdd[8] = reader.GetValue(2).ToString();
                            ValueToAdd[9] = reader.GetValue(2).ToString();
                            ValueToAdd[10] = reader.GetValue(2).ToString();
                            ValueToAdd[11] = reader.GetValue(2).ToString();

                            dtbl.Rows.Add(ValueToAdd);

                            // we get the number of row for the charges and the products
                            if (table == "Charges")
                            {
                                mNbChargeOpe = 1 + mNbChargeOpe;
                            }
                            else
                            {
                                mNbProduct = 1 + mNbProduct;
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
            return dtbl;

        }
        

        private void dataGridView1_CellFormatting3(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                if (mColorSetCount < 1000)
                {
                    Font Corbel = new Font("Corbel", 10, FontStyle.Bold);
                    DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                    // line total
                    if (row.Cells[0].Value.ToString().Contains("TOTAL"))
                    {
                        row.DefaultCellStyle.Font = Corbel;
                        row.DefaultCellStyle.ForeColor = Color.Blue;
                    }
                    // line solde
                    else if (row.Cells[0].Value.ToString().Contains("SOLDE"))
                    {
                        row.DefaultCellStyle.Font = Corbel;
                        row.DefaultCellStyle.ForeColor = Color.Red;
                    }
                    else if (row.Cells[0].Value.ToString().First<char>() != ' ')
                    {
                        row.DefaultCellStyle.Font = Corbel;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    else
                    {
                        Corbel = new Font("Corbel", 10);
                        row.DefaultCellStyle.Font = Corbel;
                        row.DefaultCellStyle.ForeColor = Color.Black;
                    }
                    mColorSetCount++;
                }
            }
            catch (Exception Ex)
            {
                //MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        
        #endregion

        #region Phase

        private void LoadPhaseGrid()
        {
            try
            {

                string query = "Select Ate_CatPhase.ID,Ate_CatPhase.Nom,Debut,Max," + Get40ph() + " From Ate_CatPhase;";
                 
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(phaseTable);
                dataGridView1.DataSource = phaseTable;
            //  dataGridView1.Columns[1].Visible = false;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToOrderColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                panelphase.Visible = true;
                RenameColumnHeader();
                SetCaptionPhase();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void RenameColumnHeader()
        {
            try
            {
                dataGridView1.Columns[1].HeaderText = Translation.Translate("Name", langue);
                dataGridView1.Columns[2].HeaderText = Translation.Translate("Begining", langue);
                dataGridView1.Columns[3].HeaderText = Translation.Translate("Max", langue);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void SetCaptionPhase()
        {
            try
            {
                int langue = Properties.Settings.Default.Langue;
                buttonOK.Text = Translation.Translate("OK", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
                buttonAdd.Text = Translation.Translate("Add", langue);
                toolTip1.SetToolTip(buttonReport, Translation.Translate("Copy value on the line", langue));
                toolTip1.SetToolTip(buttonreport0, Translation.Translate("Copy value 0 on the line", langue));
                toolTip1.SetToolTip(button123, Translation.Translate("Put the value that corresponds to the column number", langue));

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private void SaveDataPhase()
        {
            try
            {
                string query = "";

                foreach (int index in listeRecordToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(index, "Ate_CatPhase");
                    SQlQueryExecuter.RunQuery(query);
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    query = "Select * From Ate_CatPhase Where ID = '" + row.Cells[0].Value.ToString() + "';";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                   // idProduit = SQLQueryBuilder.FindId("Produits", "ID", "Nom", row.Cells[2].Value.ToString());

                    if (Commun.ListHasValue(list))
                    {
                        query = "Update Ate_CatPhase Set Nom = '" + row.Cells[1].Value.ToString() +
                            "', Debut = '" + row.Cells[3].Value.ToString() + "'," +
                            " Max = '" + row.Cells[4].Value.ToString() + "', " + Update40ph(row) + ";";
                    }
                    else
                    {
                        query = "Insert into Ate_CatPhase (Nom,Debut,Max," + Get40ph() + ")" +
                            "VALUES ('" + row.Cells[1].Value.ToString() + "','" + row.Cells[3].Value.ToString() + "','" +
                            row.Cells[4].Value.ToString() + "'," + Insert40ph(row) + ");";
                    }
                    SQlQueryExecuter.RunQuery(query);

                  //  AddItemInDefCateg(row, 1);
                 // AddItemInDefCateg(row, 2);
                    AddActivity(row);
                }
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void AddItemInDefCateg(DataGridViewRow row,int idDef)
        {
           string  query = "Select * From Def_Categ Where Nom = '" + row.Cells[1].Value.ToString() +
                "' AND IdDefinitions = '"+ idDef + "';";
           List<int>  list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
            if (!Commun.ListHasValue(list))
            {
                query = "Insert into Def_Categ (Nom,IdDefinitions)" +
                    "VALUES ('" + row.Cells[1].Value.ToString() + "','"+ idDef + "');";
            }
            SQlQueryExecuter.RunQuery(query);
        }

        private void AddActivity(DataGridViewRow row)
        {
            string query = "Select * From Activite Where Nom = '" + row.Cells[1].Value.ToString() +
                 "' AND IdType = '4';";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
            if (!Commun.ListHasValue(list))
            {
                query = "Insert into Activite (Nom,IdType)" +
                    "VALUES ('" + row.Cells[1].Value.ToString() + "','4');";
                SQlQueryExecuter.RunQuery(query);
            }
            
        }

        private string Get40ph()
        {
            string ph = "";
            try
            {
                for (int i = 1; i <= 40; i++)
                {
                    ph = ph + "Ph" + i + ",";
                }
                int index = ph.LastIndexOf(',');
                ph = ph.Remove(index);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return ph;
        }

        private string Update40ph(DataGridViewRow row)
        {
            string ph = "";
            try
            {
                for (int i = 0; i < 40; i++)
                {
                    ph = ph + "Ph" + (i+1) + "='" + row.Cells[i + 4].Value.ToString() + "',";
                }
                int index = ph.LastIndexOf(',');
                ph = ph.Remove(index);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return ph;
        }

        private string Insert40ph(DataGridViewRow row)
        {
            string ph = "";
            try
            {
                for (int i = 0; i < 40; i++)
                {
                    ph = ph + "'" + row.Cells[i + 4].Value.ToString() + "',";
                }
                int index = ph.LastIndexOf(',');
                ph = ph.Remove(index);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return ph;
        }

        private void buttonListeP_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Select ID,Nom From Produits";
                ListeForm listeForm = new ListeForm(query, "phase", "", null, null, dataGridView1, phaseTable);
                listeForm.Show();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            try
            {
                // The value we report is a surface, its initialized at -1, so if the parse failed, it stay -1
                // meaning that we dont want to report this value
                if (mValueToReport != "")
                {
                    DataGridViewSelectedRowCollection ListuserRow = dataGridView1.SelectedRows;
                    // if (ListuserRow.Count == 1)
                    {
                        foreach (DataGridViewColumn column in dataGridView1.Columns)
                        {
                            if (column.Index > dataGridView1.CurrentCell.ColumnIndex)
                            {
                                dataGridView1.CurrentRow.Cells[column.Index].Value = mValueToReport;
                            }
                        }
                    }
                    //else MessageBox.Show("Merci de reselectionner la cellule à copier");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonreport0_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection ListuserRow = dataGridView1.SelectedRows;
                // if (ListuserRow.Count == 1)
                {
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (column.Index > dataGridView1.CurrentCell.ColumnIndex)
                        {
                            dataGridView1.CurrentRow.Cells[column.Index].Value = "";
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
                mValueToReport = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void button123_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection ListuserRow = dataGridView1.SelectedRows;
                // if (ListuserRow.Count == 1)
                {
                    foreach (DataGridViewColumn column in dataGridView1.Columns)
                    {
                        if (column.Index >= dataGridView1.CurrentCell.ColumnIndex && dataGridView1.CurrentCell.ColumnIndex > 4)
                        {
                            dataGridView1.CurrentRow.Cells[column.Index].Value = column.Index - 4;
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveDataPhase();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            SaveDataPhase();
            this.Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private int GetId(int Rowindex)
        {
            int id = -1;
            try
            {
                if (dataGridView1.Rows[Rowindex].Cells[0].Value != null)
                {
                    int.TryParse(dataGridView1.Rows[Rowindex].Cells[0].Value.ToString(), out id);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return id;
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection UserSelectedRow = dataGridView1.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    if (!listeRecordToDelete.Contains(GetId(row.Index))) listeRecordToDelete.Add(GetId(row.Index));
                    dataGridView1.Rows.RemoveAt(row.Index);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string[] newvalue = new string[dataGridView1.ColumnCount];
                phaseTable.Rows.Add(newvalue);
                ManageColor();
            }
            catch
            {

            }
            
        }

        #endregion

        #region Result Immo

        private void LoadresultImmo()
        {
            try
            {
                DataTable tableImmo = new DataTable();

                tableImmo.Columns.Add();
                int year = DateTime.Now.Year;
                for (int i = 1; i <= 10; i++)
                {
                    tableImmo.Columns.Add(year.ToString());
                    year++;
                }

                tableImmo.Rows.Add("Petit Materiel", "", "", "", "", "", "", "", "", "", "");

                tableImmo = AddPetitMaterielRow(tableImmo);

                tableImmo.Rows.Add("Autres Immo", "", "", "", "", "", "", "", "", "", "");

                tableImmo = AddAutreImmo(tableImmo);

                tableImmo.Rows.Add("Recapitulation", "", "", "", "", "", "", "", "", "", "");

                tableImmo = AddRecaplines(tableImmo);

                dataGridView1.DataSource = tableImmo;
                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToDeleteRows = false;
                dataGridView1.AllowUserToOrderColumns = false;
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.CellFormatting += dataGridView1_CellFormattingImmo;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private DataTable AddPetitMaterielRow(DataTable table)
        {
            try
            {
                string query = "Select Year From Immo_PetitMateriel Where IdExploitation = '" + mIdExpl + "';";
                List<int> listyear = SQlQueryExecuter.RunQueryReaderInt("Year", query);

                string[] valueToAdd = new string[11];
                int index = 1;
                valueToAdd[0] = "";

                foreach (int year in listyear)
                {
                    query = "Select Value From Immo_PetitMateriel Where IdExploitation = '" + mIdExpl +
                         "' And Year = '" + year + "';";
                    List<string> listvalue = SQlQueryExecuter.RunQueryReaderStr("Value", query);
                    if (Commun.ListHasValue(listvalue))
                    {
                        valueToAdd[index] = listvalue[0].ToString();
                    }
                    index++;
                }
                valueToAdd[0] = "  Achat";
                table.Rows.Add(valueToAdd);
                valueToAdd[0] = "  Amortissement";
                table.Rows.Add(valueToAdd);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

            return table;
        }

        private DataTable AddAutreImmo(DataTable table)
        {
            try
            {
                table.Rows.Add("  Achat", "", "", "", "", "", "", "", "", "", "");
                for (int i = 1; i <= 10; i++)
                {
                    AddLinePerItem(table, 3, "An" + i, "Achat");
                }
                table.Rows.Add("  Vente", "", "", "", "", "", "", "", "", "", "");
                for (int i = 1; i <= 10; i++)
                {
                    AddLinePerItem(table, 7, "An" + i, "Vente");
                }
                table.Rows.Add("  V. Résiduelle", "", "", "", "", "", "", "", "", "", "");
                for (int i = 1; i <= 10; i++)
                {
                    AddLinePerItem(table, 1, "An" + i, "V. Résiduelle");
                }
                table.Rows.Add("  Amortissement", "", "", "", "", "", "", "", "", "", "");
                for (int i = 1; i <= 10; i++)
                {
                    AddLinePerItem(table, 2, "An" + i, "Amortissement");
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return table;
        }

        private DataTable AddLinePerItem(DataTable table, int NoItem, string An, string name1stColumn)
        {
            try
            {
                string query = "Select " + An + " From Agri_ImmoGlobal Where NoItem = '" + NoItem + "';";
                List<string> listAchat = SQlQueryExecuter.RunQueryReaderStr(An, query);
                double previousValue = 0;
                string NoYear = An.Substring(2);
                int columnindex;
                int.TryParse(NoYear, out columnindex);
                int year = DateTime.Now.Year;
                year = year + columnindex - 1;
                string valueToAdd = "";
                double valueTot = 0;

                foreach (string value in listAchat)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        if (row.ItemArray[0].ToString().Contains(name1stColumn))
                        {
                            if (name1stColumn.Contains("Résiduelle"))
                            {
                                if (!An.Contains("1"))
                                {
                                    double.TryParse(row.ItemArray[columnindex - 1].ToString(), out previousValue);
                                }
                                double.TryParse(value, out valueTot);
                                previousValue = previousValue + valueTot;
                                valueToAdd = previousValue.ToString();
                            }
                            else valueToAdd = value;
                            row.SetField<string>(year.ToString(), valueToAdd);
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return table;
        }

        private DataTable AddRecaplines(DataTable table)
        {
            try
            {
                table.Rows.Add("  Achat", "", "", "", "", "", "", "", "", "", "");
                AddItemRecap(table, "Achat", 1, 4, 9);
                table.Rows.Add("  Vente", "", "", "", "", "", "", "", "", "", "");
                AddItemRecap(table, "Vente", 5, -1, 10);
                table.Rows.Add("  V. Résiduelle", "", "", "", "", "", "", "", "", "", "");
                AddItemRecap(table, "V. Résiduelle", 6, -1, 11);
                table.Rows.Add("  Amortissement", "", "", "", "", "", "", "", "", "", "");
                AddItemRecap(table, "Amortissement", 7, 2, 12);

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return table;
        }

        private string Get10An()
        {
            string an = "";
            try
            {
                for (int i = 1; i <= 10; i++)
                {
                    an = an + "An" + i + ",";
                }
                int index = an.LastIndexOf(',');
                an = an.Remove(index);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return an;
        }

        private void AddItemRecap(DataTable table, string name1stcolumn, int index1, int index2, int rowindex)
        {
            try
            {
                double value1 = 0;
                double value2 = 0;
                double valueTot = 0;
                int columnindex = 0;
                bool first = true;
                string valueToAdd = "";
                foreach (DataColumn column in table.Columns)
                {
                    if (first)
                    {
                        first = false;
                        columnindex++;
                        continue;
                    }
                    if (columnindex == table.Columns.Count) break;
                    double.TryParse(table.Rows[index1].ItemArray[columnindex].ToString(), out value1);
                    if (index2 > 0)
                    {
                        double.TryParse(table.Rows[index2].ItemArray[columnindex].ToString(), out value2);
                    }
                    valueTot = value1 + value2;
                    if (valueTot == 0) valueToAdd = " ";
                    else valueToAdd = valueTot.ToString();
                    table.Rows[rowindex].SetField<string>(column.ColumnName, valueToAdd);
                    columnindex++;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void dataGridView1_CellFormattingImmo(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (mColorSetCount < 1000)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                if (e.RowIndex == 0)
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
                if (e.RowIndex == 1 && e.RowIndex == 2)
                {
                    row.DefaultCellStyle.BackColor = Color.AliceBlue;
                }
                if (e.RowIndex == 3)
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
                if (e.RowIndex > 3 && e.RowIndex <= 7)
                {
                    row.DefaultCellStyle.BackColor = Color.AliceBlue;
                }
                if (e.RowIndex == 8)
                {
                    row.DefaultCellStyle.BackColor = Color.LightBlue;
                }
                if (e.RowIndex > 8)
                {
                    row.DefaultCellStyle.BackColor = Color.AliceBlue;
                }
                mColorSetCount++;
            }
        }

        #endregion

        #region EtatSortie

        private void LoadEtatSortie()
        {
            panelEtatSortie.Visible = true;
            string query = "Select Item,Def_Categ.Nom,Famille,EtatSortie.ID From EtatSortie " +
                " Join Def_Categ on EtatSortie.IdDefCateg = Def_Categ.IdDefCateg" +
                " Join Def_EtatSortie on EtatSortie.IdDefEtatSortie = Def_EtatSortie.ID" +
                " WHERE EtatSortie.Nom = '" + FilterEtatSortieName + "';";
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(query, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
            adapter.Fill(EtatSortieTable);
            dataGridView1.DataSource = EtatSortieTable;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.ReadOnly = true;
        }

        private void buttonListe2_Click(object sender, EventArgs e)
        {
            ShowCommonFormResult showCommonFormResult = new ShowCommonFormResult(null, dataGridView1, "EtatSortie", EtatSortieTable);
            showCommonFormResult.buttonDicoClick(this, e);
        }

        private void buttonRemove2_Click(object sender, EventArgs e)
        {
            try
            {
                DataGridViewSelectedRowCollection UserSelectedRow = dataGridView1.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    if (!listeRecordToDelete.Contains(GetId(row.Index))) listeRecordToDelete.Add(GetId(row.Index));
                    dataGridView1.Rows.RemoveAt(row.Index);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void SaveEtatSortie()
        {
            try
            {
                string query = "";
                int IdDefEtatSortie = SQLQueryBuilder.FindId("Def_EtatSortie", "ID", "Nom", FilterDefEtatSortieName);

                foreach (int index in listeRecordToDelete)
                {
                    query = SQLQueryBuilder.DeleteQuery(index, "EtatSortie");
                    SQlQueryExecuter.RunQuery(query);
                }

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    query = "Select * From EtatSortie Where IDdefEtatSortie = '" + IdDefEtatSortie + "' AND " +
                        " Nom = '" + FilterEtatSortieName + "' AND IdDefCateg = null;";
                    List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);

                    if (Commun.ListHasValue(list))
                    {
                        query = "Update EtatSortie Set Item ='" + row.Cells[0].Value.ToString() +
                            "',IdDefCateg = '" + SQLQueryBuilder.FindId("Def_Categ", "IdDefCateg", "Nom", row.Cells[1].Value.ToString()) +
                            "', Famille = '" + row.Cells[2].Value.ToString() +
                            "' Where ID = '" + list[0] + "';";
                    }
                    else
                    {
                        query = "Select * From EtatSortie Where IDdefEtatSortie = '" + IdDefEtatSortie + "' AND " +
                        " Nom = '" + FilterEtatSortieName + "' And Item = '" + row.Cells[0].Value.ToString() + "';";
                        List<int> list2 = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (!Commun.ListHasValue(list2))
                        {
                            query = "Insert into EtatSortie (Item,IdDefCateg,Famille,IDdefEtatSortie,Nom) VALUES ('" + row.Cells[0].Value.ToString() +
                           "','" + SQLQueryBuilder.FindId("Def_Categ", "IdDefCateg", "Nom", row.Cells[1].Value.ToString()) +
                           "','" + row.Cells[2].Value.ToString() + "','" + IdDefEtatSortie + "','" + FilterEtatSortieName + "');";
                        }
                    }
                    SQlQueryExecuter.RunQuery(query);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonOK2_Click(object sender, EventArgs e)
        {
            SaveEtatSortie();
            this.Close();
        }

        private void buttonSave2_Click(object sender, EventArgs e)
        {
            SaveEtatSortie();
        }

        private void buttonResult_Click(object sender, EventArgs e)
        {
            StandardForm form = new StandardForm(0, "Resultat EtatSortie", "", dataGridView1);
            form.ShowDialog();
        }

        #endregion

        #region Resultat EtatSortie

        private void LoadResultatEtatSortie(DataGridView dgv)
        {

            string param1 = "";
            string param2 = "";
            DataTable ResultTable = new DataTable();
            ResultTable = AddYearToTable(ResultTable);

            foreach (DataGridViewRow row in dgv.Rows)
            {
                List<string> list = GetTableFromDataGridView(row);
                string table = list[0];
                if (list.Count > 1) param1 = list[1];
                if (list.Count > 2) param2 = list[2];
                
                string element = GetElementFromDataGridView(row);

                string fieldvalue = GetField(table, element);

                string fieldelement = GetField2(table, element);

                bool canEvolve = GetEvolutionOfElement(table, element);

                if (canEvolve)
                {
                    int idexpl = Commun.GetIdExpl();
                    string[] newvalue = new string[12];
                    newvalue[0] = element;
                    string query = "Select An_0 from Agri_DefSim Where IdExploitation ='" + idexpl + "';";
                    List<int> listAn0 = SQlQueryExecuter.RunQueryReaderInt("An_0", query);

                    if (table == Translation.Translate("Misc Incomes", langue)
                        || table == Translation.Translate("Misc Expenses", langue)
                        || table == Translation.Translate("Family Expenses", langue)
                        || table == Translation.Translate("Family Incomes", langue))
                    {
                        newvalue[1] = "€";
                        for (int i = 0; i < 10; i++)
                        {
                            query = " Select * From Result_Calcul Where Table_Origine = '" + table +
                            "' AND Nom = '" + element + "' AND IdExploitation ='" + mIdExpl +
                            "' AND Annee = " + (listAn0[0] + i) + "' ;";
                            List<string> list3 = SQlQueryExecuter.RunQueryReaderStr("Valeur", query);
                            newvalue[i + 2] = list3[0];
                        }
                    }

                    ResultTable.Rows.Add(newvalue);

                    //if (Commun.ListHasValue(listAn0))
                    //{
                    //    for (int i = 0; i < 10; i++)
                    //    {
                    //        query = "Select " + fieldvalue + " FROM " + table + " WHERE " + fieldelement + " ='" + element +
                    //            "' AND An" + (i + 1) + " = '" + (listAn0[0] + i) + "';";
                    //        List<string> list3 = SQlQueryExecuter.RunQueryReaderStr(fieldvalue, query);
                    //        newvalue[i + 1] = list3[0];
                    //    }
                    //}
                    //ResultTable.Rows.Add(newvalue);


                }
                else
                {
                    string query = "Select " + fieldvalue + " FROM " + table + "WHERE " + fieldelement + " ='" + element + "';";
                    List<string> list4 = SQlQueryExecuter.RunQueryReaderStr(fieldvalue, query);
                    ResultTable.Rows.Add(new string[] { element, list4[0], list4[0], list4[0], list4[0], list4[0], list4[0], list4[0], list4[0], list4[0], list4[0] });
                }
            }
        }

        private string GetField2(string table, string element)
        {
            throw new NotImplementedException();
        }

        private string GetField(string table, string element)
        {
            throw new NotImplementedException();
        }

        private DataTable AddYearToTable(DataTable resultTable)
        {
            throw new NotImplementedException();
        }

        private bool GetEvolutionOfElement(string table, string element)
        {
            if (table == Translation.Translate("Misc Incomes", langue)
                  || table == Translation.Translate("Misc Expenses", langue)
                  || table == Translation.Translate("Family Incomes", langue)
                  || table == Translation.Translate("Family Expenses", langue))
            {
                return true;
            }
            else
                return false;
        }

        private string GetElementFromDataGridView(DataGridViewRow row = null,
            string table = "",string param1 = "",string param2 = "")
        {

            if (table == Translation.Translate("Misc Incomes", langue)
                || table == Translation.Translate("Misc Expenses", langue)
                || table == Translation.Translate("Family Incomes", langue)
                || table == Translation.Translate("Family Expenses", langue)) 
            {
                string query = "Select Nom from " + table + " Where Nom = '" + row.Cells[3].Value.ToString() +
                    "' And Expense = '" + param1 + "' And Family = '" + param2 +"';";
                return SQlQueryExecuter.RunQueryReaderStr("Nom", query)[0];
            }
           
            return "";
        }

        private List<string> GetTableFromDataGridView(DataGridViewRow row)
        {
           
            string typetable = row.Cells[3].Value.ToString();
            List<string> returnList = new List<string>();

            if (typetable == Translation.Translate("Misc Expenses",langue))
            {
                returnList.Add("Expense_Income");
                returnList.Add("1");
                returnList.Add("0");
            }
            if (typetable == Translation.Translate("Misc Incomes", langue)) 
            {
                returnList.Add("Expense_Income");
                returnList.Add("0");
                returnList.Add("0");
            }
            if (typetable == Translation.Translate("Family Expenses", langue)) 
            {
                returnList.Add("Expense_Income");
                returnList.Add("1");
                returnList.Add("1");
            }
            if (typetable == Translation.Translate("Family Incomes", langue)) 
            {
                returnList.Add("Expense_Income");
                returnList.Add("1");
                returnList.Add("0");
            }

            return returnList;
        }

        #endregion
    
        #region CEG

        private void LoadGridCEG()
        {

            try
            {

                DataTable dtbl2 = new DataTable();
                DataTable dtblTemp = new DataTable();
                dtbl2.Columns.Add(" ");
                dtblTemp.Columns.Add(" ");

                int year = GetYear();
                for (int i = year; i < year + 10; i++)
                {
                    dtbl2.Columns.Add(i.ToString());
                    dtblTemp.Columns.Add(i.ToString());
                }

                dtbl2.Rows.Add((new object[] { Translation.Translate("Products", langue) }));
                dtblTemp.Rows.Add((new object[] { Translation.Translate("Products", langue) }));
                KeyValuePair<int, DataTable> value = Calcul.GetElementRec_Dep(dtblTemp, "Produits", 0, mNbProduct);
                dtblTemp = value.Value;
                mNbProduct = value.Key;
                dtbl2 = Calcul.GetTotalElementRec_Dep(dtblTemp, "Produits", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);


                //dtbl.Rows.Add((new object[] { Translation.UpdateCaption("Misc Income", langue) }));
                dtblTemp.Rows.Add((new object[] { Translation.Translate("Misc Income", langue) }));
                value = Calcul.GetElementRec_Dep(dtblTemp, "Misc income", 0, mNbMiscInc,0,0);
                dtblTemp = value.Value;
                mNbMiscInc = value.Key;
                dtbl2 = Calcul.GetTotalElementRec_Dep(dtblTemp, "Misc income", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                
                //dtbl.Rows.Add((new object[] { Translation.UpdateCaption("Cost", langue) }));
                dtblTemp.Rows.Add((new object[] { Translation.Translate("Cost", langue) }));
                value = Calcul.GetElementRec_Dep(dtblTemp, "Charges", 0, mNbChargeOpe);
                dtblTemp = value.Value;
                mNbChargeOpe = value.Key;
                dtbl2 = Calcul.GetTotalElementRec_Dep(dtblTemp, "Charges Operationnelles", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);


                //dtbl.Rows.Add((new object[] { Translation.UpdateCaption("Misc expense", langue) }));
                dtblTemp.Rows.Add((new object[] { Translation.Translate("Misc expense", langue) }));
                value = Calcul.GetElementRec_Dep(dtblTemp, "Misc Expense", 0, mNbMiscExp,0,1);
                dtblTemp = value.Value;
                mNbMiscExp = value.Key;
                dtbl2 = Calcul.GetTotalElementRec_Dep(dtblTemp, "Misc Expense", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);


                dtblTemp.Rows.Add((new object[] { Translation.Translate("Strutural Cost", langue) }));
                value = Calcul.GetElementRec_Dep(dtblTemp, "Charges", 1, mNbChargeOpe);
                dtblTemp = value.Value;
                mNbChargeOpe = value.Key;
                dtbl2 = Calcul.GetTotalElementRec_Dep(dtblTemp, "Charges Structurelles", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);


                dtblTemp.Rows.Add((new object[] { Translation.Translate("Frais Fin LT", langue) }));
                value = Calcul.GetElementProdFinance(dtblTemp, mNbFraisFinLT, "Agri_EmpLT");
                dtblTemp = value.Value;
                mNbFraisFinLT = value.Key;
                dtbl2 = Calcul.GetTotalElementRec_Dep(dtblTemp, "Frais Fin LT", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtblTemp.Rows.Add((new object[] { Translation.Translate("Frais Fin CT", langue) }));
                value = Calcul.GetElementProdFinance(dtblTemp, mNbFraisFinCT, "Agri_EmpCT");
                dtblTemp = value.Value;
                mNbFraisFinCT = value.Key;
                dtbl2 = Calcul.GetTotalElementRec_Dep(dtblTemp, "Frais Fin CT", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                //dtbl = Calcul.SetTotalItem(Translation.UpdateCaption("Product", langue),dtblTemp,dtbl);
                //dtbl = Calcul.SetTotalItem(Translation.UpdateCaption("Cost", langue),dtblTemp, dtbl);
                //dtbl = Calcul.SetTotalItem(Translation.UpdateCaption("Structural Cost", langue),dtblTemp, dtbl);

                dtbl2 = SetResult(dtbl2);

                dataGridView1.DataSource = dtbl2;

                AdjustGridViewDisplayForReport();

                dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting2);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        private DataTable SetResult(DataTable table1)
        {
            try
            {
                double[] tempArrDouble = new double[11];
                double[] resultArrDouble = new double[11];
                string[] resulArrString = new string[11];
                int index = 1;

                foreach (DataRow row in table1.Rows)
                {
                    if (row.ItemArray[0].ToString().Contains("TOTAL"))
                    {
                        switch (index)
                        {
                            case 1:
                                for (int i = 1; i < 11; i++)
                                {
                                    resultArrDouble[i] = resultArrDouble[i] + tempArrDouble[i];
                                }
                                index++;
                                tempArrDouble = new double[11];
                                break;
                            case 2:
                                for (int i = 1; i < 11; i++)
                                {
                                    resultArrDouble[i] = resultArrDouble[i] + tempArrDouble[i];
                                }
                                index++;
                                tempArrDouble = new double[11];
                                break;
                            case 3:
                                for (int i = 1; i < 11; i++)
                                {
                                    resultArrDouble[i] = resultArrDouble[i] - tempArrDouble[i];
                                }
                                index++;
                                tempArrDouble = new double[11];
                                break;
                            case 4:
                                for (int i = 1; i < 11; i++)
                                {
                                    resultArrDouble[i] = resultArrDouble[i] - tempArrDouble[i];
                                }
                                index++;
                                tempArrDouble = new double[11];
                                break;
                            case 5:
                                for (int i = 1; i < 11; i++)
                                {
                                    resultArrDouble[i] = resultArrDouble[i] - tempArrDouble[i];
                                }
                                index++;
                                tempArrDouble = new double[11];
                                break;
                        }
                        if (index > 5)
                        {
                            resulArrString[0] = "Resultat";
                            for (int i = 1; i < 11; i++)
                            {
                                resulArrString[i] = resultArrDouble[i].ToString();
                            }
                            table1.Rows.Add(resulArrString);
                            break;
                        }
                    }
                    else
                    {
                        for (int i = 1; i < 11; i++)
                        {
                            tempArrDouble[i] = tempArrDouble[i] + Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return table1;
        }

        #endregion

        #region Depense/recette Grand Poste

        private void LoadGridDepRecGP()
        {

            try
            {

                DataTable dtbl = new DataTable();
                dtbl.Columns.Add(" ");

                int year = GetYear();
                for (int i = year; i < year + 10; i++)
                {
                    dtbl.Columns.Add(i.ToString());
                }

                dtbl.Rows.Add((new object[] { Translation.Translate("Products", langue) }));
                KeyValuePair<int, DataTable> value = Calcul.GetElementRec_DepGP(dtbl, "Produits", 0, mNbProduct);
                dtbl = value.Value;
                mNbProduct = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Produits", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);


                dtbl.Rows.Add((new object[] { Translation.Translate("Cost", langue) }));
                value = Calcul.GetElementRec_DepGP(dtbl, "Charges", 0, mNbChargeOpe);
                dtbl = value.Value;
                mNbChargeOpe = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Charges Operationnelles", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl = Calcul.GetMarge(dtbl);

                dtbl.Rows.Add((new object[] { Translation.Translate("Strutural Cost", langue) }));
                value = Calcul.GetElementRec_DepGP(dtbl, "Charges", 1, mNbChargeStruc);
                dtbl = value.Value;
                mNbChargeStruc = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Charges Structurelles", mNbProduct, mNbChargeOpe, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc, mNbChargeStruc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Mics Income", langue) }));
                value = Calcul.GetElementExpIncGP(dtbl, "Expense_Income", Translation.Translate("Mics Income", langue), mNbMiscInc,0,0);
                dtbl = value.Value;
                mNbMiscInc = value.Key;
                
                dtbl.Rows.Add((new object[] { Translation.Translate("Mics Expense", langue) }));
                value = Calcul.GetElementExpIncGP(dtbl, "Expense_Income", Translation.Translate("Mics Expense", langue), mNbMiscExp, 0, 1);
                dtbl = value.Value;
                mNbMiscExp = value.Key;
               
                dtbl.Rows.Add((new object[] { Translation.Translate("Family Income", langue) }));
                value = Calcul.GetElementExpIncGP(dtbl, "Expense_Income", Translation.Translate("Family Income", langue), mNbFamInc, 1, 0);
                dtbl = value.Value;
                mNbFamInc = value.Key;
               
                dtbl.Rows.Add((new object[] { Translation.Translate("Family Expense", langue) }));
                value = Calcul.GetElementExpIncGP(dtbl, "Expense_Income", Translation.Translate("Family Expense", langue), mNbFamExp, 1, 1);
                dtbl = value.Value;
                mNbFamExp = value.Key;

                dtbl.Rows.Add((new object[] { Translation.Translate("Immobilisation", langue) }));
                dtbl = Calcul.Immobilisation(dtbl);

                dtbl = Calcul.GetSolde(dtbl);
                dtbl = Calcul.GetSoldCumule(dtbl);

                dataGridView1.DataSource = dtbl;

                AdjustGridViewDisplayForReport();

                dataGridView1.CellFormatting += new DataGridViewCellFormattingEventHandler(dataGridView1_CellFormatting2);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        #endregion

        #region Fonction Commune

        private void AdjustGridViewDisplayForReport()
        {
            try
            {
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[0].Width = 230;
                dataGridView1.Columns[0].SortMode = DataGridViewColumnSortMode.NotSortable;

                dataGridView1.AllowUserToAddRows = false;
                dataGridView1.AllowUserToResizeColumns = false;
                dataGridView1.AllowUserToResizeRows = false;
                dataGridView1.ReadOnly = true;

                this.WindowState = FormWindowState.Maximized;
                dataGridView1.AllowUserToOrderColumns = false;

                for (int i = 1; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = 80;
                    dataGridView1.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
                }
                dataGridView1.CellBorderStyle = DataGridViewCellBorderStyle.None;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Size = new Size(panel1.Size.Width, panel1.Size.Height - 30);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }



        private void SetIdExpl()
        {
            string query = "Select ID From Exploitation Where Encours = '1';";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
            if (Commun.ListHasValue(list))
            {
                mIdExpl = list[0];
            }
        }

      

        private int GetIdCurrentActivite()
        {
            string query = SQLQueryBuilder.SelectQuery("Activite", "ID", "Where Encours = '1';");
            List<int> listId = SQlQueryExecuter.RunQueryReaderInt("ID", query);
            if (Commun.ListHasValue(listId)) return listId[0];
            else return 0;
        }

        private bool IsNumeric(string test)
        {
            double value;
            return double.TryParse(test, out value);
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Graphics graphic = dataGridView1.CreateGraphics();
                bmp = new Bitmap(dataGridView1.Size.Width, dataGridView1.Size.Height, graphic);
                Graphics gr = Graphics.FromImage(bmp);
                gr.CopyFromScreen(dataGridView1.Location.X, dataGridView1.Location.Y, 0, 0, dataGridView1.Size);
                printDialog1.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(bmp, 0, 0);
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Export.RunExportTable(dataGridView1, Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            Export.RunExportTable(dataGridView1, Text);
        }

        private void buttonNotes_Click(object sender, EventArgs e)
        {
            try
            {
                NotesForm form = new NotesForm(Text);
                form.ShowDialog();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        /// <summary>
        /// Select the color in the database to put in the mDataGridView
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

        private void ManageColor()
        {
            if (color1 != null && color2 != null)
            {
                Commun.Setbackground(dataGridView1, color1, color2);
            }
        }

        #endregion
    }
}
