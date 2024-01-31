using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        [HttpPost("[action]")]
        public async Task<ActionResult> AddExcercise([FromBody]ExcerciseDto excerciseDto)
        {
            var excercise = await _service.Create(excerciseDto);

            if (excercise == null) { return BadRequest("Excercise cannot be created with supplied input"); }

            return Ok("Excercise created successfully");
        }
    }
}
