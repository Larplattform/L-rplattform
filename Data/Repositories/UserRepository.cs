using Data.Context;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        public readonly ApplicationDbContext _dbContext;

        public UserRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User?> GetUserById(int id)
        {
           return await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            
        }
    }
}
