namespace wpf_mvvm_utility_bills.Utils
{
    class DeviceComboBoxItem
    {
        public DeviceComboBoxItem(Models.MeterDevice device)
        {
            Id = device.Id;
            Name = $"{device.Serial} ({device.ResourceType.Name})";
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
