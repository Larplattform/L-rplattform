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
    }
}
