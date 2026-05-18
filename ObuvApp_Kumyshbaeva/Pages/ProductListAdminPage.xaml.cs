using ObuvApp_Kumyshbaeva.DbConnection;
using ObuvApp_Kumyshbaeva.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObuvApp_Kumyshbaeva.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductListAdminPage.xaml
    /// </summary>
    public partial class ProductListAdminPage : Page
    {
        public static List<Product> products { get; set; }
        public static List<Supplier> suppliers { get; set; }
        public ProductListAdminPage()
        {
            InitializeComponent();
            products = new List<Product>(ConnectionString.obuvDb.Product.ToList());
            suppliers = new List<Supplier>(ConnectionString.obuvDb.Supplier.ToList());
            suppliers.Insert(0, new Supplier { Id = -1, Name = "Все поставщики" });
            this.DataContext = this;
        }

        private void SupplierCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var sup = SupplierCmb.SelectedItem as Supplier;
            if (sup.Id != -1)
                productsLv.ItemsSource = products.Where(i => i.IdSupplier == sup.Id).ToList();
            else
                productsLv.ItemsSource = products;
        }

        private void SortProductsCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortProductsCmb.SelectedItem.ToString() == "По возрастанию")
                productsLv.ItemsSource = products.OrderBy(i => i.WorkshopCount).ToList();
            else if (SortProductsCmb.SelectedItem.ToString() == "По убыванию")
                productsLv.ItemsSource = products.OrderByDescending(i => i.WorkshopCount).ToList();
            else
                productsLv.ItemsSource = products;
        }

        private void SearchProductTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            productsLv.ItemsSource = products.Where(i => i.Name.ToLower().Contains(SearchProductTb.Text.ToLower()) ||
                                            i.Description.ToLower().Contains(SearchProductTb.Text.ToLower()) ||
                                            i.ProductCategory.Name.ToLower().Contains(SearchProductTb.Text.ToLower()) ||
                                            i.Supplier.Name.ToLower().Contains(SearchProductTb.Text.ToLower()) ||
                                            i.Manufacturer.Name.ToLower().Contains(SearchProductTb.Text.ToLower())).ToList();

        }

        private void OrderBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new OrdersListPage());
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProductWindow addProductWindow = new AddProductWindow();
            addProductWindow.Show();
        }
    }
}
