using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ProjektSemestralnyOOP.Commands
{
    /// <summary>
    /// Class that can expose a method or delegate to the view. These types act as a way to bind commands between the viewmodel and UI elements.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private Action _execute;
        private Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute"></param>
        public RelayCommand(Action execute) : this(execute, null)
        { }

        /// <summary>
        /// Initializes a new instance of <see cref="RelayCommand"/>.
        /// </summary>
        /// <param name="execute"><see cref="Action"/> delegate for command logic.</param>
        /// <param name="canExecute">Function for defining whether the command can or cannot be executed.</param>
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
