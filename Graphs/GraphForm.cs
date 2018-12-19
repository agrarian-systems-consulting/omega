using OMEGA.Other_Classes;
using OMEGA.SQLQuery;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace OMEGA.Graphs
{
    /// <summary>
    /// Class that represents the form that hold the graph
    /// </summary>
    internal partial class GraphForm : Form
    {
        private string currentCharType = "";
        private string mType = "";
        private int langue = Properties.Settings.Default.Langue;
        /// <summary>
        /// Load the graph on the form
        /// </summary>
        internal GraphForm(string[] SerieName,string chartType)
        {
            try
            {
                InitializeComponent();
                LoadGraph(SerieName, chartType);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        internal GraphForm(Dictionary<string, List<double>> DicoSerie, string chartType,string type ="")
        {
            try
            {
                InitializeComponent();
                LoadGraph(DicoSerie, chartType);
                mType = type;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadGraph(Dictionary<string,List<double>> DicoSerie, string chartype)
        {
            try
            {
                int index = 0;
                string title = "";
                foreach (KeyValuePair<string, List<double>> item in DicoSerie)
                {
                    try
                    {
                        chart1.Series.Add(item.Key + " " + SQLQueryBuilder.FindName("Exploitation", "Nom", "ID", Commun.GetIdExpl()));
                        foreach (double value in item.Value)
                        {
                            index = chart1.Series.Count - 1;
                            chart1.Series[index].Points.Add(value);
                            title = item.Key + " en €";
                        }
                    }
                    catch (Exception Ex)
                    {
                       // pour régler le problème de mettre 2 fois la même liste
                    }

                }

                chart1.ChartAreas[0].AxisY.Title = title;

                if (chartype != null)
                {
                    if (chartype.Equals("bars"))
                        buttonbars.PerformClick();
                    if (chartype.Equals("spline"))
                        buttonCurve.PerformClick();
                    if (chartype.Equals("line"))
                        buttonLine.PerformClick();
                    if (chartype.Equals("point"))
                        buttonPoint.PerformClick();
                    currentCharType = chartype;
                }

                LoadExploitation();

                for (int i = 0; i < checkedListBoxExploitation.Items.Count; i++)
                {
                    if (checkedListBoxExploitation.Items[i].ToString() == SQLQueryBuilder.FindName("Exploitation", "Nom", "ID", Commun.GetIdExpl()))
                    {
                        checkedListBoxExploitation.SetItemChecked(i, true);
                    }
                }
                
                chart1.Update();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private void LoadGraph(string[] ListeSerieName, string chartype)
        {
            try
            {
                foreach (string itemSerie in ListeSerieName)
                {
                    if (itemSerie == "") continue;

                    chart1.Series.Add(itemSerie);

                    List<string> list = CheckItemType(itemSerie);

                    foreach (string item in list)
                    {
                        double value;
                        if (double.TryParse(item, out  value))
                        {
                            chart1.Series[itemSerie].Points.Add(value);
                        }
                    }
                    // chart1.ChartAreas[0].AxisX. = 2018;
                    chart1.ChartAreas[0].AxisY.Title = itemSerie;

                }

                if (chartype != null)
                {
                    if (chartype.Contains("bars"))
                        buttonbars.PerformClick();
                    if (chartype.Contains("spline"))
                        buttonLine.PerformClick();
                }

                chart1.Update();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }

        private List<string> CheckItemType(string itemSerie)
        {
            // we know all first node, unfortunately it's not possible to do a switch here..
            // so it will be some Ifs ..
            try
            {
                int langue = Properties.Settings.Default.Langue;
                List<string> listStr = new List<string>();
                List<string> list = new List<string>();
                string[] array = itemSerie.Split(new string[] { "\\" }, StringSplitOptions.None);
                string selectedItem = array.Last<string>();

                if (itemSerie.Contains(Translation.Translate("Product Price", langue)))
                {
                    string query = SQLQueryBuilder.SelectQuery("Produits ","Prix","Where Nom = '" + selectedItem + "'");
                    list = SQlQueryExecuter.RunQueryReaderStr("Prix", query);
                   // yAxisLegend = "€";
                }
                if (itemSerie.Contains(Translation.Translate("Cost Price", langue)))
                {
                    string query = SQLQueryBuilder.SelectQuery("Charges","Prix","Where Nom = '" + selectedItem + "'");
                    list = SQlQueryExecuter.RunQueryReaderStr("Prix", query);
                    //yAxisLegend = "€";
                }
                if (itemSerie.Contains(Translation.Translate("Product Quantity", langue)))
                {
                    string query = "Select Quantite_1 From Produits" +
                        " JOIN Prod_Quantite on IdProduits = Produits.ID " +
                        " WHERE Produits.Nom = '" + selectedItem + "';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Quantite_1", query);
                   // yAxisLegend = "Quantity";
                }
                if (itemSerie.Contains(Translation.Translate("Cost Quantity", langue)))
                {
                    string query = "Select Quantite_1 From Charges" +
                        " JOIN Charge_Quantite on IdCharges = Charges.ID " +
                        " WHERE Charges.Nom = '" + selectedItem + "';";
                    list = SQlQueryExecuter.RunQueryReaderStr("Quantite_1", query);
                   // yAxisLegend = "Quantity";
                }


                if (Commun.ListHasValue(list))
                {
                    for (int i = 0; i < 10; i++)
                    {
                        listStr.Add(list[0]);
                    }
                }
                return listStr;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return null;
            }
        }

        private void LoadExploitation()
        {
            try
            {
                if (checkedListBoxExploitation.Items.Count == 0)
                {
                    string query = SQLQueryBuilder.SelectQuery("Exploitation", "Nom");
                    List<string> list = SQlQueryExecuter.RunQueryReaderStr("Nom", query);
                    if (Commun.ListHasValue(list))
                    {
                        foreach (string item in list)
                        {
                            checkedListBoxExploitation.Items.Add(item, false);
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

        private void AddExploitationSelected()
        {
            try
            {
                for (int i = 0; i < checkedListBoxExploitation.Items.Count; i++)
                {
                    if (checkedListBoxExploitation.GetItemChecked(i))
                    {
                        int id = SQLQueryBuilder.FindId("Exploitation", "ID", "Nom", checkedListBoxExploitation.Items[i].ToString());
                        Commun.SetIdExpl(id);
                        DataTable table = GetDataTable();
                        Dictionary<string, List<double>> dico = new Dictionary<string, List<double>>();
                        List<double> list = new List<double>();
                        dico.Add(table.TableName, new List<double>());
                        for (int nbrow = table.Rows.Count - 1; nbrow >= 0; nbrow--)
                        {
                            for (int numCol = 1; numCol < 11; numCol++)
                            {
                                double.TryParse(table.Rows[nbrow].ItemArray[numCol].ToString(), out double value);
                                list.Add(value);
                            }
                            break;
                        }
                        dico[table.TableName] = list;
                        LoadGraph(dico, currentCharType);
                    }
                    else
                    {
                        for (int j = chart1.Series.Count - 1; j >= 0; j--)
                        {
                            if (chart1.Series[j].Name.Contains(checkedListBoxExploitation.Items[i].ToString()))
                            {
                                chart1.Series.RemoveAt(j);
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

        private DataTable GetDataTable()
        {
            DataTable table = new DataTable();
            try
            {
                switch (mType)
                {
                    case "solde":
                        table = Calcul.GetSolde();
                        table.TableName = "Solde";
                        break;
                    case "solde cumulé":
                        table = Calcul.GetSoldCumule();
                        table.TableName = "Solde Cumulé";
                        break;
                    case "Total dépense diverse":
                        table = Calcul.GetTotalExt_Inc(0, 1);
                        table.TableName = "Total dépense diverse";
                        break;
                    case "Total produit":
                        table = Calcul.GetTotalProduit();
                        table.TableName = "Total produit";
                        break;
                }
            }
            catch
            {

            }
            return table;
        }

        private void buttonValidate_Click(object sender, EventArgs e)
        {
            int InitIdExpl = Commun.GetIdExpl();
            AddExploitationSelected();
            Commun.SetIdExpl(InitIdExpl);
        }
        
        private void buttonPoint_Click(object sender, EventArgs e)
        {
            foreach (Series item in chart1.Series)
            {
                item.ChartType = SeriesChartType.Point;
            }
            currentCharType = "point";
        }
        private void buttonCurve_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Series item in chart1.Series)
                {
                    item.ChartType = SeriesChartType.Spline;
                    item.BorderWidth = 5;
                }
                currentCharType = "spline";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void buttonLine_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Series item in chart1.Series)
                {
                    item.ChartType = SeriesChartType.Line;
                    item.BorderWidth = 5;
                }
                currentCharType = "line";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
        private void buttonbars_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (Series item in chart1.Series)
                {
                    item.ChartType = SeriesChartType.Column;
                }
                currentCharType = "bars";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, GetType().Name + " . Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
        }
    }
}
