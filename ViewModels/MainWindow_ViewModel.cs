using MyBudget.Commands;
using MyBudget.Models;
using MyBudget.Services;
using MyBudget.Services.Interfaces;
using MyBudget.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace MyBudget.ViewModels
{
    internal class MainWindow_ViewModel : ViewModel_Base, IDownload_AllBalance, IDownloadUserCard
    {
        #region заголовок окна
        private string _Title = "Мой Бюджет v1.0";
        /// <summary>Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        public ObservableCollection<Cash> Kassa => Collection.AllBalance;

        #region команда закрытия приложения 
        public ICommand CloseAppCmd { get; }
        private bool CanCloseAppCmdExecute(object p) => true;
        private void OnCloseAppCmdExecuted(object p)
        {
            Application.Current.Shutdown();
        }
        #endregion      

        #region Конструктор MainWindowViewModel
        public MainWindow_ViewModel()
        {
            CloseAppCmd = new LamdaCommand
                (OnCloseAppCmdExecuted, CanCloseAppCmdExecute);
            IDownload_AllBalance.ShowAllBalance(Collection.AllBalance);
            IDownloadUserCard.LoadAllCardsMainWindow(Collection.Cards);
        }
        #endregion
        
    }
}
