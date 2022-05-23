using MyBudget.Commands.Base;
using MyBudget.Views.Windows;
using System;
using System.Windows;

namespace MyBudget.Commands
{
    class OpenNewElectrocityCommand : Command
    {
        private Window1_AddExpensesElectricity _Window;
        public override bool CanExecute(object parameter) => _Window == null;

        public override void Execute(object parameter)
        {
            Window1_AddExpensesElectricity window = new Window1_AddExpensesElectricity
            {
                Owner = Application.Current.MainWindow
            };
            _Window = window;
            window.Closed += OnWindowClosed;
            window.ShowDialog();
        }

        private void OnWindowClosed(object Sender, EventArgs E)
        {
            ((Window)Sender).Closed -= OnWindowClosed;
            _Window = null;
        }
    }
}
