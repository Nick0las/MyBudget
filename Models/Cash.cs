/**************************************************/
/*     Модель таблицы Kassa из таблицы БД     */
/**************************************************/

using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Services;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Models 
{
    class Cash
    {
        //Id строки в таблице
        public int Id_String { get; set; }

        // Сумма в общем бюджете
        public decimal CashBalance { get; set; }

        //public void ShowAllBalance(ObservableCollection<Cash> cashes)
        //{
        //    Cash kassa = new();
        //    string sqlAllBalance = "SELECT * FROM kassa";
        //    ConnectionDB connection = new();
        //    connection.OpenConnection();
        //    SqliteCommand cmdSelectKassa = new(sqlAllBalance, connection.GetConnection());
        //    SqliteDataReader sqliteDataReader = cmdSelectKassa.ExecuteReader();
        //    while (sqliteDataReader.Read())
        //    {
        //        kassa.Id_String = Convert.ToInt32(sqliteDataReader[0]);
        //        kassa.CashBalance = Convert.ToDecimal(sqliteDataReader[1]);
        //    }
        //    cashes.Add(kassa);
        //    sqliteDataReader.Close();
        //    connection.CloseConnection();
        //}
    }
}
