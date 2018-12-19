using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using OMEGA.Data_Classes;
using System.Data.SQLite;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using OMEGA.SQLQuery;
using System.Drawing;

namespace OMEGA.Forms.ResultUserControl
{
    internal partial class UserControlProduct : UserControl
    {
        private DataTable Agri_Datatable = new DataTable();
        private ShowCommonFormResult showCommonForm;
        private int IdExp = Commun.GetIdExpl();
        private double mValueToReport;
        private List<int> ListeRecordToDelete = new List<int>();
        private Form resultForm;
        private Color color1 = Commun.GetColor("ARVB1");
        private Color color2 = Commun.GetColor("ARVB2");
        private Color color3 = Commun.GetColor("ARVB3");
        private int An0;
        private int compteur = 0;
        private int langue = Properties.Settings.Default.Langue;
        private List<AnneePhase> MegaList = new List<AnneePhase>();
        
        internal UserControlProduct(Form form)
        {
            InitializeComponent();
            dataGridView1.Font = Commun.GetCurrentFont();
            showCommonForm = new ShowCommonFormResult(textBox1, dataGridView1, "Produit");
            buttonDico.Click += new EventHandler(showCommonForm.buttonDicoClick);
            buttonCalcul.Click += new EventHandler(showCommonForm.buttonCalculClick);
            textBox1.KeyPress += new KeyPressEventHandler(showCommonForm.TextBox_KeyPress);
            dataGridView1.CellClick += dataGridView1_CellClick;
            dataGridView1.CellEndEdit += dataGridView_CellEndEdit;
            buttonReport.Click += new EventHandler(showCommonForm.buttonReportClick);
            LoadGrid();
            resultForm = form;
            ManageColor();
            Translate();

        }


        private void buttonList_Click(object sender, EventArgs e)
        {
            string query = "select ID,Nom From Produits";
            string table = "Produits";
            ListeForm listeform = new ListeForm(query, table, "Agri_Produits", null, null, dataGridView1, Agri_Datatable);
            listeform.Show();
            ManageColor();
            Translate();
        }

