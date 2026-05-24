using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationDbContexts _dbContext;
        public readonly UserManager<User> _userManager;

        public UserRepository(ApplicationDbContexts dbContext , UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
           return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            
        }

        public async Task<IEnumerable<User>> GetAllTeachersAsync()
        {
          

            var teachers = await _userManager.GetUsersInRoleAsync("Teacher");
            return teachers;
        }

        public async Task<IEnumerable<User>> GetAllStudentsAsync()
        {
            var Students = await _userManager.GetUsersInRoleAsync("Student");
            return Students;
        }

        public async Task<int> CountAllTeachersAsync()
        {
            var teachercount = await _dbContext.Roles.FirstOrDefaultAsync(u => u.Name == "Teacher");
            if(teachercount == null) return 0;
            return await _dbContext.Users.CountAsync(u => !u.IsDeleted && _dbContext.UserRoles.Any(x => x.UserId == u.Id && x.RoleId == teachercount.Id));
        }

        public async Task<int> CountAllStudentsAsync()
        {
            var studentcount = await _dbContext.Roles.FirstOrDefaultAsync(u => u.Name == "Student");
            if(studentcount == null) return 0;
            return await _dbContext.Users.CountAsync(u => !u.IsDeleted && _dbContext.UserRoles.Any(x => x.UserId == u.Id && x.RoleId == studentcount.Id));
        }

        public async Task CheckForUser()
        {
            var DoesUserExist = await _dbContext.Users.AnyAsync();

            if(!DoesUserExist)
            {
                new InvalidOperationException("Data loss warning no users found");
            }
        }
    }
}
