using System;
using System.Collections.Generic;

namespace exercise_dotnet_core_api_with_ef.Models
{
    public partial class OfficeAssignment
    {
        public long InstructorId { get; set; }
        public string Location { get; set; }

        public virtual Person Instructor { get; set; }
    }
}
