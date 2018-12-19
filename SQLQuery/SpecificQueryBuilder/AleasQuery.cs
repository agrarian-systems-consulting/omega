using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
    static class AleasQuery
    {

        internal static string GetMainQuery(bool isprice,string table,int currentIdAlea,string field100An)
        {
            string query = "";
            if (isprice)
            {
                query = "Select distinct Alea_Item.ID," + table + ".ID," + table + ".Nom,Def_Categ.Nom,1 as 'Base'," + field100An + " From Alea_Item " +
                "Join " + table + " on " + table + ".ID = Alea_Item.IdProduits " +
                "Join Def_Categ on " + table + ".IdDefCateg = Def_Categ.IdDefCateg " +
                "Join Caract_Activite on Caract_Activite.IdProduits = Alea_Item.IdProduits " +
                "Join Activite on Activite.ID = Caract_Activite.IdActivite" +
                " WHERE Alea_Item.IdAleaCateg = '" + currentIdAlea + "';";
            }
            else
            {
                query = "Select distinct Alea_Item.ID," + table + ".ID," + table + ".Nom,Activite.Nom,1 as 'Base'," + field100An + " From Alea_Item " +
                   "Join " + table + " on " + table + ".ID = Alea_Item.IdProduits " +
                   "Join Def_Categ on " + table + ".IdDefCateg = Def_Categ.IdDefCateg " +
                   "Join Caract_Activite on Caract_Activite.IdProduits = Alea_Item.IdProduits " +
                   "Join Activite on Activite.ID = Caract_Activite.IdActivite" +
                   " WHERE Alea_Item.IdAleaCateg = '" + currentIdAlea + "';";
            }
            return query;
        }
        

        static internal string InsertQuery(string table,string insertfieldRow,DataGridViewRow row,int currentid,string insertvalue)
        {
            string query = "Insert Into Alea_Item (Id" + table + ",base,IdAleaCateg," + insertfieldRow + ")" +
                            " VALUES ('" + row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString()
                            + "','" + currentid + "'," + insertvalue + ");";
            return query;
        }

        static internal string UpdateQuery(string updateAn,int id)
        {
            string query = "Update Alea_Item Set " + updateAn + " WHERE ID = '" + id + "';";
            return query;
        }

        static internal string SelectActiviteQuery(string table,int ID)
        {
            string query = "Select distinct Activite.Nom,Activite.ID From Activite" +
                            " Join Caract_Activite on Caract_Activite.IdActivite = Activite.Id" +
                            " Where Caract_Activite.Id" + table + "='" + ID + "';";
            return query;
        }

        static internal string SelectAleaID(string table,string id,int currentIdAlea,string Anfield)
        {
           string query = "Select distinct Alea_Item.ID," + table + ".ID," + table + ".Nom,Activite.Nom,1 as 'Base'," + Anfield + " From Alea_Item " +
                        " Join " + table + " on " + table + ".ID = Alea_Item.Id" + table +
                        " Join Caract_Activite on Caract_Activite.Id" + table + "= Alea_Item.Id" + table +
                        " Join Activite on Activite.ID = Caract_Activite.IdActivite" +
                        " WHERE Activite.ID = '" + id + "' AND IdAleaCateg ='" + currentIdAlea + "';";
           return query;
        }



    }
}
