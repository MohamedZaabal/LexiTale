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

        public AIService(
            HttpClient httpClient,
            IOptions<GeminiSettings> settings)
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

Write a medium-length story suitable for the student's level.

Rules:
- The story should be around 250-350 words.
- Use ALL new words.
- Use some old words naturally.
- Keep the vocabulary and grammar appropriate for the student's level.
- Generate exactly 5 questions.
- Generate exactly 5 answers.
- Each answer must match its corresponding question.
- Return ONLY valid JSON.
- Do NOT wrap the JSON inside markdown.
- Do NOT add any text before or after the JSON.

Return exactly this JSON structure:

{{
    ""story"": ""Your story here"",
    ""questions"": [
        ""Question 1"",
        ""Question 2"",
        ""Question 3"",
        ""Question 4"",
        ""Question 5""
    ],
    ""answers"": [
        ""Answer 1"",
        ""Answer 2"",
        ""Answer 3"",
        ""Answer 4"",
        ""Answer 5""
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

            _httpClient.DefaultRequestHeaders.Add(
                "X-goog-api-key",
                _settings.ApiKey);

            var url =
                $"https://generativelanguage.googleapis.com/v1beta/models/{_settings.Model}:generateContent";

            HttpResponseMessage? response = null;
            string responseContent = string.Empty;

            // Retry up to 3 times if Gemini returns 503
            for (int attempt = 1; attempt <= 3; attempt++)
            {
                response = await _httpClient.PostAsJsonAsync(
                    url,
                    requestBody);

                responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    break;
                }

                if ((int)response.StatusCode != 503 || attempt == 3)
                {
                    throw new Exception(
                        $"Gemini API Error: {(int)response.StatusCode} {response.StatusCode}\n" +
                        $"Response: {responseContent}");
                }

                // Wait before retrying: 1s, then 2s
                int delaySeconds = attempt;

                Console.WriteLine(
                    $"Gemini returned 503. Retrying in {delaySeconds} second(s)...");

                await Task.Delay(
                    TimeSpan.FromSeconds(delaySeconds));
            }

            using var document = JsonDocument.Parse(responseContent);

            var json = document.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            if (string.IsNullOrWhiteSpace(json))
            {
                throw new Exception("Gemini returned empty response.");
            }

            // Remove markdown if Gemini returned ```json ... ```
            json = json
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            Console.WriteLine("========== GEMINI RESPONSE ==========");
            Console.WriteLine(json);
            Console.WriteLine("====================================");

            try
            {
                var story = JsonSerializer.Deserialize<StoryResponse>(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                if (story == null)
                {
                    throw new Exception("Failed to parse Gemini response.");
                }

                return story;
            }
            catch (JsonException ex)
            {
                throw new Exception(
                    $"Gemini returned invalid JSON.\n" +
                    $"Response:\n{json}\n\n" +
                    $"Error:\n{ex.Message}");
            }
        }



            public async Task<EvaluationResponse> EvaluateAnswersAsync(
    List<string> questions,
    List<string> correctAnswers,
    List<string> userAnswers)
        {
            var prompt = $@"
Evaluate the student's answers.

Questions:
{string.Join("\n", questions.Select((q, i) => $"{i + 1}. {q}"))}

Correct Answers:
{string.Join("\n", correctAnswers.Select((a, i) => $"{i + 1}. {a}"))}

Student Answers:
{string.Join("\n", userAnswers.Select((a, i) => $"{i + 1}. {a}"))}

Rules:
- Compare meaning, not exact wording.
- Accept answers with different wording if they have the same meaning.
- Mark an answer false if its meaning is incorrect.
- Return ONLY valid JSON.

Return exactly:
{{
    ""results"": [true, false, true, true, false],
    ""correctAnswers"": 3,
    ""score"": 60
}}
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

            var response = await _httpClient.PostAsJsonAsync(
                url,
                requestBody);

            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(
                    $"Gemini API Error: {(int)response.StatusCode} {response.StatusCode}\n" +
                    $"Response: {responseContent}");
            }

            using var document = JsonDocument.Parse(responseContent);

            var json = document.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();

            if (string.IsNullOrWhiteSpace(json))
                throw new Exception("Gemini returned empty response.");

            json = json
                .Replace("```json", "")
                .Replace("```", "")
                .Trim();

            var result = JsonSerializer.Deserialize<EvaluationResponse>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            if (result == null)
                throw new Exception("Failed to parse evaluation response.");

            return result;
        }
    }
    }
