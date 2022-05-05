using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    interface IDownloadHouse
    {
        protected static void ShowHouse(ObservableCollection<House> coll)
        {
            string sqlShowHouses = "SELECT * from house";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdShowHouses = new SqliteCommand(sqlShowHouses, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdShowHouses.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                House newHouse = new House();
                newHouse.IdHouse = Convert.ToInt32(sqliteDataReader[0]);
                newHouse.Sity = sqliteDataReader[1].ToString();
                newHouse.Street = sqliteDataReader[2].ToString();
                newHouse.NumberHouse = sqliteDataReader[3].ToString();
                newHouse.NumberAppartament = Convert.ToInt32(sqliteDataReader[4]);
                coll.Add(newHouse);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
