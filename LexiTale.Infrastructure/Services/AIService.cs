using LexiTale.Application.DTOs;
using LexiTale.Application.Interfaces;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using System.Text.Json;

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

        public async Task<StoryResponse> GenerateStoryAsync(
            string language,
            string level,
            List<string> newWords,
            List<string> oldWords)
        {


            #region Prompt
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
- Return ONLY valid JSON.
- Do NOT wrap the JSON inside markdown.

Return exactly this JSON:

{{
    ""story"": """",
    ""questions"": [
        """",
        """",
        """",
        """",
        """"
    ],
    ""answers"": [
        """",
        """",
        """",
        """",
        """"
    ]
}}
";

            #endregion

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
            _httpClient.DefaultRequestHeaders.Add("X-goog-api-key", _settings.ApiKey);

            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/{_settings.Model}:generateContent";

            var response = await _httpClient.PostAsJsonAsync(url, requestBody);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsStringAsync();

            using var document = JsonDocument.Parse(result);

            var json = document.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            if (string.IsNullOrWhiteSpace(json))
                throw new Exception("Gemini returned empty response.");

            var story = JsonSerializer.Deserialize<StoryResponse>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (story == null)
                throw new Exception("Failed to parse Gemini response.");

            return story;
        }
    }
}