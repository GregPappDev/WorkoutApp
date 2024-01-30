namespace WorkoutAppApi.Models
{
    public class Workout
    {
        public Guid Id { get; set; }
        public required User User { get; set; }
        public required string Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<RepsOfExcercise> SetsAndReps { get; set; } = new List<RepsOfExcercise>();

    }
}
