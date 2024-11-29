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
    /// Interaction logic for JuryEventPage.xaml
    /// </summary>
    public partial class JuryEventPage : Page
    {
        public JuryEventPage(User currentUser)
        {
            InitializeComponent();
            LoadEvents();
        }

        private void LoadEvents()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var events = db.Events.ToList();
                LB_Events.ItemsSource = events;
            }
        }

        private void LB_Events_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LB_Events.SelectedItem is Event selectedEvent)
            {
                TB_EventScore.Text = selectedEvent.EventScore.ToString();
            }
        }

        private void BN_RateEvent_Click(object sender, RoutedEventArgs e)
        {
            if (LB_Events.SelectedItem is Event selectedEvent && int.TryParse(TB_EventScore.Text, out int score))
            {
                using (ApplicationContext db = new ApplicationContext())
                {
                    var eventToUpdate = db.Events.Find(selectedEvent.EventId);
                    if (eventToUpdate != null)
                    {
                        eventToUpdate.EventScore = score;
                        db.SaveChanges();
                        MessageBox.Show("Оценка сохранена.");
                    }
                    else
                    {
                        MessageBox.Show("Мероприятие не найдено.");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите мероприятие и введите оценку.");
            }
        }
    }
}
