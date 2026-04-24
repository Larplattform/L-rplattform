using Business.Interfaces;
using Data.Context;
using Data.DTOs;
using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class CourseService : ICourseInterface
    {
        private readonly ApplicationDbContext _dbContext;

        public CourseService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

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
                _dbContext.Courses.Add(course);
                await _dbContext.SaveChangesAsync();
                return courseDTO;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("An error occurred while creating the course.", ex);
            }
        }

        public async Task DeleteCourse(int courseId)
        {
            try
            {
                var course = await _dbContext.Courses.FindAsync(courseId);
                if (course == null)
                {
                    throw new KeyNotFoundException($"Course with ID {courseId} not found.");
                }
             
                course.IsDeleted = true;
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting the course with ID {courseId}.", ex);
            }
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCourses()
        {
            try
            {
                var courses = await _dbContext.Courses.Include(c => c.Users).Where(c => !c.IsDeleted).ToListAsync();
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

       
       
        public async Task<CourseDTO> GetCourseById(int courseId)
        {
            try
            {
                var course = await _dbContext.Courses.Include(c => c.Users).FirstOrDefaultAsync(c => c.CourseID == courseId && !c.IsDeleted);
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

        public async Task<LinkStudentToCourseDTO> LinkStudentToCourse(LinkStudentToCourseDTO linkDTO)
        {
            try
            {
                var course = await _dbContext.Courses.Include(x => x.Users).
                             FirstOrDefaultAsync(c => c.CourseID == linkDTO.CourseId);

                var user = await _dbContext.Users.FindAsync(linkDTO.UserId);

                if (!course.Users.Any(u => u.Id == linkDTO.UserId))
                {
                    course.Users.Add(user);
                    await _dbContext.SaveChangesAsync();
                }

                return linkDTO;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while linking student with ID {linkDTO.UserId} to course with ID {linkDTO.CourseId}.", ex);
            }
        }

        public async Task<UpdateCourseDTO> UpdateCourse(int courseId, UpdateCourseDTO courseDTO)
        {
            try
            {
                var course = await _dbContext.Courses.FindAsync(courseId);
                if (course != null)
                {
                    course.SubjectName = courseDTO.SubjectName;
                    course.TotalMarks = courseDTO.TotalMarks;
                    course.ClassName = courseDTO.ClassName;
                    course.TeacherID = courseDTO.TeacherID;
                    course.Url = courseDTO.Url;
                    await _dbContext.SaveChangesAsync();
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

