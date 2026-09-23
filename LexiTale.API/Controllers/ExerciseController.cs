using LexiTale.Application.DTOs;
using LexiTale.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LexiTale.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ExerciseController : ControllerBase
    {
        private readonly IExerciseService _exerciseService;

        public ExerciseController(
            IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit(
            [FromBody] SubmitStoryRequest request)
        {
            var result = await _exerciseService.SubmitAsync(request);

            return Ok(result);
        }
    }
}