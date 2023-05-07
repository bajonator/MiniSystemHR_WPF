using MiniSystemHR_WPF.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.Collections.ObjectModel;
using MiniSystemHR_WPF.Model.Wrappers;
using MiniSystemHR_WPF.Model.Domains;

namespace MiniSystemHR_WPF.ViewModels
{
    public class AddEditEmploeeViewModel : ViewModelBase
    {
        private Repository _repository = new Repository();

        public AddEditEmploeeViewModel(EmployeeWrapper employee = null)
        {
            ConfirmCommand = new RelayCommand(Confirm);
            CloseCommand = new RelayCommand(Close);

            if (employee == null)
            {
                Employee = new EmployeeWrapper();
            }
            else
            {
                Employee = employee;
                IsUpdate = true;
            }

            InitGroups();

        }

        public ICommand ConfirmCommand { get; set; }
        public ICommand CloseCommand { get; set; }


        private EmployeeWrapper _employee;
        public EmployeeWrapper Employee
        {
            get { return _employee; }
            set
            {
                _employee = value;
                OnPropertyChanged();
            }
        }

        private bool _isUpdate;
        public bool IsUpdate
        {
            get { return _isUpdate; }
            set
            {
                _isUpdate = value;
                OnPropertyChanged();
            }
        }

        private int _selectedGroupId;
        public int SelectedGroupId
        {
            get { return _selectedGroupId; }
            set
            {
                _selectedGroupId = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Group> _group;
        public ObservableCollection<Group> Groups
        {
            get { return _group; }
            set
            {
                _group = value;
                OnPropertyChanged();
            }
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }

        private void Close(object obj)
        {
            CloseWindow(obj as Window);
        }

        private void Confirm(object obj)
        {
            if (!Employee.IsValid)
                return;

            if (!IsUpdate)
                AddEmployee();
            else
                UpdateEmployee();

            CloseWindow(obj as Window);
        }
        private void InitGroups()
        {
            var groups = _repository.GetGroups();
            groups.Insert(0, new Group { Id = 0, Name = "-- brak --" });

            Groups = new ObservableCollection<Group>(groups);

            SelectedGroupId = Employee.Group.Id;
        }
        private void UpdateEmployee()
        {
            _repository.UpdateStudent(Employee);
        }

        private void AddEmployee()
        {
            _repository.AddStudent(Employee);
        }
    }
}
