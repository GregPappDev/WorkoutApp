using System;
using WorkoutAppApi.Models.DTOs.Excercise;
using WorkoutAppApi.Models.Enums;

namespace WorkoutAppApi.Utils
{
    public static class ValidationService<T>
    {

    public static bool ValidateExerciseTypeAvailability(int index)
        {
            return Enum.GetValues(typeof(ExerciseType)).Cast<int>().Max() >= index;
           
        }
    }
}
