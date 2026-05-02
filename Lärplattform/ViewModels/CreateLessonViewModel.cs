namespace Lärplattform.ViewModels
{
    public class CreateLessonViewModel
    {
      
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public int CourseID { get; set; }
    }
}
