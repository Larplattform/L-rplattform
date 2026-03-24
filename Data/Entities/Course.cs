using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Course
    {
        public int CourseID { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public int TotalMarks { get; set; }

        public string ClassName { get; set; } = string.Empty;
        public int TeacherID { get; set; }

       public ICollection<User> Users { get; set; } = new List<User>();
    }
}
