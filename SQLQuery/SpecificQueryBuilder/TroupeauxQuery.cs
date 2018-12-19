using OMEGA.Data_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
    static class TroupeauxQuery
    {
        internal static string MainQuery()
        {
            string query = "Select IdBestiaux,Def_Categ.Nom,Def_Bestiaux.Nom,Vallnv,Prix, TVA.Nom,Origine," +
                "Donne1,DonnePcent1,Donne2,DonnePcent2,Donne3,DonnePcent3,Donne4,DonnePcent4 " +
                "From Def_Bestiaux "+
                "JOIN TVA on TVA.Idtva = Def_Bestiaux.IdTva " +
                "JOIN Def_Categ on Def_Categ.IdDefCateg = Def_Bestiaux.IdDefCateg;";
            return query;
        }

        static internal string AddItemInGroupQuery(KeyValuePair<int, InfoUserTroupeaux> item)
        {
            string query = "INSERT INTO Def_Bestiaux (Nom,Prix,IdTva,Origine,IdDefCateg,Vallnv,Donne1," +
                "DonnePcent1,Donne2,DonnePcent2,Donne3,DonnePcent3,Donne4,DonnePcent4) " +
                   "VALUES ('" + item.Value.Nom + "','" + item.Value.Price +
                   "','" + item.Value.IdTVA + "','" + item.Value.Origine + "','" +
                    item.Value.IdDefCateg + "','" + item.Value.ValInventaire + "','" +
                    item.Value.data1 + "','" + item.Value.percent1 + "','" +
                    item.Value.data2 + "','" + item.Value.percent2 + "','" +
                    item.Value.data3 + "','" + item.Value.percent3 + "','" +
                    item.Value.data4 + "','" + item.Value.percent4  + "');";
            return query;
        }

        static internal string UpdateValueQuery(KeyValuePair<int, InfoUserTroupeaux> item)
        {
            string query = "UPDATE Def_Bestiaux Set IdDefCateg = '" + item.Value.IdDefCateg + "', Nom = '" +
                           item.Value.Nom + "', Prix = '" + item.Value.Price +
                           "', IdTva ='" + item.Value.IdTVA + "', Origine ='" + item.Value.Origine +
                           "',Vallnv  ='" + item.Value.ValInventaire + "', Donne1 ='" + item.Value.data1 +
                           "',DonnePcent1  ='" + item.Value.percent1 + "', Donne2 ='" + item.Value.data2 +
                           "',DonnePcent2  ='" + item.Value.percent2 + "', Donne3 ='" + item.Value.data3 +
                           "',DonnePcent3  ='" + item.Value.percent3 + "', Donne4 ='" + item.Value.data4 +
                           "',DonnePcent4  ='" + item.Value.percent4 +
                           "' WHERE IdBestiaux = '" + item.Value.ID + "';";
            return query;
        }

        internal static string AutoCompleteQuery()
        {
            string query = "Select distinct Def_Categ.Nom from Def_Categ " +
           "JOIN Def_Bestiaux on Def_Bestiaux.IdDefCateg = Def_Categ.IdDefCateg;";
            return query;
        }
    }
}
