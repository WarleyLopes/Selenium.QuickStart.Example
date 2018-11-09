using System;
using System.Data.SQLite;

namespace Base2.Mantis.Test.Automation.Challenge.Data
{
    public static class SqliteFactory
    {
        public static void PrepareDatabaseIfNecessary()
        {
            if (!System.IO.File.Exists("MyDatabase.sqlite"))
            {
                SQLiteConnection m_dbConnection;
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
                string tableCreation = "CREATE TABLE bugs (id UNIQUEIDENTIFIER,name VARCHAR(20), steps VARCHAR(200))";
                m_dbConnection = new SQLiteConnection("Data Source=MyDatabase.sqlite;Version=3;");
                m_dbConnection.Open();
                new SQLiteCommand(tableCreation, m_dbConnection).ExecuteNonQuery();
                string dataInsertion = "INSERT INTO bugs VALUES (\"" + Guid.NewGuid() + "\",\"Mantis - BUG " + new Random().Next(1, 1000) + "\",\"1 - Abrir o site do mantis \n 2-Testar igual louco\")," +
                                                               "(\"" + Guid.NewGuid() + "\",\"Mantis - BUG " + new Random().Next(1, 1000) + "\",\"1 - Abrir o site do mantis \n 2-Testar igual louco\")";
                new SQLiteCommand(dataInsertion, m_dbConnection).ExecuteNonQuery();
                m_dbConnection.Close();
            }
        }
    }
}
