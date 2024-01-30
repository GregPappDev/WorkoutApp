namespace WorkoutAppApi.Models
{
    public class User
    {
        public string Id { get; set; } = string.Empty;
        public List<Workout> Workout { get; set; } = new List<Workout>();
        public List<ScheduledWorkout> ScheduledWorkouts { get; set;} = new List<ScheduledWorkout>();
        public List<Excercise> Excercises { get; set; } = new List<Excercise>();
        public bool Deleted { get; set; } = false;
    }
}
