using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface ScheduleInterface
    {
        public Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync();

        public Task<ScheduleDTO?> GetScheduleByIdAsync(int id);

        public Task<ScheduleDTO> AddScheduleAsync(CreateScheduleDTO schedule);

        public Task<UpdateScheduleDTO> UpdateScheduleAsync(int id, UpdateScheduleDTO schedule);

        public Task DeleteScheduleAsync(int id);

        public Task<IEnumerable<ScheduleDTO>> GetAllCourseScheduleAsync(int courseId);

        public Task<IEnumerable<ScheduleDTO>> GetAllSchedulesByDateRangeAsync(DateTime startDate, DateTime endDate);

        public Task<IEnumerable<ScheduleDTO>> GetSchedulePagesAsync(int pageNumber, int pageSize);

        public Task<IEnumerable<ScheduleDTO>> GetScheduleStudentPagesAsync(int studentId,int pageNumber, int pageSize);


    }
}
