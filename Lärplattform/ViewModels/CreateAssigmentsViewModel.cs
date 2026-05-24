using System.ComponentModel.DataAnnotations;

namespace Lärplattform.ViewModels
{
    public class CreateAssigmentsViewModel
    {
        [Required(ErrorMessage = "Title is needed")]
        [StringLength(100, ErrorMessage = "TItle is to long (100 characters max)")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is needed")]
        [StringLength(500, ErrorMessage = "Description is to long (500 characters max)")]
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(0, 150, ErrorMessage = "Marks must be between 0 and 150")]
        public int Marks { get; set; }
        [Required(ErrorMessage = "A Url is needed")]
        [Url(ErrorMessage = "Please Enter a valid Url")]
        public string Url { get; set; } = string.Empty ;
        [Required(ErrorMessage = "CourseId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "CourseId must be 1")]
        public int CourseID { get; set; }
       
        public DateTime? DueDate { get; set; }
    }
}
