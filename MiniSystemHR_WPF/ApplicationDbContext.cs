using MiniSystemHR_WPF.Model;
using MiniSystemHR_WPF.Model.Configurations;
using MiniSystemHR_WPF.Model.Domains;
using System;
using System.Data.Entity;
using System.Linq;

namespace MiniSystemHR_WPF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
            : base("name=ApplicationDbContext")
        {
        }

        public DbSet<Employee> Emplyees { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<EmployeedTime> EmployeedTimes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new EmployeeConfiguration());
            modelBuilder.Configurations.Add(new GroupConfiguration());
            modelBuilder.Configurations.Add(new EmployeedTimesConfiguration());
        }
    }
}