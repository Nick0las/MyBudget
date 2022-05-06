

using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    interface IDownloadTypeCostsAuto
    {
        protected static void ShowTypeCostsAuto(ObservableCollection<TypeCostsAuto> types)
        {
            
            string sqlTypeCostsAuto = "SELECT * FROM type_costs_auto ";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdSelectTypeCostsAuto = new(sqlTypeCostsAuto, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectTypeCostsAuto.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                TypeCostsAuto newType = new();
                newType.IdTypeCostsAuto = Convert.ToInt32(sqliteDataReader[0]);
                newType.NameTypeCostsAuto = sqliteDataReader[1].ToString();
                types.Add(newType);
            }            
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
