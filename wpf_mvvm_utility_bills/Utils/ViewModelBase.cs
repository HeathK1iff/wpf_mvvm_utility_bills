using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.Utils
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        bool _isReady = true;

        public event Action<bool> OnUpdating;

        protected void BeginUpdate()
        {
            _isReady = false;
            try
            {
                OnUpdating?.Invoke(_isReady);
            } 
            finally
            {
                DoPropertyChanged("IsReady");
            }
        }

        protected void EndUpdate()
        {
            _isReady = true;
            try
            {
                OnUpdating?.Invoke(_isReady);
            } 
            finally
            {
                DoPropertyChanged("IsReady");
                CommandManager.InvalidateRequerySuggested();
            }
        }

        protected void RaiseErrorMessage(Exception e)
        {
            _isReady = false;
            MessageBox.Show(e.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public virtual bool IsReady { get => _isReady; }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void DoPropertyChanged([CallerMemberName] string prop = null)
        {
            PropertyChanged?.Invoke(prop, new PropertyChangedEventArgs(prop));
        }
    }
}
