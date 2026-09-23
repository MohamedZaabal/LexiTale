using LexiTale.Application.DTOs;
using LexiTale.Application.Interfaces;

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

        public async Task<StoryResponse> GenerateAsync(
            Guid userId,
            GenerateStoryRequest request)
        {
            var newWords = await _wordRepository.GetNewWordsAsync(
                userId,
                request.Language);
            if (newWords.Count == 0)
            {
                throw new Exception(
                    "You don't have any new words to practice. Add new words first.");
            }

            var oldWords = await _wordRepository.GetOldWordsAsync(
                userId,
                request.Language,
                newWords.Count);

            var story = await _aiService.GenerateStoryAsync(
                request.Language,
                request.Level,
                newWords.Select(x => x.Text).ToList(),
                oldWords.Select(x => x.Text).ToList());

            foreach (var word in newWords)
            {
                word.IsNewlyLearned = false;
                _wordRepository.Update(word);
            }

            await _wordRepository.SaveChangesAsync();

            return story;
        }
    }
}