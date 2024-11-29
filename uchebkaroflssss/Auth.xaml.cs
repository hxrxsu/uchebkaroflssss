using Npgsql;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
    /// Interaction logic for Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
        }


        public string resultCurrentUser;

        private void BN_Auth_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var currentuserauth = db.Users.FirstOrDefault(u => u.Login == TB_Login.Text && u.Password == TB_Password.Text);


                if (currentuserauth != null)
                {
                    if(currentuserauth.Role == "Администратор")
                    {
                        MessageBox.Show("Вход выполнен!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                        resultCurrentUser = $"Здравствуйте!: {currentuserauth.Name} Ваша роль: {currentuserauth.Role}";
                        NavigationService.Navigate(new AdminPanelPage(resultCurrentUser));
                    }
                    else if(currentuserauth.Role == "Участник")
                    {
                        MessageBox.Show("Вход выполнен!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                        resultCurrentUser = $"Здравствуйте!: {currentuserauth.Name} Ваша роль: {currentuserauth.Role}";
                        NavigationService.Navigate(new MemberEventPage(resultCurrentUser, currentuserauth));
                    }
                    else if(currentuserauth.Role == "Модератор")
                    {
                        MessageBox.Show("Вход выполнен!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.Navigate(new ModeratorEventPage(currentuserauth));
                    }
                    else if(currentuserauth.Role == "Жюри")
                    {
                        MessageBox.Show("Вход выполнен!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                        NavigationService.Navigate(new JuryEventPage(currentuserauth));
                    }
                    else if(currentuserauth.Role == "Организатор")
                    {
                        MessageBox.Show("Вход выполнен!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Information);
                        resultCurrentUser = $"Здравствуйте!: {currentuserauth.Name} Ваша роль: {currentuserauth.Role}";
                        NavigationService.Navigate(new OrganizerEventPage(resultCurrentUser));
                    }
                }
                else
                {
                    MessageBox.Show("Введите корректные данные или зарегистрируйтесь!", "Авторизация", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void BN_Reg_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Reg());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.CloseApplication();
            }
        }

        private void TB_Password_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_Password.Text = "";
        }

        private void TB_Login_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_Login.Text = "";
        }
    }
}
