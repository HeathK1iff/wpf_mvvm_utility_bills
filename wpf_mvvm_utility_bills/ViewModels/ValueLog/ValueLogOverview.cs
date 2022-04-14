using wpf_mvvm_utility_bills.Utils;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace wpf_mvvm_utility_bills.ViewModels.ValueLog
{
    class ValueLogOverview : ViewModelBase
    {

        Models.ValueLog _selectedItem;
        int _selectedIndex;
        readonly bool _devicesDefenitionExists;
        public ValueLogOverview()
        {
            using (var context = new DataContext())
            {
                _devicesDefenitionExists = context.Meters.ToList().Count > 0;
            }

            Remove = new Utils.RoutedCommand((param) =>
            {
                return (SelectedItem != null) && (IsReady);
            }, (item) =>
            {
                using (var context = new DataContext())
                {
                    context.ValueLog.Remove(SelectedItem);
                    context.SaveChanges();
                }
                ValueLog.Remove(SelectedItem);
            });

            Save = new Utils.RoutedCommand((param) =>
            {
                return IsReady;
            }, (item) =>
            {
                ValueLog.ToList().ForEach(item =>
                    {
                        using (var context = new DataContext())
                        {
                            var existList = context.ValueLog.ToList();

                            var dbItem = existList.FirstOrDefault(f => f.Id == item.Id);

                            if (dbItem == null)
                            {
                                context.ValueLog.Add(item);
                                context.SaveChanges();
                                return;
                            }

                            dbItem.Comments = item.Comments;
                            dbItem.CurrentValue = item.CurrentValue;
                            dbItem.DateChecked = item.DateChecked;
                            dbItem.MeterDeviceId = item.MeterDeviceId;
                            context.SaveChanges();
                        }
                    });
            });

            Refresh = new Utils.RoutedCommand((param) =>
            {
                return IsReady;
            }, (item) =>
            {
                LoadAsync();
            });

            LoadAsync();
        }

        public async void LoadAsync()
        {
            try
            {
                BeginUpdate();

                MeterDevices.Clear();
                ValueLog.Clear();
                
                try
                {
                    var meterDevices = await Task.Run<List<Models.MeterDevice>>(() =>
                    {
                        using (var context = new DataContext())
                        {
                            return context.Meters.Include(f => f.ResourceType).ToList();
                        }
                    });

                    var valueLog = await Task.Run<List<Models.ValueLog>>(() =>
                    {
                        using (var context = new DataContext())
                        {
                            return context.ValueLog.Include(f => f.MeterDevice).ToList();
                        }
                    });

                    meterDevices.ForEach(item => MeterDevices.Add(new DeviceComboBoxItem(item)));
                    valueLog.ForEach(item => ValueLog.Add(item));
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

        public ObservableCollectionEx<Models.ValueLog> ValueLog { get; set; } = new();

        public ObservableCollectionEx<DeviceComboBoxItem> MeterDevices { get; set; } = new();


        public Models.ValueLog SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                DoPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                DoPropertyChanged();

                if (_selectedIndex == ValueLog.Count)
                {
                    _selectedItem = null;
                }
            }
        }

        public ICommand Save { get; set; }

        public ICommand Remove { get; set; }

        public ICommand Refresh { get; set; }

        public override bool IsReady => base.IsReady && _devicesDefenitionExists;
    }
}
