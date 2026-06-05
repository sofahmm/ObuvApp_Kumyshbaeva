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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObuvApp_Kumyshbaeva.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public static List<Product> products { get; set; } 
        public ProductListPage(USer uSer)
        {
            InitializeComponent();
            products = new List<Product>(ConnectionString.obuvDb.Product.ToList());
            FIOTbl.Text = $"Пользователь: {uSer.FIO}";
            this.DataContext = this;
        }
    }
}
