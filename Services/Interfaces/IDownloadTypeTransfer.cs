using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    interface IDownloadTypeTransfer
    {
        protected static void ShowTypeTransfer(ObservableCollection<TypeTransfer> coll)
        {
            string sqlAllTypeTransfer = "SELECT * FROM type_transfer";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdAllTypeTransfer = new SqliteCommand(sqlAllTypeTransfer, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdAllTypeTransfer.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                TypeTransfer transfer = new();
                transfer.IdTypeTransfer = Convert.ToInt32(sqliteDataReader[0]);
                transfer.TittleTypeTransfer = sqliteDataReader[1].ToString();
                coll.Add(transfer);
            }
            
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
