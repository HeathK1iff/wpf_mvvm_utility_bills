
using wpf_mvvm_utility_bills.Models;
using wpf_mvvm_utility_bills.Utils;
using wpf_mvvm_utility_bills.Views.Pages;
using System.Windows.Controls;

namespace wpf_mvvm_utility_bills.ViewModels.Commands
{
    class OpenSuppliersOverivewCommand : wpf_mvvm_utility_bills.Utils.RoutedCommand
    {
        protected override void DoExecute(object parameter)
        {
            if (parameter is Frame overviewFrame)
            {
                overviewFrame.Content = new SuppliersOverviewPage();
            }
        }
    }
}
