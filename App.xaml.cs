using System.Windows;
using UsefulPlantsCatalog.ViewModel;

namespace UsefulPlantsCatalog
{
    public partial class App : Application
    {
        public MainVM mainVM = new MainVM();

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            new MainWindow() { DataContext = mainVM }.Show();
        }
    }
}
