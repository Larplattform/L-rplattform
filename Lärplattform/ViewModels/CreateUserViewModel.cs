using Data.DTOs;

namespace Lärplattform.ViewModels
{
    public class CreateUserViewModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;

        public ICollection<CreateCourseViewModel> Courses { get; set; } = new List<CreateCourseViewModel>();



        public bool IsDeleted { get; set; }
    }
}
