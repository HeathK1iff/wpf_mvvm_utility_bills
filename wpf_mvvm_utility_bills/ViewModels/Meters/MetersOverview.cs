using wpf_mvvm_utility_bills.Models;
using wpf_mvvm_utility_bills.Utils;
using wpf_mvvm_utility_bills.ViewModels.Meters.Commands;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.ViewModels.Meters
{
    class MetersOverview : ViewModelBase
    {
        MeterDevice _selectedItem;
        public MetersOverview()
        {
            MeterDevices = new ObservableCollectionEx<MeterDevice>();

            Append = new OpenMeterDeviceEditorCommand((parameter) =>
            {
                return IsReady;
            }, EditItem);

            Edit = new OpenMeterDeviceEditorCommand((parameter) =>
            {
                return IsReady && (parameter != null);
            }, EditItem);

            Remove = new Utils.RoutedCommand((parameter) =>
            {
                return IsReady && (parameter != null);
            }, async (parameter) =>
            {
                if (parameter is MeterDevice selected)
                {
                    using (var context = new DataContext())
                    {
                        context.Meters.Remove(selected);
                        await context.SaveChangesAsync();
                    }
                    MeterDevices.Remove(selected);
                }
            });

            _ = LoadListAsync();
        }

        public async Task LoadListAsync()
        {
            try
            {
                BeginUpdate();
                try
                {
                    var list = await Task<List<MeterDevice>>.Run(() =>
                    {
                        using (var context = new DataContext())
                        {
                            return context.Meters.Include(f => f.ResourceType).ToList();
                        }
                    });
                    list.ForEach((device) => { MeterDevices.Add(device); });
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


        private void EditItem(MeterDevice device, bool isNew)
        {
            BeginUpdate();
            try
            {
                using (var context = new DataContext())
                {
                    device.ResourceType = context.ResourceTypes.FirstOrDefault(f => f.Id == device.ResourceType.Id);

                    if (isNew)
                    {
                        context.Meters.Add(device);
                    }
                    else
                    {
                        context.Meters.Update(device);
                    }
                    context.SaveChanges();
                }

                if (isNew)
                {
                    MeterDevices.Add(device);
                }

                MeterDevices.NotifyItemChanged();
            }
            finally
            {
                EndUpdate();
            }
        }

        public MeterDevice SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                DoPropertyChanged();
            }
        }

        public ObservableCollectionEx<MeterDevice> MeterDevices { get; set; }

        public ICommand Append { get; set; }

        public ICommand Edit { get; set; }

        public ICommand Remove { get; set; }

    }
}
