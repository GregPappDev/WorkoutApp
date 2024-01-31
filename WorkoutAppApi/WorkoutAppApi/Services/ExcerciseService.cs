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

        public async Task<List<ExcerciseResponseDto>> GetAllActiveAsync()
        {
            var result = await _excerciseRepository.GetAllActiveAsync();
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
            if (!ValidationService<ExcerciseDto>.ValidateExcerciseTypeAvailability(newExcercise.ExcerciseType)) { return null; }
            
            Excercise excercise = new Excercise() 
            { 
                Name = newExcercise.Name, 
                Type = (ExcerciseType)newExcercise.ExcerciseType,
                User = currentUser
            
            };

            await _excerciseRepository.Create(excercise);
            
            return excercise;
        }

        public async Task<Excercise?> Update(Guid id, UpdateExcerciseDto excerciseDto)
        {
            // Validate if excercise with 'id' exists
            var excerciseToUpdate = await _excerciseRepository.GetExcerciseByIdAsync(id);
            if (excerciseToUpdate == null) { return null; }

            
            // Validate input to check if supplied excercise type exists in ExcerciseType enum
            if (!ValidationService<ExcerciseDto>.ValidateExcerciseTypeAvailability(excerciseDto.ExcerciseType)) { return null; }

            excerciseToUpdate.Name = excerciseDto.Name;
            excerciseToUpdate.Type = (ExcerciseType)excerciseDto.ExcerciseType;
                       
            await _excerciseRepository.UpdateAsync(excerciseToUpdate);

            return excerciseToUpdate;
        }

        public async Task<Excercise?> Delete(Guid Id)
        {            
            Excercise? excercise = await _excerciseRepository.GetExcerciseByIdAsync(Id);

            if(excercise == null) { return null; }
             
            excercise.IsDeleted = true;
            await _excerciseRepository.UpdateAsync(excercise);
            
            return excercise;
        }

        
        public async Task<Excercise?> PermanentlyDelete(Guid Id)
        {
            Excercise? excercise = await _excerciseRepository.GetExcerciseByIdAsync(Id);

            if (excercise == null) { return null; }
                        
            await _excerciseRepository.PermanentlyDeleteAsync(excercise);

            return excercise;
        }
    }
}
