using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;

namespace Shared
{
    public class Connection
    {
        private SQLiteConnection sqlite;

        public Connection()
        {
            //This part killed me in the beginning.  I was specifying "DataSource"
            //instead of "Data Source"
            sqlite = new SQLiteConnection(@"Data Source=_sqlite.db");

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

        public void CreateDocument(string text)
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
        
        public void AddAllWordsToDb(string currentLine)
        {
            if (string.IsNullOrWhiteSpace(currentLine)) return;

            var words = currentLine.Split(' ').Reverse(); // Reverse is must
            var enumerable = words as string[] ?? words.ToArray();
            if (enumerable.Length == 0) return;
            
            var total = enumerable.Count();

            for (int i = 0; i < total; i++)
            {
                var currentWord = enumerable[i];
                var nextWord = (i == 0) ? string.Empty : enumerable[i - 1];
                AddWordToDbWithNearWordsIds(currentWord, nextWord);
            }

            Debug.WriteLine("");
        }


        private void AddWordToDbWithNearWordsIds(string currentWord, string nextWord)
        {
            Debug.WriteLine("currentWord: " + currentWord + ",    nextWord:" + nextWord);
            return;


            var table = SelectQuery(string.Format("select * from Document where Text = '{0}';", currentWord));

            if (table.Rows.Count > 0)
            {
                // Work-1) Increment it's_frequency field
                // Work-2) Add nextWord's id to NearWords field of currentWord		
            }
            else
            {
                CreateDocument(currentWord); // Work-1) add currentWord to db
                // Work-2) Add nextWord's id to NearWords field of currentWord
            }
        }

    }
}
