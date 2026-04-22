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
           var course = new Course
            {
                 CourseID = courseDTO.CourseID,
                SubjectName = courseDTO.SubjectName,
                TotalMarks = courseDTO.TotalMarks,
                ClassName = courseDTO.ClassName,
                TeacherID = courseDTO.TeacherID
            };
            _dbContext.Courses.Add(course);
            await _dbContext.SaveChangesAsync();
            return courseDTO;
        }

        public async Task DeleteCourse(int id)
        {
          var course = await _dbContext.Courses.FindAsync(id);
            if (course != null)
            {
                _dbContext.Courses.Remove(course);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<CourseDTO>> GetAllCourses()
        {
           var courses = await _dbContext.Courses.ToListAsync();
            var courseDTOs = new List<CourseDTO>();
            foreach (var course in courses)
            {
                courseDTOs.Add(new CourseDTO
                {
                    SubjectName = course.SubjectName,
                    TotalMarks = course.TotalMarks,
                    ClassName = course.ClassName,
                    TeacherID = course.TeacherID
                });
            }
            return courseDTOs;
        }

        public async Task<UpdateCourseDTO> UpdateCourse(int id, UpdateCourseDTO courseDTO)
        {
            var course = await _dbContext.Courses.FindAsync(id);
            if (course != null)
            {
                course.SubjectName = courseDTO.SubjectName;
                course.TotalMarks = courseDTO.TotalMarks;
                course.ClassName = courseDTO.ClassName;
                course.TeacherID = courseDTO.TeacherID;
                await _dbContext.SaveChangesAsync();
            }
            return courseDTO;
        }
        }
    }

