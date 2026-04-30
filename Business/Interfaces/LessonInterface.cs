using Data.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Interfaces
{
    public interface LessonInterface
    {
      
        public Task<CreateLessonDTO> CreateLessonAsync( CreateLessonDTO createLessonDTO);
        public Task<IEnumerable<LessonDTO>> GetAllLessons();

        public Task<IEnumerable<LessonDTO>> GetLessonsbyCourseId(int courseId);

        public Task<LessonDTO> GetLessonById(int id);

        public Task<UpdateLessonDTO> UpdateLessonAsync(int UpdateLesson,UpdateLessonDTO updateCourseDTO);

        public Task DeleteAsync(int id);


    }
}
