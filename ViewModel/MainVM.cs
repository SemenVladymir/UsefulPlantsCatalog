using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UsefulPlantsCatalog.Model;

namespace UsefulPlantsCatalog.ViewModel
{
    public class MainVM : INotifyPropertyChanged
    {
        private ObservableCollection<Plant> plants;
        private Plant selectedPlant;

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
    }
}
