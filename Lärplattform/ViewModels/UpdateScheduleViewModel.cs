namespace Lärplattform.ViewModels
{
    public class UpdateScheduleViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Location { get; set; } = string.Empty;

        public int CourseID { get; set; }
    }
}
