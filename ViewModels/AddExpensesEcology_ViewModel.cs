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
    class AddExpensesEcology_ViewModel : ViewModel_Base, IDownloadHouse, IDownload_AllBalance, IDownloadUserCard
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
        private decimal _debtColdWater;
        public decimal DebtColdWaterOnForms
        {
            get { return _debtColdWater; }
            set => Set(ref _debtColdWater, ToBePaid - PayedEcology);
        }

        // Выбор карты для списания оплаченных средств
        public CardHolder Card { get; set; }
        #endregion

        #region Команда добавления траты "Экология"
        // Команда
        #endregion

        #region Конструктор
        public AddExpensesEcology_ViewModel()
        {
            Collection.Houses.Clear();
            Collection.CardHolders.Clear();
            IDownloadUserCard.ShowCardUser(Collection.CardHolders);
            IDownloadHouse.ShowHouse(Collection.Houses);
        }
        #endregion


        #region Методы
        // Методы
        #endregion


    }
}
