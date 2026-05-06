using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Entities
{
    public class Schedule
    {
        public int ScheduleID { get; set; }
        public DateTime StartDate { get; set; }
       public  DateTime EndDate { get; set; }

        public LocationEnum Location { get; set; }  

        public int CourseID { get; set; }

       public Course Course { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
