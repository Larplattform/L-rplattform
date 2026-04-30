using Business.Interfaces;
using Business.Services;
using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LärplattformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LessonsController : ControllerBase
    {
        public readonly LessonInterface _lessoninterface;

        public LessonsController(LessonInterface lessoninterface)
        {
            _lessoninterface = lessoninterface;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllLessons()
        {
            try
            {
                var lessons = await _lessoninterface.GetAllLessons();

                return Ok(lessons);
            }catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetlessonById([FromRoute] int id)
        {
            try
            {
                var lesson = await _lessoninterface.GetLessonById(id);
                if (lesson == null)
                {
                    return NotFound($"lessons with ID {id} not found.");
                }
                return Ok(lesson);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson([FromBody] CreateLessonDTO lessonDTO)
        {
            try
            {
                var createdlesson = await _lessoninterface.CreateLessonAsync(lessonDTO);
                return Ok(createdlesson);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateLesson([FromRoute] int id, [FromBody] UpdateLessonDTO lessonDTO)
        {
            try
            {
                var updatedlesson = await _lessoninterface.UpdateLessonAsync(id, lessonDTO);
                return Ok(updatedlesson);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteLesson([FromRoute] int id)
        {
            try
            {
                await _lessoninterface.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }

        }

        [HttpGet("Course/{courseId:int}")]
        public async Task<IActionResult> GetLessonsbyCoursesId([FromRoute] int courseId)
        {
            try
            {
                var courses = await _lessoninterface.GetLessonsbyCourseId(courseId);
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
