using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.DTOs
{
    public class StoryResultResponse
    {
        public int TotalQuestions { get; set; }

        public int CorrectAnswers { get; set; }

        public int Score { get; set; }

        public List<bool> Results { get; set; } = new();
    }
}
