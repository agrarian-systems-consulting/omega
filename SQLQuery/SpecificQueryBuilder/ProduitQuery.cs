using OMEGA.Data_Classes;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
   static class ProduitQuery
   {

        static internal string MainQuery(List<int> listId,int IdAct)
        {
            string WhereClause = "";
            string mainquery;
            if (listId != null)
            {
                foreach (int id in listId)
                {
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = " WHERE (Produits.ID = '" + id + "' ";
                    }
                    else WhereClause = WhereClause + " OR Produits.ID = '" + id + "' ";
                }
                if (listId.Count == 0) WhereClause = " WHERE ( Produits.ID = '-1'";
            }
            else WhereClause = " WHERE Produits.ID = '-1'";
            mainquery = "SELECT distinct Def_Categ.Nom as groupe ,Produits.Nom, SystemeUnite.UAte as U_atelier," +
                "SystemeUnite.UEnt as U_Entreprise , SystemeUnite.UGlobal as U_Global ,Produits.Prix," +
                "TVA.Nom as 'TVA' ,Prod_Quantite.Quantite_Avant_1 as 'Quantite avant 1',"+
                "Prod_Quantite.Quantite_1 as 'Quantite 1', Produits.IdDefCateg, Produits.IdSysUnit, " +
                " Produits.Idtva,Produits.ID " +
                " FROM Produits JOIN SystemeUnite on Produits.IdSysUnit = SystemeUnite.IdSysUnit " +
                "JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Produits.IdTva = TVA.IdTva " +
                "JOIN Prod_Quantite ON Prod_Quantite.IdProduits = Produits.ID " +
                WhereClause + ") ";
            if (IdAct > 0 )
            {
                if (listId.Count >0)
                {
                    mainquery = mainquery + " AND (";
                    foreach (int id in listId)
                    {
                        string query = "Select IdActivite From Prod_Quantite Where IdProduits = '" + id + "';";
                        List<int> list = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);
                        if (Commun.ListHasValue(list))
                        {
                            if (listId.IndexOf(id) == 0)
                            {
                                mainquery = mainquery + " Prod_Quantite.IdActivite = '" + IdAct + "'";
                            }
                            else mainquery = mainquery + " AND Prod_Quantite.IdActivite = '" + IdAct + "'";
                        }
                        else
                        {
                            if (listId.IndexOf(id) == 0)
                            {
                                mainquery = mainquery + " Prod_Quantite.IdActivite is null ";
                            }
                            else mainquery = mainquery + " OR Prod_Quantite.IdActivite is null ";
                        }
                    }
                    mainquery = mainquery +");";
                }
            }
            else
            {
                mainquery = mainquery + ";";
            }
            return mainquery;
        }

        static internal string MainQuery2(List<int> listId, int IdAct,string ActName)
        {
            string WhereClause = "";
            string mainquery;
            if (listId != null)
            {
                foreach (int id in listId)
                {
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = " WHERE (Produits.ID = '" + id + "' ";
                    }
                    else WhereClause = WhereClause + " OR Produits.ID = '" + id + "' ";
                }
                if (listId.Count == 0) WhereClause = " WHERE ( Produits.ID = '-1'";
            }
            else WhereClause = " WHERE Produits.ID = '-1'";

            mainquery = "SELECT distinct Def_Categ.Nom as groupe ,Produits.Nom, SystemeUnite.UAte as U_atelier," +
                "SystemeUnite.UEnt as U_Entreprise , SystemeUnite.UGlobal as U_Global, " +
                "Produits.Prix,TVA.Nom as 'TVA', Prod_Perenne.Quantite_Avant_1 as 'Avant 1'," + Commun.GetPhase(ActName) +
                " , Produits.IdDefCateg, Produits.IdSysUnit, Produits.Idtva,Produits.ID " +
                " FROM Produits JOIN SystemeUnite on Produits.IdSysUnit = SystemeUnite.IdSysUnit " +
                "JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Produits.IdTva = TVA.IdTva " +
                "JOIN Prod_Perenne ON Prod_Perenne.IdProduits = Produits.ID " +
                WhereClause + ");";
           
            return mainquery;
        }

        static internal string MainQuery3(List<int> listId, int IdAct, string ActName)
        {
            string WhereClause = "";
            string mainquery;
            if (listId != null)
            {
                foreach (int id in listId)
                {
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = " WHERE (Produits.ID = '" + id + "' ";
                    }
                    else WhereClause = WhereClause + " OR Produits.ID = '" + id + "' ";
                }
                if (listId.Count == 0) WhereClause = " WHERE ( Produits.ID = '-1'";
            }
            else WhereClause = " WHERE Produits.ID = '-1'";

            mainquery = "SELECT distinct Def_Categ.Nom as groupe ,Produits.Nom, SystemeUnite.UAte as U_atelier," +
                "SystemeUnite.UEnt as U_Entreprise , SystemeUnite.UGlobal as U_Global " +
                "Produits.Prix,TVA.Nom as 'TVA', Prod_Plurianuelle.Quantite_Avant_1 as 'Avant 1',Prod_Plurianuelle.1 as Ph1," +
                "Prod_Plurianuelle.2 as Ph2,Prod_Plurianuelle.3 as Ph3,Prod_Plurianuelle.4 as Ph4 " +
                " , Produits.IdDefCateg, Produits.IdSysUnit, Produits.Idtva,Produits.ID " +
                " FROM Produits JOIN SystemeUnite on Produits.IdSysUnit = SystemeUnite.IdSysUnit " +
                "JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Produits.IdTva = TVA.IdTva " +
                WhereClause + ") ";
           
            return mainquery;
        }

              
       

        static internal string AddItemInGroupQuery(KeyValuePair<int, InfoUserProduit> item)
        {
            string query =  "INSERT INTO Produits (Nom , IdSysUnit, Prix,IdTva,IdDefCateg) " +
                   "VALUES ('" + item.Value.Nom + "','" + item.Value.IdsystUnit +
                   "','" + item.Value.Price + "','" + item.Value.IdTVA + "','" +
                    item.Value.IdDefCateg + "');";
            return query;
        }

        static internal string UpdateValueQuery(KeyValuePair<int, InfoUserProduit> item)
        {
            string query = "UPDATE Produits Set IdDefCateg = '" + item.Value.IdDefCateg + "', Nom = '" +
                           item.Value.Nom + "', IdSysUnit = '" + item.Value.IdsystUnit + "', Prix = '" + 
                           item.Value.Price +   "', IdTva ='" + item.Value.IdTVA + 
                           "' WHERE ID = '" + item.Value.ID + "';";
            return query;
        }

        static internal string AddSelectedProduitQuery(int ID)
        {
            string query = "SELECT Def_Categ.Nom as groupe ,Produits.Nom, SystemeUnite.UAte as U_atelier," +
                    "SystemeUnite.UEnt as U_Entreprise , SystemeUnite.UGlobal as U_Global ,Produits.Prix," +
                    "TVA.Nom as 'TVA',Prod_Quantite.Quantite_1 as 'Quantite 1'," +
                    "Prod_Quantite.Quantite_Avant_1 as 'Quantite avant 1', Produits.IdDefCateg, Produits.IdSysUnit, " +
                    "Produits.Idtva,Produits.ID" +
                    " FROM Produits JOIN SystemeUnite on Produits.IdSysUnit = SystemeUnite.IdSysUnit " +
                    " JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Produits.IdTva = TVA.IdTva " +
                    " JOIN Prod_Quantite ON Prod_Quantite.IdProduits = Produits.ID " +
                    " WHERE Produits.ID = '" + ID + "';";
            return query;
        }

        static internal string AddSelectedProduitQuery2(int ID, int idact)
        {
            string mainquery = "SELECT distinct Def_Categ.Nom as groupe ,Produits.Nom, SystemeUnite.UAte as U_atelier,SystemeUnite.UEnt as U_Entreprise , " +
              " SystemeUnite.UGlobal as U_Global, Produits.Prix,TVA.Nom as 'TVA'," +
              " Prod_Perenne.Quantite_Avant_1," + Commun.GetPhase(SQLQueryBuilder.FindName("Activite", "Nom", "ID", idact)) + ", " +
              " Produits.IdDefCateg, Produits.IdSysUnit, Produits.Idtva,Produits.ID FROM Produits " +
              " JOIN SystemeUnite on Produits.IdSysUnit = SystemeUnite.IdSysUnit " +
              " JOIN Prod_Perenne ON Prod_Perenne.IdProduits = Produits.ID " +
              " JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Produits.IdTva = TVA.IdTva " +
               " Where Produits.ID = '" + ID + "';";
            return mainquery;
        }

        static internal string AddSelectedProduitQuery3(int ID, int idact)
        {
            string mainquery = "SELECT distinct Def_Categ.Nom as groupe ,Produits.Nom, SystemeUnite.UAte as U_atelier,SystemeUnite.UEnt as U_Entreprise," +
               " SystemeUnite.UGlobal as U_Global ,Produits.Prix,TVA.Nom as 'TVA', Produit_Plurianuelle.Quantite_Avant_1 as 'Avant 1',Produit_Plurianuelle.1 as Ph1," +
               " Produit_Plurianuelle.2 as Ph2,Produit_Plurianuelle.3 as Ph3,Produit_Plurianuelle.4 as Ph4  Produits.IdDefCateg, Produits.IdSysUnit, " +
               " Produits.Idtva,Produits.ID FROM Produits " +
               " JOIN SystemeUnite on Produits.IdSysUnit = SystemeUnite.IdSysUnit " +
               " JOIN Produit_Plurianuelle ON Produit_Plurianuelle.IdProduits = Produits.ID " +
               " JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Produits.IdTva = TVA.IdTva " +
                " Where Produits.ID = '" + ID + "';";
            return mainquery;
        }

        static internal string AddSelectedProduitQuery2(int ID)
        {
            string query = "SELECT Produits.Nom , Def_Categ.Nom as groupe , SystemeUnite.UAte as Unite " +
                  "FROM Produits JOIN SystemeUnite on Produits.IdSysUnit = SystemeUnite.IdSysUnit " +
                  "JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg " +
                  " WHERE Produits.ID = '" + ID + "' ;";
            return query;
        }

        static internal string AddSelectedProduitQuery3(int ID)
        {
            string query = "SELECT Produits.Id, Produits.Nom , Def_Categ.Nom as groupe" +
                  " FROM Produits JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg " +
                  " WHERE Produits.ID = '" + ID + "' ;";
            return query;
        }

        static internal string AddSelectedProduitQuery4(int ID)
        {
            string query = "SELECT Produits.Nom"  +
                    " FROM Produits JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg " +
                    "WHERE Produits.ID = '" + ID + "';";
            return query;
        }

        static internal string AddSelectedProduitQuery5(int ID)
        {
            string query = "SELECT 0,Produits.Id ,Produits.Nom" +
                    " FROM Produits JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg " +
                    "WHERE Produits.ID = '" + ID + "';";
            return query;
        }

        static internal string SelectProductOftheActivityQuery(List<int> listId,int IdAct)
        {
            string query="";
            string WhereClause = ""; 
            if (listId != null)
            {
                if (listId.Count == 0) WhereClause = " WHERE ( Produits.ID = '-1'";
                foreach (int id in listId)
                {
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = " WHERE ( Produits.ID = '" + id + "' ";
                    }
                    else WhereClause = WhereClause + " OR Produits.ID = '" + id + "' ";
                }
                WhereClause = WhereClause + ") AND Prod_Quantite.IdActivite = '" + IdAct+"';";
                query = "Select Produits.Nom, Def_Categ.Nom ,Produits.Prix , SystemeUnite.UAte, Prod_Quantite.Quantite_1 " +
                    "FROM Produits " +
                    "JOIN Def_Categ On Produits.IdDefCateg = Def_Categ.IdDefCateg " +
                    "JOIN Caract_Activite On Produits.ID = Caract_Activite.IdProduits " +
                    "JOIN Prod_Quantite On Produits.ID = Prod_Quantite.IdProduits " +
                    "JOIN SystemeUnite On Produits.IdSysUnit = SystemeUnite.IdSysUnit " + WhereClause;
            }
            return query;
        }

        static internal string SelectProductOftheActivityQueryPerenne(List<int> listId, int IdAct)
        {
            string query = "";
            string WhereClause = "";
            if (listId != null)
            {
                if (listId.Count == 0) WhereClause = " WHERE ( Produits.ID = '-1'";
                foreach (int id in listId)
                {
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = " WHERE ( Produits.ID = '" + id + "' ";
                    }
                    else WhereClause = WhereClause + " OR Produits.ID = '" + id + "' ";
                }
                WhereClause = WhereClause + ") AND Prod_Perenne.IdActivite = '" + IdAct + "';";
                query = "Select Produits.Nom, Def_Categ.Nom ,Produits.Prix , SystemeUnite.UAte, " + Commun.GetPhase(SQLQueryBuilder.FindName("Activite", "Nom", "ID", IdAct)) + 
                    "FROM Produits " +
                    "JOIN Def_Categ On Produits.IdDefCateg = Def_Categ.IdDefCateg " +
                    "JOIN Caract_Activite On Produits.ID = Caract_Activite.IdProduits " +
                    "JOIN Prod_Perenne On Produits.ID = Prod_Perenne.IdProduits " +
                    "JOIN SystemeUnite On Produits.IdSysUnit = SystemeUnite.IdSysUnit " + WhereClause;
            }
            return query;
        }

        static internal string SelectProductOftheActivityQueryPluriannuelle(List<int> listId, int IdAct)
        {
            string query = "";
            string WhereClause = "";
            if (listId != null)
            {
                if (listId.Count == 0) WhereClause = " WHERE ( Produits.ID = '-1'";
                foreach (int id in listId)
                {
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = " WHERE ( Produits.ID = '" + id + "' ";
                    }
                    else WhereClause = WhereClause + " OR Produits.ID = '" + id + "' ";
                }
                WhereClause = WhereClause + ") AND Prod_Pluriannuelle.IdActivite = '" + IdAct + "';";
                query = "Select Produits.Nom, Def_Categ.Nom ,Produits.Prix , SystemeUnite.UAte, Prod_Pluriannuelle.1 as Ph1, " +
                    "Prod_Pluriannuelle.2 as Ph2 , Prod_Pluriannuelle.3 as Ph3, Prod_Pluriannuelle.4 as Ph4 " +
                    "FROM Produits " +
                    "JOIN Def_Categ On Produits.IdDefCateg = Def_Categ.IdDefCateg " +
                    "JOIN Caract_Activite On Produits.ID = Caract_Activite.IdProduits " +
                    "JOIN Prod_Pluriannuelle On Produits.ID = Prod_Pluriannuelle.IdProduits " +
                    "JOIN SystemeUnite On Produits.IdSysUnit = SystemeUnite.IdSysUnit " + WhereClause;
            }
            return query;
        }

        static internal string SelectProductOftheActivityQuery2(List<int> listId, int IdAct)
        {
            string query = "";
            string WhereClause = "";
            if (listId != null)
            {
                if (listId.Count == 0) WhereClause = " WHERE ( Produits.ID = '-1'";
                foreach (int id in listId)
                {
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = " WHERE ( Produits.ID = '" + id + "' ";
                    }
                    else WhereClause = WhereClause + " OR Produits.ID = '" + id + "' ";
                }
                WhereClause = WhereClause + ") AND Prod_Quantite.IdActivite = '" + IdAct + "';";
                query = "Select Produits.Nom ,Produits.Prix, Prod_Quantite.Quantite_1 " +
                    "FROM Produits " +
                    "JOIN Caract_Activite On Produits.ID = Caract_Activite.IdProduits " +
                    "JOIN Prod_Quantite On Produits.ID = Prod_Quantite.IdProduits " +
                    "JOIN SystemeUnite On Produits.IdSysUnit = SystemeUnite.IdSysUnit " + WhereClause;
            }
            return query;
        }

        static internal string SelectProd_PiedActivityQuery(List<int> listId, int IdAct)
        {
            string query = "";
            string WhereClause = "";
            if (listId != null)
            {
                if (listId.Count == 0) WhereClause = " WHERE ( Produits.ID = '-1'";
                foreach (int id in listId)
                {
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = " WHERE (Produits.ID = '" + id + "' ";
                    }
                    else WhereClause = WhereClause + " OR Produits.ID = '" + id + "' ";
                }
                WhereClause = WhereClause + ") AND Caract_Activite.IdActivite = '" + IdAct + "';";
                query = "Select Produits.Nom, Def_Categ.Nom as categorie , SystemeUnite.UAte as Unite, " +
                    "Item_Pied.Avant_1 , Item_Pied._1,item_Pied.ID FROM Produits " +
                    "JOIN Item_Pied On Produits.ID = Item_Pied.IdProduits " +
                    "JOIN Caract_Activite On Produits.ID = Caract_Activite.IdProduits " +
                    "JOIN Def_Categ On Produits.IdDefCateg = Def_Categ.IdDefCateg " +
                    "JOIN SystemeUnite On Produits.IdSysUnit = SystemeUnite.IdSysUnit " + WhereClause;
            }
            return query;
        }

        internal static string AutoCompleteQuery()
        {
           string query = "Select distinct Def_Categ.Nom from Def_Categ " +
            "JOIN Produits on Produits.IdDefCateg = Def_Categ.IdDefCateg;";
            return query;
        }

        internal static string AddQuantiteQuery(KeyValuePair<int, InfoUserProduit> item,int idAct)
        {
            string query = "INSERT INTO Prod_Quantite (IdProduits , Quantite_Avant_1,Quantite_1,IdActivite) " +
                 "VALUES ('" + item.Value.ID + "','" + item.Value.QteAv1 + "','" + item.Value.Qte1 +
                 "','" + idAct +  "');";
            return query;
        }

        internal static string AddQuantiteQuery(double qteav1, double qte1, int idProduit, int idAct)
        {
            string query = "INSERT INTO Prod_Quantite (IdProduits , Quantite_Avant_1,Quantite_1,IdActivite) " +
                 "VALUES ('" + idProduit + "','" + qteav1 + "','" + qte1 +
                 "','" + idAct + "');";
            return query;
        }

        internal static string AddPerenneQuery(KeyValuePair<int, InfoUserProduit> item, int idAct)
        {
            string champ = "";
            int index = 1;
            string valuechamp = "";
            foreach (int value in item.Value.ListPhase)
            {
                champ = champ + "Ph" + index + ",";
                index++;
                valuechamp = valuechamp + "'"+ value + "',";
            }
            champ = champ.Remove(champ.Length - 1, 1);
            valuechamp = valuechamp.Remove(valuechamp.Length - 1, 1);

            string query = "INSERT INTO Prod_Perenne (IdProduits, Quantite_Avant_1,IdActivite,"+champ+") " +
                 "VALUES ('" + item.Value.ID + "','" + item.Value.QteAv1 + "'," + idAct  +
                 "," + valuechamp + ");";
            return query;
        }

        internal static string AddPerenneQuery(List<double> list, double qteav1, int IdProd, int idAct)
        {
            string champ = "";
            int index = 1;
            string valuechamp = "";
            foreach (int value in list)
            {
                champ = champ + "Ph" + index + ",";
                index++;
                valuechamp = valuechamp + "'" + value + "',";
            }
            champ = champ.Remove(champ.Length - 1, 1);
            valuechamp = valuechamp.Remove(valuechamp.Length - 1, 1);

            string query = "INSERT INTO Prod_Perenne (IdProduits, Quantite_Avant_1,IdActivite," + champ + ") " +
                 "VALUES ('" + IdProd + "','" + qteav1 + "','" + idAct +
                 "'," + valuechamp + ");";
            return query;
        }

        internal static string AddPluriannuelleQuery(KeyValuePair<int, InfoUserProduit> item, int idAct)
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

            string query = "INSERT INTO Prod_Pluriannuelle (IdProduit, Quantite_Avant_1,IdActivite," + champ + ") " +
                 "VALUES ('" + item.Value.ID + "','" + item.Value.QteAv1 + "','" + idAct +
                 "'," + valuechamp + ");";
            return query;
        }

        internal static string AddPluriannuelleQuery(List<double> list, double qteav1, int IdProd, int idAct)
        {
            string champ = "";
            int index = 1;
            string valuechamp = "";
            foreach (int value in list)
            {
                champ = champ + "Ph" + index + ",";
                index++;
                valuechamp = valuechamp + "'" + value + "',";
            }
            champ = champ.Remove(champ.Length - 1, 1);
            valuechamp = valuechamp.Remove(valuechamp.Length - 1, 1);

            string query = "INSERT INTO Prod_Pluriannuelle (IdProduit, Quantite_Avant_1,IdActivite," + champ + ") " +
                 "VALUES ('" + IdProd + "','" + qteav1 + "','" + idAct +
                 "'," + valuechamp + ");";
            return query;
        }

        static internal string UpdateQuantiteQuery(KeyValuePair<int, InfoUserProduit> item, int idAct)
        {
            string query = "UPDATE Prod_Quantite Set Quantite_Avant_1 = '" + item.Value.QteAv1 + "', Quantite_1 = '" +
                           item.Value.Qte1 + "', IdActivite = '" + idAct + 
                           "' WHERE IdProduits = '" + item.Value.ID + "';";
            return query;
        }

        static internal string UpdateQuantiteQuery(double qteav1,double qte1,int idProduit, int idAct)
        {
            string query = "UPDATE Prod_Quantite Set Quantite_Avant_1 = '" + qteav1 + "', Quantite_1 = '" +
                           qte1 + "', IdActivite = '" + idAct +
                           "' WHERE IdProduits = '" + idProduit + "';";
            return query;
        }

        internal static string UpdatePerenneQuery(KeyValuePair<int, InfoUserProduit> item, int idAct)
        {
            string champ = "";
            int index = 1;
            foreach (int value in item.Value.ListPhase)
            {
                champ = champ + "Ph" + index + " = '" + value +"',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "update Prod_Perenne Set Quantite_Avant_1 = '" + item.Value.QteAv1 + "', " + champ  ;
            return query;
        }

        internal static string UpdatePerenneQuery(List<double> list, double qteav1, int IdProd, int idAct)
        {
            string champ = "";
            int index = 1;
            foreach (int value in list)
            {
                champ = champ + "Ph" + index + " = '" + value + "',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "update Prod_Perenne Set Quantite_Avant_1 = '" + qteav1 + "', " + champ;
            return query;
        }

        internal static string UpdatePluriannuelleQuery(KeyValuePair<int, InfoUserProduit> item, int idAct)
        {
            string champ = "";
            int index = 1;
            foreach (int value in item.Value.ListPhase)
            {
                champ = champ + "Ph" + index + " = '" + value + "',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "update Prod_Pluriannuelle Set Quantite_Avant_1 = '" + item.Value.QteAv1 + "', " + champ  ;
            return query;
        }

        internal static string UpdatePluriannuelleQuery(List<double> list, double qteav1, int IdProd, int idAct)
        {
            string champ = "";
            int index = 1;
            foreach (int value in list)
            {
                champ = champ + "Ph" + index + " = '" + value + "',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "update Prod_Pluriannuelle Set Quantite_Avant_1 = '" + qteav1 + "', " + champ ;
            return query;
        }

        internal static string LoadAgriProduitQuery(int idExp)
        {
            string mainquery = "SELECT Produits.ID, Produits.Nom ,Def_Categ.Nom as groupe " +
                " FROM Produits JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg " +
                " JOIN Agri_Produits on Agri_Produits.IdProduits = Produits.ID" +
                " JOIN Exploitation on Exploitation.ID = Agri_Produits.IdExploitations" +
                " WHERE Agri_Produits.IdExploitations = '"+idExp+"';";
            return mainquery;
        }


        internal static string SelectProductOftheActivityQuery(int IdExp, int idProd)
        {
            string query = " Select Caract_Activite.IdActivite From Caract_Activite " +
                      " JOIN Caract_Exploitation on Caract_Exploitation.IdActivite = Caract_Activite.IdActivite" +
                      " Where Caract_Exploitation.IdExploitation = '" + IdExp + "' AND Caract_activite.IdProduits = '" + idProd + "';";
            return query;
        }

        internal static string SelectResult_calculQuery(int IdExp, int idProd,int year)
        {
            string query = "Select * From Result_Calcul WHere IdExploitations = '" + IdExp + "'" +
                          " AND Table_Origine = 'Agri_Produits' AND Annee ='" + year +
                           "' AND nom ='" + SQLQueryBuilder.FindName("Produits", "Nom", "ID", idProd) + "';";
            return query;
        }

        internal static string UpdateAgriProduitValueQuery(int year,string value,int ID)
        {
           string query = "Update Result_Calcul set Annee = '" + year + "', Valeur = '" + value +
                            "' Where ID = '" + ID + "';";
            return query;
        }

        static internal string UpdateValueQuery(int IdDefCateg, string NomDefCateg, int idSystUnit, string price,
            int IdTVA, int IdProduit)
        {
            string query = "UPDATE Produits Set IdDefCateg = '" + IdDefCateg + "', Nom = '" +
                  NomDefCateg + "', IdSysUnit = '" + idSystUnit + "', Prix = '" + price +
                    "', IdTva ='" + IdTVA +
                    "' WHERE ID = '" + IdProduit + "';";
            return query;
        }

        static internal string InsertProduitQuery(int IdDefCateg, string Nom, int idSystUnit, string price,
          int IdTVA)
        {
            string query = "INSERT INTO Produits (Nom ,IdDefCateg, IdSysUnit, Prix,IdTva) " +
                           "VALUES ('" + Nom + "','" + IdDefCateg + "','" + idSystUnit +
                            "','" + price + "','" + IdTVA + "');";
            return query;
        }



        internal static string InsertAgriProduitValueQuery(string nom,int year,string value,int IdExp)
        {
           string query = "Insert Into Result_Calcul (Table_Origine,Nom,Annee,Valeur,IdExploitations)" +
                           "VALUES ('Agri_Produits','" + nom  + "','" + year + "','" + value + "','" + IdExp + "');";
            return query;
        }
    }


}
