using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class ScheduleDTO
    {
        public int ScheduleID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public LocationEnumDTO Location { get; set; }

        public int CourseID { get; set; }

        public CourseDTO Course { get; set; } = null!;

       
    }
}
