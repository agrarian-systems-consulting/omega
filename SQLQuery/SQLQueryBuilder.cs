using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.SQLQuery
{
    static internal class SQLQueryBuilder
    {
        /// <summary>
        /// Return the ID from the element we add to the current object (eg : Activity, exploitation or region)
        /// tableElement maybe : Charges, Products, Externality...
        /// TableOject : Activity, Exploitation or Region and the Id
        /// </summary>
        /// <param name="tableObject"></param>
        /// <param name="tableElement"></param>
        /// <param name="IdObject"></param>
        /// <returns></returns>
        static internal List<int> GetListID(string tableObject, string tableElement, int IdObject)
        {
            List<int> listId = new List<int>();
            try
            {
                if (tableObject == "")
                {
                    string query = "SELECT ID From " + tableElement + " ;";
                    listId = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                }
                else
                {
                    string query = "SELECT distinct " + tableElement + ".ID From  " + tableObject +
                        " JOIN Caract_" + tableObject + " on Caract_" + tableObject + ".Id" + tableObject + " = " + tableObject + ".ID " +
                        "JOIN " + tableElement + " on " + tableElement + ".ID = Caract_" + tableObject + ".Id" + tableElement + " " +
                        "WHERE " + tableObject + ".ID = '" + IdObject + "'; ";
                    listId = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return listId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="idfield"></param>
        /// <param name="fieldfilter"></param>
        /// <param name="valuefilter"></param>
        /// <returns></returns>
        static internal int FindId(string table, string idfield, string fieldfilter, string valuefilter)
        {
            string SQLQuery = "";
            try
            {
                SQLQuery = "SELECT " + idfield + " FROM " + table + " WHERE " + fieldfilter + " = '" + valuefilter + "';";
                List<int> listid = SQlQueryExecuter.RunQueryReaderInt(idfield, SQLQuery);
                if (Commun.ListHasValue(listid))
                {
                    return listid[0];
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="idfield"></param>
        /// <param name="fieldfilter"></param>
        /// <param name="valuefilter"></param>
        /// <returns></returns>
        static internal int FindId(string table, string idfield, string WHEREClause)
        {
            string SQLQuery = "";
            try
            {
                SQLQuery = "SELECT " + idfield + " FROM " + table + " WHERE " + WHEREClause;
                List<int> listid = SQlQueryExecuter.RunQueryReaderInt(idfield, SQLQuery);
                if (Commun.ListHasValue(listid))
                {
                    return listid[0];
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="idfield"></param>
        /// <param name="fieldfilter"></param>
        /// <param name="valuefilter"></param>
        /// <returns></returns>
        static internal int FindId(string table, string idfield, string fieldfilter, int valuefilter)
        {
            string SQLQuery = "";
            try
            {
                SQLQuery = "SELECT " + idfield + " FROM " + table + " WHERE " + fieldfilter + " = '" + valuefilter + "';";
                List<int> listid = SQlQueryExecuter.RunQueryReaderInt(idfield, SQLQuery);
                if (Commun.ListHasValue(listid))
                {
                    return listid[0];
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="idfield"></param>
        /// <param name="fieldfilter"></param>
        /// <param name="valuefilter"></param>
        /// <returns></returns>
        static internal string FindName(string table, string Namefield, string IDfield, int ID)
        {
            string SQLQuery = "";
            try
            {
                SQLQuery = "SELECT " + Namefield + " FROM " + table + " WHERE " + IDfield + " = '" + ID + "';";
                List<string> listid = SQlQueryExecuter.RunQueryReaderStr(Namefield, SQLQuery);
                if (Commun.ListHasValue(listid))
                {
                    return listid[0];
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return "";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="idfield"></param>
        /// <param name="fieldfilter"></param>
        /// <param name="valuefilter"></param>
        /// <returns></returns>
        static internal string FindName(string table, string Namefield, string IDfield, string ID)
        {
            string SQLQuery = "";
            try
            {
                SQLQuery = "SELECT " + Namefield + " FROM " + table + " WHERE " + IDfield + " = '" + ID + "';";
                List<string> listid = SQlQueryExecuter.RunQueryReaderStr(Namefield, SQLQuery);
                if (Commun.ListHasValue(listid))
                {
                    return listid[0];
                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return "";
        }

        /// <summary>
        /// Delete the list of record
        /// </summary>
        /// <param name="ListRecordToDelete"></param>
        /// <param name="table"></param>
        static internal string DeleteQuery(int index, string table)
        {
            string query = "";
            try
            {
                query = "DELETE FROM " + table + " WHERE ID = '" + index.ToString() + "';";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return query;
        }

        /// <summary>
        /// Delete the list of record
        /// </summary>
        /// <param name="ListRecordToDelete"></param>
        /// <param name="table"></param>
        static internal string DeleteQuery(string table, string Whereclause)
        {
            string query = "";
            try
            {
                query = "DELETE FROM " + table + " " + Whereclause;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return query;
        }

        static internal string DeleteQuery(int index, string table, string IdField)
        {
            string query = "";
            try
            {
                query = "DELETE FROM " + table + " WHERE " + IdField + " = '" + index.ToString() + "';";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
            return query;
        }

        static internal string AddNewgGroupeQuery(string groupname, int IdDefinition)
        {
            string query = " INSERT INTO Def_Categ (idDefinitions,Nom) VALUES ('"+ IdDefinition + "','" + groupname + "');";
            return query;
        }

        static internal string SelectGroupeQuery(string groupname)
        {
            string query = "SELECT IdDefCateg from Def_Categ WHERE Nom LIKE '" + groupname + "';";
            return query;
        }

        static internal string SelectQuery(string table, string field)
        {
            string query = "Select " + field + " from " + table + ";";
            return query;
        }

        static internal string SelectQuery(string table, string field, string whereclause)
        {
            string query = "Select " + field + " from " + table + " " + whereclause + ";";
            return query;
        }

        static internal string GetIdSystemeUniteQuery(string UAE, string UEnt, string UGlo)
        {
            string query = "SELECT idSysUnit From SystemeUnite WHERE UAte LIKE '" + UAE + "' AND UEnt LIKE '" + UEnt + "' AND UGlobal  LIKE '" + UGlo + "' ;";
            return query;
        }

        static internal string GetIdSystemeUniteQuery(string UAE)
        {
            string query = "SELECT idSysUnit From SystemeUnite WHERE UAte LIKE '" + UAE + "' ;";
            return query;
        }

        static internal string FindGroupeQuery(string table, string groupname)
        {
            string query = "SELECT * From " + table + " WHERE Nom = '" + groupname + "';";
            return query;
        }

        static internal string SeqIdQuery(string table)
        {
            string query = "SELECT Seq FROM SQlite_sequence Where name = '" + table + "';";
            return query;
        }

        static internal string FirstIdQuery(string table)
        {
            string query = "SELECT ID FROM " + table + ";";
            return query;
        }

        static internal string MaxIdQuery(string table)
        {
            string query = "SELECT MAX(ID) FROM " + table + ";";
            return query;
        }

        static internal string NbRowQuery(string table)
        {
            string query = "SELECT COUNT(*) FROM " + table;
            return query;
        }

        static internal string DropQuery(string table)
        {
            string query = "DROP TABLE " + table;
            return query;
        }


        internal static string AutoCompleteQueryTVA()
        {
            string query = "Select distinct TVA.Nom from TVA;";
            return query;
        }

        internal static string AutoCompleteQueryUnity(string field)
        {
            string query = "Select distinct " + field + " from SystemeUnite" +
                " WHERE Monnaie = '0';";
            return query;
        }

        internal static string UpdateColorQuery(Color color1, Color color2, Color color3)
        {
            string query = "Update Couleur Set couleur1 = '" + color1.Name +
                   "', couleur2 = '" + color2.Name +
                   "', couleur3 = '" + color3.Name +
                   "', ARVB1 = '" + color1.ToArgb() +
                   "', ARVB2 = '" + color2.ToArgb() +
                   "', ARVB3 = '" + color3.ToArgb() + "';";
            return query;
        }

        internal static string UpdateColorQuery(Color color1, Color color2)
        {
            string query = "Update Couleur Set couleur1 = '" + color1.Name +
                   "', couleur2 = '" + color2.Name +
                   "', ARVB1 = '" + color1.ToArgb() +
                   "', ARVB2 = '" + color2.ToArgb() +  "';";
            return query;
        }

        internal static string SaveExploitationDataQuery(string NameIdTable, int idExp, int IdTable)
        {
            string query = "INSERT INTO Caract_Exploitation (IdExploitation," + NameIdTable + ")" +
                          "VALUES ( '" + idExp + "','" + IdTable + "');";
            return query;

        }

        internal static string GetNomFromId(string table, int id, string fieldName, string idField)
        {
            try
            {
                string query = "Select " + fieldName + " From " + table + " Where " + idField + " = '" + id + "';";
                List<string> list = SQlQueryExecuter.RunQueryReaderStr(fieldName, query);
                if (Commun.ListHasValue(list))
                {
                    return list[0];
                }
                else return "";
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return "";
            }
        }

        internal static string SetEncoursTo0(string table, int id)
        {
            string query = "Update " + table + " Set Encours = '0' WHERE ID <> '" + id + "';";
            return query;
        }

        internal static string  AddItemToAleaQuery (string table, int id)
        {
            string query = "Select '0'," + table + ".ID," + table + ".Nom , Def_Categ.Nom" +
                " From  " + table +
                " Join Def_Categ on " + table + ".IdDefCateg = Def_Categ.IdDefCateg " +
                " WHERE " + table + ".ID = '"+id+"'";
            return query;
        }


        internal static string FindValueResultCalculQuery(int year, string nom, string tableOrigine, int IdExp)
        {
            string query = "Select Valeur From Result_Calcul " +
                       "Where Table_Origine = '"+tableOrigine+"'" +
                       " AND Annee = '" + year +
                       "' AND Nom = '" + nom +
                       "' AND IdExploitations = '" + IdExp + "';";
            return query;
        }
        internal static string FindValueResultCalculQuery(string year, string nom, string tableOrigine, int IdExp)
        {
            string query = "Select Valeur From Result_Calcul " +
                       "Where Table_Origine = '" + tableOrigine + "'" +
                       " AND Annee = '" + year +
                       "' AND Nom = '" + nom +
                       "' AND IdExploitations = '" + IdExp + "';";
            return query;
        }
    }
}
