using System;

namespace wpf_mvvm_utility_bills.Models
{
    public class MeterDevice
    {
        int _id;
        public int Id { get => _id; }
        
        public string Serial { get; set; }

        public DateTime DateIssued { get; set; } = DateTime.Now;

        public DateTime DateExpired { get; set; } = DateTime.Now;
        
        public bool IsActive { get; set; }

        public ResourceType? ResourceType { get; set; }
    }
}
