using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    interface IDownloadWaterRemove
    {
        protected static void ShowAllWaterRemove(ObservableCollection<WaterRemove> waterRemoves)
        {
            string sqlQuery = "SELECT * FROM water_disposal ORDER BY date_water_disposal DESC";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdSelectAllWaterRemove = new(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAllWaterRemove.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                WaterRemove waterRemove = new();
                waterRemove.IdWaterRemove = Convert.ToInt32(sqliteDataReader[0]);
                waterRemove.IdHouse = Convert.ToInt32(sqliteDataReader[1]);
                waterRemove.DateWaterRemove = sqliteDataReader[2].ToString();
                waterRemove.KubWaterRemove = Convert.ToDecimal(sqliteDataReader[3]);
                waterRemove.Price1KubWaterRemove = Convert.ToDecimal(sqliteDataReader[4]);
                waterRemove.PayedWaterRemove = Convert.ToDecimal(sqliteDataReader[5]);
                waterRemove.DebtWaterRemove = Convert.ToDecimal(sqliteDataReader[6]);
                waterRemoves.Add(waterRemove);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
