using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models;

namespace WorkoutAppApi.IntegrationTests.Fixtures
{
    public class WebApplicationFactoryFixture : IAsyncLifetime
    {
        private WebApplicationFactory<Program> _factory;
        public HttpClient Client { get; private set; }
        private User user1 = new User() { Id = "12345", Deleted = false };
        private User user2 = new User() { Id = "67890", Deleted = false };

        public WebApplicationFactoryFixture() 
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

            Client = _factory.CreateClient();
        }

        async Task IAsyncLifetime.InitializeAsync()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopeService = scope.ServiceProvider;
                var dbContext = scopeService.GetRequiredService<DataContext>();


                await dbContext.Database.EnsureCreatedAsync();
                await dbContext.Exercises.AddAsync(new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "lunge",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = user1,
                    IsDeleted = false,
                });
                await dbContext.Exercises.AddAsync(new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "push up",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = user1,
                    IsDeleted = true,
                });
                await dbContext.Exercises.AddAsync(new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "push up",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = user2,
                    IsDeleted = true,
                });
                await dbContext.SaveChangesAsync();
            }
        }

        

        async Task IAsyncLifetime.DisposeAsync()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopeService = scope.ServiceProvider;
                var dbContext = scopeService.GetRequiredService<DataContext>();

                await dbContext.Database.EnsureDeletedAsync();
            }
        }
    }
}
