using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.Interfaces
{
    public interface IAIService
    {
        Task<string> GenerateStoryAsync(
            string language,
            string level,
            List<string> newWords,
            List<string> oldWords
        );
    }
}
