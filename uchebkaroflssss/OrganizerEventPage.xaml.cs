using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static System.Net.Mime.MediaTypeNames;
using Activity = uchebkaroflssss.Data.Activity;
using Application = System.Windows.Application;

namespace uchebkaroflssss
{
    /// <summary>
    /// Interaction logic for SponsorEventPage.xaml
    /// </summary>
    public partial class OrganizerEventPage : Page
    {
        public OrganizerEventPage(string resultCurrentUser)
        {
            InitializeComponent();

            TB_CurrentUser.Text = resultCurrentUser;

            using (ApplicationContext db = new ApplicationContext())
            {
                var _eventsList = db.Events.ToList();
                var _activityList = db.Activities.ToList();
                var _usersList = db.Users.ToList();

                LB_EventUsers.Items.Clear();
                LB_Events.Items.Clear();
                LB_Activities.Items.Clear();


                foreach (User u in _usersList)
                {
                    LB_EventUsers.Items.Add($"{u.UserId}, {u.Name}, {u.Role}");
                }

                foreach (Activity act in _activityList)
                {
                    LB_Activities.Items.Add($"{act.ActivityId} Имя активности: {act.ActivityName}");
                }


                LoadDataToLBEvent(_eventsList);
                LoadSubscriptions();
            }
        }

