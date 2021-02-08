using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VDVZ.Models
{
    public class Order
    {
        public virtual int ID { get; set; }

        public virtual string Counterparty { get; set; }

        public virtual DateTime DateTime { get; set; }

        public virtual Employee Author { get; set; }

        public virtual Item Item { get; set; }

        public override string ToString() => $"{ID} {Counterparty}";
    }
}
