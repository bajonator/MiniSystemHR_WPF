using MahApps.Metro.Controls;
using MiniSystemHR_WPF.ViewModels;

namespace MiniSystemHR_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
