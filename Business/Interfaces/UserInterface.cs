using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface IUserInterface
    {
        
        Task<IEnumerable<UserDTO>> GetAllTeachersAsync();
    }
}
