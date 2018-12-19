using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;

namespace OMEGA
{
    static class SQlQueryExecuter 
    {

        private static SQLiteConnection MSQLConnection = new SQLiteConnection();

        internal static int RunQuery(string query)
        {
            int RowAffected=0;
            try
            {
                MSQLConnection = new SQLiteConnection(Properties.Settings.Default.ConnectionString);
                MSQLConnection.Open();
                SQLiteCommand UpdateCommande = new SQLiteCommand();
                UpdateCommande = MSQLConnection.CreateCommand();
                UpdateCommande.CommandText = query;
                RowAffected = UpdateCommande.ExecuteNonQuery();
                MSQLConnection.Close();
            }
            catch ( Exception Ex)
            {
                MSQLConnection.Close();
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class Program. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return RowAffected;
        }

        internal static String[] RunQueryReader(string query)
        {
            SQLiteDataReader reader;
            String [] QueryReponse;
            try
            {
                MSQLConnection = new SQLiteConnection(Properties.Settings.Default.ConnectionString);
                MSQLConnection.Open();
                SQLiteCommand UpdateCommande = new SQLiteCommand();
                UpdateCommande = MSQLConnection.CreateCommand();
                UpdateCommande.CommandText = query;
                reader = UpdateCommande.ExecuteReader();
                QueryReponse = new string [reader.FieldCount];
                while (reader.Read())
                {
                    for (int i = 0; i < QueryReponse.Length ;i++)
                    {
                        QueryReponse[i] = reader.GetValue(i).ToString();
                    } 
                }
                MSQLConnection.Close();
                return QueryReponse;
            }
            catch (Exception Ex)
            {
                MSQLConnection.Close();
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class SQLQueryExecuter. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return null;
        }

        internal static SQLiteDataReader RunQueryDataReader(string query)
        {
            SQLiteDataReader reader;
            try
            {
                MSQLConnection = new SQLiteConnection(Properties.Settings.Default.ConnectionString);
                MSQLConnection.Open();
                SQLiteCommand UpdateCommande = new SQLiteCommand();
                UpdateCommande = MSQLConnection.CreateCommand();
                UpdateCommande.CommandText = query;
                reader = UpdateCommande.ExecuteReader();
                //MSQLConnection.Close();
                return reader;
            }
            catch (Exception Ex)
            {
                MSQLConnection.Close();
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class SQLQueryExecuter. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return null;
        }

        internal static List<string> RunQueryReaderStr(string filter, string query)
        {
            List<string> MyList = new List<string>();
            try
            {
                MSQLConnection = new SQLiteConnection(Properties.Settings.Default.ConnectionString);
                {
                    MSQLConnection.Open();
                    using (SQLiteCommand fmd = MSQLConnection.CreateCommand())
                    {
                        fmd.CommandText = query;
                        fmd.CommandType = CommandType.Text;
                        SQLiteDataReader r = fmd.ExecuteReader();
                        while (r.Read())
                        {
                            MyList.Add(Convert.ToString(r[filter]));
                        }
                    }
                }
                MSQLConnection.Close();
            }
            catch (Exception Ex)
            {
                MSQLConnection.Close();
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class SQLQueryExecuter.  Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return MyList;
        }

        internal static List<string> RunQueryReaderStrSilent(string filter, string query)
        {
            List<string> MyList = new List<string>();
            try
            {
                MSQLConnection = new SQLiteConnection(Properties.Settings.Default.ConnectionString);
                {
                    MSQLConnection.Open();
                    using (SQLiteCommand fmd = MSQLConnection.CreateCommand())
                    {
                        fmd.CommandText = query;
                        fmd.CommandType = CommandType.Text;
                        SQLiteDataReader r = fmd.ExecuteReader();
                        while (r.Read())
                        {
                            MyList.Add(Convert.ToString(r[filter]));
                        }
                    }
                }
                MSQLConnection.Close();
            }
            catch (Exception Ex)
            {
                MSQLConnection.Close();
               // MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class SQLQueryExecuter.  Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return MyList;
        }

        internal static List<Int32> RunQueryReaderInt(string filter, string query)
        {
            List<Int32> MyList = new List<Int32>();
            try
            {
                //MessageBox.Show(Properties.Settings.Default.ConnectionString);
                MSQLConnection = new SQLiteConnection(Properties.Settings.Default.ConnectionString);
                {
                    MSQLConnection.Open();
                    using (SQLiteCommand fmd = MSQLConnection.CreateCommand())
                    {
                        fmd.CommandText = query;
                        fmd.CommandType = CommandType.Text;
                        SQLiteDataReader r = fmd.ExecuteReader();

                        //MessageBox.Show("query executed");

                        while (r.Read())
                        {
                            if (!(r[filter].GetType().ToString().Contains("DBNull")))
                            {
                                try
                                {
                                    if (r.HasRows) MyList.Add(Convert.ToInt32(r[filter]));
                                }
                                catch (Exception Ex)
                                {
                                    Log.WriteLog(Ex.Message, "Class SQLQueryExecuter. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                                }

                            }
                        }
                        MSQLConnection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                MSQLConnection.Close();
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class SQLQueryExecuter. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return MyList;
        }

        internal static List<Int32> RunQueryReaderIntSilent(string filter, string query)
        {
            List<Int32> MyList = new List<Int32>();
            try
            {
                //MessageBox.Show(Properties.Settings.Default.ConnectionString);
                MSQLConnection = new SQLiteConnection(Properties.Settings.Default.ConnectionString);
                {
                    MSQLConnection.Open();
                    using (SQLiteCommand fmd = MSQLConnection.CreateCommand())
                    {
                        fmd.CommandText = query;
                        fmd.CommandType = CommandType.Text;
                        SQLiteDataReader r = fmd.ExecuteReader();

                        //MessageBox.Show("query executed");

                        while (r.Read())
                        {
                            if (!(r[filter].GetType().ToString().Contains("DBNull")))
                            {
                                try
                                {
                                    if (r.HasRows) MyList.Add(Convert.ToInt32(r[filter]));
                                }
                                catch (Exception Ex)
                                {
                                    Log.WriteLog(Ex.Message, "Class SQLQueryExecuter. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
                                }

                            }
                        }
                        MSQLConnection.Close();
                    }
                }
            }
            catch (Exception Ex)
            {
                MSQLConnection.Close();
                //MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class SQLQueryExecuter. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return MyList;
        }

        internal static List<double> RunQueryReaderDouble(string filter, string query)
        {
            List<double> MyList = new List<double>();
            try
            {
                MSQLConnection = new SQLiteConnection(Properties.Settings.Default.ConnectionString);
                {
                    MSQLConnection.Open();
                    using (SQLiteCommand fmd = MSQLConnection.CreateCommand())
                    {
                        fmd.CommandText = query;
                        fmd.CommandType = CommandType.Text;
                        SQLiteDataReader r = fmd.ExecuteReader();
                        while (r.Read())
                        {
                            if (!(r[filter].GetType().ToString().Contains("DBNull")))
                            {
                                if (r.HasRows) MyList.Add(Convert.ToDouble(r[filter]));
                            } 
                        }
                    }
                }
                MSQLConnection.Close();
            }
            catch (Exception Ex)
            {
                MSQLConnection.Close();
                MessageBox.Show(Ex.Message);
                Log.WriteLog(Ex.Message, "Class SQLQueryExecuter. Function :  " + System.Reflection.MethodBase.GetCurrentMethod().Name.ToString());
            }
            return MyList;
        }

    }
}
