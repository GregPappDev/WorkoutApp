using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using WorkoutAppApi.Data;

namespace WorkoutAppApi.IntegrationTests.Fixtures
{
    public class WebApplicationFactoryFixture : IAsyncLifetime
    {
        private WebApplicationFactory<Program> _factory;
        public HttpClient Client { get; private set; }

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
                await dbContext.Exercises.AddRangeAsync(DataFixture.GetExercises());
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
