using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.DTOs
{
    public class AddWordsRequest
    {
        public string Language { get; set; } = string.Empty;

        public List<string> Words { get; set; } = new();
    }
}
