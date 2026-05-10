using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class GradeReportViewModel
    {
        public GradeEnumViewModel Grade { get; set; }





        public int AssigmentId { get; set; }



        public string StudentName { get; set; } = string.Empty!;

        public string AssigmentTitle { get; set; } = string.Empty;

        public int TotalAssigmentTurnedIn { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; } = string.Empty;
    }
}
