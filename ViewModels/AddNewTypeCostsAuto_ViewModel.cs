using MyBudget.ViewModels.Base;
using System;

namespace MyBudget.ViewModels
{
    internal class AddNewTypeCostsAuto_ViewModel : ViewModel_Base
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
    }
}
