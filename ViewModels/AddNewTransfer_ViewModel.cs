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
    internal class AddNewTransfer_ViewModel : ViewModel_Base
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
    }
}
