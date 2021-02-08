using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDVZ.Models
{
    public class Division
    {
        public virtual int ID { get; set; }

        public virtual string Name { get; set; }

        public virtual Employee Chief { get; set; }

        private List<Employee> _Employees;
        public virtual List<Employee> Employees
        {
            get => _Employees ?? (_Employees = new List<Employee>());
            set => _Employees = value;
        }

        public override string ToString() => $"{ID} {Name}";
    }
}
