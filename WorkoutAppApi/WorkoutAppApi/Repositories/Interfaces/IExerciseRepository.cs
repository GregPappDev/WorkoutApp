using System.Collections;
using WorkoutAppApi.Models;

namespace WorkoutAppApi.Repositories.Interfaces
{
    public interface IExerciseRepository
    {
        Task<IQueryable<Exercise>> GetAllAsync();
        Task<IQueryable<Exercise>> GetAllActiveAsync();
        Task<IQueryable<Exercise>> GetExcercisesByUserAsync(string id);
        Task<Exercise?> GetExcerciseByIdAsync(Guid id);
        Task CreateAsync(Exercise excercise);
        Task UpdateAsync(Exercise excercise);
        Task PermanentlyDeleteAsync(Exercise excercise); 
    }
}
