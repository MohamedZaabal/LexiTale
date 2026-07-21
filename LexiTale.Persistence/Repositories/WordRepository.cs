using LexiTale.Application.Interfaces;
using LexiTale.Domain.Entities;
using LexiTale.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Persistence.Repositories
{
    public class WordRepository : IWordRepository
    {
        private readonly AppDbContext _context;
        public WordRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Word word)
        {
           await _context.Words.AddAsync(word);
        }

        public Task<List<Word>> GetAllAsync(Guid userId)
        {
            return _context.Words
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        public async Task<Word?> GetByIdAsync(Guid id)
        {
            return await _context.Words.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(Word word)
        {
            _context.Words.Update(word);
        }

        public void Delete(Word word)
        {
            _context.Words.Remove(word);
        }

        public async Task<List<Word>> GetNewWordsAsync(Guid userId, string language)
        {
            return await _context.Words
                .Where(x =>
                    x.UserId == userId &&
                    x.Language == language &&
                    x.IsNewlyLearned)
                .ToListAsync();
        }

        public async Task<List<Word>> GetOldWordsAsync(Guid userId, string language, int count)
        {
            return await _context.Words
                .Where(x =>
                    x.UserId == userId &&
                    x.Language == language &&
                    !x.IsNewlyLearned)
                .Take(count)
                .ToListAsync();
        }
    }
}
