using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class AssigmentsDTO
    {
        public int AssigmentID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Marks { get; set; }

        public DateTime DueDate { get; set; }

        public string Url { get; set; } = string.Empty;

         public int CourseID { get; set; }

        public CourseDTO Course { get; set; } = null!;

        public bool IsPublished { get; set; }

       

       
    }
}
