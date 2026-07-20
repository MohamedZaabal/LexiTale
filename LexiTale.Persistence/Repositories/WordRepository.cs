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
    }
}
