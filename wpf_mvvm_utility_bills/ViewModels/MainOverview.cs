using wpf_mvvm_utility_bills.Utils;
using wpf_mvvm_utility_bills.ViewModels.Commands;
using System.Windows;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.ViewModels
{
    public class MainOverview : ViewModelBase
    {
        public MainOverview()
        {
            CloseApplication = new Utils.RoutedCommand(null, (obj) =>
            {
                Application.Current.MainWindow.Close();
            });
        }

        public ICommand OpenValueLogOverview { get; set; } = new OpenValueLogOverviewCommand();
        
        public ICommand OpenPaidLogOverview { get; set; } = new OpenPaidLogOverviewCommand();
        
        public ICommand OpenMetersOverview { get; set; } = new OpenMetersOverviewCommand();
        
        public ICommand OpenSuppliersOverview { get; set; } = new OpenSuppliersOverivewCommand();

        public ICommand OpenResourceTypeOverview { get; set; } = new OpenResourceOverviewCommand();
        
        public ICommand CloseApplication { get; set; }
    }
}
