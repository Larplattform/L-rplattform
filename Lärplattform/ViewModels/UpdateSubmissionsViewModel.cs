using System.ComponentModel.DataAnnotations;

namespace Lärplattform.ViewModels
{
    public class UpdateSubmissionsViewModel
    {
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "Feedback is needed")]
        [StringLength(5000, ErrorMessage = "Feedback is to long (5000 characters max)")]
        public string Feedback { get; set; } = string.Empty;

        public UpdateGradeEnumViewModel Grade { get; set; }
        [Required(ErrorMessage = "UserId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be 1")]
        public int UserId { get; set; }


        [Required(ErrorMessage = "AssignmentId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "AssignmentId must be 1")]
        public int AssigmentId { get; set; }
    }
}
