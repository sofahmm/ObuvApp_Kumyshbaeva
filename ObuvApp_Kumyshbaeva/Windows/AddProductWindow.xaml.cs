using ObuvApp_Kumyshbaeva.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace ObuvApp_Kumyshbaeva.Windows
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public static List<ProductCategory> productCategories {  get; set; } 
        public static List<Manufacturer> manufacturers{  get; set; }
        public static List<Supplier> suppliers{  get; set; }
        public static List<Unit> units{ get; set; }
        public string _projectDirectory = "C:\\Users\\Student\\source\\repos\\ObuvApp_Kumyshbaeva\\ObuvApp_Kumyshbaeva\\Resources\\";//'
        private string _selectedPhototPath = null;

        public AddProductWindow()
        {
            InitializeComponent();
            productCategories = new List<ProductCategory>(ConnectionString.obuvDb.ProductCategory.ToList());
            manufacturers = new List<Manufacturer>(ConnectionString.obuvDb.Manufacturer.ToList());
            suppliers = new List<Supplier>(ConnectionString.obuvDb.Supplier.ToList());
            units = new List<Unit>(ConnectionString.obuvDb.Unit.ToList());
            this.DataContext = this;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            if (nameProdTb.Text != string.Empty && categoryCmb.SelectedItem != null
                && descriptionTb.Text != string.Empty && manufacturerCmb.SelectedItem != null
                && supplierCmb.SelectedItem != null && priceTb.Text != string.Empty
                && discountTb.Text != string.Empty && wshCountTb.Text != string.Empty
                && unitTb.SelectedItem != null)
            {
                product.Name = nameProdTb.Text.Trim();
                product.IdProductCategory = (categoryCmb.SelectedItem as ProductCategory).Id;
                product.Description = descriptionTb.Text.Trim();
                product.IdManufacturer = ((Manufacturer)manufacturerCmb.SelectedItem).Id;
                product.IdSupplier = ((Supplier)supplierCmb.SelectedItem).Id;
                product.Price = Convert.ToDouble(priceTb.Text.Trim());
                product.ActiveDiscount = Convert.ToInt32(discountTb.Text.Trim());
                product.WorkshopCount = Convert.ToInt32(wshCountTb.Text.Trim());
                product.IdUnit = ((Unit)unitTb.SelectedItem).Id;
                if (!string.IsNullOrEmpty(_selectedPhototPath))
                {
                    product.Photo = SavePhoto(_selectedPhototPath);
                }
                ConnectionString.obuvDb.Product.Add(product);
                ConnectionString.obuvDb.SaveChanges();
                MessageBox.Show("успех");
            }
            else
                MessageBox.Show("Заполните все поля");
        }

        private void ChoosePhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения|*.jpg;*jpeg;*png";
            openFileDialog.Title = "Выберите фото товара";

            if(openFileDialog.ShowDialog() == true)
            {
               _selectedPhototPath = openFileDialog.FileName;
                productImage.Source = new BitmapImage(new Uri(_selectedPhototPath));
            }
        }
        private string SavePhoto(string sourcePath)
        {

            string fullPath = System.IO.Path.GetFullPath(sourcePath);
            string fileExtension = System.IO.Path.GetFileName(fullPath);

            return $"/Resources/{fileExtension}";
        }
    }
}
