using Microsoft.Win32;
using Pharmacy.Model;
using System.Windows;
using System.Windows.Controls;
using UsefulPlantsCatalog.Model;

namespace UsefulPlantsCatalog.View
{
    public partial class PlantCard : UserControl
    {
        public int Id { get; set; }
        public PlantCard()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Зберигти/додати картку рослини?", "Корегування/додавання картки", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (result == MessageBoxResult.Yes)
            {
                using (BDContext context = new BDContext())
                {
                    if (Id > 0)
                    {
                        Plant newPlant = new Plant
                        {
                            Id = this.Id,
                            FolkName = FName.Text,
                            ScienceName = SName.Text,
                            GrowthRegion = GRegion.Text,
                            Description = Desc.Text,
                            NegativeProp = NegProp.Text,
                            PositiveProp = PosProp.Text,
                            UrlPhoto = UrlPhotoPath.Text
                        };
                        context.Update(newPlant);
                        if (context.SaveChanges() > 0)
                        {
                            MessageBox.Show("Картка рослини змінена!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            Window myw = Window.GetWindow(this);
                            myw.Close();
                        }
                        else
                        {
                            MessageBox.Show("Щось пошло не так! Картка не була змінена!");
                        }
                    }
                    else
                    {
                        Plant newPlant = new Plant
                        {
                            FolkName = FName.Text,
                            ScienceName = SName.Text,
                            GrowthRegion = GRegion.Text,
                            Description = Desc.Text,
                            NegativeProp = NegProp.Text,
                            PositiveProp = PosProp.Text,
                            UrlPhoto = UrlPhotoPath.Text
                        };
                        context.CatalogOfPlants.Add(newPlant);
                        if (context.SaveChanges() > 0)
                        {
                            MessageBox.Show("Картка рослини додана!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            Window myw = Window.GetWindow(this);
                            myw.Close();
                        }
                        else
                        {
                            MessageBox.Show("Щось пошло не так! Картка не була додана!");
                        }
                    }
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (Id > 0)
            {
                MessageBoxResult result = MessageBox.Show("Ви дійсно хочете видалити цю картку рослини?", "Видалення картки", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    using (BDContext context = new BDContext())
                    {
                        context.Remove(new Plant { Id = this.Id });
                        if (context.SaveChanges() > 0)
                        {
                            MessageBox.Show("Картка була видалена");
                            Window myw = Window.GetWindow(this);
                            myw.Close();
                        }
                        else
                        {
                            MessageBox.Show("Щось пошло не так! Картка не була видалена!");
                        }
                    }

                }
            }
        }

        private void BtnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpeg.gif.webp.bmp)|*.png;*.jpeg.gif.webp.bmp|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
                UrlPhotoPath.Text = openFileDialog.FileName; 
        }
    }
}
