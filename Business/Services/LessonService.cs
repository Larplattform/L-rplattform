using Business.Interfaces;
using Data.DTOs;
using Data.Entities;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Services
{
    public class LessonService : LessonInterface
    {
        public readonly ILessonRepository _LessonRepository;

        public LessonService(ILessonRepository LessonRepository)
        {
            _LessonRepository = LessonRepository;

        }

        // This method creates a new lesson based on the provided CreatelessonDTO and saves it to the repository.
        public async Task<CreateLessonDTO> CreateLessonAsync(CreateLessonDTO createLessonDTO)
        {
            try
            {
                var newLesson = new Lesson
                {

                    Content = createLessonDTO.Content,
                    Description = createLessonDTO.Description,
                    Title = createLessonDTO.Title,
                    CourseID = createLessonDTO.CourseID,

                };


               await _LessonRepository.AddLessonAsync(newLesson);
                await _LessonRepository.SaveChangesAsync();

                return createLessonDTO;
            }
            catch (Exception ex)
            {

                throw new ApplicationException("An error occurred while creating the Lesson.", ex);
            }
        }

        // This method deletes a lesson with the specified Id from the repository.

        public async Task DeleteAsync(int id)
        {
            try
            {
                var Lesson = await _LessonRepository.DeleteAsync(id);
                if (Lesson == null)
                {
                    throw new KeyNotFoundException($"lessons with ID {id} not found.");
                }
                await _LessonRepository.SaveChangesAsync();
            }catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while deleting the Lesson with ID {id}.", ex);
            }
        }

        // This method retrieves all lessons from the repository and returns them as a collection of lessonDTOs.
        public async Task<IEnumerable<LessonDTO>> GetAllLessons()
        {
            try
            {
                var GetAllesson = await _LessonRepository.AllLessonsWithCoursesAsync();


                return GetAllesson.Select(x => new LessonDTO
                {
                    LessonID = x.LessonID,
                    Title = x.Title,
                    Description = x.Description,
                    Content = x.Content,
                    CourseID = x.CourseID,
                    CourseName = x.Course?.SubjectName ?? "No CourseName",
                    CourseTotalMark = x.Course?.TotalMarks.ToString() ?? "no Total Marks",
                    TeacherID = x.Course?.TeacherID ?? 0,
                    TeacherName = x.Course?.CourseUsers.Where(u => u.UserID == x.Course.TeacherID).Select(u => $"{u.User.FirstName} {u.User.LastName}").FirstOrDefault() ?? "Unknown Teacher"

                }).ToList();
                
                
                
              
            }catch(Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving Lessons.", ex);
            }
        }

        // This method retrieves a lesson with the specified Id from the repository and returns it as a lessonDTO.

        public async Task<LessonDTO> GetLessonById(int id)
        {
            try
            {
                var lesson = await _LessonRepository.GetByIdAsync(id);

                if(lesson == null)
                {
                    throw new KeyNotFoundException($"lessons with ID {id} not found.");
                }
                return new LessonDTO
                {
                    LessonID = lesson.LessonID
                   ,
                    Content = lesson.Content,
                    Title = lesson.Title,
                    Description = lesson.Description,
                    CourseID = lesson.CourseID,
                    CourseName = lesson.Course?.SubjectName ?? "No CourseName",
                    CourseTotalMark = lesson.Course?.TotalMarks.ToString() ?? "no Total Marks",
                    TeacherID = lesson.Course?.TeacherID ?? 0,
                    TeacherName = lesson.Course?.CourseUsers.Where(u => u.UserID == u.Course.TeacherID).Select(u => $"{u.User.FirstName} {u.User.LastName}").FirstOrDefault() ?? "Unknown Teacher"

                };
            }catch(Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving the Lesson with ID {id}.", ex);
            }
        }

        // This method retrieves a lessons with the specified courseId associated with the course and returns it as a lessonDTO.

        public async Task<IEnumerable<LessonDTO>> GetLessonsbyCourseId(int courseId)
        {
            try
            {
                var GetAllesson = await _LessonRepository.AllLessonsWithCoursesAsync();

                var filteredLessons = GetAllesson.Where(l => l.CourseID == courseId);

                var lessonDtos = new List<LessonDTO>();

                foreach (var lesson in filteredLessons)
                {
                    lessonDtos.Add(new LessonDTO
                    {
                        LessonID = lesson.LessonID,
                        Content = lesson.Content,
                        Description = lesson.Description,
                        Title = lesson.Title,
                        CourseID = lesson.CourseID,
                        TeacherName = lesson.Course?.CourseUsers.Where(u => u.UserID == u.Course.TeacherID).Select(u => $"{u.User.FirstName} {u.User.LastName}").FirstOrDefault() ?? "Unknown Teacher",
                        TeacherID = lesson.Course?.TeacherID ?? 0,

                    });

                }

                return lessonDtos;
            }catch(Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving lessons for course with ID {courseId}.", ex);
            }
           
        }

        // This method updates an existing lesson with the specified Id based on the provided UpdatelessonDTO.

        public async Task<UpdateLessonDTO> UpdateLessonAsync(int UpdateLesson,UpdateLessonDTO updatelessonDTO)
        {
            try
            {
                var lessonUpdate = await _LessonRepository.GetByIdAsync(UpdateLesson);
                if (lessonUpdate != null)
                {
                    lessonUpdate.Title = updatelessonDTO.Title;
                    lessonUpdate.Content = updatelessonDTO.Content;
                    lessonUpdate.Description = updatelessonDTO.Description;
                    lessonUpdate.CourseID = updatelessonDTO.CourseID;
                    await _LessonRepository.SaveChangesAsync();

                }
                return updatelessonDTO;
            }catch (Exception ex)
            {
                throw new ApplicationException($"An error occurred while updating the Lesson with ID {UpdateLesson}.", ex);
            }
          
        }
    }
}
