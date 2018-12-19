using OMEGA.Data_Classes;
using OMEGA.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
    class DictionnaryQuery
    {


        static internal string MainQuery(List<int> listId)
        {
            string WhereClause = "";
            string mainquery;
            if (listId != null)
            {

                foreach (int id in listId)
                {
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = " WHERE Produits.ID = '" + id + "' ";
                    }
                    else WhereClause = WhereClause + " OR Produits.ID = '" + id + "' ";
                }
                if (listId.Count == 0) WhereClause = " WHERE Produits.ID = '-1'";
            }
            else WhereClause = " WHERE Produits.ID = '-1'";
            mainquery = "SELECT Def_Categ.Nom as groupe ,Produits.Nom, SystemeUnite.UAte as U_atelier," +
                "SystemeUnite.UEnt as U_Entreprise , SystemeUnite.UGlobal as U_Global ,Produits.Prix," +
                "TVA.Nom as 'TVA',Produits.Quantite_1 as 'Quantite 1',Produits.Quantite_2 as 'Quantite 2' " +
                ",Produits.Quantite_avant_1 as 'Quantite avant 1', Produits.IdDefCateg, Produits.IdSysUnit, " +
                "Produits.Idtva,Produits.ID" +
                " FROM Produits JOIN SystemeUnite on Produits.IdSysUnit = SystemeUnite.IdSysUnit " +
                "JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Produits.IdTva = TVA.IdTva " +
               WhereClause + " ;";

            return mainquery;
        }

        static internal string AddItemInGroupQuery(KeyValuePair<int, InfoUserProduit> item)
        {
            string query = "INSERT INTO Produits (Nom , IdSysUnit, Prix,IdTva,IdDefCateg,Quantite_1,Quantite_2,Quantite_avant_1) " +
                   "VALUES ('" + item.Value.Nom + "','" + item.Value.IdsystUnit +
                   "','" + item.Value.Price + "','" + item.Value.IdTVA + "','" +
                    item.Value.IdDefCateg + "','" + item.Value.Qte1 + "','" +
                    item.Value.Qte2 + "','" + item.Value.QteAv1 + "');";
            return query;
        }

        static internal string UpdateValueQuery(KeyValuePair<int, InfoUserProduit> item)
        {
            string query = "UPDATE Produits Set IdDefCateg = '" + item.Value.IdDefCateg + "', Nom = '" +
                           item.Value.Nom + "', IdSysUnit = '" + item.Value.IdsystUnit + "', Prix = '" + item.Value.Price +
                           "', IdTva ='" + item.Value.IdTVA + "', Quantite_1 ='" + item.Value.Qte1 + "', Quantite_2 ='" + item.Value.Qte2 +
                           "', Quantite_avant_1 ='" + item.Value.QteAv1 +
                           "' WHERE ID = '" + item.Value.ID + "';";
            return query;
        }

        static internal string AddSelectedProduitQuery(int ID)
        {
            string query = "SELECT Def_Categ.Nom as groupe ,Produits.Nom, SystemeUnite.UAte as U_atelier," +
                    "SystemeUnite.UEnt as U_Entreprise , SystemeUnite.UGlobal as U_Global ,Produits.Prix," +
                    "TVA.Nom as 'TVA',Produits.Quantite_1 as 'Quantite 1',Produits.Quantite_2 as 'Quantite 2' " +
                    ",Produits.Quantite_avant_1 as 'Quantite avant 1', Produits.IdDefCateg, Produits.IdSysUnit, " +
                    "Produits.Idtva,Produits.ID" +
                    " FROM Produits JOIN SystemeUnite on Produits.IdSysUnit = SystemeUnite.IdSysUnit " +
                    "JOIN Def_Categ on Produits.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Produits.IdTva = TVA.IdTva " +
                    "WHERE Produits.ID = '" + ID + "';";
            return query;
        }

        static internal string AddOrUpdateDicoQuery(Village item,int id)
        {
           string query;
           if(item.Update)
            {
               query = "UPDATE VILLAGE Set Nom = '" + item.Nom + "', CAN = '" + item.Canton +
                "',DEP = '" + item.Dept + "',PRO = '" + item.Province +
                "',LAT = '" + item.Lattitude + "',LON = '" + item.Longitude +
                "' WHERE ID = '" + id + "';";
            }
            else
            {    
              query = "INSERT INTO VILLAGE (NOM,CAN,DEP,PRO,LAT,LON) " +
              "VALUES ('" + item.Nom + "','" + item.Canton + "','" + item.Dept + "','" +
              item.Province + "','" + item.Lattitude + "','" + item.Longitude + "');";       
            }
            return query;
        }

        static internal string AddOrUpdateDicoQuery(Nature item, int id)
        {
            string query;
            if (item.Update)
            {
                query = "UPDATE NATURE Set NATURE = '" + item.Occupation + "', A_PRECISER = '" + item.A_preciser +
                                   "' WHERE ID = '" + id + "';";
            }
            else
            {
                query = "INSERT INTO NATURE (A_PRECISER,NATURE) " +
                "VALUES ('" + item.A_preciser + "','" + item.Occupation + "');";
            }
            return query;
        }
    }
}
    
