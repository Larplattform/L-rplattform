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
            var courses = await _dbContext.Courses.Include(x => x.Users).FirstOrDefaultAsync(x => x.CourseID == assigment.CourseID);

            if (courses != null)
            {
                var teacher = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == courses.TeacherID);

                if (teacher != null && !courses.Users.Any(x => x.Id == teacher.Id))
                {
                    courses.Users.Add(teacher);
                }
            }
            _dbContext.Assigments.Add(assigment);
          
            return assigment;
        }

        public async Task<IEnumerable<Assigment>> GetAllAssigmentsByCourseIdAsync(int courseId)
        {
            return await _dbContext.Assigments.Include(c => c.Course).ThenInclude(c => c.Users).Where(c => c.CourseID == courseId && !c.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<Assigment>> GetAllAssigmentsByTeacherIdAsync(int teacherId)
        {
            return await _dbContext.Assigments.Include(c => c.Course).ThenInclude(c => c.Users).Where(c => c.Course.TeacherID == teacherId && !c.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<Assigment>> GetAllAssigmentsWithCourseAsync()
        {
            return await _dbContext.Assigments.Include(c => c.Course).ThenInclude(c => c.Users).Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<Assigment?> GetAssigmentByIdAsync(int id)
        {
            return await _dbContext.Assigments.Include(c => c.Course).ThenInclude(c => c.Users).FirstOrDefaultAsync(c => c.AssigmentID == id && !c.IsDeleted);
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
