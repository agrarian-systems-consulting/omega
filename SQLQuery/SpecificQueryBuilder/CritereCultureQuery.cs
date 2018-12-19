using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
    static class CritereCultureQuery
    {

        internal static string PiedHaMainQuery(int idAct)
        {
            string query = "Select ID,Qte_Av_1,Ph1 From PiedHa WHERE IdActivite = " + idAct + ";";
            return query;
        }


        internal static string PiedHaMainQuery2(int idAct)
        {
            string query = "Select ID,Qte_Av_1," + Commun.GetPhase(SQLQueryBuilder.FindName("Activite", "Nom", "ID", idAct), false) +
                " From PiedHa WHERE IdActivite = " + idAct + ";";
            return query;
        }


        internal static string PiedHaMainQuery3(int idAct)
        {
            string query = "Select ID,Qte_Av_1,Ph1,Ph2,Ph3,Ph4 From PiedHa WHERE IdActivite = " + idAct + ";";
            return query;
        }


        internal static string PiedHaUpdateQuery(int ID, DataGridViewRow row)
        {
            string query = "Update PiedHa Set Qte_Av_1 = '" + row.Cells[1].Value.ToString() + "',Ph1 ='" + row.Cells[2].Value.ToString()
                + "' WHere ID = '" + ID + "';";
            return query;
        }

        internal static string PiedHaUpdateQuery2(int ID, DataGridViewRow row, int nbcolonne)
        {
            string field = "'";
            for (int i = 1; i <= nbcolonne; i++)
            {
                field = field + "," + i + "='" + row.Cells[i + 1].Value.ToString() + "'";
            }
            string query = "Update PiedHa Set Qte_Av_1 = '" + row.Cells[1].Value.ToString() + field
                + " WHere ID = '" + ID + "';";
            return query;
        }

        internal static string PiedHaUpdateQuery3(int ID, DataGridViewRow row)
        {
            string query = "Update PiedHa Set Qte_Av_1 = '" + row.Cells[1].Value.ToString() +
                "',1 ='" + row.Cells[2].Value.ToString() +
                "',2 ='" + row.Cells[3].Value.ToString() +
                "',3 ='" + row.Cells[4].Value.ToString() +
                "',4 ='" + row.Cells[5].Value.ToString() +
                "' WHere ID = '" + ID + "';";
            return query;
        }

        internal static string PiedHaInsertQuery(DataGridViewRow row, int idact)
        {
            string query = "Insert into PiedHa (Qte_Av_1,Ph1,IdActivite) VALUES ('" + row.Cells[1].Value.ToString() +
                 "','" + row.Cells[2].Value.ToString() + "','" + idact + "');";
            return query;
        }

        internal static string PiedHaInsertfieldQuery2(int nbcolonne)
        {
            string field = "";
            for (int i = 1; i <= nbcolonne; i++)
            {
                field = field + ",Ph" + i;
            }
            string query = "Insert into PiedHa (Qte_Av_1" + field + ")"; 
            return query;
        }

        internal static string PiedHaInsertQuery2(DataGridViewRow row, int idact, int nbcolonne)
        {
            string field = "";
            for (int i = 2; i <= nbcolonne; i++)
            {
                field = field + "','" + row.Cells[i].Value.ToString();
            }
            string query = "('" + row.Cells[1].Value.ToString() + field;
            return query + "');";
        }

        internal static string PiedHaInsertQuery3(DataGridViewRow row, int idact)
        {
            string query = "Insert into PiedHa (Qte_Av_1,Ph1,Ph2,Ph3,Ph4,IdActivite) VALUES ('" + row.Cells[1].Value.ToString() +
                 "','" + row.Cells[2].Value.ToString() +
                 "','" + row.Cells[3].Value.ToString() +
                 "','" + row.Cells[4].Value.ToString() +
                 "','" + row.Cells[5].Value.ToString() +
                 "','" + idact + "');";
            return query;
        }

        internal static string ItemPiedMainQuery(int idact,string ProdCharfield,string table)
        {
            string query = "Select Item_Pied.ID, " + table+ ".Nom,Def_Categ.Nom as Groupe,SystemeUnite.UAte, Qte_Av_1,Ph1 from Item_Pied" +
                 " JOIN "+ table +" On " + table + ".ID = item_Pied." + ProdCharfield + 
                 " JOIN Def_Categ On item_Pied." + ProdCharfield + " = Def_Categ.IdDefCateg " +
                 " JOIN SystemeUnite On " + table + ".IdSysUnit = SystemeUnite.IdSysUnit " +
                 " Where " + ProdCharfield + " is not null And IdActivite ='" + idact + "'";
            return query;
        }

        internal static string ItemPiedMainQuery3(int idact, string ProdCharfield, string table)
        {
            string query = "Select ID, " + table + ".Nom,Def_Categ.Nom,SystemeUnite.UAte, Qte_Av_1,"
                 + Commun.GetPhase(SQLQueryBuilder.FindName("Activite","Nom","ID",idact),false)+" from Item_Pied" +
                 " JOIN " + table + " On " + table + ".ID = item_Pied." + ProdCharfield +
                 " JOIN Def_Categ On item_Pied." + ProdCharfield + " = Def_Categ.IdDefCateg " +
                 " JOIN SystemeUnite On " + table + ".IdSysUnit = SystemeUnite.IdSysUnit " +
                 " Where " + ProdCharfield + " is not null And IdActivite ='" + idact + "'";
            return query;
        }

        internal static string ItemPiedMainQuery2(int idact, string ProdCharfield, string table)
        {
            string query = "Select ID, " + table + ".Nom,Def_Categ.Nom,SystemeUnite.UAte, Qte_Av_1,Ph1,Ph2,Ph3,Ph4 from Item_Pied" +
                 " JOIN " + table + " On " + table + ".ID = item_Pied." + ProdCharfield +
                 " JOIN Def_Categ On item_Pied." + ProdCharfield + " = Def_Categ.IdDefCateg " +
                 " JOIN SystemeUnite On " + table + ".IdSysUnit = SystemeUnite.IdSysUnit " +
                 " Where " + ProdCharfield + " is not null And IdActivite ='" + idact + "'";
            return query;
        }


        internal static string ItemPiedUpdateQuery(DataGridViewRow row, int IdAct,string IdField,int ID,string table)
        {
             string query = " Update Item_pied Set "+IdField+" = '"+SQLQueryBuilder.FindId(table,"ID","Nom",row.Cells[1].Value.ToString()) +
                "', Qte_Av_1 = '" + row.Cells[4].Value.ToString() +
                "', Ph1 = '" + row.Cells[5].Value.ToString() +
                "' WHERE ID = '" + ID + "' AND IdActivite = '" + IdAct + "';";
            return query;
        }

        internal static string ItemPiedUpdateQuery2(DataGridViewRow row, int IdAct, string IdField, int ID,int nbcolonne, string table)
        {
            string field = "'";
            for (int i = 5;i<nbcolonne;i++)
            {
                field = field + ",Ph" + (i - 4) + "='" + row.Cells[i].Value.ToString() + "'";
            }

            string query = " Update Item_pied Set " + IdField + " = '" + SQLQueryBuilder.FindId(table, "ID", "Nom", row.Cells[1].Value.ToString()) 
                + "', Qte_Av_1 = '" + row.Cells[4].Value.ToString() + field 
                + " WHERE ID = '" + ID + "' AND IdActivite = '" + IdAct + "';";
            return query;
        }

        internal static string ItemPiedUpdateQuery3(DataGridViewRow row, int IdAct, string IdField, int ID,string table)
        {
            string query = " Update Item_Pied Set  " + IdField + " = '" + SQLQueryBuilder.FindId(table, "ID", "Nom", row.Cells[1].Value.ToString()) +
                "' ,Qte_Av_1 = '" + row.Cells[4].Value.ToString() +
                "', Ph1 = '" + row.Cells[5].Value.ToString() +
                "', Ph2 = '" + row.Cells[6].Value.ToString() +
                "', Ph3 = '" + row.Cells[7].Value.ToString() +
                "', Ph4 = '" + row.Cells[8].Value.ToString() +
               "' WHERE ID = '" + ID + "' AND IdActivite = '" + IdAct + "';";
            return query;
        }

        internal static string ItemPiedInsertQuery(DataGridViewRow row, int idact, string IdField,string table)
        {
            string query = "Insert into Item_Pied (Qte_Av_1,Ph1,IdActivite,"+IdField+") VALUES ('" + row.Cells[4].Value.ToString() +
                 "','" + row.Cells[5].Value.ToString() + "','" + idact + "','"+ SQLQueryBuilder.FindId(table, "ID", "Nom", row.Cells[1].Value.ToString()) + "',);";
            return query;
        }

        internal static string ItemPiedInsertfieldQuery2(int nbcolonne,string IdField)
        {
            string field = "";
            for (int i = 1; i <= nbcolonne; i++)
            {
                field = field + ",Ph" + i;
            }
            string query = "Insert into Item_Pied (Qte_Av_1,"+IdField + field + ")";
            return query;
        }

        internal static string ItemPiedInsertQuery2(DataGridViewRow row, int idact, int nbcolonne)
        {
            string field = "";
            for (int i = 5; i <= nbcolonne; i++)
            {
                field = field + "','" + row.Cells[i].Value.ToString();
            }
            string query = "('" + row.Cells[4].Value.ToString() + row.Cells[1].Value.ToString() + field;
            return query + "');";
        }

        internal static string ItemPiedInsertQuery3(DataGridViewRow row, int idact, string IdField)
        {
            string query = "Insert into PiedHa (Qte_Av_1,Ph1,Ph2,Ph3,Ph4,IdActivite,"+ IdField + ") VALUES ('" + row.Cells[4].Value.ToString() +
                 "','" + row.Cells[5].Value.ToString() +
                 "','" + row.Cells[6].Value.ToString() +
                 "','" + row.Cells[7].Value.ToString() +
                 "','" + row.Cells[8].Value.ToString() +
                 "','" + idact +
                  "','" + row.Cells[1].Value.ToString() +"');";
            return query;
        }

        internal static string ItemPiedDeleteQuery(int id, string IdTable, int IdAct)
        {
            string query = "Delete From Item_Pied Where " + IdTable + " ='" + id +
                "' AND IdActivite = '" + IdAct + "'";
            return query;
        }

        internal static string ItemPiedDeleteQueryCaractAct(int id, string IdTable, int IdAct)
        {
            string query = "Delete From Caract_Activite Where " + IdTable + " = '" + id +
                "' AND IdActivite = '" + IdAct + "'";
            return query;
        }

        internal static string AddItemToGridQuery(int id,int type,string table,string ProdCharfield,int idact)
        {
            string query = "";
            switch (type)
            {

                case 1: //annuelle
                     query = "Select Item_Pied.ID, " + table + ".Nom,Def_Categ.Nom as Groupe,SystemeUnite.UAte, Qte_Av_1,Ph1 from Item_Pied" +
                " JOIN " + table + " On " + table + ".ID = item_Pied." + ProdCharfield +
                " JOIN Def_Categ On item_Pied." + ProdCharfield + " = Def_Categ.IdDefCateg " +
                " JOIN SystemeUnite On " + table + ".IdSysUnit = SystemeUnite.IdSysUnit " +
                " Where IdActivite ='" + idact + "' AND Item_Pied."+ProdCharfield+" = '" + id + "'"; ;
                    break;
                case 4: //perenne
                    query = "Select ID, " + table + ".Nom,Def_Categ.Nom,SystemeUnite.UAte, Qte_Av_1,"
                 + Commun.GetPhase(SQLQueryBuilder.FindName("Activite", "Nom", "ID", idact), false) + " from Item_Pied" +
                 " JOIN " + table + " On " + table + ".ID = item_Pied." + ProdCharfield +
                 " JOIN Def_Categ On item_Pied." + ProdCharfield + " = Def_Categ.IdDefCateg " +
                 " JOIN SystemeUnite On " + table + ".IdSysUnit = SystemeUnite.IdSysUnit " +
                 " Where IdActivite ='" + idact + "' AND Item_Pied." + ProdCharfield + " = '" + id + "'"; ;
                    break;
                case 6: // pluriannuelle
                  query = "Select ID, " + table + ".Nom,Def_Categ.Nom,SystemeUnite.UAte, Qte_Av_1,Ph1,Ph2,Ph3,Ph4 from Item_Pied" +
                 " JOIN " + table + " On " + table + ".ID = item_Pied." + ProdCharfield +
                 " JOIN Def_Categ On item_Pied." + ProdCharfield + " = Def_Categ.IdDefCateg " +
                 " JOIN SystemeUnite On " + table + ".IdSysUnit = SystemeUnite.IdSysUnit " +
                 " Where IdActivite ='" + idact + "' AND Item_Pied." + ProdCharfield + " = '" + id + "'"; ;
                    break;
            }
            return query;
        }

    }

  
}
