using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class UserDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;

        public ICollection<CourseDTO> Courses { get; set; } = new List<CourseDTO>();



        public bool IsDeleted { get; set; }
    }
}

