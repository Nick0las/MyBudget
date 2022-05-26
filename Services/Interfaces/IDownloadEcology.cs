using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    interface IDownloadEcology
    {
        protected static void ShowAllEcology(ObservableCollection<Ecology> ecologies)
        {

            string sqlQuery = "SELECT * FROM ecology";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdSelectAllEcology = new(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAllEcology.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                Ecology ecology = new();
                ecology.IdEcology = Convert.ToInt32(sqliteDataReader[0]);
                ecology.DateEcology = sqliteDataReader[1].ToString();
                ecology.IdHouse = Convert.ToInt32(sqliteDataReader[2]);
                ecology.PriceEcology = Convert.ToDecimal(sqliteDataReader[3]);
                ecology.PayedEcology = Convert.ToDecimal(sqliteDataReader[4]);
                ecology.DebtEcology = Convert.ToDecimal(sqliteDataReader[5]);
                ecologies.Add(ecology);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
