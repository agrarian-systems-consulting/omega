using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OMEGA.Data_Classes
{
    class CheckData
    {
        internal bool DataExist(string name,string table)
        {
            try
            {
                string query = "Select * From "+table+" Where Nom ='" + name + "';";
                List<int> list = SQlQueryExecuter.RunQueryReaderInt("ID", query);
                if (Commun.ListHasValue(list))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Program. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                return false;
            }
        }

        internal void AddData(string name, int type,string table)
        {
            string query = "Insert into "+table+" (Nom,IdType,Encours) VALUES ('" + name + "','" + type + "','0');";
            SQlQueryExecuter.RunQuery(query);
        }
    }
}
