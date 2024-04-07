using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.User;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<RegisterResponse> RegisterClienteAsync(SaveUserViewModel vm, string origin);
        Task<RegisterResponse> RegisterAgenteAsync(SaveUserViewModel vm, string origin);

        Task SignOutAsync();
    }
}