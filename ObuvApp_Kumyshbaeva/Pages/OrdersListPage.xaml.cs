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
    /// Логика взаимодействия для OrdersListPage.xaml
    /// </summary>
    public partial class OrdersListPage : Page
    {
        public static List<Order> orders { get;set; }
        public OrdersListPage()
        {
            InitializeComponent();
            orders = new List<Order>(ConnectionString.obuvDb.Order.ToList());
            this.DataContext = this;
        }
    }
}
