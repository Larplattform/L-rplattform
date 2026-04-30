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
            }catch (Exception ex)
            {

                throw new ApplicationException("An error occurred while creating the Lesson.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var Lesson = await _LessonRepository.Delete(id);
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

        public async Task<IEnumerable<LessonDTO>> GetAllLessons()
        {
            try
            {
                var GetAllesson = await _LessonRepository.AllLessonsWithCoursesAsync();
                
                var lessonDtos = new List<LessonDTO>();
               
                foreach (var lesson in GetAllesson)
                {
                    lessonDtos.Add(new LessonDTO
                    {
                        LessonID = lesson.LessonID,
                        Content = lesson.Content,
                        Description = lesson.Description,
                        Title = lesson.Title,
                        CourseID = lesson.CourseID,

                    });

                }
                
                return lessonDtos;
            }catch(Exception ex)
            {
                throw new ApplicationException("An error occurred while retrieving Lessons.", ex);
            }
        }

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

                };
            }catch(Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving the Lesson with ID {id}.", ex);
            }
        }

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

                    });

                }

                return lessonDtos;
            }catch(Exception ex)
            {
                throw new ApplicationException($"An error occurred while retrieving lessons for course with ID {courseId}.", ex);
            }
           
        }

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
