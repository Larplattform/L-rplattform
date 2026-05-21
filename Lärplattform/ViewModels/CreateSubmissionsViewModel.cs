using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class CreateSubmissionsViewModel
    {
        public string Content { get; set; } = string.Empty;

      

        public int UserId { get; set; }



        public int AssigmentId { get; set; }
    }
}
