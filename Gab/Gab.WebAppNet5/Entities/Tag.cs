using System;
using System.Collections.Generic;

namespace Gab.WebAppNet5.Entities
{
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public ICollection<Product> Products { get; set; }

        // public ICollection<Post> Posts { get; set; }
    }
}
