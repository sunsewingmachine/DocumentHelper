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
            sqlite = new SQLiteConnection(@"Data Source=d:\_sqlite.db");
        }

        public void OpenConnection()
        {
            sqlite.Open();
        }

        public void CloseConnection()
        {
            sqlite.Close();
        }

        public DataTable SelectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                //sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }

            //sqlite.Close();
            return dt;
        }

        public int UpdateDocumentFrequency(string id, string nextWordId)
        {
            string space = " ";
            var oldNearWordIDs = GetOldNearWordIds(id);
            var kk = space + oldNearWordIDs + space;

            string nearWordIds;
            if (kk.Contains(space + nextWordId + space)) // since ('3 863 52').contains(6) would return true!
            {
                nearWordIds = oldNearWordIDs;
            }
            else
            {
                nearWordIds = oldNearWordIDs + space + nextWordId;
            }

            using (SQLiteCommand command = new SQLiteCommand(sqlite))
            {
                // command.CommandText = "update Document set Frequency = Frequency + 1 where ID=:id";
                command.CommandText = string.Format("update Document set Frequency = Frequency + 1, NearWords = '{0}' where ID=:id", nearWordIds);
                command.Parameters.Add("id", DbType.String).Value = id;
                return command.ExecuteNonQuery();
            }
        }

        public string GetOldNearWordIds(string id)
        {
            var table = SelectQuery(string.Format("select * from Document where ID = '{0}';", id));
            if (table.Rows.Count == 0) return "";

            // result = $"{table.Rows[0].ItemArray[0]?.ToString()} {table.Rows[0].ItemArray[3]?.ToString()}";
            return table.Rows[0].ItemArray[3]?.ToString();
        }


        public void CreateDocument(string text, string nextWordId)
        {
            //sqlite.Open();
            SQLiteCommand insertSQL = new SQLiteCommand(string.Format("INSERT INTO Document (ID,Text,Frequency,NearWords) VALUES ((last_insert_rowid()+1),'{0}',1,'{1}');", text, nextWordId), sqlite);
            //insertSQL.Parameters.Add("Text",);

            try
            {
                int result = insertSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //sqlite.Close();
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
            var table = SelectQuery(string.Format("select * from Document where Text = '{0}';", currentWord));

            if (table.Rows.Count > 0)
            {
                UpdateDocumentFrequency(table.Rows[0].ItemArray[0].ToString(), GetNextWordId(nextWord));	
            }
            else
            {
                CreateDocument(currentWord, GetNextWordId(nextWord));
            }
        }

        private string GetNextWordId(string nextWord)
        {
            var result = string.Empty;
            if (string.IsNullOrWhiteSpace(nextWord)) return result;

            var table = SelectQuery(string.Format("select * from Document where Text = '{0}';", nextWord));

            if (table.Rows.Count > 0)
            {
                // result = $"{table.Rows[0].ItemArray[0]?.ToString()} {table.Rows[0].ItemArray[3]?.ToString()}";
                result = table.Rows[0].ItemArray[0]?.ToString();
            }
            return result;
        }

    }
}