        private void BN_CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BN_SwapAccount_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Auth());
        }

        private void BN_AddEvent_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                TB_CreationDateEvent.Text = DateTime.Now.ToString();
                try
                {
                    if (string.IsNullOrEmpty(TB_EventName.Text) || string.IsNullOrEmpty(TB_EventDesc.Text) && string.IsNullOrEmpty(DP_CreatedToDateEvent.SelectedDate.Value.ToString()))
                    {
                    }
                    else
                    {
                        Event _newEvent = new Event
                        {
                            EventName = TB_EventName.Text,
                            EventDescription = TB_EventDesc.Text,
                            EventCreatedDay = Convert.ToDateTime(TB_CreationDateEvent.Text),
                            EventDateToEnd = DP_CreatedToDateEvent.SelectedDate.Value
                        };

                        db.Events.Add(_newEvent);
                        db.SaveChanges();
                        MessageBox.Show("Вы добавили мероприятие!", "Мероприятие", MessageBoxButton.OK, MessageBoxImage.Information);

                        var _eventsList = db.Events.ToList();

                        LB_Events.Items.Clear();

                        LoadDataToLBEvent(_eventsList);
                    }
                }
                catch (Exception ex) { MessageBox.Show($"Заполните все данные мероприятия!", "Мероприятие", MessageBoxButton.OK, MessageBoxImage.Error); }
            }
        }

        private void LoadDataToLBEvent(List<Event> _eventsList)
        {
            foreach (Event et in _eventsList)
            {
                TextBlock textBlock = new TextBlock
                {
                    Text = $"{et.EventId} {et.EventName}\nДата создания: {et.EventCreatedDay}\nДата окончания: {et.EventDateToEnd}",
                    TextWrapping = TextWrapping.Wrap,
                    Width = 280
                };

                ListBoxItem listBoxItem = new ListBoxItem
                {
                    Content = textBlock
                };

                LB_Events.Items.Add(listBoxItem);
            }
        }

        private void BN_DeleteSelectedItem_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (LB_Events.SelectedIndex >= 0)
                {
                    ListBoxItem selectedItem = (ListBoxItem)LB_Events.SelectedItem;

                    TextBlock textBlock = (TextBlock)selectedItem.Content;

                    string text = textBlock.Text;

                    int eventId = int.Parse(text.Split(' ')[0]);

                    var _event = db.Events.Find(eventId);
                    if (_event != null)
                    {
                        db.Events.Remove(_event);
                        db.SaveChanges();

                        LB_Events.Items.Remove(selectedItem);
                    }
                }
            }
        }

        private void TB_ActivityName_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_ActivityName.Text = "";
        }

        private void TB_EventDesc_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_EventDesc.Text = "";
        }

        private void TB_EventName_GotFocus(object sender, RoutedEventArgs e)
        {
            TB_EventName.Text = "";
        }

        private void BN_AddActivity_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                try
                {
                    if (string.IsNullOrEmpty(TB_ActivityName.Text))
                    {
                        MessageBox.Show($"Заполните имя активности!", "Активность", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        Activity _newActivity = new Activity
                        {
                            ActivityName = TB_ActivityName.Text
                        };

                        db.Activities.Add(_newActivity);
                        db.SaveChanges();
                        MessageBox.Show("Вы добавили активность!", "Активность", MessageBoxButton.OK, MessageBoxImage.Information);

                        var _activityList = db.Activities.ToList();

                        LB_Activities.Items.Clear();

                        foreach (Activity act in _activityList)
                        {
                            LB_Activities.Items.Add($"{act.ActivityId} Имя активности: {act.ActivityName}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Заполните имя активности!", "Активность", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void BN_DeleteActivity_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (LB_Activities.SelectedIndex >= 0)
                {
                    string selectedItem = LB_Activities.SelectedItem.ToString();
                    int activitiesId = int.Parse(selectedItem.Split(' ')[0]);

                    var _activity = db.Activities.Find(activitiesId);
                    if (_activity != null)
                    {
                        db.Activities.Remove(_activity);
                        db.SaveChanges();

                        LB_Activities.Items.Remove(selectedItem);
                    }
                }

            }
        }

        private void BN_AddToActivity_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {

                    if (string.IsNullOrEmpty(TB_EventNameFromLB.Text) || string.IsNullOrEmpty(TB_ActivityNameFromLB.Text) || string.IsNullOrEmpty(TB_UserNameFromLB.Text))
                    {
                        MessageBox.Show("Заполните все поля!", "Активность", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    string inputActivity = TB_ActivityNameFromLB.Text.ToUpper();
                    string inputEvent = TB_EventNameFromLB.Text.ToUpper();
                    string inputUser = TB_UserNameFromLB.Text.ToUpper();

                    // Проверка наличия элементов в базе данных
                    var activity = db.Activities.FirstOrDefault(a => a.ActivityName.ToUpper().Equals(inputActivity));
                    var eventItem = db.Events.FirstOrDefault(ev => ev.EventName.ToUpper().Equals(inputEvent));
                    var user = db.Users.FirstOrDefault(u => u.Name.ToUpper().Equals(inputUser));

                    if (activity == null)
                    {
                        MessageBox.Show($"Активность '{TB_ActivityNameFromLB.Text}' не найдена в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (eventItem == null)
                    {
                        MessageBox.Show($"Мероприятие '{TB_EventNameFromLB.Text}' не найдено в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (user == null)
                    {
                        MessageBox.Show($"Пользователь '{TB_UserNameFromLB.Text}' не найден в базе данных.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                var subscription = new ActivitySubscription
                {
                    ActivityId = activity.ActivityId,
                    EventId = eventItem.EventId,
                    UserId = user.UserId,
                    ActivityNameSub = activity.ActivityName, 
                    EventNameSub = eventItem.EventName
                };

                    db.ActivitieSubs.Add(subscription);
                    db.SaveChanges();

                    LoadSubscriptions();
               
            }
        }

        private void LoadSubscriptions()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var subscriptions = db.ActivitieSubs.Include(s => s.Activity).Include(s => s.User).Include(s => s.Event).ToList();
                LB_EventUserSubscription.Items.Clear();
                foreach (var subscription in subscriptions)
                {
                    LB_EventUserSubscription.Items.Add($"{subscription.ActivitySubscriptionId} - Мероприятие -  - {subscription.Activity.ActivityName} (Пользователь - {subscription.User.Name}) - Мероприятие - {subscription.Event.EventName}");
                }
            }
        }

        private void BN_KickFromActivity_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                if (LB_EventUserSubscription.SelectedIndex >= 0)
                {
                    string selectedItem = LB_EventUserSubscription.SelectedItem.ToString();
                    int activitiesSubId = int.Parse(selectedItem.Split(' ')[0]);

                    var _activity = db.ActivitieSubs.Find(activitiesSubId);
                    if (_activity != null)
                    {
                        db.ActivitieSubs.Remove(_activity);
                        db.SaveChanges();
                        
                        LB_Activities.Items.Remove(selectedItem);

                        LoadSubscriptions();
                    }
                }

            }
        }
    }
} 
       

