using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_mvvm_utility_bills.Models
{
    public class ResourceType
    {
        int _id;
        public int Id { get => _id; }

        public string Name { get; set; }

        public string Measurement { get; set; }

        public List<MeterDevice> MeterDevices { get; set; } = new();
    }
}
