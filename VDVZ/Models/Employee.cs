using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDVZ.Models
{
    public class Employee
    {
        public virtual int ID { get; set; }

        public virtual string FirstName { get; set; }

        public virtual string MiddleName { get; set; }

        public virtual string LastName { get; set; }

        public virtual DateTime BirthDate { get; set; }

        public virtual Gender Gender { get; set; }

        public virtual Division Division { get; set; }

        public virtual Division RunsDivision { get; set; }

        private List<Order> _Orders;
        public virtual List<Order> Orders
        {
            get => _Orders ?? (_Orders = new List<Order>());
            set => _Orders = value;
        }

        public override string ToString() => $"{ID} {FirstName} {LastName}";
    }

    public enum Gender { M, F }
}
