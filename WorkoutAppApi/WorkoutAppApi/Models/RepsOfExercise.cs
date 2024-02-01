namespace WorkoutAppApi.Models
{
    public class RepsOfExercise
    {
        public Guid Id { get; set; }
        public required Exercise Excercise { get; set; }
        public required Workout Workout { get; set; }
        public int? PlannedWeight { get; set; }
        public int? PlannedReps { get; set; }
        public int? ActualWeight { get; set; }
        public int? ActualReps { get; set; }
        public int? RestTime { get; set; }
        public int? Duration { get; set; }
        public bool Completed { get; set; } = false;

    }
}
