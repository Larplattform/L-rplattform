using Business.Interfaces;
using Data.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class DatabaseControlService : IDatabaseControllInterface
    {
        public readonly IUserRepository _UserRepository;
        public ILogger<DatabaseControlService> _logger;

        public DatabaseControlService(IUserRepository userRepository, ILogger<DatabaseControlService> logger)
        {
            _UserRepository = userRepository;
            _logger = logger;
        }

        public async Task CheckForData()
        {
            try
            {
                await _UserRepository.CheckForUser();
                _logger.LogInformation("User exists , Good");

            }catch (Exception ex)
            {
                _logger.LogCritical("Warning for dataloss {Message}", ex.Message);
            }
        }
    }
}
