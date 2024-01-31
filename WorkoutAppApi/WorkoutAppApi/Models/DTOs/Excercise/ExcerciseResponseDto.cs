using WorkoutAppApi.Models.Enums;

namespace WorkoutAppApi.Models.DTOs.Excercise
{
    public class ExcerciseResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ExcerciseType { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
