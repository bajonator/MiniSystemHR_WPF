using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace SystemHR_WPF.Model
{
    public class LoginParams
    {
        public LoginParams()
        {
            Window = new Window();
        }
        public Window Window { get; set; }
        public PasswordBox PasswordBox { get; set; }
    }
}
