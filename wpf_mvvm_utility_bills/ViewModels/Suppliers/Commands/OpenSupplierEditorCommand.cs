using wpf_mvvm_utility_bills.Models;
using wpf_mvvm_utility_bills.Utils;
using wpf_mvvm_utility_bills.Views.Windows;
using System;
using System.Linq;
using System.Windows;

namespace wpf_mvvm_utility_bills.ViewModels.Suppliers.Commands
{
    delegate void SupplierUpdateDelegate(Supplier item, bool isAppend);

    class OpenSupplierEditorCommand : RoutedCommand
    {
        bool isNewItem = false;
        SupplierUpdateDelegate _updateDel;
        public OpenSupplierEditorCommand(Func<object, bool> canExecuteFunc, SupplierUpdateDelegate updateDel) : base(canExecuteFunc, null)
        {
            _updateDel = updateDel;
        }

        protected override void DoExecute(object parameter)
        {
            Supplier selected = null;
            if (parameter is Supplier supplier)
                selected = supplier;

            isNewItem = (selected is null);

            if (isNewItem)
            {
                selected = new Supplier();
            }

            var form = new SupplierEditorWindow();
            form.Owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);

            var dataContext = new SupplierEditor();
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
