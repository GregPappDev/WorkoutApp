using WorkoutAppApi.Models;

namespace WorkoutAppApi.Repositories.Interfaces
{
    public interface IExcerciseRepository
    {
        Task Create(Excercise excercise);
    }
}
