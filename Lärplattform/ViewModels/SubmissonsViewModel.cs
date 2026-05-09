using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class SubmissonsViewModel
    {
        public int SubmissionID { get; set; }



        public string Content { get; set; } = string.Empty;

        public string Feedback { get; set; } = string.Empty;

        public GradeEnumViewModel Grade { get; set; }

        public int UserId { get; set; }



        public int AssigmentId { get; set; }



        public string StudentName { get; set; } = string.Empty!;

        public string AssigmentTitle { get; set; } = string.Empty;

        public bool Status { get; set; }
    }
}
