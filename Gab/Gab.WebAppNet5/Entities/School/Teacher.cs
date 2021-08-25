using System;

namespace Gab.WebAppNet5.Entities.School
{
    public class Teacher
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Group Group { get; set; }
    }
}
