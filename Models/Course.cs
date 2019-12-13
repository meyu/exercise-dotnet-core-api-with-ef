using System;
using System.Collections.Generic;

namespace exercise_dotnet_core_api_with_ef.Models
{
    public partial class Course
    {
        public Course()
        {
            CourseInstructor = new HashSet<CourseInstructor>();
            Enrollment = new HashSet<Enrollment>();
        }

        public long CourseId { get; set; }
        public string Title { get; set; }
        public long Credits { get; set; }
        public long DepartmentId { get; set; }
        public DateTime DateModified { get; set; }

        public virtual Department Department { get; set; }
        public virtual ICollection<CourseInstructor> CourseInstructor { get; set; }
        public virtual ICollection<Enrollment> Enrollment { get; set; }
    }
}
