namespace Lärplattform.ViewModels
{
    public class UpdateCourseViewModel
    {
        public int CourseID { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public int TotalMarks { get; set; }

        public string? Url { get; set; }

        public string ClassName { get; set; } = string.Empty;
        public int TeacherID { get; set; }
    }
}
