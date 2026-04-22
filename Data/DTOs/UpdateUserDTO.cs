using System;
using System.Collections.Generic;
using System.Text;

namespace Data.DTOs
{
    public class UpdateUserDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string Street { get; set; } = string.Empty;

        public ICollection<UpdateCourseDTO> Courses { get; set; } = new List<UpdateCourseDTO>();



        public bool IsDeleted { get; set; }
    }
}

