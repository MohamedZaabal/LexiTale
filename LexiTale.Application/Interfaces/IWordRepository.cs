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

    }
}
