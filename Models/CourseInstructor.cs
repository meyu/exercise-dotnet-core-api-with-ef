using System;
using System.Collections.Generic;

namespace exercise_dotnet_core_api_with_ef.Models
{
    public partial class CourseInstructor
    {
        public long CourseId { get; set; }
        public long InstructorId { get; set; }

        public virtual Course Course { get; set; }
        public virtual Person Instructor { get; set; }
    }
}
