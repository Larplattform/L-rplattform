using Data.Entities;

namespace Lärplattform.ViewModels
{
    public class LessonViewModel
    {
        public int LessonID { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
        public int TeacherID { get; set; }
        public int CourseID { get; set; }

       

      
    }
}
