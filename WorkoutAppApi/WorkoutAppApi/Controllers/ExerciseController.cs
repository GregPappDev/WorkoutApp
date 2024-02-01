using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WorkoutAppApi.Models;
using WorkoutAppApi.Models.DTOs.Excercise;
using WorkoutAppApi.Services.Interfaces;

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

            if (excercise == null) { return BadRequest("Excercise cannot be created with supplied input"); }

            return Ok("Excercise created successfully");
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateExerciseDto excerciseDto)
        {
            var excercise = await _service.Update(id, excerciseDto);

            if (excercise == null) { return BadRequest("Excercise cannot be created with supplied input"); }

            return Ok("Excercise updated successfully");
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var excercise = await _service.Delete(id);

            if (excercise == null) { return BadRequest("Excercise cannot be created with supplied input"); }

            return Ok("Excercise deleted successfully");
        }

        [HttpDelete("[action]")]
        public async Task<ActionResult> PermanentlyDelete(Guid id)
        {
            var excercise = await _service.PermanentlyDelete(id);

            if (excercise == null) { return BadRequest("Excercise cannot be created with supplied input"); }

            return Ok("Excercise deleted successfully");
        }
    }
}
