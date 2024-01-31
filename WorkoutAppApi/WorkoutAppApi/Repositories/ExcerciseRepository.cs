using Microsoft.EntityFrameworkCore;
using System.Collections;
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
                
        public async Task<IQueryable<Excercise>> GetAllAsync()
        {
            return await Task.FromResult(_context.Excercises.Include(x => x.User));
        }

        public async Task<IQueryable<Excercise>> GetAllActiveAsync()
        {
            return await Task.FromResult(_context.Excercises
                .Where(x => x.IsDeleted == false)
                .Include(x => x.User));
        }

        public async Task<IQueryable<Excercise>> GetExcercisesByUserAsync(string id)
        {            
            return await Task.FromResult(_context.Excercises.Where(excercise => excercise.User.Id == id).Include(x => x.User));
        }

        public async Task<Excercise?> GetExcerciseByIdAsync(Guid id)
        {
            return await _context.Excercises.FirstOrDefaultAsync(excercise => excercise.Id == id);
        }

        public async Task UpdateAsync(Excercise excercise)
        {
            _context.Update(excercise);
            await _context.SaveChangesAsync();
        }

        public async Task PermanentlyDeleteAsync(Excercise excercise)
        {
            _context.Remove(excercise);
            await _context.SaveChangesAsync();
        }
                
    }
}
