using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;

namespace WorkoutAppApi.Services.Interfaces
{
    public interface IExerciseService
    {
        Task<List<ExerciseResponseDto>> GetAllAsync();
        Task<List<ExerciseResponseDto>> GetAllActiveAsync();
        Task<List<ExerciseResponseDto>> GetExercisesByUserAsync(string UserId);
        Task<Exercise?> CreateAsync(ExerciseDto newExcercise);
        Task<Exercise?> Update(Guid id, UpdateExerciseDto excerciseDto);
        Task<Exercise?> Delete(Guid Id);
        Task<Exercise?> PermanentlyDelete(Guid Id);
    }
}
