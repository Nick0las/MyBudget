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
    class AddExpensesColdWater_ViewModel : ViewModel_Base, IDownloadHouse, IDownloadColdWater
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
        public House SelectedHouse { get; set; }

        private ColdWater selectedDataGridColdWater;
        public ColdWater SelectedDataGrid
        {
            get { return selectedDataGridColdWater; }
            set
            {
                if (selectedDataGridColdWater != value)
                {
                    selectedDataGridColdWater = value;
                    OnPropertyChanged("SelectedDataGrid");
                }
            }
        }
        
        public int NewMetter
        {
            get;
            set;
        }
        public int ExpenKub
        {
            get;
            set;
        }

        #endregion

        public AddExpensesColdWater_ViewModel()
        {
            Collection.ColdWaters.Clear();
            Collection.Houses.Clear();
            IDownloadHouse.ShowHouse(Collection.Houses);
            IDownloadColdWater.ShowAllColdWater(Collection.ColdWaters);
        }
    }
}
