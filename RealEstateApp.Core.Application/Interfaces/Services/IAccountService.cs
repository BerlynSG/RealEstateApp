using RealEstateApp.Core.Application.Dtos.Account;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IAccountService
    {
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<string> ConfirmAccountAsync(string userId, string token);
        Task<RegisterResponse> RegisterClienteUserAsync(RegisterRequest request, string origin);
        Task<RegisterResponse> RegisterAgenteUserAsync(RegisterRequest request, string origin);
        Task SignOutAsync();
    }
}