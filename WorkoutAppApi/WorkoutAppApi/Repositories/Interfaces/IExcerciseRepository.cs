using System.Collections;
using WorkoutAppApi.Models;

namespace WorkoutAppApi.Repositories.Interfaces
{
    public interface IExcerciseRepository
    {
        Task<IQueryable<Excercise>> GetAllAsync();
        Task<IQueryable<Excercise>> GetAllActiveAsync();
        Task<IQueryable<Excercise>> GetExcercisesByUserAsync(string id);
        Task<Excercise?> GetExcerciseByIdAsync(Guid id);
        Task Create(Excercise excercise);
        Task UpdateAsync(Excercise excercise);
        Task PermanentlyDeleteAsync(Excercise excercise); 
    }
}
