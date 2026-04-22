using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    internal class CreateCourseDTO
    {
        public string SubjectName { get; set; } = string.Empty;
        public int TotalMarks { get; set; }

        public string ClassName { get; set; } = string.Empty;
        public int TeacherID { get; set; }

        public ICollection<CreateUserDTO> Users { get; set; } = new List<CreateUserDTO>();
    }
}
