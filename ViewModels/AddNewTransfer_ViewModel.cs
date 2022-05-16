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
    internal class AddNewTransfer_ViewModel : ViewModel_Base, IDownloadUserCard, IDownloadTypeTransfer, IDownload_AllBalance
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

        // Привязка к DataGrid 
        private CardHolder _selectedDataGgridRow;        
        public CardHolder SelectedDataGridRow
        {
            get { return this._selectedDataGgridRow; }
            set
            {
                if(this._selectedDataGgridRow != value)
                {
                    this._selectedDataGgridRow = value;
                    OnPropertyChanged("SelectedDataGridRow");
                }
            }
        }
        //Привязка к TextBox Сумма поступления
        private string _summa;
        public string Summa
        {
            get { return this._summa; }
            set
            {
                if (_summa != value)
                {
                    _summa = value;
                    OnPropertyChanged("TextBoxName");
                }
            }
        }
        // Привязка к DatePicker
        private DateTime _dateTransfer = DateTime.Now;
        public DateTime DateTransfer
        {
            get { return _dateTransfer; }
            set
            {
                _dateTransfer = value;
                OnPropertyChanged(nameof(DateTransfer));
            }
        }
        #endregion

        #region Команда добавления нового поступления
        //Команда
        public ICommand AddNewTransferCmd { get; }
        private bool CanAddNewTransferCmdExecute(object p) => true;
        private void OnAddNewTransferCmdExecuted(object p)
        {            
            if (Summa is null or "")
            {
                MessageBox.Show("Все поля должны быть заполнены");
                return;
            }
            else
            {
                Transfer newTransfer = new();
                newTransfer.IdTypeTransfer = SelectedTypeTransfer.IdTypeTransfer;
                newTransfer.IdCard = SelectedDataGridRow.IdCard;
                newTransfer.Summa = Convert.ToDecimal(Summa);
                newTransfer.Date = DateTransfer.Date.ToLongDateString();
                AddNewTransfer(newTransfer);
                Collection.CardHolders.Clear();
                Collection.AllTypeTransfer.Clear();
                IDownloadUserCard.ShowCardUser(Collection.CardHolders);
                IDownloadTypeTransfer.ShowTypeTransfer(Collection.AllTypeTransfer);
                Collection.AllBalance.Clear();
                IDownload_AllBalance.ShowAllBalance(Collection.AllBalance);
                MessageBox.Show("Выполнено!", "Успех!");
            }
            
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

        #region Метод добавления нового поступления ₽ в БД
        private void AddNewTransfer(Transfer transfer)
        {
            int idTypeTransfer = transfer.IdTypeTransfer;
            int idCard = transfer.IdCard;
            decimal summa = transfer.Summa;
            string date = "'" + transfer.Date.ToString() + "'";
            string sqlQueryInsertTransfer =
                "INSERT INTO transfer VALUES (NULL, " + idTypeTransfer + "," + idCard + "," + summa + "," + date + ")";
            ConnectionDB connection = new ConnectionDB();
            connection.OpenConnection();
            SqliteCommand cmdInsertNewTransfer = new(sqlQueryInsertTransfer, connection.GetConnection());
            cmdInsertNewTransfer.ExecuteNonQuery();
            connection.CloseConnection();
        }
        #endregion
    }
}
