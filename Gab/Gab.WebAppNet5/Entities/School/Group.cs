using System;
using System.Collections.Generic;

namespace Gab.WebAppNet5.Entities.School
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<Student> Students { get; set; }

        public Guid TeacherId { get; set; }
        public Teacher Teacher { get; set; }

    }
}
