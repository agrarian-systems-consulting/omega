using OMEGA.Data_Classes;
using System.Collections.Generic;
using System.Windows.Forms;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
   static internal class ChargeQuery
    {
        static internal string MainQuery(List<int> listId,int type,int IdAct)
        {
            string WhereClause = "";
            string mainquery;
            if (listId.Count != 0)
            {
                if (type != 2) WhereClause = "WHERE Structurelle = '" + type + "'";
                foreach (int id in listId)
                {
                    // if its the first, And + open bracket
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = WhereClause + " AND ( Charges.ID = '" + id + "' ";
                    }
                    // if its the last, close the bracket
                    if (listId.IndexOf(id) == listId.Count -1)
                    {
                        WhereClause = WhereClause + " OR Charges.ID = '" + id + "') ";
                    }
                    // otherwise add or clause
                    else WhereClause = WhereClause + " OR Charges.ID = '" + id + "' ";
                }
                
            }
            else if (type != 2) WhereClause = " WHERE Charges.ID = '-1' AND Structurelle = '"+type+"'";
            if (type == 2) //charge quantité
            {
                mainquery = " Select distinct Caract_Activite.IdCharges,Caract_Activite.IdProduits,Produits.Nom," +
                             " null,Charges.Nom, null," +
                             " Charge_Quantite.Quantite_Avant_1 as 'Quantite avant 1'," +
                             " Charge_Quantite.Quantite_1 as 'Quantite 1'" +
                             " FROM Caract_Activite " +
                             " JOIN Charge_Quantite ON Charge_Quantite.IdCharges = Caract_Activite.IdCharges " +
                             " JOIN Produits on Caract_Activite.IdProduits = Produits.ID" +
                             " JOIN Charges on Caract_Activite.IdCharges = Charges.ID " +
                              WhereClause;
            }
            else
            {
                mainquery = "SELECT distinct Def_Categ.Nom as groupe ,Charges.Nom, SystemeUnite.UAte as U_atelier,SystemeUnite.UEnt as U_Entreprise," +
                              " SystemeUnite.UGlobal as U_Global ,Charges.Prix,TVA.Nom as 'TVA',Charge_Quantite.Quantite_Avant_1 as 'Quantite avant 1'," +
                              " Charge_Quantite.Quantite_1 as 'Quantite 1'," +
                              " Charges.IdDefCateg, Charges.IdSysUnit, Charges.Idtva,Charges.ID"+
                              " FROM Charges " +
                              " JOIN SystemeUnite on Charges.IdSysUnit = SystemeUnite.IdSysUnit " +
                              " JOIN Charge_Quantite ON Charge_Quantite.IdCharges = Charges.ID " +
                              "JOIN Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Charges.IdTva = TVA.IdTva " +
                               WhereClause;
            }
            if (IdAct != 0)
            {
                if (listId.Count > 0)
                {
                    mainquery = mainquery + " AND (";
                    foreach (int id in listId)
                    {
                        string query = "Select IdActivite From Charge_Quantite Where IdCharges = '" + id + "';";
                        List<int> list = SQlQueryExecuter.RunQueryReaderInt("IdActivite", query);
                        if (Commun.ListHasValue(list))
                        {
                            if (listId.IndexOf(id) == 0)
                            {
                                mainquery = mainquery + " Charge_Quantite.IdActivite = '" + IdAct + "'";
                            }
                            else mainquery = mainquery + " AND Charge_Quantite.IdActivite = '" + IdAct + "'";
                        }
                        else
                        {
                            if (listId.IndexOf(id) == 0)
                            {
                                mainquery = mainquery + " Charge_Quantite.IdActivite is null ";
                            }
                            else mainquery = mainquery + " OR Charge_Quantite.IdActivite is null ";
                        }
                    }
                    mainquery = mainquery + ");";
                }
            }
            else
            {
                mainquery = mainquery + ";";
            }
            return mainquery;
        }

        static internal string MainQuery2(List<int> listId, int type, int IdAct)
        {
            string WhereClause = "";
            string mainquery;
            if (listId.Count != 0)
            {
                WhereClause = "WHERE Structurelle = '" + type + "'";
                foreach (int id in listId)
                {
                    // if its the first, And + open bracket
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = WhereClause + " AND (Charges.ID = '" + id + "' ";
                    }
                    // if its the last, close the bracket
                    if (listId.IndexOf(id) == listId.Count - 1)
                    {
                        WhereClause = WhereClause + " OR Charges.ID = '" + id + "') ";
                    }
                    // otherwise add or clause
                    else WhereClause = WhereClause + " OR Charges.ID = '" + id + "' ";
                }
            }
            else WhereClause = " WHERE Charges.ID = '-1' AND Structurelle = '" + type + "'";
            string ActName = SQLQueryBuilder.FindName("Activite", "Nom", "ID", IdAct);
            if (type == 2)
            {
                string null_ = "";
                for (int i = 1;i<=Commun.GetNbPhase(IdAct);i++)
                {
                    null_ = null_ + "null,";
                }
                null_.Remove(null_.Length - 1, 1);
                mainquery = " Select distinct Caract_Activite.IdCharges,Caract_Activite.IdProduits,Produits.Nom," +
                             " null,Charges.Nom, null," 
                             + Commun.GetPhase(ActName) +
                             " FROM Caract_Activite " +
                             " JOIN Charge_Perenne ON Charge_Perenne.IdCharges = Charges.ID " +
                             " JOIN Produits on Caract_Activite.IdProduits = Produits.ID" +
                             " JOIN Charges on Caract_Activite.IdCharges = Charges.ID " +
                              WhereClause;
            }
            else
            {
                mainquery = "SELECT distinct Def_Categ.Nom as groupe ,Charges.Nom, SystemeUnite.UAte as U_atelier,SystemeUnite.UEnt as U_Entreprise," +
                    " SystemeUnite.UGlobal as U_Global,Charges.Prix,TVA.Nom as 'TVA'," +
                             " Charge_Perenne.Quantite_Avant_1," + Commun.GetPhase(ActName) + ", Charges.IdDefCateg, Charges.IdSysUnit, Charges.Idtva,Charges.ID FROM Charges " +
                             " JOIN SystemeUnite on Charges.IdSysUnit = SystemeUnite.IdSysUnit " +
                             " JOIN Charge_Perenne ON Charge_Perenne.IdCharges = Charges.ID " +
                             " JOIN Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Charges.IdTva = TVA.IdTva " +
                              WhereClause + ";";
            }
            return mainquery;
        }

        static internal string MainQuery3(List<int> listId, int type, int IdAct)
        {
            string WhereClause = "";
            string mainquery;
            if (listId.Count != 0)
            {
                WhereClause = "WHERE Structurelle = '" + type + "'";
                foreach (int id in listId)
                {
                    // if its the first, And + open bracket
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = WhereClause + " AND ( Charges.ID = '" + id + "' ";
                    }
                    // if its the last, close the bracket
                    if (listId.IndexOf(id) == listId.Count - 1)
                    {
                        WhereClause = WhereClause + " OR Charges.ID = '" + id + "') ";
                    }
                    // otherwise add or clause
                    else WhereClause = WhereClause + " OR Charges.ID = '" + id + "' ";
                }

            }
            else WhereClause = " WHERE Charges.ID = '-1' AND Structurelle = '" + type + "'";
            if (type == 2)
            {
                mainquery = " Select distinct Caract_Activite.IdCharges,Caract_Activite.IdProduits,Produits.Nom," +
                            " null,Charges.Nom, null," +
                            " Charge_Plurianuelle.Quantite_Avant_1 as 'Avant 1',Charge_Plurianuelle.1 as Ph1," +
                            " Charge_Plurianuelle.2 as Ph2,Charge_Plurianuelle.3 as Ph3,Charge_Plurianuelle.4 as Ph4" +
                            " FROM Caract_Activite " +
                            " JOIN Charge_Plurianuelle ON Charge_Plurianuelle.IdCharges = Charges.ID " +
                            " JOIN Produits on Caract_Activite.IdProduits = Produits.ID" +
                            " JOIN Charges on Caract_Activite.IdCharges = Charges.ID " +
                             WhereClause;
            }
            else
            {
                mainquery = "SELECT distinct Def_Categ.Nom as groupe ,Charges.Nom, SystemeUnite.UAte as U_atelier,SystemeUnite.UEnt as U_Entreprise," +
                    " SystemeUnite.UGlobal as U_Global ,Charges.Prix,TVA.Nom as 'TVA', Charge_Plurianuelle.Quantite_Avant_1 as 'Avant 1',Charge_Plurianuelle._1 as Ph1," +
                    " Charge_Plurianuelle._2 as Ph2,Charge_Plurianuelle._3 as Ph3,Charge_Plurianuelle._4 as Ph4,  Charges.IdDefCateg, Charges.IdSysUnit, " +
                    " Charges.Idtva,Charges.ID FROM Charges " +
                    " JOIN SystemeUnite on Charges.IdSysUnit = SystemeUnite.IdSysUnit " +
                    " JOIN Charge_Plurianuelle ON Charge_Plurianuelle.IdCharges = Charges.ID " +
                    " JOIN Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Charges.IdTva = TVA.IdTva " +
                        WhereClause + ";";
            }
            return mainquery;
        }

        static internal string InsertChargeQuery(KeyValuePair<int, InfoUserCharges> item,int type)
        {
           string query = "INSERT INTO Charges (Nom ,IdDefCateg, IdSysUnit, Prix,IdTva,Structurelle,IdProduits) " +
                          "VALUES ('" + item.Value.Nom + "','" + item.Value.IdDefCateg + "','" + item.Value.IdsystUnit +
                           "','" + item.Value.Price + "','" + item.Value.IdTVA 
                           + "','" + type + "','" + item.Value.IdProduits + "');";
            return query;
        }


        static internal string InsertChargeQuery(int IdDefCateg,string Nom,int idSystUnit,string price,
            int IdTVA,int IdProduit, int type)
        {
            string query = "INSERT INTO Charges (Nom ,IdDefCateg, IdSysUnit, Prix,IdTva,Structurelle,IdProduits) " +
                           "VALUES ('" + Nom + "','" + IdDefCateg + "','" + idSystUnit +
                            "','" + price + "','" + IdTVA + "','" + type + "','" +
                           IdProduit + "');";
            return query;
        }

        static internal string InsertChargeInCaract_ActQuery(DataGridViewRow row, int IdAct)
        {
            string query = "INSERT INTO Caract_Activite (IdProduits,IdCharges,IdActivite) " +
                           "VALUES ('" + row.Cells[1].Value.ToString() + "','" + row.Cells[0].Value.ToString() +
                           "','" + IdAct + "');";
            return query;
        }

        static internal string UpdateValueQuery(KeyValuePair<int, InfoUserCharges> item)
        {
            string  query = "UPDATE Charges Set IdDefCateg = '" + item.Value.IdDefCateg + "', Nom = '" +
                    item.Value.Nom + "', IdSysUnit = '" + item.Value.IdsystUnit + "', Prix = '" + item.Value.Price +
                    "', IdTva ='" + item.Value.IdTVA + "', IdProduits = '" + item.Value.IdProduits +
                    "' WHERE ID = '" + item.Value.ID + "';";
            return query;
        }

        static internal string UpdateValueQuery(int IdDefCateg, string NomDefCateg, int idSystUnit, string price,
            int IdTVA, int IdProduit, int IdCharge)
        {
            string query = "UPDATE Charges Set IdDefCateg = '" + IdDefCateg + "', Nom = '" +
                  NomDefCateg + "', IdSysUnit = '" + idSystUnit + "', Prix = '" + price +
                    "', IdTva ='" + IdTVA + "', IdProduits = '" + IdProduit +
                    "' WHERE ID = '" + IdCharge + "';";
            return query;
        }

        static internal string UpdateChargeInCaractActQuery(KeyValuePair<int, InfoUserCharges> item)
        {
            string query = "UPDATE Caract_Activite Set IdProduits = '" + item.Value.IdProduits + ",IdCharges = '" + item.Value.ID +
                    "' WHERE ID = '" + item.Value.ID + "';";
            return query;
        }

        static internal string AddSelectedChargeQuery(int ID,int mIdAct)
        {
            string query = "SELECT Def_Categ.Nom as groupe ,Charges.Nom, SystemeUnite.UAte as U_atelier,SystemeUnite.UEnt as U_Entreprise," +
                "SystemeUnite.UGlobal as U_Global ,Charges.Prix,TVA.Nom as 'TVA'," +
                "Charge_Quantite.Quantite_1 as 'Quantite 1',Charge_Quantite.Quantite_Avant_1 as 'Quantite avant 1',"+
                " Charges.IdDefCateg, Charges.IdSysUnit,Charges.Idtva,Charges.ID" +
                " FROM Charges " +
                "JOIN Charge_Quantite ON Charge_Quantite.IdCharges = Charges.ID " +
                "JOIN SystemeUnite on Charges.IdSysUnit = SystemeUnite.IdSysUnit " +
                "JOIN Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Charges.IdTva = TVA.IdTva " +
                " WHERE Charges.ID = '" + ID + "' AND Charge_Quantite.IdActivite = '"+mIdAct+"';";
            return query;
        }

        static internal string AddSelectedChargeQuery2(int ID,int idact)
        {
           string  mainquery = "SELECT distinct Def_Categ.Nom as groupe ,Charges.Nom, SystemeUnite.UAte as U_atelier," +
             " SystemeUnite.UEnt as U_Entreprise, SystemeUnite.UGlobal as U_Global, Charges.Prix,TVA.Nom as 'TVA'," +
             " Charge_Perenne.Quantite_Avant_1," + Commun.GetPhase(SQLQueryBuilder.FindName("Activite","Nom","ID", idact))+ ", " +
             " Charges.IdDefCateg, Charges.IdSysUnit, Charges.Idtva,Charges.ID FROM Charges " +
             " JOIN SystemeUnite on Charges.IdSysUnit = SystemeUnite.IdSysUnit " +
             " JOIN Charge_Perenne ON Charge_Perenne.IdCharges = Charges.ID " +
             " JOIN Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Charges.IdTva = TVA.IdTva " +
              " Where Charges.ID = '"+ID +"';";
            return mainquery;
        }

        static internal string AddSelectedChargeQuery3(int ID, int idact)
        {
          string mainquery = "SELECT distinct Def_Categ.Nom as groupe ,Charges.Nom, SystemeUnite.UAte as U_atelier,SystemeUnite.UEnt as U_Entreprise," +
             " SystemeUnite.UGlobal as U_Global ,Charges.Prix,TVA.Nom as 'TVA', Charge_Plurianuelle.Quantite_Avant_1 as 'Avant 1',Charge_Plurianuelle.1 as Ph1," +
             " Charge_Plurianuelle.2 as Ph2,Charge_Plurianuelle.3 as Ph3,Charge_Plurianuelle.4 as Ph4  Charges.IdDefCateg, Charges.IdSysUnit, " +
             " Charges.Idtva,Charges.ID FROM Charges " +
             " JOIN SystemeUnite on Charges.IdSysUnit = SystemeUnite.IdSysUnit " +
             " JOIN Charge_Plurianuelle ON Charge_Plurianuelle.IdCharges = Charges.ID " +
             " JOIN Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg JOIN TVA on Charges.IdTva = TVA.IdTva " +
              " Where Charges.ID = '" + ID + "';";
            return mainquery;
        }

        static internal string SelectChargeOftheActivityQuery(List<int> listId,int type,int idAct)
        {
            string WhereClause = "";
            string query;
            if (listId != null)
            {
                WhereClause = "WHERE ( Structurelle = '" + type + "'";
                foreach (int id in listId)
                {
                    // if its the first, And + open bracket
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = WhereClause + " AND ( Charges.ID = '" + id + "' ";
                    }
                    // if its the last, close the bracket
                    if (listId.IndexOf(id) == listId.Count - 1)
                    {
                        WhereClause = WhereClause + " OR Charges.ID = '" + id + "') ";
                    }
                    // otherwise add or clause
                    else WhereClause = WhereClause + " OR Charges.ID = '" + id + "' ";
                }

            }
            else WhereClause = " WHERE ( Charges.ID = '-1' AND Structurelle = '" + type + "'";
            WhereClause = WhereClause + ") AND Charge_Quantite.IdActivite = '" + idAct + "';";
            query = "Select Charges.Nom, Def_Categ.Nom ,Charges.Prix , SystemeUnite.Uate, Charge_Quantite.Quantite_1 " +
                   "FROM Charges " +
                   "JOIN Charge_Quantite ON Charge_Quantite.IdCharges = Charges.ID " +
                   "JOIN Caract_Activite On Charges.ID = Caract_Activite.IdCharges " +
                   "JOIN Def_Categ On Charges.IdDefCateg = Def_Categ.IdDefCateg " +
                   "JOIN SystemeUnite On Charges.IdSysUnit = SystemeUnite.IdSysUnit " + WhereClause;
            
            return query;
        }

        static internal string SelectChargeOftheActivityQueryPerenne(List<int> listId, int type, int idAct)
        {
            string WhereClause = "";
            string query;
            if (listId != null)
            {
                WhereClause = "WHERE ( Structurelle = '" + type + "'";
                foreach (int id in listId)
                {
                    // if its the first, And + open bracket
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = WhereClause + " AND ( Charges.ID = '" + id + "' ";
                    }
                    // if its the last, close the bracket
                    if (listId.IndexOf(id) == listId.Count - 1)
                    {
                        WhereClause = WhereClause + " OR Charges.ID = '" + id + "') ";
                    }
                    // otherwise add or clause
                    else WhereClause = WhereClause + " OR Charges.ID = '" + id + "' ";
                }

            }
            else WhereClause = " WHERE ( Charges.ID = '-1' AND Structurelle = '" + type + "'";
            WhereClause = WhereClause + ") AND Charge_Perenne.IdActivite = '" + idAct + "';";
            string ActName = SQLQueryBuilder.FindName("Activite", "Nom", "ID", idAct);
            query = "Select Charges.Nom, Def_Categ.Nom ,Charges.Prix , SystemeUnite.Uate," + Commun.GetPhase(ActName) +
              " FROM Charges " +
              " JOIN SystemeUnite on Charges.IdSysUnit = SystemeUnite.IdSysUnit " +
              " JOIN Charge_Perenne ON Charge_Perenne.IdCharges = Charges.ID " +
              " JOIN Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg  " +
               WhereClause + ";";

            return query;
        }

        static internal string SelectChargeOftheActivityQueryPluriannuelle(List<int> listId, int type, int idAct)
        {
            string WhereClause = "";
            string query;
            if (listId != null)
            {
                WhereClause = "WHERE ( Structurelle = '" + type + "'";
                foreach (int id in listId)
                {
                    // if its the first, And + open bracket
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = WhereClause + " AND ( Charges.ID = '" + id + "' ";
                    }
                    // if its the last, close the bracket
                    if (listId.IndexOf(id) == listId.Count - 1)
                    {
                        WhereClause = WhereClause + " OR Charges.ID = '" + id + "') ";
                    }
                    // otherwise add or clause
                    else WhereClause = WhereClause + " OR Charges.ID = '" + id + "' ";
                }

            }
            else WhereClause = " WHERE ( Charges.ID = '-1' AND Structurelle = '" + type + "'";
            WhereClause = WhereClause + ") AND Charge_Pluriannuelle.IdActivite = '" + idAct + "';";
            query = "Select Charges.Nom, Def_Categ.Nom ,Charges.Prix , SystemeUnite.Uate, Charge_Pluriannuelle.1 as Ph1" +
                   "Charge_Pluriannuelle.2 as Ph2,Charge_Pluriannuelle.3 as Ph3, Charge_Pluriannuelle.4 as Ph4" +
                   "FROM Charges " +
                   "JOIN Charge_Quantite ON Charge_Quantite.IdCharges = Charges.ID " +
                   "JOIN Caract_Activite On Charges.ID = Caract_Activite.IdCharges " +
                   "JOIN Def_Categ On Charges.IdDefCateg = Def_Categ.IdDefCateg " +
                   "JOIN SystemeUnite On Charges.IdSysUnit = SystemeUnite.IdSysUnit " + WhereClause;

            return query;
        }

        static internal string SelectChargeOftheActivityQuery2(List<int> listId, int type, int idAct)
        {
            string WhereClause = "";
            string query;
            if (listId != null)
            {
                WhereClause = "WHERE ( Structurelle = '" + type + "'";
                foreach (int id in listId)
                {
                    // if its the first, And + open bracket
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = WhereClause + " AND ( Charges.ID = '" + id + "' ";
                    }
                    // if its the last, close the bracket
                    if (listId.IndexOf(id) == listId.Count - 1)
                    {
                        WhereClause = WhereClause + " OR Charges.ID = '" + id + "') ";
                    }
                    // otherwise add or clause
                    else WhereClause = WhereClause + " OR Charges.ID = '" + id + "' ";
                }

            }
            else WhereClause = " WHERE ( Charges.ID = '-1' AND Structurelle = '" + type + "'";
            WhereClause = WhereClause + ") AND Charge_Quantite.IdActivite = '" + idAct + "';";
            query = "Select Charges.Nom,Charges.Prix , Charge_Quantite.Quantite_1 " +
                   "FROM Charges " +
                   "JOIN Charge_Quantite ON Charge_Quantite.IdCharges = Charges.ID " +
                   "JOIN Caract_Activite On Charges.ID = Caract_Activite.IdCharges " +
                    WhereClause;

            return query;
        }

        static internal string AddSelectedChargeQuery2(int ID)
        {
            string query = "SELECT Charges.Nom , Def_Categ.Nom as groupe , SystemeUnite.UAte as Unite " +
                  "FROM Charges JOIN SystemeUnite on Charges.IdSysUnit = SystemeUnite.IdSysUnit " +
                  "JOIN Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg " +
                  " WHERE Charges.ID = '" + ID + "' ;";
            return query;
        }

        static internal string AddSelectedChargeQuery3(int ID)
        {
            string query = "SELECT Charges.Id, Charges.Nom , Def_Categ.Nom as groupe" +
                  " FROM Charges JOIN Def_Categ on Charges.IdDefCateg = Def_Categ.IdDefCateg " +
                  " WHERE Charges.ID = '" + ID + "' ;";
            return query;
        }

        internal static string AddPerenneQuery(KeyValuePair<int, InfoUserCharges > item, int idAct)
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

            string query = "INSERT INTO Charge_Perenne (IdCharges, Quantite_Avant_1,IdActivite," + champ + ") " +
                 "VALUES ('" + item.Value.ID + "','" + item.Value.QtyAv1 + "'," + idAct +
                 "," + valuechamp + ");";
            return query;
        }

        internal static string AddPerenneQuery(List<double> listphase, double QteAv1, int idCharge, int idAct)
        {
            string champ = "";
            int index = 1;
            string valuechamp = "";
            foreach (int value in listphase)
            {
                champ = champ + "Ph" + index + ",";
                index++;
                valuechamp = valuechamp + "'" + value + "',";
            }
            champ = champ.Remove(champ.Length - 1, 1);
            valuechamp = valuechamp.Remove(valuechamp.Length - 1, 1);

            string query = "INSERT INTO Charge_Perenne (IdCharges, Quantite_Avant_1,IdActivite," + champ + ") " +
                 "VALUES ('" + idCharge + "','" + QteAv1 + "'," + idAct +
                 "," + valuechamp + ");";
            return query;
        }

        internal static string AddPluriannuelleQuery(KeyValuePair<int, InfoUserCharges> item, int idAct)
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

            string query = "INSERT INTO Charge_Pluriannuelle (IdCharges, Quantite_Avant_1,IdActivite," + champ + ") " +
                 "VALUES ('" + item.Value.ID + "','" + item.Value.QtyAv1 + "'," + idAct +
                 ",'" + valuechamp + "');";
            return query;
        }

        internal static string AddPluriannuelleQuery(List<double> listphase, double QteAv1, int idCharge, int idAct)
        {
            string champ = "";
            int index = 1;
            string valuechamp = "";
            foreach (int value in listphase)
            {
                champ = champ + "Ph" + index + ",";
                index++;
                valuechamp = valuechamp + "'" + value + "',";
            }
            champ = champ.Remove(champ.Length - 1, 1);
            valuechamp = valuechamp.Remove(valuechamp.Length - 1, 1);

            string query = "INSERT INTO Charge_Pluriannuelle (IdCharges, Quantite_Avant_1,IdActivite," + champ + ") " +
                 "VALUES ('" + idCharge + "','" + QteAv1 + "'," + idAct +
                 ",'" + valuechamp + "');";
            return query;
        }

        internal static string UpdatePerenneQuery(KeyValuePair<int, InfoUserCharges> item, int idAct)
        {
            string champ = "";
            int index = 1;
            foreach (int value in item.Value.ListPhase)
            {
                champ = champ + "Ph" + index + " = '" + value + "',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "Update Charge_Perenne Set Quantite_Avant_1 = '" + item.Value.QtyAv1 + "', " + champ + " ";
            return query;
        }

        internal static string UpdatePerenneQuery(List<double> listphase, double QteAv1, int idCharge, int idAct)
        {
            string champ = "";
            int index = 1;
            foreach (double value in listphase)
            {
                champ = champ + "Ph" + index + " = '" + value + "',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "Update Charge_Perenne Set Quantite_Avant_1 = '" + QteAv1 + "', " + champ  +
                " WHERE IdCharges = '" + idCharge + "';";
            return query;
        }



        internal static string UpdatePluriannuelleQuery(List<double> listphase, double QteAv1, int idCharge, int idAct)
        {
            string champ = "";
            int index = 1;
            foreach (double value in listphase)
            {
                champ = champ + "Ph" + index + " = '" + value + "',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "update Charge_Pluriannuelle Set Quantite_Avant_1 = '" + QteAv1 + "', " + champ 
            + " WHERE IdCharges = '" + idCharge + "';";
            return query;
        }

        internal static string UpdatePluriannuelleQuery(KeyValuePair<int, InfoUserCharges> item, int idAct)
        {
            string champ = "";
            int index = 1;
            foreach (int value in item.Value.ListPhase)
            {
                champ = champ + "Ph" + index + " = '" + value + "',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "update Charge_Pluriannuelle Set Quantite_Avant_1 = '" + item.Value.QtyAv1 + "', " + champ + ";";
            return query;
        }

        static internal string SelectCh_PiedActivityQuery(List<int> listId,int IdAct)
        {
            string query = "";
            string WhereClause = "";
            if (listId != null)
            {
                if (listId.Count == 0) WhereClause = " WHERE ( Charges.ID = '-1'";
                foreach (int id in listId)
                {
                    if (listId.IndexOf(id) == 0)
                    {
                        WhereClause = " WHERE ( Charges.ID = '" + id + "' ";
                    }
                    else WhereClause = WhereClause + " OR Charges.ID = '" + id + "' ";
                }
                WhereClause = WhereClause + ") AND item_Pied.IdActivite = '" + IdAct + "';";
                query = "Select Charges.Nom, Def_Categ.Nom as 'Groupe' , SystemeUnite.UAte as Unite, " +
                    "item_Pied.Qte_Av_1,item_Pied.Ph1,item_Pied.ID FROM Charges " +
                    "JOIN item_Pied On Charges.ID = item_Pied.IdCharges " +
                    "JOIN Def_Categ On Charges.IdDefCateg = Def_Categ.IdDefCateg " +
                    "JOIN SystemeUnite On Charges.IdSysUnit = SystemeUnite.IdSysUnit " + WhereClause;
            }
            return query;
        }

        internal static string AutoCompleteQuery()
        {
            string query = "Select distinct Def_Categ.Nom from Def_Categ " +
             "JOIN Charges on Charges.IdDefCateg = Def_Categ.IdDefCateg;";
            return query;
        }

        static internal string UpdateQuantiteQuery(KeyValuePair<int, InfoUserCharges> item, int idAct)
        {
            string query = "UPDATE Charge_Quantite Set Quantite_Avant_1 = '" + item.Value.QtyAv1 + "', Quantite_1 = '" +
                           item.Value.Qty1 + "', IdActivite = '" + idAct +
                           "' WHERE ID = '" + item.Value.ID + "';";
            return query;
        }

        static internal string UpdateQuantiteQuery(double QteAv1, double Qte1, int IdCharges, int idAct)
        {
            string query = "UPDATE Charge_Quantite Set Quantite_Avant_1 = '" + QteAv1 + "', Quantite_1 = '" +
                           Qte1 + "', IdActivite = '" + idAct +
                           "' WHERE ID = '" + IdCharges + "';";
            return query;
        }

       
        internal static string AddQuantiteQuery(KeyValuePair<int, InfoUserCharges> item, int idAct)
        {
            string query = "INSERT INTO Charge_Quantite (IdCharges , Quantite_Avant_1,Quantite_1,IdActivite) " +
                 "VALUES ('" + item.Value.ID + "','" + item.Value.QtyAv1 + "','" + item.Value.Qty1 +
                 "','" + idAct + "');";
            return query;
        }

        internal static string AddQuantiteQuery(double QteAv1,double Qte1,int IdCharges, int idAct)
        {
            string query = "INSERT INTO Charge_Quantite (IdCharges , Quantite_Avant_1,Quantite_1,IdActivite) " +
                 "VALUES ('" + IdCharges + "','" + QteAv1 + "','" + Qte1 +
                 "','" + idAct + "');";
            return query;
        }

        internal static string AddQuantiteQuery(DataGridViewRow row, int idAct)
        {
            double.TryParse(row.Cells[6].Value.ToString(), out double qteAv1);
            double.TryParse(row.Cells[7].Value.ToString(), out double qte1);
            int.TryParse(row.Cells[6].Value.ToString(), out int id);
            int.TryParse(row.Cells[0].Value.ToString(), out int idcharge);
            string query = "INSERT INTO Charge_Quantite (IdCharges , Quantite_Avant_1,Quantite_1,IdActivite) " +
                 "VALUES ('" + idcharge + "','" + qteAv1 + "','" + qte1 +
                 "','" + idAct + "');";
            return query;
        }

        internal static string AddPerenneQuery(DataGridViewRow row, int idAct,List<int> list)
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
            double.TryParse(row.Cells[6].Value.ToString(), out double qteAv1);
            int.TryParse(row.Cells[6].Value.ToString(), out int id);
            string query = "INSERT INTO Charge_Perenne (IdCharges, Quantite_Avant_1,IdActivite," + champ + ") " +
                 "VALUES ('" + id + "','" + qteAv1 + "'," + idAct +
                 "," + valuechamp + ");";
            return query;
        }

        internal static string AddPluriannuelleQuery(DataGridViewRow row, int idAct, List<int> list)
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
            double.TryParse(row.Cells[6].Value.ToString(), out double qteAv1);
            int.TryParse(row.Cells[6].Value.ToString(), out int id);
            string query = "INSERT INTO Charge_Pluriannuelle (IdCharges, Quantite_Avant_1,IdActivite," + champ + ") " +
                 "VALUES ('" + id + "','" + qteAv1 + "'," + idAct +
                 ",'" + valuechamp + "');";
            return query;
        }

        static internal string UpdateQuantiteQuery(DataGridViewRow row, int idAct)
        {
            double.TryParse(row.Cells[6].Value.ToString(), out double qteAv1);
            double.TryParse(row.Cells[7].Value.ToString(), out double qte1);
            int.TryParse(row.Cells[6].ToString(), out int id);
            string query = "UPDATE Charge_Quantite Set Quantite_Avant_1 = '" + qteAv1 + "', Quantite_1 = '" +
                           qte1 + "', IdActivite = '" + idAct +
                           "' WHERE ID = '" + id + "';";
            return query;
        }

        internal static string UpdatePerenneQuery(DataGridViewRow row, int idAct, List<int> list)
        {
            string champ = "";
            double.TryParse(row.Cells[6].Value.ToString(), out double qteAv1);
            int index = 1;
            foreach (int value in list)
            {
                champ = champ + "Ph" + index + " = '" + value + "',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "Update Charge_Perenne Set Quantite_Avant_1 = '" + qteAv1 + "', " + champ ;
            return query;
        }

        internal static string UpdatePluriannuelleQuery(DataGridViewRow row, int idAct, List<int> list)
        {
            string champ = "";
            double.TryParse(row.Cells[6].Value.ToString(), out double qteAv1);
            int index = 1;
            foreach (int value in list)
            {
                champ = champ + "Ph" + index + " = '" + value + "',";
                index++;
            }
            champ = champ.Remove(champ.Length - 1, 1);

            string query = "update Charge_Pluriannuelle Set Quantite_Avant_1 = '" + qteAv1 + "', " + champ + ") ";
            return query;
        }

    }
}
