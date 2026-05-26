using Business.Interfaces;
using Data.DTOs;
using Data.Entities;
using Data.Enums;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class ScheduleService : ScheduleInterface
    {
        private readonly IScheduleRepository _scheduleRepository;
        



        public ScheduleService(IScheduleRepository scheduleRepository)
        {
            _scheduleRepository = scheduleRepository;
           
            
           
        
        }
        // It adds a new schedule to the database by creating a Schedule entity from the provided CreateScheduleDTO, saving it using the repository, and returning the original DTO.
        public async Task<ScheduleDTO> AddScheduleAsync(CreateScheduleDTO schedule)
        {
            try
            {

                var scheduleCreate = new Schedule
                {
                    StartDate = schedule.StartDate,
                    EndDate = schedule.EndDate,
                    Location = (LocationEnum)schedule.Location,
                    TeacherID = schedule.TeacherID,
                    CourseID = schedule.CourseID

                };
                await _scheduleRepository.AddScheduleAsync(scheduleCreate);
                await _scheduleRepository.SaveChangesAsync();


                var course = await _scheduleRepository.GetScheduleByIdAsync(scheduleCreate.ScheduleID);
                return new ScheduleDTO
                {

                    ScheduleID = course.ScheduleID,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,
                    Location = (LocationEnumDTO)course.Location,
                    CourseID = course.CourseID,
                    Course = new CourseDTO
                    {
                        CourseID = course.Course.CourseID,
                        SubjectName = course.Course.SubjectName,
                        TotalMarks = course.Course.TotalMarks,
                        Url = course.Course.Url,
                        ClassName = course.Course.ClassName,
                        TeacherName = course.Teacher != null ? $"{course.Teacher.FirstName} {course.Teacher.FirstName}" : "Uknown Teacher",
                        TeacherID = course.Course.TeacherID,
                        StartDate = course.Course.StartDate,
                        EndDate = course.Course.EndDate,

                        Users = course.Course.CourseUsers.Select(u => new UserDTO
                        {
                            FirstName = u.User.FirstName,
                            LastName = u.User.LastName,
                            Email = u.User.Email,
                        }).ToList()
                    }
                };



            }
            catch (Exception ex)
            {
                // Log the exception (you can use a logging framework like Serilog, NLog, etc.)
                Console.WriteLine($"An error occurred while adding a schedule: {ex.Message}");
                throw; // Rethrow the exception to be handled by the caller
            }
        }
        // It deletes a schedule from the database by its ID. If the schedule is not found, it throws an exception. After deletion, it saves the changes using the repository.
        public async Task DeleteScheduleAsync(int id)
        {
            try
            {
                var schedule = await _scheduleRepository.DeleteScheduleAsync(id);
                if (schedule == null)
                {
                    throw new Exception($"Schedule with ID {id} not found.");
                }
                await _scheduleRepository.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while deleting a schedule: {ex.Message}");
                throw; // Rethrow the exception to be handled by the caller
            }
        }
        // It retrieves all schedules for a specific course by its ID. It converts the retrieved Schedule entities into ScheduleDTOs, including the related Course information, and returns a list of ScheduleDTOs.
        public async Task<IEnumerable<ScheduleDTO>> GetAllCourseScheduleAsync(int courseId)
        {
            try
            {
                var AllSchdule = await _scheduleRepository.GetAllCourseScheduleAsync(courseId);
               

                var scheduleDTOs = new List<ScheduleDTO>();
                foreach (var schedule in AllSchdule)
                {
                   
                    var scheduleDTO = new ScheduleDTO
                    {
                        ScheduleID = schedule.ScheduleID,
                        StartDate = schedule.StartDate,
                        EndDate = schedule.EndDate,
                   Location = (LocationEnumDTO)schedule.Location,
                        CourseID = schedule.CourseID,
                        Course = new CourseDTO
                        {
                            CourseID = schedule.Course.CourseID,
                            SubjectName = schedule.Course.SubjectName,
                            TotalMarks = schedule.Course.TotalMarks,
                            Url = schedule.Course.Url,
                            ClassName = schedule.Course.ClassName,
                            TeacherName = schedule.Teacher != null ? $"{schedule.Teacher.FirstName} {schedule.Teacher.FirstName}" : "Uknown Teacher",
                            TeacherID = schedule.Course.TeacherID,
                            StartDate = schedule.Course.StartDate,
                            EndDate = schedule.Course.EndDate,
                           
                            Users = schedule.Course.CourseUsers.Select(u => new UserDTO
                            {
                                FirstName = u.User.FirstName,
                                LastName = u.User.LastName,
                                Email = u.User.Email,
                            }).ToList()
                        },
                       
                    };
                    scheduleDTOs.Add(scheduleDTO);
                }
                return scheduleDTOs;

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while retrieving course schedules: {ex.Message}");
                throw; // Rethrow the exception to be handled by the caller
            }
        }
        // It retrieves all schedules from the database, converts them into ScheduleDTOs (including related Course information), and returns a list of ScheduleDTOs. If an error occurs during retrieval, it logs the exception and rethrows it.
        public async Task<IEnumerable<ScheduleDTO>> GetAllSchedulesAsync()
        {
            try
            {
               var AllSchdule =  await _scheduleRepository.GetAllSchedulesAsync();
              
                var scheduleDTOs = new List<ScheduleDTO>();
                foreach (var schedule in AllSchdule)
                {
                    var scheduleDTO = new ScheduleDTO
                    {
                        ScheduleID = schedule.ScheduleID,
                        StartDate = schedule.StartDate,
                        EndDate = schedule.EndDate,
                      Location = (LocationEnumDTO)schedule.Location,
                        CourseID = schedule.CourseID,
                        Course = new CourseDTO
                        {
                            CourseID = schedule.Course.CourseID,
                            SubjectName = schedule.Course.SubjectName,
                            TotalMarks = schedule.Course.TotalMarks,
                            Url = schedule.Course.Url,
                            ClassName = schedule.Course.ClassName,
                            TeacherName = schedule.Teacher != null ? $"{schedule.Teacher.FirstName} {schedule.Teacher.FirstName}" : "Uknown Teacher",
                            TeacherID = schedule.Course.TeacherID,
                            StartDate = schedule.Course.StartDate,
                            EndDate = schedule.Course.EndDate,
                            Users = schedule.Course.CourseUsers.Select(u => new UserDTO
                            {
                                FirstName = u.User.FirstName,
                                LastName = u.User.LastName,
                                Email = u.User.Email,
                            }).ToList()
                        },
                        
                    };
                    scheduleDTOs.Add(scheduleDTO);
                }
                return scheduleDTOs;

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while retrieving all schedules: {ex.Message}");
                throw; // Rethrow the exception to be handled by the caller
            }
        }
        // It retrieves all schedules that fall within a specified date range. It converts the retrieved Schedule entities into ScheduleDTOs, including related Course information, and returns a list of ScheduleDTOs. If an error occurs during retrieval, it logs the exception and rethrows it.
        public async Task<IEnumerable<ScheduleDTO>> GetAllSchedulesByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            try
            {

                var AllSchdule = await _scheduleRepository.GetAllSchedulesByDateRangeAsync(startDate, endDate);
                
                var scheduleDTOs = new List<ScheduleDTO>();
                foreach (var schedule in AllSchdule)
                {
                    var scheduleDTO = new ScheduleDTO
                    {
                        ScheduleID = schedule.ScheduleID,
                        StartDate = schedule.StartDate,
                        EndDate = schedule.EndDate,
                        Location = (LocationEnumDTO)schedule.Location,
                        CourseID = schedule.CourseID,
                        Course = new CourseDTO
                        {
                            CourseID = schedule.Course.CourseID,
                            SubjectName = schedule.Course.SubjectName,
                            TotalMarks = schedule.Course.TotalMarks,
                            ClassName = schedule.Course.ClassName,
                            TeacherName = schedule.Teacher != null ? $"{schedule.Teacher.FirstName} {schedule.Teacher.LastName}" : "Uknown Teacher",
                            TeacherID = schedule.Course.TeacherID,
                            StartDate = schedule.Course.StartDate,
                            EndDate = schedule.Course.EndDate,
                            Users = schedule.Course.CourseUsers.Select(u => new UserDTO
                            {
                                FirstName = u.User.FirstName,
                                LastName = u.User.LastName,
                                Email = u.User.Email,
                            }).ToList()
                        },
                       
                    };
                    scheduleDTOs.Add(scheduleDTO);
                }
                return scheduleDTOs;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while retrieving schedules by date range: {ex.Message}");
                throw; // Rethrow the exception to be handled by the caller
            }
        }
        // It retrieves a schedule by its ID. If the schedule is found, it converts it into a ScheduleDTO (including related Course information) and returns it. If the schedule is not found, it returns null. If an error occurs during retrieval, it logs the exception and rethrows it.
        public async Task<ScheduleDTO?> GetScheduleByIdAsync(int id)
        {
            try
            {
                var schedule = await _scheduleRepository.GetScheduleByIdAsync(id);
                if (schedule == null)
                {
                    return null;
                }
                var scheduleDTO = new ScheduleDTO
                {
                    ScheduleID = schedule.ScheduleID,
                    StartDate = schedule.StartDate,
                    EndDate = schedule.EndDate,
                  Location = (LocationEnumDTO)schedule.Location,
                    CourseID = schedule.CourseID,
                    Course = new CourseDTO
                    {
                        CourseID = schedule.Course.CourseID,
                        SubjectName = schedule.Course.SubjectName,
                        TotalMarks = schedule.Course.TotalMarks,
                        Url = schedule.Course.Url,
                        ClassName = schedule.Course.ClassName,
                        TeacherName = schedule.Teacher != null ? $"{schedule.Teacher.FirstName} {schedule.Teacher.LastName}" : "Uknown Teacher",
                        TeacherID = schedule.Course.TeacherID,
                        StartDate = schedule.Course.StartDate,
                        EndDate = schedule.Course.EndDate,
                        Users = schedule.Course.CourseUsers.Select(u => new UserDTO
                        {
                            FirstName = u.User.FirstName,
                            LastName = u.User.LastName,
                            Email = u.User.Email,
                        }).ToList()
                    }
                };

                return scheduleDTO;

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while retrieving a schedule by ID: {ex.Message}");
                throw; // Rethrow the exception to be handled by the caller
            }
        }

        // It retrieves a paginated list of schedules based on the provided page number and page size. It converts the retrieved Schedule entities into ScheduleDTOs, including related Course information, and returns an IEnumerable of ScheduleDTOs. If an error occurs during retrieval, it logs the exception and rethrows it.
        public async Task<IEnumerable<ScheduleDTO>> GetSchedulePagesAsync(int pageNumber, int pageSize)
        {
            try
            {
                var AllSchdule = await _scheduleRepository.GetSchedulePagesAsync(pageNumber, pageSize);
                    
                var scheduleDTOs = new List<ScheduleDTO>();
                foreach (var schedule in AllSchdule)
                {
                    var scheduleDTO = new ScheduleDTO
                    {
                        ScheduleID = schedule.ScheduleID,
                        StartDate = schedule.StartDate,
                        EndDate = schedule.EndDate,
                       Location = (LocationEnumDTO)schedule.Location,
                        CourseID = schedule.CourseID,
                        TeacherName = schedule.Teacher != null ? $"{schedule.Teacher.FirstName} {schedule.Teacher.LastName}" : "Uknown Teacher",
                        Course = new CourseDTO
                        {
                            CourseID = schedule.Course.CourseID,
                            SubjectName = schedule.Course.SubjectName,
                            TotalMarks = schedule.Course.TotalMarks,
                            Url = schedule.Course.Url,
                            ClassName = schedule.Course.ClassName,
                         
                            TeacherID = schedule.Course.TeacherID,
                            StartDate = schedule.Course.StartDate,
                            EndDate = schedule.Course.EndDate,
                            Users = schedule.Course.CourseUsers.Select(u => new UserDTO
                            {
                                FirstName = u.User.FirstName,
                                LastName = u.User.LastName,
                                Email = u.User.Email,
                            }).ToList()
                        },
                        
                    };
                    scheduleDTOs.Add(scheduleDTO);
                }
                return scheduleDTOs;

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while retrieving paginated schedules: {ex.Message}");
                throw; // Rethrow the exception to be handled by the caller
            }
        }

        public async Task<IEnumerable<ScheduleDTO>> GetScheduleStudentPagesAsync(int studentId, int pageNumber, int pageSize)
        {
            try
            {
                var AllSchdule = await _scheduleRepository.GetScheduleStudentPagesAsync(studentId,pageNumber, pageSize);

                var scheduleDTOs = new List<ScheduleDTO>();
                foreach (var schedule in AllSchdule)
                {
                    var scheduleDTO = new ScheduleDTO
                    {
                        ScheduleID = schedule.ScheduleID,
                        StartDate = schedule.StartDate,
                        EndDate = schedule.EndDate,
                        Location = (LocationEnumDTO)schedule.Location,
                        CourseID = schedule.CourseID,
                        TeacherName = schedule.Teacher != null ? $"{schedule.Teacher.FirstName} {schedule.Teacher.LastName}" : "Uknown Teacher",
                        Course = new CourseDTO
                        {
                            CourseID = schedule.Course.CourseID,
                            SubjectName = schedule.Course.SubjectName,
                            TotalMarks = schedule.Course.TotalMarks,
                            Url = schedule.Course.Url,
                            ClassName = schedule.Course.ClassName,
                          
                            TeacherID = schedule.Course.TeacherID,
                            StartDate = schedule.Course.StartDate,
                            EndDate = schedule.Course.EndDate,
                            Users = schedule.Course.CourseUsers.Select(u => new UserDTO
                            {
                                FirstName = u.User.FirstName,
                                LastName = u.User.LastName,
                                Email = u.User.Email,
                            }).ToList()
                        },

                    };
                    scheduleDTOs.Add(scheduleDTO);
                }
                return scheduleDTOs;

            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while retrieving paginated schedules: {ex.Message}");
                throw; // Rethrow the exception to be handled by the caller
            }
        }

        public async Task<UpdateScheduleDTO> UpdateScheduleAsync(int id, UpdateScheduleDTO schedule)
        {
            try
            {
                var scheduleid = await _scheduleRepository.GetScheduleByIdAsync(id);

               if (scheduleid == null)
                {
                    throw new Exception($"Schedule with ID {id} not found.");
                }
                scheduleid.StartDate = schedule.StartDate;
                scheduleid.EndDate = schedule.EndDate;
                scheduleid.Location = (LocationEnum)schedule.Location;
                scheduleid.CourseID = schedule.CourseID;
                await _scheduleRepository.UpdateScheduleAsync(scheduleid);
                await _scheduleRepository.SaveChangesAsync();

                return schedule;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"An error occurred while updating a schedule: {ex.Message}");
                throw; // Rethrow the exception to be handled by the caller
            }
        }
    }
}
