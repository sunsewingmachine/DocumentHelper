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
            sqlite = new SQLiteConnection(@"Data Source=d:\DocumentTypingHelper_sqlite.db");
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
            SQLiteCommand insertSQL = new SQLiteCommand(string.Format("INSERT INTO Document (Text,Frequency,NearWords) VALUES ('{0}',1,'{1}');", text, nextWordId), sqlite);
            //insertSQL.Parameters.Add("Text",);

            try
            {
                int result = insertSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateScannedEntry(string url, string uName, int frequency = 1)
        {
            //sqlite.Open();
            SQLiteCommand insertSQL = new SQLiteCommand(
                string.Format("INSERT INTO ScannedUrl (Url,UName,Frequency) VALUES ('{0}','{1}','{2}');", 
                    url, uName, frequency), sqlite);

            //insertSQL.Parameters.Add("Text",);

            try
            {
                int result = insertSQL.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in CreateScannedEntry" + ex.Message); 
            }
        }

        public bool HasScannedEntry(string url, string uName)
        {
            var table = SelectQuery(string.Format("select * from ScannedUrl where Url = '{0}' or UName = '{1}';", url, uName));

            var res = table.Rows.Count > 0;
            Debug.WriteLine("From, HasScannedEntry, Url: " + url + ",    Has: " + res);
            return res;
        }

        public void AddAllWordsToDb(string currentLine, bool enableNearWordsEntry)
        {
            if (string.IsNullOrWhiteSpace(currentLine)) return;


            var words = currentLine.Split(' ').Reverse(); // Reverse is must
            words = RemoveWordsHavingNumbers(words.ToList());

            var enumerable = words as string[] ?? words.ToArray();
            if (enumerable.Length == 0) return;

            Debug.WriteLine("CurrentLine: " + currentLine);
            var total = enumerable.Count();

            for (int i = 0; i < total; i++)
            {
                var currentWord = enumerable[i];
                var nextWord = (i == 0) ? string.Empty : enumerable[i - 1];
                if (!enableNearWordsEntry) nextWord = string.Empty;
                AddWordToDbWithNearWordsIds(currentWord, nextWord);
            }

            Debug.WriteLine("");
        }

        public List<string> RemoveWordsHavingNumbers(List<string> list)
        {
            list.RemoveAll(x => x.Any(char.IsDigit));
            list.RemoveAll(x => x.Length < 3);
            return list;
        }


        private void AddWordToDbWithNearWordsIds(string currentWord, string nextWord)
        {
            // Debug.WriteLine("currentWord: " + currentWord + ",    nextWord:" + nextWord);

            var table = SelectQuery(string.Format("select * from Document where Text = '{0}';", currentWord));
            
            var nextWordId = GetNextWordId(nextWord);

            if (table.Rows.Count > 0)
            {
                UpdateDocumentFrequency(table.Rows[0].ItemArray[0].ToString(), nextWordId);	
            }
            else
            {
                CreateDocument(currentWord, nextWordId);
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


        public bool CreateAlreadyAddedLink(string linkText)
        {
            //sqlite.Open();
            SQLiteCommand insertSQL = new SQLiteCommand(string.Format("INSERT INTO Links (link) VALUES ('{0}');", linkText), sqlite);
            //insertSQL.Parameters.Add("Text",);

            try
            {
                int result = insertSQL.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                // throw new Exception(ex.Message);
                Debug.WriteLine("Already contains this link: " + linkText);
                return false;
            }
        }


        public bool DeleteAllData(string tableName)
        {
            SQLiteCommand insertSQL = new SQLiteCommand("Delete from " + tableName + ";", sqlite);

            try
            {
                int result = insertSQL.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error in DeleteAllData: " + ex.Message);
                return false;
            }
        }


        private bool ContainsLink(string linkText)
        {
            var table = SelectQuery(string.Format("select * from Links where link = '{0}';", linkText));
            return table.Rows.Count > 0;
        }


    }
}
