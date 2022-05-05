using Microsoft.Data.Sqlite;
using MyBudget.Commands;
using MyBudget.Data;
using MyBudget.Models;
using MyBudget.Services;
using MyBudget.Services.Interfaces;
using MyBudget.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MyBudget.ViewModels
{
    internal class AddNewHouse_ViewModel : ViewModel_Base, IDownloadHouse
    {
        #region Заголовок окна Tittle
        private string _Title = "Новый дом....";
        /// <summary>Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства для привязки к элементам окна
        private string _newSity;
        public string NewSity
        {
            get { return _newSity; }
            set
            {
                _newSity = value;
                OnPropertyChanged(nameof(NewSity));
            }
        }

        private string _newStreet;
        public string NewStreet
        {
            get { return _newStreet; }
            set
            {
                _newStreet = value;
                OnPropertyChanged(nameof(NewStreet));
            }
        }

        private string _newHouse;
        public string NewHouse
        {
            get { return _newHouse; }
            set
            {
                _newHouse = value;
                OnPropertyChanged(nameof(NewHouse));
            }
        }

        private string _newAppartament = null;
        public string NewAppartament
        {
            get { return _newAppartament; }
            set
            {
                _newAppartament = value;
                OnPropertyChanged(nameof(NewAppartament));
            }
        }
        #endregion

        #region Команда добавления нового адреса
        public ICommand AddNewHouseCmd { get; }
        private bool CanAddNewHouseCmdExecute(object p) => true;
        private void OnAddNewHouseCmdExecuted(object p)
        {
            if(NewSity is null or "" || NewStreet is null or "" || NewHouse is null or "" || NewAppartament is null or "")
            {
                MessageBox.Show("Заполните все поля!", "Ошибка!");
            }
            else
            {
                House home = new();
                home.Sity = NewSity;
                home.Street = NewStreet;
                home.NumberHouse = NewHouse;
                home.NumberAppartament = Convert.ToInt32(NewAppartament);
                AddNewHouse(home);
                Collection.Houses.Clear();
                IDownloadHouse.ShowHouse(Collection.Houses);
                MessageBox.Show("Операция выполнена", "Успех!");
                NewSity = "";
                NewStreet = "";
                NewHouse = "";
                NewAppartament = "";
            }
        }

        #endregion

        #region Конструктор
        public AddNewHouse_ViewModel()
        {
            AddNewHouseCmd = new LamdaCommand(OnAddNewHouseCmdExecuted, CanAddNewHouseCmdExecute);
            Collection.Houses.Clear();
            IDownloadHouse.ShowHouse(Collection.Houses);
        }
        #endregion

        #region метод добавления нового дома
        private void AddNewHouse(House home)
        {
            //string id = "null, ";
            string sity = "'" + home.Sity + "', ";
            string street = "'" + home.Street + "', ";
            string house = "'" + home.NumberHouse + "', ";
            int appartament = home.NumberAppartament;

            string sqlAddNewHouse = "INSERT INTO house VALUES (NULL, " + sity + street + house + appartament + ")";
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdInsertNewHouse = new(sqlAddNewHouse, connection.GetConnection());
            cmdInsertNewHouse.ExecuteNonQuery();
            connection.CloseConnection();
        }

        #endregion
    }
}
