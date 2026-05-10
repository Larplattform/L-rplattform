using Data.Entities;
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

        public Task<Submission> Update(Submission submission);

        public Task<Submission?> GetSubmissionById(int id);

        public Task<IEnumerable<Submission>> GetsubmissionPageAsync(int pageNumber , int pageSize);

        public Task SaveChangesAsync();
    }
}
