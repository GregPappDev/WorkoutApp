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
using WorkoutAppApi.Utils;

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

        //
        //  Endpoint: GetAllAsync
        //

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


        //
        //  Endpoint: GetAllActiveAsync
        //

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


        //
        //  Endpoint: GetExercisesByUser
        //

        [Fact]
        public async Task OnGetExercisesByUserAsync_WhenSuccesful_ShouldReturn3Excercises()
        {
            string userId = "12345";

            _exerciseServiceMock.Setup(t => t.GetExercisesByUserAsync(userId)).ReturnsAsync(DataFixture.GetAllExercise());

            var response = (OkObjectResult)(await _controller.GetExercisesByUserAsync(userId)).Result;
            var responseResult = (List<ExerciseResponseDto>)response.Value;

            Assert.Equal(3, responseResult.Count);
        }

        [Fact]
        public async Task OnGetExercisesByUserAsync_WhenSuccesful_ShouldReturnResultStatus200()
        {
            string userId = "12345";

            _exerciseServiceMock.Setup(t => t.GetExercisesByUserAsync(userId)).ReturnsAsync(DataFixture.GetAllExercise());

            var response = (OkObjectResult)(await _controller.GetExercisesByUserAsync(userId)).Result;

            Assert.Equal(200, response.StatusCode);

        }


        //
        //  Endpoint: AddAsync
        //

        [Fact]
        public async Task OnAddAsync_WhenSuccesful_ShouldReturnOK()
        {
            ExerciseDto exerciseDto = DataFixture.GetExerciseDto();

            _exerciseServiceMock.Setup(t => t.CreateAsync(exerciseDto)).Returns(Task.FromResult<Exercise?>(DataFixture.GetExercise()));

            var response = (OkObjectResult)(await _controller.AddAsync(exerciseDto));
            
            Assert.Equal(ResponseMessage.createdSuccessfully, response.Value);
        }

        [Fact]
        public async Task OnAddAsync_WhenSuccesful_ShouldReturnResultStatus200()
        {
            var exerciseDto = DataFixture.GetExerciseDto();

            _exerciseServiceMock.Setup(t => t.CreateAsync(exerciseDto)).Returns(Task.FromResult<Exercise?>(DataFixture.GetExercise()));

            var response = (OkObjectResult)(await _controller.AddAsync(exerciseDto));

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task OnAddAsync_WhenUnSuccesful_ShouldReturnErrorMessage()
        {
            ExerciseDto exerciseDto = DataFixture.GetExerciseDto();

            _exerciseServiceMock.Setup(t => t.CreateAsync(exerciseDto)).Returns(Task.FromResult<Exercise?>(null));

            var response = (BadRequestObjectResult)(await _controller.AddAsync(exerciseDto));

            Assert.Equal(ResponseMessage.cannotBeCreated, response.Value);
        }

        [Fact]
        public async Task OnAddAsync_WhenUnSuccesful_ShouldReturnResultStatus400()
        {
            ExerciseDto exerciseDto = DataFixture.GetExerciseDto();

            _exerciseServiceMock.Setup(t => t.CreateAsync(exerciseDto)).Returns(Task.FromResult<Exercise?>(null));

            var response = (BadRequestObjectResult)(await _controller.AddAsync(exerciseDto));

            Assert.Equal(400, response.StatusCode);
        }

        //
        //  Endpoint: UpdateAsync
        //

        [Fact]
        public async Task OnUpdateAsync_WhenSuccesful_ShouldReturnOKMessage()
        {
            var id = Guid.NewGuid();
            var updateExerciseDto = DataFixture.GetUpdateExerciseDto();

            _exerciseServiceMock.Setup(t => t.UpdateAsync(id, updateExerciseDto)).Returns(Task.FromResult<Exercise?>(DataFixture.GetExercise()));

            var response = (OkObjectResult)(await _controller.UpdateAsync(id, updateExerciseDto));

            Assert.Equal(ResponseMessage.updatedSuccessfully, response.Value);
        }

        [Fact]
        public async Task OnUpdateAsync_WhenSuccesful_ShouldReturnResultStatus200()
        {
            var id = Guid.NewGuid();
            var updateExerciseDto = DataFixture.GetUpdateExerciseDto();
           
            _exerciseServiceMock.Setup(t => t.UpdateAsync(id, updateExerciseDto)).Returns(Task.FromResult<Exercise?>(DataFixture.GetExercise()));

            var response = (OkObjectResult)(await _controller.UpdateAsync(id, updateExerciseDto));

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task OnUpdateAsync_WhenSuccesful_ShouldReturnErrorMessage()
        {
            var id = Guid.NewGuid();
            var updateExerciseDto = DataFixture.GetUpdateExerciseDto();
            
            _exerciseServiceMock.Setup(t => t.UpdateAsync(id, updateExerciseDto)).Returns(Task.FromResult<Exercise?>(null));

            var response = (BadRequestObjectResult)(await _controller.UpdateAsync(id, updateExerciseDto));

            Assert.Equal(ResponseMessage.cannotBeUpdated, response.Value);
        }

        [Fact]
        public async Task OnUpdateAsync_WhenSuccesful_ShouldReturnResultStatus400()
        {
            var id = Guid.NewGuid();
            var updateExerciseDto = DataFixture.GetUpdateExerciseDto();
            
            _exerciseServiceMock.Setup(t => t.UpdateAsync(id, updateExerciseDto)).Returns(Task.FromResult<Exercise?>(null));

            var response = (BadRequestObjectResult)(await _controller.UpdateAsync(id, updateExerciseDto));

            Assert.Equal(400, response.StatusCode);
        }

        //
        //  Endpoint: DeleteAsync
        //

        [Fact]
        public async Task OnDeleteAsync_WhenSuccesful_ShouldReturnOKMessage()
        {
            var id = Guid.NewGuid();
            
            _exerciseServiceMock.Setup(t => t.DeleteAsync(id)).Returns(Task.FromResult<Exercise?>(DataFixture.GetExercise()));

            var response = (OkObjectResult)(await _controller.DeleteAsync(id));

            Assert.Equal(ResponseMessage.deletedSuccessfully, response.Value);
        }

        [Fact]
        public async Task OnDeleteAsync_WhenSuccesful_ShouldReturnResultStatus200()
        {
            var id = Guid.NewGuid() ;

            _exerciseServiceMock.Setup(t => t.DeleteAsync(id)).Returns(Task.FromResult<Exercise?>(DataFixture.GetExercise()));

            var response = (OkObjectResult)(await _controller.DeleteAsync(id));

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task OnDeleteAsync_WhenUnSuccesful_ShouldReturnErrorMessage()
        {
            var id = Guid.NewGuid();

            _exerciseServiceMock.Setup(t => t.DeleteAsync(id)).Returns(Task.FromResult<Exercise?>(null));

            var response = (BadRequestObjectResult)(await _controller.DeleteAsync(id));

            Assert.Equal(ResponseMessage.cannotBeDeleted, response.Value);
        }

        [Fact]
        public async Task OnDeleteAsync_WhenUnSuccesful_ShouldReturnResultStatus400()
        {
            var id = Guid.NewGuid();

            _exerciseServiceMock.Setup(t => t.DeleteAsync(id)).Returns(Task.FromResult<Exercise?>(null));

            var response = (BadRequestObjectResult)(await _controller.DeleteAsync(id));

            Assert.Equal(400, response.StatusCode);
        }


        //
        //  Endpoint: PermanentlyDeleteAsync
        //

        [Fact]
        public async Task OnPermanentlyDeleteAsync_WhenSuccesful_ShouldReturnOKMessage()
        {
            var id = Guid.NewGuid();

            _exerciseServiceMock.Setup(t => t.PermanentlyDeleteAsync(id)).Returns(Task.FromResult<Exercise?>(DataFixture.GetExercise()));

            var response = (OkObjectResult)(await _controller.PermanentlyDeleteAsync(id));

            Assert.Equal(ResponseMessage.deletedSuccessfully, response.Value);
        }

        [Fact]
        public async Task OnPermanentlyDeleteAsync_WhenSuccesful_ShouldReturnResultStatus200()
        {
            var id = Guid.NewGuid();

            _exerciseServiceMock.Setup(t => t.PermanentlyDeleteAsync(id)).Returns(Task.FromResult<Exercise?>(DataFixture.GetExercise()));

            var response = (OkObjectResult)(await _controller.PermanentlyDeleteAsync(id));

            Assert.Equal(200, response.StatusCode);
        }

        [Fact]
        public async Task OnPermanentlyDeleteAsync_WhenUnSuccesful_ShouldReturnErrorMessage()
        {
            var id = Guid.NewGuid();

            _exerciseServiceMock.Setup(t => t.PermanentlyDeleteAsync(id)).Returns(Task.FromResult<Exercise?>(null));

            var response = (BadRequestObjectResult)(await _controller.PermanentlyDeleteAsync(id));

            Assert.Equal(ResponseMessage.cannotBeDeleted, response.Value);
        }

        [Fact]
        public async Task OnPermanentlyDeleteAsync_WhenUnSuccesful_ShouldReturnResultStatus400()
        {
            var id = Guid.NewGuid();

            _exerciseServiceMock.Setup(t => t.PermanentlyDeleteAsync(id)).Returns(Task.FromResult<Exercise?>(null));

            var response = (BadRequestObjectResult)(await _controller.PermanentlyDeleteAsync(id));

            Assert.Equal(400, response.StatusCode);
        }

    }
}
