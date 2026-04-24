using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Assigment
    {
        public int AssigmentID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Marks { get; set; }

        public int LessonID { get; set; }

        public Lesson Lesson { get; set; } = null!;

        public bool IsPublished { get; set; }

        public bool IsDeleted { get; set; }


    }
}
