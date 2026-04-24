using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class CourseViewModel
    {
        public string SubjectName { get; set; } = string.Empty;
        public int TotalMarks { get; set; }

        public string ClassName { get; set; } = string.Empty;
        public int TeacherID { get; set; }

        public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
