using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface ILessonRepository
    {
       Task<IEnumerable<Lesson>> AllLessonsWithCoursesAsync();

        Task<IEnumerable<Lesson>> GetByCourseIdAsync(int id, string userId);

        Task<Lesson?> GetByIdAsync(int courseID);

        Task <Lesson> AddLessonAsync(Lesson lesson);

        void UpdateLessonAsync(Lesson lesson );

        Task<Lesson?> Delete(int id);

        Task SaveChangesAsync();
    }
}
