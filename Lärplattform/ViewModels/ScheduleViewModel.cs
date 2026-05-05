using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class ScheduleViewModel
    {
        public int ScheduleID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public int CourseID { get; set; }

        public CourseViewModel Course { get; set; } = null!;

        public string TeacherName { get; set; } = string.Empty;
    }
}
