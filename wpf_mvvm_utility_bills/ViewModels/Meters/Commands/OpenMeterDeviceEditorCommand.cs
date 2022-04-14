using wpf_mvvm_utility_bills.Models;
using wpf_mvvm_utility_bills.Utils;
using wpf_mvvm_utility_bills.Views;
using System;
using System.Windows;

namespace wpf_mvvm_utility_bills.ViewModels.Meters.Commands
{
    delegate void MeterDeviceUpdateDelegate(MeterDevice item, bool isAppend);

    class OpenMeterDeviceEditorCommand : RoutedCommand
    {
        bool isNewItem = false;
        MeterDeviceUpdateDelegate _updateDel;

        public OpenMeterDeviceEditorCommand(Func<object, bool> canExecuteFunc, MeterDeviceUpdateDelegate updateDel) : base(canExecuteFunc, null)
        {
            _updateDel = updateDel;
        }

        protected override void DoExecute(object parameter)
        {
            MeterDevice selected = null;
            if (parameter is MeterDevice counter)
                selected = counter;

            isNewItem = (selected is null);

            if (isNewItem)
            {
                selected = new MeterDevice();
            }

            var form = new MeterEditorWindow();
            form.Owner = Application.Current.MainWindow;

            var dataContext = new MeterEditor();
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
