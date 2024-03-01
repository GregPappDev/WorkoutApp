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
using Xunit.Abstractions;

namespace WorkoutAppApi.IntegrationTests.Controllers
{
    public class ExerciseControllerTests
    {
        private WebApplicationFactory<Program> _factory;

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
        }

        [Fact]
        public async Task OnGetAllAsync_WhenExecuteApi_ShouldReturnExcercises()
        {
            // Arrange
                        
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
                    User = new User() { Id = "12345", Deleted = false }
                }); 
                dbContext.SaveChanges();
            }

            var client = _factory.CreateClient();

            // Act

            var response = await client.GetAsync(HttpHelper.Urls.GetAllAsync);
            var result = await response.Content.ReadFromJsonAsync<List<ExerciseResponseDto>>();

            // Assert

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count.Should().Be(1);
            result[0].Name.Should().Be("lunge");
            result[0].ExerciseType.Should().Be("bodyweight");
            result[0].UserId.Should().Be("12345");
        }

        [Fact]
        public async Task OnGetAllActiveAsync_WhenExecuteApi_ShouldReturnOnlyActiveExcercises()
        {
            // Arrange

            using (var scope = _factory.Services.CreateScope())
            {
                var scopeService = scope.ServiceProvider;
                var dbContext = scopeService.GetRequiredService<DataContext>();

                var currentUser = new User() { Id = "12345", Deleted = false };

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Exercises.Add(new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "lunge",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = currentUser,
                    IsDeleted = false,
                });
                dbContext.Exercises.Add(new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "push up",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = currentUser,
                    IsDeleted = true,
                });
                dbContext.SaveChanges();
            }

            var client = _factory.CreateClient();

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
        public async Task OnAddExercise_WhenExecuteController_ShouldStoreinDb()
        {
            // Arrange 

            using (var scope = _factory.Services.CreateScope())
            {
                var scopeService = scope.ServiceProvider;
                var dbContext = scopeService.GetRequiredService<DataContext>();

                dbContext.Database.EnsureDeleted();
                dbContext.Database.EnsureCreated();
                dbContext.Users.Add(new User() { Id = "12345", Deleted = false });
                dbContext.SaveChanges();
                
            }

            var client = _factory.CreateClient();

            var newExercise = new ExerciseDto()
            {
                UserId = "12345",
                Name = "lunge",
                ExerciseType = 0,
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(newExercise), Encoding.UTF8, "application/json");

            // Act

            var request = await client.PostAsync(HttpHelper.Urls.AddAsync, httpContent);
            var response = await client.GetAsync(HttpHelper.Urls.GetAllAsync);
            var result = await response.Content.ReadFromJsonAsync<List<ExerciseResponseDto>>();

            // Assert
            request.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count.Should().Be(1);
            result[0].Name.Should().Be("lunge");
            result[0].ExerciseType.Should().Be("bodyweight");
            result[0].UserId.Should().Be("12345");

        }
    }
}