using System.Collections.Generic;

namespace Inventory.EntityLayer
{
    public class Category : Table
    {
        public Category()
        {
            this.Products = new HashSet<Product>();
        }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
