using Business.Interfaces;
using Data.DTOs;
using Data.Entities;
using Data.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SkolplattformTests.CourseTests
{
    public class CourseTests
    {
        public readonly Mock<ICourseRepository> _courseRepository;

        public readonly Mock<ICourseInterface> _courseInterface;

        public CourseTests()
        {
            _courseRepository = new Mock<ICourseRepository>();
            _courseInterface = new Mock<ICourseInterface>();
        }

        [Fact]
        public async Task IF_CreateCourse_Works_Send_NewCourse()
        {
            var newCourse = new Course { ClassName = "Test", SubjectName = "testsubjsct" };

            _courseRepository.Setup(repo => repo.AddAsync(It.IsAny<Course>())).ReturnsAsync(newCourse);

            var respository = _courseRepository.Object;

            var result = await respository.AddAsync(newCourse);

            Assert.NotNull(result);
            Assert.Equal("Test", result.ClassName);
            Assert.Equal("testsubjsct", result.SubjectName);

            _courseRepository.Verify(repo => repo.AddAsync(It.IsAny<Course>()), Times.Once);
        }

        [Fact]
        public async Task IF_CreateCourseDTO_Works_Send_CreatedCourse()
        {
            var createCourseDTO = new CreateCourseDTO
            {
                ClassName = "TestClass",
                SubjectName = "TestSubject",
                TotalMarks = 100,
                TeacherID = 1,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddMonths(1),
                Url = null
            };

           
            var returnedDto = new CreateCourseDTO
            {
                ClassName = createCourseDTO.ClassName,
                SubjectName = createCourseDTO.SubjectName,
                TotalMarks = createCourseDTO.TotalMarks,
                TeacherID = createCourseDTO.TeacherID,
                StartDate = createCourseDTO.StartDate,
                EndDate = createCourseDTO.EndDate,
                Url = createCourseDTO.Url
            };

            _courseInterface
                .Setup(service => service.CreateCourse(It.IsAny<CreateCourseDTO>()))
                .ReturnsAsync(returnedDto);

            var courseService = _courseInterface.Object;
            var result = await courseService.CreateCourse(createCourseDTO);

            Assert.NotNull(result);
            Assert.Equal(createCourseDTO.ClassName, result.ClassName);
            Assert.Equal(createCourseDTO.SubjectName, result.SubjectName);

            _courseInterface.Verify(service => service.CreateCourse(It.IsAny<CreateCourseDTO>()), Times.Once);
        }

        [Fact]
        public async Task IF_GetAllCourse_Works_Send_AllCoures()
        {
            var newCourse = new Course { ClassName = "Test", SubjectName = "testsubjsct" };

            // GetAllWithUsersAsync has no parameters and returns IEnumerable<Course>
            _courseRepository
                .Setup(repo => repo.GetAllWithUsersAsync())
                .ReturnsAsync(new List<Course> { newCourse });

            var respository = _courseRepository.Object;

            var result = await respository.GetAllWithUsersAsync();

            Assert.NotNull(result);
            var first = result.FirstOrDefault();
            Assert.NotNull(first);
            Assert.Equal("Test", first.ClassName);
            Assert.Equal("testsubjsct", first.SubjectName);

            _courseRepository.Verify(repo => repo.GetAllWithUsersAsync(), Times.Once);
        }

        [Fact]
        public async Task IF_GetbOneCourse_Works_Send_CourseID_1()
        {
            var newCourse = new Course { CourseID = 1, ClassName = "Test", SubjectName = "testsubjsct" };

            _courseRepository
                .Setup(repo => repo.GetByIdAsync(1)).ReturnsAsync(newCourse);

            var respository = _courseRepository.Object;

            var result = await respository.GetByIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("Test", result.ClassName);
        }

        [Fact]
        public async Task IF_UpdateCourse_Works_Send_UpdatedCourse()
        {
            var newCourse = new Course { ClassName = "Test", SubjectName = "testsubjsct" };
            var UpdateCourse = new Course { ClassName = "TestUpdated", SubjectName = "testsubjectUpdated" };

            _courseRepository
                .Setup(repo => repo.Update(It.IsAny<Course>()))
                .Callback<Course>(c =>
                {
                    c.ClassName = UpdateCourse.ClassName;
                    c.SubjectName = UpdateCourse.SubjectName;
                });

            var respository = _courseRepository.Object;

            // Call Update to trigger the callback
            respository.Update(newCourse);

            Assert.NotNull(newCourse);
            Assert.Equal("TestUpdated", newCourse.ClassName);
            Assert.Equal("testsubjectUpdated", newCourse.SubjectName);

            _courseRepository.Verify(repo => repo.Update(It.IsAny<Course>()), Times.Once);
        }

        [Fact]
        public async Task IF_DeleteCourse_Works_Send_CourseID_1()
        {
            var newCourse = new Course { CourseID = 1, ClassName = "Test", SubjectName = "testsubjsct" };
            _courseRepository
                .Setup(repo => repo.DeleteAsync(1))
                .ReturnsAsync(newCourse);
            var respository = _courseRepository.Object;
            await respository.DeleteAsync(1);
            _courseRepository.Verify(repo => repo.DeleteAsync(1), Times.Once);
        }
    }
}
