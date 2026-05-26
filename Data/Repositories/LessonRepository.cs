using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class LessonRepository : ILessonRepository
    {
        private readonly ApplicationDbContexts _dbContext;

        public LessonRepository(ApplicationDbContexts dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Lesson> AddLessonAsync(Lesson lesson)
        {
           

            await _dbContext.Lessons.AddAsync(lesson);

          
            return  lesson;
        }


        // It retrieves all lessons that are not marked as deleted, including their associated courses.
        public async Task<IEnumerable<Lesson>> AllLessonsWithCoursesAsync()
        {

            return await _dbContext.Lessons.Include(x => x.Teacher).Include(c => c.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User).Where(c => !c.IsDeleted).ToListAsync();
        }

        // It deletes the lesson by setting the IsDeleted property to true instead of removing it from the database.

        public async Task<Lesson?> DeleteAsync(int id)
        {
            var lesson = await GetByIdAsync(id);

            if (lesson != null)
            {
                lesson.IsDeleted = true;
            }
            return lesson;
        }
        // It retrieves all lessons associated with a specific course ID, including their associated courses, if they are not marked as deleted.
        public async Task<IEnumerable<Lesson>> GetByCourseIdAsync(int id , string userid)
        {
            return await _dbContext.Lessons.Include(x => x.Teacher).Include(c => c.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User)
                .Where(l => l.CourseID == id && l.Course.TeacherID.ToString() == userid && !l.IsDeleted).ToListAsync();
        }

        // It retrieves a lessons by its ID, including its associated courses, if it is not marked as deleted.

        public async Task<Lesson?> GetByIdAsync(int courseID)
        {
            return await _dbContext.Lessons.Include(x => x.Teacher).Include(c => c.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User).FirstOrDefaultAsync(c => c.LessonID == courseID && !c.IsDeleted);
        }

        // It saves the changes made to the database context asynchronously.
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        // It updates an existing course in the database context.
        public void UpdateLesson(Lesson lesson)
        {
            _dbContext.Lessons.Update(lesson);
        }
    }
}
