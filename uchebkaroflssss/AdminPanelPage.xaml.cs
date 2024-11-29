using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
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
    /// Interaction logic for MainEventPage.xaml
    /// </summary>
    public partial class AdminPanelPage : Page
    {
        public AdminPanelPage(string currentuserauth)
        {
            InitializeComponent();

            TB_CurrentUser.Text = currentuserauth;

            using (ApplicationContext db = new ApplicationContext())
            {
                var _usersList = db.Users.ToList();

                foreach (User u in _usersList)
                {
                    LB_Users.Items.Add($"{u.UserId} {u.Name}");
                }
            }

        }

        bool _isFirstClicked = false;
        private void BN_CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BN_SwapAccount_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Auth());
        }

        private void BN_DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (LB_Users.SelectedIndex >= 0)
                {
                    string selectedItem = LB_Users.SelectedItem.ToString();
                    int userId = int.Parse(selectedItem.Split(' ')[0]);

                   
                        var user = db.Users.Find(userId);
                        if (user != null)
                        {
                            db.Users.Remove(user);
                            db.SaveChanges();

                            LB_Users.Items.Remove(selectedItem);
                        }              
                }
            }
        }
        private void ShowInfoFromListBox()
        {
            try
            {
                if (LB_Users.SelectedIndex >= 0)
                {
                    using (ApplicationContext db = new ApplicationContext())
                    {
                        string selectedItem = LB_Users.SelectedItem.ToString();
                        int userId = int.Parse(selectedItem.Split(' ')[0]);
                        var dBUserId = db.Users.Find(userId);

                        var _userName = dBUserId.Name;
                        var _userPhone = dBUserId.PhoneNumber;
                        var _userLogin = dBUserId.Login;
                        var _userPassword = dBUserId.Password;
                        var _userRole = dBUserId.Role;
                        var _userEmail = dBUserId.Email;
                        var _userDateOfBirth = dBUserId.DateOfBirth;

                        TB_UserFIO.Text = _userName;
                        TB_UserRole.Text = _userRole;
                        TB_UserPhoneNumber.Text = _userPhone.ToString();
                        TB_UserLogin.Text = _userLogin;
                        TB_UserPassword.Text = _userPassword;
                        TB_UserEmail.Text = _userEmail;
                        TB_UserDateBirth.Text = _userDateOfBirth.ToString();
                    }
                }
            }
            catch(Exception ex) { }

        }

        private void LB_Users_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowInfoFromListBox();
        }

        private void BN_EditUser_Click(object sender, RoutedEventArgs e)
        {
            _isFirstClicked = !_isFirstClicked;

            if (_isFirstClicked)
            {
                BR_UserInfoHide.Visibility = Visibility.Hidden;
            }
            else
            {
                BR_UserInfoHide.Visibility = Visibility.Visible;

            }

        }

        private void BN_UserInfoSave_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (LB_Users.SelectedItem == null)
                {
                    MessageBox.Show("Выберите пользователя!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    string selectedItem = LB_Users.SelectedItem.ToString();
                    int userId = int.Parse(selectedItem.Split(' ')[0]);
                    var dBUserId = db.Users.Find(userId);

                   

                    if(TB_UserRole.Text == "Администратор" || TB_UserRole.Text == "Жюри" || TB_UserRole.Text == "Организатор" || TB_UserRole.Text == "Модератор" || TB_UserRole.Text == "Участник")
                    {
                        dBUserId.Role = TB_UserRole.Text;
                        dBUserId.Name = TB_UserFIO.Text;
                        dBUserId.PhoneNumber = Convert.ToInt32(TB_UserPhoneNumber.Text);
                        dBUserId.Login = TB_UserLogin.Text;
                        dBUserId.Password = TB_UserPassword.Text;
                        dBUserId.Email = TB_UserEmail.Text;
                        dBUserId.DateOfBirth = Convert.ToDateTime(TB_UserDateBirth.Text);
                        MessageBox.Show("Данные обновлены");
                    }
                    else
                    {
                        MessageBox.Show("Введите корректную роль!");
                    }

                    db.SaveChanges();

                    var _usersList = db.Users.ToList();

                    LB_Users.Items.Clear();

                    foreach (User u in _usersList)
                    {
                        LB_Users.Items.Add($"{u.UserId} {u.Name}");
                    }
                }
            }
        }

        private void TB_UserRole_GotFocus(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Доступные роли: Администратор, Участник, Модератор, Организатор, Жюри");
        }
    }
}
