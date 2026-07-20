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

        public async Task<List<Word>> GetWordsAsync(Guid userId)
        {
         
            return await _wordRepository.GetAllAsync(userId);
        }
    }
}
