namespace WorkoutAppApi.Models
{
    public class ScheduledWorkout : Workout
    {
        public DateTime? Date { get; set; }
        public string Notes { get; set; } = string.Empty;
        public DateTime? StartofWorkout { get; set; }
        public DateTime? EndOfWorkout { get; set; }

    }
}
