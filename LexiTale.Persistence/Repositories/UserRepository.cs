using LexiTale.Application.Interfaces;
using LexiTale.Domain.Entities;
using LexiTale.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LexiTale.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext  _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        
        }
        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
          return await _context.RefreshTokens
                .Include(x=>x.User)
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }


        }
    }

