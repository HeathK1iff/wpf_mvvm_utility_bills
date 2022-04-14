using System;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.Utils
{
    public class RoutedCommand : ICommand, ICommandEvents
    {
        Func<object, bool> _canExecuteFunc;
        Action<object> _executeAction;  

        public RoutedCommand(Func<object, bool> canExecuteFunc, Action<object> executeAction) : base ()
        {
            _canExecuteFunc = canExecuteFunc;
            _executeAction = executeAction;
        }

        public RoutedCommand() { }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }
        public event Action OnSuccess;
        public event Action<Exception> OnError;

        protected virtual void DoExecute(object parameter)
        {
            _executeAction?.Invoke(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteFunc?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            try
            {
                DoExecute(parameter);
                OnSuccess?.Invoke();
            }
            catch (Exception e)
            {
                if (OnError != null)
                {
                    OnError?.Invoke(e);
                }
                else
                {
                    throw;
                }  
            }
        }
    }
}
