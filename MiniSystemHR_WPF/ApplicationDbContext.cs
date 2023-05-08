using MiniSystemHR_WPF.Model;
using MiniSystemHR_WPF.Model.Configurations;
using MiniSystemHR_WPF.Model.Domains;
using MiniSystemHR_WPF.Properties;
using System;
using System.Data.Entity;
using System.Linq;

namespace MiniSystemHR_WPF
{
    public class ApplicationDbContext : DbContext
    {
        private static string _connectionString =
            $@"Server={Settings.Default.AdressServer}\{Settings.Default.NameServer};Database={Settings.Default.DatabaseName};User Id={Settings.Default.User};Password={Settings.Default.Password};";

        public ApplicationDbContext()
            : base(_connectionString)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<EmployeedTime> Times { get; set; }
        public DbSet<UserSettings> UserSettings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new EmployeedTimesConfiguration());
            modelBuilder.Configurations.Add(new SettingsConfiguration());
        }
    }
}