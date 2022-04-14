using wpf_mvvm_utility_bills.Models;
using wpf_mvvm_utility_bills.Utils;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.ViewModels.Suppliers
{
    class SupplierEditor : ViewModelBase
    {
        public SupplierEditor()
        {
            Save = new Utils.RoutedCommand(item=> !string.IsNullOrEmpty(CompanyName), null);
        }

        public void Fill(Supplier item) 
        {
            CompanyName = item.Name;
            Address = item.Address;
            EMail = item.EMail;
        }

        public void Post(Supplier item)
        {
            item.Name = CompanyName;
            item.Address = Address;
            item.EMail = EMail;
        }

        public string CompanyName { get; set; }

        public string Address { get; set; }

        public string EMail { get; set; }

        public ICommand Save { get; set; }
        
        public ICommand Cancel { get; set; } = new Utils.RoutedCommand();

    }
}
