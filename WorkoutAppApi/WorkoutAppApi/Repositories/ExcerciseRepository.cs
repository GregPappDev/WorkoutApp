using Microsoft.EntityFrameworkCore;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models;
using WorkoutAppApi.Repositories.Interfaces;

namespace WorkoutAppApi.Repositories
{
    public class ExcerciseRepository : IExcerciseRepository
    {
        private readonly DataContext _context;
        public ExcerciseRepository(DataContext context) 
        {
            _context = context;
        }
        public async Task Create(Excercise excercise)
        {
            await _context.Excercises.AddAsync(excercise);
            await _context.SaveChangesAsync();
        }
    }
}