        private void LoadGrid()
        {
            try
            {
                MegaList.Clear();
                Agri_Datatable.Clear();
                Agri_Datatable.Columns.Clear();
                string mainquery = ProduitQuery.LoadAgriProduitQuery(IdExp);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(new SQLiteCommand(mainquery, new SQLiteConnection(Properties.Settings.Default.ConnectionString)));
                adapter.Fill(Agri_Datatable);
                Agri_Datatable = AddYearToTable(Agri_Datatable);
                CalculProductQuantity(HasAlea());
                //if (HasAlea()) Agri_Datatable = SetAleaToValuesToTable(Agri_Datatable);
                Agri_Datatable = AddValuesToTable(Agri_Datatable);
                dataGridView1.DataSource = Agri_Datatable;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.AllowUserToAddRows = false;
                string query = "Select Formule From Agri_Produits" +
                " Where IdExploitations = '" + IdExp + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Formule", query);
                if (Commun.ListHasValue(list))
                {
                    textBox1.Text = list[0];
                }
                //buttonSave.PerformClick();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }


        private void Translate()
        {
            try
            {
                buttonCancel.Text = Translation.Translate("Cancel", langue);
                buttonRemove.Text = Translation.Translate("Remove", langue);
                buttonDico.Text = Translation.Translate("Dico", langue);
                buttonSave.Text = Translation.Translate("Save", langue);
                // buttonAdd1.Text = Translation.UpdateCaption("Add", langue);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        /// <summary>
        /// Set the color of the gridview
        /// </summary>
        private void ManageColor()
        {
            if (color1 != null && color2 != null)
            {
                Commun.Setbackground(dataGridView1, color1, color2);
                string query = SQLQuery.SQLQueryBuilder.UpdateColorQuery(color1, color2, color3);
                if (!(color1.Name == "0" && color2.Name == "0")) SQlQueryExecuter.RunQuery(query);
            }
        }

        private DataTable AddYearToTable(DataTable table)
        {
            try
            {
                string query = "Select An_0 From Agri_DefSim Where IdExploitations = '" + IdExp + "'";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
                if (Commun.ListHasValue(list))
                {
                    An0 = list[0] + (compteur * 10);
                }
                else An0 = DateTime.Now.Year;
                for (int i = 1; i <= 10; i++)
                {
                    table.Columns.Add(An0.ToString());
                    An0++;
                }
                An0 = An0 - 10;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return table;
        }

        private void CalculProductQuantity(bool hasAlea)
        {
            try
            {
                int An0Origine = Commun.GetYear();
                string query = "Select IdProduits From Agri_Produits WHERE IdExploitations = '" + IdExp + "';";
                List<int> listProd = SQlQueryExecuter.RunQueryReaderInt("IdProduits", query);
                foreach (int idProd in listProd)
                {
                    query = ProduitQuery.SelectProductOftheActivityQuery(IdExp, idProd);
                    List<int> listAct = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);
                    if (Commun.ListHasValue(listAct))
                    {
                        query = "Select IdType From activite Where ID ='" + listAct[0] + "';";
                        List<int> listType = SQlQueryExecuter.RunQueryReaderInt("IdType", query);
                        int type = listType[0];
                        for (int i = 0; i < 10; i++)
                        {
                            query = ProduitQuery.SelectResult_calculQuery(IdExp, idProd, (An0 + i));
                            List<int> listResul = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                           switch (type)
                           {
                                case 1: // annual
                                        if (Commun.ListHasValue(listResul))
                                        {
                                            query = ProduitQuery.UpdateAgriProduitValueQuery((An0 + i), GetValeurAnnuel(idProd, listAct[0], (An0 + i), type), listResul[0]);
                                        }
                                        else
                                        {
                                            //if (An0 < An0Origine + 10 && An0 > An0Origine - 10)
                                            {
                                                query = ProduitQuery.InsertAgriProduitValueQuery(SQLQueryBuilder.FindName("Produits", "Nom", "ID", idProd), (An0 + i), GetValeurAnnuel(idProd, listAct[0], (An0 + i), type),IdExp);
                                            }
                                        }
                                    break;
                                case 4: // perenne
                                    if (MegaList.Count == 0)
                                    {
                                        query = "Select * From Ate_CatPhase Where nom = '" + SQLQueryBuilder.FindName("Activite", "Nom", "ID", listAct[0]) + "';";
                                        string[] reader = SQlQueryExecuter.RunQueryReader(query);
                                        AnneePhase anneePhase = new AnneePhase();

                                        int index = 1;
                                        for (int j = 4; j < reader.Length; j++)
                                        {
                                            if (reader[j] == "") break;
                                            int.TryParse(reader[j], out int PhValue);
                                            for (int k = 1; k <= PhValue; k++)
                                            {
                                                anneePhase = new AnneePhase();
                                                anneePhase.disponible = true;
                                                anneePhase.numPhase = j-3;
                                                MegaList.Add(anneePhase);
                                            }
                                        }
                                    }

                                    if (Commun.ListHasValue(listResul))
                                    {
                                        query = ProduitQuery.UpdateAgriProduitValueQuery((An0 + i), GetValeurPerenne2(idProd, listAct[0], (An0 + i)), listResul[0]);
                                    }
                                    else
                                    {
                                        if (An0 < An0Origine + 10 && An0 > An0Origine - 10)
                                        {
                                            query = ProduitQuery.InsertAgriProduitValueQuery(SQLQueryBuilder.FindName("Produits", "Nom", "ID", idProd), (An0 + i), GetValeurPerenne2(idProd, listAct[0], (An0 + i)),IdExp);
                                        }
                                    }
                                    break;
                                case 6: // pluriannuelle
                                    if (Commun.ListHasValue(listResul))
                                    {
                                        query = ProduitQuery.UpdateAgriProduitValueQuery((An0 + i), GetValeurPluriannuelle(idProd, listAct[0], (An0 + i)), listResul[0]);
                                    }
                                    else
                                    {
                                        if (An0 < An0Origine + 10 && An0 > An0Origine - 10)
                                        {
                                            query = ProduitQuery.InsertAgriProduitValueQuery(SQLQueryBuilder.FindName("Produits", "Nom", "ID", idProd), (An0 + i), GetValeurPluriannuelle(idProd, listAct[0], (An0 + i)),IdExp);
                                        }
                                    }
                                    break;
                                case 5: // animaux
                                    if (Commun.ListHasValue(listResul))
                                    {
                                        query = ProduitQuery.UpdateAgriProduitValueQuery((An0 + i), GetValeurAnnuel(idProd, listAct[0], (An0 + i), type), listResul[0]);
                                    }
                                    else
                                    {
                                        if (An0 < An0Origine + 10 && An0 > An0Origine - 10)
                                        {
                                            query = ProduitQuery.InsertAgriProduitValueQuery(SQLQueryBuilder.FindName("Produits", "Nom", "ID", idProd), (An0 + i), GetValeurAnnuel(idProd, listAct[0], (An0 + i), type),IdExp);
                                        }
                                           
                                    }
                                    break;
                           }
                            SQlQueryExecuter.RunQuery(query);
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


        private string GetValeurAnnuel(int id,int IdAct,int year,int type)
        {
            try
            {

                string query = "";
                double value = 0;
                double Qte = 0;
                double Qteprod = 0;
                double surface = 1;
                string tableAgri = "";

                query = "Select Prix From Produits Where ID = '" + id + "';";
                List<string> listPrice = SQlQueryExecuter.RunQueryReaderStr("Prix", query);
                double price = 1;
                if (Commun.ListHasValue(listPrice)) price = Commun.GetDoubleFromString(listPrice[0]);

                if (type == 1) tableAgri = "Agri_Assol";
                if (type == 5) tableAgri = "Agri_Animaux";
                query = SQLQueryBuilder.FindValueResultCalculQuery(year, SQLQueryBuilder.FindName("Activite", "Nom", "ID", IdAct), tableAgri, IdExp);
                List<string> listSurface = SQlQueryExecuter.RunQueryReaderStr("Valeur", query);
                if (Commun.ListHasValue(listSurface)) surface = Commun.GetDoubleFromString(listSurface[0]);

                query = "Select * From PiedHa Where IdActivite = '" + IdAct + "';";
                List<int> listIdPiedHa = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (Commun.ListHasValue(listIdPiedHa))
                {
                    query = "Select Ph1 From PiedHa WHere IdActivite = '" + IdAct + "';";
                    List<string> listQtePiedHa = SQlQueryExecuter.RunQueryReaderStr("Ph1", query);
                    if (Commun.ListHasValue(listQtePiedHa)) Qte = Commun.GetDoubleFromString(listQtePiedHa[0]);
                    query = "Select Ph1 From Item_Pied WHere IdActivite = '" + IdAct + "' and IdProduits  = '"+id+"';";
                    List<string> listQteProdPied = SQlQueryExecuter.RunQueryReaderStr("Ph1", query);
                    if (Commun.ListHasValue(listQteProdPied)) Qteprod = Commun.GetDoubleFromString(listQteProdPied[0]);
                    value = Qte * Qteprod * price * surface;
                }
                else
                {
                    query = "Select Quantite_1 From Prod_Quantite WHere IdProduits = '" + id + "'AND IdActivite = '" + IdAct + "';";
                    List<string> listQte = SQlQueryExecuter.RunQueryReaderStr("Quantite_1", query);
                    if (Commun.ListHasValue(listQte)) Qte = Commun.GetDoubleFromString(listQte[0]);
                    value = Qte * price * surface;
                }
                return value.ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }

        private string GetValeurPerenne(int id, int IdAct, int Annee)
        {
            try
            {
                string query = "Select Prix From Produits Where ID = '" + id + "';";
                List<string> listPrice = SQlQueryExecuter.RunQueryReaderStr("Prix", query);
                double price = Commun.GetDoubleFromString(listPrice[0]);
                int AnnPlant = 0;
                int AnnArr = 0;
                double surface = 1;
                double qteprod = 1;
                query = "Select * From Agri_Perenne Where IdActivite = '" + IdAct + "';";
                List<int> listAn = SQlQueryExecuter.RunQueryReaderInt("AnPlant", query);
                List<int> listArr = SQlQueryExecuter.RunQueryReaderInt("AnArr", query);
                List<double> listSur = SQlQueryExecuter.RunQueryReaderDouble("Surface", query);
                if (Commun.ListHasValue(listAn) && Commun.ListHasValue(listArr) && Commun.ListHasValue(listSur))
                {
                    AnnPlant = listAn[0];
                    AnnArr = listArr[0];
                    surface = listSur[0];
                }
                if (AnnArr <= Annee || AnnArr <= 0) return "";
                query = "Select * From Ate_CatPhase Where nom = '" + SQLQueryBuilder.FindName("Activite", "Nom", "ID", IdAct) + "';";
                string[] reader = SQlQueryExecuter.RunQueryReader(query);
                int index = 1;
                for (int i = 4; i < reader.Length; i++)
                {
                    if (reader[i] == "") break;
                    int.TryParse(reader[i], out int Ph);
                    if (Annee < AnnPlant + Ph)
                    {
                        break;
                    }
                    index++;
                }
                
                double qte = 1;
                if (index < 40)
                {
                    query = "Select * From PiedHa Where IdActivite = '" + IdAct + "';";
                    List<int> listIdPiedHa = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(listIdPiedHa))
                    {
                        query = "Select Ph" + index + " From Item_Pied Where IdProduits ='" + id + "';";
                        List<string> listValue = SQlQueryExecuter.RunQueryReaderStr("Ph" + index, query);
                        if (Commun.ListHasValue(listValue))
                        {
                            qteprod = Commun.GetDoubleFromString(listValue[0]);
                        }
                        query = "Select Ph" + index + " From PiedHa Where IdActivite ='" + IdAct + "';";
                         listValue = SQlQueryExecuter.RunQueryReaderStr("Ph" + index, query);
                        if (Commun.ListHasValue(listValue))
                        {
                            qte = Commun.GetDoubleFromString(listValue[0]);
                        }
                    }
                    else
                    {
                        query = "Select Ph" + index + " From Prod_Perenne Where IdProduits ='" + id + "';";
                        List<string> listValue = SQlQueryExecuter.RunQueryReaderStr("Ph" + index, query);
                        if (Commun.ListHasValue(listValue))
                        {
                            qte = Commun.GetDoubleFromString(listValue[0]);
                        }
                    }
                } //qteprod = si arbre, sinon défini à 1
                return (qte * price * surface * qteprod).ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }

        private string GetValeurPerenne2(int id, int IdAct, int Annee)
        {
            try
            {
                string query = "Select Prix From Produits Where ID = '" + id + "';";
                List<string> listPrice = SQlQueryExecuter.RunQueryReaderStr("Prix", query);
                double price = Commun.GetDoubleFromString(listPrice[0]);
                int AnnPlant = 0;
                int AnnArr = 0;
                double surface = 1;
                double qteprod = 1;
                double qte = 1;
                query = "Select * From Agri_Perenne Where IdActivite = '" + IdAct + "';";
                List<int> listAn = SQlQueryExecuter.RunQueryReaderInt("AnPlant", query);
                List<int> listArr = SQlQueryExecuter.RunQueryReaderInt("AnArr", query);
                List<double> listSur = SQlQueryExecuter.RunQueryReaderDouble("Surface", query);
                if (Commun.ListHasValue(listAn) && Commun.ListHasValue(listArr) && Commun.ListHasValue(listSur))
                {
                    AnnPlant = listAn[0];
                    AnnArr = listArr[0];
                    surface = listSur[0];
                }
                if (AnnArr <= Annee || AnnArr <= 0) return "";
    
                int numphase = GetNextPhase();

                query = "Select * From PiedHa Where IdActivite = '" + IdAct + "';";
                List<int> listIdPiedHa = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (Commun.ListHasValue(listIdPiedHa))
                {
                    query = "Select Ph" + numphase + " From Item_Pied Where IdProduits ='" + id + "';";
                    List<string> listValue = SQlQueryExecuter.RunQueryReaderStr("Ph" + numphase, query);
                    if (Commun.ListHasValue(listValue))
                    {
                        qteprod = Commun.GetDoubleFromString(listValue[0]);
                    }
                    query = "Select Ph" + numphase + " From PiedHa Where IdActivite ='" + IdAct + "';";
                    listValue = SQlQueryExecuter.RunQueryReaderStr("Ph" + numphase, query);
                    if (Commun.ListHasValue(listValue))
                    {
                        qte = Commun.GetDoubleFromString(listValue[0]);
                    }
                }
                else
                {
                    query = "Select Ph" + numphase + " From Prod_Perenne Where IdProduits ='" + id + "';";
                    List<string> listValue = SQlQueryExecuter.RunQueryReaderStr("Ph" + numphase, query);
                    if (Commun.ListHasValue(listValue))
                    {
                        qte = Commun.GetDoubleFromString(listValue[0]);
                    }
                }
                 //qteprod = si arbre, sinon défini à 1
                return (qte * price * surface * qteprod).ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }


        private int GetNextPhase()
        {
            int tempvalue = 0;
            int index = 0;
            foreach (AnneePhase annee in MegaList)
            {
                if (annee.disponible)
                {
                    tempvalue = annee.numPhase;
                    break;
                }
                index++;
            }
            MegaList[index].disponible = false;
            return tempvalue;
        }

        private string GetValeurPluriannuelle(int id, int IdAct, int Annee)
        {
            try
            {
                string query = "Select Prix From Produits Where ID = '" + id + "';";
                List<string> listPrice = SQlQueryExecuter.RunQueryReaderStr("Prix", query);
                double price = Commun.GetDoubleFromString(listPrice[0]);
                int AnnPlant = 0;
                int AnnArr = 0;
                double surface = 1;
                double qteprod = 1;
                query = "Select * From Agri_Pluriannuelle Where IdActivite = '" + IdAct + "';";
                List<int> listAn = SQlQueryExecuter.RunQueryReaderInt("AnPlant", query);
                List<int> listArr = SQlQueryExecuter.RunQueryReaderInt("AnArr", query);
                List<double> listSur = SQlQueryExecuter.RunQueryReaderDouble("Surface", query);
                if (Commun.ListHasValue(listAn) && Commun.ListHasValue(listArr) && Commun.ListHasValue(listSur))
                {
                    AnnPlant = listAn[0];
                    AnnArr = listArr[0];
                    surface = listSur[0];
                }
                if (AnnArr < An0 && AnnArr <= 0) return "";
                if (Annee > An0 + 4) return "";
                double qte = 1;
                for (int i = 1; i <= 4; i++)
                {
                    query = "Select * From PiedHa Where IdActivite = '" + IdAct + "';";
                    List<int> listIdPiedHa = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (Commun.ListHasValue(listIdPiedHa))
                    {
                        query = "Select Ph" + i + " From Item_Pied Where IdProduits ='" + id + "';";
                        List<string> listValue = SQlQueryExecuter.RunQueryReaderStr("Ph" + i, query);
                        if (Commun.ListHasValue(listValue))
                        {
                            qteprod = Commun.GetDoubleFromString(listValue[0]);
                        }
                        query = "Select Ph" + i + " From PiedHa Where IdActivite ='" + IdAct + "';";
                        listValue = SQlQueryExecuter.RunQueryReaderStr("Ph" + i, query);
                        if (Commun.ListHasValue(listValue))
                        {
                            qte = Commun.GetDoubleFromString(listValue[0]);
                        }
                    }
                    else
                    {
                        query = "Select Ph" + i + " From Prod_Pluriannuelle Where IdProduits ='" + id + "';";
                        List<string> listValue = SQlQueryExecuter.RunQueryReaderStr("Ph" + i, query);
                        if (Commun.ListHasValue(listValue))
                        {
                            qte = Commun.GetDoubleFromString(listValue[0]) + qte;
                        }
                    }
                    if (Annee == An0 + i) break;
                }
                return (qte * qteprod * price * surface).ToString();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }

        private DataTable AddValuesToTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    for (int i = 3; i < 13; i++)
                    {
                        string query = SQLQueryBuilder.FindValueResultCalculQuery(table.Columns[i].ColumnName, row.ItemArray[1].ToString(),  "Agri_Produits", IdExp); 
                        List<string> list = SQlQueryExecuter.RunQueryReaderStr("Valeur", query);
                        if (Commun.ListHasValue(list))
                        {
                            row.SetField<string>(table.Columns[i], list[0].ToString());
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }
            return table;
        }


        private DataTable SetAleaToValuesToTable(DataTable table)
        {
            foreach (DataRow row in table.Rows)
            {
                try
                {
                    int year0 = Commun.GetYear();
                    int indexP = 0;
                    bool aleaP = false;
                    bool aleaQ = false;
                    double[] array = null;
                    double[] array2 = null;
                    double temp = 100;
                    double temp2 = 100;
                    string query2 = "";
                    string query3 = "";
                    List<int> list3 = new List<int>();
                    string query = "Select * From Agri_DefSim Where IdExploitations = '" + IdExp + "';";
                    List<int> listP = SQlQueryExecuter.RunQueryReaderInt("NoPrixProd", query);
                    if(Commun.ListHasValue(listP)) query2 = "Select * From Alea_Item Where ID = '" + listP[0] + "';";
                    if (Commun.ListHasValue(listP)) list3 = SQlQueryExecuter.RunQueryReaderInt("IdProduits", query2);
                    if (Commun.ListHasValue(list3))
                    {
                        if(list3[0] == SQLQueryBuilder.FindId("Produits","ID","Nom", row.ItemArray[1].ToString()))
                        {
                            if (Commun.ListHasValue(listP))
                            {
                                query3 = "Select " + GetAn() + " From Alea_item where ID = '" + listP[0] + "';";
                                array = Commun.CastStringArrayToDouble(SQlQueryExecuter.RunQueryReader(query3));
                                if (Commun.ArrayHasValue(array))
                                {
                                    for (int i = 3; i < 13; i++)
                                    {
                                        if (year0.ToString() == table.Columns[i].ColumnName)
                                        {
                                            indexP = i;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    int indexQ = 0;
                    List<int> list33 = new List<int>();
                    List<int> listQ = SQlQueryExecuter.RunQueryReaderInt("NoQProd", query);
                    if (Commun.ListHasValue(listQ)) query2 = "Select * From Alea_Item Where ID = '" + listQ[0] + "';";
                    if (Commun.ListHasValue(listQ)) list33 = SQlQueryExecuter.RunQueryReaderInt("IdProduits", query2);
                    if (Commun.ListHasValue(list33))
                    {
                        if (list33[0] == SQLQueryBuilder.FindId("Produits", "ID", "Nom", row.ItemArray[1].ToString()))
                        {
                            if (Commun.ListHasValue(listQ))
                            {
                                query3 = "Select " + GetAn() + " From Alea_item where ID = '" + listQ[0] + "';";
                                array2 = Commun.CastStringArrayToDouble(SQlQueryExecuter.RunQueryReader(query3));
                                if (Commun.ArrayHasValue(array2))
                                {
                                    for (int i = 3; i < 13; i++)
                                    {
                                        if (year0.ToString() == table.Columns[i].ColumnName)
                                        {
                                            indexQ = i;
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    for (int i = 3; i < 13; i++)
                    {
                        query = SQLQueryBuilder.FindValueResultCalculQuery(table.Columns[i].ColumnName, row.ItemArray[1].ToString(), "Agri_Produits", IdExp);
                        List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Valeur", query);
                        if (Commun.ListHasValue(list2))
                        {
                            if ( array != null)
                            {
                                int index = compteur * 10 + (i - 3);
                                temp = array[index];
                                aleaP = true;
                            }
                            if (aleaP && array2 != null) temp = array2[i-3];
                            list2[0] = list2[0] * (temp / 100);
                            if ( array2 != null)
                            {
                                int index = compteur * 10 + (i - 3);
                                temp2 = array2[index];
                                aleaQ = true;
                            }
                            if (aleaQ && array2 != null) temp2 = array2[i - 3];
                            list2[0] = list2[0] * (temp2 / 100);
                            row.SetField<string>(table.Columns[i], list2[0].ToString());
                        }
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            return table;
        }

        private string GetAn()
        {
            string query = "";
            for (int i = 1; i<=100;i++)
            {
                query = query + "An" + i + ",";
            }
            query = query.Remove(query.Length - 1, 1);
            return query;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool update = false;
                int id = 0;
                string query = "";

                foreach (int index in ListeRecordToDelete)
                {
                    query = "Delete From Agri_Produits Where IdProduits = '"+index+"' And IdExploitations = '" + IdExp + "';";
                    SQlQueryExecuter.RunQuery(query);
                }
                ListeRecordToDelete.Clear();

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    query = " select * From Agri_produits Where Idproduits = '" + row.Cells[0].Value.ToString() + 
                        "' AND IdExploitations = '"+IdExp+"';";
                    List<int> list2 =  SQlQueryExecuter.RunQueryReaderInt("ID", query);
                    if (!Commun.ListHasValue(list2))
                    {
                        query = "Insert into Agri_produits (Idproduits,Formule,IdExploitations) VALUES ('" + row.Cells[0].Value.ToString() 
                            +"','" + textBox1.Text + "','"+ IdExp + "')";
                    }
                    else
                    {
                        id = list2[0];
                        //query = "Update Agri_Produits Set Formule = '" + textBox1.Text + "' WHERE Id = '"+id+"';";
                    }
                    SQlQueryExecuter.RunQuery(query);

                    for (int i = 3; i < 13; i++)
                    {
                        query = SQLQueryBuilder.FindValueResultCalculQuery(dataGridView1.Columns[i].HeaderText, row.Cells[1].Value.ToString(), "Agri_Produits", IdExp);
                      
                        List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                        if (Commun.ListHasValue(list))
                        {
                            update = true;
                            id = list[0];
                        }
                        else update = false;
                        if (update)
                        {
                            query = "Update Result_Calcul Set Valeur = '" + row.Cells[i].Value.ToString() +
                                "' WHERE ID = '" + id + "';";
                        }
                        else
                        {
                            query = "Insert Into Result_calcul (table_Origine,Nom,Annee,Valeur,IdExploitations) " +
                                "VALUES ('Agri_Produits','" + row.Cells[1].Value.ToString() + "','" +
                                 dataGridView1.Columns[i].HeaderText + "','" + row.Cells[i].Value.ToString() +
                                 "','" + IdExp + "');";
                        }
                        SQlQueryExecuter.RunQuery(query);
                    }
                }
                if (resultForm != null)
                {
                    if (resultForm.Text.Contains("*"))
                    {
                        resultForm.Text = resultForm.Text.Substring(resultForm.Text.Length - 1, 1);
                    }
                }
                ManageColor();
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex < 0) return;
                if (e.RowIndex < 0) return;
                if (!double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out mValueToReport))
                { mValueToReport = -1; }
                buttonList.Enabled = false;
                
                int id;
                int.TryParse(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(), out id);
                string query = "Select Formule From Agri_Produits" +
                   " Where IdExploitations = '" + IdExp +
                   "' AND IdProduits = '" + id + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Formule", query);
                if (Commun.ListHasValue(list))
                {
                    textBox1.Text = list[0];
                }
            }
            catch
            {

            }
        }

        private void TextBoxClicked(object sender, EventArgs e)
        {
            textBox1.SelectionStart = Math.Max(0, textBox1.Text.Length);
            textBox1.SelectionLength = 0;
        }

        private void buttonNotes_Click(object sender, EventArgs e)
        {
            NotesForm form = new NotesForm("Agri_Produits");
            form.ShowDialog();
        }

        private void buttonReport_Click(object sender, EventArgs e)
        {
            try
            { 
                // The value we report is a surface, its initialized at -1, so if the parse failed, it stay -1
                // meaning that we dont want to report this value
                if (mValueToReport > 0)
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
                    if (!resultForm.Text.Contains("*"))
                    {
                        resultForm.Text = resultForm.Text + "*";
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
                Log.WriteLog(Ex.Message, GetType().Name + ". Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
        }

        private void dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                if (e.RowIndex < 0) return;
                if (e.ColumnIndex < 0) return;

                if (!double.TryParse(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out mValueToReport))
                { mValueToReport = -1; }
                if (!resultForm.Text.Contains("*"))
                {
                    resultForm.Text = resultForm.Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.AllowUserToDeleteRows = true;
                DataGridViewSelectedRowCollection UserSelectedRow = dataGridView1.SelectedRows;
                foreach (DataGridViewRow row in UserSelectedRow)
                {
                    int index = row.Index;
                    ListeRecordToDelete.Add(GetId(index));
                    dataGridView1.Rows.RemoveAt(index);
                }
                if (!resultForm.Text.Contains("*"))
                {
                    resultForm.Text = resultForm.Text + "*";
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private int GetId(int Rowindex)
        {
            int id = -1;
            try
            {
                if (dataGridView1.Rows[Rowindex].Cells[0].Value.ToString() != null)
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
        
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(164, 226, 165));

            e.Graphics.FillRectangle(brush, panel1.ClientRectangle);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (!resultForm.Text.Contains("*"))
            {
                resultForm.Text = resultForm.Text + "*";
            }
        }

        private void buttonPlus10_Click(object sender, EventArgs e)
        {
            if (compteur==9)
            {
                MessageBox.Show(Translation.Translate("Maximum year reached", langue));
            }
            else
            {
                compteur++;
                LoadGrid();
            }
          
        }

        private void buttonMoins10_Click(object sender, EventArgs e)
        {
            
            if (compteur == 0)
            {
                MessageBox.Show(Translation.Translate("Minimum year reached", langue));
            }
            else
            {
                compteur--;
                LoadGrid();
            }
        }

        private bool HasAlea()
        {
            string query = "Select * From Agri_DefSim Where IdExploitations = '" + IdExp +"';";
            List<string> list = SQlQueryExecuter.RunQueryReaderStr("TypeAlea",query);
            if (Commun.ListHasValue(list))
            {
                return true;
            }
            else return false;
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            resultForm.Close();
        }
    }
}
