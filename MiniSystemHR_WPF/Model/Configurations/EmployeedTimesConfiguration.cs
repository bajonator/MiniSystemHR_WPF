using MiniSystemHR_WPF.Model.Domains;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSystemHR_WPF.Model.Configurations
{
    public class EmployeedTimesConfiguration : EntityTypeConfiguration<EmployeedTime>
    {
        public EmployeedTimesConfiguration()
        {
            ToTable("dbo.EmployeedTimes");

            HasKey(x => x.Id);
        }
    }
}
