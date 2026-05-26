using Data.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Lärplattform.ViewModels
{
    public class UpdateScheduleViewModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public LocationEnumUpdateViewModel Location { get; set; }
        [Required(ErrorMessage = "CourseId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "CourseId must be 1")]
        public int CourseID { get; set; }

        public int TeacherID { get; set; }
    }
}
