using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class AssigmentsRepository : IAssigmentsRepository
    {

        private readonly ApplicationDbContexts _dbContext;

        public AssigmentsRepository(ApplicationDbContexts dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Assigment> CreateAssigmentAsync(Assigment assigment)
        {
            _dbContext.Assigments.Add(assigment);
          
            return assigment;
        }

        public async Task<IEnumerable<Assigment>> GetAllAssigmentsByCourseIdAsync(int courseId)
        {
            return await _dbContext.Assigments.Include(x => x.Teacher).Include(c => c.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User).Where(c => c.CourseID == courseId && !c.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<Assigment>> GetAllAssigmentsbyStudentId(int studentId)
        {
            return await _dbContext.Assigments.Include(x => x.Teacher).Include(c => c.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User).Where(c => c.Course.CourseUsers.Any(x => x.UserID == studentId)).ToListAsync();
        }

        public async Task<IEnumerable<Assigment>> GetAllAssigmentsByTeacherIdAsync(int teacherId)
        {
            return await _dbContext.Assigments.Include(x => x.Teacher).Include(c => c.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User).Where(c => c.Course.TeacherID == teacherId && !c.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<Assigment>> GetAllAssigmentsWithCourseAsync()
        {
            return await _dbContext.Assigments.Include(x => x.Teacher).Include(c => c.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User).Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<Assigment?> GetAssigmentByIdAsync(int id)
        {
            return await _dbContext.Assigments.Include(x => x.Teacher).Include(c => c.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User).FirstOrDefaultAsync(c => c.AssigmentID == id && !c.IsDeleted);
        }
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
