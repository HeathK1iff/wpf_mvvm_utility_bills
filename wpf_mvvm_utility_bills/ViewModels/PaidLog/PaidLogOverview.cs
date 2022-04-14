using wpf_mvvm_utility_bills.Utils;
using wpf_mvvm_utility_bills.ViewModels.PaidLog.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.ViewModels.PaidLog
{
    
    class PaidLogOverview: ViewModelBase
    {
        public PaidLogOverview()
        {
            Append = new OpenPaidLogEditorCommand(param => IsReady, UpdateItem);

            Edit = new OpenPaidLogEditorCommand(param => IsReady && PaidLogSelectedItem != null, UpdateItem);

            Remove = new Utils.RoutedCommand((param) =>
            {
                return IsReady && param != null;
            }, param =>
            {
                if (param is Models.PaidLog selected)
                {
                    using (var context = new DataContext())
                    {
                        context.PaidLog.Remove(selected);
                        context.SaveChanges();
                    }
                    PaidLogTransactions.Remove(selected);
                }
            });

            LoadAsync();
        }


        public void UpdateItem(Models.PaidLog item, bool isNew)
        {
            BeginUpdate();
            try
            {
                using (var context = new DataContext())
                {

                    item.PaidValue = context.ValueLog.FirstOrDefault(f => f.Id == item.PaidValue.Id);

                    if (isNew)
                    {
                        context.PaidLog.Add(item);
                    }
                    else
                    {
                        context.PaidLog.Update(item);
                    }
                    context.SaveChanges();
                }

                if (isNew)
                {
                    PaidLogTransactions.Add(item);
                }

                PaidLogTransactions.NotifyItemChanged();
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
                    var list = await Task<List<Models.PaidLog>>.Run(() =>
                    {
                        using (var context = new DataContext())
                        {
                            return context.PaidLog.Include(f => f.PaidValue).ToList();
                        }
                    });

                    list.ForEach((transaction) => { PaidLogTransactions.Add(transaction); });
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

        public ObservableCollectionEx<Models.PaidLog> PaidLogTransactions { get; set; } = new();

        public Models.PaidLog PaidLogSelectedItem { get; set; }

        public ICommand Append { get; set; }

        public ICommand Edit { get; set; }

        public ICommand Remove { get; set; }

    }
}
