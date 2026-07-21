using LexiTale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.Interfaces
{
    public interface IWordRepository
    {
        Task AddAsync(Word word);
        Task<List<Word>> GetAllAsync(Guid userId);
        Task SaveChangesAsync();
        Task<Word?> GetByIdAsync(Guid id);

        void Update(Word word);

        void Delete(Word word);
        Task<List<Word>> GetNewWordsAsync(Guid userId, string language);

        Task<List<Word>> GetOldWordsAsync(Guid userId, string language, int count);
    }
}
