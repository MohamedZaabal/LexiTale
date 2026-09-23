using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.DTOs
{
    public class EvaluateAnswersRequest
    {
        public List<string> Questions { get; set; } = new();
        public List<string> CorrectAnswers { get; set; } = new();
        public List<string> UserAnswers { get; set; } = new();
    }
}
