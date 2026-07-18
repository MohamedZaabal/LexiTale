using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Domain.Entities
{
    public class Word
    {
        public Guid Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public bool IsNewlyLearned { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime?LastReviewedAt { get; set; }


        public Guid UsrId { get; set; }
        public User User { get; set; } = null!;

    }
}
