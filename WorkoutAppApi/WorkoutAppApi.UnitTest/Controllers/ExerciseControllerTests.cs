using Microsoft.AspNetCore.Http.HttpResults;
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
        private readonly Mock<IExerciseService> _exerciseServiceMock;
        public ExerciseControllerTests()
        {
            _exerciseServiceMock = new Mock<IExerciseService>();
            _controller = new ExerciseController(_exerciseServiceMock.Object);
            

        }
        [Fact]
        public async Task OnGetAllAsync_WhenSuccesful_ShouldReturn3Results()
        {
            _exerciseServiceMock.Setup(t => t.GetAllAsync()).ReturnsAsync(DataFixture.GetAllExercise());
            var response = (OkObjectResult)(await _controller.GetAllAsync()).Result;
            var responseResult = (List<ExerciseResponseDto>)response.Value;

            Assert.Equal(3, responseResult.Count);
        }

        [Fact]
        public async Task OnGetAllAsync_WhenSuccesful_ShouldReturnResultStatus200()
        {
            _exerciseServiceMock.Setup(t => t.GetAllAsync()).ReturnsAsync(DataFixture.GetAllExercise());

            var response = (OkObjectResult)(await _controller.GetAllAsync()).Result;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task OnGetAllActiveAsync_WhenSuccesful_ShouldReturn3Results()
        {
            _exerciseServiceMock.Setup(t => t.GetAllActiveAsync()).ReturnsAsync(DataFixture.GetAllExercise());

            var response = (OkObjectResult)(await _controller.GetAllActiveAsync()).Result;
            var responseResult = (List<ExerciseResponseDto>)response.Value;

            Assert.Equal(3, responseResult.Count);
        }

        [Fact]
        public async Task OnGetAllActiveAsync_WhenSuccesful_ShouldReturnResultStatus200()
        {
            _exerciseServiceMock.Setup(t => t.GetAllActiveAsync()).ReturnsAsync(DataFixture.GetAllExercise());

            var response = (OkObjectResult)(await _controller.GetAllActiveAsync()).Result;

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task OnGetExercisesByUserAsync_WhenSuccesful_ShouldReturn3Excercises()
        {
            _exerciseServiceMock.Setup(t => t.GetExercisesByUserAsync("12345")).ReturnsAsync(DataFixture.GetAllExercise());

            var response = (OkObjectResult)(await _controller.GetExercisesByUserAsync("12345")).Result;
            var responseResult = (List<ExerciseResponseDto>)response.Value;

            Assert.Equal(3, responseResult.Count);
        }

        [Fact]
        public async Task OnGetExercisesByUserAsync_WhenSuccesful_ShouldReturnResultStatus200()
        {
            _exerciseServiceMock.Setup(t => t.GetExercisesByUserAsync("12345")).ReturnsAsync(DataFixture.GetAllExercise());

            var response = (OkObjectResult)(await _controller.GetExercisesByUserAsync("12345")).Result;

            Assert.Equal(200, response.StatusCode);

        }

        [Fact]
        public async Task OnAddAsync_WhenSuccesful_ShouldReturnExercise()
        {
            ExerciseDto exerciseDto = DataFixture.GetExerciseDto();
            
            Exercise exercise = new Exercise() { Id = Guid.NewGuid(), Name = "test", Type = ExerciseType.weightTraining, User = DataFixture.GetOneUser()};

            _exerciseServiceMock.Setup(t => t.CreateAsync(exerciseDto)).Returns(Task.FromResult<Exercise?>(exercise));

            var response = (OkObjectResult)(await _controller.AddAsync(exerciseDto));
            
            Assert.Equal("Excercise created successfully", response.Value);
        }

        [Fact]
        public async Task OnAddAsync_WhenSuccesful_ShouldReturnResultStatus200()
        {
            ExerciseDto exerciseDto = DataFixture.GetExerciseDto();
            
            Exercise exercise = new Exercise() { Id = Guid.NewGuid(), Name = "test", Type = ExerciseType.weightTraining, User = DataFixture.GetOneUser() };

            _exerciseServiceMock.Setup(t => t.CreateAsync(exerciseDto)).Returns(Task.FromResult<Exercise?>(exercise));

            var response = (OkObjectResult)(await _controller.AddAsync(exerciseDto));

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task OnAddAsync_WhenUnSuccesful_ShouldReturnErrorMessage()
        {
            ExerciseDto exerciseDto = DataFixture.GetExerciseDto();

            _exerciseServiceMock.Setup(t => t.CreateAsync(exerciseDto)).Returns(Task.FromResult<Exercise?>(null));

            var response = (BadRequestObjectResult)(await _controller.AddAsync(exerciseDto));

            Assert.Equal("Excercise cannot be created with supplied input", response.Value);
        }

        [Fact]
        public async Task OnAddAsync_WhenUnSuccesful_ShouldReturnResultStatus400()
        {
            ExerciseDto exerciseDto = DataFixture.GetExerciseDto();

            _exerciseServiceMock.Setup(t => t.CreateAsync(exerciseDto)).Returns(Task.FromResult<Exercise?>(null));

            var response = (BadRequestObjectResult)(await _controller.AddAsync(exerciseDto));

            Assert.Equal(400, response.StatusCode);
        }
    }
}
