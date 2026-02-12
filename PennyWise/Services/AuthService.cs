using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using PennyWise.Interfaces;
using PennyWise.Models.Entities;

namespace PennyWise.Services
{
    // Simple auth service with SHA256 hashing (for demo purposes only)
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly PennyWise.Data.ApplicationDbContext _context;

        public AuthService(IUserRepository userRepository, PennyWise.Data.ApplicationDbContext context)
        {
            _userRepository = userRepository;
            _context = context;
        }

        public async Task<User?> RegisterAsync(string userName, string email, string password, int currencyId)
        {
            if (await _userRepository.UserNameExistsAsync(userName))
            {
                return null;
            }

            var user = new User
            {
                UserName = userName,
                Email = email,
                PasswordHash = HashPassword(password),
                CurrencyId = currencyId
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            // Load navigation property using explicit loading
            try 
            {
                // We need to attach if it's not tracked properly or reload
                if (_context.Entry(user).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    _context.Users.Attach(user);
                }
                await _context.Entry(user).Reference(u => u.Currency).LoadAsync();
            }
            catch 
            {
                // Fallback or log if needed
            }

            return user;
        }

        public async Task<User?> ValidateUserAsync(string userName, string password)
        {
            var user = await _userRepository.GetByUserNameAsync(userName);
            if (user == null)
            {
                return null;
            }

            var hash = HashPassword(password);
            if (!string.Equals(hash, user.PasswordHash, StringComparison.Ordinal))
            {
                return null;
            }
            
            // Ensure currency is loaded
            try 
            {
                 if (_context.Entry(user).State == Microsoft.EntityFrameworkCore.EntityState.Detached)
                {
                    _context.Users.Attach(user);
                }
                await _context.Entry(user).Reference(u => u.Currency).LoadAsync();
            }
            catch
            {
                // ignore
            }

            return user;
        }

        public ClaimsPrincipal CreateClaimsPrincipal(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            if (user.Currency != null)
            {
                claims.Add(new Claim("CurrencySymbol", user.Currency.Symbol));
                claims.Add(new Claim("CurrencyCode", user.Currency.Code));
            }
            else 
            {
                 // Fallback
                 claims.Add(new Claim("CurrencySymbol", "$"));
            }

            var identity = new ClaimsIdentity(claims, "Cookies");
            return new ClaimsPrincipal(identity);
        }

        private static string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}

