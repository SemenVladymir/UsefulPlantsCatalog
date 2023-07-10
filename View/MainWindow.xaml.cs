using System.Windows;
using UsefulPlantsCatalog.ViewModel;

namespace UsefulPlantsCatalog
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainVM();
        }
    }
}
