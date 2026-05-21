using Business.Interfaces;
using Data.DTOs;
using Data.Entities;
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


        [HttpGet("page/{pageNumber:int}/size/{pageSize:int}")]
        public async Task<IActionResult> GetAllSubmissonsPages(int pageNumber , int pageSize)
        {
            try
            {
                var submission = await _submissonsInterface.GetAllSubmissionbyPagesAsync(pageNumber, pageSize);
                if(submission == null || !submission.Any()){
                    return NotFound($"No submission found for page {pageNumber} with page size {pageSize}.");
                }
                return Ok(submission);
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

        [HttpGet("{Id:int}")]
        public async Task<IActionResult> GetSubmissionbyId( int id)
        {
            try
            {
                var createSubmission = await _submissonsInterface.FindSubmissionbyId( id);
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


        [HttpGet("assignment/{assigmentId:int}/student{studentId:int}")]

        public async Task<IActionResult> GetAllSubmssionsbyAssignmentStudent(int assigmentId , int studentId)
        {
            try
            {
                var submissons = await _submissonsInterface.GetSubmissionbyCourseAssignmentbyStudent( assigmentId, studentId);
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

        [HttpGet("Gradereport{courseId:int}/{studentId:int}/{PageNumber:int}/size/{PageSize:int}")]

        public async Task<IActionResult> GetGradeReport(int courseId , int studentId, int PageNumber , int PageSize)
        {
            try
            {
                var submissons = await _submissonsInterface.GetSubmissonForReportPagesAsync(courseId, studentId, PageNumber , PageSize);
                return Ok(submissons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("AllGradeReports{teacherId:int}/{pageNumber:int}/size/{pageSize:int}")]

        public async Task<IActionResult> GeAllGradeReport( int teacherId, int pageNumber, int pageSize)
        {
            try
            {
                var submissons = await _submissonsInterface.GetAllSubmissionsForReportPagesAsync(teacherId, pageNumber, pageSize);
                return Ok(submissons);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


       
    }
}
