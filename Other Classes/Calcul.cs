using OMEGA.SQLQuery;
using OMEGA.SQLQuery.SpecificQueryBuilder;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Other_Classes
{
    /// <summary>
    /// This class contains all the function to run calculs and generate reports and graphs
    /// For some of them it's not very easy to follow because the data type maybe vevry different
    /// I tried to use explicite variables name as much as possible
    /// </summary>
    static class Calcul
    {
        private static int mNbProduct;
        private static int mNbCharge;
        private static int mNbMiscExp;
        private static int mNbFamExp;
        private static int mNbMiscInc;
        private static int mNbFamInc;
        private static int langue = Properties.Settings.Default.Langue;
        private static int mIdExpl = Commun.GetIdExpl();

        static internal KeyValuePair<int, DataTable> GetElementRec_Dep(DataTable dtbl, string table, int structurelle, int nbRow, int family = -1, int expense = -1)
        {

            KeyValuePair<int, DataTable> returnValue = new KeyValuePair<int, DataTable>(nbRow, dtbl);
            try
            {
                string tableAgri = "";
                bool CharProd = true;
                string query = "";
                List<int> list = new List<int>();
                switch (table)
                {
                    case "Produits":
                        tableAgri = "Agri_Produits";
                        break;
                    case "Charges":
                        tableAgri = "Agri_Charges";
                        break;
                    default:
                        tableAgri = "Expense_Income";
                        table = "Expense_Income";
                        CharProd = false;
                        break;
                }

                if (CharProd)
                {
                    query = "Select *  From " + tableAgri +
                    " Join " + table + " on " + tableAgri + ".Id" + table + " = " + table + ".ID" +
                    " Where IdExploitations = '" + Commun.GetIdExpl() + "'";
                    if (table == "Charges") query = query + " And Structurelle  = '" + structurelle + "';";
                    else query = query + ";";
                    list = SQlQueryExecuter.RunQueryReaderInt("Id" + table, query);
                }
                else
                {
                    query = "Select *  From " + tableAgri +
                    " Where Family = '" + family +
                    "' AND Expense  ='" + expense + "'";
                    list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                }


                int year0 = GetYear();
                foreach (int id in list)
                {
                    query = "Select Valeur,Annee From Result_Calcul " +
                        " WHere IdExploitations = '" + Commun.GetIdExpl() +
                        "' AND Nom = '" + SQLQueryBuilder.FindName(table, "Nom", "ID", id) + "'" +
                        " AND Table_Origine = '" + tableAgri + "';";
                    List<int> listyear = SQlQueryExecuter.RunQueryReaderInt("Annee", query);

                    List<double> listValue = new List<double>();
                    foreach (DataColumn column in dtbl.Columns)
                    {
                        query = "Select Valeur From Result_Calcul " +
                       " WHere IdExploitations = '" + Commun.GetIdExpl() +
                       "' AND Nom = '" + SQLQueryBuilder.FindName(table, "Nom", "ID", id) + "'" +
                       " AND Table_Origine = '" + tableAgri + "'" +
                       " AND Annee = '" + column.ColumnName + "';";
                        List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Valeur", query);
                        if (Commun.ListHasValue(list2)) listValue.Add(list2[0]);
                    }

                    string[] valueToAdd = new string[11];
                    for (int i = 0; i < 11; i++)
                    {
                        valueToAdd[0] = " " + SQLQueryBuilder.FindName(table, "Nom", "ID", id);
                        if (i > 0 && listValue.Count > 0) valueToAdd[i] = listValue[i - 1].ToString();
                    }
                    dtbl.Rows.Add(valueToAdd);
                    nbRow++;
                }
                returnValue = new KeyValuePair<int, DataTable>(nbRow, dtbl);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return returnValue;
        }

        static internal KeyValuePair<int, DataTable> GetElementProdFinance(DataTable dtbl, int nbRow, string tableAgri)
        {
            KeyValuePair<int, DataTable> returnValue = new KeyValuePair<int, DataTable>(nbRow, dtbl);
            try
            {
                string query = "";
                List<int> list = new List<int>();

                query = "Select *  From " + tableAgri +
                " Where IdExploitation = '" + Commun.GetIdExpl() + "'";

                list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                List<string> listNom = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                List<double> listMontant = SQlQueryExecuter.RunQueryReaderDouble("Montant", query);
                List<string> listDateReal = SQlQueryExecuter.RunQueryReaderStr("DateReal", query);
                List<string> listDateRemb = SQlQueryExecuter.RunQueryReaderStr("DateRemb", query);
                List<double> listTaux = SQlQueryExecuter.RunQueryReaderDouble("Taux", query);

                int index = 0;
                int year0 = GetYear();

                foreach (int id in list)
                {
                    string[] valueToAdd = new string[11];

                    valueToAdd[0] = " " + listNom[index];
                    int.TryParse(listDateRemb[index].Substring(5, 4), out int anneeRemb);
                    for (int i = 0; i < 11; i++)
                    {
                        if (year0 + i == anneeRemb)
                        {
                            valueToAdd[i + 1] = GetFraisRemb(listMontant[index], listTaux[index]);
                            break;
                        }
                    }
                    dtbl.Rows.Add(valueToAdd);
                    nbRow++;
                    index++;
                }
                returnValue = new KeyValuePair<int, DataTable>(nbRow, dtbl);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return returnValue;
        }

        private static string GetFraisRemb(double montant, double taux)
        {
            return (montant * (taux / 100)).ToString();
        }

        static internal DataTable GetTotalElementRec_Dep(DataTable dtbl, string table, int nbrowProd, int nbrowChar,
            int nbrowMiscEpx, int nbrowMiscInc, int nbrowFamExp, int nbrowFamInc, int nbrowChargeStruc = 0)
        {
            List<double> listprix = new List<double>();

            for (int i = 1; i <= 10; i++)
            {
                double temp2 = 0;
                int rowindex = 0;
                foreach (DataRow row in dtbl.Rows)
                {
                    rowindex++;
                    if (rowindex > 1)
                    {
                        double temp;
                        if (table == "Produits")
                        {
                            temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                            temp2 = temp2 + temp;
                        }
                        if (table == "Charges Operationnelles")
                        {
                            if (rowindex <= nbrowProd + 3) continue;
                            temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                            temp2 = temp2 + temp;
                        }
                        if (table == "Charges Structurelles")
                        {
                            if (rowindex <= nbrowProd + nbrowChar + 5) continue;
                            temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                            temp2 = temp2 + temp;
                        }

                        if (table == "Misc Expense")
                        {
                            if (rowindex <= nbrowProd + nbrowChar + nbrowChargeStruc + 7) continue;
                            temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                            temp2 = temp2 + temp;
                        }
                        if (table == "Family Expense")
                        {
                            if (rowindex <= nbrowFamExp + nbrowMiscEpx + nbrowProd + nbrowChar + nbrowChargeStruc + 7) continue;
                            temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                            temp2 = temp2 + temp;
                        }
                        if (table == "Misc Income")
                        {
                            if (rowindex <= nbrowMiscInc + nbrowMiscEpx + nbrowProd + nbrowChar + nbrowFamExp + nbrowChargeStruc + 10) continue;
                            temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                            temp2 = temp2 + temp;
                        }
                        if (table == "Family Income")
                        {
                            if (rowindex <= nbrowFamInc + nbrowMiscInc + nbrowFamExp + nbrowMiscEpx + nbrowProd + nbrowChar + nbrowChargeStruc + 11) continue;
                            temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                            temp2 = temp2 + temp;
                        }
                    }
                }
                listprix.Add(temp2);
            }

            dtbl.Rows.Add((new object[] { Translation.Translate("   TOTAL " + table,langue),listprix[0],
                listprix[1], listprix[2], listprix[3], listprix[4],
                listprix[5], listprix[6], listprix[7], listprix[8],
                listprix[9] }));
            return dtbl;
        }

        static internal DataTable GetSolde(DataTable dtbl)
        {

            try
            {
                List<double> listTotal = new List<double>();
                List<double> listTotalCumule = new List<double>();
                for (int i = 1; i <= 10; i++)
                {
                    double temp = 0;
                    double temp2 = 0;
                    foreach (DataRow row in dtbl.Rows)
                    {
                        if (row.ItemArray[0].ToString().Contains("TOTAL"))
                        {
                            if (row.ItemArray[0].ToString().Contains("Charges") || row.ItemArray[0].ToString().Contains("Expense"))
                            {
                                temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                                temp2 = temp2 - temp;
                            }
                            else
                            {
                                temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                                temp2 = temp + temp2;
                            }
                        }
                    }
                    listTotal.Add(temp2);
                }
                dtbl.Rows.Add((new object[] { "SOLDE",listTotal[0],
                listTotal[1], listTotal[2], listTotal[3], listTotal[4],
                listTotal[5], listTotal[6], listTotal[7], listTotal[8],
                listTotal[9] }));

                //    double previousValue = 0;
                //    for (int i = 0; i <= listTotal.Count - 1; i++)
                //    {
                //        previousValue = previousValue + listTotal[i];
                //        listTotalCumule.Add(previousValue);
                //    }
                //    dtbl.Rows.Add((new object[] { "SOLDE CUMULE",listTotalCumule[0],
                //listTotalCumule[1], listTotalCumule[2], listTotalCumule[3], listTotalCumule[4],
                //listTotalCumule[5], listTotalCumule[6], listTotalCumule[7], listTotalCumule[8],
                //listTotalCumule[9] }));
                return dtbl;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return null;
                // Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        static internal DataTable GetSolde()
        {

            try
            {
                DataTable dtbl = new DataTable();
                dtbl.Columns.Add(" ");

                int year = GetYear();
                for (int i = year; i < year + 11; i++)
                {
                    dtbl.Columns.Add(i.ToString());
                }
                mNbProduct = 0; mNbCharge = 0; mNbMiscExp = 0; mNbMiscInc = 0; mNbFamExp = 0; mNbFamInc = 0;
                dtbl.Rows.Add((new object[] { Translation.Translate("Products", langue) }));
                KeyValuePair<int, DataTable> value = Calcul.GetElementRec_Dep(dtbl, "Produits", 0, mNbProduct);
                dtbl = value.Value;
                mNbProduct = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Produits", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);


                dtbl.Rows.Add((new object[] { Translation.Translate("Operational Cost", langue) }));
                value = GetElementRec_Dep(dtbl, "Charges", 0, mNbCharge);
                dtbl = value.Value;
                mNbCharge = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Charges Operationnelles", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Operational Cost", langue) }));
                value = GetElementRec_Dep(dtbl, "Charges", 1, mNbCharge);
                dtbl = value.Value;
                mNbCharge = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Charges Structurelles", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Misc expense", langue) }));
                value = GetElementRec_Dep(dtbl, "Misc Expense", 0, mNbMiscExp, 0, 1);
                dtbl = value.Value;
                mNbMiscExp = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Charges Structurelles", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Family expense", langue) }));
                value = GetElementRec_Dep(dtbl, "Family Expense", 0, mNbFamExp, 1, 1);
                dtbl = value.Value;
                mNbFamExp = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Family Expense", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Misc Income", langue) }));
                value = GetElementRec_Dep(dtbl, "Misc income", 0, mNbMiscInc, 0, 0);
                dtbl = value.Value;
                mNbMiscInc = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Misc income", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Family Income", langue) }));
                value = GetElementRec_Dep(dtbl, "Family income", 0, mNbFamInc, 1, 0);
                dtbl = value.Value;
                mNbFamInc = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Family income", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);


                List<double> listTotal = new List<double>();
                List<double> listTotalCumule = new List<double>();
                for (int i = 1; i <= 10; i++)
                {
                    double temp = 0;
                    double temp2 = 0;
                    foreach (DataRow row in dtbl.Rows)
                    {
                        if (row.ItemArray[0].ToString().Contains("TOTAL"))
                        {
                            if (row.ItemArray[0].ToString().Contains("Charges") || row.ItemArray[0].ToString().Contains("Expense"))
                            {
                                temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                                temp2 = temp2 - temp;
                            }
                            else
                            {
                                temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                                temp2 = temp + temp2;
                            }
                        }
                    }
                    listTotal.Add(temp2);
                }
                dtbl.Rows.Add((new object[] { "SOLDE",listTotal[0],
                listTotal[1], listTotal[2], listTotal[3], listTotal[4],
                listTotal[5], listTotal[6], listTotal[7], listTotal[8],
                listTotal[9] }));

                return dtbl;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return null;
                // Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }

        }

        static internal DataTable GetSoldCumule(DataTable dtbl)
        {
            try
            {
                List<double> listTotal = new List<double>();
                List<double> listTotalCumule = new List<double>();
                for (int i = 1; i <= 10; i++)
                {
                    double temp = 0;
                    double temp2 = 0;
                    foreach (DataRow row in dtbl.Rows)
                    {
                        if (row.ItemArray[0].ToString().Contains("TOTAL"))
                        {
                            if (row.ItemArray[0].ToString().Contains(Translation.Translate("Cost", langue)) || row.ItemArray[0].ToString().Contains(Translation.Translate("Expense", langue)))
                            {
                                temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                                temp2 = temp2 - temp;
                            }
                            else
                            {
                                temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                                temp2 = temp + temp2;
                            }
                        }
                    }
                    listTotal.Add(temp2);
                }
                //dtbl.Rows.Add((new object[] { Translation.UpdateCaption("SOLDE",langue),listTotal[0],
                //listTotal[1], listTotal[2], listTotal[3], listTotal[4],
                //listTotal[5], listTotal[6], listTotal[7], listTotal[8],
                //listTotal[9] }));

                double previousValue = 0;
                for (int i = 0; i <= listTotal.Count - 1; i++)
                {
                    previousValue = previousValue + listTotal[i];
                    listTotalCumule.Add(previousValue);
                }
                dtbl.Rows.Add((new object[] { "SOLDE CUMULE",listTotalCumule[0],
                listTotalCumule[1], listTotalCumule[2], listTotalCumule[3], listTotalCumule[4],
                listTotalCumule[5], listTotalCumule[6], listTotalCumule[7], listTotalCumule[8],
                listTotalCumule[9] }));
                return dtbl;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return null;
                //  Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        static internal DataTable GetSoldCumule()
        {
            try
            {
                DataTable dtbl = new DataTable();
                dtbl.Columns.Add(" ");

                int year = GetYear();
                for (int i = year; i < year + 11; i++)
                {
                    dtbl.Columns.Add(i.ToString());
                }
                mNbProduct = 0; mNbCharge = 0; mNbMiscExp = 0; mNbMiscInc = 0; mNbFamExp = 0; mNbFamInc = 0;
                dtbl.Rows.Add((new object[] { Translation.Translate("Products", langue) }));
                KeyValuePair<int, DataTable> value = Calcul.GetElementRec_Dep(dtbl, "Produits", 0, mNbProduct);
                dtbl = value.Value;
                mNbProduct = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Produits", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Operational Cost", langue) }));
                value = GetElementRec_Dep(dtbl, "Charges", 0, mNbCharge);
                dtbl = value.Value;
                mNbCharge = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Charges Operationnelles", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Operational Cost", langue) }));
                value = GetElementRec_Dep(dtbl, "Charges", 1, mNbCharge);
                dtbl = value.Value;
                mNbCharge = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Charges Structurelles", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Misc expense", langue) }));
                value = GetElementRec_Dep(dtbl, "Misc Expense", 0, mNbMiscExp);
                dtbl = value.Value;
                mNbMiscExp = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Charges Structurelles", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Family expense", langue) }));
                value = GetElementRec_Dep(dtbl, "Family Expense", 0, mNbFamExp);
                dtbl = value.Value;
                mNbFamExp = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Family Expense", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Misc Income", langue) }));
                value = GetElementRec_Dep(dtbl, "Misc income", 0, mNbMiscInc);
                dtbl = value.Value;
                mNbMiscInc = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Misc income", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Family Income", langue) }));
                value = GetElementRec_Dep(dtbl, "Family income", 0, mNbFamInc);
                dtbl = value.Value;
                mNbFamInc = value.Key;
                dtbl = GetTotalElementRec_Dep(dtbl, "Family income", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);


                List<double> listTotal = new List<double>();
                List<double> listTotalCumule = new List<double>();
                for (int i = 1; i <= 10; i++)
                {
                    double temp = 0;
                    double temp2 = 0;
                    foreach (DataRow row in dtbl.Rows)
                    {
                        if (row.ItemArray[0].ToString().Contains("TOTAL"))
                        {
                            if (row.ItemArray[0].ToString().Contains(Translation.Translate("Cost", langue)) || row.ItemArray[0].ToString().Contains(Translation.Translate("Expense", langue)))
                            {
                                temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                                temp2 = temp2 - temp;
                            }
                            else
                            {
                                temp = Commun.GetDoubleFromString(row.ItemArray[i].ToString());
                                temp2 = temp + temp2;
                            }
                        }
                    }
                    listTotal.Add(temp2);
                }



                double previousValue = 0;
                for (int i = 0; i <= listTotal.Count - 1; i++)
                {
                    previousValue = previousValue + listTotal[i];
                    listTotalCumule.Add(previousValue);
                }
                dtbl.Rows.Add((new object[] { "SOLDE CUMULE",listTotalCumule[0],
                listTotalCumule[1], listTotalCumule[2], listTotalCumule[3], listTotalCumule[4],
                listTotalCumule[5], listTotalCumule[6], listTotalCumule[7], listTotalCumule[8],
                listTotalCumule[9] }));
                return dtbl;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return null;
                //  Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        static internal DataTable GetElementQuantite2(DataTable dtbl, string table, int nbrowProd, int nbrowChar)
        {
            try
            {
                mNbCharge = nbrowChar;
                mNbProduct = nbrowProd;
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
                    " Join Agri_" + table + " on Agri_" + table + "." + IdTable + " = " + table + ".ID " +
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



                            // we get the number of row for the charges and the products
                            if (table == "Charges")
                            {
                                mNbCharge = 1 + mNbCharge;
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
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return dtbl;

        }

        static internal DataTable GetElementAnnualQuantite(DataTable dtbl, string table, string agriTable, int nbrowProd, int nbrowChar)
        {
            try
            {
                mIdExpl = Commun.GetIdExpl();
                string query = "";
                string tableQuantite;
                query = " Select * From caract_Exploitation" +
                       " WHere IdExploitation = '" + mIdExpl + "';";
                List<int> listAct = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);


                query = "Select distinct " + agriTable + ".Id" + table + " From " + agriTable +
                    " JOIN " + table + " on " + agriTable + ".Id" + table + " = " + table + ".ID ";
                if (table == "Charges")
                {
                    query = query + " Where Charges.Structurelle = '0' " +
                        "AND IdExploitations = '" + mIdExpl + "';";
                }
                else query = query + " Where IdExploitations = '" + mIdExpl + "';";

                List<int> listProdAnnuelle = SQlQueryExecuter.RunQueryReaderInt("Id" + table, query);

                foreach (int Idprod in listProdAnnuelle)
                {
                    string[] ValueToAdd = new string[12];

                    ValueToAdd[0] = " " + SQLQueryBuilder.FindName(table, "Nom", "ID", Idprod);

                    query = "Select UEnt From " + table +
                        " Join SystemeUnite on " + table + ".IdSysUnit = SystemeUnite.IdSysUnit" +
                        " Where " + table + ".ID = '" + Idprod + "';";
                    ValueToAdd[1] = SQlQueryExecuter.RunQueryReaderStr("UEnt", query)[0];
                    List<double> list = new List<double>();
                    if (table == "Produits")
                    {
                        tableQuantite = "Prod_Quantite";
                    }
                    else
                    {
                        tableQuantite = "Charge_Quantite";
                    }
                    foreach (int IdActivite2 in listAct)
                    {

                        query = "Select * From " + tableQuantite + " Where Id" + table + "= '" + Idprod + "' and IdActivite = '" + IdActivite2 + "';";
                        list = SQlQueryExecuter.RunQueryReaderDouble("Quantite_1", query);
                        if (Commun.ListHasValue(list))
                        {
                            for (int n = 2; n < 12; n++)
                            {
                                query = "Select Valeur From Result_Calcul" +
                                    " Where Table_Origine = 'Agri_Assol" +
                                    "' AND Annee = '" + (Commun.GetYear() + (n - 2)) +
                                    "' AND Nom = '" + SQLQueryBuilder.FindName("Activite", "Nom", "ID", IdActivite2) +
                                    "' AND IdExploitations = '" + mIdExpl + "'";
                                List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Valeur", query);
                                if (!Commun.ListHasValue(list2)) list2.Add(1);
                                ValueToAdd[n] = (list[0] * list2[0]).ToString();
                            }
                            dtbl.Rows.Add(ValueToAdd);
                            break;
                        }
                        break;
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Calcul. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return dtbl;
        }

        static internal DataTable GetElementPerenneQuantite(DataTable dtbl, string table, string agriTable, int nbrowProd, int nbrowChar)
        {
            try
            {
                mIdExpl = Commun.GetIdExpl();
                string query = "";
                string tableQuantite;
                string[] ValueToAdd = new string[12];
                query = " Select * From caract_Exploitation WHere IdExploitation = '" + mIdExpl + "';";
                List<int> listAct = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);
                bool addthisrow = false;
                if (table == "Produits")
                {
                    tableQuantite = "Prod_Perenne";
                }
                else
                {
                    tableQuantite = "Charge_Perenne";
                }
                foreach (int IdActivite2 in listAct)
                {
                    addthisrow = false;

                    query = "Select * From " + tableQuantite + " Where IdActivite = '" + IdActivite2 + "';";

                    string[] reader = SQlQueryExecuter.RunQueryReader(query);

                    int maxlenght = 0;
                    int i = 4;
                    while (reader[i] != "" && reader[i] != null)
                    {
                        maxlenght++;
                        i++;
                        if (i == reader.Length - 3) break;
                    }
                    for (int j = 0; j < maxlenght; j++)
                    {
                        string query2 = "Select Id" + table + " From Caract_Activite Where IdActivite ='" + IdActivite2 + "';";
                        List<int> listidProd = SQlQueryExecuter.RunQueryReaderIntSilent("Id" + table, query);
                        if (Commun.ListHasValue(listidProd))
                        {
                            ValueToAdd[0] = " " + SQLQueryBuilder.FindName(table, "Nom", "ID", listidProd[0]);
                            query2 = "Select UEnt From  " + table +
                                 " Join SystemeUnite on " + table + ".IdSysUnit = SystemeUnite.IdSysUnit" +
                                 " Where " + table + ".ID = '" + listidProd[0] + "';";
                            ValueToAdd[1] = SQlQueryExecuter.RunQueryReaderStrSilent("UEnt", query2)[0];
                        }
                        string nomChamp = Commun.GetPhase(SQLQueryBuilder.FindName("Activite", "Nom", "ID", IdActivite2), false);
                        string[] arrChamp = nomChamp.Split(',');
                        for (int n = 2; n < 12; n++)
                        {
                            int phIndex = GetCurrentPhase(arrChamp[j], Commun.GetYear() + (n - 2), IdActivite2, n);
                            query = " Select AnArr From " + agriTable + " WHERE  IdActivite = '" + IdActivite2 +
                                "' AND IdExploitations = '" + mIdExpl + "';";
                            int AnArr = SQlQueryExecuter.RunQueryReaderInt("AnArr", query)[0];

                            if (AnArr < Commun.GetYear() + (n - 2)) break;
                            if (phIndex <= 0) continue;
                            query = "Select Ph" + phIndex + " From " + tableQuantite + " Where IdActivite = '" + IdActivite2 + "';";
                            List<double> list = SQlQueryExecuter.RunQueryReaderDouble("Ph" + phIndex, query);
                            if (!Commun.ListHasValue(list)) list.Add(1);

                            query = "Select Surface From " + agriTable + " WHERE  IdActivite = '" + IdActivite2 +
                                "' AND IdExploitations = '" + mIdExpl + "';";
                            List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Surface", query);
                            if (!Commun.ListHasValue(list2)) list2.Add(1);

                            ValueToAdd[n] = (list[0] * list2[0]).ToString();
                            addthisrow = true;
                        }
                    }
                    if (addthisrow) dtbl.Rows.Add(ValueToAdd);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Calcul. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return dtbl;
        }

        static internal DataTable GetElementPluriannualQuantite(DataTable dtbl, string table, string agriTable, int nbrowProd, int nbrowChar)
        {
            try
            {
                mIdExpl = Commun.GetIdExpl();
                string query = "";
                string[] ValueToAdd = new string[12];
                bool addthisrow = false;
                string tableQuantite;
                query = " Select * From caract_Exploitation WHere IdExploitation = '" + mIdExpl + "';";
                List<int> listAct = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);
                if (table == "Produits")
                {
                    tableQuantite = "Prod_Pluriannuelle";
                }
                else
                {
                    tableQuantite = "Charge_Pluriannuelle";
                }
                foreach (int IdActivite2 in listAct)
                {
                    addthisrow = false;
                    List<double> list = new List<double>();
                    query = "Select * From " + tableQuantite + " Where IdActivite = '" + IdActivite2 + "';";
                    List<int> listpluriannuelle = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);
                    if (!Commun.ListHasValue(listpluriannuelle)) continue;
                    for (int j = 1; j <= 4; j++)
                    {
                        string query2 = "Select Id" + table + " From Caract_Activite Where IdActivite ='" + IdActivite2 + "';";
                        List<int> listidProd = SQlQueryExecuter.RunQueryReaderIntSilent("Id" + table, query);
                        if (Commun.ListHasValue(listidProd))
                        {
                            ValueToAdd[0] = " " + SQLQueryBuilder.FindName(table, "Nom", "ID", listidProd[0]);
                            query2 = "Select UEnt From  " + table +
                                 " Join SystemeUnite on " + table + ".IdSysUnit = SystemeUnite.IdSysUnit" +
                                 " Where " + table + ".ID = '" + listidProd[0] + "';";
                            ValueToAdd[1] = SQlQueryExecuter.RunQueryReaderStrSilent("UEnt", query2)[0];
                        }

                        string nomChamp = Commun.GetPhase(SQLQueryBuilder.FindName("Activite", "Nom", "ID", IdActivite2), false);
                        string[] arrChamp = nomChamp.Split(',');

                        query = " Select AnArr From " + agriTable + " WHERE  IdActivite = '" + IdActivite2 +
                            "' AND IdExploitations = '" + mIdExpl + "';";
                        int AnArr = SQlQueryExecuter.RunQueryReaderInt("AnArr", query)[0];

                        if (AnArr < Commun.GetYear() + j) break;
                        query = "Select " + j + " From " + tableQuantite + " Where IdActivite = '" + IdActivite2 + "';";
                        list = SQlQueryExecuter.RunQueryReaderDouble(j.ToString(), query);
                        if (!Commun.ListHasValue(list)) list.Add(1);

                        query = "Select Surface From " + agriTable + " WHERE  IdActivite = '" + IdActivite2 +
                            "' AND IdExploitations = '" + mIdExpl + "';";
                        List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Surface", query);
                        if (!Commun.ListHasValue(list2)) list2.Add(1);

                        ValueToAdd[j] = (list[0] * list2[0]).ToString();
                        addthisrow = true;

                    }
                    if (addthisrow) dtbl.Rows.Add(ValueToAdd);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Calcul. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return dtbl;

        }

        private static int GetCurrentPhase(string arrChamp, int currentYear, int idAct, int yeartoAdd)
        {
            int index = 1;
            string phase = Commun.GetPhase(SQLQueryBuilder.FindName("Activite", "Nom", "ID", idAct));
            string[] arr = phase.Split(',');
            for (int i = 0; i < arr.Length; i++)
            {
                if (arr[i].Contains(arrChamp))
                {
                    break;
                }
                else
                {
                    index++;
                }
            }

            return index;
        }

        static internal DataTable GetElementPrennialQuantite(DataTable dtbl, string table, int nbrowProd, int nbrowChar)
        {
            try
            {
                string query = "";

                query = " Select * From caract_Exploitation" +
                       " WHere IdExploitation = " + mIdExpl + "';";
                List<int> listAct = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);

                // annuelle
                query = "Select distinct Agri_Perenne.IdProduits From Agri_Perenne " +
                    "JOIN " + table + " on Agri_Perenne.IdProduits = " + table + ".ID ";
                if (table == "Charges")
                {
                    query = query + " Where Charges.Structurelle = '0' " +
                        "AND IdExploitations = '" + mIdExpl + "';";
                }
                else query = query + " Where IdExploitations = '" + mIdExpl + "';";

                List<int> listProdAnnuelle = SQlQueryExecuter.RunQueryReaderInt("IdProduits", query);

                foreach (int Idprod in listProdAnnuelle)
                {

                    string[] ValueToAdd = new string[12];

                    ValueToAdd[0] = " " + SQLQueryBuilder.FindName("Produits", "Nom", "ID", Idprod);

                    query = "Select UEnt From Produits " +
                        " Join SystemeUnite on Produits.IdSysUnit = SytemeUnite.IdSysUnit" +
                        " Where Produits.ID = '" + Idprod + "';";
                    ValueToAdd[1] = SQlQueryExecuter.RunQueryReaderStr("UEnt", query)[0];

                    foreach (int IdActivite2 in listAct)
                    {
                        query = "Select * From Prod_Quantite Where IdProduits = '" + Idprod + "' and IdActivite = '" + IdActivite2 + "';";
                        List<double> list = SQlQueryExecuter.RunQueryReaderDouble("Quantite_1", query);
                        if (Commun.ListHasValue(list))
                        {
                            for (int i = 2; i < 12; i++)
                            {
                                query = "Select Valeur From Result_Calcul" +
                                    " Where Table_Origine = 'Agri_Assol' " +
                                    " AND Année = '" + Commun.GetYear() + (i - 2) + "'" +
                                    " AND Nom = '" + SQLQueryBuilder.FindName("Activite", "Nom", "ID", IdActivite2) +
                                    " IdExploitations = '" + mIdExpl + "'";
                                List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Valeur", query);
                                if (!Commun.ListHasValue(list2)) list2.Add(1);
                                ValueToAdd[i] = (list[0] * list2[0]).ToString();
                            }
                            dtbl.Rows.Add(ValueToAdd);
                            break;
                        }
                    }

                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return dtbl;

        }

        static internal KeyValuePair<int, DataTable> AddElement(DataTable dtbl, string table, int mIdAct, int nbrowProd, int nbrowChar)
        {
            try
            {
                List<int> listId = SQLQueryBuilder.GetListID("Activite", table, mIdAct);

                string query = "";
                if (table == "Charges")
                {
                    query = ChargeQuery.SelectChargeOftheActivityQuery(listId, 0, mIdAct);
                }
                else if (table == "Produits")
                {
                    query = ProduitQuery.SelectProductOftheActivityQuery(listId, mIdAct);
                }

                string previousname = "";
                SQLiteDataReader reader = SQlQueryExecuter.RunQueryDataReader(query);

                string[] ValueToAdd = new string[7];
                while (reader.Read())
                {
                    if (reader != null)
                    {
                        // 0 = vide, 1 = nom , 2 = catégorie , 3 = unité , 4 = prix , 5 = qté , 6 = valeur
                        ValueToAdd[0] = "";
                        ValueToAdd[1] = reader.GetValue(0).ToString();
                        ValueToAdd[2] = reader.GetValue(1).ToString();
                        ValueToAdd[3] = reader.GetValue(3).ToString();
                        ValueToAdd[4] = reader.GetValue(2).ToString();
                        ValueToAdd[5] = reader.GetValue(4).ToString();
                        double prix = Commun.GetDoubleFromString(ValueToAdd[4]);
                        double qte;
                        double.TryParse(ValueToAdd[5], out qte);
                        double valeur = prix * qte;
                        ValueToAdd[6] = valeur.ToString();



                        if (previousname != ValueToAdd[1])
                        {
                            //mTotalValeur = mTotalValeur + valeur;

                            dtbl.Rows.Add(ValueToAdd);
                            // we get the number of row for the charges and the products
                            if (table == "Charges")
                            {
                                mNbCharge = 1 + mNbCharge;
                            }
                            else
                            {
                                mNbProduct = 1 + mNbProduct;
                            }
                            previousname = ValueToAdd[1];
                        }
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return new KeyValuePair<int, DataTable>();
        }

        static internal KeyValuePair<int, DataTable> GetElementRec_DepGP(DataTable dtbl, string table, int structurelle,
            int nbRow, int family = -1, int expense = -1)
        {
            KeyValuePair<int, DataTable> returnValue = new KeyValuePair<int, DataTable>(nbRow, dtbl);
            try
            {
                Dictionary<string, List<double>> DicoDefCateg = new Dictionary<string, List<double>>();
                bool DefItemExist = false;
                string tableAgri = "";
                bool CharProd = true;
                string query = "";
                List<int> list = new List<int>();
                switch (table)
                {
                    case "Produits":
                        tableAgri = "Agri_Produits";
                        break;
                    case "Charges":
                        tableAgri = "Agri_Charges";
                        break;
                    default:
                        tableAgri = "Expense_Income";
                        table = "Expense_Income";
                        CharProd = false;
                        break;
                }

                if (CharProd)
                {
                    query = "Select *  From " + tableAgri +
                    " Join " + table + " on " + tableAgri + ".Id" + table + " = " + table + ".ID" +
                    " Where IdExploitations = '" + Commun.GetIdExpl() + "'";
                    if (table == "Charges") query = query + " And Structurelle  = '" + structurelle + "';";
                    else query = query + ";";
                    list = SQlQueryExecuter.RunQueryReaderInt("Id" + table, query);

                    int year0 = GetYear();
                    foreach (int id in list)
                    {
                        query = "Select Def_Categ.Nom From Def_Categ " +
                            " Join " + table + "  on Def_categ.IdDefCateg = " + table + " .IdDefCateg" +
                            " Where " + table + " .ID = '" + id + "'";
                        List<string> listItemDefCateg = SQlQueryExecuter.RunQueryReaderStr("Nom", query);

                        if (Commun.ListHasValue(listItemDefCateg))
                        {
                            if (!DicoDefCateg.ContainsKey(listItemDefCateg[0]))
                            {
                                DicoDefCateg.Add(listItemDefCateg[0], new List<double>());
                                DefItemExist = false;
                            }
                            else DefItemExist = true;


                            List<double> listValue = new List<double>();
                            foreach (DataColumn column in dtbl.Columns)
                            {
                                query = "Select Valeur From Result_Calcul " +
                                   " WHere IdExploitations = '" + Commun.GetIdExpl() +
                                   "' AND Nom = '" + SQLQueryBuilder.FindName(table, "Nom", "ID", id) + "'" +
                                   " AND Table_Origine = '" + tableAgri + "'" +
                                   " AND Annee = '" + column.ColumnName + "';";
                                List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Valeur", query);
                                if (Commun.ListHasValue(list2)) listValue.Add(list2[0]);
                            }

                            if (!DefItemExist)
                            {
                                DicoDefCateg[listItemDefCateg[0]] = listValue;
                            }
                            else
                            {
                                int index = 0;
                                foreach (double item in listValue)
                                {
                                    DicoDefCateg[listItemDefCateg[0]][index] = DicoDefCateg[listItemDefCateg[0]][index] + item;
                                    index++;
                                }
                            }
                        }
                    }
                    foreach (KeyValuePair<string, List<double>> dicoItem in DicoDefCateg)
                    {
                        string[] valueToAdd = new string[11];
                        valueToAdd[0] = " " + dicoItem.Key;
                        for (int i = 1; i < dicoItem.Value.Count; i++)
                        {
                            valueToAdd[i] = dicoItem.Value[i - 1].ToString();
                        }
                        dtbl.Rows.Add(valueToAdd);
                        nbRow++;
                    }
                }

                returnValue = new KeyValuePair<int, DataTable>(nbRow, dtbl);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return returnValue;
        }

        static internal KeyValuePair<int, DataTable> GetElementExpIncGP(DataTable dtbl, string table, string element,
      int nbRow, int family = -1, int expense = -1)
        {
            KeyValuePair<int, DataTable> returnValue = new KeyValuePair<int, DataTable>(nbRow, dtbl);
            try
            {
                Dictionary<string, List<double>> DicoExpInc = new Dictionary<string, List<double>>();
                bool elementExist = false;
                string tableAgri = "Expense_Income";
                string query = "";
                List<int> list = new List<int>();

                query = "Select *  From " + table +
                  " Where Family = '" + family +
                  "' AND Expense  ='" + expense + "'";
                list = SQlQueryExecuter.RunQueryReaderInt("ID", query);

                int year0 = GetYear();
                foreach (int id in list)
                {
                    if (!DicoExpInc.ContainsKey(element))
                    {
                        DicoExpInc.Add(element, new List<double>());
                        elementExist = false;
                    }
                    else elementExist = true;


                    List<double> listValue = new List<double>();
                    foreach (DataColumn column in dtbl.Columns)
                    {
                        query = "Select Valeur From Result_Calcul " +
                            " WHere IdExploitations = '" + Commun.GetIdExpl() +
                            "' AND Nom = '" + SQLQueryBuilder.FindName(table, "Nom", "ID", id) + "'" +
                            " AND Table_Origine = '" + tableAgri + "'" +
                            " AND Annee = '" + column.ColumnName + "';";
                        List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Valeur", query);
                        if (Commun.ListHasValue(list2)) listValue.Add(list2[0]);
                    }

                    if (!elementExist)
                    {
                        DicoExpInc[element] = listValue;
                    }
                    else
                    {
                        int index = 0;
                        foreach (double item in listValue)
                        {
                            DicoExpInc[element][index] = DicoExpInc[element][index] + item;
                            index++;
                        }
                    }

                }
                foreach (KeyValuePair<string, List<double>> dicoItem in DicoExpInc)
                {
                    string[] valueToAdd = new string[11];
                    valueToAdd[0] = " TOTAL " + dicoItem.Key;
                    for (int i = 1; i < dicoItem.Value.Count; i++)
                    {
                        valueToAdd[i] = dicoItem.Value[i - 1].ToString();
                    }
                    dtbl.Rows.Add(valueToAdd);
                    nbRow++;
                }

                returnValue = new KeyValuePair<int, DataTable>(nbRow, dtbl);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return returnValue;
        }

        internal static DataTable GetTotalProduit()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("");
                string query = "Select *  From Agri_Produits " +
                    " Join Produits on Agri_Produits.IdProduits = Produits.ID" +
                    " Where IdExploitations = '" + Commun.GetIdExpl() + "';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("IdProduits", query);
                int year0 = GetYear();
                foreach (int id in list)
                {
                    query = "Select Valeur,Annee From Result_Calcul " +
                        " WHere IdExploitations = '" + Commun.GetIdExpl() +
                        "' AND Nom = '" + SQLQueryBuilder.FindName("Produits", "Nom", "ID", id) + "'" +
                        " AND Table_Origine = 'Agri_Produits';";
                    List<int> listyear = SQlQueryExecuter.RunQueryReaderInt("Annee", query);

                    if (table.Columns.Count == 1)
                    {
                        foreach (int year in listyear)
                        {
                            if (!(table.Columns.Contains(year.ToString())) && year < year0 + 10 && year >= year0)
                            {
                                table.Columns.Add(year.ToString());
                            }
                        }
                    }
                    List<double> listValue = new List<double>();
                    foreach (DataColumn column in table.Columns)
                    {
                        query = "Select Valeur From Result_Calcul " +
                      " WHere IdExploitations = '" + Commun.GetIdExpl() +
                      "' AND Nom = '" + SQLQueryBuilder.FindName("Produits", "Nom", "ID", id) + "'" +
                      " AND Table_Origine = 'Agri_Produits'" +
                      " AND Annee = '" + column.ColumnName + "';";
                        List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Valeur", query);
                        if (Commun.ListHasValue(list2)) listValue.Add(list2[0]);
                    }

                    string[] valueToAdd = new string[11];
                    for (int i = 0; i < 11; i++)
                    {
                        valueToAdd[0] = SQLQueryBuilder.FindName("Produits", "Nom", "ID", id);
                        if (i > 0) valueToAdd[i] = listValue[i - 1].ToString();
                    }
                    table.Rows.Add(valueToAdd);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return table;
        }

        static internal DataTable GetTotalChargeStructure()
        {
            DataTable table = new DataTable();
            try
            {
                table.Columns.Add("");
                string query = "Select *  From Agri_Charges " +
                    " Join Charges on Agri_Charges.IdCharges = Charges.ID" +
                    " Where Structurelle = '1' and IdExploitations = '" + Commun.GetIdExpl() + "';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("IdCharges", query);
                int year0 = GetYear();
                foreach (int id in list)
                {
                    query = "Select Valeur,Annee From Result_Calcul " +
                        " WHere IdExploitations = '" + Commun.GetIdExpl() +
                        "' AND Nom = '" + SQLQueryBuilder.FindName("Charges", "Nom", "ID", id) + "'" +
                        " AND Table_Origine = 'Agri_Charges';";
                    List<int> listyear = SQlQueryExecuter.RunQueryReaderInt("Annee", query);

                    if (table.Columns.Count == 1)
                    {
                        foreach (int year in listyear)
                        {
                            if (!(table.Columns.Contains(year.ToString())) && year < year0 + 10 && year >= year0)
                            {
                                table.Columns.Add(year.ToString());
                            }
                        }
                    }
                    List<double> listValue = new List<double>();
                    foreach (DataColumn column in table.Columns)
                    {
                        query = "Select Valeur From Result_Calcul " +
                      " WHere IdExploitations = '" + Commun.GetIdExpl() +
                      "' AND Nom = '" + SQLQueryBuilder.FindName("Charges", "Nom", "ID", id) + "'" +
                      " AND Table_Origine = 'Agri_Charges'" +
                      " AND Annee = '" + column.ColumnName + "';";
                        List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Valeur", query);
                        if (Commun.ListHasValue(list2)) listValue.Add(list2[0]);
                    }

                    string[] valueToAdd = new string[11];
                    for (int i = 0; i < 11; i++)
                    {
                        valueToAdd[0] = SQLQueryBuilder.FindName("Charges", "Nom", "ID", id);
                        if (i > 0) valueToAdd[i] = listValue[i - 1].ToString();
                    }
                    table.Rows.Add(valueToAdd);
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return table;
        }

        static internal DataTable GetTotalExt_Inc(int family, int expense)
        {
            DataTable table = new DataTable();
            table.Columns.Add();
            try
            {
                int year0 = GetYear();
                string query = "Select Nom From Expense_Income " +
                    "Where Family = '" + family + "' AND Expense = '" + expense + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                foreach (string nom in list)
                {
                    query = "Select Valeur,Annee From Result_Calcul " +
                           " WHere IdExploitations = '" + Commun.GetIdExpl() +
                           "' AND Nom = '" + nom + "'" +
                           " AND Table_Origine = 'Expense_Income';";
                    List<int> listyear = SQlQueryExecuter.RunQueryReaderInt("Annee", query);

                    if (table.Columns.Count <= 1)
                    {
                        foreach (int year in listyear)
                        {
                            if (!(table.Columns.Contains(year.ToString())) && year < year0 + 10 && year >= year0)
                            {
                                table.Columns.Add(year.ToString());
                            }
                        }
                    }

                    List<double> listValue = new List<double>();
                    foreach (DataColumn column in table.Columns)
                    {
                        query = "Select Valeur From Result_Calcul " +
                        " WHere IdExploitations = '" + Commun.GetIdExpl() +
                        "' AND Nom = '" + nom + "'" +
                        " AND Table_Origine = 'Expense_Income'" +
                        " AND Annee = '" + column.ColumnName + "';";
                        List<double> list2 = SQlQueryExecuter.RunQueryReaderDouble("Valeur", query);
                        if (Commun.ListHasValue(list2)) listValue.Add(list2[0]);
                    }

                    string[] valueToAdd = new string[11];
                    for (int i = 0; i < 11; i++)
                    {
                        valueToAdd[0] = nom;
                        if (i > 0 && listValue.Count > 0) valueToAdd[i] = listValue[i - 1].ToString();
                    }
                    if (table.Columns.Count > 10) table.Rows.Add(valueToAdd);

                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return table;
        }

        private static int GetYear()
        {
            int An0 = 0;
            string query = "Select An_0 From Agri_DefSim Where IdExploitations = '" + Commun.GetIdExpl() + "'";
            List<int> list = SQlQueryExecuter.RunQueryReaderInt("An_0", query);
            if (Commun.ListHasValue(list))
            {
                An0 = list[0];
            }
            else An0 = DateTime.Now.Year;
            return An0;
        }

        internal static DataTable SetTotalItem(string item, DataTable dtbl, DataTable dtbl2)
        {
            bool Go = false;

            double temp;
            double[] values = new double[11];
            foreach (DataRow row in dtbl.Rows)
            {
                if (row.ItemArray[0].ToString() == item)
                {
                    Go = true;
                }

                if (row.ItemArray[0].ToString().First() == ' ' && Go)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        double.TryParse(row.ItemArray[i].ToString(), out temp);
                        values[i] = temp + values[i];
                    }

                }
            }
            foreach (DataRow row in dtbl2.Rows)
            {
                if (row.ItemArray[0].ToString() == item)
                {
                    for (int i = 1; i <= 10; i++)
                    {
                        row.SetField(i, values[i]);
                    }
                    break;
                }

            }
            return dtbl2;
        }


        internal static KeyValuePair<double, DataTable> AddElement(DataTable dtbl, string table, int IdAct, double totalValeur, int nbrowChar, int nbrowProd)
        {
            KeyValuePair<double, DataTable> Value = new KeyValuePair<double, DataTable>();
            try
            {
                List<int> listId = SQLQueryBuilder.GetListID("Activite", table, IdAct);
                mNbCharge = nbrowChar;
                mNbProduct = nbrowProd;

                int IdType = SQLQueryBuilder.FindId("Activite", "IdType", "ID", IdAct);
                string query = "";
                int nbcolonne = 0;

                if (table == "Charges")
                {
                    switch (IdType)
                    {
                        case 1://annuellle
                            query = ChargeQuery.SelectChargeOftheActivityQuery(listId, 0, IdAct);
                            nbcolonne = 7;
                            break;
                        case 4: //perenne
                            query = ChargeQuery.SelectChargeOftheActivityQueryPerenne(listId, 0, IdAct);
                            nbcolonne = 6 + Commun.GetNbPhase(IdAct);
                            break;
                        case 6: //pluriannuelle
                            query = ChargeQuery.SelectChargeOftheActivityQueryPluriannuelle(listId, 0, IdAct);
                            nbcolonne = 10;
                            break;
                    }

                }
                else if (table == "Produits")
                {
                    switch (IdType)
                    {
                        case 1://annuellle
                            query = ProduitQuery.SelectProductOftheActivityQuery(listId, IdAct);
                            nbcolonne = 7;
                            break;
                        case 4: //perenne
                            query = ProduitQuery.SelectProductOftheActivityQueryPerenne(listId, IdAct);
                            nbcolonne = 6 + Commun.GetNbPhase(IdAct); ;
                            break;
                        case 6: //pluriannuelle
                            query = ProduitQuery.SelectProductOftheActivityQueryPluriannuelle(listId, IdAct);
                            nbcolonne = 10;
                            break;
                    }
                }

                string previousname = "";
                SQLiteDataReader reader = SQlQueryExecuter.RunQueryDataReader(query);
                double valeur = 1;
                string[] ValueToAdd = new string[nbcolonne];
                while (reader.Read())
                {
                    if (reader != null)
                    {
                        // 0 = vide, 1 = nom , 2 = catégorie , 3 = unité , 4 = prix , 5 = qté , 6 = valeur
                        ValueToAdd[0] = "";
                        ValueToAdd[1] = reader.GetValue(0).ToString();
                        ValueToAdd[2] = reader.GetValue(1).ToString();
                        ValueToAdd[3] = reader.GetValue(3).ToString();
                        ValueToAdd[4] = reader.GetValue(2).ToString();

                        for (int i = 5; i < nbcolonne; i++)
                        {
                            if (i == nbcolonne - 1) break;
                            ValueToAdd[i] = reader.GetValue(i - 1).ToString();
                            double prix = Commun.GetDoubleFromString(ValueToAdd[4]);
                            double qte = Commun.GetDoubleFromString(ValueToAdd[i]);
                            valeur = prix * qte;
                            ValueToAdd[i + 1] = valeur.ToString();
                        }

                        if (previousname != ValueToAdd[1])
                        {
                            totalValeur = totalValeur + valeur;

                            dtbl.Rows.Add(ValueToAdd);
                            // we get the number of row for the charges and the products
                            if (table == "Charges")
                            {
                                mNbCharge = 1 + mNbCharge;
                            }
                            else
                            {
                                mNbProduct = 1 + mNbProduct;
                            }
                            previousname = ValueToAdd[1];
                        }
                    }
                }
                Value = new KeyValuePair<double, DataTable>(totalValeur, dtbl);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return Value;
        }

        internal static DataTable GetMarge(DataTable dtbl)
        {
            try
            {
              
                string[] valuetoadd = new string[11];
                double[] temp1 = new double[10];
                double[] temp2 = new double[10];
                double[] temp3 = new double[10];
                int index1 = 0;
                int index2 = 0;
                int index3 = 0;
                foreach (DataRow row in dtbl.Rows)
                {
                    index1 = 0;
                    if (row.ItemArray[0].ToString().Contains(Translation.Translate("   TOTAL Products", langue)))
                    {
                        foreach (DataColumn column in dtbl.Columns)
                        {
                            if (int.TryParse(column.ColumnName, out int year))
                            {
                                double.TryParse(row.ItemArray[index1+ 1].ToString(), out temp1[index1]);
                                index1++;
                            }
                        }
                    }
                    index2 = 0;
                    if (row.ItemArray[0].ToString().Contains(Translation.Translate("   TOTAL Operational Costs", langue)))
                    {
                        foreach (DataColumn column in dtbl.Columns)
                        {
                            if (int.TryParse(column.ColumnName, out int year))
                            {
                                double.TryParse(row.ItemArray[index2 + 1].ToString(), out temp2[index2]);
                                index2++;
                            }
                        }
                    }
                    index3 = 0;
                    if (row.ItemArray[0].ToString().Contains(Translation.Translate("   TOTAL Operational Costs", langue)))
                    {
                        foreach (DataColumn column in dtbl.Columns)
                        {
                            if (int.TryParse(column.ColumnName, out int year))
                            {
                                double.TryParse(row.ItemArray[index3 + 1].ToString(), out temp2[index3]);
                                index2++;
                            }
                        }
                    }
                }
                for (int i = 1; i < 11; i++)
                {
                    valuetoadd[i] = (temp1[i - 1] - temp2[i - 1]).ToString();
                }
                valuetoadd[0] = "MARGE";
                dtbl.Rows.Add(valuetoadd);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
               // Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return dtbl;
        }

        internal static DataTable TotalMargeForGraph()
        {
            DataTable dtbl = new DataTable();
            try
            {
                dtbl.Columns.Add(" ");

                int year = GetYear();
                for (int i = year; i < year + 10; i++)
                {
                    dtbl.Columns.Add(i.ToString());
                }
                mNbProduct = 0;
                dtbl.Rows.Add((new object[] { Translation.Translate("Products", langue) }));
                KeyValuePair<int, DataTable> value = Calcul.GetElementRec_Dep(dtbl, "Produits", 0, mNbProduct);
                dtbl = value.Value;
                mNbProduct = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Produits", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Operational Cost", langue) }));
                value = Calcul.GetElementRec_Dep(dtbl, "Charges", 0, mNbCharge);
                dtbl = value.Value;
                mNbCharge = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Charges Operationnelles", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc);

                dtbl.Rows.Add((new object[] { Translation.Translate("Structural Cost", langue) }));
                value = Calcul.GetElementRec_Dep(dtbl, "Charges", 1, mNbCharge);
                dtbl = value.Value;
                mNbCharge = value.Key;
                dtbl = Calcul.GetTotalElementRec_Dep(dtbl, "Charges Structurelles", mNbProduct, mNbCharge, mNbMiscExp, mNbMiscInc, mNbFamExp, mNbFamInc, mNbCharge);

                dtbl = Calcul.GetMarge(dtbl);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return dtbl;

        }

        internal static DataTable Immobilisation(DataTable dtbl)
        {
            try
            {
                string[] valuetoadd = new string[11];
                double temp1 = 0;
                double temp2 = 0;
                string query = "Select * From Agri_ImmoGlobal Where IdExploitation = '" + Commun.GetIdExpl() +
                    "' AND NoItem ='3'";
                string[] arrayAchat = SQlQueryExecuter.RunQueryReader(query);
                query = "Select * From Agri_ImmoGlobal Where IdExploitation = '" + Commun.GetIdExpl() +
                   "' AND NoItem ='7'";
                string[] arrayVente = SQlQueryExecuter.RunQueryReader(query);
                valuetoadd[0] = " Achat";
                for (int i = 1; i < 11; i++)
                {
                    valuetoadd[i] = arrayAchat[i+2];
                }
                dtbl.Rows.Add(valuetoadd);
                valuetoadd[0] = " Vente";
                for (int i = 1; i < 11; i++)
                {
                    valuetoadd[i] = arrayVente[i + 2];
                }
                dtbl.Rows.Add(valuetoadd);
                valuetoadd[0] = "TOTAL Immobilisation";
                for (int i = 3; i < 13; i++)
                {
                    double.TryParse(arrayAchat[i], out temp1);
                    double.TryParse(arrayVente[i], out temp2);
                    valuetoadd[i - 2] = (temp1 - temp2).ToString();
                }
                dtbl.Rows.Add(valuetoadd);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                //Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());

            }
            return dtbl;
        }
     }
}
