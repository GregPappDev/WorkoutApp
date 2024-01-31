using Microsoft.EntityFrameworkCore;
using System.Collections;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models;
using WorkoutAppApi.Repositories.Interfaces;

namespace WorkoutAppApi.Repositories
{
    public class ExerciseRepository : IExerciseRepository
    {
        private readonly DataContext _context;
        public ExerciseRepository(DataContext context) 
        {
            _context = context;
        }
        public async Task Create(Exercise excercise)
        {
            await _context.Excercises.AddAsync(excercise);
            await _context.SaveChangesAsync();
        }
                
        public async Task<IQueryable<Exercise>> GetAllAsync()
        {
            return await Task.FromResult(_context.Excercises.Include(x => x.User));
        }

        public async Task<IQueryable<Exercise>> GetAllActiveAsync()
        {
            return await Task.FromResult(_context.Excercises
                .Where(x => x.IsDeleted == false)
                .Include(x => x.User));
        }

        public async Task<IQueryable<Exercise>> GetExcercisesByUserAsync(string id)
        {            
            return await Task.FromResult(_context.Excercises.Where(excercise => excercise.User.Id == id).Include(x => x.User));
        }

        public async Task<Exercise?> GetExcerciseByIdAsync(Guid id)
        {
            return await _context.Excercises.FirstOrDefaultAsync(excercise => excercise.Id == id);
        }

        public async Task UpdateAsync(Exercise excercise)
        {
            _context.Update(excercise);
            await _context.SaveChangesAsync();
        }

        public async Task PermanentlyDeleteAsync(Exercise excercise)
        {
            _context.Remove(excercise);
            await _context.SaveChangesAsync();
        }
                
    }
}
