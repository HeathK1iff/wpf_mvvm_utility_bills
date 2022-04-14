using System;

namespace wpf_mvvm_utility_bills.Models
{
    public class ValueLog
    {
        int _id;

        public int Id { get => _id; }

        public DateTime DateChecked { get; set; } = DateTime.Today;

        public int CurrentValue { get; set; }

        public string Comments { get; set; }

        public int MeterDeviceId { get; set; }

        public MeterDevice MeterDevice { get; set; }
    }
}
