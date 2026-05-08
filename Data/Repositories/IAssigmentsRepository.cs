using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IAssigmentsRepository
    {
      public Task<Assigment> CreateAssigmentAsync(Assigment assigment);

       public Task<Assigment?> GetAssigmentByIdAsync(int id);

        public Task<IEnumerable<Assigment>> GetAllAssigmentsWithCourseAsync();

            public Task<IEnumerable<Assigment>> GetAllAssigmentsByTeacherIdAsync(int teacherId);

        public Task<IEnumerable<Assigment>> GetAllAssigmentsByCourseIdAsync(int courseId);

        public Task SaveChangesAsync();


    }
}
