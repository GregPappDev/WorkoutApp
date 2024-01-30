using WorkoutAppApi.Models.DTOs.Excercise;

namespace WorkoutAppApi.Utils
{
    public static class Validation<T>
    {
        public static bool ValidateNotNull(T input) 
        { 
            return input != null;
        }


    }
}
