using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;

namespace WorkoutAppApi.IntegrationTests.Fixtures
{
    public class DataFixture
    {

        public static List<User> GetUsers()
        {
            return new List<User>()
            {
                new User() { Id = "12345", Deleted = false },
                new User() { Id = "67890", Deleted = false }
            };
        }

        public static List<Exercise> GetExercises()
        {
            List<User> userList = GetUsers();
            return new List<Exercise>()
            {
                new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "lunge",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = userList[0],
                    IsDeleted = false,
                },
                new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "push up",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = userList[0],
                    IsDeleted = true,
                },
                new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "lunge",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = userList[1],
                    IsDeleted = false,
                },
                new Exercise()
                {
                    Id = Guid.NewGuid(),
                    Name = "push up",
                    Type = Models.Enums.ExerciseType.bodyweight,
                    User = userList[1],
                    IsDeleted = true,
                }
            };
        }

        public static ExerciseDto newExercise = new ExerciseDto()
        {
            UserId = "12345",
            Name = "squat",
            ExerciseType = 0,
        };
    }
}
