/*********************************************************************/
/*          VM к окну AddExpensesElectricity (электроэнергия)                    
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
    class AddExpensesElectricity_ViewModel : ViewModel_Base, IDownloadHouse, IDownload_AllBalance, IDownloadElectricity, IDownloadUserCard
    {
        #region Заголовок окна
        private string _Title = "Добавлние нового расхода электроэнергии";
        /// <summary>Заголовок окна </summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства, привязанные к окну
        //Выбор даты
        public DateTime NewDateElectricity { get; set; } = DateTime.Now;

        //Выбор дома
        public House SelectedHouse { get; set; }

        // Привязка к DataGrid предыдущее значение счетчика электроэнергии
        private Electricity selectedElectricity;
        public Electricity SelectedElectricity
        {
            get { return selectedElectricity; }
            set => Set(ref selectedElectricity, value);
        }

        // Привязка к TextBox "Новое значение счетчика электроэнергии"
        private decimal newMetterElectricity;
        public decimal NewMetterElectricity
        {
            get { return newMetterElectricity; }
            set => Set(ref newMetterElectricity, value);
        }


        // Привязка к TextBox "Разница нового и старого значения счетчика" (количество ватт)
        private decimal _expenWt;
        public decimal ExpenWt
        {
            get => _expenWt;
            set
            {
                if (SelectedElectricity == null)
                {
                    SelectedElectricity.LastMetterElectricity = value;
                }
                Set(ref _expenWt, NewMetterElectricity - SelectedElectricity.LastMetterElectricity);
            }
        }

        // Привязка к TextBox "Стоимость 1 ватта"
        private decimal _price1Wt;
        public decimal Price1Wt
        {
            get { return _price1Wt; }
            set => SetProperty(ref _price1Wt, value);
        }

        // Привязка к TextBox "Должно быть начислено"
        private decimal _сalculationWt;
        public decimal CalculationWt //
        {
            get { return _сalculationWt; }
            set => SetProperty(ref _сalculationWt, value);
        }
        //Привязка к TextBox "Начислено в квитанции"
        private decimal _2BePaid;
        public decimal ToBePaid
        {
            get { return _2BePaid; }
            set => Set(ref _2BePaid, value);
        }

        // Привязка к TextBox "Оплачено"
        private decimal _payedElectricity;
        public decimal PayedElectricity
        {
            get { return _payedElectricity; }
            set => Set(ref _payedElectricity, value);
        }

        // Привязка к TextBox "Долг"
        private decimal _debtElectricity;
        public decimal DebtElectricityOnForms
        {
            get { return _debtElectricity; }
            set => Set(ref _debtElectricity, ToBePaid - PayedElectricity);
        }
        // Выбор карты для списания оплаченных средств
        public CardHolder Card { get; set; }
        #endregion

        #region Команда добавления нового расхода "Электроэнергия" в БД
        public ICommand AddNewElecricityCmd { get; }
        private bool CanAddNewElecricityCmdExecute(object p) => true;
        private void OnAddNewElecricityCmdExecuted(object p)
        {
            Electricity electricity = new();
            if (SelectedHouse == null)
            {
                MessageBoxResult result = MessageBox.Show("Забыли указать дом!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (NewDateElectricity == DateTime.Today)
            {
                MessageBoxResult result = MessageBox.Show("Выбрана текущая дата! Продолжить?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        MessageBox.Show("Пользователь отказался!");
                        break;
                    case MessageBoxResult.OK:
                        AddNewElecticity2DB();
                        Collection.Electricities.Clear();
                        IDownloadElectricity.ShowElectricity(Collection.Electricities);
                        IDownload_AllBalance.UpdateCashAfterInsertCosts(PayedElectricity, Collection.AllBalance);
                        IDownloadUserCard.UpdateBalanceUserCard(Card, PayedElectricity);
                        Collection.Cards.Clear();
                        IDownloadUserCard.LoadAllCardsMainWindow(Collection.Cards);
                        Collection.AllBalance.Clear();
                        IDownload_AllBalance.ShowAllBalance(Collection.AllBalance);
                        break;
                }
            }
            else
            {
                AddNewElecticity2DB();
                Collection.Electricities.Clear();
                IDownloadElectricity.ShowElectricity(Collection.Electricities);
                IDownload_AllBalance.UpdateCashAfterInsertCosts(PayedElectricity, Collection.AllBalance);
                IDownloadUserCard.UpdateBalanceUserCard(Card, PayedElectricity);
                Collection.Cards.Clear();
                IDownloadUserCard.LoadAllCardsMainWindow(Collection.Cards);
                Collection.AllBalance.Clear();
                IDownload_AllBalance.ShowAllBalance(Collection.AllBalance);
            }
           

        }

        #endregion


        #region Конструктор
        public AddExpensesElectricity_ViewModel()
        {
            AddNewElecricityCmd = new LamdaCommand(OnAddNewElecricityCmdExecuted, CanAddNewElecricityCmdExecute);
            Collection.Houses.Clear();
            Collection.Electricities.Clear();
            Collection.CardHolders.Clear();
            IDownloadUserCard.ShowCardUser(Collection.CardHolders);
            IDownloadHouse.ShowHouse(Collection.Houses);
            IDownloadElectricity.ShowElectricity(Collection.Electricities);
        }
        #endregion

        #region Методы
        // Переопределение метода SetProperty
        protected override void SetProperty<T>(ref T fieldProperty, T newValue, [CallerMemberName] string propertyName = "")
        {
            base.SetProperty(ref fieldProperty, newValue, propertyName);
            if (propertyName == nameof(Price1Wt) || propertyName == nameof(ExpenWt))
            {
                CalculationWt = Price1Wt * ExpenWt;
            }
        }
        //Добавление данных о расходе Электроэнергии
        private void AddNewElecticity2DB()
        {
            string idHouse = "'" + SelectedHouse.IdHouse.ToString() + "', ";
            string dateElectricity = "'" + NewDateElectricity.ToLongDateString() + "', ";
            string lastMettersElectricity = "'" + NewMetterElectricity.ToString() + "', ";
            string wtElectricity = "'" + ExpenWt.ToString() + "', ";
            string priceElectricity = "'" + Price1Wt.ToString() + "', ";
            string payedElectricity = "'" + PayedElectricity.ToString() + "', ";
            string debtElectriciy = "'" + DebtElectricityOnForms.ToString() + "'";
            string sqlQueryInsert2Electricity = @"INSERT INTO electricity VALUES (NULL, " + idHouse + dateElectricity + lastMettersElectricity + wtElectricity + priceElectricity + payedElectricity + debtElectriciy + ");";

            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdInsert2Electricity = new(sqlQueryInsert2Electricity, connection.GetConnection());
            cmdInsert2Electricity.ExecuteNonQuery();
            connection.CloseConnection();
        }
        #endregion
    }
}
