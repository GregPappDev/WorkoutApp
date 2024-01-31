using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;
using WorkoutAppApi.Models.Enums;
using WorkoutAppApi.Repositories.Interfaces;
using WorkoutAppApi.Services.Interfaces;
using WorkoutAppApi.Utils;

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

        public async Task<List<ExcerciseResponseDto>> GetAllAsync()
        {
            var result = await _excerciseRepository.GetAllAsync();
            var response = await result
                .Select(x => new ExcerciseResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ExcerciseType = x.Type.ToString(),
                    UserId = x.User.Id,
                })
                .ToListAsync();
            return response;
        }

        public async Task<List<ExcerciseResponseDto>> GetExcercisesByUserAsync(string UserId)
        {
            var result = await _excerciseRepository.GetExcercisesByUserAsync(UserId);
            var response = await result
                .Select(x => new ExcerciseResponseDto { 
                    Id = x.Id,
                    Name = x.Name,
                    ExcerciseType = x.Type.ToString(),
                    UserId = x.User.Id,
                })
                .ToListAsync();
            return response;
        }

        public async Task<Excercise?> Create(ExcerciseDto newExcercise)
        {
            // Validate input to check if supplied UserId exists in database
            var currentUser = await _userRepository.GetUserById(newExcercise.UserId);
            if (currentUser == null) { return null; }

            // Validate input to check if supplied excercise type exists in ExcerciseType enum
            if (!ValidationService<ExcerciseDto>.ValidateExcerciseTypeAvailability(newExcercise)) { return null; }
            
            Excercise excercise = new Excercise() 
            { 
                Name = newExcercise.Name, 
                Type = (ExcerciseType)newExcercise.ExcerciseType,
                User = currentUser
            
            };

            await _excerciseRepository.Create(excercise);
            
            return excercise;
        }

        public Task<Excercise> Delete(Guid Id)
        {
            throw new NotImplementedException();
        }

        

        public Task<Excercise> Update(Guid Id, ExcerciseDto newExcercise)
        {
            throw new NotImplementedException();
        }



    }
}
