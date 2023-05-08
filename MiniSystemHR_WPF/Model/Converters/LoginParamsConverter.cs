using MiniSystemHR_WPF.Views;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using SystemHR_WPF.Model;

namespace MiniSystemHR_WPF.Model.Converters
{
    public class LoginParamsConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            return new LoginParams { Window = values[0] as LoginWindow, PasswordBox = values[1] as PasswordBox };
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
