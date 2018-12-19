using OMEGA.Data_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
    internal static class FamilyQuery
    {

        /// <summary>
        /// Return the IDs from the family 
        /// </summary>
        /// <param name="IdObject"></param>
        /// <returns></returns>
        static internal List<int> GetListID( int idExpl)
        {
            List<int> listId = new List<int>();
            try
            {
                string query = "SELECT ID From FAMILY WHERE IdExploitation = '" + idExpl + "'; ";
                listId = SQlQueryExecuter.RunQueryReaderInt("ID", query);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return listId;
        }


        internal static string MainQuery(List<int> listId,int idExp)
        {
            string WhereClause = "";
            string mainquery;
            if (listId != null)
            {
                foreach (int id in listId)
                {
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = " WHERE Family.ID = '" + id + "' "; 
                    }
                    else WhereClause = WhereClause + " OR Family.ID = '" + id + "' ";
                }
                if (listId.Count == 0) WhereClause =" Where Family.ID = '-1'";
            }
            else WhereClause = " WHERE Family.ID = '-1'";
            mainquery = "SELECT Family.Id,Family.Nom,Family.Role,Family.Responsable," +
                "Age,Sexe, Exploitation.Nom as 'Exploitation' From Family " +
                "JOIN Exploitation on Exploitation.Id = Family.IdExploitation " +
            WhereClause + " ;";

            return mainquery;
        }

        static internal string UpdateValueQuery(string nom, string role, string resp, int idexpl, double Age, string sexe, int id)
        {
            string query = "UPDATE Family Set Nom = '" + nom + "', Role = '" + role +
                "', Responsable = '" + resp + "',IdExploitation = '" + idexpl +
                 "', Age = '" + Age + "',Sexe = '" + sexe +
                "' WHERE ID = '" + id + "';";
            return query;
        }

        static internal string InsertValueQuery(string nom,string role,string resp,int idexpl,double Age,string sexe,int id)
        {
            string query = "INSERT INTO Family (Nom , Role, Responsable,IdExploitation,Age,Sexe) " +
                           "VALUES ('" + nom + "','" + role + "','" + resp + "','"+ idexpl + "','" + Age + "','" + sexe + "')";
            return query;
        }

        internal static string AutoCompleteQuery(string field)
        {
            string query = "Select distinct " + field + " from Family";
            return query;
        }

        internal static string AddSelectedFamilyQuery(int id)
        {
            string query = "SELECT* From Family Where ID = '" + id + "';";
            return query;
        }
    }
}
