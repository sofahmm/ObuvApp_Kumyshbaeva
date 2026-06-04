using ObuvApp_Kumyshbaeva.DbConnection;
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
using System.Windows.Shapes;

namespace ObuvApp_Kumyshbaeva.Windows
{
    /// <summary>
    /// Логика взаимодействия для EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        Product product = new Product();
        public static List<ProductCategory> categories {  get; set; }
        public static List<Manufacturer> manufacturers { get; set; }
        public static List<Supplier> suppliers { get; set; }    
        public static List<Unit> units { get; set; }
        public EditProductWindow(Product product1)
        {
            InitializeComponent();
            categories = new List<ProductCategory>
                (ConnectionString.obuvDb.ProductCategory.ToList());

            manufacturers = new List<Manufacturer>
                (ConnectionString.obuvDb.Manufacturer.ToList()); 
            
            suppliers = new List<Supplier>
                (ConnectionString.obuvDb.Supplier.ToList());
            units = new List<Unit>
                (ConnectionString.obuvDb.Unit.ToList());

            product = product1; //ОБЯЗАТЕЛЬНО!!!!!!!!!!!!!!!!!

            nameProdTb.Text = product.Name;
            descriptionTb.Text = product.Description;
            discountTb.Text = product.ActiveDiscount.ToString();
            priceTb.Text = product.Price.ToString();
            wshCountTb.Text = product.WorkshopCount.ToString();

            categoryCmb.SelectedItem = categories.
                FirstOrDefault(i => i.Name == product.ProductCategory.Name);

            manufacturerCmb.SelectedItem = manufacturers.
                FirstOrDefault(i => i.Name == product.Manufacturer.Name);

            supplierCmb.SelectedItem = suppliers.
                FirstOrDefault(i => i.Name == product.Supplier.Name);

            unitTb.SelectedItem = units.
                FirstOrDefault(i => i.Name == product.Unit.Name);

            this.DataContext = this;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
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

                ConnectionString.obuvDb.SaveChanges();
                MessageBox.Show("успех");
            }
            else
                MessageBox.Show("Заполните все поля");
        }
    }
}
