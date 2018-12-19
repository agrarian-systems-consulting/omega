using OMEGA.Data_Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.SQLQuery.SpecificQueryBuilder
{
   static class TerritoryQuery
   {

        internal static string UpdateQuery (InfoShapeFile shapefile,int id)
        {
            string query = "Update SHP_Info Set path = '" + shapefile.FilePath + "' , Nom = '" +
                          shapefile.FileName + "' , Color_ARGB = '" + shapefile.ColorARGB + "' , Position ='" + shapefile.Position +
                          "' Where ID = '" + id + "';";
            return query;
        }

        internal static string InsertQuery(InfoShapeFile shapefile, string code)
        {
            string  query = "Insert into SHP_Info (path,Nom,MainMap,type,IdActivite,IdExploitation,Color_ARGB,Position,Code_point) " +
                            "VALUES ('" + shapefile.FilePath + "','" + shapefile.FileName + "','" + shapefile.MainMap +
                            "','" + shapefile.type + "'," + GetShapeFileTypeId(shapefile) +
                            ",'" + shapefile.ColorARGB + "','" + shapefile.Position + "','" + code + "');";
            return query;
        }

        private static string GetShapeFileTypeId(InfoShapeFile myShapefile)
        {
            string fieldValue = "'0','0'";
            try
            {
                if (myShapefile.MainMap)
                {
                    return fieldValue;
                }
                else
                {
                    if (myShapefile.type.Contains("Farm"))
                    {
                        fieldValue = "'0','" + myShapefile.IdActExploi + "'";
                    }
                    else fieldValue = "'" + myShapefile.IdActExploi + "','0'";
                    return fieldValue;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                return fieldValue;
            }
        }

        internal static string UpdateFicheAttQuery(string time, int IdEnqueteur, string maison, 
            string occup, int idVillage, string remarque, int IdShapefile)
        {
            string query = "UPDATE OCCUP set DATE ='" + time + "',ID_ENQUETEUR = '" + IdEnqueteur
                         + "',NB_MAISON = '" + maison + "',NB_OCCUPANT='" + occup
                         + "',ID_VILLAGE = '" + idVillage + "', REMARQUE = '" + remarque + "' WHERE ID_SHP = '"
                         + IdShapefile + "'; ";
            return query;
        }

        internal static string InsertFicheAttQuery(string time, int IdEnqueteur, string maison,
            string occup, int idVillage, string remarque, int IdShapefile)
        {
            string query = "Insert into OCCUP (DATE,ID_ENQUETEUR,NB_MAISON,NB_OCCUPANT,ID_VILLAGE,ID_SHP,REMARQUE)" +
                       " VALUES ('" + time + "','" + IdEnqueteur + "','" + maison + "','" + occup + 
                       "','" + idVillage + "','" + IdShapefile + "','" +
                       remarque + "');";
            return query;
        }



    }
}
