namespace Lärplattform.ViewModels
{
    public class CreateCourseViewModel
    {
        public string SubjectName { get; set; } = string.Empty;
        public int TotalMarks { get; set; }

        public string ClassName { get; set; } = string.Empty;
        public int TeacherID { get; set; }
    }
}
