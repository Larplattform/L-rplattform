using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface ILessonRepository
    {
        //Gets all Lessons with their associated Courses asynchronously.
        Task<IEnumerable<Lesson>> AllLessonsWithCoursesAsync();

        // Gets a Lesson by its ID along with its associated course asynchronously.
        Task<IEnumerable<Lesson>> GetByCourseIdAsync(int id, string userId);

        // Gets a lesson by its ID asynchronously.
        Task<Lesson?> GetByIdAsync(int courseID);

        // Create a lesson  asynchronously.
        Task<Lesson> AddLessonAsync(Lesson lesson);

        // Update a lesson.
        void UpdateLesson(Lesson lesson );

        // Delete a lesson  asynchronously.
        Task<Lesson?> DeleteAsync(int id);

        // Save changes asynchronously.

        Task SaveChangesAsync();
    }
}
