using Microsoft.Data.Sqlite;
using MyBudget.Data;
using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services.Interfaces
{
    internal interface IDownloadUserCard
    {
        // Загрузка данных из таблицы user_cash БД (сведения о картах пользователей)
        protected static void LoadAllCardsMainWindow(ObservableCollection<Card_User> p)
        {
            string sqlAllCard = "SELECT * FROM user_cash";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdAllCardUsers = new SqliteCommand
                (sqlAllCard, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdAllCardUsers.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                Card_User card = new Card_User
                {
                    IdUser = Convert.ToInt32(sqliteDataReader[0]),
                    IdCard = Convert.ToInt32(sqliteDataReader[1]),
                    CardNumber = sqliteDataReader[2].ToString(),
                    CardBalance = Convert.ToDecimal(sqliteDataReader[3])
                };
                p.Add(card);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }

        /*метод отображает принадлежащие пользователю карты (имя, фамилию, номер карты)*/
        protected static void ShowCardUser(ObservableCollection<CardHolder> cards)
        {
            //Команда получения данных к БД
            string sqlRequestShowCard =
                "SELECT name, surname, card_number FROM user JOIN user_cash WHERE user.id_user = user_cash.id_user";
            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdAllCardUsers = new SqliteCommand
                (sqlRequestShowCard, connection.GetConnection());
            SqliteDataReader sqliteDataReader = cmdAllCardUsers.ExecuteReader();
            while (sqliteDataReader.Read())
            {
                CardHolder cardUser = new CardHolder
                {
                    NameUser = sqliteDataReader[0].ToString(),
                    SurnameUser = sqliteDataReader[1].ToString(),
                    CardNumber = sqliteDataReader[2].ToString()
                };
                cards.Add(cardUser);
            }
            sqliteDataReader.Close();
            connection.CloseConnection();
        }
    }
}
