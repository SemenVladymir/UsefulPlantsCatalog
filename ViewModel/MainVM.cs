using GalaSoft.MvvmLight.Command;
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
        public ObservableCollection<PlantIcon> icons = new ObservableCollection<PlantIcon>();
        private Plant selectedPlant;
        private PlantIcon selectedIcon;
        private string searchName;

        public string SearchName
        {
            get 
            {
                SelectPlants(searchName);
                return searchName; 
            }
            set { searchName = value; OnPropertyChanged("SearchName"); }
        }

        //private RelayCommand search;
        //public RelayCommand Search
        //{
        //    get
        //    {
        //        if (search == null)
        //            search = new RelayCommand(Action);

        //        return search;
        //    }
        //}

        //public void Action()
        //{
        //    if (!string.IsNullOrEmpty(searchName))
        //        SelectPlants(false, searchName);
        //}

        public ObservableCollection<PlantIcon> Icons
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
            get 
            {
                OpenInformWindow();
                return selectedPlant;
            }
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
               Plants = new ObservableCollection<Plant>(context.CatalogOfPlants.ToList().OrderBy(i => i.FolkName));
                
                foreach (var item in Plants)
                    Icons.Add(new PlantIcon{ UrlPhoto = item.UrlPhoto ?? "", FolkName=item.FolkName, ScienceName=item.ScienceName ?? ""});
                selectedPlant = Plants[0];
            }
        }

        public void SelectPlants(string searchName)
        {
            using (BDContext context = new BDContext())
            {
                if (!string.IsNullOrEmpty(searchName))
                {
                    Plants.Clear();
                    Plants = new ObservableCollection<Plant>(context.CatalogOfPlants.Where(c => c.FolkName.Contains(searchName) || c.ScienceName.Contains(searchName)
                    || c.Description.Contains(searchName) || c.PositiveProp.Contains(searchName) || c.NegativeProp.Contains(searchName)).ToList().OrderBy(i => i.FolkName));
                }
                else
                { 
                    Plants.Clear();
                    Plants = new ObservableCollection<Plant>(context.CatalogOfPlants.ToList().OrderBy(i => i.FolkName));
                }
                    
            }
            Icons.Clear();
            if (Plants.Count > 0)
            {
                selectedPlant = Plants[0];
                foreach (var item in Plants)
                    Icons.Add(new PlantIcon { UrlPhoto = item.UrlPhoto ?? "", FolkName = item.FolkName, ScienceName = item.ScienceName ?? "" });
            }
        }

        public void OpenInformWindow()
        {
            NewWindow newWindow = new NewWindow();
            PlantCard card = new PlantCard();
            card.DataContext = selectedPlant;
            newWindow.Content = card;
            if (selectedPlant != null)
                newWindow.Title = $"{selectedPlant.FolkName} ({selectedPlant.ScienceName})";
            newWindow.Show();
        }
    }
}
