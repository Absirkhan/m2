﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m2
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public int Rating { get; set; }
        public string ShippingOption { get; set; }

        public string ImagePath { get; set; }
    }

}
