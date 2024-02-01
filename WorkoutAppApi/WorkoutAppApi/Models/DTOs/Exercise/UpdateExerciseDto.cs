namespace WorkoutAppApi.Models.DTOs.Excercise
{
    public class UpdateExerciseDto
    {
        public required string Name { get; set; }
        public required int ExerciseType { get; set; }
    }
}
