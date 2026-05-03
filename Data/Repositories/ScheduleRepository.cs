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

        public async Task<Schedule> AddScheduleAsync(Schedule schedule)
        {
            _dbContext.Schedules.Add(schedule);
           
            return schedule;
        }

        public async Task<Schedule?> DeleteScheduleAsync(int id)
        {
           
            var schedule = await GetScheduleByIdAsync(id);

            if (schedule != null)
            {
                schedule.IsDeleted = true;

            }
            return schedule;
        }

        public async Task<IEnumerable<Schedule>> GetAllCourseScheduleAsync(int courseId)
        {
             var schedules = await _dbContext.Schedules.Include(s => s.Course).Where(s => s.CourseID == courseId && !s.IsDeleted).ToListAsync();
            return schedules;
        }

        public async Task<IEnumerable<Schedule>> GetAllSchedulesAsync()
        {
            var schedules = await _dbContext.Schedules.Include(s => s.Course).Where(s => !s.IsDeleted).ToListAsync();
            return schedules;
        }

        public async Task<Schedule?> GetScheduleByIdAsync(int id)
        {
            var schedule = await _dbContext.Schedules.Include(s => s.Course).FirstOrDefaultAsync(s => s.ScheduleID == id && !s.IsDeleted);
            return schedule;
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Schedule> UpdateScheduleAsync(Schedule schedule)
        {
            _dbContext.Schedules.Update(schedule);
            return schedule;
        }
    }
}
