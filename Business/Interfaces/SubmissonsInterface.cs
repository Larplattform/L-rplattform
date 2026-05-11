using Data.DTOs;
using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface SubmissonsInterface
    {
        public Task<CreateSubmissonsDTO> AddSubmissionAsync(CreateSubmissonsDTO submission);

        public Task<IEnumerable<SubmissonsDTO>> GetAllSubmissionsAsync();

        public Task<IEnumerable<SubmissonsDTO>> GetSubmissonsbyStudentAsync(int studentId);

        public Task<IEnumerable<SubmissonsDTO>> GetAllSubmíssonbyAssigmentAsync(int assigmentId);

        public Task<IEnumerable<SubmissonsDTO>> GetSubmissonForReportPagesAsync(int coursesId, int studentId, int PageNumber , int PageSize);

        public Task<IEnumerable<GradeReportDTO>> GetAllSubmissionsForReportPagesAsync(int teacherId, int pageNumber , int pageSize);
        public Task<UpdateSubmissionsDTO> UpdateSubmissionsAsync(UpdateSubmissionsDTO submissions, int id);

        public Task<IEnumerable<SubmissonsDTO>> GetAllSubmissionbyPagesAsync(int pageNumber, int pageSize);

        public Task<SubmissonsDTO?> FindSubmissionbyId(int id);

        public Task<bool> UpdateCourseFinalGrade(int id, GradeEnumDTO FinalGrade);
    }
}
