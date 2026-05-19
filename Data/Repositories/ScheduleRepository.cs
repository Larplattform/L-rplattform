using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationDbContexts _dbContext;

        public ScheduleRepository(ApplicationDbContexts dbContext)
        {
            _dbContext = dbContext;
        }

        // It adds a new schedule to the database context and returns the added schedule.
        public async Task<Schedule> AddScheduleAsync(Schedule schedule)
        {

           

            var IsOcopied = await _dbContext.Schedules.AnyAsync(s => s.Location == schedule.Location && schedule.StartDate < s.EndDate && schedule.EndDate > s.StartDate);
            if (IsOcopied)
            {
                throw new InvalidOperationException("The schedule overlaps with an existing schedule at the same location.");
            }
            var course = await _dbContext.Courses.Include(x => x.CourseUsers).ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.CourseID == schedule.CourseID);

            if (course != null)
            {
                schedule.Course = course;

                var teacher = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == course.TeacherID);

                if (teacher != null)
                {
                    var teacherscedule = new CourseUser
                    {
                        UserID = teacher.Id,
                        User = teacher,
                        CourseID = course.CourseID,

                    };
                   if(!course.CourseUsers.Any(x => x.UserID == teacher.Id))
                    {
                        course.CourseUsers.Add(teacherscedule);
                    }
                }
            }
            await _dbContext.Schedules.AddAsync(schedule);
          
            return schedule;
        }
        // It marks a schedule as deleted by setting the IsDeleted property to true, instead of actually removing it from the database.
        public async Task<Schedule?> DeleteScheduleAsync(int id)
        {
           
            var schedule = await GetScheduleByIdAsync(id);

            if (schedule != null)
            {
                schedule.IsDeleted = true;

            }
            return schedule;
        }
        // It retrieves all schedules for a specific course by filtering the schedules based on the CourseID and ensuring that they are not marked as deleted.
        public async Task<IEnumerable<Schedule>> GetAllCourseScheduleAsync(int courseId)
        {
             var schedules = await _dbContext.Schedules.Include(s => s.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User).Where(s => s.CourseID == courseId && !s.IsDeleted).ToListAsync();
            return schedules;
        }
        // It retrieves all schedules that are not marked as deleted, including their associated course information.
        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
        {
            var schedules = await _dbContext.Schedules.Include(s => s.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User).Where(s => !s.IsDeleted).ToListAsync();
            return schedules;
        }
        // It retrieves a schedule by its ID, including the associated course information, and ensures that the schedule is not marked as deleted.
        public async Task<Schedule?> GetScheduleByIdAsync(int id)
        {
            var schedule = await _dbContext.Schedules.Include(s => s.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User).FirstOrDefaultAsync(s => s.ScheduleID == id && !s.IsDeleted);
            return schedule;
        }
        // It saves the changes made to the database context asynchronously.
        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
        // It updates an existing schedule in the database context and returns the updated schedule.
        public async Task<Schedule> UpdateScheduleAsync(Schedule schedule)
        {
            
          
                var IsOcopied = await _dbContext.Schedules.AnyAsync(s => s.ScheduleID != schedule.ScheduleID && s.Location == schedule.Location && schedule.StartDate < s.EndDate && schedule.EndDate > s.StartDate);
            if(IsOcopied)
            {
                throw new InvalidOperationException("The schedule overlaps with an existing schedule at the same location.");
            }

            _dbContext.Schedules.Update(schedule);
            return schedule;
        }
        // It retrieves all schedules that fall within a specified date range, including their associated course information, and ensures that they are not marked as deleted.
        public async Task<IEnumerable<Schedule>> GetAllSchedulesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            var schedules = await _dbContext.Schedules.Include(s => s.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User)
                .Where(s => s.StartDate >= startDate && s.EndDate <= endDate && !s.IsDeleted)
                .ToListAsync();
            return schedules;
        }
        // It retrieves a paginated list of schedules, including their associated course information, and ensures that they are not marked as deleted. The pagination is achieved by skipping a certain number of records based on the page number and page size, and then taking a specified number of records for the current page.
        public async Task<IEnumerable<Schedule>> GetSchedulePagesAsync(int pageNumber, int pageSize)
        {
            var schedules = await _dbContext.Schedules.Include(s => s.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User)
                .Where(s => !s.IsDeleted)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return schedules;
        }

        public async Task<IEnumerable<Schedule>> GetScheduleStudentPagesAsync(int studentId, int pageNumber, int pageSize)
        {
           var schedules = await _dbContext.Schedules.Include(s => s.Course).ThenInclude(c => c.CourseUsers).ThenInclude(x => x.User)
                 .Where(s => !s.IsDeleted && s.Course.CourseUsers.Any(x => x.UserID == studentId))
                 .Skip((pageNumber - 1) * pageSize)
                 .Take(pageSize)
                 .ToListAsync();
            return schedules;
        }
    }
}
