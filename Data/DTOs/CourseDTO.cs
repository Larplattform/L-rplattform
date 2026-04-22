using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class CourseDTO
    {
        public string SubjectName { get; set; } = string.Empty;
        public int TotalMarks { get; set; }

        public string ClassName { get; set; } = string.Empty;
        public int TeacherID { get; set; }

        public ICollection<UserDTO> Users { get; set; } = new List<UserDTO>();
    }
}
