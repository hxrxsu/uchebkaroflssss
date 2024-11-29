using Microsoft.EntityFrameworkCore;

namespace uchebkaroflssss.Data
{
    internal class ApplicationContext : DbContext
    {

        public DbSet<User> Users => Set<User>();
        public DbSet<Activity> Activities => Set<Activity>();
        public DbSet<ActivitySubscription> ActivitieSubs => Set<ActivitySubscription>();
        public DbSet<Event> Events => Set<Event>();
        public ApplicationContext() => Database.EnsureCreated();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=EventsApp.db");
        }
    }
}
