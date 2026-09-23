using LexiTale.Application.DTOs;
using LexiTale.Application.Interfaces;
using LexiTale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Infrastructure.Services
{
    public class WordService : IWordService
    {
        private readonly  IWordRepository _wordRepository;
        public WordService(IWordRepository wordRepository)
        {
            _wordRepository = wordRepository;
        }

        public async Task AddMultipleAsync(Guid userId, AddWordsRequest request)
        {
            if (request.Words == null || request.Words.Count == 0)
                throw new Exception("Please provide at least one word.");

            foreach (var text in request.Words)
            {
                if (string.IsNullOrWhiteSpace(text))
                    continue;

                var word = new Word
                {
                    Id = Guid.NewGuid(),
                    Text = text.Trim(),
                    Language = request.Language,
                    IsNewlyLearned = true,
                    DateAdded = DateTime.UtcNow,
                    LastReviewedAt = null,
                    UserId = userId
                };

                await _wordRepository.AddAsync(word);
            }

            await _wordRepository.SaveChangesAsync();

        }

        public async Task AddWordAsync(Guid userId, CreateWordRequest request)
        {
            var word = new Word
            {
                Id = Guid.NewGuid(),
                Text = request.Text,
                Language = request.Language,
                IsNewlyLearned = request.IsNewlyLearned,
                DateAdded = DateTime.UtcNow,
                UserId = userId
            };
            await  _wordRepository.AddAsync(word);
            await _wordRepository.SaveChangesAsync();
        }

        public async Task DeleteWordAsync(Guid userId, Guid wordId)
        {
            var word = await _wordRepository.GetByIdAsync(wordId);

            if (word == null || word.UserId != userId)
                throw new Exception("Word not found.");

            _wordRepository.Delete(word);

            await _wordRepository.SaveChangesAsync();
        }

        public async Task<List<Word>> GetWordsAsync(Guid userId)
        {
         
            return await _wordRepository.GetAllAsync(userId);
        }

        public async Task UpdateWordAsync(Guid userId, Guid wordId, UpdateWordRequest request)
        {
            var word = await _wordRepository.GetByIdAsync(wordId);

            if (word == null || word.UserId != userId)
                throw new Exception("Word not found.");

            word.Text = request.Text;
            word.Language = request.Language;
            word.IsNewlyLearned = request.IsNewlyLearned;

            _wordRepository.Update(word);

            await _wordRepository.SaveChangesAsync();
        }
    }
}
