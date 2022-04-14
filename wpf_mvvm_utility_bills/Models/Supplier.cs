namespace wpf_mvvm_utility_bills.Models
{
    public class Supplier
    {
        private int _id;
        public int Id { get => _id; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string EMail { get; set; }
    }
}
