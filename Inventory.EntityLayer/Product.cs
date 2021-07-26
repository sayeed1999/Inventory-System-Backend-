using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.EntityLayer
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public float AvailableQuantity { get; set; }
        public ICollection<Stock> Stocks { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
