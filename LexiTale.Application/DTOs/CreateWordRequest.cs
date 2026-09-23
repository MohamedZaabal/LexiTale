using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Application.DTOs
{
    public class CreateWordRequest
    {
        [Required]
        public string Text { get; set; } = string.Empty;

        [Required]
        public string Language { get; set; } = string.Empty;

        public bool IsNewlyLearned { get; set; }
    }
}
