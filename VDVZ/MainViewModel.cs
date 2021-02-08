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

        private void DeleteObject<T>(T obj, ObservableCollection<T> col)
        {
            using (Session.BeginTransaction())
            {
                Session.Delete(obj);
                Session.GetCurrentTransaction()?.Commit();
            }
            col.Remove(obj);
        }

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
                            var ds = Session.Query<Division>();
                            foreach (var d in ds)
                            {
                                if (d.Chief.ID == SelectedEmployee.ID) d.Chief = null;
                                Session.SaveOrUpdate(d);
                            }
                            var os = Session.Query<Order>();
                            foreach (var o in os)
                            {
                                if (o.Author.ID == SelectedEmployee.ID) o.Author = null;
                                Session.SaveOrUpdate(o);
                            }
                            Session.Delete(SelectedEmployee);
                            Session.GetCurrentTransaction()?.Commit();
                        }
                        Employees.Remove(SelectedEmployee);
                        //DeleteObject(SelectedEmployee, Employees);
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
                            var es = Session.Query<Employee>();
                            foreach (var e in es)
                            {
                                if (e.Division.ID == SelectedDivision.ID) e.Division = null;
                                Session.SaveOrUpdate(e);
                            }
                            Session.Delete(SelectedDivision);
                            Session.GetCurrentTransaction()?.Commit();
                        }
                        Divisions.Remove(SelectedDivision);
                        //DeleteObject(SelectedDivision, Divisions);
                        SelectedDivision = new Division();
                        System.Windows.MessageBox.Show("Подразделение удалено.");
                    }));
            }
        }

        private Item _SelectedItem;
        public Item SelectedItem
        {
            get => _SelectedItem ?? (_SelectedItem = new Item());
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _SaveItem;
        public RelayCommand SaveItem
        {
            get
            {
                return _SaveItem ??
                    (_SaveItem = new RelayCommand(obj =>
                    {
                        using (Session.BeginTransaction())
                        {
                            SelectedItem.ID = (int)Session.Save(SelectedItem);
                            var item = Items
                                .FirstOrDefault(e => e.ID == SelectedItem.ID);
                            if (item is null) Items.Add(SelectedItem);
                            Session.GetCurrentTransaction()?.Commit();
                        }
                        System.Windows.MessageBox.Show(
                            $"Товар {SelectedItem.Name} сохранён.");
                    }));
            }
        }

        private RelayCommand _DeleteItem;
        public RelayCommand DeleteItem
        {
            get
            {
                return _DeleteItem ??
                    (_DeleteItem = new RelayCommand(obj =>
                    {
                        using (Session.BeginTransaction())
                        {
                            var os = Session.Query<Order>();
                            foreach (var o in os)
                            {
                                if (o.Item.ID == SelectedItem.ID) o.Item = null;
                                Session.SaveOrUpdate(o);
                            }
                            Session.Delete(SelectedItem);
                            Session.GetCurrentTransaction()?.Commit();
                        }
                        Items.Remove(SelectedItem);
                        //DeleteObject(SelectedItem, Items);
                        SelectedItem = new Item();
                        System.Windows.MessageBox.Show("Товар удалён.");
                    }));
            }
        }

        private Order _SelectedOrder;
        public Order SelectedOrder
        {
            get => _SelectedOrder ?? (_SelectedOrder = new Order());
            set
            {
                _SelectedOrder = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _SaveOrder;
        public RelayCommand SaveOrder
        {
            get
            {
                return _SaveOrder ??
                    (_SaveOrder = new RelayCommand(obj =>
                    {
                        using (Session.BeginTransaction())
                        {
                            SelectedOrder.ID = (int)Session.Save(SelectedOrder);
                            var order = Orders
                                .FirstOrDefault(e => e.ID == SelectedOrder.ID);
                            if (order is null) Orders.Add(SelectedOrder);
                            Session.GetCurrentTransaction()?.Commit();
                        }
                        System.Windows.MessageBox.Show(
                            $"Заказ для {SelectedOrder.Counterparty} от {SelectedOrder.DateTime.Date} сохранён.");
                    }));
            }
        }

        private RelayCommand _DeleteOrder;
        public RelayCommand DeleteOrder
        {
            get
            {
                return _DeleteOrder ??
                    (_DeleteOrder = new RelayCommand(obj =>
                    {
                        DeleteObject(SelectedOrder, Orders);
                        SelectedOrder = new Order();
                        System.Windows.MessageBox.Show("Заказ удалён.");
                    }));
            }
        }
    }
}
