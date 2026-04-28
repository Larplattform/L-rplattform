using Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetUserById (int id);
    }
}
