using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int id);

        Task<IEnumerable<User>> GetAllTeachersAsync();

        Task<IEnumerable<User>> GetAllStudentsAsync();

        Task<int> CountAllTeachersAsync();

        Task<int> CountAllStudentsAsync();
        
    }
}
