using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDVZ.Models.NHibernate
{
    public class DivisionMap : ClassMap<Division>
    {
        public DivisionMap()
        {
            Id(division => division.ID);
            Map(division => division.Name);
            References(division => division.Chief).Cascade.SaveUpdate();
            //HasMany(division => division.Employees).Inverse();
        }
    }
}
