using LexiTale.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.Interfaces
{
    public interface IAIService
    {
        Task<StoryResponse> GenerateStoryAsync(
            string language,
            string level,
            List<string> newWords,
            List<string> oldWords
        );

        Task<EvaluationResponse> EvaluateAnswersAsync(
    List<string> questions,
    List<string> correctAnswers,
    List<string> userAnswers);
    }
}
