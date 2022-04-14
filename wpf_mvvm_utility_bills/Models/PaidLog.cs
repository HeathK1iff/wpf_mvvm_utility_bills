
using System;

namespace wpf_mvvm_utility_bills.Models
{
    public class PaidLog
    {
        int _id;
        public int Id { get => _id; }

        public DateTime PaidDate { get; set; } = DateTime.Now;

        public double Amount { get; set; }

        public ValueLog PaidValue { get; set; }
    }
}
