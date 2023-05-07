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
                    .Emplyees
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
                    context.EmployeedTimes.FirstOrDefault(x => x.EmployeeId == employee.Id).StartDate = Convert.ToDateTime(times.FirstOrDefault(x => x.EmployeeId == employee.Id).StartDate);

                if (!string.IsNullOrWhiteSpace(employeeWrapper.EndDate.ToString()))
                    context.EmployeedTimes.FirstOrDefault(x => x.EmployeeId == employee.Id).EndDate = Convert.ToDateTime(times.FirstOrDefault(x => x.EmployeeId == employee.Id).EndDate);

                context.SaveChanges();
            }
        }

        private object GetEmployeesTimes(ApplicationDbContext context, Employee employee)
        {
            return context
                .EmployeedTimes
                .Where(x => x.EmployeeId == employee.Id).ToList();
        }

        private void UpdateProperties(ApplicationDbContext context, Employee employee)
        {
            var employeeToUpdate = context.Emplyees.Find(employee.Id);
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
                var dbEmployee = context.Emplyees.Add(employee);

                employeedtimes.ForEach(x =>
                {
                    x.EmployeeId = dbEmployee.Id;
                    context.EmployeedTimes.Add(x);
                });

                context.SaveChanges();
            }
        }

        public void DissmissEmployee(int id)
        {
            using (var context = new ApplicationDbContext())
            {
                var employeeToDissmiss = context.Emplyees.Find(id);
                context.EmployeedTimes.FirstOrDefault(x => x.EmployeeId == id).EndDate = DateTime.Now;
                context.Emplyees.FirstOrDefault(x => x.Id == id).GroupId = 2;

                context.SaveChanges();
            }
        }
    }
}
