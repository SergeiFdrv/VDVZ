using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDVZ.Models.NHibernate
{
    public class ItemMap : ClassMap<Item>
    {
        public ItemMap()
        {
            Id(item => item.ID);
            Map(item => item.Name);
            Map(item => item.Price);
            Map(item => item.Quantity);
            References(item => item.Order).Cascade.SaveUpdate();
        }
    }
}
