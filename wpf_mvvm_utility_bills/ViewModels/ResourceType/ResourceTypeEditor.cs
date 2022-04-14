using wpf_mvvm_utility_bills.Utils;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.ViewModels.ResourceType
{
    class ResourceTypeEditor: ViewModelBase
    {
        string _name;
        string _meassurement;


        public ResourceTypeEditor()
        {
            Save = new Utils.RoutedCommand(item => !string.IsNullOrEmpty(Name), null);
        }

        public string Name
        {
            get
            {
                return _name;
            }

            set
            {
                _name = value;
                DoPropertyChanged();
            }
        }

        public string Meassurement 
        {
            get
            {
                return _meassurement;
            }
            
            set
            {
                _meassurement = value;
                DoPropertyChanged();
            }
        }

        public void Fill(Models.ResourceType item)
        {
            this.Name = item.Name;
            this.Meassurement = item.Measurement;
        }

        public void Post(Models.ResourceType item)
        {
            item.Measurement = _meassurement;
            item.Name = _name;
        }

        public ICommand Save { get; set; }

        public ICommand Cancel { get; set; } = new Utils.RoutedCommand();
    }
}
