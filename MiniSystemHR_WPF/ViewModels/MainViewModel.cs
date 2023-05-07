using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MiniSystemHR_WPF.Commands;
using MiniSystemHR_WPF.Model;
using MiniSystemHR_WPF.Model.Domains;
using MiniSystemHR_WPF.Model.Wrappers;
using MiniSystemHR_WPF.Properties;
using MiniSystemHR_WPF.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MiniSystemHR_WPF.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        public MainViewModel()
        {
            RefreshEmployees();
            AddEmployeeCommand = new RelayCommand(AddEditEmployee);
            EditEmployeeCommand = new RelayCommand(AddEditEmployee, CanEditDeleteEmployee);
            DissmissEmployeeCommand = new AsyncRelayCommand(DissmissEmployee, CanEditDeleteEmployee);
            RefreshEmployeesCommand = new RelayCommand(RefreshEmployees, CanRefreshEmployees);
            SettingsCommand = new RelayCommand(ChangeSettings);
            OnLoadedCommand = new RelayCommand(OnLoaded);
        }

        public ICommand RefreshEmployeesCommand { get; set; }
        public ICommand AddEmployeeCommand { get; set; }
        public ICommand EditEmployeeCommand { get; set; }
        public ICommand DissmissEmployeeCommand { get; set; }
        public ICommand OnLoadedCommand { get; set; }
        public ICommand SettingsCommand { get; set; }


        private EmployeeWrapper _selectedEmployee;
        private ObservableCollection<EmployeeWrapper> _employee;
        private int _selectedGroupId;
        private ObservableCollection<Group> _group;

        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        public EmployeeWrapper SelectedEmployee
        {
            get { return _selectedEmployee; }
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<EmployeeWrapper> Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }
        private async void OnLoaded(object obj)
        {
            if (!ConnectToDatabase())
            {
                var metroWindow = Application.Current.MainWindow as MetroWindow;
                var dialogResults = await metroWindow.ShowMessageAsync($"Nie można nawiązać połączenia z bazą danych.", "Chcesz wprowadzic ustawienia dla połączenia?", MessageDialogStyle.AffirmativeAndNegative);

                if (dialogResults == MessageDialogResult.Negative)
                {
                    Application.Current.Shutdown();
                }
                else
                {
                    var settingWindow = new UserSettingsView(false);
                    settingWindow.ShowDialog();
                }
            }
            else
            {
                RefreshEmployees();
                InitGroups();
            }
        }

        private bool ConnectToDatabase()
        {
            var connectionString = $@"Server={Settings.Default.AdressServer}\{Settings.Default.NameServer};Database={Settings.Default.DatabaseName};User Id={Settings.Default.User};Password={Settings.Default.Password};";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    connection.Close();
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }
        private void RefreshEmployees(object obj)
        {
            RefreshEmployees();
        }
        private bool CanRefreshEmployees(object obj)
        {
            return true;
        }

        private bool CanEditDeleteEmployee(object obj)
        {
            return SelectedEmployee != null;
        }
        private void AddEditEmployee(object obj)
        {
            var addEditEmployeeWindow = new AddEditEmployeeView(obj as EmployeeWrapper);
            addEditEmployeeWindow.Closed += AddEditEmployeeWindow_Closed;
            addEditEmployeeWindow.ShowDialog();
        }

        private void AddEditEmployeeWindow_Closed(object sender, EventArgs e)
        {
            RefreshEmployees();
        }

        private async Task DissmissEmployee(object obj)
        {
            var metroWindow = Application.Current.MainWindow as MetroWindow;
            var dialog = await metroWindow.ShowMessageAsync("Zwalnianie pracownika", $"Czy na pewno chcesz zwolnić pracownika {SelectedEmployee.FirstName} {SelectedEmployee.LastName}?", MessageDialogStyle.AffirmativeAndNegative);

            if (dialog != MessageDialogResult.Affirmative)
                return;

            _repository.DissmissEmployee(SelectedEmployee.Id);

            RefreshEmployees();
        }

        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "Wszystkie" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = 0;
        }

        private void RefreshEmployees()
        {
            Employee = new ObservableCollection<EmployeeWrapper>(_repository.GetEmployees(SelectedGroupId));
        }

        private void ChangeSettings(object obj)
        {
            var ChangeSettingsWindows = new UserSettingsView(true);
            ChangeSettingsWindows.ShowDialog();
        }
    }
}
