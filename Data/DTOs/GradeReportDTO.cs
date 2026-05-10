using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Data.DTOs
{
    public class GradeReportDTO
    {
      



      

        public GradeEnumDTO Grade { get; set; }

      



        public int AssigmentId { get; set; }



        public string StudentName { get; set; } = string.Empty!;

        public string AssigmentTitle { get; set; } = string.Empty;

        public int TotalAssigmentTurnedIn { get; set; }

        public int CourseId { get; set; }

        public string CourseName {  get; set; } = string.Empty;

        public DateTime CourseEndDate { get; set; }

    }
}
