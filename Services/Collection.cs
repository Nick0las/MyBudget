using MyBudget.Models;
using System;
using System.Collections.ObjectModel;

namespace MyBudget.Services
{
    internal class Collection
    {
        // Коллекция для хранения данных о пользователях (члены семьи) из таблицы БД 'Users'.
        public static ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        // Коллекция для хранения данных о содержащихся финансах в общем бюджете из таблицы БД 'Kassa'.
        public static ObservableCollection<Cash> AllBalance { get; set; } = new ObservableCollection<Cash>();

        // Коллекция для хранения о картах пользователей (членов семьи) из таблицы User_Cash.
        public static ObservableCollection<Card_User> Cards { get; set; } = new ObservableCollection<Card_User>();

        //коллекция всех добавленных счетов с балансом
        public static ObservableCollection<CardHolder> CardHolders { get; set; } = new ObservableCollection<CardHolder>();

    }
}
