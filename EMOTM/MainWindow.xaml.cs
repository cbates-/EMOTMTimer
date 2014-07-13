using System.Windows;
using EMOTM.Infrastructure;
using EMOTM.ViewModel;

namespace EMOTM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the MainWindow class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            Closing += (s, e) => ViewModelLocator.Cleanup();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            MainViewModel vm = (this.DataContext as MainViewModel);
            if (vm != null)
            {
                vm.WhichMinute = (vm.WhichMinute == ThisThatMin.ThisMinute)
                    ? ThisThatMin.ThatMinute
                    : ThisThatMin.ThisMinute;
            }
        }
    }
}