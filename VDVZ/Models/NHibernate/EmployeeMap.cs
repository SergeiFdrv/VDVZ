using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDVZ.Models.NHibernate
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Id(employee => employee.ID);
            Map(employee => employee.FirstName);
            Map(employee => employee.MiddleName);
            Map(employee => employee.LastName);
            Map(employee => employee.BirthDate);
            Map(employee => employee.Gender);
            References(employee => employee.Division).Cascade.SaveUpdate();
            //References(employee => employee.RunsDivision).Cascade.SaveUpdate();
            //HasMany(employee => employee.Orders).Inverse();
        }
    }
}
