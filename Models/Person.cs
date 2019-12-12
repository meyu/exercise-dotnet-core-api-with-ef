using System;
using System.Collections.Generic;

namespace exercise_dotnet_core_api_with_ef.Models
{
    public partial class Person
    {
        public Person()
        {
            CourseInstructor = new HashSet<CourseInstructor>();
            Department = new HashSet<Department>();
            Enrollment = new HashSet<Enrollment>();
        }

        public long Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public byte[] HireDate { get; set; }
        public byte[] EnrollmentDate { get; set; }
        public string Discriminator { get; set; }

        public virtual OfficeAssignment OfficeAssignment { get; set; }
        public virtual ICollection<CourseInstructor> CourseInstructor { get; set; }
        public virtual ICollection<Department> Department { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
