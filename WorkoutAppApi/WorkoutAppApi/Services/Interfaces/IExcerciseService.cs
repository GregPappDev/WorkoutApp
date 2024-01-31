using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;

namespace WorkoutAppApi.Services.Interfaces
{
    public interface IExcerciseService
    {
        Task<List<ExcerciseResponseDto>> GetAllAsync();
        Task<List<ExcerciseResponseDto>> GetExcercisesByUserAsync(string UserId);
        Task<Excercise?> Create(ExcerciseDto newExcercise);
        Task<Excercise?> Update(ExcerciseDto newExcercise);
        Task<Excercise?> Delete(Guid Id);
        Task<Excercise> PermanentlyDelete(Guid Id);
    }
}
