using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutAppApi.Controllers;
using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;
using WorkoutAppApi.Models.Enums;
using WorkoutAppApi.Services.Interfaces;
using WorkoutAppApi.UnitTests.Fixture;

namespace WorkoutAppApi.UnitTest.Controllers
{
    public class ExerciseControllerTests
    {
        private readonly ExerciseController _controller;
        public ExerciseControllerTests()
        {
            var exerciseServiceMock = new Mock<IExerciseService>();
            _controller = new ExerciseController(exerciseServiceMock.Object);
            exerciseServiceMock.Setup(t => t.GetAllAsync()).ReturnsAsync(DataFixture.GetAllExercise());

        }
        [Fact]
        public async Task OnGetAllAsync_WhenSuccesful_ShouldReturn3Results()
        {
            var response = (OkObjectResult)(await _controller.GetAllAsync()).Result;
            var responseResult = (List<ExerciseResponseDto>)response.Value;

            Assert.Equal(3, responseResult.Count);
        }

        [Fact]
        public async Task OnGetAllAsync_WhenSuccesful_ShouldReturnResultStatus200()
        {
            var response = (OkObjectResult)(await _controller.GetAllAsync()).Result;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task GetAllActive_WhenSuccesful_ShouldReturn3Results()
        {
            var response = (OkObjectResult)(await _controller.GetAllAsync()).Result;
            var responseResult = (List<ExerciseResponseDto>)response.Value;

            Assert.Equal(3, responseResult.Count);
        }

        [Fact]
        public async Task GetAllActive_WhenSuccesful_ShouldReturnResultStatus200()
        {
            var response = (OkObjectResult)(await _controller.GetAllAsync()).Result;

            Assert.Equal(200, response.StatusCode);
        }
    }
}
