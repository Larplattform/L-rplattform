using Data.Context;
using Data.Entities;
using Data.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContexts _dbContext;

        public CourseRepository(ApplicationDbContexts dbContext)
        {
            _dbContext = dbContext;
        }

        // It adds a new course to the database asynchronously.
        public async Task<Course> AddAsync(Course course)
        {
          
            await _dbContext.Courses.AddAsync(course);
            return course;
        }

        // It deletes the course by setting the IsDeleted property to true instead of removing it from the database.
        public async Task<Course?> DeleteAsync(int id)
        {
            
            var course = await GetByIdAsync(id);
            if (course != null)
            {
                course.IsDeleted = true;
            }
           
            return course;
        }

        // It retrieves all courses that are not marked as deleted, including their associated users.
        public async Task<IEnumerable<Course>> GetAllWithUsersAsync()
        {
            return await _dbContext.Courses.Include(c => c.CourseUsers).ThenInclude(x => x.User).Where(c => !c.IsDeleted).ToListAsync();

        }

        // It retrieves a course by its ID, including its associated users, if it is not marked as deleted.
        public async Task<Course?> GetByIdAsync(int id)
        {
          return await _dbContext.Courses.Include(c => c.CourseUsers).ThenInclude(x => x.User).FirstOrDefaultAsync(c => c.CourseID == id && !c.IsDeleted);
            
        }

        // It retrieves all courses associated with a specific user ID, including their associated users, if they are not marked as deleted.
        public async Task<IEnumerable<Course>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Courses.Include(c => c.CourseUsers).ThenInclude(x => x.User)
                .Where(c => c.CourseUsers.Any(u => u.UserID == userId) && !c.IsDeleted)
                .ToListAsync();
        }

        // It saves the changes made to the database context asynchronously.

        public async Task SaveChangesAsync()
        {
           await _dbContext.SaveChangesAsync();
        }

        // It updates an existing course in the database context.
        public void Update(Course course)
        {
       
            _dbContext.Courses.Update(course);
        }


        // It retrieves all courses associated with a specific teacher ID, including their associated users, if they are not marked as deleted.


        public async Task<IEnumerable<Course>> GetAllTeachersByIdAsync(int teacherId)
        {
            return await _dbContext.Courses.Include(c => c.CourseUsers).ThenInclude(x => x.User)
                .Where(c => c.TeacherID == teacherId && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<bool> SetFinalGrade(int studentid, int CourseId, GradeEnum FinalGrade)
        {
           var enrollment = await _dbContext.CourseUsers.FirstOrDefaultAsync(x => x.UserID == studentid && x.CourseID == CourseId);

            if(enrollment == null)
            {
                return false;
            }

            enrollment.FinalGrade = FinalGrade;
            enrollment.IsReported = true;

            return true;
        }

        public async Task<int> CountAllCourses()
        {
            return await _dbContext.Courses.CountAsync();
        }
    }
}

