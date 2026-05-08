namespace Lärplattform.ViewModels
{
    public class CreateAssigmentsViewModel
    {
        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Marks { get; set; }

        public string Url { get; set; } = string.Empty ;

        public int CourseID { get; set; }

        public DateTime? DueDate { get; set; }
    }
}
