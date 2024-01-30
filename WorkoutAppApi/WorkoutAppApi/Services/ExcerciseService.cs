using AutoMapper;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;
using WorkoutAppApi.Services.Interfaces;

namespace WorkoutAppApi.Services
{
    public class ExcerciseService : IExcerciseService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _context;
        public ExcerciseService(IMapper mapper, DataContext context) 
        { 
            _mapper = mapper;
            _context = context;
        }
        public async Task<Excercise?> Create(ExcerciseDto newExcercise)
        {
            var currentUser = _context.Users.FirstOrDefault(user => user.Id == newExcercise.UserId);
            if (currentUser == null) { return null; }
            
            Excercise excercise = new Excercise() 
            { 
                Name = newExcercise.Name, 
                Type = newExcercise.Type,
                User = currentUser
            
            };

            await _context.Excercises.AddAsync(excercise);
            await _context.SaveChangesAsync();
            
            return excercise;
        }

        public Task<Excercise> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Excercise>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Excercise>> GetUserExcercises(string UserId)
        {
            throw new NotImplementedException();
        }

        public Task<Excercise> Update(Guid Id, ExcerciseDto newExcercise)
        {
            throw new NotImplementedException();
        }
    }
}
