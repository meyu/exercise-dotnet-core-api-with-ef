using System;
using System.Collections.Generic;

namespace exercise_dotnet_core_api_with_ef.Models
{
    public partial class VwCourseStudentCount
    {
        public long? DepartmentId { get; set; }
        public string Name { get; set; }
        public long? CourseId { get; set; }
        public string Title { get; set; }
        public byte[] StudentCount { get; set; }
    }
}
