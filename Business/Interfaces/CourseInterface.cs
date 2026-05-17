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

        public Task<IEnumerable<CourseDTO>> GetCoursesByTeacherId(int teacherId);

        public Task<CourseDTO> GetCourseById(int courseId);

        public Task<UpdateCourseDTO> UpdateCourse(int courseId, UpdateCourseDTO courseDTO);

        public Task<LinkStudentToCourseDTO> LinkStudentToCourse(LinkStudentToCourseDTO linkDTO);

        public Task DeleteCourse(int courseId);

        public Task<bool> SetFinalGrade (int courseId , int studentid , GradeEnumDTO FinalGrade);

        public Task<IEnumerable<CourseDTO>> GetCoursebyUserid(int userid);
    }
}
