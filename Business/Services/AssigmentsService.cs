using Business.Interfaces;
using Data.DTOs;
using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class AssigmentsService : AssigmentsInterface
    {
        public readonly IAssigmentsRepository AssigmentsRepository;
        public AssigmentsService(IAssigmentsRepository assigmentsRepository)
        {
            AssigmentsRepository = assigmentsRepository;
        }

        public async Task<CreateAssigmentsDTO> CreateAssigmentAsync(CreateAssigmentsDTO assigment)
        {
            try
            {
                var creaeatedAssigment = new Assigment
                {
                    Title = assigment.Title,
                    Description = assigment.Description,
                    Marks = assigment.Marks,
                    Url = assigment.Url,
                    CourseID = assigment.CourseID,
                    DueDate = assigment.DueDate,
                    
                    
                };

                await AssigmentsRepository.CreateAssigmentAsync(creaeatedAssigment);
                await AssigmentsRepository.SaveChangesAsync();
                return assigment;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while creating the assigment: {ex.Message}");
            }
        }
        public async Task<IEnumerable<AssigmentsDTO>> GetAllAssigmentsByCourseIdAsync(int courseId)
        {
            try
            {
                var assigments = await AssigmentsRepository.GetAllAssigmentsByCourseIdAsync(courseId);
                if (assigments == null)
                {
                    throw new Exception("No assigments found for the specified course.");
                }
                var assigmentsDTO = new List<AssigmentsDTO>();
                foreach (var assigment in assigments)
                {
                    assigmentsDTO.Add(new AssigmentsDTO
                    {
                        AssigmentID = assigment.AssigmentID,
                        Title = assigment.Title,
                        Description = assigment.Description,
                        Marks = assigment.Marks,
                         CourseID = assigment.CourseID,
                         Url = assigment.Url,
                         DueDate= assigment.DueDate,
                         
                         Course = new CourseDTO
                         {
                             CourseID = assigment.Course.CourseID,
                             SubjectName = assigment.Course.SubjectName,
                             TotalMarks = assigment.Course.TotalMarks,
                             StartDate = assigment.Course.StartDate,
                             EndDate = assigment.Course.EndDate,
                             Url = assigment.Course.Url,
                             ClassName = assigment.Course.ClassName,
                             TeacherID = assigment.Course.TeacherID,
                             TeacherName = assigment.Course?.CourseUsers.Where(u => u.UserID == assigment.Course.TeacherID).Select(u => $"{u.User.FirstName} {u.User.LastName}").FirstOrDefault() ?? "Unknown Teacher"
                         }
                    });
                }
                return assigmentsDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the assigments: {ex.Message}");

            }
        }

        public  async Task<IEnumerable<AssigmentsDTO>> GetAllAssigmentsbyStudentIdAsync(int studentId)
        {
            try
            {
                var assigments = await AssigmentsRepository.GetAllAssigmentsbyStudentId(studentId);
                if (assigments == null)
                {
                    throw new Exception("No assigments found for the specified teacher.");
                }
                var assigmentsDTO = new List<AssigmentsDTO>();
                foreach (var assigment in assigments)
                {
                    assigmentsDTO.Add(new AssigmentsDTO
                    {
                        AssigmentID = assigment.AssigmentID,
                        Title = assigment.Title,
                        Description = assigment.Description,
                        Marks = assigment.Marks,
                        CourseID = assigment.Course.CourseID,
                        DueDate = assigment.DueDate,
                        Url = assigment.Url,
                        Course = new CourseDTO
                        {
                            CourseID = assigment.Course.CourseID,
                            SubjectName = assigment.Course.SubjectName,
                            TotalMarks = assigment.Course.TotalMarks,
                            StartDate = assigment.Course.StartDate,
                            EndDate = assigment.Course.EndDate,
                            Url = assigment.Course.Url,
                            ClassName = assigment.Course.ClassName,
                            TeacherID = assigment.Course.TeacherID,
                            TeacherName = assigment.Course?.CourseUsers.Where(u => u.UserID == assigment.Course.TeacherID).Select(u => $"{u.User.FirstName} {u.User.LastName}").FirstOrDefault() ?? "Unknown Teacher"
                        }
                    });
                }
                return assigmentsDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the assigments: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AssigmentsDTO>> GetAllAssigmentsByTeacherIdAsync(int teacherId)
        {
            try
            {
                var assigments = await AssigmentsRepository.GetAllAssigmentsByTeacherIdAsync(teacherId);
                if (assigments == null)
                {
                    throw new Exception("No assigments found for the specified teacher.");
                }
                var assigmentsDTO = new List<AssigmentsDTO>();
                foreach (var assigment in assigments)
                {
                    assigmentsDTO.Add(new AssigmentsDTO
                    {
                        AssigmentID = assigment.AssigmentID,
                        Title = assigment.Title,
                        Description = assigment.Description,
                        Marks = assigment.Marks,
                        CourseID = assigment.Course.CourseID,
                        DueDate = assigment.DueDate,
                        Url = assigment.Url,
                        Course = new CourseDTO
                        {
                            CourseID = assigment.Course.CourseID,
                            SubjectName = assigment.Course.SubjectName,
                            TotalMarks = assigment.Course.TotalMarks,
                            StartDate = assigment.Course.StartDate,
                            EndDate = assigment.Course.EndDate,
                            Url = assigment.Course.Url,
                            ClassName = assigment.Course.ClassName,
                            TeacherID = assigment.Course.TeacherID,
                            TeacherName = assigment.Course?.CourseUsers.Where(u => u.UserID == assigment.Course.TeacherID).Select(u => $"{u.User.FirstName} {u.User.LastName}").FirstOrDefault() ?? "Unknown Teacher"
                        }
                    });
                }
                return assigmentsDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the assigments: {ex.Message}");
            }
        }

        public async Task<IEnumerable<AssigmentsDTO>> GetAllAssigmentsWithCoursesAsync()
        {
            try
            {
                var assigments = await AssigmentsRepository.GetAllAssigmentsWithCourseAsync();
                if (assigments == null)
                {
                    throw new Exception("No assigments found.");
                }
                var assigmentsDTO = new List<AssigmentsDTO>();
                foreach (var assigment in assigments)
                {
                    assigmentsDTO.Add(new AssigmentsDTO
                    {
                        AssigmentID = assigment.AssigmentID,
                        Title = assigment.Title,
                        Description = assigment.Description,
                        Marks = assigment.Marks,
                       CourseID = assigment.Course.CourseID,
                       DueDate = assigment.DueDate,
                       Url = assigment.Url,
                       Course = new CourseDTO
                       {
                           CourseID = assigment.Course.CourseID,
                           SubjectName = assigment.Course.SubjectName,
                           TotalMarks = assigment.Course.TotalMarks,
                           StartDate = assigment.Course.StartDate,
                           EndDate = assigment.Course.EndDate,
                           Url = assigment.Course.Url,
                           ClassName = assigment.Course.ClassName,
                           TeacherID = assigment.Course.TeacherID,
                           TeacherName = assigment.Course?.CourseUsers.Where(u => u.UserID == assigment.Course.TeacherID).Select(u => $"{u.User.FirstName} {u.User.LastName}").FirstOrDefault() ?? "Unknown Teacher"
                       }

                    });
                }
                return assigmentsDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the assigments: {ex.Message}");
            }
        }

        public async Task<AssigmentsDTO?> GetAssigmentByIdAsync(int id)
        {
            try
            {
                var assigment = await AssigmentsRepository.GetAssigmentByIdAsync(id);
                if (assigment == null)
                {
                    throw new Exception("Assigment not found.");
                }

                return new AssigmentsDTO
                {
                    AssigmentID = assigment.AssigmentID,
                    Title = assigment.Title,
                    Description = assigment.Description,
                    Marks = assigment.Marks,
                    CourseID = assigment.CourseID,
                    DueDate = assigment.DueDate,
                    Url = assigment.Url,
                        Course = new CourseDTO
                        {
                            CourseID = assigment.Course.CourseID,
                            SubjectName = assigment.Course.SubjectName,
                            TotalMarks = assigment.Course.TotalMarks,
                            StartDate = assigment.Course.StartDate,
                            EndDate = assigment.Course.EndDate,
                            Url = assigment.Course.Url,
                            ClassName = assigment.Course.ClassName,
                            TeacherID = assigment.Course.TeacherID,
                            TeacherName = assigment.Course?.CourseUsers.Where(u => u.UserID == assigment.Course.TeacherID).Select(u => $"{u.User.FirstName} {u.User.LastName}").FirstOrDefault() ?? "Unknown Teacher"
                        }

                };

            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while retrieving the assigment: {ex.Message}");
            }
        }
    }
}
