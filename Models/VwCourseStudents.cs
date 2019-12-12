using System;
using System.Collections.Generic;

namespace exercise_dotnet_core_api_with_ef.Models
{
    public partial class VwCourseStudents
    {
        public long? DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public long? CourseId { get; set; }
        public string CourseTitle { get; set; }
        public long? StudentId { get; set; }
        public byte[] StudentName { get; set; }
    }
}
