using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using wpf_mvvm_utility_bills.Utils;
using System.Collections.Generic;
using wpf_mvvm_utility_bills.ViewModels.ResourceType.Commands;
using System;

namespace wpf_mvvm_utility_bills.ViewModels.ResourceType
{
    internal class ResourceTypeOverview : ViewModelBase
    {
        public ResourceTypeOverview()
        {
            Remove = new Utils.RoutedCommand(parameter => parameter != null && IsReady
            , (parameter) =>
            {
                if (parameter is Models.ResourceType selected)
                {
                    BeginUpdate();
                    try
                    {
                        using (var context = new DataContext())
                        {
                            context.ResourceTypes.Remove(selected);
                            context.SaveChanges();
                        }
                        ResourceTypes.Remove(selected);
                    }
                    finally
                    {
                        EndUpdate();
                    }
                }
            });

            Append = new OpenResourceTypeEditorCommand(param => IsReady, UpdateItem);

            Edit = new OpenResourceTypeEditorCommand(param => IsReady && ResourceTypeSelectedItem != null, UpdateItem);

            LoadAsync();
        }


        public void UpdateItem(Models.ResourceType item, bool isNew)
        {
            BeginUpdate();
            try
            {
                using (var context = new DataContext())
                {
                    if (isNew)
                    {
                        context.ResourceTypes.Add(item);
                        ResourceTypes.Add(item);
                    }
                    else
                    {
                        context.ResourceTypes.Update(item);
                    }

                    context.SaveChanges();

                    ResourceTypes.NotifyItemChanged();
                }
            }
            finally
            {
                EndUpdate();
            }
        }


        public async void LoadAsync()
        {
            try
            {
                BeginUpdate();
                try
                {
                    var list = await Task.Run<List<wpf_mvvm_utility_bills.Models.ResourceType>>(() =>
                    {
                        using (var context = new DataContext())
                        {
                            return context.ResourceTypes.ToList();
                        }
                    });

                    list.ForEach(item => ResourceTypes.Add(item));
                }
                finally
                {
                    EndUpdate();
                }
            }
            catch (Exception e)
            {
                RaiseErrorMessage(e);
            }
        }

        public ObservableCollectionEx<Models.ResourceType> ResourceTypes { get; set; } = new ObservableCollectionEx<Models.ResourceType>();

        public Models.ResourceType ResourceTypeSelectedItem { get; set; }

        public ICommand Append { get; set; }

        public ICommand Edit { get; set; }

        public ICommand Remove { get; set; }
    }
}
