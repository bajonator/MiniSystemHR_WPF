using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSystemHR_WPF.Model
{
    public class LoginSettings : IDataErrorInfo
    {
        public string Login { get; set; }
        public string Password { get; set; }

        private bool _isLoginValid;
        private bool _isPasswordValid;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Login):
                        if (string.IsNullOrWhiteSpace(Login))
                        {
                            Error = "Pole Login jest wymagane.";
                            _isLoginValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isLoginValid = true;
                        }
                        break;
                    case nameof(Password):
                        if (string.IsNullOrWhiteSpace(Password))
                        {
                            Error = "Pole Hasło jest wymagane.";
                            _isPasswordValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isPasswordValid = true;
                        }
                        break;

                    default:
                        break;
                }
                return Error;
            }
        }
        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
                return _isLoginValid && _isPasswordValid;
            }
        }
    }
}
