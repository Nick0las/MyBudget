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
    internal class AddNewTypeTransfer_ViewModel : ViewModel_Base, IDownloadTypeTransfer
    {
        #region Заголовок окна Tittle
        private string _Title = "Новый тип поступления!)";
        /// <summary>Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства для привязки к элементам окна
        private string _typeTransfer;
        public string NameTypeTransfer
        {
            get { return _typeTransfer; }
            set
            {
                _typeTransfer = value;
                OnPropertyChanged(nameof(NameTypeTransfer));
            }
        }
        #endregion

        #region Команда добавления нового типа поступления
        public ICommand AddNewTypeTransferCmd { get; }
        private bool CanAddNewTypeTransferCmdExecute(object p) => true;
        private void OnAddNewTypeTransferCmdExecuted(object p)
        {
            if (NameTypeTransfer is null or "")
            {
                NameTypeTransfer = "";
                MessageBox.Show("Что-то пошло не так=(", "Ошибка!");
                return;
            }
            else
            {
                TypeTransfer type = new();
                type.TittleTypeTransfer = NameTypeTransfer;
                AddNewTypeTransfer(type);
                Collection.AllTypeTransfer.Clear();
                IDownloadTypeTransfer.ShowTypeTransfer(Collection.AllTypeTransfer);
                NameTypeTransfer = "";
                MessageBox.Show("Тип поступления добавлен!", "Успех!");
            }
        }

        #endregion

        #region Конструктор
        public AddNewTypeTransfer_ViewModel()
        {
            Collection.AllTypeTransfer.Clear();
            AddNewTypeTransferCmd = new LamdaCommand(OnAddNewTypeTransferCmdExecuted, CanAddNewTypeTransferCmdExecute);
            IDownloadTypeTransfer.ShowTypeTransfer(Collection.AllTypeTransfer);
        }
        #endregion

        #region Метод добавления нового типа поступления
        // Метод добавления нового типа поступления
        private void AddNewTypeTransfer(TypeTransfer typeTransfer)
        {
            string type = "'" + typeTransfer.TittleTypeTransfer + "'";
            string sqlAddNewType = "INSERT INTO type_transfer VALUES (null, " + type +")";
            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdInsertNewTypeTransfer = new SqliteCommand(sqlAddNewType, connection.GetConnection());
            cmdInsertNewTypeTransfer.ExecuteNonQuery();
            connection.CloseConnection();
        }
        #endregion
    }
}
