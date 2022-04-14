using wpf_mvvm_utility_bills.Models;
using wpf_mvvm_utility_bills.Utils;
using wpf_mvvm_utility_bills.ViewModels.Suppliers.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.ViewModels.Suppliers
{
    class SuppliersOverview : ViewModelBase
    {
        Supplier _selectedItem;

        public SuppliersOverview()
        {

            Append = new OpenSupplierEditorCommand(param => IsReady, UpdateItem);
            Edit = new OpenSupplierEditorCommand(param => IsReady && SupplierSelectedItem != null, UpdateItem);

            Remove = new Utils.RoutedCommand(param => param != null && IsReady, param =>
            {
                BeginUpdate();
                try
                {
                    if (param is Supplier selected)
                    {
                        using (var context = new DataContext())
                        {
                            context.Suppliers.Remove(selected);
                            context.SaveChanges();
                        }
                        Suppliers.Remove(selected);
                    }
                }
                finally
                {
                    EndUpdate();
                }

            });

           LoadAsync();
        }

        public async void LoadAsync()
        {
            try
            {
                BeginUpdate();
                try
                {
                    var list = await Task<ObservableCollection<Supplier>>.Run(() =>
                    {
                        using (var context = new DataContext())
                        {
                            return context.Suppliers.ToList();
                        }
                    });
                    list.ForEach(supplier => Suppliers.Add(supplier));
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


        public void UpdateItem(Supplier item, bool isNew)
        {
            BeginUpdate();
            try
            {
                using (var context = new DataContext())
                {
                    if (isNew)
                    {
                        context.Suppliers.Add(item);
                    }
                    else
                    {
                        context.Suppliers.Update(item);
                    }

                    context.SaveChanges();
                }

                if (isNew)
                {
                    Suppliers.Add(item);
                }

                Suppliers.NotifyItemChanged();
            }
            finally
            {
                EndUpdate();
            }
        }

        public Supplier SupplierSelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                DoPropertyChanged();
            }
        }

        public ObservableCollectionEx<Supplier> Suppliers { get; set; } = new ObservableCollectionEx<Supplier>();

        public ICommand Append { get; set; }

        public ICommand Edit { get; set; }

        public ICommand Remove { get; set; }
    }
}
