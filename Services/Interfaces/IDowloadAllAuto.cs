using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    interface IDowloadAllAuto
    {
        protected static void ShowAllAuto(ObservableCollection<Auto> autos)
        {
            
            string sqlAllAuto = "SELECT * FROM auto";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdSelectAutos = new(sqlAllAuto, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectAutos.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                Auto auto = new();
                auto.IdAuto = Convert.ToInt32(sqliteDataReader[0]);
                auto.NameAuto = sqliteDataReader[1].ToString();
                autos.Add(auto);
            }
            
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
