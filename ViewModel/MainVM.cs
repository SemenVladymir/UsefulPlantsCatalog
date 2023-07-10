using Pharmacy.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using UsefulPlantsCatalog.Model;
using UsefulPlantsCatalog.View;

namespace UsefulPlantsCatalog.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        private ObservableCollection<Plant> plants;
        public List<PlantIcon> icons = new List<PlantIcon>();
        private Plant selectedPlant;
        private PlantIcon selectedIcon;

        public List<PlantIcon> Icons
        {
            get { return icons; }
            set { icons = value; OnPropertyChanged("Icons"); }
        }
        public PlantIcon SelectedIcon
        {
            get { return selectedIcon; }
            set { selectedIcon = value; OnPropertyChanged("SelectedIcon"); }
        }

        public Plant SelectedPlant
        {
            get {return selectedPlant;}
            set { selectedPlant = value; OnPropertyChanged("SelectedPlant"); }
        }

        public ObservableCollection<Plant> Plants
        {
            get { return plants; }
            set { plants = value; OnPropertyChanged("Plants"); }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public MainVM()
        {
            using (BDContext context = new BDContext())
            {
               Plants = new ObservableCollection<Plant>(context.CatalogOfPlants.ToList());
                
                foreach (var item in Plants)
                    Icons.Add(new PlantIcon{ UrlPhoto = item.UrlPhoto ?? "", FolkName=item.FolkName, ScienceName=item.ScienceName ?? ""});
              
            }
        }

        public void AddItemsToWrapPanel()
        {
            
            WrapPanel wrapPanel = new WrapPanel();
            PlantIcon plantIcon = new PlantIcon();

        }
    }
}
