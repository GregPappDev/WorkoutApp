using WorkoutAppApi.Models.Enums;

namespace WorkoutAppApi.Models.DTOs.Excercise
{
    public class ExcerciseDto
    {
        public required User User { get; set; }
        public required string Name { get; set; }
        public required ExcerciseType Type { get; set; }
    }
}
