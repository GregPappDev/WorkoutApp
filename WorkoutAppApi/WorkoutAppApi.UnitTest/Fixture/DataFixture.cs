using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;

namespace WorkoutAppApi.UnitTests.Fixture
{
    internal class DataFixture
    {
        internal static List<ExerciseResponseDto> GetAllExercise()
        {
            return new List<ExerciseResponseDto>
            {
                new ExerciseResponseDto
                {
                    Name = "row",
                    ExerciseType = "bodyweight",
                    UserId = "12345",
                },
                new ExerciseResponseDto
                {
                    Name = "push up",
                    ExerciseType = "bodyweight",
                    UserId = "12345"
                },
                new ExerciseResponseDto
                {
                    Name = "pull up",
                    ExerciseType = "bodyweight",
                    UserId = "12345"
                },
            };
        }

        public static ExerciseResponseDto GetExerciseResponseDto()
        {
            return new ExerciseResponseDto
            {
                Name = "row",
                ExerciseType = "bodyweight",
                UserId = "12345",
            };
        }

        public static ExerciseDto GetExerciseDto()
        {
            return new ExerciseDto
            {
                Name = "row",
                ExerciseType = 0,
                UserId = "12345",
            };
        }

        internal static User GetOneUser()
        {
            return new User() { Id = "12345", Deleted = false };
        }
    }
}
