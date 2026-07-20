using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.DTOs
{
    public class CreateWordRequest
    {
        public string Text { get; set; }=string.Empty;
        public string Language { get; set; } = string.Empty;
        public bool IsNewlyLearned { get; set; }
    }
}
