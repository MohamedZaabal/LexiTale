using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.DTOs
{
    public class StoryResponse
    {
        public string Story { get; set; } = string.Empty;
        public List<string> Questions { get; set; } = new();
        public List<string> Answers { get; set; } = new();

    }
}
