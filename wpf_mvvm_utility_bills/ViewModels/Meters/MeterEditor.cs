using wpf_mvvm_utility_bills.Models;
using wpf_mvvm_utility_bills.Utils;
using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.ViewModels.Meters
{
    class MeterEditor : ViewModelBase
    {
        Models.ResourceType _resourceType;
        string _serial;
        DateTime _dateIssued;
        DateTime _dateExpired;

        public MeterEditor()
        {
            Save = new Utils.RoutedCommand(item => IsReady && (ResourceType != null), null);

            using (var context = new DataContext())
            {
                context.ResourceTypes
                    .ToList()
                    .ForEach(f => ResourceTypes.Add(f));
            }
        }

        public void Fill(MeterDevice item)
        {
            if (item.ResourceType != null)
            {
                ResourceType = ResourceTypes.FirstOrDefault(f => f.Id == item.ResourceType.Id);
            }

            Serial = item.Serial;
            DateIssued = item.DateIssued;
            DateExpired = item.DateExpired;
        }

        public void Post(MeterDevice item)
        {
            item.ResourceType = ResourceType;
            item.Serial = Serial;
            item.DateIssued = DateIssued;
            item.DateExpired = DateExpired;
        }


        public ObservableCollection<Models.ResourceType> ResourceTypes { get; set; } = new ObservableCollection<Models.ResourceType>();

        public Models.ResourceType ResourceType
        {
            get => _resourceType;
            set
            {
                _resourceType = value;
                DoPropertyChanged();
            }
        }

        public string Serial
        {
            get => _serial;
            set
            {
                _serial = value;
                DoPropertyChanged();
            }
        }

        public DateTime DateIssued
        {
            get => _dateIssued;
            set
            {
                _dateIssued = value;
                DoPropertyChanged();
            }
        }

        public DateTime DateExpired {
            get => _dateExpired;
            set
            {
                _dateExpired = value;
                DoPropertyChanged();
            }
        }

        public ICommand Save { get; set; }

        public ICommand Cancel { get; set; } = new Utils.RoutedCommand();

    }
}
