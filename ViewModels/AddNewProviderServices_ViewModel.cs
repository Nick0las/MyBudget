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
    internal class AddNewProviderServices_ViewModel : ViewModel_Base, IDownloadProviders
    {
        #region Заголовок окна Tittle
        private string _Title = "Новая услуга провайдера";
        /// <summary>Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства, привязываемые к окну
        // Свойство, для перехвата выбранного пользователем провайдера
        public Provider SelectedProvider { get; set; }
        
        // Привязка к TextBox
        private string _nameServices;
        public string NameServices
        {
            get { return _nameServices; }
            set
            {
                _nameServices = value;
                OnPropertyChanged(nameof(NameServices));
            }
        }
        #endregion

        #region Команда добавления новой услуги провайдера
        public ICommand AddNewServicesCmd { get; }
        private bool CanAddNewServicesCmdExecute(object p) => true;
        private void OnAddNewServicesCmdExecuted(object p)
        {
            if(NameServices is null or "" || SelectedProvider==null)
            {
                MessageBox.Show("Команда не выполнена! Проверьте правильность заполнения!");
                return;
            }
            else
            {
                ProviderServices newService = new();
                newService.IdProvider = SelectedProvider.IdProvider;
                newService.NameProviderServices = NameServices;
                AddNewServices(newService);
                Collection.Providers.Clear();
                Collection.ProviderJoinServices.Clear();
                IDownloadProviders.ShowAllProviders(Collection.Providers);
                IDownloadProviders.ShowProviderJoinServices(Collection.ProviderJoinServices);
                MessageBox.Show("Команда выполнена!");
                NameServices = "";
            }

        }
        #endregion



        #region Конструктор
        public AddNewProviderServices_ViewModel()
        {
            AddNewServicesCmd = new LamdaCommand(OnAddNewServicesCmdExecuted, CanAddNewServicesCmdExecute);
            Collection.Providers.Clear();
            Collection.ProviderJoinServices.Clear();
            IDownloadProviders.ShowAllProviders(Collection.Providers);
            IDownloadProviders.ShowProviderJoinServices(Collection.ProviderJoinServices);
        }

        #endregion

        #region Метод - добавление новой услуги провайдера
        private static void AddNewServices(ProviderServices service)
        {
            string nameService = ", '" + service.NameProviderServices + "')";
            string sqlInsertQuery = "INSERT INTO services_provider VALUES (NULL, " + service.IdProvider + nameService;
            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdInsertNewService = new(sqlInsertQuery, connection.GetConnection());
            cmdInsertNewService.ExecuteNonQuery();
            connection.CloseConnection();
        }

        #endregion
    }
}
