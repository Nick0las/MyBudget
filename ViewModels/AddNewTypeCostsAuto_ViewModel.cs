using MyBudget.Commands;
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
            MessageBox.Show("Проверка команды.");
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
    }
}
