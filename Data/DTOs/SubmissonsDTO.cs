using Data.Entities;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class SubmissonsDTO
    {
        public int SubmissionID { get; set; }



        public string Content { get; set; } = string.Empty;

        public string Feedback { get; set; } = string.Empty;

        public GradeEnumDTO Grade { get; set; }

        public int UserId { get; set; }

     

        public int AssigmentId { get; set; }

      

        public string StudentName { get; set; } = string.Empty!;

        public string AssigmentTitle { get; set; } = string.Empty;

        public bool Status { get; set; }

       
    }
}
