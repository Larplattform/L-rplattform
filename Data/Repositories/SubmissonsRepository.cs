using Data.Context;
using Data.Entities;
using Data.Enums;
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

        public async Task<IEnumerable<Submission>> GetAllSubmissionForTeacherReportPages(int teacherid, int pageNumber, int pageSize)
        {
            return await _dbContext.Submissions.Include(x => x.User).Include(x => x.Assigment).ThenInclude(x => x.Course).ThenInclude(x => x.CourseUsers).Where(x => x.Assigment.Course.TeacherID == teacherid).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<Submission>> GetAllSubmissionsAsync()
        {
            return await _dbContext.Submissions.Include(x => x.Assigment).ThenInclude(x => x.Course).ThenInclude(x => x.CourseUsers).ThenInclude(x => x.User).ToListAsync();
        }

        public async Task<IEnumerable<Submission>> GetAllSubmíssonbyAssigmentAsync(int assigmentId)
        {
          return await _dbContext.Submissions.Include(x => x.Assigment).ThenInclude(x => x.Course).ThenInclude(x => x.CourseUsers).ThenInclude(x => x.User).Where(x => x.AssigmentId == assigmentId).ToListAsync();
        }

        public async Task<Submission?> GetSubmissionById(int id)
        {
            return await _dbContext.Submissions.Include(x => x.Assigment).ThenInclude(z => z.Course).ThenInclude(x => x.CourseUsers).ThenInclude(x => x.User).FirstOrDefaultAsync(z => z.SubmissionID == id);
        }

        public async Task<IEnumerable<Submission>> GetsubmissionPageAsync(int pageNumber, int pageSize)
        {
           var submissions =  await _dbContext.Submissions.Include(x => x.Assigment).ThenInclude(x => x.Course).ThenInclude(x => x.CourseUsers).ThenInclude(x => x.User).Where(x => !x.IsDeleted).Skip((pageNumber -1) * pageSize).Take(pageSize).ToListAsync();
            return submissions;
        }

        public async Task<Dictionary<int, int>> GetSubmissionsCountForStudents(List<int> studentids)
        {
           return await _dbContext.Submissions.Where(x => studentids.Contains(x.UserId)).GroupBy(x => x.UserId).Select(g => new {UserId = g.Key , Count = g.Count()}).ToDictionaryAsync(x => x.UserId, x => x.Count);
        }

        public async Task<IEnumerable<Submission>> GetSubmissonForReportPagesAsync(int coursesId, int studentId, int PageNumber , int PageSize)
        {
            return await _dbContext.Submissions.Include(x => x.Assigment).ThenInclude(x => x.Course).ThenInclude(x =>x.CourseUsers).ThenInclude(x => x.User).Where(x => x.UserId == studentId && x.Assigment.CourseID == coursesId).Skip((PageNumber - 1) * PageSize).Take(PageSize).ToListAsync();
          
        }

        public async Task<IEnumerable<Submission>> GetSubmissonsbyStudentAsync(int studentId)
        {
            return await _dbContext.Submissions.Include(x => x.Assigment).ThenInclude(x => x.Course).ThenInclude(x => x.CourseUsers).ThenInclude(x => x.User).Where(x => x.UserId == studentId).ToListAsync();
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
