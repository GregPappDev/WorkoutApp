using WorkoutAppApi.Models;

namespace WorkoutAppApi.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetUserById(string id);
    }
}
