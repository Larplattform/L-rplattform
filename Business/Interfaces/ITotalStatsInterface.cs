using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface ITotalStatsInterface
    {
        public Task<TotalCountDTO> GetAllStats();
    }
}
