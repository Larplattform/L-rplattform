using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseRepository(ApplicationDbContext dbContext)
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
            return await _dbContext.Courses.Include(c => c.Users).Where(c => !c.IsDeleted).ToListAsync();

        }

        // It retrieves a course by its ID, including its associated users, if it is not marked as deleted.
        public async Task<Course?> GetByIdAsync(int id)
        {
          return await _dbContext.Courses.Include(c => c.Users).FirstOrDefaultAsync(c => c.CourseID == id && !c.IsDeleted);
            
        }

        // It retrieves all courses associated with a specific user ID, including their associated users, if they are not marked as deleted.
        public async Task<IEnumerable<Course>> GetByUserIdAsync(int userId)
        {
            return await _dbContext.Courses.Include(c => c.Users)
                .Where(c => c.Users.Any(u => u.Id == userId) && !c.IsDeleted)
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
        }
    }

