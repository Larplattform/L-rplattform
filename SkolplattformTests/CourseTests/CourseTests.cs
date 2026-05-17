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
        public async Task IF_CreateCourse_Works_Send_Sucess()
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
        public async Task IF_UpdateCourse_Works_Send_Sucess()
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
