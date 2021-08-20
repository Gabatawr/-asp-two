using System;
using System.Collections.Generic;

namespace Gab.WebAppNet5.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Vendor Vendor { get; set; }

        public ICollection<Tag> Tags { get; set; }
    }
}
