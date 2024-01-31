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
    public class ExcerciseController : ControllerBase
    {
        private readonly IExcerciseService _service;

        public ExcerciseController(IExcerciseService service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<ActionResult<IEnumerable<Excercise>>> GetAll()
        {
            var excercises = await _service.GetAllAsync();

            return Ok(excercises);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<IEnumerable<Excercise>>> GetAllActive()
        {
            var excercises = await _service.GetAllActiveAsync();

            return Ok(excercises);
        }

        [HttpGet("ExcercisesByUser/{id}")]
        public async Task<ActionResult<IEnumerable<Excercise>>> GetExcercisesByUser(string id)
        {
            var excercises = await _service.GetExcercisesByUserAsync(id);

            return Ok(excercises);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> Add([FromBody]ExcerciseDto excerciseDto)
        {
            var excercise = await _service.Create(excerciseDto);

            if (excercise == null) { return BadRequest("Excercise cannot be created with supplied input"); }

            return Ok("Excercise created successfully");
        }

        [HttpPut("Update/{id}")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UpdateExcerciseDto excerciseDto)
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
