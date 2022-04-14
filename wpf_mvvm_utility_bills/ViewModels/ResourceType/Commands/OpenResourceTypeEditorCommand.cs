using wpf_mvvm_utility_bills.Utils;
using System.Linq;
using wpf_mvvm_utility_bills.Views.Windows;
using System.Windows;
using System;

namespace wpf_mvvm_utility_bills.ViewModels.ResourceType.Commands
{
    delegate void ResourceTypeEditorDelegate(Models.ResourceType item, bool isAppend);

    class OpenResourceTypeEditorCommand : RoutedCommand
    {
        ResourceTypeEditorDelegate _updateDel;
        public OpenResourceTypeEditorCommand(Func<object,bool> canExecuteFunc, ResourceTypeEditorDelegate updateDel) : base(canExecuteFunc, null)
        {
            _updateDel = updateDel;
        }

        protected override void DoExecute(object parameter)
        {

            Models.ResourceType selected = null;

            if (parameter is Models.ResourceType paramResource)
            {
                selected = paramResource;
            } 
            else
            {
                selected = new Models.ResourceType();
            }


            bool isNewItem = parameter == null;

            var window = new ResourceTypeEditorWindow();
            window.Owner = App.Current.Windows.OfType<Window>().FirstOrDefault(f => f.IsActive);
            var context = new ResourceTypeEditor();
            context.Fill(selected);

            if (context.Save is ICommandEvents saveEvents)
            {
                saveEvents.OnSuccess += () =>
                {
                    window.Close();
                    context.Post(selected);
                    _updateDel?.Invoke(selected, isNewItem);
                };
            }

            if (context.Cancel is ICommandEvents cancelEvents)
            {
                cancelEvents.OnSuccess += () =>
                {
                    window.Close();
                };
            }

            window.DataContext = context;
            window.ShowDialog();
        }
    }
}
