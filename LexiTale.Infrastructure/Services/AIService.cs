using LexiTale.Application.DTOs;
using LexiTale.Application.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Infrastructure.Services
{
    public class AIService : IAIService
    {
        private readonly HttpClient _httpClient;
        private readonly GeminiSettings _settings;
        public AIService(HttpClient httpClient, IOptions<GeminiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }
        public async Task<string> GenerateStoryAsync(
     string language,
     string level,
     List<string> newWords,
     List<string> oldWords)
        {
        var prompt = $@"
            You are a language teacher.

            Language: {language}

            Student Level: {level}

            New Words:
            {string.Join(", ", newWords)}

            Old Words:
            {string.Join(", ", oldWords)}

            Write a short story suitable for the student's level.

            Rules:
            - Use ALL new words.
            - Use some old words naturally.
            - Make the story easy to understand.
            - After the story, generate 5 comprehension questions.
            - Finally provide the answers.
            ";

            var requestBody = new
            {
              contents = new[]
                    {
                        new
                        {
                            parts = new[]
                            {
                                new
                                {
                                    text = prompt
                                }
                            }
                        }
                    }
            };
            _httpClient.DefaultRequestHeaders.Clear();

            _httpClient.DefaultRequestHeaders.Add(
     "X-goog-api-key",
     _settings.ApiKey);

            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/{_settings.Model}:generateContent";

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);

            var result = await response.Content.ReadAsStringAsync();

            return result;
        }
    }
}
