using System;
using System.Collections.Generic;
using System.Data;

namespace OMEGA.Data_Classes
{
    internal class RepresentDataClass
    {

    }

    internal class AnneePhase
    {
       internal bool disponible { get; set; }
        internal int numPhase { get; set; }
    }

internal class InfoUserProduit
    {
       
        internal Boolean GroupeExiste { get; set; }
        internal Boolean AddToProductTable { get; set; }
        internal Boolean Modification { get; set; }
        internal String Groupe { get; set; }
        internal String[] OldValueInCell { get; set; }
        internal String Nom { get; set; }
        internal String U_Ent { get; set; }
        internal String U_ate { get; set; }
        internal String U_Glo { get; set; }
        internal double Qte1 { get; set; }
        internal double Qte2 { get; set; }
        internal double QteAv1 { get; set; }
        internal String TVA { get; set; }
        internal string Price { get; set; }
        internal int IdTVA { get; set; }
        internal int IdDefCateg { get; set; }
        internal int IdsystUnit { get; set; }
        internal int ID { get; set; }
        internal List<int> ListPhase { get; set; }
    }

    internal class InfoUserExternalite
    {

        internal Boolean AddToExternalityTable { get; set; }
        internal Boolean Modification { get; set; }
        internal Boolean GroupeExiste { get; set; }
        internal String _Type { get; set; }
        internal String Nom { get; set; }
        internal String U_Ent { get; set; }
        internal String U_ate { get; set; }
        internal String U_Glo { get; set; }
        internal int IdDefCateg { get; set; }
        internal int IdsystUnit { get; set; }
        internal int ID { get; set; }
        internal String[] OldValueInCell { get; set; }
        internal List<int> ListPhase { get; set; }
        internal Double Qty1 { get; set; }
        internal Double QtyAv1 { get; set; }

    }

    internal class InfoUserVariable
    {

        internal Boolean AddToVariableTable { get; set; }
        internal Boolean Modification { get; set; }
        internal String Categorie { get; set; }
        internal String Nom { get; set; }
        internal String U_Ent { get; set; }
        internal String U_ate { get; set; }
        internal String U_Glo { get; set; }
        internal int IdsystUnit { get; set; }
        internal int ID { get; set; }
        internal String[] OldValueInCell { get; set; }

    }

    /// <summary>
    /// Class that represents the data of an input( or Charge)
    /// </summary>
    internal class InfoUserCharges
    {
        internal Boolean GroupeExiste { get; set; }
        internal Boolean AddtoChargeTable { get; set; }
        internal Boolean Modification { get; set; }
        internal String Groupe { get; set; }
        internal String Nom { get; set; }
        internal String NomProduits { get; set; }
        internal String U_Ent { get; set; }
        internal String U_ateProduit { get; set; }
        internal String U_ate { get; set; }
        internal String U_Glo { get; set; }
        internal String[] OldValueInCell { get; set; }
        internal string TVA { get; set; }
        internal string Price { get; set; }
        internal Double Qty1 { get; set; }
        internal Double QtyAv1 { get; set; }
        internal int IdTVA { get; set; }
        internal int IdDefCateg { get; set; }
        internal int IdsystUnit { get; set; }
        internal int ID { get; set; }
        internal int IdProduits { get; set; }
        internal List<int> ListPhase { get; set; }
    }

    internal class InfoUserFamily
    {
        internal Boolean AddtoFamilyTable { get; set; }
        internal Boolean Modification { get; set; }
        internal String Role { get; set; }
        internal String Nom { get; set; }
        internal String Sexe { get; set; }
        internal int ID { get; set; }
        internal int Responsable { get; set; }
        internal int Age { get; set; }
        internal String[] OldValueInCell { get; set; }
    }

    internal class InfoUserTroupeaux
    {
        internal Boolean AddtoDefBestiauxTable { get; set; }
        internal Boolean Modification { get; set; }
        internal Boolean GroupeExiste { get; set; }
        internal String ValInventaire { get; set; }
        internal String Nom { get; set; }
        internal String Groupe { get; set; }
        internal int ID { get; set; }
        internal int IdDefCateg { get; set; }
        internal int Responsable { get; set; }
        internal String[] OldValueInCell { get; set; }
        internal int IdTVA { get; set; }
        internal string TVA { get; set; }
        internal string Origine { get; set; }
        internal Double percent1 { get; set; }
        internal Double Price { get; set; }
        internal string data1 { get; set; }
        internal Double percent2 { get; set; }
        internal string data2 { get; set; }
        internal Double percent3 { get; set; }
        internal string data3 { get; set; }
        internal Double percent4 { get; set; }
        internal string data4 { get; set; }

    }

    internal class GPSPoint
    {
        internal string Name { get; set; }
        internal string NameShown { get; set; }
        internal double Lat { get; set; }
        internal double Lon { get; set; }
        internal double Alt { get; set; }
        internal DateTime Date { get; set; }
    }

    internal class Tracename
    {
        internal string Name { get; set; }
        internal string Display { get; set; }
        internal bool IsRoute { get; set; }

        internal Tracename(string name, string display, bool isroute)
        {
            Name = Name;
            Display = display;
            IsRoute = isroute;
        }
    }

    internal class InfoUserClassification
    {
        internal string Nom { get; set; }
        internal bool Modification { get; set; }
        internal int Id { get; set; }
        internal string Groupe { get; set; }
        internal bool GroupeExiste { get; set; }

    }

    internal class InfoUserExpInc
    {
        internal string Nom { get; set; }
        internal string TVA { get; set; }
        internal int Expense { get; set; }
        internal int Family { get; set; }
        internal int ID { get; set; }
        internal int IdTVa { get; set; }
        internal bool Modification { get; set; }
        internal bool AddToExpenseIncomeTable { get; set; }
        internal bool DeleteFromExpenseIncomeTable { get; set; }

    }

    public class InfoShapeFile
    {
        internal string FileName { get; set; }
        internal string FilePath { get; set; }
        internal int NoLayer { get; set; }
        internal int Id { get; set; }
        internal int IdActExploi { get; set; }
        internal bool visible { get; set; }
        internal bool MainMap { get; set; }
        internal string type { get; set; }
        internal string CoordonateType { get; set; }
        internal int ColorARGB { get; set; }
        internal int Position { get; set; }
        internal List<string> ListPoints { get; set; }

        internal int SetPositionInit()
        {
            Position = -1;
            return Position;
        }
    }

    internal class CalculElementExp_Inc
    {
        internal DataTable table { get; set; }
        internal int nbRow { get; set; }
        internal int Position { get; set; }
    }    
}



