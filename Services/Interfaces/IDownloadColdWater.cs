using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    interface IDownloadColdWater
    {
        protected static void ShowAllColdWater(ObservableCollection<ColdWater> coldWaters)
        {

            string sqlQuery = "SELECT * FROM cold_water";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdSelectAllColdWater = new(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAllColdWater.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                ColdWater coldWater = new();
                coldWater.IdColdWater = Convert.ToInt32(sqliteDataReader[0]);
                coldWater.IdHouse = Convert.ToInt32(sqliteDataReader[1]);
                coldWater.DateColdWater = sqliteDataReader[2].ToString();                
                coldWater.LastMetterColdWater = Convert.ToInt32(sqliteDataReader[3]);
                coldWater.KubColdWater = Convert.ToInt32(sqliteDataReader[4]);
                coldWater.PriceColdWater = Convert.ToDecimal(sqliteDataReader[5]);
                coldWater.PayedColdWater = Convert.ToDecimal(sqliteDataReader[6]);
                coldWater.DebtColdWater = Convert.ToDecimal(sqliteDataReader[7]);
                coldWaters.Add(coldWater);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
