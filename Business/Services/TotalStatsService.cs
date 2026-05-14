using Business.Interfaces;
using Data.DTOs;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class TotalStatsService : ITotalStatsInterface
    {
        public ICourseRepository _courseRepository;
        public IUserRepository _userRepository;

        public TotalStatsService(IUserRepository userRepository, ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }
           
        public async Task<TotalCountDTO> GetAllStats()
        {
            int CourseCounts = await _courseRepository.CountAllCourses();
            int StudentsCounts = await _userRepository.CountAllStudentsAsync();
            int TeacherCounts = await _userRepository.CountAllTeachersAsync();

            return new TotalCountDTO
            {
                CourseCount = CourseCounts,
                Students = StudentsCounts,
                Teachers = TeacherCounts,

              

            };

        }
    }
}
