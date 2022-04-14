using wpf_mvvm_utility_bills.Utils;
using wpf_mvvm_utility_bills.Views.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace wpf_mvvm_utility_bills.ViewModels.PaidLog.Commands
{
    delegate void OpenPaidLogUpdateDelegate(Models.PaidLog item, bool isNew);
    class OpenPaidLogEditorCommand : Utils.RoutedCommand
    {
        OpenPaidLogUpdateDelegate _updateDel;
        public OpenPaidLogEditorCommand(Func<object, bool> canExecuteFunc, OpenPaidLogUpdateDelegate updateDel) : base(canExecuteFunc, null)
        {
            _updateDel = updateDel;
        }

        protected override void DoExecute(object parameter)
        {
            Models.PaidLog selected = null;
            if (parameter is Models.PaidLog itemLog)
            {
                selected = itemLog;
            }

            bool isNewItem = parameter == null;

            if (isNewItem)
                selected = new Models.PaidLog();

            var form = new PaidLogEditorWindow();
            form.Owner = App.Current.Windows.OfType<Window>().FirstOrDefault(f => f.IsActive);

            var dataContext = new PaidLogEditor();
            dataContext.Fill(selected);

            if (dataContext.Cancel is ICommandEvents events)
            {
                events.OnSuccess += () =>
                {
                    form.Close();
                };
            }

            if (dataContext.Save is ICommandEvents events1)
            {
                events1.OnSuccess += () =>
                {
                    form.Close();
                    dataContext.Post(selected);
                    _updateDel?.Invoke(selected, isNewItem);
                };
            }

            form.DataContext = dataContext;

            form.ShowDialog();
        }
    }
}
