using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;


namespace MyBudget.Services
{
     interface IDownload_AllBalance
    {
        protected static void ShowAllBalance(ObservableCollection<Cash> cashes)
        {
            Cash kassa = new();
            string sqlAllBalance = "SELECT * FROM kassa";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdSelectKassa = new(sqlAllBalance, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdSelectKassa.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                kassa.Id_String = Convert.ToInt32(sqliteDataReader[0]);
                kassa.CashBalance = Convert.ToDecimal(sqliteDataReader[1]);
            }
            cashes.Add(kassa);
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
        protected static void UpdateCashAfterInsertCosts(decimal costs, ObservableCollection<Cash> cashes)
        {
            decimal newBalance;
            Cash kassa = new();
            for (int i = 0; i< cashes.Count; i++)
            {
                kassa = cashes[i];
            }
            newBalance = kassa.CashBalance - costs;
            string sqlQeuryUpdate = @"UPDATE kassa SET all_balance = " + "'" + newBalance.ToString() + "'" + " WHERE id_kassa = 1";
            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdUpdateAllBalance = new(sqlQeuryUpdate, connection.GetConnection());
            cmdUpdateAllBalance.ExecuteNonQuery();
            connection.CloseConnection();
        }
    }
    
}
