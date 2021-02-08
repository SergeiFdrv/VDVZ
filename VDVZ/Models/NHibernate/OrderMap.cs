using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDVZ.Models.NHibernate
{
    public class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(order => order.ID);
            Map(order => order.Counterparty);
            Map(order => order.DateTime).Formula("now()");
            References(order => order.Author).Cascade.SaveUpdate();
            References(order => order.Item).Cascade.SaveUpdate();
        }
    }
}
