namespace WorkoutAppApi.Models.DTOs.Excercise
{
    public class UpdateExcerciseDto
    {
        public required string Name { get; set; }
        public required int ExcerciseType { get; set; }
    }
}
