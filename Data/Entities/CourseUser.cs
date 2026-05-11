using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class CourseUser
    {
        public int CourseID { get; set; }
        public Course Course { get; set; } = null!;

        public int UserID { get; set; }

        public User User { get; set; } = null!;

        public GradeEnum? FinalGrade { get; set; }

        public bool IsReported { get; set; }
    }
}
