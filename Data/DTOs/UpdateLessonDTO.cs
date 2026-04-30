using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class UpdateLessonDTO
    {
      

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int CourseID { get; set; }

     
    }
}
