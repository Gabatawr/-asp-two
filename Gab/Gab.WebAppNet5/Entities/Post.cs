using System;
using System.Collections.Generic;

namespace Gab.WebAppNet5.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }

        // 1:many
        public Category Category { get; set; }

        // many:many
        // public ICollection<Tag> Tags { get; set; }
    }
}
