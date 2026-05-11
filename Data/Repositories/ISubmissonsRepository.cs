using Data.Entities;
using Data.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface ISubmissonsRepository
    {
        public Task<Submission> AddSubmissionAsync(Submission submission);

        public Task<IEnumerable<Submission>> GetAllSubmissionsAsync();

        public Task<IEnumerable<Submission>> GetSubmissonsbyStudentAsync(int studentId);

        public Task<IEnumerable<Submission>> GetAllSubmíssonbyAssigmentAsync(int assigmentId);

        public Task<IEnumerable<Submission>> GetSubmissonForReportPagesAsync(int coursesId, int studentId, int PageNumber, int PageSize);

        public Task<IEnumerable<Submission>> GetAllSubmissionForTeacherReportPages(int teacherid, int pageNumber, int pageSize);

        public Task<Dictionary<int,int>> GetSubmissionsCountForStudents(List<int> studentids);
        public Task<Submission> Update(Submission submission);

        public Task<bool> UpdateCourseFinalGrade (int submissionId , GradeEnum FinalGrade);

        public Task<Submission?> GetSubmissionById(int id);

        public Task<IEnumerable<Submission>> GetsubmissionPageAsync(int pageNumber , int pageSize);

        public Task SaveChangesAsync();
    }
}
