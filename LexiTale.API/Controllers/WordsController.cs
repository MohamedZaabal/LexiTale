using LexiTale.Application.DTOs;
using LexiTale.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LexiTale.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WordsController : ControllerBase
    {
        private readonly IWordService _wordService;
        public WordsController(IWordService wordService)
        {
            _wordService = wordService;
            
        }
        [HttpGet]
        public async Task<IActionResult> GetWords()
        {
            var userId=Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var words = await _wordService.GetWordsAsync(userId);
            return Ok(words);
        }
        [HttpPost]
        public async Task<IActionResult> AddWord(CreateWordRequest request)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _wordService.AddWordAsync(userId, request);
            return Ok(new { message = "Word added successfully" });
        }
    }
}
