using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    interface IDownloadProviders
    {
        protected static void ShowAllProviders(ObservableCollection<Provider> prov)
        {
            string sqlShowProvider = "SELECT * FROM provider";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdSelectProvider = new(sqlShowProvider, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectProvider.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                Provider provider = new();
                provider.IdProvider = Convert.ToInt32(sqliteDataReader[0]);
                provider.NameProvider = sqliteDataReader[1].ToString();
                prov.Add(provider);
            }
            
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
