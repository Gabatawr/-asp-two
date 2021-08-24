using System;

namespace Gab.WebAppNet5.Entities
{
    public class Catalog
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid? ParentId { get; set; }
        public Catalog Parent { get; set; }
    }
}
