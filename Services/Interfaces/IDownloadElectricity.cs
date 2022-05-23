using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    interface IDownloadElectricity
    {
        protected static void ShowElectricity(ObservableCollection<Electricity> electricities)
        {

            string sqlQuery = "SELECT * FROM electricity";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdSelectAllElectricity = new(sqlQuery, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAllElectricity.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                Electricity electricity = new();
                electricity.IdElectricity = Convert.ToInt32(sqliteDataReader[0]);
                electricity.IdHouse = Convert.ToInt32(sqliteDataReader[1]);
                electricity.DateElectricity = sqliteDataReader[2].ToString();
                electricity.LastMetterElectricity = Convert.ToDecimal(sqliteDataReader[3]);
                electricity.SumWtElectricity = Convert.ToDecimal(sqliteDataReader[4]);
                electricity.PriceElectricity = Convert.ToDecimal(sqliteDataReader[5]);
                electricity.PayedElectricity = Convert.ToDecimal(sqliteDataReader[6]);
                electricity.DebtElectricity = Convert.ToDecimal(sqliteDataReader[7]);
                electricities.Add(electricity);
            }

            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
