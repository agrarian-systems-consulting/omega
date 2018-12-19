using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
    static class ResultQuery
    {

        internal static string AddValueExpIncQuery(DataTable table, DataRow row,int i,int IdExpl)
        {
            string query = "Select Valeur From Result_Calcul " +
                       "Where Table_Origine = 'Expense_Income' " +
                       "AND Annee = '" + table.Columns[i].ColumnName +
                       "' AND IdExploitations = '" + IdExpl +
                       "' AND Nom = '" + row.ItemArray[0].ToString() + "';";
            return query;
        }

        internal static string SelectExpIncQuery(DataGridViewRow row, DataGridView dataGridView1,int i,int IdExpl)
        {
            string query = "Select * From Result_Calcul Where table_Origine = 'Expense_Income' " +
                       "AND Nom = '" + row.Cells[0].Value.ToString() +
                       "' AND IdExploitations = '" + IdExpl +
                       "' AND Annee = '" + dataGridView1.Columns[i].HeaderText + "';";
            return query;
        }

        internal static string UpdateExpIncQuery (string value,int id)
        {
            string query = "Update Result_Calcul Set Valeur = '" + value +
                                "' WHERE ID = '" + id + "';";
            return query;
        }

        internal static string InsertExpIncQuery (DataGridViewRow row, DataGridView dataGridView1, int i, string value,int IdExpl)
        {
            string query = "Insert Into Result_calcul (table_Origine,Nom,Annee,Valeur,IdExploitations) " +
                            "VALUES ('Expense_Income','" + row.Cells[0].Value.ToString() + "','" +
                                dataGridView1.Columns[i].HeaderText + "','" + value + "','" + IdExpl + "');";
            return query;
        }


        internal static string MainQueryCharge(int IdExp)
        {
            string mainquery = "SELECT Charges.Id,Charges.Nom ,Def_Categ.Nom as groupe " +
                 " FROM Charges JOIN Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg " +
                 " JOIN Agri_Charges on Agri_Charges.IdCharges = Charges.ID" +
                 " JOIN Exploitation on Exploitation.ID = Agri_Charges.IdExploitations" +
                 " WHERE Structurelle = '1' and Agri_Charges.IdExploitations = '" + IdExp + "';";
            return mainquery;
        }

        internal static string MainQueryCharge2(int IdExp)
        {
            string mainquery = "SELECT Charges.Id,Charges.Nom ,Def_Categ.Nom as groupe " +
                " FROM Charges JOIN Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg " +
                " JOIN Agri_Charges on Agri_Charges.IdCharges = Charges.ID" +
                " JOIN Exploitation on Exploitation.ID = Agri_Charges.IdExploitations" +
                " WHERE Structurelle = '0'and Agri_Charges.IdExploitations = '" + IdExp + "';";
            return mainquery;
        }

        internal static string AddValueChargeQuery(DataRow row, DataTable table, int i, int IdExp)
        {
            string query = "Select Valeur From Result_Calcul " +
                       "Where Table_Origine = 'Agri_Charges' " +
                       " AND Annee = '" + table.Columns[i].ColumnName +
                       "' AND Nom = '" + row.ItemArray[1].ToString() +
                       "' AND IdExploitations = '" + IdExp + "';";
            return query;
        }

        internal static string AddValueAssolQuery(DataRow row, DataTable table, int i, int IdExp)
        {
            string query = "Select Valeur From Result_Calcul " +
                       "Where Table_Origine = 'Agri_Assol' " +
                       " AND Annee = '" + table.Columns[i].ColumnName +
                       "' AND Nom = '" + row.ItemArray[1].ToString() +
                       "' AND IdExploitations = '" + IdExp + "';";
            return query;
        }
        internal static string AddValueAnimauxQuery(DataRow row, DataTable table, int i, int IdExp)
        {
            string query = "Select Valeur From Result_Calcul " +
                       "Where Table_Origine = 'Agri_Animaux' " +
                       " AND Annee = '" + table.Columns[i].ColumnName +
                       "' AND Nom = '" + row.ItemArray[1].ToString() +
                       "' AND IdExploitations = '" + IdExp + "';";
            return query;
        }

        internal static string MainQueryAssol(int id)
        {
            string query = "Select Activite.Id, Activite.nom " +
                   "From Agri_Assol JOIN Activite On Agri_Assol.IdActivite = Activite.Id " +
                   "Where IdExploitations = " + id + ";";
            return query;

        }

        internal static string MainQueryAnimaux(int id)
        {
            string query = "Select distinct Activite.Id, Activite.nom " +
                   "From Agri_Animaux JOIN Activite On Agri_Animaux.IdActivite = Activite.Id " +
                   "Where IdExploitations = " + id + ";";
            return query;

        }
    }
}
