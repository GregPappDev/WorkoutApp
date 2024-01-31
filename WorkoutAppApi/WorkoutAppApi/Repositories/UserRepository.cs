using Microsoft.EntityFrameworkCore;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models;
using WorkoutAppApi.Repositories.Interfaces;

namespace WorkoutAppApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context) 
        { 
            _context = context;
        }
        public async Task<User?> GetUserById(string id)
        {
            return await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
        }
    }
}
