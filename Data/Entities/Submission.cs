using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Submission
    {
        public int SubmissionID { get; set; }

        public DateTime DueDate { get; set; }

        public string Content { get; set; } = string.Empty;

        public string Feedback { get; set; } = string.Empty;

        public int UserId { get; set; } 

        public User User { get; set; } = null!;

        public int AssigmentId { get; set; }

        public Assigment Assigment { get; set; } = null!;

        public bool Status { get; set; }
    }
}
