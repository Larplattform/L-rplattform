using Data.Entities;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface ICourseRepository
    {
        //Gets all courses with their associated users asynchronously.
        Task<IEnumerable<Course>> GetAllWithUsersAsync();

        // Gets a course by its ID along with its associated users asynchronously.
        Task<IEnumerable<Course>> GetByUserIdAsync(int userId);

        // Gets a course by its ID asynchronously.
        Task<Course?> GetByIdAsync(int id);

        // Adds a new course asynchronously.

        Task<Course> AddAsync(Course course);

        // Updates an existing course.
        void Update(Course course);

        // Deletes a course by its ID asynchronously.
        Task<Course?> DeleteAsync(int id);

        // Saves changes to the data source asynchronously.

        Task SaveChangesAsync();

        // Gets all teachers by their ID asynchronously.
        Task<IEnumerable<Course>> GetAllTeachersByIdAsync(int teacherId);

        //This method only updates the finalgrade.

        Task<bool> SetFinalGrade(int studentid, int CourseId , GradeEnum FinalGrade);

        Task<int> CountAllCourses();
    }
}
