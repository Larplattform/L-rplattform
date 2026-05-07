using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class AssigementsViewModel
    {
        public int AssigmentID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int Marks { get; set; }

        public int LessonID { get; set; }

        public int CourseID { get; set; }

        public LessonViewModel Lesson { get; set; } = null!;

        public bool IsPublished { get; set; }
    }
}
