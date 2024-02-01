using WorkoutAppApi.Models.Enums;

namespace WorkoutAppApi.Models
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public required User User { get; set; }
        public required string Name { get; set; }
        public required ExerciseType Type { get; set; }
        public List<RepsOfExercise> RepsOfExercise { get; set; } = new List<RepsOfExercise>();
        public bool IsDeleted { get; set; } = false;
        
    }
}
