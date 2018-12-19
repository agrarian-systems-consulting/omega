using OMEGA.Data_Classes;
using OMEGA.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
    class PeriodeQuery
    {
       private static int langue = Properties.Settings.Default.Langue;

        static internal string MainQuery(int IdAct)
        {
            string mainquery = "";
            if (IdAct == 0)
            {
                mainquery = "SELECT distinct Def_Calendrier.IdPeriode,Def_Calendrier.Nom FROM Def_Calendrier; ";
            }
            else
            {
            mainquery = "SELECT distinct Def_Calendrier.IdPeriode,Def_Calendrier.Nom FROM Def_Calendrier " +
                    "JOIN Caract_Activite on Def_Calendrier.IdPeriode = Caract_Activite.IdPeriodes " +
                    "JOIN Activite on Caract_Activite.IdActivite = Activite.ID " +
                    "WHERE Activite.ID = '" + IdAct + "'; ";
            }
            
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

        static internal string AddSelectedPeriodeQuery(int ID)
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

        static internal string SelectPeriodeOftheActivityQuery()
        {
            return "Select Def_Calendrier.Nom From Def_calendrier";
        }

        internal static string LoadPeriodeQuery()
        {
            string query = "SELECT distinct Def_Calendrier.IdPeriode FROM Def_Calendrier ;";
            return query;
        }

        internal static string MainQueryDetail(int id)
        {
           string query = "SELECT distinct Per_Act as Periode, JDeb as "+Translation.Translate("Beginning", langue)+ ", Jfin as  " + Translation.Translate("End", langue) + ", Hpj as 'h/p',PcentDispo as '%Dispo'  " +
          "FROM Def_Calendrier " +
          "WHERE Def_Calendrier.IdPeriode = '" + id + "'; ";
            return query;
        }

        internal static string MainQueryDetailForActivity(int idPeriode, int IdAct)
        {

            string query = "SELECT Per_Act as Periode  From Def_Calendrier" +
                " WHERE IdPeriode = '" + idPeriode + "';";
            return query;
        }

        internal static string InsertDefCalQuery(DataGridViewRow row, string nom, int currentId)
        {
            string query = "INSERT INTO Def_Calendrier (Nom,Per_Act,IdPeriode,JDeb,Jfin,Hpj,PcentDispo,Heures_Utilisees)" +
                            " VALUES ('" + nom + "','" + row.Cells[0].Value.ToString() + "','" + currentId + "','" +
                             row.Cells[1].Value.ToString() + "','" + row.Cells[2].Value.ToString() +
                             "','" + row.Cells[3].Value.ToString() + "','" + row.Cells[4].Value.ToString() +
                             "','" + row.Cells[7].Value.ToString() + "')";
            return query;
        }

        internal static string InsertTravailQuery(string nom,int idperiode, int idAct,string insertFiled,string insertValue)
        {
            string query = "INSERT INTO Travail (Nom_Periode,IdPeriode,IdActivite," + insertFiled + ")" +
                                        " VALUES ('" + nom + "','" + idperiode + "','" + idAct + "'"
                                         + insertValue + ");";
            return query;
        }

        internal static string SelectTravailQuery(int IdAct,int IdPeridoe,string nom)
        {
           string query = "Select * from Travail Where IdActivite = '" + IdAct +
                          "' AND IdPeriode = '" + IdPeridoe + "' AND Nom_Periode = '"+ nom + "';";
            return query;
        }

    }
}
