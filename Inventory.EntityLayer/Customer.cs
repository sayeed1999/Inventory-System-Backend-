﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.EntityLayer
{
    public class Customer : Table
    {
        public Customer()
        {
            this.Sales = new HashSet<Sale>();
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public long Contact { get; set; }
        public ICollection<Sale> Sales { get; set; }
    }
}
