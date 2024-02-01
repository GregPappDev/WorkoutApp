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
    public class ExerciseService : IExerciseService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IExerciseRepository _excerciseRepository;
        public ExerciseService(IMapper mapper, IUserRepository userRepository, IExerciseRepository excerciseRepository) 
        { 
            _mapper = mapper;
            _userRepository = userRepository;
            _excerciseRepository = excerciseRepository;
        }

        public async Task<List<ExerciseResponseDto>> GetAllAsync()
        {
            var result = await _excerciseRepository.GetAllAsync();
            var response = await result
                .Select(x => new ExerciseResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ExerciseType = x.Type.ToString(),
                    UserId = x.User.Id,
                })
                .ToListAsync();
            return response;
        }

        public async Task<List<ExerciseResponseDto>> GetAllActiveAsync()
        {
            var result = await _excerciseRepository.GetAllActiveAsync();
            var response = await result
                .Select(x => new ExerciseResponseDto
                {
                    Id = x.Id,
                    Name = x.Name,
                    ExerciseType = x.Type.ToString(),
                    UserId = x.User.Id,
                })
                .ToListAsync();
            return response;
        }

        public async Task<List<ExerciseResponseDto>> GetExercisesByUserAsync(string UserId)
        {
            var result = await _excerciseRepository.GetExcercisesByUserAsync(UserId);
            var response = await result
                .Select(x => new ExerciseResponseDto { 
                    Id = x.Id,
                    Name = x.Name,
                    ExerciseType = x.Type.ToString(),
                    UserId = x.User.Id,
                })
                .ToListAsync();
            return response;
        }

        public async Task<Exercise?> CreateAsync(ExerciseDto newExcercise)
        {
            // Validate input to check if supplied UserId exists in database
            var currentUser = await _userRepository.GetUserById(newExcercise.UserId);
            if (currentUser == null) { return null; }

            // Validate input to check if supplied excercise type exists in ExcerciseType enum
            if (!ValidationService<ExerciseDto>.ValidateExerciseTypeAvailability(newExcercise.ExerciseType)) { return null; }
            
            Exercise excercise = new Exercise() 
            { 
                Name = newExcercise.Name, 
                Type = (ExerciseType)newExcercise.ExerciseType,
                User = currentUser
            
            };

            await _excerciseRepository.CreateAsync(excercise);
            
            return excercise;
        }

        public async Task<Exercise?> UpdateAsync(Guid id, UpdateExerciseDto excerciseDto)
        {
            // Validate if excercise with 'id' exists
            var excerciseToUpdate = await _excerciseRepository.GetExcerciseByIdAsync(id);
            if (excerciseToUpdate == null) { return null; }

            
            // Validate input to check if supplied excercise type exists in ExcerciseType enum
            if (!ValidationService<ExerciseDto>.ValidateExerciseTypeAvailability(excerciseDto.ExerciseType)) { return null; }

            excerciseToUpdate.Name = excerciseDto.Name;
            excerciseToUpdate.Type = (ExerciseType)excerciseDto.ExerciseType;
                       
            await _excerciseRepository.UpdateAsync(excerciseToUpdate);

            return excerciseToUpdate;
        }

        public async Task<Exercise?> Delete(Guid Id)
        {            
            Exercise? excercise = await _excerciseRepository.GetExcerciseByIdAsync(Id);

            if(excercise == null) { return null; }
             
            excercise.IsDeleted = true;
            await _excerciseRepository.UpdateAsync(excercise);
            
            return excercise;
        }

        
        public async Task<Exercise?> PermanentlyDelete(Guid Id)
        {
            Exercise? excercise = await _excerciseRepository.GetExcerciseByIdAsync(Id);

            if (excercise == null) { return null; }
                        
            await _excerciseRepository.PermanentlyDeleteAsync(excercise);

            return excercise;
        }
    }
}
