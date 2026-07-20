using LexiTale.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Domain.Entities
{
    public class User
    {
        public Guid Id {  get; set; }
        public string Name { get; set; }=string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; }=string.Empty;
        public DateTime CreatedAt { get; set; }


        //---

        public CEFRLevel CurretLevel { get; set; }
        public ICollection<Word> Words { get; set; } = new List<Word>();
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();

        public ICollection<RefreshToken> RefreshTokens { get; set; }
            =new List<RefreshToken>();
    }
}
