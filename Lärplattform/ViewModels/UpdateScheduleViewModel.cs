using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class UpdateScheduleViewModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public LocationEnumUpdateViewModel Location { get; set; }

        public int CourseID { get; set; }
    }
}
