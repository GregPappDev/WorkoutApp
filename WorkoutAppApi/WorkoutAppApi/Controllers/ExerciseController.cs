using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;
using WorkoutAppApi.Services.Interfaces;
using WorkoutAppApi.Utils;

namespace WorkoutAppApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _service;

        public ExerciseController(IExerciseService service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetAllAsync()
        {
            var excercises = await _service.GetAllAsync();

            return Ok(excercises);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetAllActiveAsync()
        {
            var excercises = await _service.GetAllActiveAsync();

            return Ok(excercises);
        }

        [HttpGet("ExcercisesByUser/{id}")]
        public async Task<ActionResult<IEnumerable<Exercise>>> GetExercisesByUserAsync(string id)
        {
            var excercises = await _service.GetExercisesByUserAsync(id);

            return Ok(excercises);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> AddAsync([FromBody]ExerciseDto excerciseDto)
        {
            var excercise = await _service.CreateAsync(excerciseDto);

            if (excercise == null) { return BadRequest(ResponseMessage.cannotBeCreated); }

            return Ok(ResponseMessage.createdSuccessfully);
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] UpdateExerciseDto excerciseDto)
        {
            var excercise = await _service.UpdateAsync(id, excerciseDto);

            if (excercise == null) { return BadRequest(ResponseMessage.cannotBeUpdated); }

            return Ok(ResponseMessage.updatedSuccessfully);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> DeleteAsync(Guid id)
        {
            var excercise = await _service.DeleteAsync(id);

            if (excercise == null) { return BadRequest(ResponseMessage.cannotBeDeleted); }

            return Ok(ResponseMessage.deletedSuccessfully);
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> PermanentlyDeleteAsync(Guid id)
        {
            var excercise = await _service.PermanentlyDeleteAsync(id);

            if (excercise == null) { return BadRequest(ResponseMessage.cannotBeDeleted); }

            return Ok(ResponseMessage.deletedSuccessfully);
        }
    }
}
