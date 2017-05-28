using System;
using System.Windows.Input;

namespace _360journeys
{
    class RelayCommand : ICommand
    {

        private Action<object> _execute; //Me dice qué metodo se va a ejecutar.
        private Func<object, bool> _canExecute; //Nos dirá si se puede o no ejecutar la llamada al método.

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("Falta el comando de ejecucion");
            else
            {
                this._execute = execute;
                this._canExecute = canExecute;
            }
        }

        //Si se puede ejecutar o no la llamada al método.
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested -= value;
            }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
