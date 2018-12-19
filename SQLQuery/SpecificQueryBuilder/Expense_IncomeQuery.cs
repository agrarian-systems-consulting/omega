using OMEGA.Data_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
    internal static class Expense_IncomeQuery
    {

        internal static string MainQuery(int family, int income)
        {
            string query = "SELECT  Expense_Income.ID ,Expense_Income.Nom , TVA.Nom as TVA " +
                " From Expense_Income" +
                " JOIN TVA on TVA.IdTVA = Expense_Income.IdTVA" +
                " WHERE Expense = '" + income + "' AND Family = '" + family + "'; ";
            return query;
        }

        internal static string UpdateQuery(string nom,int idTVA,int ID )
        {
            string query = "Update Expense_Income Set Nom ='" + nom + "' , IdTVA ='" + idTVA +
                "' WHERE ID = '" + ID + "';";
            return query;
        }

        internal static string InsertQuery(int family, int income, string nom, int idTVA)
        {
            string query = "Insert into Expense_Income (Nom,IdTVA,Expense,Family)" +
                " VALUES ('" + nom + "','" + idTVA + "','" + income + "','" + family + "');";
            return query;
        }

        internal static string AddItemQuery(int id)
        {
            string query = "Select Nom From Expense_Income Where ID = '" + id + "';";
            return query;
        }
    }
}
