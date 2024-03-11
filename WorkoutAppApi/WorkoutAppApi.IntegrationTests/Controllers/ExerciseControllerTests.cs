using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Newtonsoft.Json;
using System.Net.Http.Json;
using System.Text;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;
using WorkoutAppApi.Models.Enums;
using Xunit.Abstractions;

namespace WorkoutAppApi.IntegrationTests.Controllers
{
    public class ExerciseControllerTests
    {
        private WebApplicationFactory<Program> _factory;
        private readonly HttpClient client;
        private User user1 = new User() { Id = "12345", Deleted = false };
        private User user2 = new User() { Id = "67890", Deleted = false };
        private readonly ExerciseDto newExercise = new ExerciseDto()
        {
            UserId = "12345",
            Name = "squat",
            ExerciseType = 0,
        };

        public ExerciseControllerTests()
        {
            _factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll(typeof(DbContextOptions<DataContext>));
                    services.AddDbContext<DataContext>(options =>
                    {
                        options.UseInMemoryDatabase("test");
                    });
                });
            });

            using (var scope = _factory.Services.CreateScope())
            {
                var scopeService = scope.ServiceProvider;
                var dbContext = scopeService.GetRequiredService<DataContext>();



                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Exercises.Add(new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "lunge",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = user1,
                    IsDeleted = false,
                });
                dbContext.Exercises.Add(new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "push up",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = user1,
                    IsDeleted = true,
                });
                dbContext.Exercises.Add(new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "push up",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = user2,
                    IsDeleted = true,
                });
                dbContext.SaveChanges();
            }

            client = _factory.CreateClient();

        }

        [Fact]
        public async Task OnGetAllAsync_WhenExecuteApi_ShouldReturnExcercises()
        {
            // Arrange
                        
            

            // Act

            var response = await client.GetAsync(HttpHelper.Urls.GetAllAsync);
            var result = await response.Content.ReadFromJsonAsync<List<ExerciseResponseDto>>();

            // Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count.Should().Be(3);
            result[0].Name.Should().Be("lunge");
            result[0].ExerciseType.Should().Be("bodyweight");
            result[0].UserId.Should().Be("12345");
        }

        [Fact]
        public async Task OnGetAllActiveAsync_WhenExecuteApi_ShouldReturnOnlyActiveExcercises()
        {
            // Arrange
                        
            // Act

            var response = await client.GetAsync(HttpHelper.Urls.GetAllActiveAsync);
            var result = await response.Content.ReadFromJsonAsync<List<ExerciseResponseDto>>();

            // Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count.Should().Be(1);
            result[0].Name.Should().Be("lunge");
            result[0].ExerciseType.Should().Be("bodyweight");
            result[0].UserId.Should().Be("12345");
        }

        [Fact]
        public async Task OnGetExercisesByUserAsync_WhenExecuteApi_ShouldReturnUsersActiveExcercises()
        {
            // Arrange
            
            // Act
            var url = $"{HttpHelper.Urls.GetExercisesByUserAsync}{user1.Id}";
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadFromJsonAsync<List<ExerciseResponseDto>>();

            // Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count.Should().Be(2);
            result[0].Name.Should().Be("lunge");
            result[1].Name.Should().Be("push up");
            result[0].ExerciseType.Should().Be("bodyweight");
            result[0].UserId.Should().Be("12345");
        }

        [Fact]
        public async Task OnAddExerciseAsync_WhenExecuteController_ShouldStoreinDb()
        {
            // Arrange 

            var httpContent = new StringContent(JsonConvert.SerializeObject(newExercise), Encoding.UTF8, "application/json");

            // Act

            var request = await client.PostAsync(HttpHelper.Urls.AddAsync, httpContent);
            var response = await client.GetAsync(HttpHelper.Urls.GetAllAsync);
            var result = await response.Content.ReadFromJsonAsync<List<ExerciseResponseDto>>();

            // Assert
            request.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count.Should().Be(4);
            result[3].Name.Should().Be("squat");
            result[3].ExerciseType.Should().Be("bodyweight");
            result[3].UserId.Should().Be("12345");

        }
    }
}