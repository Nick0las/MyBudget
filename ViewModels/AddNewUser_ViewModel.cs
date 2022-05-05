using Microsoft.Data.Sqlite;
using MyBudget.Commands;
using MyBudget.Data;
using MyBudget.Models;
using MyBudget.Services;
using MyBudget.Services.Interfaces;
using MyBudget.ViewModels.Base;
using System;
using System.Windows;
using System.Windows.Input;

namespace MyBudget.ViewModels 
{
    internal class AddNewUser_ViewModel : ViewModel_Base, IDownloadUsers
    {
        #region Заголовок окна Tittle
        private string _Title = "Новый член семьи....";
        /// <summary>Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства для привязки к элементам окна TextBox и CheckBox
        private string _name = "";
        private string _surname = "";
        private bool _child;
        public string NewUserName
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(NewUserName));
            }
        }

        
        public string NewUserSurname 
        {
            get { return _surname; }
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(NewUserSurname));
            }
        }
        public bool NewUserChild
        {
            get { return _child; }
            set
            {
                _child = value;
                OnPropertyChanged(nameof(NewUserChild));
            }
        }
        #endregion

        User newUser = new();

        #region команда добавления члена семьи
        public ICommand AddNewUserCmd { get; }
        private bool CanAddNewUserCmdExecute(object p) => true;
        private void OnAddNewUserCmdExecuted(object p)
        {
            if(NewUserName == "" || NewUserSurname == "")
            {
                _ = MessageBox.Show(
                    "Заполните все поля!", 
                    "Ошибка!",
                     MessageBoxButton.OK,
                     MessageBoxImage.Hand
                    );
                NewUserName = "";
                NewUserSurname = "";
                NewUserChild = false;
                return;
            }
            else
            {
                newUser.Name = NewUserName;
                newUser.Surname = NewUserSurname;
                newUser.Child = NewUserChild;
                Collection.Users.Add(newUser);
                //AddNewUser(newUser);
                _ = MessageBox.Show(
                    "Пользователь добавлен!",
                    "Успех!",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information
                    );
                NewUserName = "";
                NewUserSurname = "";
                NewUserChild = false;
            }
            
        }
        #endregion

        #region Конструктор класса
        public AddNewUser_ViewModel()
        {
            Collection.Users.Clear();
            IDownloadUsers.ShowUser(Collection.Users);
            AddNewUserCmd = new LamdaCommand
                (OnAddNewUserCmdExecuted, CanAddNewUserCmdExecute);
        }
        #endregion


        #region метод добавления нового члена семьи в БД
        private static void AddNewUser(User newUser)
        {
            string name = newUser.Name;
            string surname = newUser.Surname;
            bool? child = newUser.Child;
            string sqlInsertNewUser =
                "INSERT INTO user VALUES " + "(NULL, " + "'" + name + "'" + ", " + "'" + surname + "'" + ", " + child + ")";
            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdInsertNewUser = new SqliteCommand
                (sqlInsertNewUser, connection.GetConnection());
            cmdInsertNewUser.ExecuteNonQuery();
            connection.CloseConnection();
        }
        #endregion
    }
}
