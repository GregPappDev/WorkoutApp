using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkoutAppApi.IntegrationTests.Helper
{
    internal class HttpHelper
    {
        internal static class Urls
        {
            public readonly static string GetAllAsync = "/api/Exercise"; // implemented
            public readonly static string GetAllActiveAsync = "/api/Exercise/GetAllActive"; // implemented
            public readonly static string GetExercisesByUserAsync = "/api/Exercise/GetExercisesByUser/";
            public readonly static string AddAsync = "api/Exercise/Add"; // implemented
            public readonly static string UpdateAsync = "/api/Exercise/Update";
            public readonly static string DeleteAsync = "/api/Exercise/Delete";
            public readonly static string PermanentlyDeleteAsync = "/api/Exercise/PermanentlyDelete";
        }
    }
}
