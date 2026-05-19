using Data.Entities;
using Data.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkolplattformTests.CourseTests
{
    public class CourseTests
    {
        public readonly Mock<ICourseRepository> _courseRepository;

        public CourseTests()
        {
            _courseRepository = new Mock<ICourseRepository>();
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
            var newCourse = new Course { CourseID = 1,ClassName = "Test", SubjectName = "testsubjsct" };

            // GetAllWithUsersAsync has no parameters and returns IEnumerable<Course>
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
    }
}
