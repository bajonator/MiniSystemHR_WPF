using MiniSystemHR_WPF.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSystemHR_WPF.Model
{
    public class UserSettings : IDataErrorInfo
    {

        public int Id { get; set; }
        public int UserId { get; set; }

        public string Address
        {
            get
            {
                return Settings.Default.AdressServer;
            }
            set
            {
                Settings.Default.AdressServer = value;
            }
        }
        public string Server
        {
            get
            {
                return Settings.Default.NameServer;
            }
            set
            {
                Settings.Default.NameServer = value;
            }
        }
        public string DatabaseName
        {
            get
            {
                return Settings.Default.DatabaseName;
            }
            set
            {
                Settings.Default.DatabaseName = value;
            }
        }
        public string UserName
        {
            get
            {
                return Settings.Default.User;
            }
            set
            {
                Settings.Default.User = value;
            }
        }
        public string Password
        {
            get
            {
                return Settings.Default.Password;
            }
            set
            {
                Settings.Default.Password = value;
            }
        }
        

        private bool _isAddressValid;
        private bool _isServerValid;
        private bool _isUserNameValid;
        private bool _isDatabaseNameValid;
        private bool _isPasswordValid;

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case nameof(Address):
                        if (string.IsNullOrWhiteSpace(Address))
                        {
                            Error = "Pole Adres servera jest wymagane.";
                            _isAddressValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isAddressValid = true;
                        }
                        break;
                    case nameof(Server):
                        if (string.IsNullOrWhiteSpace(Server))
                        {
                            Error = "Pole Nazwa Servera jest wymagane.";
                            _isServerValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isServerValid = true;
                        }
                        break;
                    case nameof(DatabaseName):
                        if (string.IsNullOrWhiteSpace(DatabaseName))
                        {
                            Error = "Pole Nazwa bazy danych jest wymagane.";
                            _isDatabaseNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isDatabaseNameValid = true;
                        }
                        break;
                    case nameof(UserName):
                        if (string.IsNullOrWhiteSpace(UserName))
                        {
                            Error = "Pole Nazwa użytkownika jest wymagane.";
                            _isUserNameValid = false;
                        }
                        else
                        {
                            Error = string.Empty;
                            _isUserNameValid = true;
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

        public static void SaveSettings()
        {
            Settings.Default.Save();
        }

        public string Error { get; set; }

        public bool IsValid
        {
            get
            {
                return _isAddressValid && _isServerValid && _isUserNameValid && _isDatabaseNameValid && _isPasswordValid;
            }
        }
    }
}
