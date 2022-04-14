namespace wpf_mvvm_utility_bills.Models
{
    public class Resource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Supplier Supplier { get; set; }
        public MeterDevice Meter { get; set; }
    }
}
