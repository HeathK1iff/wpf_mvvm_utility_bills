using wpf_mvvm_utility_bills.Utils;
using wpf_mvvm_utility_bills.Views.Pages;
using System.Windows.Controls;

namespace wpf_mvvm_utility_bills.ViewModels.Commands
{
    internal class OpenResourceOverviewCommand : RoutedCommand
    {
        protected override void DoExecute(object parameter)
        {
            if (parameter is Frame overviewFrame)
            {
                overviewFrame.Content = new ResourceTypeOverviewPage();
            }
        }
    }
}
