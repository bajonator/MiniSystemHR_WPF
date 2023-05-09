using MiniSystemHR_WPF.Commands;
using MiniSystemHR_WPF.Model;
using System.Windows;
using System.Windows.Input;
using MiniSystemHR_WPF.Views;
using SystemHR_WPF.Model;

namespace MiniSystemHR_WPF.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private LoginSettings _loginSettings;

        public LoginViewModel() 
        {
            ConfirmCommand = new RelayCommand(Confirm);
            ExitCommand = new RelayCommand(Exit);
            _loginSettings = new LoginSettings();
        }


        public ICommand ConfirmCommand { get; set; }
        public ICommand ExitCommand { get; set; }

        public LoginSettings LoginSettings
        {
            get { return _loginSettings; }
            set
            {
                _loginSettings = value;
                OnPropertyChanged();
            }
        }


        private void Confirm(object obj)
        {
            var login = obj as LoginParams;
            LoginSettings.Password = login.PasswordBox.Password;

            if (Login())
            {
                CloseWindow(login.Window);
            }
        }

        private bool Login()
        {
            if (LoginSettings.Password == "1" && LoginSettings.Login == "admin")
                return true;
            return false;

        }

        private void Exit(object obj)
        {
            Application.Current.Shutdown();
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}
