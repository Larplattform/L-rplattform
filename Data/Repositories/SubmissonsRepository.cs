using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class SubmissonsRepository : ISubmissonsRepository
    {
        private readonly ApplicationDbContexts _dbContext;

        public SubmissonsRepository(ApplicationDbContexts dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<Submission> AddSubmissionAsync(Submission submission)
        {
            _dbContext.Submissions.Add(submission);

            return submission;
        }

        public async Task<IEnumerable<Submission>> GetAllSubmissionsAsync()
        {
            return await _dbContext.Submissions.Include(x => x.Assigment).ThenInclude(x => x.Course).ThenInclude(x => x.Users).ToListAsync();
        }

        public async Task<IEnumerable<Submission>> GetAllSubmíssonbyAssigmentAsync(int assigmentId)
        {
          return await _dbContext.Submissions.Include(x => x.Assigment).Where(x => x.AssigmentId == assigmentId).ToListAsync();
        }

        public async Task<Submission?> GetSubmissionById(int id)
        {
            return await _dbContext.Submissions.Include(x => x.Assigment).ThenInclude(z => z.Course).ThenInclude(x => x.Users).FirstOrDefaultAsync(z => z.SubmissionID == id);
        }

        public Task<IEnumerable<Submission>> GetsubmissionPageAsync(int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Submission>> GetSubmissonForReportAsync(int coursesId, int studentId)
        {
            return await _dbContext.Submissions.Include(x => x.Assigment).ThenInclude(x => x.Course).ThenInclude(x =>x.Users).Where(x => x.UserId == studentId && x.Assigment.CourseID == coursesId).ToListAsync();
        }

        public async Task<IEnumerable<Submission>> GetSubmissonsbyStudentAsync(int studentId)
        {
            return await _dbContext.Submissions.Include(x => x.Assigment).ThenInclude(x => x.Course).ThenInclude(x => x.Users).Where(x => x.UserId == studentId).ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Submission> Update(Submission submission)
        {
             _dbContext.Submissions.Update(submission);
            return submission;
        }
    }
}
