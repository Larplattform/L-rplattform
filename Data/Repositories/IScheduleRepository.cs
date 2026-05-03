using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IScheduleRepository
    {
        // CRUD operations for Schedule entity
        public Task<IEnumerable<Schedule>> GetAllSchedulesAsync();

        // Get a schedule by its ID
        public Task<Schedule?> GetScheduleByIdAsync(int id);

        // Add a new schedule
        public Task<Schedule> AddScheduleAsync(Schedule schedule);

        // Update an existing schedule
        public Task<Schedule> UpdateScheduleAsync( Schedule schedule);

        // Delete a schedule by its ID

        public Task<Schedule?> DeleteScheduleAsync( int id);

        // Get all schedules for a specific course
        public Task<IEnumerable<Schedule>> GetAllCourseScheduleAsync(int courseId);

        // Save changes to the database
        public Task SaveChangesAsync();


    }
}
