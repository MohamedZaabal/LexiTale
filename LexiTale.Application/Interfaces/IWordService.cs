using LexiTale.Application.DTOs;
using LexiTale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.Interfaces
{
    public interface IWordService
    {
        Task AddWordAsync(Guid userId, CreateWordRequest request);
        Task<List<Word>> GetWordsAsync(Guid userId);

        Task UpdateWordAsync(Guid userId, Guid wordId, UpdateWordRequest request);

        Task DeleteWordAsync(Guid userId, Guid wordId);

        Task AddMultipleAsync(Guid userId,AddWordsRequest request);
    }
}
