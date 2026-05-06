namespace Lärplattform.ViewModels
{
    public class CreateScheduleViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public LocationEnumCreateViewModel Location { get; set; }

        public int CourseID { get; set; }

        public string TeacherName { get; set; } = string.Empty;
    }
}
