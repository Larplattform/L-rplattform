using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class UpdateCourseDTO
    {
        public string SubjectName { get; set; } = string.Empty;
        public int TotalMarks { get; set; }

        public string ClassName { get; set; } = string.Empty;
        public int TeacherID { get; set; }

       
    }
}
