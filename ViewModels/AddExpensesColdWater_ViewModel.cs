/*********************************************************************/
/*                      VM к окну AddNewColdWater                    */
/*     необходимо добавить реализацию вычисления в TextBox runTime   */
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
    //ViewModel_Base,OnPropertyChangedClass
    internal class AddExpensesColdWater_ViewModel : ViewModel_Base, IDownloadHouse, IDownloadColdWater
    {
        #region Заголовок окна
        private string _Title = "Добавлние нового расхода холодной воды.";
        /// <summary>Заголовок окна </summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства, привязанные к окну
        //Свойство для захвата даты
        public DateTime NewDateColdWater { get; set; } = DateTime.Now;

        //private DateTime _newDateColdWater = DateTime.Now;
        //public DateTime NewDateColdWater
        //{
        //    get { return _newDateColdWater; }
        //    set => Set(ref _newDateColdWater, value);
        //}


        // Свойство для перехвата значения из ComboBox        
        public House SelectedHouse { get; set; }
        
        // Привязка к DataGrid
        private ColdWater selectedColdWater;
        public ColdWater SelectedColdWater
        {
            get { return selectedColdWater; }
            set => Set(ref selectedColdWater, value);
        }
        // Привязка к TextBox "Новое значение"
        private decimal newMetter;
        public decimal NewMetter
        {
            get { return newMetter; }
            set => Set(ref newMetter, value);
        }

        // Привязка к TextBox "Разница нового и старого значения счетчика"
        private decimal _expenKub;
        public decimal ExpenKub
        {
            get => _expenKub;
            // Вот тут делаю что-то не то // 
            set
            { if(SelectedColdWater == null)
                {
                    SelectedColdWater.LastMetterColdWater = value;
                }
                Set(ref _expenKub, NewMetter - SelectedColdWater.LastMetterColdWater);
            }
        }

        // Привязка к TextBox "Стоимость 1 куба"
        private decimal _price1Kub;
        public decimal Price1Kub
        {
            get { return _price1Kub; }
            set => SetProperty(ref _price1Kub, value);
        }

        // Привязка к TextBox "Должно быть начислено"
        private decimal _сalculation;
        public decimal Calculation //
        {
            get { return _сalculation; }
            set => SetProperty(ref _сalculation, value);
        }

        //Привязка к TextBox "Начислено в квитанции"
        private decimal _2BePaid;
        public decimal ToBePaid
        {
            get { return _2BePaid; }
            set => Set(ref _2BePaid, value);
        }

        // Привязка к TextBox "Оплачено"
        private decimal _payedColdWater;
        public decimal PayedColdWater
        {
            get { return _payedColdWater; }
            set => Set(ref _payedColdWater, value);
        }

        // Привязка к TextBox "Долг"
        private decimal _debtColdWater;
        public decimal DebtColdWaterOnForms
        {
            get { return _debtColdWater; }
            set => Set(ref _debtColdWater, ToBePaid - PayedColdWater);
        }
        #endregion

        #region Команда добавления нового расхода холдной воды в БД
        public ICommand AddNewColdWaterCmd { get; }
        private bool CanAddNewColdWaterCmdExecute(object p) => true;
        private void OnAddNewColdWaterCmdExecuted(object p)
        {
            ColdWater coldWater = new();
            if(SelectedHouse == null)
            {
                MessageBoxResult result = MessageBox.Show("Забыли указать дом!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if(NewDateColdWater == DateTime.Today)
            {
                MessageBoxResult result = MessageBox.Show("Выбрана текущая дата! Продолжить?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                switch(result)
                {
                    case MessageBoxResult.Cancel:
                        MessageBox.Show("Пользователь отказался!");
                        break;
                    case MessageBoxResult.OK:                       
                        Add2ColdWater(coldWater);
                        AddNewMetterColdWater2DB(coldWater);
                        Collection.ColdWaters.Clear();
                        IDownloadColdWater.ShowAllColdWater(Collection.ColdWaters);
                        MessageBox.Show("Данные добавлены!");
                        break;
                }
            }
            else
            {
                Add2ColdWater(coldWater);
                AddNewMetterColdWater2DB(coldWater);
                Collection.ColdWaters.Clear();
                IDownloadColdWater.ShowAllColdWater(Collection.ColdWaters);
                MessageBox.Show("Данные добавлены!");
            }
            
        }

        #endregion
        #region Конструктор
        // Конструктор
        public AddExpensesColdWater_ViewModel()
        {
            AddNewColdWaterCmd = new LamdaCommand(OnAddNewColdWaterCmdExecuted, CanAddNewColdWaterCmdExecute);
            Collection.ColdWaters.Clear();
            Collection.Houses.Clear();
            IDownloadHouse.ShowHouse(Collection.Houses);
            IDownloadColdWater.ShowAllColdWater(Collection.ColdWaters);
        }
        #endregion

        #region Методы
        protected override void SetProperty<T>(ref T fieldProperty, T newValue, [CallerMemberName] string propertyName = "")
        {
            base.SetProperty(ref fieldProperty, newValue, propertyName);
            if (propertyName == nameof(Price1Kub) || propertyName == nameof(ExpenKub))
            {
                Calculation = Price1Kub * ExpenKub;
            }
        }

        // Метод загружает данные в БД
        private void AddNewMetterColdWater2DB(ColdWater water)
        {
            string idHouse = water.IdHouse.ToString() + ", ";
            string dateWater = @"'" + water.DateColdWater + "', ";
            string metterWater = @"'" + water.LastMetterColdWater.ToString() + "', ";
            string kub = @"'" + water.KubColdWater.ToString() + "', ";
            string price = @"'" + water.PriceColdWater.ToString() + "', ";
            string payed = @"'" + water.PayedColdWater.ToString() + "', ";
            string debt = @"'" + water.DebtColdWater.ToString() + "')";
            string sqlQueryNewColdWater = @"INSERT INTO cold_water VALUES (NULL, " + idHouse + dateWater + metterWater + kub + price + payed + debt;

            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdInsertNewColdWater = new(sqlQueryNewColdWater, connection.GetConnection());
            cmdInsertNewColdWater.ExecuteNonQuery();
            connection.CloseConnection();
        }

        private void Add2ColdWater(ColdWater coldWater)
        {
            coldWater.IdHouse = SelectedHouse.IdHouse;
            coldWater.DateColdWater = NewDateColdWater.ToLongDateString();
            coldWater.LastMetterColdWater = NewMetter;
            coldWater.KubColdWater = ExpenKub;
            coldWater.PriceColdWater = Price1Kub;
            coldWater.PayedColdWater = PayedColdWater;
            coldWater.DebtColdWater = DebtColdWaterOnForms;
        }
        #endregion
    }
}

