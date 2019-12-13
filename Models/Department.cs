using System;
using System.Collections.Generic;

namespace exercise_dotnet_core_api_with_ef.Models
{
    public partial class Department
    {
        public Department()
        {
            Course = new HashSet<Course>();
        }

        public long DepartmentId { get; set; }
        public string Name { get; set; }
        public byte[] Budget { get; set; }

        // TODO: 時間格式要用 byte[]?
        public byte[] StartDate { get; set; }
        public long? InstructorId { get; set; }
        public byte[] RowVersion { get; set; }
        public DateTime DateModified { get; set; }
        public Boolean IsDeleted { get; set; }


        public virtual Person Instructor { get; set; }
        public virtual ICollection<Course> Course { get; set; }
    }
}
