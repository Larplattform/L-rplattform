using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class CreateCourseDTO
    {
       
        public string SubjectName { get; set; } = string.Empty;
        public int TotalMarks { get; set; }

        public string? Url { get; set; }

        public string ClassName { get; set; } = string.Empty;
        public int TeacherID { get; set; }


    }
}
