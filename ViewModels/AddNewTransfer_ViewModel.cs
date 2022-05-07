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
    internal class AddNewTransfer_ViewModel : ViewModel_Base, IDownloadUserCard, IDownloadTypeTransfer
    {
        #region Заголовок окна
        private string _Title = "Новое поступление ₽";
        /// <summary>Заголовок окна </summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства, привязываемые к окну
        // Свойство для ComboBox (выбор типа поступления)
        public TypeTransfer SelectedTypeTransfer { get; set; }


        #endregion

        #region Команда добавления нового поступления
        //Команда
        public ICommand AddNewTransferCmd { get; }
        private bool CanAddNewTransferCmdExecute(object p) => true;
        private void OnAddNewTransferCmdExecuted(object p)
        {
            //Команда
            MessageBox.Show("Привязка команды");
        }
        #endregion

        #region Конструктор
        public AddNewTransfer_ViewModel()
        {
            AddNewTransferCmd = new LamdaCommand(OnAddNewTransferCmdExecuted, CanAddNewTransferCmdExecute);
            Collection.CardHolders.Clear();
            Collection.AllTypeTransfer.Clear();
            IDownloadUserCard.ShowCardUser(Collection.CardHolders);
            IDownloadTypeTransfer.ShowTypeTransfer(Collection.AllTypeTransfer);
        }
        #endregion
    }
}
