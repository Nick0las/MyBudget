using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    internal interface IDownloadUsers
    {
        protected static void ShowUser(ObservableCollection<User> p)
        {
            string sqlSelectAllUsers = "SELECT * FROM user";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdSelectAllUsers = new SqliteCommand
                (sqlSelectAllUsers, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAllUsers.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                User user = new User
                {
                    IdUser = Convert.ToInt32(sqliteDataReader[0]),
                    Name = sqliteDataReader[1].ToString(),
                    Surname = sqliteDataReader[2].ToString(),
                    Child = Convert.ToBoolean(sqliteDataReader[3])
                };
                p.Add(user);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
