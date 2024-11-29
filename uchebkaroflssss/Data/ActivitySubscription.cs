using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uchebkaroflssss.Data
{
    internal class ActivitySubscription
    {
        public int ActivitySubscriptionId {  get; set; }
        public string? ActivityNameSub { get; set; }
        public string? EventNameSub {  get; set; }
        public DateTime SubscribedDate { get; set; }
        public int EventId { get; set; }
        public Event Event { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int ActivityId {  get; set; }
        public Activity Activity { get; set; }
    }
}
