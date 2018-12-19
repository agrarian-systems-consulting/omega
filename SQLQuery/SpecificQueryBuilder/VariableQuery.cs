using OMEGA.Data_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
    static class VariableQuery
    {

        static internal string UpdateValueQuery(KeyValuePair<int, InfoUserVariable> item)
        {
            string query = "UPDATE Variable Set Nom = '" +  item.Value.Nom + "', IdSysUnite = '" 
                + item.Value.IdsystUnit +  "', Categorie ='"
                + item.Value.Categorie + "' WHERE ID = '" + item.Value.ID + "';";
            return query;
        }

        static internal string AddItemInGroupQuery(KeyValuePair<int, InfoUserVariable> item)
        {
            string query = "INSERT INTO Variable (Nom ,Categorie, IdSysUnite) " +
                   "VALUES ('" + item.Value.Nom + "','" + item.Value.Categorie + 
                   "','" + item.Value.IdsystUnit+ "');";
            return query;
        }


        static internal string AddSelectedVariableQuery(int ID)
        {
            string query = "SELECT Variable.ID,Variable.Nom,Variable.Categorie" +
                    " FROM Variable WHERE Variable.ID = '" + ID + "';";
            return query;
        }

    }
}
