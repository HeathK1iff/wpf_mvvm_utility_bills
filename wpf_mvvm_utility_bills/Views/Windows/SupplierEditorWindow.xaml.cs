using System.Windows;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.Views.Windows
{
    /// <summary>
    /// Interaction logic for SupplierEditorWindow.xaml
    /// </summary>
    public partial class SupplierEditorWindow : Window
    {
        FrameworkElement _windowHeader;
        public SupplierEditorWindow()
        {
            InitializeComponent();
        }

        private void _windowHeader_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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
