using Microsoft.EntityFrameworkCore;
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
    /// Interaction logic for UsessrEventPage.xaml
    /// </summary>
    public partial class MemberEventPage : Page
    {
        public MemberEventPage(string resultCurrentUser, User currentUserInfo  )
        {
            InitializeComponent();

            TB_CurrentUser.Text = resultCurrentUser;

            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.ToList();

                _currentUser = currentUserInfo;

                LoadDataToTB();
            }
        }

        private void LoadDataToTB()
        {
            TB_UserFIO.Text = _currentUser.Name;
            TB_UserPhoneNumber.Text = _currentUser.PhoneNumber.ToString();
            TB_UserLogin.Text = _currentUser.Login;
            TB_UserPassword.Text = _currentUser.Password;
            TB_UserEmail.Text = _currentUser.Email;
            TB_UserDateBirth.Text = _currentUser.DateOfBirth.ToString();

            TB_DescFromModer.Text = _currentUser.Description;
        }

        private User _currentUser;

        private void ChangeUserData()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Attach(_currentUser);
                db.Entry(_currentUser).State = EntityState.Modified;

                _currentUser.Name = TB_CurrentUser.Text;
                _currentUser.Email = TB_UserEmail.Text;
                _currentUser.Login = TB_UserLogin.Text;
                _currentUser.Password = TB_UserPassword.Text;
                _currentUser.PhoneNumber = Convert.ToInt32(TB_UserPhoneNumber.Text);
                _currentUser.DateOfBirth = Convert.ToDateTime(TB_UserDateBirth.Text);

                LoadDataToTB();
                MessageBox.Show("Данные обновлены!");
                db.SaveChanges();
                LoadDataToTB();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Auth());
        }

        private void BN_UserInfoSave_Click(object sender, RoutedEventArgs e) => ChangeUserData();

        private void BN_ShowCurrentEvent_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var subsInfo = db.ActivitieSubs
                    .Include(s => s.User)
                    .Include(s => s.Activity)
                    .ToList();

                var showResult = subsInfo.FirstOrDefault(sub => sub.User != null && sub.User.UserId == _currentUser.UserId);

                if (showResult != null)
                {
                    MessageBox.Show($"Сейчас вы участвуете в {showResult.ActivityNameSub}");
                }
                else
                {
                    MessageBox.Show($"Сейчас вы нигде не участвуете. Обратитесь к организатору!");
                }
            }
        }
    }
}
