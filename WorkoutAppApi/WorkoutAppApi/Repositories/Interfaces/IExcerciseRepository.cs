using System.Collections;
using WorkoutAppApi.Models;

namespace WorkoutAppApi.Repositories.Interfaces
{
    public interface IExcerciseRepository
    {
        Task<IQueryable<Excercise>> GetAllAsync();
        Task<IQueryable<Excercise>> GetExcercisesByUserAsync(string id);
        Task Create(Excercise excercise);
    }
}
