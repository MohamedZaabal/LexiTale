using LexiTale.Application.DTOs;
using LexiTale.Application.Interfaces;

namespace LexiTale.Infrastructure.Services
{
    public class ExerciseService : IExerciseService
    {
        private readonly IAIService _aiService;

        public ExerciseService(IAIService aiService)
        {
            _aiService = aiService;
        }

        public async Task<StoryResultResponse> SubmitAsync(
            SubmitStoryRequest request)
        {
            if (request.Story == null)
                throw new Exception("Story is required.");

            if (request.UserAnswers == null)
                throw new Exception("Answers are required.");

            if (request.UserAnswers.Count != request.Story.Questions.Count)
                throw new Exception(
                    "The number of answers must match the number of questions.");

            var evaluation = await _aiService.EvaluateAnswersAsync(
                request.Story.Questions,
                request.Story.Answers,
                request.UserAnswers);

            return new StoryResultResponse
            {
                TotalQuestions = request.Story.Questions.Count,
                CorrectAnswers = evaluation.CorrectAnswers,
                Score = evaluation.Score,
                Results = evaluation.Results
            };
        }
    }
}