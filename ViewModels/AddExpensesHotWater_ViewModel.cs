/*********************************************************************/
/*                      VM к окну AddNewHotWater                    
/*     необходимо добавить реализацию вычисления в TextBox runTime   
/*                 реализовать вычисления, если БД пустая
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
    class AddExpensesHotWater_ViewModel : ViewModel_Base, IDownLoadHotWater, IDownloadHouse, IDownload_AllBalance, IDownloadUserCard
    {
        #region Заголовок окна
        private string _Title = "Добавлние нового расхода горячей воды.";
        /// <summary>Заголовок окна </summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства, привязываемые к окну
        
        // Свойство для даты
        public DateTime NewDateHotWater { get; set; } = DateTime.Now;

        // Свойство для перехвата значения из ComboBox "Выбор дома"
        public House SelectedHouse { get; set; }

        // Привязка к DataGrid
        private HotWater selectedHotWater;
        public HotWater SelectedHotWater
        {
            get { return selectedHotWater; }
            set => Set(ref selectedHotWater, value);
        }

        // Привязка к TextBox "Новое значение"
        private decimal newMetterHotWater;
        public decimal NewMetterHotWater
        {
            get { return newMetterHotWater; }
            set => Set(ref newMetterHotWater, value);
        }

        // Привязка к TextBox "Разница нового и старого значения счетчика"
        private decimal _expenKub;
        public decimal ExpenKub
        {
            get => _expenKub;
            // Вот тут делаю что-то не то // 
            set
            {
                if (SelectedHotWater == null)
                {
                    SelectedHotWater.LastMetterHotWater = value;
                }
                Set(ref _expenKub, NewMetterHotWater - SelectedHotWater.LastMetterHotWater);
            }
        }

        // Привязка к TextBox "Стоимость 1 куба"
        private decimal _price1KubHotWater;
        public decimal Price1Kub
        {
            get { return _price1KubHotWater; }
            set => SetProperty(ref _price1KubHotWater, value);
        }

        // Привязка к TextBox "Должно быть начислено"
        private decimal _сalculationHotWater;
        public decimal CalculationHotWater //
        {
            get { return _сalculationHotWater; }
            set => SetProperty(ref _сalculationHotWater, value);
        }

        //Привязка к TextBox "Начислено в квитанции"
        private decimal _2BePaidHotWater;
        public decimal ToBePaidHotWater
        {
            get { return _2BePaidHotWater; }
            set => Set(ref _2BePaidHotWater, value);
        }

        // Привязка к TextBox "Оплачено"
        private decimal _payedHotWater;
        public decimal PayedHotWater
        {
            get { return _payedHotWater; }
            set => Set(ref _payedHotWater, value);
        }

        // Привязка к TextBox "Долг"
        private decimal _debtHotWater;
        public decimal DebtHotWaterOnForms
        {
            get { return _debtHotWater; }
            set => Set(ref _debtHotWater, ToBePaidHotWater - PayedHotWater);
        }

        // Выбор карты для списания средств оплаченных трат
        public CardHolder Card { get; set; }
        #endregion


        #region Команда добавления нового расхода горячей воды в БД
        public ICommand AddNewHotWaterCmd { get; }
        private bool CanAddNewHotWaterCmdExecute(object p) => true;
        private void OnAddNewHotWaterCmdExecuted(object p)
        {
            HotWater hotWater = new();
            if (SelectedHouse == null)
            {
                MessageBoxResult result = MessageBox.Show("Забыли указать дом!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (NewDateHotWater == DateTime.Today)
            {
                MessageBoxResult result = MessageBox.Show("Выбрана текущая дата! Продолжить?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        MessageBox.Show("Пользователь отказался!");
                        break;
                    case MessageBoxResult.OK:
                        Add2HotWater(hotWater);
                        AddNewMetterHotWater2DB(hotWater);
                        Collection.HotWaters.Clear();
                        IDownLoadHotWater.ShowAllHotWater(Collection.HotWaters);
                        IDownload_AllBalance.UpdateCashAfterInsertCosts(hotWater.PayedHotWater, Collection.AllBalance);
                        Collection.AllBalance.Clear();
                        IDownload_AllBalance.ShowAllBalance(Collection.AllBalance);
                        IDownloadUserCard.UpdateBalanceUserCard(Card, PayedHotWater);
                        Collection.Cards.Clear();
                        IDownloadUserCard.LoadAllCardsMainWindow(Collection.Cards);
                        MessageBox.Show("Данные добавлены!");
                        break;
                }
            }
            else
            {
                Add2HotWater(hotWater);
                AddNewMetterHotWater2DB(hotWater);
                Collection.HotWaters.Clear();
                IDownLoadHotWater.ShowAllHotWater(Collection.HotWaters);
                IDownload_AllBalance.UpdateCashAfterInsertCosts(hotWater.PayedHotWater, Collection.AllBalance);
                Collection.AllBalance.Clear();
                IDownload_AllBalance.ShowAllBalance(Collection.AllBalance);
                IDownloadUserCard.UpdateBalanceUserCard(Card, PayedHotWater);
                Collection.Cards.Clear();
                IDownloadUserCard.LoadAllCardsMainWindow(Collection.Cards);
                MessageBox.Show("Данные добавлены!");
            }
        }

        #endregion


        #region Конструктор
        public AddExpensesHotWater_ViewModel()
        {
            AddNewHotWaterCmd = new LamdaCommand(OnAddNewHotWaterCmdExecuted, CanAddNewHotWaterCmdExecute);
            Collection.HotWaters.Clear();
            Collection.Houses.Clear();
            Collection.CardHolders.Clear();
            IDownloadUserCard.ShowCardUser(Collection.CardHolders);
            IDownloadHouse.ShowHouse(Collection.Houses);
            IDownLoadHotWater.ShowAllHotWater(Collection.HotWaters);
        }
        #endregion

        #region Методы
        protected override void SetProperty<T>(ref T fieldProperty, T newValue, [CallerMemberName] string propertyName = "")
        {
            base.SetProperty(ref fieldProperty, newValue, propertyName);
            if (propertyName == nameof(Price1Kub) || propertyName == nameof(ExpenKub))
            {
                CalculationHotWater = Price1Kub * ExpenKub;
            }
        }
        // Добавление данных о г.воде в БД
        private void AddNewMetterHotWater2DB(HotWater water)
        {
            string idHouse = water.IdHouse.ToString() + ", ";
            string dateWater = @"'" + water.DateHotWater + "', ";
            string metterWater = @"'" + water.LastMetterHotWater.ToString() + "', ";
            string kub = @"'" + water.KubHotWater.ToString() + "', ";
            string price = @"'" + water.PriceHotWater.ToString() + "', ";
            string payed = @"'" + water.PayedHotWater.ToString() + "', ";
            string debt = @"'" + water.DebtHotWater.ToString() + "')";
            string sqlQueryNewHotWater = @"INSERT INTO hot_water VALUES (NULL, " + idHouse + dateWater + metterWater + kub + price + payed + debt;

            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdInsertNewHotWater = new(sqlQueryNewHotWater, connection.GetConnection());
            cmdInsertNewHotWater.ExecuteNonQuery();
            connection.CloseConnection();
        }

        // добавление свойств в объект "горячая вода"
        private void Add2HotWater(HotWater hotWater)
        {
            hotWater.IdHouse = SelectedHouse.IdHouse;
            hotWater.DateHotWater = NewDateHotWater.ToLongDateString();
            hotWater.LastMetterHotWater = NewMetterHotWater;
            hotWater.KubHotWater = ExpenKub;
            hotWater.PriceHotWater = Price1Kub;
            hotWater.PayedHotWater = PayedHotWater;
            hotWater.DebtHotWater = DebtHotWaterOnForms;
        }

        #endregion
    }
}
