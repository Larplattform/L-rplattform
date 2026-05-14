using Business.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LärplattformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TotalStatsController : ControllerBase
    {
        public ITotalStatsInterface _totalStatsInterface;

        public TotalStatsController(ITotalStatsInterface totalStatsInterface)
        {
            _totalStatsInterface = totalStatsInterface;
        }

        [HttpGet("TotalStats")]

        public async Task<IActionResult> GetAllCouts()
        {
            try
            {
                var count = await _totalStatsInterface.GetAllStats();
                return Ok(count);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
