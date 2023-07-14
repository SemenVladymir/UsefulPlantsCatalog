using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace UsefulPlantsCatalog.Model
{
    public class Plant : INotifyPropertyChanged
    {
        private int id;

        private string folkName;

        private string? scienceName;

        private string? description;

        private string? positiveProp;

        private string? negativeProp;

        private string? growthRegion;

        private string? urlPhoto;

        public event PropertyChangedEventHandler? PropertyChanged;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        public string FolkName
        {
            get { return folkName; }
            set { folkName = value; OnPropertyChanged("FolkName"); }
        }

        public string ScienceName
        {
            get { return scienceName; }
            set { scienceName = value; OnPropertyChanged("ScienceName"); }
        }

        public string Description
        {
            get { return description ?? ""; }
            set { description = value; OnPropertyChanged("Description"); }
        }

        public string PositiveProp
        {
            get { return positiveProp; }
            set { positiveProp = value; OnPropertyChanged("PositiveProp"); }
        }

        public string NegativeProp
        {
            get { return negativeProp; }
            set { negativeProp = value; OnPropertyChanged("NegativeProp"); }
        }

        public string GrowthRegion
        {
            get { return growthRegion; }
            set { growthRegion = value; OnPropertyChanged("GrowthRegion"); }
        }

        public string UrlPhoto
        {
            get { return urlPhoto; }
            set { urlPhoto = value; OnPropertyChanged("UrlPhoto"); }
        }

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
