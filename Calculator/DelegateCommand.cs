using System;
using System.Windows.Input;
namespace Calculator
{
    public class DelegateCommand : ICommand
    {
        private readonly Action _command;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action command, Func<bool> canExecute = null)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            _canExecute = canExecute;
            _command = command;
        }

        public void Execute(object parameter)
        {
            _command();
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }
    }

    public class DelegateCommand<T> : ICommand
    {
        private readonly Action<T> _command;
        private readonly Func<bool> _canExecute;
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public DelegateCommand(Action<T> command, Func<bool> canExecute = null)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            _canExecute = canExecute;
            _command = command;
        }

        public void Execute(object parameter)
        {
            _command((T)parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

    }
}
