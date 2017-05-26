﻿using System.Collections.Generic;

namespace Model.Registers
{
    public class Supplier
    {
        public long SupplierId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}