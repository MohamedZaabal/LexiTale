using LexiTale.Application.Features.Authentication.DTOs;
using LexiTale.Application.Interfaces;
using LexiTale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        public AuthService( IUserRepository userRepository)
        {
            _userRepository = userRepository;
            
        }
        public Task<string> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            //mapping 

            var user = new User
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                CurretLevel=request.CEFRLevel,
                CreatedAt = DateTime.UtcNow
            };
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
        }
    }
}
