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
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        public static List<ProductCategory> productCategories {  get; set; } 
        public static List<Manufacturer> manufacturers{  get; set; }
        public static List<Supplier> suppliers{  get; set; }
        public static List<Unit> units{ get; set; }

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
            product.Name = nameProdTb.Text.Trim();
            product.IdProductCategory = (categoryCmb.SelectedItem as ProductCategory).Id;
            product.Description = descriptionTb.Text.Trim();
            product.IdManufacturer = ((Manufacturer)manufacturerCmb.SelectedItem).Id;
            product.IdSupplier = ((Supplier)supplierCmb.SelectedItem).Id;
            product.Price = Convert.ToDouble(priceTb.Text.Trim());
            product.ActiveDiscount = Convert.ToInt32(discountTb.Text.Trim());
            product.WorkshopCount = Convert.ToInt32(wshCountTb.Text.Trim());
            product.IdUnit = ((Unit)unitTb.SelectedItem).Id;

            ConnectionString.obuvDb.Product.Add(product);
            ConnectionString.obuvDb.SaveChanges();
            MessageBox.Show("успех");
        }
    }
}
