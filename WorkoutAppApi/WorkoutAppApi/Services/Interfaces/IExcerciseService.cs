using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;

namespace WorkoutAppApi.Services.Interfaces
{
    public interface IExcerciseService
    {
        Task<List<Excercise>> GetAllAsync();
        Task<List<ExcerciseResponseDto>> GetExcercisesByUserAsync(string UserId);
        Task<Excercise?> Create(ExcerciseDto newExcercise);
        Task<Excercise> Update(Guid Id, ExcerciseDto newExcercise);
        Task<Excercise> Delete(Guid Id);
    }
}
