using Microsoft.Data.Sqlite;
using System;

namespace MyBudget.Data
{
    internal class ConnectionDB
    {
        private readonly SqliteConnection connection = new("Data Source=" + @"..\\..\\..\\Data\\home_budget.db; Mode=ReadWrite");

        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqliteConnection GetConnection()
        {
            return connection;
        }
    }
}
