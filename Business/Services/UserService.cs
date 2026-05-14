using Business.Interfaces;
using Data.DTOs;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class UserService : IUserInterface
    {
        public readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetAllStudentsAsync()
        {
            try
            {

                var student = await userRepository.GetAllStudentsAsync();

                return student.Select(student => new UserDTO
                {
                  
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    Email = student.Email
                });

            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred while fetching students: {ex.Message}");
                throw; // Re-throw the exception to be handled by the caller
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllTeachersAsync()
        {

            try
            {
              
                var teachers = await userRepository.GetAllTeachersAsync();

                return teachers.Select(teacher => new UserDTO
                {
                    FirstName = teacher.FirstName,
                    LastName = teacher.LastName,
                    Email = teacher.Email
                });

            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework here)
                Console.WriteLine($"An error occurred while fetching teachers: {ex.Message}");
                throw; // Re-throw the exception to be handled by the caller
            }
        }
    }
}
