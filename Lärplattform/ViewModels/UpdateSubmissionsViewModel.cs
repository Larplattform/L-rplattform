namespace Lärplattform.ViewModels
{
    public class UpdateSubmissionsViewModel
    {
        public string Content { get; set; } = string.Empty;

        public string Feedback { get; set; } = string.Empty;

        public UpdateGradeEnumViewModel Grade { get; set; }

        public int UserId { get; set; }



        public int AssigmentId { get; set; }
    }
}
