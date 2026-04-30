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
    /// Логика взаимодействия для AuthorizationPage.xaml
    /// </summary>
    public partial class AuthorizationPage : Page
    {
        public static List<USer> users { get; set; }
        public AuthorizationPage()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, RoutedEventArgs e)
        {
            string login = loginTb.Text.Trim();
            string password = passwordPb.Password.Trim();

            users = new List<USer>(ConnectionString.obuvDb.USer.ToList());
            USer currentUser = users.FirstOrDefault(i => i.Login.Trim() == login && i.Password.Trim() == password);
            if (currentUser != null)
            {
                MessageBox.Show("Авторизация прошла успешно 👍");
                NavigationService.Navigate(new ProductListPage());
            }
            else
                MessageBox.Show("Авторизация не прошла успешно ");

        }
    }
}
