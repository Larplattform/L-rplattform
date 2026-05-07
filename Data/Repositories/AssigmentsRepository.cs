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
            return await _dbContext.Assigments.Include(c =>c.Lesson).ThenInclude(c => c.Course).ThenInclude(c => c.Users).Where(c => c.Lesson.Course.CourseID == courseId).ToListAsync();
        }

        public async Task<IEnumerable<Assigment>> GetAllAssigmentsByTeacherIdAsync(int teacherId)
        {
            return await _dbContext.Assigments.Include(c => c.Lesson).ThenInclude(c => c.Course).ThenInclude(t => t.Users).Where(c => c.Lesson.Course.TeacherID == teacherId).ToListAsync();
        }

        public async Task<IEnumerable<Assigment>> GetAllAssigmentsWithLessonsAsync()
        {
            return await _dbContext.Assigments.Include(c => c.Lesson).ThenInclude(c => c.Course).ThenInclude(c => c.Users).ToListAsync();
        }

        public async Task<Assigment?> GetAssigmentByIdAsync(int id)
        {
            return await _dbContext.Assigments.Include(c => c.Lesson).ThenInclude(c => c.Course).ThenInclude(c => c.Users).FirstOrDefaultAsync(c => c.AssigmentID == id);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
