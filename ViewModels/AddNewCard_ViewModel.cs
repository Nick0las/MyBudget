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
    class AddNewCard_ViewModel : ViewModel_Base, IDownloadUserCard, IDownload_AllBalance, IDownloadUsers
    {

        #region Заголовок окна
        private string _Title = "Новый счет пользователя...";
        /// <summary>Заголовок окна </summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region свойства для привязки к окну
        private string _cardNumber4DB = "";
        public string CardNumber4DB
        {
            get { return _cardNumber4DB; }
            set
            {
                _cardNumber4DB = value;
                OnPropertyChanged(nameof(CardNumber4DB));
            } 
        }

        //свойство, привязанное к TextBox текущий баланс
        private string _currentBalance4DB = "";
        public string CurrentBalance4Db
        {
            get { return _currentBalance4DB; }
            set
            {
                _currentBalance4DB = value;
                OnPropertyChanged(nameof(CurrentBalance4Db));
            }
        }
        //свойство, привязанное к ComboBox для перехвата выбранного пользователем значения
        public User SelectedUser { get; set; }
        #endregion

        #region команда добавления новой карты(счет) пользователя
        public ICommand AddNewCardUserCmd { get; }
        private bool CanAddNewCardUserCmdExecute(object p) => true;
        private void OnAddNewCardUserCmdExecuted(object p)
        {
            Card_User newCard = new Card_User();
            newCard.IdUser = SelectedUser.IdUser;
            newCard.CardNumber = CardNumber4DB;
            newCard.CardBalance = Convert.ToDecimal(CurrentBalance4Db);
            
            bool x = FindCard(Collection.CardHolders, newCard);
            if (x)
            {
                AddNewCardUser2DB(newCard);
                Collection.CardHolders.Clear();
                Collection.Cards.Clear();
                IDownloadUserCard.LoadAllCardsMainWindow(Collection.Cards);
                IDownloadUserCard.ShowCardUser(Collection.CardHolders);
                Collection.AllBalance.Clear();
                IDownload_AllBalance.ShowAllBalance(Collection.AllBalance);
                MessageBox.Show(
                    "Карта добавлена!",
                    "Успех!");
            }
            else
            {
                return;
            }
            CardNumber4DB = "";
            CurrentBalance4Db = "";
        }
        #endregion

        #region Конструктор
        public AddNewCard_ViewModel()
        {
            Collection.Users.Clear();
            Collection.CardHolders.Clear();
            AddNewCardUserCmd = new LamdaCommand(OnAddNewCardUserCmdExecuted, CanAddNewCardUserCmdExecute);
            IDownloadUserCard.ShowCardUser(Collection.CardHolders);
            IDownloadUsers.ShowUser(Collection.Users);
        }
        #endregion

        #region методы
        // Поиск карты в коллекции
        private bool FindCard(ObservableCollection<CardHolder> cardsHolder, Card_User usersCard)
        {
            foreach (var card in cardsHolder)
            {
                if (card.CardNumber == usersCard.CardNumber)
                {
                    MessageBox.Show(
                       "Карта существует! Попробовать еще раз?",
                       "Что-то не так =(",
                       MessageBoxButton.YesNo,
                       MessageBoxImage.Error
                       );
                    return false;
                }
            }
            return true;
        }
        
        // добавление новой карты
        private void AddNewCardUser2DB(Card_User p)
        {
            int id_User4Db = p.IdUser;
            string cardNumber4Db = "'" + p.CardNumber + "'";
            string balance4Db = p.CardBalance.ToString();
            string sqlQueryNewCard = "INSERT INTO user_cash VALUES (NUll" + ", " + id_User4Db + ", " + cardNumber4Db + ", " + balance4Db + ")";
            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdInsertNewCard = new SqliteCommand(sqlQueryNewCard, connection.GetConnection());
            cmdInsertNewCard.ExecuteNonQuery();
            connection.CloseConnection();
        }

        #endregion

    }
}
