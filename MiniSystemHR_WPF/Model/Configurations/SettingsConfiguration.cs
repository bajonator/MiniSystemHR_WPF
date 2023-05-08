using MiniSystemHR_WPF.Model.Domains;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSystemHR_WPF.Model.Configurations
{
    public class SettingsConfiguration : EntityTypeConfiguration<UserSettings>
    {
        public SettingsConfiguration() 
        {
            ToTable("dbo.Settings");

            Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }
}
