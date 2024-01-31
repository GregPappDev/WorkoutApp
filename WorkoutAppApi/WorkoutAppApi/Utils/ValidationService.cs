using System;
using WorkoutAppApi.Models.DTOs.Excercise;
using WorkoutAppApi.Models.Enums;

namespace WorkoutAppApi.Utils
{
    public static class ValidationService<T>
    {

    public static bool ValidateExcerciseTypeAvailability(int index)
        {
            return Enum.GetValues(typeof(ExcerciseType)).Cast<int>().Max() >= index;
           
        }
    }
}
