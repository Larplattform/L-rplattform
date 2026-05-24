using Data.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Lärplattform.ViewModels
{
    public class CreateSubmissionsViewModel
    {
        [Required(ErrorMessage = "Content is needed")]
        [StringLength(5000, ErrorMessage = "Content is to long (5000 characters max)")]
        public string Content { get; set; } = string.Empty;

        [Required(ErrorMessage = "UserId must be specified")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be 1")]

        public int UserId { get; set; }


        public int AssigmentId { get; set; }
    }
}
