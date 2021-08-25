using System;

namespace Gab.WebAppNet5.Entities.School
{
    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid GroupId { get; set; }
        public Group Group { get; set; }
    }
}
