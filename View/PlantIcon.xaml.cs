using System.Windows;
using System.Windows.Controls;
using UsefulPlantsCatalog.Model;

namespace UsefulPlantsCatalog.View
{
    public partial class PlantIcon : UserControl
    {
        public static readonly DependencyProperty myDepPropertyFolk = DependencyProperty.Register(nameof(FolkName), typeof(string), typeof(UserControl));
        public static readonly DependencyProperty myDepPropertyImg = DependencyProperty.Register(nameof(UrlPhoto), typeof(string), typeof(UserControl));
        public static readonly DependencyProperty myDepPropertyScience = DependencyProperty.Register(nameof(ScienceName), typeof(string), typeof(UserControl));
        public static readonly DependencyProperty myDepPropertyDescription = DependencyProperty.Register(nameof(Description), typeof(string), typeof(UserControl));
        public string FolkName
        {
            get { return (string)GetValue(myDepPropertyFolk); }
            set { SetValue(myDepPropertyFolk, value); }
        }

        public string UrlPhoto
        {
            get { return (string)GetValue(myDepPropertyImg); }
            set { SetValue(myDepPropertyImg, value); }
        }

        public string ScienceName
        {
            get { return (string)GetValue(myDepPropertyScience); }
            set { SetValue(myDepPropertyScience, value); }
        }

        public string Description
        {
            get { return (string)GetValue(myDepPropertyDescription); }
            set { SetValue(myDepPropertyDescription, value); }
        }
        public PlantIcon()
        {
            InitializeComponent();
            this.DataContext = this;
            
        }

        private RelayCommand openWindow;
        public RelayCommand OpenWindow
        {
            get
            {
                if (openWindow == null)
                    openWindow = new RelayCommand(obj => { MessageBox.Show($"{FolkName} ({ScienceName})\n{Description}"); });
                return openWindow;
            }
        }
    }
}
