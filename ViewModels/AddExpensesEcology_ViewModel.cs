/*********************************************************************/
/*                      VM к окну AddNewEcology                   
/*     необходимо добавить реализацию вычисления в TextBox runTime   
/*                  реализовать вычисления, если БД пустая
/*********************************************************************/

using Microsoft.Data.Sqlite;
using MyBudget.Commands;
using MyBudget.Data;
using MyBudget.Models;
using MyBudget.Services;
using MyBudget.Services.Interfaces;
using MyBudget.ViewModels.Base;
using System;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
namespace MyBudget.ViewModels
{
    class AddExpensesEcology_ViewModel : ViewModel_Base, IDownloadHouse, IDownload_AllBalance, IDownloadUserCard, IDownloadEcology
    {
        #region Заголовок окна
        private string _Title = "Добавлние нового расхода Экология (вывоз мусора)";
        /// <summary>Заголовок окна </summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства, привязываемые к окну
        // Свойство для перехвата значения из ComboBox Дом       
        public House SelectedHouse { get; set; }

        //Свойство для захвата даты
        public DateTime NewDateEcology { get; set; } = DateTime.Now;

        //Привязка к TextBox "Начислено:"
        private decimal _2BePaid;
        public decimal ToBePaid
        {
            get { return _2BePaid; }
            set => Set(ref _2BePaid, value);
        }
        // Привязка к TextBox "Оплачено"
        private decimal _payedEcology;
        public decimal PayedEcology
        {
            get { return _payedEcology; }
            set => Set(ref _payedEcology, value);
        }
        // Привязка к TextBox "Долг"
        private decimal _debtEcology;
        public decimal DebtEcologyOnForms
        {
            get { return _debtEcology; }
            set => Set(ref _debtEcology, ToBePaid - PayedEcology);
        }

        // Выбор карты для списания оплаченных средств
        public CardHolder Card { get; set; }
        #endregion

        #region Команда добавления траты "Экология"
        public ICommand AddNewEcologyCmd { get; }
        private bool CanAddNewEcologyCmdCmdExecute(object p) => true;
        private void OnAddAddNewEcologyCmdExecuted(object p)
        {
            Ecology ecology = new();
            if (SelectedHouse == null)
            {
                MessageBoxResult result = MessageBox.Show("Забыли указать дом!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (NewDateEcology == DateTime.Today)
            {
                MessageBoxResult result = MessageBox.Show("Выбрана текущая дата! Продолжить?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        break;
                    case MessageBoxResult.OK:                        
                        AddNewEcology2DB();
                        Collection.Ecologies.Clear();
                        IDownloadEcology.ShowAllEcology(Collection.Ecologies);
                        IDownload_AllBalance.UpdateCashAfterInsertCosts(PayedEcology, Collection.AllBalance);
                        Collection.AllBalance.Clear();
                        IDownload_AllBalance.ShowAllBalance(Collection.AllBalance);
                        IDownloadUserCard.UpdateBalanceUserCard(Card, PayedEcology);
                        Collection.Cards.Clear();
                        IDownloadUserCard.LoadAllCardsMainWindow(Collection.Cards);
                        break;
                }
            }
            else
            {
                AddNewEcology2DB();
                Collection.Ecologies.Clear();
                IDownloadEcology.ShowAllEcology(Collection.Ecologies);
                IDownload_AllBalance.UpdateCashAfterInsertCosts(PayedEcology, Collection.AllBalance);
                Collection.AllBalance.Clear();
                IDownload_AllBalance.ShowAllBalance(Collection.AllBalance);
                IDownloadUserCard.UpdateBalanceUserCard(Card, PayedEcology);
                Collection.Cards.Clear();
                IDownloadUserCard.LoadAllCardsMainWindow(Collection.Cards);
            }
        }
        #endregion

        #region Конструктор
        public AddExpensesEcology_ViewModel()
        {
            AddNewEcologyCmd = new LamdaCommand(OnAddAddNewEcologyCmdExecuted, CanAddNewEcologyCmdCmdExecute);
            Collection.Houses.Clear();
            Collection.CardHolders.Clear();
            IDownloadEcology.ShowAllEcology(Collection.Ecologies);
            IDownloadUserCard.ShowCardUser(Collection.CardHolders);
            IDownloadHouse.ShowHouse(Collection.Houses);
        }
        #endregion


        #region Методы
        // Добавление нового расхода "Экология" в БД
        private void AddNewEcology2DB()
        {
            string dateEcology = "'" + NewDateEcology.ToLongDateString() +  "', ";
            string idHouser = "'" + SelectedHouse.IdHouse +  "', ";
            string priceEcology = "'" + ToBePaid + "', ";
            string payedEcology = "'" + PayedEcology + "', ";
            string debtEcology = "'" + DebtEcologyOnForms + "'";
            string sqlQureyNewEcology = @"INSERT INTO ecology VALUES (NULL, " + dateEcology + idHouser + priceEcology + payedEcology + debtEcology + ")";

            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdInsert2Ecology = new(sqlQureyNewEcology, connection.GetConnection());
            cmdInsert2Ecology.ExecuteNonQuery();
            connection.CloseConnection();
        }
        #endregion


    }
}
