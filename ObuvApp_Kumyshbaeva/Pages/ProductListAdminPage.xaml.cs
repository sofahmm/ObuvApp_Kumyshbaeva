using ObuvApp_Kumyshbaeva.DbConnection;
using ObuvApp_Kumyshbaeva.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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

        public static Supplier currentSupplier = null;
        public static string searchCurrent = "";
        public static string currentSort = "Все поставщики";
        public ProductListAdminPage(USer uSer)
        {
            InitializeComponent();
            products = new List<Product>(ConnectionString.obuvDb.Product.ToList());
            suppliers = new List<Supplier>(ConnectionString.obuvDb.Supplier.ToList());
            suppliers.Insert(0, new Supplier { Id = -1, Name = "Все поставщики" });

            FIOTbl.Text = $"Администратор: {uSer.FIO}";

            this.DataContext = this;
        }

        private void SupplierCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentSupplier = SupplierCmb.SelectedItem as Supplier;
            ApplyFilters();
        }

        private void SortProductsCmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = SortProductsCmb.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                currentSort = selectedItem.Content.ToString();
                ApplyFilters();
            }
        }

        private void SearchProductTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            searchCurrent = SearchProductTb.Text;
            ApplyFilters();
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

        private void productsLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var product = productsLv.SelectedItem as Product;
            if (product != null)
            {
                EditProductWindow editProductWindow = new EditProductWindow(product);
                editProductWindow.Show();
            }
        }
        private void ApplyFilters()
        {
            List<Product> filteredProducts = products;
            //ФИЛЬТРАЦИЯ
            if (currentSupplier != null && currentSupplier.Id != -1)
                filteredProducts = filteredProducts.Where(i => i.IdSupplier == currentSupplier.Id).ToList();
            else
                filteredProducts = products;

            //ПОИСК
            if (!string.IsNullOrWhiteSpace(searchCurrent))
            {
                    filteredProducts = filteredProducts.Where(i => i.Name.ToLower().Contains(SearchProductTb.Text.ToLower()) ||
                                                    i.Description.ToLower().Contains(SearchProductTb.Text.ToLower()) ||
                                                    i.ProductCategory.Name.ToLower().Contains(SearchProductTb.Text.ToLower()) ||
                                                    i.Supplier.Name.ToLower().Contains(SearchProductTb.Text.ToLower()) ||
                                                    i.Manufacturer.Name.ToLower().Contains(SearchProductTb.Text.ToLower())).ToList();
            }
            //СОРТИРОВКА
            if (currentSort == "По возрастанию")
                filteredProducts = filteredProducts.OrderBy(i => i.WorkshopCount).ToList();
            else if (currentSort == "По убыванию")
                filteredProducts = filteredProducts.OrderByDescending(i => i.WorkshopCount).ToList();
            else
                filteredProducts = filteredProducts;

            productsLv.ItemsSource = filteredProducts;
        }
    }
   
}
