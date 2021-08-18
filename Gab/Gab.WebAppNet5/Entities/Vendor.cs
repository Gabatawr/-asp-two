using System;
using System.Collections.Generic;

namespace Gab.WebAppNet5.Entities
{
    public class Vendor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}
