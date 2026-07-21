using LexiTale.Application.DTOs;
using LexiTale.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Infrastructure.Services
{
    public class StoryService : IStoryService
    {
        private readonly IWordRepository _wordRepository;
        private readonly IAIService _aiService;

        public StoryService(
            IWordRepository wordRepository,
            IAIService aiService)
        {
            _wordRepository = wordRepository;
            _aiService = aiService;
        }

        public async Task<string> GenerateAsync(Guid userId, GenerateStoryRequest request)
        {
            var newWords = await _wordRepository.GetNewWordsAsync(userId, request.Language);

            var oldWords = await _wordRepository.GetOldWordsAsync(userId, request.Language, 10);

            return await _aiService.GenerateStoryAsync(
                request.Language,
                request.Level,
                newWords.Select(x => x.Text).ToList(),
                oldWords.Select(x => x.Text).ToList());
        }
    }
}
