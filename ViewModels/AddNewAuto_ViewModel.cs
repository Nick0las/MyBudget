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
    internal class AddNewAuto_ViewModel : ViewModel_Base
    {
        #region Заголовок окна
        private string _Title = "Новое авто";
        /// <summary>Заголовок окна </summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства, привязываемые к окну
        // Название авто (привязка к textBox)
        private string _nameAuto;
        public string NameAuto
        {
            get { return _nameAuto; }
            set
            {
                _nameAuto = value;
                OnPropertyChanged(nameof(NameAuto));
            }
        }
        #endregion

        #region Команда добавления нового авто в БД
        public ICommand AddNewAutoCmd { get; }
        private bool CanAddNewAutoCmdExecute(object p) => true;
        private void OnAddNewAutoCmdExecuted(object p)
        {
            MessageBox.Show("Команда привязана!", "Проверка привязки команды.");
        }
        #endregion

        #region Конструктор
        public AddNewAuto_ViewModel()
        {
            AddNewAutoCmd = new LamdaCommand(OnAddNewAutoCmdExecuted, CanAddNewAutoCmdExecute);
        }
        #endregion
    }
}
