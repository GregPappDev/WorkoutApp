using System;

namespace WorkoutAppApi.Utils
{
    public static class Validation<T>
    {
        public static bool ValidateNotNull(T input) 
        { 
            return input != null;
        }

        public static bool ValidateInputType<T>(object input)
        {
            return input is T;
        }
    }
}
