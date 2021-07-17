using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.EntityLayer
{
    public class Sale : Table
    {
        public Product Product { get; set; }
        public int ProductId { get; set; }
        public float Quantity { get; set; }
        public Customer Customer { get; set; }
        public int CustomerId { get; set; }
        public DateTime Date { get; set; }
    }
}
