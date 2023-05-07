using MiniSystemHR_WPF.Model.Wrappers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSystemHR_WPF.Model.Domains
{
    public class Employee
    {
        public Employee()
        {
            Times = new Collection<EmployeedTime>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Wage { get; set; }
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public ICollection<EmployeedTime> Times { get; set; }
    }
}
