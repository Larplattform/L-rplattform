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
            return lesson;
        }

        public async Task<IEnumerable<Lesson>> AllLessonsWithCoursesAsync()
        {

            return await _dbContext.Lessons.Include(c => c.Course).Where(c => !c.IsDeleted).ToListAsync();
        }

        public async Task<Lesson?> Delete(int id)
        {
            var lesson = await GetByIdAsync(id);

            if (lesson != null)
            {
                lesson.IsDeleted = true;
            }
            return lesson;
        }

        public async Task<IEnumerable<Lesson>> GetByCourseIdAsync(int id , string userid)
        {
            return await _dbContext.Lessons.Include(c => c.Course)
                .Where(l => l.CourseID == id && l.Course.TeacherID.ToString() == userid && !l.IsDeleted).ToListAsync();
        }

        public async Task<Lesson?> GetByIdAsync(int courseID)
        {
            return await _dbContext.Lessons.Include(c => c.Course).FirstOrDefaultAsync(c => c.LessonID == courseID && !c.IsDeleted);
        }

       
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public void UpdateLessonAsync(Lesson lesson)
        {
            _dbContext.Lessons.Update(lesson);
        }
    }
}
