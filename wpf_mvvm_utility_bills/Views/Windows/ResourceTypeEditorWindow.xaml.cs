using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace wpf_mvvm_utility_bills.Views.Windows
{
    public partial class ResourceTypeEditorWindow : Window
    {
        public ResourceTypeEditorWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement) this.Template.FindName("WindowHeader", this);
            if (element != null)
                element.MouseLeftButtonDown += (object sender, MouseButtonEventArgs e) =>
                {
                    DragMove();
                };
        }
    }
}
