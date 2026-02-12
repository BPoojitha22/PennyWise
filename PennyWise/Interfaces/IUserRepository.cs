using System.Threading.Tasks;
using PennyWise.Models.Entities;

namespace PennyWise.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByUserNameAsync(string userName);
        Task<User?> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task<bool> UserNameExistsAsync(string userName);
        Task SaveChangesAsync();
    }
}

