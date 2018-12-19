using OMEGA.Data_Classes;
using OMEGA.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OMEGA.Forms.ExternaliteForm;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
    static class ExternaliteQuery
    {
        static internal string MainQuery(List<int> listId)
        {
            string WhereClause = "";
            string mainquery;
            if (listId != null)
            {
                if (listId.Count != 0)
                {
                    foreach (int id in listId)
                    {
                        if (listId.IndexOf(id) == 0)
                        {
                            WhereClause = " WHERE Externalites.ID = '" + id + "' ";
                        }
                        else WhereClause = WhereClause + " OR Externalites.ID = '" + id + "' ";
                    }
                }
                else WhereClause = " WHERE Externalites.ID = '-1'";
            }
            mainquery = "SELECT Def_Categ.Nom as Type ,Externalites.Nom, SystemeUnite.UAte as U_atelier,SystemeUnite.UEnt as U_Entreprise," +
           "SystemeUnite.UGlobal as U_Global , Externalites.IdDefCateg, Externalites.IdSystUnit, " +
           "Externalites.ID FROM Externalites JOIN SystemeUnite on Externalites.IdSystUnit = SystemeUnite.IdSysUnit " +
           "JOIN Def_Categ on Externalites.IdDefCateg = Def_Categ.IdDefCateg  " +
           WhereClause +
           " UNION ALL SELECT Def_Categ.Nom as Type ,Externalites.Nom,null,null," +
           "null , Externalites.IdDefCateg, Externalites.IdSystUnit, " +
           "Externalites.ID FROM Externalites " +
           "JOIN Def_Categ on Externalites.IdDefCateg = Def_Categ.IdDefCateg  " +
           WhereClause + " AND (Def_Categ.IdDefinitions = '3'AND Externalites.IdSystUnit = '0'); ";
           

            return mainquery;
        }

 


        static internal string AddItemInGroupQuery(KeyValuePair<int, InfoUserExternalite> item)
        {
            string query = "INSERT INTO Externalites (Nom , IdSystUnit,IdDefCateg) " +
                        "VALUES ('" + item.Value.Nom + "','" + item.Value.IdsystUnit +
                        "',' " +  item.Value.IdDefCateg + "');";
            return query;
        }

        static internal string UpdateValueQuery(KeyValuePair<int, InfoUserExternalite> item)
        {
            string query = "UPDATE Externalites Set IdDefCateg = '" + item.Value.IdDefCateg + "', Nom = '" +
           item.Value.Nom + "', IdSystUnit = '" + item.Value.IdsystUnit + "'" +
           "  WHERE ID = '" + item.Value.ID + "';";
            return query;
        }

        static internal string AddSelectedExtQuery(int ID)
        {
            string query = "SELECT Def_Categ.Nom as Type ,Externalites.Nom, SystemeUnite.UAte as U_atelier,SystemeUnite.UEnt as U_Entreprise," +
                      "SystemeUnite.UGlobal as U_Global , Externalites.IdDefCateg, Externalites.IdSystUnit, " +
                      "Externalites.ID FROM Externalites JOIN SystemeUnite on Externalites.IdSystUnit = SystemeUnite.IdSysUnit " +
                      "JOIN Def_Categ on Externalites.IdDefCateg = Def_Categ.IdDefCateg  " +
                       "WHERE Externalites.ID = '" + ID +
                       "' UNION ALL SELECT Def_Categ.Nom as Type ,Externalites.Nom,null,null," +
                       "null , Externalites.IdDefCateg, Externalites.IdSystUnit, " +
                       "Externalites.ID FROM Externalites " +
                       "JOIN Def_Categ on Externalites.IdDefCateg = Def_Categ.IdDefCateg  " +
                       "WHERE Externalites.ID = '" + ID +  "' AND (Def_Categ.IdDefinitions = '3'AND Externalites.IdSystUnit = '0'); ";
            return query;
        }

        internal static string AutoCompleteQuery()
        {
            string query = "Select distinct Def_Categ.Nom from Def_Categ " +
             "WHERE IDdefinitions = '3'";
            return query;
        }

        internal static string MainQueryExternaliteResult(int IDdefcateg)
        {
            string query = "SELECT Externalites.ID, Externalites.Nom ,Def_Categ.Nom as groupe " +
                " FROM Externalites JOIN Def_Categ on Externalites.IdDefCateg = Def_Categ.IdDefCateg " +
                " JOIN Agri_Externalites on Agri_Externalites.IdExternalites = Externalites.ID" +
                " JOIN Exploitation on Exploitation.ID = Agri_Externalites.IdExploitations" +
                " WHERE Externalites.IdDefCateg = '" + IDdefcateg + "' ;";
            return query;
        }


        static internal string UpdateQuantiteQuery(KeyValuePair<int, InfoUserExternalite> item, int idAct)
        {
            string query = "UPDATE Ext_Quantite Set Quantite_Avant_1 = '" + item.Value.QtyAv1 + "', Quantite_1 = '" +
                           item.Value.Qty1 + "', IdActivite = '" + idAct +
                           "' WHERE ID = '" + item.Value.ID + "';";
            return query;
        }

        internal static string AddQuantiteQuery(KeyValuePair<int, InfoUserExternalite> item, int idAct)
        {
            string query = "INSERT INTO Ext_Quantite (IdExternalite , Quantite_Avant_1,Quantite_1,IdActivite) " +
                 "VALUES ('" + item.Value.ID + "','" + item.Value.QtyAv1 + "','" + item.Value.Qty1 +
                 "','" + idAct + "');";
            return query;
        }


        internal static string AddPerenneQuery(KeyValuePair<int, InfoUserExternalite> item, int idAct)
        {
            string champ = "";
            int index = 1;
            string valuechamp = "";
            foreach (int value in item.Value.ListPhase)
            {
                champ = champ + "Ph" + index + ",";
                index++;
                valuechamp = valuechamp + "'" + value + "',";
            }
            champ = champ.Remove(champ.Length - 1, 1);
            valuechamp = valuechamp.Remove(valuechamp.Length - 1, 1);

            string query = "INSERT INTO Ext_Perenne (IdExternalite, Quantite_Avant_1,IdActivite," + champ + ") " +
                 "VALUES ('" + item.Value.ID + "','" + item.Value.QtyAv1 + "'," + idAct +
                 "," + valuechamp + ");";
            return query;
        }

        internal static string AddPluriannuelleQuery(KeyValuePair<int, InfoUserExternalite> item, int idAct)
        {
            string champ = "";
            int index = 1;
            string valuechamp = "";
            foreach (int value in item.Value.ListPhase)
            {
                champ = champ + "Ph" + index + ",";
                index++;
                valuechamp = valuechamp + "'" + value + "',";
            }
            champ = champ.Remove(champ.Length - 1, 1);
            valuechamp = valuechamp.Remove(valuechamp.Length - 1, 1);

            string query = "INSERT INTO Ext_Pluriannuelle (IdExternalite, Quantite_Avant_1,IdActivite," + champ + ") " +
                 "VALUES ('" + item.Value.ID + "','" + item.Value.QtyAv1 + "'," + idAct +
                 ",'" + valuechamp + "');";
            return query;
        }

        internal static string UpdatePerenneQuery(KeyValuePair<int, InfoUserExternalite> item, int idAct)
        {
            string champ = "";
            int index = 1;
            foreach (int value in item.Value.ListPhase)
            {
                champ = champ + "Ph" + index + " = '" + value + "',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "Update Ext_Perenne Set Quantite_Avant_1 = '" + item.Value.QtyAv1 + "', " + champ + ") ";
            return query;
        }

        internal static string UpdatePluriannuelleQuery(KeyValuePair<int, InfoUserExternalite> item, int idAct)
        {
            string champ = "";
            int index = 1;
            foreach (int value in item.Value.ListPhase)
            {
                champ = champ + "Ph" + index + " = '" + value + "',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "update Ext_Pluriannuelle Set Quantite_Avant_1 = '" + item.Value.QtyAv1 + "', " + champ + ") ";
            return query;
        }

    }
}
