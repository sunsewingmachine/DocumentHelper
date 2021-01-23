using System;
using System.Data;
using System.Data.SQLite;

namespace Shared
{
    public class Connection
    {
        private SQLiteConnection sqlite;

        public Connection()
        {
            //This part killed me in the beginning.  I was specifying "DataSource"
            //instead of "Data Source"
            sqlite = new SQLiteConnection(@"Data Source=D:\_sqlite.db");

        }

        public DataTable SelectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
            sqlite.Close();
            return dt;
        }

        public void CreateDocumnet(string text)
        {
            sqlite.Open();
            SQLiteCommand insertSQL = new SQLiteCommand(string.Format("INSERT INTO Document (Text) VALUES ('{0}');",text), sqlite);
            //insertSQL.Parameters.Add("Text",);
            try
            {
                insertSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlite.Close();
            }
        }
    }
}
