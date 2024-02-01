using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;

namespace WorkoutAppApi.IntegrationTests.Controllers
{
    public class ExerciseControllerTests
    {
        

        public ExerciseControllerTests()
        {
            
        }

        [Fact]
        public async Task OnGetAllAsync_WhenExecuteApi_ShouldReturnExcercises()
        {
            var factory = new WebApplicationFactory<Program>().WithWebHostBuilder(builder =>
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
            
            using (var scope = factory.Services.CreateScope())
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

            var client = factory.CreateClient();

            var response = await client.GetAsync("/api/Exercise");
            var result = await response.Content.ReadFromJsonAsync<List<ExerciseResponseDto>>();

            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            result.Count.Should().Be(1);
            result[0].Name.Should().Be("lunge");
            result[0].ExerciseType.Should().Be("bodyweight");
            result[0].UserId.Should().Be("12345");
        }
    }
}