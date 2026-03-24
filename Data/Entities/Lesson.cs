using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Lesson
    {
        public int LessonID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int CourseID { get; set; }

        public Course Course { get; set; } = null!;
    }
}
