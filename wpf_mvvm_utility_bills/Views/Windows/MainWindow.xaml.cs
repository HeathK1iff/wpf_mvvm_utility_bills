using System.Windows;

namespace wpf_mvvm_utility_bills.Views.Windows
{
    public partial class MainWindow : Window
    {
        FrameworkElement _windowHeader;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void _windowHeader_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _windowHeader = (FrameworkElement)this.Template.FindName("WindowHeader", this);
            if (_windowHeader != null)
                _windowHeader.MouseLeftButtonDown += _windowHeader_MouseLeftButtonDown;
        }
    }
}
