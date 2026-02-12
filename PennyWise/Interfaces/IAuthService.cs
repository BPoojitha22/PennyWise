using System.Security.Claims;
using System.Threading.Tasks;
using PennyWise.Models.Entities;

namespace PennyWise.Interfaces
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(string userName, string email, string password, int currencyId);
        Task<User?> ValidateUserAsync(string userName, string password);
        ClaimsPrincipal CreateClaimsPrincipal(User user);
    }
}

