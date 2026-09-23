using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.DTOs
{
    public class SubmitStoryRequest
    {
        public StoryResponse Story { get; set; } = new();

        public List<string> UserAnswers { get; set; } = new();
    }
}
