using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
    static class ActivityQuery
    {
        internal static string SelectActivityQuery(string filter)
        {
            string query;
            if (filter == "")
            {
                query = "SELECT Activite.Id,Activite.Nom,Type.Nom  as 'Type' from Activite " +
               "JOIN TYPE on Type.ID = Activite.IdType " +
               "UNION ALL SELECT Activite.Id,Activite.Nom,Activite.IdType as 'Type' " +
               "from Activite WHERE Activite.IdType is null; ";
            }
            else
            {
                query = "SELECT Activite.Id,Activite.Nom,Type.Nom as 'Type' from Activite " +
               "JOIN TYPE on Type.ID = Activite.IdType " +
               "WHERE Type.Nom = '" + filter + "';";
            }
            return query;
        }

        static internal string AddSelectedActiviteQuery(int ID)
        {
            string query = "SELECT Activite.ID,Activite.Nom,Type.Nom   " +
                  " FROM Activite "+
                  " JOIN Type on Activite.IdType = Type.ID "+
                  " WHERE Activite.ID = '" + ID + "' ;";
            return query;
        }

        internal static string LoadProductToActivityQuery(int ID)
        {
            string query = "SELECT distinct Produits.ID From Activite " +
                        "JOIN Caract_Activite on Caract_Activite.IdActivite = Activite.ID " +
                        "JOIN Produits on Produits.ID = Caract_Activite.IdProduits " +
                        "WHERE Activite.ID = '" + ID + "'; ";
            return query;
        }

        internal static string SelectTypeActivityQuery(int id)
        {
            string query = "Select Type.Nom From Type " +
                    "JOIN Activite on Activite.IdType = Type.ID " +
                    "WHERE Activite.ID = '" + id + "';";
            return query;
        }

        internal static string InsertNewActivityQuery(string nom, int idtype, int mainAct)
        {
            string query = "Insert into Activite (Nom,Encours,IdType,Culture_princ)" +
                " VALUES ('" + nom + "','1','" + idtype + "','"+ mainAct+"'); ";
            return query;
        }
       
        internal static string UpdateActivityQuery(string nom, int IdAct,int idtype,int mainAct)
        {
           string query = "Update Activite Set Nom = '" + nom + "' , IdType = '" + idtype + "', Culture_princ = '"+ mainAct +
                    "' WHERE ID = '" + IdAct + "' ;";
            return query;
        }

        internal static string LoadChargeToActivtyQuery(int ID)
        {
            string query = "SELECT distinct Charges.ID From Activite " +
                    "JOIN Caract_Activite on Caract_Activite.IdActivite = Activite.ID " +
                    "JOIN Charges on Charges.ID = Caract_Activite.IdCharges " +
                    "WHERE Activite.ID = '" + ID + "'; ";
            return query;
        }

        internal static string LoadExternalityToActivityQuery(int ID)
        {
            string query = "SELECT distinct Externalites.ID From Activite " +
                       "JOIN Caract_Activite on Caract_Activite.IdActivite = Activite.ID " +
                       "JOIN Externalites on Externalites.ID = Caract_Activite.IdExternalites " +
                       "WHERE Activite.ID = '" + ID + "'; ";
            return query;
        }

        internal static string LoadComboBoxTypeQuery(int ID)
        {
            string query = "SELECT Type.Nom From Type JOIN Activite ON Activite.IdType = Type.ID WHERE Activite.ID = '" + ID + "'  ;";
            return query;
        }

        internal static string LoadPeriodeToActivityQuery(int ID)
        {
            string query = "SELECT distinct Def_Calendrier.IdPeriode FROM Def_Calendrier " +
                     "JOIN Caract_Activite on Def_Calendrier.IdPeriode = Caract_Activite.IdPeriodes " +
                    "JOIN Activite on Caract_Activite.IdPeriodes = Activite.ID ";
            return query;
        }

        internal static string SelectActivityQuery(int mIdExpl)
        {
            string  query = "Select Activite.Nom From Activite " +
               " Join Caract_Exploitation on Caract_Exploitation.IdActivite = Activite.Id" +
               " Where Caract_Exploitation.IdExploitation = '" + mIdExpl + "';";
            return query;
        }

    }


}
