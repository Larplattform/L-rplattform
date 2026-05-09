using Business.Interfaces;
using Data.DTOs;
using Data.Entities;
using Data.Enums;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class SubmissonsService : SubmissonsInterface
    {
        public readonly ISubmissonsRepository _SubmissonsRepository;

        public SubmissonsService(ISubmissonsRepository submissonsRepository)
        {
            _SubmissonsRepository = submissonsRepository;
        }
        public async Task<CreateSubmissonsDTO> AddSubmissionAsync(CreateSubmissonsDTO submission)
        {
            try
            {
                var newSubmisson = new Submission
                {

                    Content = submission.Content,
                    Status = submission.Status,
                    Grade = (GradeEnum)submission.Grade,
                    UserId = submission.UserId,
                    AssigmentId = submission.AssigmentId,
                    Feedback = submission.Feedback,
                };

                await _SubmissonsRepository.AddSubmissionAsync(newSubmisson);
                await _SubmissonsRepository.SaveChangesAsync();

                return submission;

            }catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding a submisson: {ex.Message}");
                throw;
            }
        }

        public async Task<SubmissonsDTO?> FindSubmissionbyId(int id)
        {
            try
            {
                var submission = await _SubmissonsRepository.GetSubmissionById(id);
                if(submission == null)
                {
                    return null;
                }
                var scheduledto = new SubmissonsDTO
                {
                    SubmissionID = submission.SubmissionID,
                    Grade = (GradeEnumDTO)submission.Grade,
                    UserId = submission.UserId,
                    Feedback = submission.Feedback,
                    Status = submission.Status,
                    Content = submission.Content,
                    AssigmentId = submission.AssigmentId,
                    StudentName = submission.User != null ? $"{submission.User.FirstName} {submission.User.LastName}" : "Onknown Student",
                    AssigmentTitle = submission.Assigment != null ? submission.Assigment.Title : "Onkown Assigment Title"
                };

                return scheduledto;

            }catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving a submission by ID: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<SubmissonsDTO>> GetAllSubmissionbyPagesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var Allsubmissions = await _SubmissonsRepository.GetsubmissionPageAsync(pageNumber, pageSize);
                var SubmissionDtos = new List<SubmissonsDTO>();
                foreach (var submission in Allsubmissions)
                {
                    var submissionsDto = new SubmissonsDTO
                    {
                        SubmissionID = submission.SubmissionID,
                        Grade = (GradeEnumDTO)submission.Grade,
                        UserId = submission.UserId,
                        Feedback = submission.Feedback,
                        Status = submission.Status,
                        Content = submission.Content,
                        AssigmentId = submission.AssigmentId,
                        StudentName = submission.User != null ? $"{submission.User.FirstName} {submission.User.LastName}" : "Onknown Student",
                        AssigmentTitle = submission.Assigment != null ? submission.Assigment.Title : "Onkown Assigment Title"
                    };

                    SubmissionDtos.Add(submissionsDto);
                }

                return SubmissionDtos;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving paginated submission: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<SubmissonsDTO>> GetAllSubmissionsAsync()
        {
            try
            {
                var AllSubmissions = await _SubmissonsRepository.GetAllSubmissionsAsync();

                var SubmissionDtos = new List<SubmissonsDTO>();
                foreach (var submission in AllSubmissions)
                {
                    var submissionsDto = new SubmissonsDTO
                    {
                        SubmissionID = submission.SubmissionID,
                        Grade = (GradeEnumDTO)submission.Grade,
                        UserId = submission.UserId,
                        Feedback = submission.Feedback,
                        Status = submission.Status,
                        Content = submission.Content,
                        AssigmentId = submission.AssigmentId,
                        StudentName = submission.User != null ? $"{submission.User.FirstName} {submission.User.LastName}" : "Onknown Student",
                        AssigmentTitle = submission.Assigment != null ? submission.Assigment.Title : "Onkown Assigment Title"
                    };

                    SubmissionDtos.Add(submissionsDto);
                }

                return SubmissionDtos;
            }catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving Submissions: {ex.Message}");
                throw;
            }

        }

        public async Task<IEnumerable<SubmissonsDTO>> GetAllSubmíssonbyAssigmentAsync(int assigmentId)
        {
            try
            {
                var AllSubmissions = await _SubmissonsRepository.GetAllSubmíssonbyAssigmentAsync(assigmentId);

                var SubmissionDtos = new List<SubmissonsDTO>();
                foreach (var submission in AllSubmissions)
                {
                    var submissionsDto = new SubmissonsDTO
                    {
                        SubmissionID = submission.SubmissionID,
                        Grade = (GradeEnumDTO)submission.Grade,
                        UserId = submission.UserId,
                        Feedback = submission.Feedback,
                        Status = submission.Status,
                        Content = submission.Content,
                        AssigmentId = submission.AssigmentId,
                        StudentName = submission.User != null ? $"{submission.User.FirstName} {submission.User.LastName}" : "Onknown Student",
                        AssigmentTitle = submission.Assigment != null ? submission.Assigment.Title : "Onkown Assigment Title"
                    };

                    SubmissionDtos.Add(submissionsDto);
                }

                return SubmissionDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving Submissions: {ex.Message}");
                throw;
            }
        }

        public  async Task<IEnumerable<SubmissonsDTO>> GetSubmissonForReportAsync(int coursesId, int studentId)
        {
            try
            {
                var AllSubmissions = await _SubmissonsRepository.GetSubmissonForReportAsync(coursesId, studentId);

                var SubmissionDtos = new List<SubmissonsDTO>();
                foreach (var submission in AllSubmissions)
                {
                    var submissionsDto = new SubmissonsDTO
                    {
                        SubmissionID = submission.SubmissionID,
                        Grade = (GradeEnumDTO)submission.Grade,
                        UserId = submission.UserId,
                        Feedback = submission.Feedback,
                        Status = submission.Status,
                        Content = submission.Content,
                        AssigmentId = submission.AssigmentId,
                        StudentName = submission.User != null ? $"{submission.User.FirstName} {submission.User.LastName}" : "Onknown Student",
                        AssigmentTitle = submission.Assigment != null ? submission.Assigment.Title : "Onkown Assigment Title"
                    };

                    SubmissionDtos.Add(submissionsDto);
                }

                return SubmissionDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving Submissions: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<SubmissonsDTO>> GetSubmissonsbyStudentAsync(int studentId)
        {
            try
            {
                var AllSubmissions = await _SubmissonsRepository.GetSubmissonsbyStudentAsync(studentId);

                var SubmissionDtos = new List<SubmissonsDTO>();
                foreach (var submission in AllSubmissions)
                {
                    var submissionsDto = new SubmissonsDTO
                    {
                        SubmissionID = submission.SubmissionID,
                        Grade = (GradeEnumDTO)submission.Grade,
                        UserId = submission.UserId,
                        Feedback = submission.Feedback,
                        Status = submission.Status,
                        Content = submission.Content,
                        AssigmentId = submission.AssigmentId,
                        StudentName = submission.User != null ? $"{submission.User.FirstName} {submission.User.LastName}" : "Onknown Student",
                        AssigmentTitle = submission.Assigment != null ? submission.Assigment.Title : "Onkown Assigment Title"
                    };

                    SubmissionDtos.Add(submissionsDto);
                }

                return SubmissionDtos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while retrieving Submissions: {ex.Message}");
                throw;
            }
        }

        public async Task<UpdateSubmissionsDTO> UpdateSubmissionsAsync(UpdateSubmissionsDTO submissions , int id)
        {
            try
            {

                var submission = await _SubmissonsRepository.GetSubmissionById(id);
                if(submission == null)
                {
                    throw new Exception($"Schedule with ID {id} not found.");
                }
                submission.Feedback = submissions.Feedback;
                submission.Grade = (GradeEnum)submissions.Grade;
                submission.UserId = submissions.UserId;
                submission.AssigmentId = submissions.AssigmentId;
                await _SubmissonsRepository.Update(submission);
                await _SubmissonsRepository.SaveChangesAsync();

                return submissions;

            }catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating a submission: {ex.Message}");
                throw;
            }
        }
    }
}
