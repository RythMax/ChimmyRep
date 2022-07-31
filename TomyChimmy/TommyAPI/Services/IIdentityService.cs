using System.Threading.Tasks;
using TommyAPI.Domain;

namespace TommyAPI.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string Email, string Password);

        Task<AuthenticationResult> LoginAsync(string Email, string Password);
    }
}
