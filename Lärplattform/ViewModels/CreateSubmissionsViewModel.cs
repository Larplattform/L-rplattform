using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class CreateSubmissionsViewModel
    {
        public string Content { get; set; } = string.Empty;

        public string Feedback { get; set; } = string.Empty;

        public CreateGradeEnumViewModel Grade { get; set; }

        public int UserId { get; set; }



        public int AssigmentId { get; set; }
    }
}
