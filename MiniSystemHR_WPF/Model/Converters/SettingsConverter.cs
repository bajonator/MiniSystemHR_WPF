using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSystemHR_WPF.Model.Converters
{
    public static class SettingsConverter
    {
        public static UserSettings ToDao(this UserSettings model)
        {
            return new UserSettings
            {
                UserId = model.UserId,
                Id = model.Id,
                Address = model.Address,
                Server = model.Server,
                DatabaseName = model.DatabaseName,
                UserName = model.UserName,
                Password = model.Password,
            };
        }
    }
}
