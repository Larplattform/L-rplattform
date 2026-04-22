using System;
using System.Collections.Generic;
using System.Text;
using Data.DTOs;

namespace Business.Interfaces
{
    public interface ICourseInterface
    {
        public Task<CreateCourseDTO> CreateCourse(CreateCourseDTO courseDTO);

        public Task<IEnumerable<CourseDTO>> GetAllCourses();

        public Task<UpdateCourseDTO> UpdateCourse(int id, UpdateCourseDTO courseDTO);
        public Task DeleteCourse(int id);

    }
}
