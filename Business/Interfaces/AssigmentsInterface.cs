using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface AssigmentsInterface
    {
        public Task<CreateAssigmentsDTO> CreateAssigmentAsync(CreateAssigmentsDTO assigment);

        public Task<AssigmentsDTO?> GetAssigmentByIdAsync(int id);

        public Task<IEnumerable<AssigmentsDTO>> GetAllAssigmentsWithCoursesAsync();

        public Task<IEnumerable<AssigmentsDTO>> GetAllAssigmentsByTeacherIdAsync(int teacherId);

        public Task<IEnumerable<AssigmentsDTO>> GetAllAssigmentsbyStudentIdAsync (int studentId);

        public Task<IEnumerable<AssigmentsDTO>> GetAllAssigmentsByCourseIdAsync(int courseId);
    }
}
