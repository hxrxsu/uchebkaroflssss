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
using uchebkaroflssss.Data;

namespace uchebkaroflssss
{
    /// <summary>
    /// Interaction logic for ModeratorEventPage.xaml
    /// </summary>
    public partial class ModeratorEventPage : Page
    {
        public ModeratorEventPage(User currentUser)
        {
            InitializeComponent();

            _currentUser = currentUser;

            using (ApplicationContext db = new ApplicationContext())
            {
                var dbUsers = db.Users.Where(u => u.Role == "Участник").ToList();

                TB_CurrentUserInfo.Text = $"Здравствуйте! {currentUser.Name}  Ваша роль:{currentUser.Role}";

                foreach (User u in dbUsers)
                {
                    LB_Users.Items.Add($"{u.UserId} {u.Name}");
                } 
            }
        }

        public User _currentUser;

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (LB_Users.SelectedIndex >= 0)
                {
                    string selectedItem = LB_Users.SelectedItem.ToString();
                    int userId = int.Parse(selectedItem.Split(' ')[0]);
                    var dBUserId = db.Users.Find(userId);


                    dBUserId.Description = TB_Desc.Text;

                    db.SaveChanges();
                    MessageBox.Show("Данные обновлены");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Auth());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
