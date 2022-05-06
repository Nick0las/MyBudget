using MyBudget.Commands;
using MyBudget.Services.Interfaces;
using MyBudget.ViewModels.Base;
using MyBudget.Services;
using System;
using System.Windows;
using System.Windows.Input;
using MyBudget.Models;
using MyBudget.Data;
using Microsoft.Data.Sqlite;

namespace MyBudget.ViewModels
{
    internal class AddNewProvider_ViewModel : ViewModel_Base, IDownloadProviders
    {
        #region Заголовок окна Tittle
        private string _Title = "Новый провайдер в доме....";
        /// <summary>Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства, привязываемые к элементам окна
        private string _nameProvider;
        public string NameProvider
        {
            get { return _nameProvider; }
            set
            {
                _nameProvider = value;
                OnPropertyChanged(nameof(NameProvider));
            }
        }
        #endregion

        #region Команда добавления нового провайдера
        public ICommand AddNewProviderCmd { get; }
        private bool CanAddNewProviderCmdExecute(object p) => true;
        private void OnAddNewProviderCmdExecuted(object p)
        {
            if(NameProvider is null or "")
            {
                MessageBox.Show("Команда не выполнена. Проверьте правильность ввода.", "Ошибка!");
                return;
            }
            else
            {
                Provider provider = new();
                provider.NameProvider = NameProvider;
                AddNewProvider(provider);
                Collection.Providers.Clear();
                IDownloadProviders.ShowAllProviders(Collection.Providers);
                //MessageBox.Show("Команда выполнена","Успех!=)");
                NameProvider = "";
            }
        }
        #endregion


        #region Конструктор
        public AddNewProvider_ViewModel()
        {
            AddNewProviderCmd = new LamdaCommand(OnAddNewProviderCmdExecuted, CanAddNewProviderCmdExecute);
            Collection.Providers.Clear();
            IDownloadProviders.ShowAllProviders(Collection.Providers);
        }
        #endregion

        #region Метод добавления нового провайдера
        private void AddNewProvider(Provider p)
        {
            string nameProv = "'" + p.NameProvider + "')";
            string sqlQueryAddNewProvider = "INSERT INTO provider VALUES (null, " + nameProv;
            ConnectionDB connection = new();
            connection.OpenConnection();
            SqliteCommand cmdInsertNewProvider = new(sqlQueryAddNewProvider, connection.GetConnection());
            cmdInsertNewProvider.ExecuteNonQuery();
            connection.CloseConnection();
        }
        #endregion
    }
}
