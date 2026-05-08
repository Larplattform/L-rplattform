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
                         Course = new CourseDTO
                         {
                             CourseID = assigment.Course.CourseID,
                             SubjectName = assigment.Course.SubjectName,
                             TotalMarks = assigment.Course.TotalMarks,
                             StartDate = assigment.Course.StartDate,
                             EndDate = assigment.Course.EndDate,
                             Url = assigment.Course.Url,
                             ClassName = assigment.Course.ClassName,
                             TeacherID = assigment.Course.TeacherID
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
                        Course = new CourseDTO
                        {
                            CourseID = assigment.Course.CourseID,
                            SubjectName = assigment.Course.SubjectName,
                            TotalMarks = assigment.Course.TotalMarks,
                            StartDate = assigment.Course.StartDate,
                            EndDate = assigment.Course.EndDate,
                            Url = assigment.Course.Url,
                            ClassName = assigment.Course.ClassName,
                            TeacherID = assigment.Course.TeacherID
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

        public async Task<IEnumerable<AssigmentsDTO>> GetAllAssigmentsWithLessonsAsync()
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
                       Course = new CourseDTO
                       {
                           CourseID = assigment.Course.CourseID,
                           SubjectName = assigment.Course.SubjectName,
                           TotalMarks = assigment.Course.TotalMarks,
                           StartDate = assigment.Course.StartDate,
                           EndDate = assigment.Course.EndDate,
                           Url = assigment.Course.Url,
                           ClassName = assigment.Course.ClassName,
                           TeacherID = assigment.Course.TeacherID
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
                        Course = new CourseDTO
                        {
                            CourseID = assigment.Course.CourseID,
                            SubjectName = assigment.Course.SubjectName,
                            TotalMarks = assigment.Course.TotalMarks,
                            StartDate = assigment.Course.StartDate,
                            EndDate = assigment.Course.EndDate,
                            Url = assigment.Course.Url,
                            ClassName = assigment.Course.ClassName,
                            TeacherID = assigment.Course.TeacherID
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
