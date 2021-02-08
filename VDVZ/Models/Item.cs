using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDVZ.Models
{
    public class Item
    {
        public virtual int ID { get; set; }

        public virtual string Name { get; set; }

        public virtual int Quantity { get; set; }

        public virtual decimal Price { get; set; }

        public virtual Order Order { get; set; }

        public override string ToString() => $"{ID} {Name}";
    }
}
