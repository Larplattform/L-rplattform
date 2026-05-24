using System.ComponentModel.DataAnnotations;

namespace Lärplattform.ViewModels
{
    public class UpdateLessonViewModel
    {
        [Required(ErrorMessage = "LessonId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "LessonId must be 1")]
        public int LessonID { get; set; }
        [Required(ErrorMessage = "Title is needed")]
        [StringLength(100, ErrorMessage = "TItle is to long (100 characters max)")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "Description is needed")]
        [StringLength(500, ErrorMessage = "Description is to long (500 characters max)")]
        public string Description { get; set; } = string.Empty;
        [Required(ErrorMessage = "Content is needed")]
        [StringLength(5000, ErrorMessage = "Content is to long (5000 characters max)")]
        public string Content { get; set; } = string.Empty;
        [Required(ErrorMessage = "CourseId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "CourseId must be 1")]
        public int CourseID { get; set; }
    }
}
