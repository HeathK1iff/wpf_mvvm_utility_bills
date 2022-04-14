using wpf_mvvm_utility_bills.Utils;
using wpf_mvvm_utility_bills.Views.Windows;
using System.Windows;

namespace wpf_mvvm_utility_bills
{
    public partial class App : Application
    {
        DataContext _dataContext;
        private void OnStartup(object sender, StartupEventArgs e)
        {
            _dataContext = new DataContext();
            var view = new MainWindow();
            view.Show();
        }

        public DataContext Context { get => _dataContext; }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(e.Exception.Message, "Application Error", MessageBoxButton.OK, MessageBoxImage.Error);
            e.Handled = true;
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
            _dataContext.Dispose();
        }
    }
}
