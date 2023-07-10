using System.Windows.Controls;

namespace UsefulPlantsCatalog.View
{
    public partial class PlantCard : UserControl
    {
        public PlantCard()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}
