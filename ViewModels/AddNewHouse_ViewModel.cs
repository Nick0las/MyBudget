using MyBudget.Services;
using MyBudget.Services.Interfaces;
using MyBudget.ViewModels.Base;
using System;

namespace MyBudget.ViewModels
{
    internal class AddNewHouse_ViewModel : ViewModel_Base, IDownloadHouse
    {
        #region Заголовок окна Tittle
        private string _Title = "Новый дом....";
        /// <summary>Заголовок окна </summary>
        public string Title
        {
            get => _Title;
            set => Set(ref _Title, value);
        }
        #endregion

        #region Свойства для привязки к элементам окна
        private string _newSity;
        public string NewSity
        {
            get { return _newSity; }
            set
            {
                _newSity = value;
                OnPropertyChanged(nameof(NewSity));
            }
        }

        private string _newStreet;
        public string NewStreet
        {
            get { return _newStreet; }
            set
            {
                _newStreet = value;
                OnPropertyChanged(nameof(NewStreet));
            }
        }

        private string _newHouse;
        public string NewHouse
        {
            get { return _newHouse; }
            set
            {
                _newHouse = value;
                OnPropertyChanged(nameof(NewHouse));
            }
        }

        private int? _newAppartament = null;
        public int? NewAppartament
        {
            get { return _newAppartament; }
            set
            {
                _newAppartament = value;
                OnPropertyChanged(nameof(NewAppartament));
            }
        }
        #endregion

        #region Конструктор
        public AddNewHouse_ViewModel()
        {
            Collection.Houses.Clear();
            IDownloadHouse.ShowHouse(Collection.Houses);
        }
        #endregion
    }
}
