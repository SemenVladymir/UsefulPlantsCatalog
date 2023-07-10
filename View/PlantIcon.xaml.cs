using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UsefulPlantsCatalog.View
{
    /// <summary>
    /// Логика взаимодействия для PlantIcon.xaml
    /// </summary>
    public partial class PlantIcon : UserControl
    {
        public static readonly DependencyProperty myDepPropertyFolk = DependencyProperty.Register(nameof(FolkName), typeof(string), typeof(UserControl));
        public static readonly DependencyProperty myDepPropertyImg = DependencyProperty.Register(nameof(UrlPhoto), typeof(string), typeof(UserControl));
        public static readonly DependencyProperty myDepPropertyScience = DependencyProperty.Register(nameof(ScienceName), typeof(string), typeof(UserControl));
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
        public PlantIcon()
        {
            InitializeComponent();
            this.DataContext = this;
        }

    }
}
