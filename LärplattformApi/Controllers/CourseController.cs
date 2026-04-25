using Business.Interfaces;
using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LärplattformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        public ICourseInterface CourseService;

        public CourseController(ICourseInterface courseService)
        {
            CourseService = courseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCourses()
        {
            try
            {
                var courses = await CourseService.GetAllCourses();
                return Ok(courses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpGet("{courseId:int}")]
        public async Task<IActionResult> GetCourseById([FromRoute] int courseId)
        {
            try
            {
                var course = await CourseService.GetCourseById(courseId);
                if(course == null)
                {
                    return NotFound($"Course with ID {courseId} not found.");
                }
                return Ok(course);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse([FromBody] CreateCourseDTO courseDTO)
        {
            try
            {
                var createdCourse = await CourseService.CreateCourse(courseDTO);
                return Ok(createdCourse);
            }catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }

        [HttpPost("AddStudentToCourse")]
        public async Task<IActionResult> AddStudentToCourse([FromBody] LinkStudentToCourseDTO linkDTO)
        {
            try
            {
                await CourseService.LinkStudentToCourse(linkDTO);


                return Ok();
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

        [HttpPut("{courseId:int}")]
        public async Task<IActionResult> UpdateCourse([FromRoute] int courseId, [FromBody] UpdateCourseDTO courseDTO)
        {
            try
            {
                var updatedCourse = await CourseService.UpdateCourse(courseId, courseDTO);
                return Ok(updatedCourse);
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

        [HttpDelete("{courseId:int}")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
        {
            try
            {
                await CourseService.DeleteCourse(courseId);
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
    }
}
