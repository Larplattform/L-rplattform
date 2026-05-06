using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class UpdateScheduleDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public UpdateLocationEnumDTO Location { get; set; }

        public int CourseID { get; set; }
    }
}
