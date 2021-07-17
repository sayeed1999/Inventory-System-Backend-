using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.EntityLayer
{
    public class Product : Table
    {
        public Product()
        {
            this.Stocks = new HashSet<Stock>();
            this.Sales = new HashSet<Sale>();
        }
        public string Name { get; set; }
        public float Price { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
