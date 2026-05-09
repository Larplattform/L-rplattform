using Business.Interfaces;
using Data.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LärplattformApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionsController : ControllerBase
    {
        public readonly SubmissonsInterface _submissonsInterface;

        public SubmissionsController(SubmissonsInterface submissonsInterface)
        {
            _submissonsInterface = submissonsInterface;
        }

        [HttpGet]

        public async Task<IActionResult> GetAllSubmissons()
        {
            try
            {
                var submissons = await _submissonsInterface.GetAllSubmissionsAsync();
                return Ok(submissons);
            }catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]

        public async Task<IActionResult> CreateSubmission(CreateSubmissonsDTO submissonsDTO)
        {
            try
            {
                var createSubmission = await _submissonsInterface.AddSubmissionAsync(submissonsDTO);
                return Ok(createSubmission);

            }catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateSubmission([FromBody]UpdateSubmissionsDTO submissonsDTO, int id)
        {
            try
            {
                var createSubmission = await _submissonsInterface.UpdateSubmissionsAsync(submissonsDTO, id);
                return Ok(createSubmission);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet("student{studentId:int}")]

        public async Task<IActionResult> GetAllSubmssionsbyStudent(int studentId)
        {
            try
            {
                var submissons = await _submissonsInterface.GetSubmissonsbyStudentAsync(studentId);
                return Ok(submissons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("assigment{assigmentId:int}")]

        public async Task<IActionResult> GetAllSubmssionsbyAssigment(int assigmentId)
        {
            try
            {
                var submissons = await _submissonsInterface.GetAllSubmíssonbyAssigmentAsync(assigmentId);
                return Ok(submissons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("Gradereport{courseId:int}/{studentId:int}")]

        public async Task<IActionResult> GetGradeReport(int courseId , int studentId)
        {
            try
            {
                var submissons = await _submissonsInterface.GetSubmissonForReportAsync(courseId, studentId);
                return Ok(submissons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


    }
}
