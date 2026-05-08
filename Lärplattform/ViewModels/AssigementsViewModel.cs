using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class AssigementsViewModel
    {
        public int AssigmentID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Marks { get; set; }

        public string Url { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

       

        public int CourseID { get; set; }

        public CourseViewModel Course { get; set; } = null!;

        public bool IsPublished { get; set; }
    }
}
