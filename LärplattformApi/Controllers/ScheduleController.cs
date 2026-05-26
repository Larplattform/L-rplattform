using Business.Interfaces;
using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LärplattformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        public readonly ScheduleInterface _scheduleInterface;

        public ScheduleController(ScheduleInterface scheduleInterface)
        {
            _scheduleInterface = scheduleInterface;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllSchedules()
        {
            try
            {
                var schedules = await _scheduleInterface.GetAllSchedulesAsync();
                return Ok(schedules);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetScheduleById(int id)
        {
            try
            {
                var schedule = await _scheduleInterface.GetScheduleByIdAsync(id);
                if (schedule == null)
                {
                    return NotFound($"Schedule with ID {id} not found.");
                }
                return Ok(schedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddSchedule(CreateScheduleDTO schedule)
        {
            try
            {
                var createdSchedule = await _scheduleInterface.AddScheduleAsync(schedule);

                return Ok(createdSchedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSchedule(int id, UpdateScheduleDTO schedule)
        {
            try
            {
                var updatedSchedule = await _scheduleInterface.UpdateScheduleAsync(id, schedule);
                if (updatedSchedule == null)
                {
                    return NotFound($"Schedule with ID {id} not found.");
                }
                return Ok(updatedSchedule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteSchedule(int id)
        {
            try
            {
                await _scheduleInterface.DeleteScheduleAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("course/{courseId:int}")]
        public async Task<IActionResult> GetSchedulesByCourseId(int courseId)
        {
            try
            {
                var schedules = await _scheduleInterface.GetAllCourseScheduleAsync(courseId);
                if (schedules == null || !schedules.Any())
                {
                    return NotFound($"No schedules found for course with ID {courseId}.");
                }
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("page/{pageNumber:int}/size/{pageSize:int}")]
        public async Task<IActionResult> GetSchedulesByPage(int pageNumber, int pageSize)
        {
            try
            {
                var schedules = await _scheduleInterface.GetSchedulePagesAsync(pageNumber, pageSize);
                if (schedules == null || !schedules.Any())
                {
                    return NotFound($"No schedules found for page {pageNumber} with page size {pageSize}.");
                }
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("studentId/{studentId:int}/page/{pageNumber:int}/size/{pageSize:int}")]
        public async Task<IActionResult> GetStudentSchedulesByPage(int studentId,int pageNumber, int pageSize)
        {
            try
            {
                var schedules = await _scheduleInterface.GetScheduleStudentPagesAsync(studentId,pageNumber, pageSize);
                if (schedules == null || !schedules.Any())
                {
                    return NotFound($"No schedules found for page {pageNumber} with page size {pageSize}.");
                }
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("teacherId/{teacherId:int}/page/{pageNumber:int}/size/{pageSize:int}")]
        public async Task<IActionResult> GetTeacherSchedulesByPage(int teacherId, int pageNumber, int pageSize)
        {
            try
            {
                var schedules = await _scheduleInterface.GetScheduleTeacherPagesAsync(teacherId,pageNumber, pageSize);
                if (schedules == null || !schedules.Any())
                {
                    return NotFound($"No schedules found for page {pageNumber} with page size {pageSize}.");
                }
                return Ok(schedules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}