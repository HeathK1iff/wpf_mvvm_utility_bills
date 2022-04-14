using System.Windows;
using System.Windows.Input;
using System.Linq;

namespace wpf_mvvm_utility_bills.ViewModels.Commands
{
    static class WindowCommands {
        public static ICommand CloseActiveForm { get; set; } = new wpf_mvvm_utility_bills.Utils.RoutedCommand(null, (parameter) =>
        {
            var activeForm = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            if (activeForm != null)
                activeForm.Close();
        });

    }
}
