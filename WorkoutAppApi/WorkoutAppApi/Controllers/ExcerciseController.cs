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
        private readonly IExcercise _service;

        public ExcerciseController(IExcercise service)
        {
            _service = service;
        }

        [HttpPost("[action]")]
        public async Task<ActionResult<Excercise>> AddExcercise(ExcerciseDto excerciseDto)
        {
            var excercise = await _service.Create(excerciseDto);

            if (excercise == null) { return BadRequest("User is not registered"); }

            return Ok(excercise);
        }
    }
}
