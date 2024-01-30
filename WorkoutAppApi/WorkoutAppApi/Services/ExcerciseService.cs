using AutoMapper;
using WorkoutAppApi.Data;
using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;
using WorkoutAppApi.Repositories.Interfaces;
using WorkoutAppApi.Services.Interfaces;

namespace WorkoutAppApi.Services
{
    public class ExcerciseService : IExcerciseService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IExcerciseRepository _excerciseRepository;
        public ExcerciseService(IMapper mapper, IUserRepository userRepository, IExcerciseRepository excerciseRepository) 
        { 
            _mapper = mapper;
            _userRepository = userRepository;
            _excerciseRepository = excerciseRepository;
        }
        public async Task<Excercise?> Create(ExcerciseDto newExcercise)
        {
            var currentUser = await _userRepository.GetUserById(newExcercise.UserId);
            if (currentUser == null) { return null; }
            
            Excercise excercise = new Excercise() 
            { 
                Name = newExcercise.Name, 
                Type = newExcercise.Type,
                User = currentUser
            
            };

            await _excerciseRepository.Create(excercise);
            
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
