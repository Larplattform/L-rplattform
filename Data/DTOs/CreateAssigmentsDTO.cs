using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class CreateAssigmentsDTO
    {
      

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Marks { get; set; }

        public int LessonID { get; set; }

       

    }
}
