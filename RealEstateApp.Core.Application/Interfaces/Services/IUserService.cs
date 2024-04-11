using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.User;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<string> ConfirmEmailAsync(string userId, string token);
        Task<RegisterResponse> RegisterClienteAsync(SaveUserViewModel vm, string origin, IFormFile profileImage);
        Task<RegisterResponse> RegisterAgenteAsync(SaveUserViewModel vm, string origin, IFormFile profileImage);
        Task SignOutAsync();
    }
}
