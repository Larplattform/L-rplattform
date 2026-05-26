using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Data.DTOs
{
    public class CreateScheduleDTO
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime StartDate { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime EndDate { get; set; }

        public LocationEnumCreateDTO Location { get; set; }

        [Required(ErrorMessage = "CourseId must be specified")]
        [Range(1 , int.MaxValue , ErrorMessage ="CourseId must be 1")]
        public int CourseID { get; set; }

        public int TeacherID { get; set; }
    }
}
