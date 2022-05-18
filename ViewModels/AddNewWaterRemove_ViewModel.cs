/*********************************************************************/
/*                      VM к окну WaterRemove                    
/*
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
    internal class AddNewWaterRemove_ViewModel : ViewModel_Base, IDownloadHouse, IDownloadWaterRemove
    {
        #region Заголовок окна
        private string _Title = "Добавлние нового расхода 'Водоотведение'.";
        /// <summary>Заголовок окна </summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства, привязанные к окну
        // Свойство для перехвата значения из ComboBox        
        public House SelectedHouse { get; set; }
                
        //Свойство для захвата даты
        public DateTime NewDateWaterRemove { get; set; } = DateTime.Now;

        //Начисленный расход водоотведения (Куб)
        private decimal _summaryAmountWaterRremove;
        public decimal SummaryAmountWaterRremove
        {
            get { return _summaryAmountWaterRremove; }
            set => SetProperty(ref _summaryAmountWaterRremove, value);
        }
        //Стоимость 1 куба
        private decimal _priceWaterRemove;
        public decimal PiceWaterRemove
        {
            get { return _priceWaterRemove; }
            set => SetProperty(ref _priceWaterRemove, value);
        }
        // Должно быть начислено (Руб)
        private decimal _calculatedWaterRemove;
        public decimal CalculatedWaterRemove
        {
            get { return _calculatedWaterRemove; }
            set => SetProperty(ref _calculatedWaterRemove, value);
        }
        // Оплачено
        private decimal _paydWaterRemove;
        public decimal PaydWaterRemove
        {
            get { return _paydWaterRemove; }
            set => Set(ref _paydWaterRemove, value);
        }
        // Начислено в квитанции
        private decimal _2BePaidWaterRemove;
        public decimal ToBePaidWaterRemove
        {
            get { return _2BePaidWaterRemove; }
            set => Set(ref _2BePaidWaterRemove, value);
        }

        private decimal _debtWaterRemove;
        public decimal DebtWaterRemove
        {
            get { return _debtWaterRemove; }
            set => Set(ref _debtWaterRemove, ToBePaidWaterRemove - PaydWaterRemove);
        }

        #endregion

        #region Команда добавления нового "Водоотведения
        public ICommand AddNewWaterRemoveCmd { get; }
        private bool CanAddNewWaterRemoveCmdExecute(object p) => true;
        private void OnAddNewWaterRemoveCmdExecuted(object p)
        {
            WaterRemove waterRemove = new();
            if (SelectedHouse == null)
            {
                MessageBoxResult result = MessageBox.Show("Забыли указать дом!", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (NewDateWaterRemove == DateTime.Today)
            {
                MessageBoxResult result = MessageBox.Show("Выбрана текущая дата! Продолжить?", "Внимание!", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                switch (result)
                {
                    case MessageBoxResult.Cancel:
                        MessageBox.Show("Пользователь отказался!");
                        break;
                    case MessageBoxResult.OK:
                        FillObjectWaterRemove(waterRemove);
                        AddNewWaterRemove2DB(waterRemove);                        
                        Collection.WaterRemoves.Clear();
                        IDownloadWaterRemove.ShowAllWaterRemove(Collection.WaterRemoves);
                        MessageBox.Show("Данные добавлены!");
                        break;
                }
            }
            else
            {
                FillObjectWaterRemove(waterRemove);
                AddNewWaterRemove2DB(waterRemove);
                Collection.WaterRemoves.Clear();
                IDownloadWaterRemove.ShowAllWaterRemove(Collection.WaterRemoves);
                MessageBox.Show("Данные добавлены!");
            }
        }
        #endregion




        #region Конструктор
        public AddNewWaterRemove_ViewModel()
        {
            AddNewWaterRemoveCmd = new LamdaCommand(OnAddNewWaterRemoveCmdExecuted, CanAddNewWaterRemoveCmdExecute);
            Collection.Houses.Clear();
            Collection.WaterRemoves.Clear();
            IDownloadWaterRemove.ShowAllWaterRemove(Collection.WaterRemoves);
            IDownloadHouse.ShowHouse(Collection.Houses);
        }

        #endregion


        #region Методы
        protected override void SetProperty<T>(ref T fieldProperty, T newValue, [CallerMemberName] string propertyName = "")
        {
            base.SetProperty(ref fieldProperty, newValue, propertyName);
            if (propertyName == nameof(PiceWaterRemove) || propertyName == nameof(SummaryAmountWaterRremove))
            {
                CalculatedWaterRemove = Convert.ToDecimal(PiceWaterRemove) * Convert.ToDecimal(SummaryAmountWaterRremove);
            }
        }
        // Метод добавления "Водоотведения" в БД
        private void AddNewWaterRemove2DB(WaterRemove water)
        {
            string idHouse = water.IdHouse.ToString() + ", ";
            string dateWater = @"'" + water.DateWaterRemove + "', ";            
            string kub = @"'" + water.KubWaterRemove.ToString() + "', ";
            string price = @"'" + water.Price1KubWaterRemove.ToString() + "', ";
            string payed = @"'" + water.PayedWaterRemove.ToString() + "', ";
            string debt = @"'" + water.DebtWaterRemove.ToString() + "')";
            string sqlQueryNewWaterRemove = @"INSERT INTO water_disposal VALUES (NULL, " + idHouse + dateWater + kub + price + payed + debt;

            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdInsertNewHotWater = new(sqlQueryNewWaterRemove, connection.GetConnection());
            cmdInsertNewHotWater.ExecuteNonQuery();
            connection.CloseConnection();
        }

        //Заполнение объекта WaterRemove
        private void FillObjectWaterRemove(WaterRemove waterRemove)
        {
            waterRemove.IdHouse = SelectedHouse.IdHouse;
            waterRemove.DateWaterRemove = NewDateWaterRemove.ToLongDateString();
            waterRemove.KubWaterRemove = SummaryAmountWaterRremove;
            waterRemove.Price1KubWaterRemove = PiceWaterRemove;
            waterRemove.PayedWaterRemove = PaydWaterRemove;
            waterRemove.DebtWaterRemove = DebtWaterRemove;
        }
        #endregion
    }
}
