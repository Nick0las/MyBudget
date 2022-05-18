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

        //Коллекция всех добавленных видов поступления
        public static ObservableCollection<TypeTransfer> AllTypeTransfer { get; set; } = new ObservableCollection<TypeTransfer>();

        // Коллекция всех добавленных домов
        public static ObservableCollection<House> Houses { get; set; } = new ObservableCollection<House>();

        // Коллекция добавленных провайдеров
        public static ObservableCollection<Provider> Providers { get; set; } = new ObservableCollection<Provider>();

        // Коллекция для хранения данных об услугах провайдера (для пользователя) из запроаса SELECT provider JOIN services_provider
        public static ObservableCollection<ProviderJoinServices> ProviderJoinServices { get; set; } = new ObservableCollection<ProviderJoinServices>();

        // Коллекция для хранения типов трат на авто
        public static ObservableCollection<TypeCostsAuto> TypesCostsAuto { get; set; } = new ObservableCollection<TypeCostsAuto>();

        // Коллекция для хранения всех авто
        public static ObservableCollection<Auto> Autos { get; set; } = new ObservableCollection<Auto>();

        // Коллекция для хранения данных о холодной воде
        public static ObservableCollection<ColdWater> ColdWaters { get; set; } = new ObservableCollection<ColdWater>();

        // Коллекция для хранения данных о горячей воде
        public static ObservableCollection<HotWater> HotWaters { get; set; } = new ObservableCollection<HotWater>();

        // Коллекция для хранения данных о водоотведении
        public static ObservableCollection<WaterRemove> WaterRemoves { get; set; } = new ObservableCollection<WaterRemove>();
    }
}
