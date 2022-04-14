using wpf_mvvm_utility_bills.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.ViewModels.PaidLog
{

    class PaidLogEditor : Utils.ViewModelBase
    {
        DeviceComboBoxItem _selectedDeviceItem;
        Models.ValueLog _selectedLogTransaction;
        DateTime _paidDate = DateTime.Now;
        double _amount;

        public PaidLogEditor()
        {
            Save = new Utils.RoutedCommand(item => IsReady && (SelectedLogTransaction != null), null);
            Load();
        }

        public void Load()
        {
            try
            {
                BeginUpdate();
                try
                {
                    using (var context = new DataContext())
                    {
                        var devices = context.Meters.Include(f => f.ResourceType).ToList();
                        devices.ForEach((device) => { Devices.Add(new DeviceComboBoxItem(device)); });
                    }
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

        public void Fill(Models.PaidLog item)
        {
            PaidDate = item.PaidDate;
            Amount = item.Amount;
            if (item.PaidValue != null)
            {
                SelectedDeviceItem = Devices.ToList().FirstOrDefault(f => f.Id == item.PaidValue.MeterDeviceId);
                SelectedLogTransaction = LogTransactions.ToList().FirstOrDefault(f => f.Id == item.PaidValue.Id);
            }
        }

        public void Post(Models.PaidLog item)
        {
            if (SelectedLogTransaction is null)
                throw new Exception("Current Value is mandatory field.");

            item.PaidDate = PaidDate;
            item.Amount = Amount;
            item.PaidValue = SelectedLogTransaction;
        }

        public List<DeviceComboBoxItem> Devices { get; set; } = new List<DeviceComboBoxItem>();

        public List<Models.ValueLog> LogTransactions { get; set; } = new List<Models.ValueLog>();

        public DeviceComboBoxItem SelectedDeviceItem
        {
            get => _selectedDeviceItem;
            set
            {
                _selectedDeviceItem = value;

                using (var context = new DataContext())
                {
                    var list = context.ValueLog.Where(f => f.MeterDeviceId == _selectedDeviceItem.Id).ToList();
                    list.ForEach(item =>
                    {
                        LogTransactions.Add(item);
                    });


                    DoPropertyChanged("LogTransactions");
                }

                DoPropertyChanged();
            }
        }

        public Models.ValueLog SelectedLogTransaction
        {
            get => _selectedLogTransaction;
            set
            {
                _selectedLogTransaction = value;
                DoPropertyChanged();
            }
        }

        public DateTime PaidDate
        {
            get => _paidDate;
            set
            {
                _paidDate = value;
                DoPropertyChanged();
            }
        }

        public double Amount
        {
            get => _amount;
            set
            {
                _amount = value;
                DoPropertyChanged();
            }
        }


        public ICommand Save { get; set; }

        public ICommand Cancel { get; set; } = new Utils.RoutedCommand();
    }
}
