using MiniSystemHR_WPF.Model.Domains;
using MiniSystemHR_WPF.Model.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using MiniSystemHR_WPF.Model.Converters;
using System.Security.Cryptography.X509Certificates;
using MiniSystemHR_WPF.Model;
using MiniSystemHR_WPF.Properties;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Remoting.Contexts;
using System.Runtime;

namespace MiniSystemHR_WPF
{
    public class Repository
    {
        public List<Group> GetGroups()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Groups.ToList();
            }
        }

        public List<EmployeeWrapper> GetEmployees(int groupId)
        {
            using (var context = new ApplicationDbContext())
            {
                var employees = context
                    .Employees
                    .Include(x => x.Group)
                    .Include(x => x.Times)
                    .AsQueryable();

                if (groupId != 0)
                    employees = employees.Where(x => x.GroupId == groupId);

                return employees
                    .ToList()
                    .Select(x => x.ToWrapper())
                    .ToList();
            }
        }

        public void UpdateStudent(EmployeeWrapper employeeWrapper)
        {
            var employee = employeeWrapper.ToDao();
            var times = employeeWrapper.ToTimeDao();
            using (var context = new ApplicationDbContext())
            {
                UpdateProperties(context, employee);

                var employeedTimes = GetEmployeesTimes(context, employee);

                if (!string.IsNullOrWhiteSpace(employeeWrapper.StartDate.ToString()))
                    context
                        .Times
                        .FirstOrDefault(x => x.EmployeeId == employee.Id)
                        .StartDate = Convert.ToDateTime(times.FirstOrDefault(x => x.EmployeeId == employee.Id).StartDate);

                if (!string.IsNullOrWhiteSpace(employeeWrapper.EndDate.ToString()))
                    context
                        .Times
                        .FirstOrDefault(x => x.EmployeeId == employee.Id)
                        .EndDate = Convert.ToDateTime(times.FirstOrDefault(x => x.EmployeeId == employee.Id).EndDate);

                context.SaveChanges();
            }
        }

        private object GetEmployeesTimes(ApplicationDbContext context, Employee employee)
        {
            return context
                .Times
                .Where(x => x.EmployeeId == employee.Id).ToList();
        }

        private void UpdateProperties(ApplicationDbContext context, Employee employee)
        {
            var employeeToUpdate = context.Employees.Find(employee.Id);
            employeeToUpdate.FirstName = employee.FirstName;
            employeeToUpdate.LastName = employee.LastName;
            employeeToUpdate.Wage = employee.Wage;
            employeeToUpdate.GroupId = employee.GroupId;
        }

        public void AddStudent(EmployeeWrapper employeeWrapper)
        {
            var employee = employeeWrapper.ToDao();
            var employeedtimes = employeeWrapper.ToTimeDao();

            using (var context = new ApplicationDbContext())
            {
                var dbEmployee = context.Employees.Add(employee);

                employeedtimes.ForEach(x =>
                {
                    x.EmployeeId = dbEmployee.Id;
                    context.Times.Add(x);
                });

                context.SaveChanges();
            }
        }

        public void DissmissEmployee(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var employeeToDissmiss = context.Employees.Find(id);
                context.Times.FirstOrDefault(x => x.EmployeeId == id).EndDate = DateTime.Now;
                context.Employees.FirstOrDefault(x => x.Id == id).GroupId = 2;

                context.SaveChanges();
            }
        }

        public void SaveSettings(UserSettings userSettings)
        {
            using (var context = new ApplicationDbContext())
            {
                var dbsettings = context.UserSettings.FirstOrDefault(x => x.Id == userSettings.Id);

                if (dbsettings == null)
                {
                    var settings = userSettings.ToDao();
                    context.UserSettings.Add(settings);
                }
                else
                {
                    dbsettings.Server = userSettings.Server;
                    dbsettings.UserName = userSettings.UserName;
                    dbsettings.Password = userSettings.Password;
                    dbsettings.Address = userSettings.Address;
                    dbsettings.DatabaseName = userSettings.DatabaseName;
                    dbsettings.UserId = userSettings.UserId;
                    dbsettings.Id = userSettings.Id;
                }
                context.SaveChanges();                
            }
        }

        public string GetSettings(int userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var settings = context.UserSettings.FirstOrDefault(x => x.Id == userId);
                if (settings != null)
                return $@"Server={settings.Address}\{settings.Server};Database={settings.DatabaseName};User Id={settings.UserName};Password={settings.Password};";
                else
                    return null;
            }
        }
    }
}
