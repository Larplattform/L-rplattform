using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class CourseViewModel
    {
        public int CourseID { get; set; }
        public string SubjectName { get; set; } = string.Empty;
        public int TotalMarks { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string? Url { get; set; }
        public string ClassName { get; set; } = string.Empty;
        public int TeacherID { get; set; }

        public string TeacherName { get; set; } = string.Empty;



        public IEnumerable<UserViewModel> Users { get; set; } = new List<UserViewModel>();
    }
}
