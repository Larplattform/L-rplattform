using Business.Interfaces;
using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LärplattformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AssigmentsController : ControllerBase
    {
        public readonly AssigmentsInterface AssigmentsInterface;

        public AssigmentsController(AssigmentsInterface assigmentsInterface)
        {
            AssigmentsInterface = assigmentsInterface;
        }

        [HttpGet("course/{courseId:int}")]
        public async Task<IActionResult> GetAllAssigmentsByCourseId(int courseId)
        {
            try
            {
                var assigments = await AssigmentsInterface.GetAllAssigmentsByCourseIdAsync(courseId);
                return Ok(assigments);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");

            }
        }



        [HttpPost]
        public async Task<IActionResult> CreateAssigment(CreateAssigmentsDTO assigment)
        {
            try
            {
                var createdAssigment = await AssigmentsInterface.CreateAssigmentAsync(assigment);
                return Ok(createdAssigment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAssigmentById(int id)
        {
            try
            {
                var assigment = await AssigmentsInterface.GetAssigmentByIdAsync(id);
                if (assigment == null)
                {
                    return NotFound();
                }
                return Ok(assigment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("teacher/{teacherId:int}")]
        public async Task<IActionResult> GetAllAssigmentsByTeacherId(int teacherId)
        {
            try
            {
                var assigments = await AssigmentsInterface.GetAllAssigmentsByTeacherIdAsync(teacherId);
                return Ok(assigments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("lessons")]
        public async Task<IActionResult> GetAllAssigmentsWithLessons()
        {
            try
            {
                var assigments = await AssigmentsInterface.GetAllAssigmentsWithLessonsAsync();
                return Ok(assigments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}