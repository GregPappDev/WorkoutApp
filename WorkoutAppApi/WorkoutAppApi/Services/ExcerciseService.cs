using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WorkoutAppApi.Data;
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
