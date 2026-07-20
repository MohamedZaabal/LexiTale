using LexiTale.Application.DTOs;
using LexiTale.Application.Interfaces;
using LexiTale.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        public AuthService( IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }
        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user= await _userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new Exception("Invalid email or password.");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password.");
            }
            
            var accesssToken = _jwtService.GenerateAccessToken(user);

            var refreshToken =new RefreshToken
            {
                //mapping
                Token = GenerateRefreshToken(),
                ExpiresOn = DateTime.UtcNow.AddDays(7),
                UserId=user.Id,
                IsRevoked=false

            };
            user.RefreshTokens.Add(refreshToken);
            await _userRepository.SaveChangesAsync();
            return new AuthResponse 
            {

            AccessToken = accesssToken,
            RefreshToken=refreshToken.Token

            };




        }

        public async Task RegisterAsync(RegisterRequest request)
        {
            //validation
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new Exception("User with this email already exists.");
            }

            if(request.Password.Length < 6)
            {
                throw new Exception("Password must be at least 6 characters.");
            }
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
        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString() + Guid.NewGuid().ToString();
        }

        public async Task<AuthResponse> RefreshTokenAsync(RefreshTokenRequest requset)
        {
            var refreshToken = await _userRepository.GetRefreshTokenAsync(requset.RefreshToken);
            if (refreshToken is null)
                throw new Exception("Invalid Refresh Token.");

            if (refreshToken.IsRevoked)
                throw new Exception("Refresh Token is revoked.");

            if (refreshToken.ExpiresOn <= DateTime.UtcNow)
                throw new Exception("Refresh Token expired.");

            var accessToken = _jwtService.GenerateAccessToken(refreshToken.User);

            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
        }
    }
}
