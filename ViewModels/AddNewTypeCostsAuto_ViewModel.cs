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
    internal class AddNewTypeCostsAuto_ViewModel : ViewModel_Base, IDownloadTypeCostsAuto
    {
        #region Заголовок окна
        private string _Title = "Новый тип траты на авто";
        /// <summary>Заголовок окна </summary>

        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства, привязываемые к окну
        private string _typeCosts;
        public string TypeCosts
        {
            get { return _typeCosts; }
            set
            {
                _typeCosts = value;
                OnPropertyChanged(nameof(TypeCosts));
            }
        }
        #endregion

        #region Команда добавления нового типа траты(авто)
        public ICommand AddNewTypeCostsAutoCmd { get; }
        private bool CanAddNewTypeCostsAutoCmdExecute(object p) => true;
        private void OnAddNewTypeCostsAutoCmdExecuted(object p)
        {
            if(TypeCosts is null or "")
            {
                MessageBox.Show("Текстовое поле должно быть заполнено!", "Ошибка!");
                return;
            }
            else
            {
                TypeCostsAuto newType = new();
                newType.NameTypeCostsAuto = TypeCosts;
                AddNewTypeCostsAuto(newType);
                Collection.TypesCostsAuto.Clear();
                IDownloadTypeCostsAuto.ShowTypeCostsAuto(Collection.TypesCostsAuto);
            }
        }

        #endregion

        #region Конструктор
        public AddNewTypeCostsAuto_ViewModel()
        {
            AddNewTypeCostsAutoCmd = new LamdaCommand(OnAddNewTypeCostsAutoCmdExecuted, CanAddNewTypeCostsAutoCmdExecute);
            Collection.TypesCostsAuto.Clear();
            IDownloadTypeCostsAuto.ShowTypeCostsAuto(Collection.TypesCostsAuto);
        }

        #endregion

        #region Метод добавления нового типа трат(авто) в БД
        private void AddNewTypeCostsAuto(TypeCostsAuto typeCosts)
        {
            string type = "'" + typeCosts.NameTypeCostsAuto + "')";
            string sqlQueryInsertTypeCostsAuto = "INSERT INTO type_costs_auto VALUES (NULL, " + type;
            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdInsertNewTypeCostsAuto = new SqliteCommand(sqlQueryInsertTypeCostsAuto, connection.GetConnection());
            cmdInsertNewTypeCostsAuto.ExecuteNonQuery();
            connection.CloseConnection();
        }

        #endregion
    }
}
