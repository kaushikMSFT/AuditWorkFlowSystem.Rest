using IdentityService.Domain;
using System.Threading.Tasks;

namespace IdentityService
{
    public interface IIdentityProvider
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}