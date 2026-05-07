namespace Lärplattform.ViewModels
{
    public class CreateAssigmentsViewModel
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Marks { get; set; }

        public int LessonID { get; set; }

        public int CourseID { get; set; }
    }
}
