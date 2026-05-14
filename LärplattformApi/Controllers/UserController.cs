using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LärplattformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public IUserInterface UserService;

        public UserController(IUserInterface userService)
        {
            UserService = userService;
        }

        [HttpGet("Teachers")]

        public async Task<IActionResult> GetAllTeachers()
        {
            try
            {
                var teachers = await UserService.GetAllTeachersAsync();
                return Ok(teachers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }

        [HttpGet("Students")]

        public async Task<IActionResult> GetAllStudents()
        {
            try
            {
                var student = await UserService.GetAllStudentsAsync();
                return Ok(student);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }

        }
    }
}
