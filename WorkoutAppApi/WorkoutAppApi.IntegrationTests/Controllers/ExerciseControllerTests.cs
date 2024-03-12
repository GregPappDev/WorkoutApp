using FluentAssertions;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using WorkoutAppApi.IntegrationTests.Fixtures;
using WorkoutAppApi.Models.DTOs.Excercise;

namespace WorkoutAppApi.IntegrationTests.Controllers
{
    public class ExerciseControllerTests : IClassFixture<DockerWebApplicationFactoryFixture>
    {
        private DockerWebApplicationFactoryFixture _factory;
        private HttpClient _client;

        public ExerciseControllerTests(DockerWebApplicationFactoryFixture factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        [Fact]
        public async Task OnGetAllAsync_WhenExecuteApi_ShouldReturnExcercises()
        {
            // Arrange

            // Act
            var response = await _client.GetAsync(HttpHelper.Urls.GetAllAsync);
            var res = await response.Content.ReadFromJsonAsync<List<ExerciseResponseDto>>();
           
            var result = res.OrderBy(e => e.Name).ThenBy(e => e.UserId).ToList();

            // Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count.Should().Be(4);
            result[0].Name.Should().Be("lunge");
            result[0].ExerciseType.Should().Be("bodyweight");
            result[0].UserId.Should().Be("12345");
        }

        [Fact]
        public async Task OnGetAllActiveAsync_WhenExecuteApi_ShouldReturnOnlyActiveExcercises()
        {
            // Arrange

            // Act

            var response = await _client.GetAsync(HttpHelper.Urls.GetAllActiveAsync);
            var res = await response.Content.ReadFromJsonAsync<List<ExerciseResponseDto>>();

            var result = res.OrderBy(e => e.Name).ThenBy(e => e.UserId).ToList();

            // Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count.Should().Be(2);
            result[0].Name.Should().Be("lunge");
            result[0].ExerciseType.Should().Be("bodyweight");
            result[0].UserId.Should().Be("12345");
        }

        [Fact]
        public async Task OnGetExercisesByUserAsync_WhenExecuteApi_ShouldReturnUsersActiveExcercises()
        {
            // Arrange

            // Act
            var url = $"{HttpHelper.Urls.GetExercisesByUserAsync}{DataFixture.GetUsers()[1].Id}";
            var response = await _client.GetAsync(url);
            var res = await response.Content.ReadFromJsonAsync<List<ExerciseResponseDto>>();

            var result = res.OrderBy(e => e.Name).ThenBy(e => e.UserId).ToList();

            // Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count.Should().Be(2);
            result[0].Name.Should().Be("lunge");
            result[1].Name.Should().Be("push up");
            result[0].ExerciseType.Should().Be("bodyweight");
            result[0].UserId.Should().Be("67890");
        }

        [Fact]
        public async Task OnAddExerciseAsync_WhenExecuteController_ShouldStoreinDb()
        {
            // Arrange 

            var httpContent = new StringContent(JsonConvert.SerializeObject(DataFixture.newExercise), Encoding.UTF8, "application/json");

            // Act

            var request = await _client.PostAsync(HttpHelper.Urls.AddAsync, httpContent);
            var response = await _client.GetAsync(HttpHelper.Urls.GetAllAsync);
            var res = await response.Content.ReadFromJsonAsync<List<ExerciseResponseDto>>();

            var result = res.OrderBy(e => e.Name).ThenBy(e => e.UserId).ToList();

            // Assert
            request.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count.Should().Be(5);
            result[4].Name.Should().Be("squat");
            result[4].ExerciseType.Should().Be("bodyweight");
            result[4].UserId.Should().Be("12345");

        }
    }
}