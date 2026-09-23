using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.DTOs
{
    public class AddWordsRequest
    {
        [Required]
        public string Language { get; set; } = string.Empty;
        [Required]
        public List<string> Words { get; set; } = new();
    }
}
