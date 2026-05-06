using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class CreateScheduleDTO
    {
      
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public LocationEnumCreateDTO Location { get; set; }

        public int CourseID { get; set; }

     
    }
}
