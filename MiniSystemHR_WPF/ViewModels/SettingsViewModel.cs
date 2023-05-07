using MiniSystemHR_WPF.Commands;
using MiniSystemHR_WPF.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace MiniSystemHR_WPF.ViewModels
{
    internal class SettingsViewModel : ViewModelBase
    {
        public SettingsViewModel(bool closeWindow)
        {
            CancelCommand = new RelayCommand(Cancel);
            ConfirmChangeCommand = new RelayCommand(SaveChanges);
            _userSettings = new UserSettings();
            _closeWindow = closeWindow;
        }

        public ICommand CancelCommand { get; set; }
        public ICommand ConfirmChangeCommand { get; set; }

        private readonly bool _closeWindow;
        private UserSettings _userSettings;

        public UserSettings UserSettings
        {
            get
            {
                return _userSettings;
            }
            set
            {
                _userSettings = value;
                OnPropertyChanged();
            }
        }


        private void SaveChanges(object obj)
        {
            if (!UserSettings.IsValid)
                return;

            UserSettings.SaveSettings();            
            RestartApp();
        }


        private void Cancel(object obj)
        {
            if (_closeWindow)
                CloseWindow(obj as Window);
            else
                Application.Current.Shutdown();
        }
        private void CloseWindow(Window window)
        {
            window.Close();
        }
        private void RestartApp()
        {
            Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }
    }
}
