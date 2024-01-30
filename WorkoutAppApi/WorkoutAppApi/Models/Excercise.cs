using WorkoutAppApi.Models.Enums;

namespace WorkoutAppApi.Models
{
    public class Excercise
    {
        public Guid Id { get; set; }
        public required User User { get; set; }
        public required string Name { get; set; }
        public required ExcerciseType Type { get; set; }
        public List<RepsOfExcercise> RepsOfExcercise { get; set; } = new List<RepsOfExcercise>();
        
    }
}
