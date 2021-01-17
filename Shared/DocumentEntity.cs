using System.Data;
using System.Data.SQLite;

namespace Shared
{
    public class DocumentEntity
    {
        private SQLiteConnection sqlite;

        public DocumentEntity()
        {
            //This part killed me in the beginning.  I was specifying "DataSource"
            //instead of "Data Source"
            sqlite = new SQLiteConnection(@"Data Source=C:\_sqlite.db");

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
    }
}
