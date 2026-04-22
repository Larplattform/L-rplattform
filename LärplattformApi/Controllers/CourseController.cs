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
            var courses = await CourseService.GetAllCourses();
            return Ok(courses);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDTO courseDTO)
        {
            var createdCourse = await CourseService.CreateCourse(courseDTO);
            return Ok(createdCourse);
        }

        [HttpPost("AddStudentToCourse")]
        public async Task<IActionResult> AddStudentToCourse([FromBody] LinkStudentToCourseDTO linkDTO)
        {
            await CourseService.LinkStudentToCourse(linkDTO);       
            
       
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCourse(int id, UpdateCourseDTO courseDTO)
        {
            var updatedCourse = await CourseService.UpdateCourse(id, courseDTO);
            return Ok(updatedCourse);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await CourseService.DeleteCourse(id);
            return NoContent();
        }
    }
}
