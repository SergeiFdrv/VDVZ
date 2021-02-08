using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using VDVZ.Models;
using VDVZ.Models.NHibernate;

namespace VDVZ
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            Divisions = new ObservableCollection<Division>(Session.Query<Division>());
            Employees = new ObservableCollection<Employee>(Session.Query<Employee>());
            Items = new ObservableCollection<Item>(Session.Query<Item>());
            Orders = new ObservableCollection<Order>(Session.Query<Order>());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?
                .Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<Division> _Divisions;
        public ObservableCollection<Division> Divisions
        {
            get => _Divisions;
            set
            {
                _Divisions = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Employee> _Employees;
        public ObservableCollection<Employee> Employees
        {
            get => _Employees;
            set
            {
                _Employees = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Item> _Items;
        public ObservableCollection<Item> Items
        {
            get => _Items;
            set
            {
                _Items = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Order> _Orders;
        public ObservableCollection<Order> Orders
        {
            get => _Orders;
            set
            {
                _Orders = value;
                OnPropertyChanged();
            }
        }

        private ISession Session { get; } = NHibernateHelper.OpenSession();

        private Employee _SelectedEmployee;
        public Employee SelectedEmployee
        {
            get => _SelectedEmployee ?? (_SelectedEmployee = new Employee());
            set
            {
                _SelectedEmployee = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _SaveEmployee;
        public RelayCommand SaveEmployee
        {
            get
            {
                return _SaveEmployee ??
                  (_SaveEmployee = new RelayCommand(obj =>
                  {
                      using (Session.BeginTransaction())
                      {
                          SelectedEmployee.ID = (int)Session.Save(SelectedEmployee);
                          var employee = Employees
                              .FirstOrDefault(e => e.ID == SelectedEmployee.ID);
                          if (employee is null) Employees.Add(SelectedEmployee);
                          Session.GetCurrentTransaction()?.Commit();
                      }
                      System.Windows.MessageBox.Show(
                          $"Сотрудник {SelectedEmployee.FirstName} " +
                          $"{SelectedEmployee.LastName} сохранён.");
                  }));
            }
        }

        private RelayCommand _DeleteEmployee;
        public RelayCommand DeleteEmployee
        {
            get
            {
                return _DeleteEmployee ??
                  (_DeleteEmployee = new RelayCommand(obj =>
                  {
                      using (Session.BeginTransaction())
                      {
                          Session.Delete(SelectedEmployee);
                          Session.GetCurrentTransaction()?.Commit();
                      }
                      Employees.Remove(SelectedEmployee);
                      SelectedEmployee = new Employee();
                      System.Windows.MessageBox.Show("Сотрудник удалён.");
                  }));
            }
        }

        private Division _SelectedDivision;
        public Division SelectedDivision
        {
            get => _SelectedDivision ?? (_SelectedDivision = new Division());
            set
            {
                _SelectedDivision = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _SaveDivision;
        public RelayCommand SaveDivision
        {
            get
            {
                return _SaveDivision ??
                  (_SaveDivision = new RelayCommand(obj =>
                  {
                      using (Session.BeginTransaction())
                      {
                          SelectedDivision.ID = (int)Session.Save(SelectedDivision);
                          var division = Divisions
                              .FirstOrDefault(e => e.ID == SelectedDivision.ID);
                          if (division is null) Divisions.Add(SelectedDivision);
                          Session.GetCurrentTransaction()?.Commit();
                      }
                      System.Windows.MessageBox.Show(
                          $"Подразделение {SelectedDivision.Name} сохранено.");
                  }));
            }
        }

        private RelayCommand _DeleteDivision;
        public RelayCommand DeleteDivision
        {
            get
            {
                return _DeleteDivision ??
                  (_DeleteDivision = new RelayCommand(obj =>
                  {
                      using (Session.BeginTransaction())
                      {
                          Session.Delete(SelectedDivision);
                          Session.GetCurrentTransaction()?.Commit();
                      }
                      Divisions.Remove(SelectedDivision);
                      SelectedDivision = new Division();
                      System.Windows.MessageBox.Show("Подразделение удалено.");
                  }));
            }
        }
    }
}
