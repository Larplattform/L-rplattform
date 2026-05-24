using Business.Interfaces;
using Business.Services;
using Data.DTOs;
using Data.Entities;
using Data.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkolplattformTests.LessonTests
{
    public class LessonTests
    {
        public readonly Mock<ILessonRepository> _LessonRepository;
        public readonly Mock<LessonInterface> _LessonService;

        public LessonTests()
        {
           _LessonRepository = new Mock<ILessonRepository>();
            _LessonService = new Mock<LessonInterface>();
        }

        [Fact]
        public async Task IF_Lesson_works_Send_NewLesson()
        {
            var NewLesson = new Lesson { Title = "Test", Description = "testlesson" };

            _LessonRepository.Setup(repo => repo.AddLessonAsync(It.IsAny<Lesson>())).ReturnsAsync(NewLesson);

            var respository = _LessonRepository.Object;

            var result = await respository.AddLessonAsync(NewLesson);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Title);
            Assert.Equal("testlesson", result.Description);

            _LessonRepository.Verify(repo => repo.AddLessonAsync(It.IsAny<Lesson>()), Times.Once);
        }

        [Fact]
        public async Task IF_CreateLessonDTO_Works_Send_CreatedLesson()
        {
            var createLessonDTO = new CreateLessonDTO
            {
               Title = "Test",
               Description = "Test",
               Content = "Test",
               CourseID = 1

               
            };


            var returnedDto = new CreateLessonDTO
            {
                Title = createLessonDTO.Title,
                Description = createLessonDTO.Description,
                Content = createLessonDTO.Content,
                CourseID = createLessonDTO.CourseID
            };

            _LessonService
                .Setup(service => service.CreateLessonAsync(It.IsAny<CreateLessonDTO>()))
                .ReturnsAsync(returnedDto);

            var lessonService = _LessonService.Object;
            var result = await lessonService.CreateLessonAsync(createLessonDTO);

            Assert.NotNull(result);
            Assert.Equal(createLessonDTO.Title, result.Title);
            Assert.Equal(createLessonDTO.Description, result.Description);

            _LessonService.Verify(service => service.CreateLessonAsync(It.IsAny<CreateLessonDTO>()), Times.Once);
        }


        [Fact]
        public async Task IF_GetAllLessons_Works_Send_AllLessons()
        {
            var newLesson = new Lesson { Title = "Test", Description = "testlesson" };

            // GetAllWithUsersAsync has no parameters and returns IEnumerable<Course>
            _LessonRepository
                .Setup(repo => repo.AllLessonsWithCoursesAsync())
                .ReturnsAsync(new List<Lesson> { newLesson });

            var respository = _LessonRepository.Object;

            var result = await respository.AllLessonsWithCoursesAsync();

            Assert.NotNull(result);
            var first = result.FirstOrDefault();
            Assert.NotNull(first);
            Assert.Equal("Test", first.Title);
            Assert.Equal("testlesson", first.Description);

            _LessonRepository.Verify(repo => repo.AllLessonsWithCoursesAsync(), Times.Once);
        }


        [Fact]
        public async Task IF_GetbOneLesson_Works_Send_LessonID_1()
        {
            var LessonId1 = new Lesson {  LessonID= 1, Title = "Test", Description = "testLesson" };

            _LessonRepository
                .Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(LessonId1);

            var respository = _LessonRepository.Object;

            var result = await respository.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Test", result.Title);
        }

        [Fact]
        public async Task IF_Updatelesson_Works_Send_Updatedlesson()
        {
            var newLesson = new Lesson {  Title= "Test", Description = "testLesson" };
            var UpdateLesson = new Lesson { Title = "TestUpdated", Description = "testlessonUpdated" };

           _LessonRepository
                .Setup(repo => repo.UpdateLesson(It.IsAny<Lesson>()))
                .Callback<Lesson>(c =>
                {
                    c.Title = UpdateLesson.Title;
                    c.Description = UpdateLesson.Description;
                });

            var respository = _LessonRepository.Object;

            // Call Update to trigger the callback
            respository.UpdateLesson(newLesson);

            Assert.NotNull(newLesson);
            Assert.Equal("TestUpdated", newLesson.Title);
            Assert.Equal("testlessonUpdated", newLesson.Description);

            _LessonRepository.Verify(repo => repo.UpdateLesson(It.IsAny<Lesson>()), Times.Once);
        }

        [Fact]
       
        public async Task IF_DeleteLesson_Works_Delete_Lesson()
        {
            var DeleteLesson = new Lesson { LessonID = 1, Title = "Test", Description = "testLesson" };
            _LessonRepository
                .Setup(repo => repo.DeleteAsync(1))
                .ReturnsAsync(DeleteLesson);
            var respository = _LessonRepository.Object;
            await respository.DeleteAsync(1);
           _LessonRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }

        [Fact]
        public async Task IF_GetCourseById_send_Course()
        {
            var teacherid = 1;
            var courseid = 1;

            var lessons = new List<Lesson>
            {
                 new Lesson {  LessonID=1 , Title = "Test", Description = "testlesson", CourseID= courseid, IsDeleted = false , Course = new Course { TeacherID =teacherid } },
                  new Lesson {  LessonID=2 , Title = "Test", Description = "testlesson", CourseID= courseid, IsDeleted = false , Course = new Course { TeacherID = teacherid } },
                  new Lesson {  LessonID=3 , Title = "Test", Description = "testlesson", CourseID= courseid, IsDeleted = true , Course = new Course { TeacherID = teacherid } }
            };

            _LessonRepository
               .Setup(repo => repo.GetByCourseIdAsync(courseid, teacherid.ToString())).ReturnsAsync(lessons.Where(x => x.CourseID == courseid && !x.IsDeleted).ToList());

            var repository = _LessonRepository.Object;

            var result = await repository.GetByCourseIdAsync(courseid , teacherid.ToString());

            Assert.NotNull(result);
            Assert.Equal(2 , result.Count());
            Assert.DoesNotContain(result , x => x.IsDeleted);
        }
    }
}
