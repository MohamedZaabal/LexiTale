using LexiTale.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Domain.Entities
{
    public class Exercise
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Language { get; set; } = string.Empty;
        public string GeneratedContent { get; set; }=string.Empty;
        public DateTime CreatedAt { get; set; }


        public User User { get; set; } = null;
        public CEFRLevel Level { get; set; }
    }
}
