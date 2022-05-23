using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.Commands
{
    public class RelayCommand : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public RelayCommand(Action execute) : this(execute, null)
        { }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
            => _canExecute == null || _canExecute();

        public void Execute(object parameter)
        {
            _execute();
        }
    }

}
