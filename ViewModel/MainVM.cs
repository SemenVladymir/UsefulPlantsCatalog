using Pharmacy.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using UsefulPlantsCatalog.Model;
using UsefulPlantsCatalog.View;
using RelayCommand = UsefulPlantsCatalog.Model.RelayCommand;

namespace UsefulPlantsCatalog.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        private ObservableCollection<Plant> plants;
        public ObservableCollection<PlantIcon> icons = new ObservableCollection<PlantIcon>();
        private Plant selectedPlant;
        private PlantIcon selectedIcon;
        private string searchName;
        private bool _isChecked;
        private bool _isAdmin = false;
        private bool _isVisible = true;
        private bool _isEnable = false;
        private bool btnAddStation = false;


        public bool IsVisible
        {
            get { return _isVisible; }
            set
            {
                _isVisible = value;
                OnPropertyChanged(nameof(IsVisible));
            }
        }

        public bool IsEnable
        {
            get { return _isEnable; }
            set
            {
                _isEnable = value;
                OnPropertyChanged(nameof(IsEnable));
            }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                OnPropertyChanged(nameof(IsAdmin));
            }
        }

        private RelayCommand enterPass;
        public RelayCommand EnterPass
        {
            get
            {
                if (enterPass == null)
                    enterPass = new RelayCommand(obj => { IsEnable=true; IsVisible = false; });

                return enterPass;
            }
        }

        private RelayCommand secretKeyChangedCommand;
        public RelayCommand SecretKeyChangedCommand
        {
            get
            {
                if (secretKeyChangedCommand == null)
                    secretKeyChangedCommand = new RelayCommand(obj => { Check(obj); });

                return secretKeyChangedCommand;
            }
        }

        public bool BtnAddStation
        {
            get { return btnAddStation; }
            set
            {
                btnAddStation = value;
                OnPropertyChanged(nameof(BtnAddStation));
            }
        }

        private void Check(object pass)
        {
            PasswordBox box = (PasswordBox)pass;
            if(box.Password.Equals("admin"))
            {
                IsAdmin= true;
                BtnAddStation = true;
                IsEnable = false;
            }
            
        }

        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                _isChecked = value;
                OnPropertyChanged(nameof(IsChecked));
            }
        }
       
        public string SearchName
        {
            get 
            {
                SelectPlants(searchName);
                return searchName; 
            }
            set { searchName = value; OnPropertyChanged("SearchName"); }
        }

        private RelayCommand adding;
        public RelayCommand Adding
        {
            get
            {
                if (adding == null)
                    adding = new RelayCommand(obj => { OpenWindowAddPlant(); });

                return adding;
            }
        }

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
                OpenInformWindow(selectedPlant);
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
                    Icons.Add(new PlantIcon{ UrlPhoto = item.UrlPhoto ?? "", FolkName=item.FolkName, ScienceName=item.ScienceName, Description=item.Description});
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
                    icons.Add(new PlantIcon { UrlPhoto = item.UrlPhoto ?? "", FolkName = item.FolkName, ScienceName = item.ScienceName ?? "", Description = item.Description });
            }
        }

        public void OpenInformWindow(Plant myPlant)
        {
            if (myPlant == null) {  return; }
            NewWindow newWindow = new NewWindow();
            PlantCard card = new PlantCard();
            card.Id = myPlant.Id;
            if (IsAdmin)
            {
                card.FName.IsReadOnly = false;
                card.SName.IsReadOnly = false;
                card.GRegion.IsReadOnly = false;
                card.Desc.IsReadOnly = false;
                card.PosProp.IsReadOnly = false;
                card.NegProp.IsReadOnly = false;
                card.btnSave.Visibility = System.Windows.Visibility.Visible;
                card.btnDelete.Visibility = System.Windows.Visibility.Visible;
                card.Photo.Visibility = System.Windows.Visibility.Visible;
                card.OpenPhoto.Visibility = System.Windows.Visibility.Visible;
            }
            card.DataContext = selectedPlant;
            newWindow.Content = card;
            if (selectedPlant != null)
                newWindow.Title = $"{selectedPlant.FolkName} ({selectedPlant.ScienceName})";
            newWindow.Show();
            newWindow.Closed += (s, ee) => { SelectPlants(""); };
        }

        public void OpenWindowAddPlant()
        {
            NewWindow newWindow = new NewWindow();
            PlantCard card = new PlantCard();
            Plant newplant = new Plant{ Id=-1, FolkName="Нема", ScienceName="Нема", Description = "Нема", 
                UrlPhoto = "https://upload.wikimedia.org/wikipedia/commons/9/9a/%D0%9D%D0%B5%D1%82_%D1%84%D0%BE%D1%82%D0%BE.png", 
                GrowthRegion = "Нема", NegativeProp = "Нема", PositiveProp = "Нема" };
            card.Id = -1;
            card.FName.IsReadOnly = false;
            card.SName.IsReadOnly = false;
            card.GRegion.IsReadOnly = false;
            card.Desc.IsReadOnly = false;
            card.PosProp.IsReadOnly = false;
            card.NegProp.IsReadOnly = false;
            card.btnSave.Visibility = System.Windows.Visibility.Visible;
            card.btnDelete.Visibility = System.Windows.Visibility.Visible;
            card.Photo.Visibility = System.Windows.Visibility.Visible;
            card.OpenPhoto.Visibility = System.Windows.Visibility.Visible;
            card.DataContext = newplant;
            newWindow.Content = card;
            newWindow.Show();
            newWindow.Closed += (s, ee) => { SelectPlants(""); };
        }
    }
}
