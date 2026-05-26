using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class ScheduleViewModel
    {
        public int ScheduleID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public LocationEnumViewModel Location { get; set; }

        public int CourseID { get; set; }

        public CourseViewModel Course { get; set; } = null!;
        public int TeacherID { get; set; }
        public string TeacherName { get; set; } = string.Empty;
    }
}
