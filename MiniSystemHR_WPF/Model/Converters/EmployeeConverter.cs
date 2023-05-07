using MiniSystemHR_WPF.Model.Domains;
using MiniSystemHR_WPF.Model.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MiniSystemHR_WPF.Model.Converters
{
    public static class EmployeeConverter
    {
        public static EmployeeWrapper ToWrapper(this Employee model)
        {
            return new EmployeeWrapper
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Wage = model.Wage,
                EmployedStatus = model.Group.Name,
                StartDate = model.Times.FirstOrDefault(x => x.EmployeeId == model.Id).StartDate,
                EndDate = model.Times.FirstOrDefault(x => x.EmployeeId == model.Id).EndDate,
                Group = new GroupWrapper
                {
                    Id = model.Group.Id,
                    Name = model.Group.Name
                },
            };
        }

        public static Employee ToDao(this EmployeeWrapper model)
        {
            return new Employee
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Wage = model.Wage,
                GroupId = model.Group.Id,
            };
        }

        public static List<EmployeedTime> ToTimeDao(this EmployeeWrapper model)
        {
            var times = new List<EmployeedTime>();

            if (!string.IsNullOrWhiteSpace(model.StartDate.ToString()))
            {
                times.Add(new EmployeedTime
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    EmployeeId = model.Id
                });
            }
            if (!string.IsNullOrWhiteSpace(model.EndDate.ToString()))
            {
                times.Add(new EmployeedTime
                {
                    StartDate = model.StartDate,
                    EndDate = model.EndDate,
                    EmployeeId = model.Id
                });
            }
            return times;
        }
    }
}
