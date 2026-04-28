using Business.Interfaces;
using Data.Context;
using Data.DTOs;
using Data.Entities;
using Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class CourseService : ICourseInterface
    {
        public readonly ICourseRepository _courseRepository;
        public readonly IUserRepository _userRepository;

        public CourseService(ICourseRepository courseRepository, IUserRepository userRepository)
        {
            _courseRepository = courseRepository;
            _userRepository = userRepository;
        }

        // This method creates a new course based on the provided CreateCourseDTO and saves it to the repository.
        public async Task<CreateCourseDTO> CreateCourse(CreateCourseDTO courseDTO)
        {
            try
            {
                var course = new Course
                {

                    SubjectName = courseDTO.SubjectName,
                    TotalMarks = courseDTO.TotalMarks,
                    ClassName = courseDTO.ClassName,
                    Url = courseDTO.Url,
                    TeacherID = courseDTO.TeacherID
                };
               
                await _courseRepository.AddAsync(course);
                await _courseRepository.SaveChangesAsync();
             return courseDTO;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("An error occurred while creating the course.", ex);
            }
        }

        // This method deletes a course with the specified courseId from the repository.
        public async Task DeleteCourse(int courseId)
        {
            try
            {
              var course = await _courseRepository.DeleteAsync(courseId);
                if (course == null)
                {
                    throw new KeyNotFoundException($"Course with ID {courseId} not found.");
                }
             
             
               await _courseRepository.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting the course with ID {courseId}.", ex);
            }
        }

        // This method retrieves all courses from the repository and returns them as a collection of CourseDTOs.
        public async Task<IEnumerable<CourseDTO>> GetAllCourses()
        {
            try
            {
                var courses = await _courseRepository.GetAllWithUsersAsync();
                var courseDTOs = new List<CourseDTO>();
                foreach (var course in courses)
                {
                    courseDTOs.Add(new CourseDTO
                    {
                        CourseID = course.CourseID,
                        SubjectName = course.SubjectName,
                        TotalMarks = course.TotalMarks,
                        ClassName = course.ClassName,
                        TeacherID = course.TeacherID,
                        Url = course.Url,
                        Users = course.Users.Select(u => new UserDTO
                        {

                            FirstName = u.FirstName,
                            LastName = u.LastName,
                          

                        }).ToList()


                    });
                }
                return courseDTOs;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving courses.", ex);

            }
        }


        // This method retrieves a course with the specified courseId from the repository and returns it as a CourseDTO.

        public async Task<CourseDTO> GetCourseById(int courseId)
        {
            try
            {
                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    throw new KeyNotFoundException($"Course with ID {courseId} not found.");
                }
                return new CourseDTO
                {
                    CourseID = course.CourseID,
                    SubjectName = course.SubjectName,
                    TotalMarks = course.TotalMarks,
                    ClassName = course.ClassName,
                    TeacherID = course.TeacherID,
                    Url = course.Url,
                    Users = course.Users.Select(u => new UserDTO
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                    }).ToList()
                };
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving the course with ID {courseId}.", ex);
            }
        }

        public async Task<IEnumerable<CourseDTO>> GetCoursesByTeacherId(int teacherId)
        {
            try
            {

                var courses = await _courseRepository.GetAllTeachersByIdAsync(teacherId);
                var courseDTOs = new List<CourseDTO>();
                foreach (var course in courses)
                {
                    courseDTOs.Add(new CourseDTO
                    {
                        CourseID = course.CourseID,
                        SubjectName = course.SubjectName,
                        TotalMarks = course.TotalMarks,
                        ClassName = course.ClassName,
                        TeacherID = course.TeacherID,
                        Url = course.Url,
                        Users = course.Users.Select(u => new UserDTO
                        {
                            FirstName = u.FirstName,
                            LastName = u.LastName,
                        }).ToList()
                    });
                }
                return courseDTOs;


            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving courses for teacher with ID {teacherId}.", ex);
            }
        }

        // This method links a student to a course based on the provided LinkStudentToCourseDTO.
        public async Task<LinkStudentToCourseDTO> LinkStudentToCourse(LinkStudentToCourseDTO linkDTO)
        {
            try
            {
              

                var course = await _courseRepository.GetByIdAsync(linkDTO.CourseId);
                if (course == null)
                {
                    throw new KeyNotFoundException($"Course with ID {linkDTO.CourseId} not found.");
                }
                var user = await _userRepository.GetUserByIdAsync(linkDTO.UserId);
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {linkDTO.UserId} not found.");
                }
                course.Users.Add(user);
                await _courseRepository.SaveChangesAsync();
                return linkDTO;





            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while linking student with ID {linkDTO.UserId} to course with ID {linkDTO.CourseId}.", ex);
            }
        }


        

        // This method updates an existing course with the specified courseId based on the provided UpdateCourseDTO.
        public async Task<UpdateCourseDTO> UpdateCourse(int courseId, UpdateCourseDTO courseDTO)
        {
            try
            {
                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course != null)
                {
                    course.SubjectName = courseDTO.SubjectName;
                    course.TotalMarks = courseDTO.TotalMarks;
                    course.ClassName = courseDTO.ClassName;
                    course.TeacherID = courseDTO.TeacherID;
                    course.Url = courseDTO.Url;
                    await _courseRepository.SaveChangesAsync();
                }
                return courseDTO;

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while updating the course with ID {courseId}.", ex);

            }
        }
    }
    }

