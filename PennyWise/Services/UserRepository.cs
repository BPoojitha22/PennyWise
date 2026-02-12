using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PennyWise.Data;
using PennyWise.Interfaces;
using PennyWise.Models.Entities;

namespace PennyWise.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<User?> GetByUserNameAsync(string userName)
        {
            return _context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public Task<User?> GetByIdAsync(int id)
        {
            return _context.Users.FindAsync(id).AsTask();
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public Task<bool> UserNameExistsAsync(string userName)
        {
            return _context.Users.AnyAsync(u => u.UserName == userName);
        }

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}

