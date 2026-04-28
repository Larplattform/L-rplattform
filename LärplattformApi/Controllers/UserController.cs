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

        [HttpGet]

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
    }
}
