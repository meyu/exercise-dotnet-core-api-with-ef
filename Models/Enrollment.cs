using System;
using System.Collections.Generic;

namespace exercise_dotnet_core_api_with_ef.Models
{
    public partial class Enrollment
    {
        public long EnrollmentId { get; set; }
        public long CourseId { get; set; }
        public long StudentId { get; set; }
        public long? Grade { get; set; }

        public virtual Course Course { get; set; }
        public virtual Person Student { get; set; }
    }
}
