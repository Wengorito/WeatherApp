using System;
using System.Windows.Input;

namespace WeatherApp.ViewModels.Commands
{
    public class SearchCommand : ICommand
    {
        private WeatherVM _vM;

        public SearchCommand(WeatherVM vM)
        {
            _vM = vM;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }


        public bool CanExecute(object parameter)
        {
            string query = parameter as string;

            if (string.IsNullOrWhiteSpace(query))
                return false;

            return true;
        }

        public void Execute(object parameter)
        {
            _vM.MakeQuery();
        }
    }
}
