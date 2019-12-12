using System;
using System.Collections.Generic;

namespace exercise_dotnet_core_api_with_ef.Models
{
    public partial class VwDepartmentCourseCount
    {
        public long? DepartmentId { get; set; }
        public string Name { get; set; }
        public byte[] CourseCount { get; set; }
    }
}
