using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    interface IDownLoadHotWater
    {
        protected static void ShowAllHotWater(ObservableCollection<HotWater> hotWaters)
        {
            string sqlQuery = "SELECT * FROM hot_water ORDER BY date_hot_water ASC";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdSelectAllColdWater = new(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAllColdWater.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                HotWater hotWater = new();
                hotWater.IdHotWater = Convert.ToInt32(sqliteDataReader[0]);
                hotWater.IdHouse = Convert.ToInt32(sqliteDataReader[1]);
                hotWater.DateHotWater = sqliteDataReader[2].ToString();
                hotWater.LastMetterHotWater = Convert.ToDecimal(sqliteDataReader[3]);
                hotWater.KubHotWater = Convert.ToDecimal(sqliteDataReader[4]);
                hotWater.PriceHotWater = Convert.ToDecimal(sqliteDataReader[5]);
                hotWater.PayedHotWater = Convert.ToDecimal(sqliteDataReader[6]);
                hotWater.DebtHotWater = Convert.ToDecimal(sqliteDataReader[7]);
                hotWaters.Add(hotWater);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
